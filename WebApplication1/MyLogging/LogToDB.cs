namespace WebApplication1.MyLogging
{
    public class LogToDB : IMyLogger
    {
        public void Log(string message)
        {
            // Logic to log the message to a database
            // This is a placeholder implementation
            Console.WriteLine($"LogToDB: {message}");
        }
    }
}
