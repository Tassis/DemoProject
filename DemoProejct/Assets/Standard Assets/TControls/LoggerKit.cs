using UnityEngine;


namespace TControls.Core
{
    public class UnityLoggerListener : ILoggerListener
    {
        public void Log(string msg , TLogger.LOGGER_CHANNEL channel)
        {
            switch (channel)
            {
                case TLogger.LOGGER_CHANNEL.ERROR:
                    Debug.LogError(msg); break;
                case TLogger.LOGGER_CHANNEL.WARRING:
                    Debug.LogWarning(msg); break;
                default:
                    Debug.Log(msg); break; 
            }
        }
    }

    public class LoggerKit
    {
        static public void Init()
        {
            TLogger.instance.Init();
            TLogger.instance.AddListner(new UnityLoggerListener());
            TLogger.INFO("TLogger Initialzation");
        }

        static public void UnInit()
        {
            TLogger.instance.UnInit();
            TLogger.INFO("TLogger UnInitialzation");
        }
    }

}
