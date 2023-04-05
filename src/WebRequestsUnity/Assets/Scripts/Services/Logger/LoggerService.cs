using UnityEngine;

namespace Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public void PrintInfo(string objectName, string message)
        {
            Debug.Log($"{objectName}: {message}");
        }
    
        public void PrintWarning(string objectName, string message)
        {
            Debug.LogWarning($"{objectName}: {message}");
        }
    }
}