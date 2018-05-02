using System.Collections.Generic;

namespace TControls.Core
{
    public interface ILoggerListener
    {
        void Log(string msg , TLogger.LOGGER_CHANNEL channel);
    }

    public class TLogger : TStaticHelper<TLogger>  {
        
        public enum LOGGER_CHANNEL
        {
            Default =0,
            DEBUG,
            WARRING,
            ERROR,
            INFO,
            NUM,
        }

        public enum LOGGER_PRINTMODE
        {
            SIMPLE,
            ADVANCED,
        }

        //===========================
        // Static Method.
        //===========================
        public static void DEBUG(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.DEBUG);
        }

        public static void WARRING(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.WARRING);
        }

        public static void ERROR(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.ERROR);
        }

        public static void INFO(string msg)
        {
            TLogger.instance.Log(msg, LOGGER_CHANNEL.INFO);
        }
        //============================
        public LOGGER_PRINTMODE LoggerPrintMode = LOGGER_PRINTMODE.SIMPLE;
        private bool[] _enableChannel;
        private List<ILoggerListener> _listener;

        protected override void OnInit()
        {
            _enableChannel = new bool[(int)LOGGER_CHANNEL.NUM];
            for (int i=0; i< _enableChannel.Length; i++)
            {
                _enableChannel[i] = true;
            }
            _listener = new List<ILoggerListener>();
        }

        protected override void OnUnInit()
        {
            _enableChannel = null;
            _listener = null;
        }

        //============================
        // Fixed Method.
        //============================
        public void SetChannel(LOGGER_CHANNEL channel, bool value)
        {
            if (channel == LOGGER_CHANNEL.NUM || !hasInited)
                return;

            _enableChannel[(int)channel] = value;   
        }

        public void AddListner(ILoggerListener listener)
        {
            if (!hasInited)
                return;

            _listener.Add(listener);
        }

        public void Log(string msg, LOGGER_CHANNEL channel)
        {
            if (!hasInited)
                return;

            if (_enableChannel[(int)channel] == false)
                return;

            string outMsg;
            if (LoggerPrintMode == LOGGER_PRINTMODE.ADVANCED)
            {
                outMsg = "[" + System.DateTime.Now.ToString() + "]" + channel.ToString() + ":" + msg;
            }else
            {
                outMsg = msg;
            }

            for(int i = 0; i < _listener.Count; i++)
            {
                _listener[i].Log(outMsg, channel);
            }
        }
    }
}

