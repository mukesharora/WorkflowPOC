using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using WebSphereLib.Messages;
using Microsoft.VisualBasic.Activities;

namespace WebSphereWorkFlow
{

    class Program
    {
        /// <summary>
        /// Serilizes the object to string
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="obj">Obj</param>
        /// <returns></returns>
        public string SerializeObject<T>(T obj)
        {
            XmlSerializer serializer = null;
            serializer = new XmlSerializer(typeof(T));
            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, obj);
                writer.Flush();
                return sb.ToString();
            }
        }

        static void Main(string[] args)
        {
            //Announce announceObj = new Announce { State = 2 };

            //Program obj = new Program();

            //PartDeliveredMessage partDeliveredMessage = new PartDeliveredMessage();
            //partDeliveredMessage.MessageType = MessageType.PartDelivered;
            //partDeliveredMessage.RouteColor = RouteColor.Red;
            //partDeliveredMessage.PartNumber = "100";
            //partDeliveredMessage.Timestamp = DateTime.Now;
            //partDeliveredMessage.UID = 1;

            //string xmlPartDeliveredMessage = obj.SerializeObject<PartDeliveredMessage>(partDeliveredMessage);
            //string xmlPartDeliveredMessage = obj.SerializeObject<PartDeliveredMessage>(partDeliveredMessage).Replace("\"", "\"\"");

            //PartReorderMessage partReorderMessage = new PartReorderMessage();
            //partReorderMessage.MessageType = MessageType.PartReorder;
            //partReorderMessage.RouteColor = RouteColor.Blue;
            //partReorderMessage.PartNumber = "101";
            //partReorderMessage.Timestamp = DateTime.Now;
            //partReorderMessage.UID = 2;

            //string xmlPartReorderMessage = obj.SerializeObject<PartReorderMessage>(partReorderMessage).Replace("\"", "\"\"");

            //string strQueueManagerName = string.Empty;
            //string strQueueName = string.Empty;
            //string strChannelInfo = string.Empty;
            //string getQueueName = string.Empty;
            //string putQueueName = string.Empty;

            //strQueueManagerName = "queuemanager";
            //strQueueName = "omniIdQueue";
            //strChannelInfo = "SYSTEM.ADMIN.SVRCONN/TCP/192.168.10.221(1414)";
            //getQueueName = "omniIdQueue";
            //putQueueName = "omniIdQueue";

            //Dictionary<string, object> announceInput = new Dictionary<string, object>()
            //{
            //    {"AnnounceInfo",announceObj},
            //    {"PartReOrderMessage",xmlPartReorderMessage},
            //    {"PartDeliveredMessage",xmlPartDeliveredMessage}
            //};

            //WorkflowInvoker.Invoke(new WebSphereActivity(), announceInput);
            
            WorkflowInvoker.Invoke(new WebSphereActivityLib.WebSphereActivity());
        }
    }
}
