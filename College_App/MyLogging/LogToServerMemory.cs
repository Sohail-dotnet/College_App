namespace WebApplication1.MyLogging
{
    public class LogToServerMemory : IMyLogger
    {
        public void Log(string message)
        {
            // Logic to log the message to server memory
            // This is a placeholder implementation
            Console.WriteLine($"LogToServerMemory: {message}");
        }
    }
}
