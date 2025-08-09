namespace MartianRobots
{
    internal sealed class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Orientation O { get; private set; }
        public bool IsLost { get; private set; }

        public Robot(int x, int y, Orientation o)
        {
            X = x; Y = y; O = o;
        }

        public void TurnLeft() { if (!IsLost) O = O.TurnLeft(); }
        public void TurnRight() { if (!IsLost) O = O.TurnRight(); }

        public void Forward(World world)
        {
            if (IsLost) return;

            var (dx, dy) = O.ForwardDelta();
            int nx = X + dx, ny = Y + dy;

            if (world.IsInside(nx, ny))
            {
                X = nx; Y = ny;
                return;
            }

            // Would fall off—check scent at the current tile+orientation.
            if (world.HasScent(X, Y, O))
            {
                // Ignore this instruction.
                return;
            }

            // First robot to fall here in this orientation: leave scent and mark lost.
            world.LeaveScent(X, Y, O);
            IsLost = true;
        }

        public override string ToString()
            => $"{X} {Y} {O}{(IsLost ? " LOST" : "")}";
    }
}