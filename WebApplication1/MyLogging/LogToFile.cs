namespace WebApplication1.MyLogging
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            // Logic to log the message to a file
            // This is a placeholder implementation
            Console.WriteLine($"LogToFile: {message}");
        }
    }
    
}
