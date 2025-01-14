﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.server
{
    internal class Program
    {
        [ServiceContract]
        public interface ImessageService
        {
            [OperationContract]
            string[] GetMessages();

        }
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        public class MessageService : ImessageService
        {
            public string[] GetMessages()
            {
                return new string[] { "server1", "server2", "server3" };
            }
        }
        static void Main(string[] args)
        {
            var uris = new Uri[1];
            string address = "net.tcp://localhost:6565/MessageService";
            uris[0] = new Uri(address);
            ImessageService message = new MessageService();
            ServiceHost host = new ServiceHost(message,uris);
            var binding = new NetTcpBinding(SecurityMode.None);
            host.AddServiceEndpoint(typeof(ImessageService), binding, "");
            host.Opened += Host_Opened;
            host.Open();
            Console.ReadLine();

        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("message service started");
            //throw new NotImplementedException();
        }
    }
}
