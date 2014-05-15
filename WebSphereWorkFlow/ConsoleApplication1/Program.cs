using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSphereLib.Messages;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient())
            //{
            //    client.GetData(1);
            //}

            //using (WorkflowService.ServiceClient client = new WorkflowService.ServiceClient())
            //{
            //    client.GetData(1);
            //}
            
            //ClsMessageQueue clsObj = new ClsMessageQueue();

            //for (int i = 0; i < 10; i++)
            //{
            //    ClsAnnounce announceObj = new ClsAnnounce();

            //    if (i % 2 == 0)
            //    {
            //        announceObj.State = 2;
            //    }
            //    else
            //    {
            //        announceObj.State = 4;
            //    }

            //    clsObj.SendMessageToRemoteQueue<ClsAnnounce>("OmniIDQueue", announceObj);
            //    //clsObj.SendMessage<ClsAnnounce>("OmniIDQueue", announceObj);
            //}

            //for (int i = 0; i < 1; i++)
            //{
            //    using (MSMQServiceReference.ContractIMsmqServiceClient client = new MSMQServiceReference.ContractIMsmqServiceClient())
            //    {
            //        var req = new MSMQServiceReference.AnnounceRequest { state = 2 };
            //        client.StartProcess(req);
            //    }
            //}
          
        }
    }
}
