using System;
using System.IO;
using Xunit;

namespace TestMartianRobots
{
    public class MartianRobotTests
    {
        private static string Normalize(string s) => s.Replace("\r\n", "\n").Replace("\r", "\n").TrimEnd('\n');

        private static string RunWithInput(string input)
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;
            try
            {
                using var reader = new StringReader(input);
                using var writer = new StringWriter();
                Console.SetIn(reader);
                Console.SetOut(writer);

                MartianRobots.Program.Main();

                return Normalize(writer.ToString());
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Sample_input_produces_expected_output()
        {
            var input = string.Join("\n", new[]
            {
                "5 3",
                "1 1 E",
                "RFRFRFRF",
                "3 2 N",
                "FRRFLLFFRRFLL",
                "0 3 W",
                "LLFFFLFLFL"
            }) + "\n";

            var expected = Normalize(string.Join("\n", new[]
            {
                "1 1 E",
                "3 3 N LOST",
                "2 3 S"
            }));

            var actual = RunWithInput(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Invalid_grid_line_throws_InvalidDataException()
        {
            var badGrid = string.Join("\n", new[]
            {
                "5",          // missing Y value
                "1 1 E",
                "F"
            }) + "\n";

            Assert.Throws<InvalidDataException>(() => RunWithInput(badGrid));
        }

        [Fact]
        public void Non_numeric_grid_values_throw_FormatException()
        {
            var badGrid = string.Join("\n", new[]
            {
            "A B",        // non-numeric
            "1 1 E",
            "F"
        }) + "\n";

            Assert.Throws<FormatException>(() => RunWithInput(badGrid));
        }

        [Fact]
        public void Invalid_orientation_in_position_throws_InvalidDataException()
        {
            var badPos = string.Join("\n", new[]
            {
            "5 3",
            "1 1 X",     // X is not a valid orientation
            "F"
        }) + "\n";

            Assert.Throws<InvalidDataException>(() => RunWithInput(badPos));
        }

        [Fact]
        public void Unknown_instruction_characters_are_ignored()
        {
            var input = string.Join("\n", new[]
            {
            "2 2",
            "0 0 N",
            "FXRZL?F"     // X, Z, ? should be ignored
        }) + "\n";

            var output = RunWithInput(input);
            Assert.Equal("0 2 N", output);
        }
    }
}
