namespace MartianRobots
{
    // Command framework for easy extensibility
    interface IRobotCommand
    {
        void Execute(Robot robot, World world);
    }
}