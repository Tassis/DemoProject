using System;
using System.IO;
using System.Collections.Generic;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using DemoApp.Actors;
using DemoApp.Cells;


namespace DemoApp
{
    public class ServerApp : ApplicationBase
    {
        public static ILogger Logger { get; }
        public static ServerApp instance { get; private set; }

        public ActorManager actorManager;
        public CellManager cellManager;

        public RequestManager requestManager;
        public EventManager eventManager;

        static ServerApp()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ServerPeer(initRequest, this);
        }

        protected override void Setup()
        {
            // Setting Log folder path.
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationPath, "log");
            FileInfo file = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
             }



            // Setting base system.
            actorManager = new ActorManager();
            cellManager = new CellManager();
            requestManager = new RequestManager();
            eventManager = new EventManager();

            Logger.Info("Server Setup Successful.......");
        }

        

        // When closed will run this function.
        protected override void TearDown()
        {
        }
    }
}
