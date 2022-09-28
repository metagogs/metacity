using System;
using Gogs;
using Google.Protobuf;
namespace Model
{
    public static class GInit
    {
        public const int SERVICE_TYPE = 2;

        public static void Init()
        {
            
            Dispatch.AddField("JoinWorld", typeof(JoinWorld), Packet.CreateAction(SERVICE_TYPE, 1, 1));  // 0x810001 8454145 
            
            Dispatch.AddField("JoinWorldSuccess", typeof(JoinWorldSuccess), Packet.CreateAction(SERVICE_TYPE, 1, 2));  // 0x810002 8454146 
            
            Dispatch.AddField("JoinWorldNotifiy", typeof(JoinWorldNotifiy), Packet.CreateAction(SERVICE_TYPE, 1, 3));  // 0x810003 8454147 
            
            Dispatch.AddField("UpdateUserInWorld", typeof(UpdateUserInWorld), Packet.CreateAction(SERVICE_TYPE, 1, 4));  // 0x810004 8454148 
            
            Dispatch.AddField("LeaveWorldNotifiy", typeof(LeaveWorldNotifiy), Packet.CreateAction(SERVICE_TYPE, 1, 5));  // 0x810005 8454149 
            
            Gogs.Messages.Instance();
        }
    }

    public static class GMessages
    {
        public static void Message(string name, byte[] data)
        {
            Gogs.Messages.Message(name, data);
        }

        public static byte[] GetPong()
        {
            Pong pong = new Pong();
            return pong.ToPacketData();
        }
    }

    public static class MessageExtension
    {
        public static byte[] ToPacketData(this IMessage obj)
        {
            if (Gogs.Messages.Instance().Encode(obj, out Gogs.Packet packet))
            {
                return packet.ToByteArray();
            }

            return new byte[] { };
        }
    }


    public static class GEvents
    {
        public static void OnPing(Action<string, Ping> action)
        {
            Gogs.EventsManager.AddListener<Ping>(action);
        }
        
        public static void OnJoinWorld(Action<string, JoinWorld> action)
        {
            Gogs.EventsManager.AddListener<JoinWorld>(action);
        }
        
        public static void OnJoinWorldSuccess(Action<string, JoinWorldSuccess> action)
        {
            Gogs.EventsManager.AddListener<JoinWorldSuccess>(action);
        }
        
        public static void OnJoinWorldNotifiy(Action<string, JoinWorldNotifiy> action)
        {
            Gogs.EventsManager.AddListener<JoinWorldNotifiy>(action);
        }
        
        public static void OnUpdateUserInWorld(Action<string, UpdateUserInWorld> action)
        {
            Gogs.EventsManager.AddListener<UpdateUserInWorld>(action);
        }
        
        public static void OnLeaveWorldNotifiy(Action<string, LeaveWorldNotifiy> action)
        {
            Gogs.EventsManager.AddListener<LeaveWorldNotifiy>(action);
        }
        
    }
}
