using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;

namespace WebSphereLib.Messages
{
    public class ClsMessageQueue
    {
        private MessageQueue GetMessageQueue(string queueName)
        {
            MessageQueue msgQueue = null;

            string fullyQualifiedQueueName = string.Format(@".\private$\{0}", queueName);

            //if (!MessageQueue.Exists(fullyQualifiedQueueName))
            //{
            //    msgQueue = MessageQueue.Create(fullyQualifiedQueueName);
            //}
            //else
            //{
            //    msgQueue = new MessageQueue(fullyQualifiedQueueName);
            //}

            File.AppendAllText(@"C:\Files\Log.txt", string.Format("Connection for queue : {0}" + Environment.NewLine, fullyQualifiedQueueName));

            msgQueue = new MessageQueue(fullyQualifiedQueueName);
            //msgQueue.SetPermissions(Environment.UserName, MessageQueueAccessRights.FullControl, AccessControlEntryType.Set);

            if (msgQueue != null)
            {
                File.AppendAllText(@"C:\Files\Log.txt", "Connection made with queue" + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(@"C:\Files\Log.txt", "Connection not made with queue" + Environment.NewLine);
            }

            return msgQueue;
        }

        private MessageQueue GetRemoteMessageQueue(string queueName, string hostname)
        {
            MessageQueue msgQueue = null;
            string fullyQualifiedQueueName = string.Format(@"FormatName:Direct=TCP:{0}\private$\{1}", hostname, queueName);

            msgQueue = new MessageQueue(fullyQualifiedQueueName);
            //msgQueue.SetPermissions(Environment.UserName, MessageQueueAccessRights.FullControl, AccessControlEntryType.Set);

            if (msgQueue != null)
            {
                File.AppendAllText(@"C:\Files\Log.txt", "Connection made with queue" + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(@"C:\Files\Log.txt", "Connection not made with queue" + Environment.NewLine);
            }

            return msgQueue;
        }

        public void SendMessage<T>(string queueName, T message)
        {
            try
            {
                var messageQueue = this.GetMessageQueue(queueName);
                Message msg = new Message();
                msg.Body = message;
                messageQueue.Send(msg);
            }
            catch (MessageQueueException)
            {

            }
            catch (Exception)
            {

            }
        }

        public void SendMessageToRemoteQueue<T>(string queueName, string hostName, T message)
        {
            try
            {
                var messageQueue = this.GetRemoteMessageQueue(queueName, hostName);
                Message msg = new Message();
                msg.Body = message;
                messageQueue.Send(msg);
            }
            catch (MessageQueueException)
            {

            }
            catch (Exception)
            {

            }
        }

        //public T RetrieveMessages<T>(string queueName, T obj)
        //{
        //    T message = default(T);

        //    try
        //    {
        //        var messageQueue = this.GetMessageQueue(queueName);
        //        messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
        //        message = (T)messageQueue.Receive().Body;
        //    }
        //    catch (MessageQueueException)
        //    {

        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return message;
        //}

        public ClsAnnounce RetrieveMessages(string queueName, ClsAnnounce obj)
        {
            ClsAnnounce message = null;

            try
            {
                var messageQueue = this.GetMessageQueue(queueName);
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ClsAnnounce) });
                if (messageQueue.GetAllMessages() != null && messageQueue.GetAllMessages().Length > 0)
                {
                    var retrievedmessage = messageQueue.Receive();

                    if (retrievedmessage != null)
                    {
                        var body = retrievedmessage.Body;

                        if (body != null)
                        {
                            message = body as ClsAnnounce;

                            if (message != null)
                            {
                                //File.AppendAllText(@"C:\Files\Log.txt", string.Format("Message retrieved with state: {0}" + Environment.NewLine, message.State));
                            }
                        }
                    }
                }
            }
            catch (MessageQueueException ex)
            {
                File.AppendAllText(@"C:\Files\Log.txt", string.Format("Error details : " + Environment.NewLine + "Message : {0}" + Environment.NewLine + "Inner Exception : {1}" + Environment.NewLine + "Stack trace : {2}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : string.Empty, (!string.IsNullOrWhiteSpace(ex.StackTrace)) ? ex.StackTrace : string.Empty));
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Files\Log.txt", string.Format("Error details : " + Environment.NewLine + "Message : {0}" + Environment.NewLine + "Inner Exception : {1}" + Environment.NewLine + "Stack trace : {2}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : string.Empty, (!string.IsNullOrWhiteSpace(ex.StackTrace)) ? ex.StackTrace : string.Empty));
            }

            return message;
        }

        public ClsAnnounce RetrieveMessagesFromRemoteQueue(string queueName, string hostName, ClsAnnounce obj)
        {
            ClsAnnounce message = null;

            try
            {
                var messageQueue = this.GetRemoteMessageQueue(queueName, hostName);
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ClsAnnounce) });

                if (messageQueue.GetAllMessages() != null && messageQueue.GetAllMessages().Length > 0)
                {
                    var retrievedmessage = messageQueue.Receive();

                    if (retrievedmessage != null)
                    {
                        var body = retrievedmessage.Body;

                        if (body != null)
                        {
                            message = body as ClsAnnounce;

                            if (message != null)
                            {
                                //File.AppendAllText(@"C:\Files\Log.txt", string.Format("Message retrieved with state: {0}" + Environment.NewLine, message.State));
                            }
                        }
                    }
                }
            }
            catch (MessageQueueException ex)
            {
                File.AppendAllText(@"C:\Files\Log.txt", string.Format("Error details : " + Environment.NewLine + "Message : {0}" + Environment.NewLine + "Inner Exception : {1}" + Environment.NewLine + "Stack trace : {2}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : string.Empty, (!string.IsNullOrWhiteSpace(ex.StackTrace)) ? ex.StackTrace : string.Empty));
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Files\Log.txt", string.Format("Error details : " + Environment.NewLine + "Message : {0}" + Environment.NewLine + "Inner Exception : {1}" + Environment.NewLine + "Stack trace : {2}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : string.Empty, (!string.IsNullOrWhiteSpace(ex.StackTrace)) ? ex.StackTrace : string.Empty));
            }

            return message;
        }
    }
}
