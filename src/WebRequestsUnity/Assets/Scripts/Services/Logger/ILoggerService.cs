namespace Services.Logger
{
    public interface ILoggerService
    {
        public void PrintInfo(string objectName, string message);
        public void PrintWarning(string objectName, string message);
    }
}