using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using server = Exiled.Events.Handlers.Server;


namespace StaffLogger
{
    public class StaffLogger : Plugin<Config>
    {
        public override string Name => "StaffLogger";
        public override string Author => "giac02cant";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(8, 13, 2);
        public override string Prefix => "StaffLogger";

        public static StaffLogger Instance;
        private static EventsHandler eventshandler;

        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            UnRegisterEvents();
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            eventshandler = new EventsHandler();
            Exiled.Events.Handlers.Player.Verified += eventshandler.OnVerified;
            Exiled.Events.Handlers.Player.Left += eventshandler.PlayerLeft;
        }

        public void UnRegisterEvents()
        {
            eventshandler = null;
            Exiled.Events.Handlers.Player.Verified -= eventshandler.OnVerified;
            Exiled.Events.Handlers.Player.Left -= eventshandler.PlayerLeft;
        }

    }

}
