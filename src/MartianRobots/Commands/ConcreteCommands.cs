namespace MartianRobots
{
    sealed class LeftCommand : IRobotCommand
    {
        public void Execute(Robot robot, World world) => robot.TurnLeft();
    }

    sealed class RightCommand : IRobotCommand
    {
        public void Execute(Robot robot, World world) => robot.TurnRight();
    }

    sealed class ForwardCommand : IRobotCommand
    {
        public void Execute(Robot robot, World world) => robot.Forward(world);
    }
}