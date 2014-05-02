﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBM.WMQ;
using System.Collections;

namespace WebSphereLib.Messages
{
    /// MQ Manager Class
    /// Put/Get Messages To/From Local Queue 
    /// </summary>
    public class MQManager
    {
        MQQueueManager queueManager;
        MQQueue queue;
        MQMessage queueMessage;
        MQPutMessageOptions queuePutMessageOptions;
        MQGetMessageOptions queueGetMessageOptions;
        private Hashtable queueProperties;

        static string SendQueueName;
        static string ReceiveQueueName;
        static string QueueManagerName;
        string message;

        public MQManager()
        {
            queueProperties = new Hashtable();
        }

        /// <summary>
        /// Connect to MQ Server
        /// </summary>
        /// <param name="strQueueManagerName">Queue Manager Name</param>
        /// <param name="strChannelInfo">Channel Information</param>
        /// <returns></returns>
        public MQMessageStatus ConnectMQ(string strQueueManagerName, string strChannelName, string strHostName, string port, string userName, string password)
        {
            MQMessageStatus messageStatus = new MQMessageStatus();
            string strReturn = string.Empty;

            queueProperties[MQC.HOST_NAME_PROPERTY] = strHostName;
            queueProperties[MQC.PORT_PROPERTY] = port;
            queueProperties[MQC.CHANNEL_PROPERTY] = strChannelName;
            queueProperties[MQC.USER_ID_PROPERTY] = userName;
            queueProperties[MQC.PASSWORD_PROPERTY] = password;

            try
            {
                queueManager = new MQQueueManager(strQueueManagerName, queueProperties);
                messageStatus.Message = "Connected Successfully";
                messageStatus.Status = true;
            }
            catch (MQException exp)
            {
                strReturn = "Exception: " + exp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }

            return messageStatus;
        }


        private MQQueueManager GetConnectedQueueManager(string strQueueManagerName, string strChannelName, string strHostName, string port, string userName, string password)
        {
            QueueManagerName = strQueueManagerName;
            queueProperties[MQC.HOST_NAME_PROPERTY] = strHostName;
            queueProperties[MQC.PORT_PROPERTY] = port;
            queueProperties[MQC.CHANNEL_PROPERTY] = strChannelName;
            queueProperties[MQC.USER_ID_PROPERTY] = userName;
            queueProperties[MQC.PASSWORD_PROPERTY] = password;

            try
            {
                queueManager = new MQQueueManager(strQueueManagerName, queueProperties);
            }
            catch (MQException)
            {
            }
            catch (Exception)
            {
            }

            return queueManager;
        }

        /// <summary>
        /// Write Message to Local Queue
        /// </summary>
        /// <param name="strInputMsg">Text Message</param>
        /// <param name="strqueueName">Queue Name</param>
        /// <returns></returns>
        public MQMessageStatus WriteLocalQMsg(string strInputMsg, string strQueueName)
        {
            MQMessageStatus messageStatus = new MQMessageStatus();
            string strReturn = string.Empty;
            SendQueueName = strQueueName;
            try
            {
                queue = queueManager.AccessQueue(SendQueueName,
                  MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);
                message = strInputMsg;
                queueMessage = new MQMessage();
                queueMessage.WriteString(message);
                queueMessage.Format = MQC.MQFMT_STRING;
                queuePutMessageOptions = new MQPutMessageOptions();
                queue.Put(queueMessage, queuePutMessageOptions);
                strReturn = "Message sent to the queue successfully";

                messageStatus.Message = strReturn;
                messageStatus.Status = true;
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            return messageStatus;
        }

        /// <summary>
        /// Write Message to Local Queue
        /// </summary>
        /// <param name="strQueueManagerName">Queue Manager Name</param>
        /// <param name="strChannelInfo">Channel Information</param>
        /// <param name="strInputMsg">Text Message</param>
        /// <param name="strqueueName">Queue Name</param>
        /// <returns></returns>
        public MQMessageStatus WriteLocalQMsg(string strQueueManagerName, string strChannelName, string strHostName, string port, string userName, string password, string strInputMsg, string strQueueName)
        {
            MQMessageStatus messageStatus = new MQMessageStatus();
            string strReturn = string.Empty;
            SendQueueName = strQueueName;
            try
            {
                queue = this.GetConnectedQueueManager(strQueueManagerName, strChannelName, strHostName, port, userName, password).AccessQueue(SendQueueName,
                  MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);
                message = strInputMsg;
                queueMessage = new MQMessage();
                queueMessage.WriteString(message);
                queueMessage.Format = MQC.MQFMT_STRING;
                queuePutMessageOptions = new MQPutMessageOptions();
                queue.Put(queueMessage, queuePutMessageOptions);
                strReturn = "Message sent to the queue successfully";

                messageStatus.Message = strReturn;
                messageStatus.Status = true;
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            return messageStatus;
        }

        /// <summary>
        /// Read Message from Local Queue
        /// </summary>
        /// <param name="strqueueName">Queue Name</param>
        /// <returns>Text Message</returns>
        public MQMessageStatus ReadLocalQMsg(string strQueueName)
        {
            MQMessageStatus messageStatus = new MQMessageStatus();
            string strReturn = string.Empty;
            ReceiveQueueName = strQueueName;
            try
            {
                queue = queueManager.AccessQueue(ReceiveQueueName,
                  MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING);
                queueMessage = new MQMessage();
                queueMessage.Format = MQC.MQFMT_STRING;
                queueGetMessageOptions = new MQGetMessageOptions();
                queue.Get(queueMessage, queueGetMessageOptions);
                strReturn = queueMessage.ReadString(queueMessage.MessageLength);

                messageStatus.Message = strReturn;
                messageStatus.Status = true;
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            return messageStatus;
        }

        /// <summary>
        /// Get Message count from Local Queue
        /// </summary>
        /// <param name="strqueueName">Queue Name</param>
        /// <returns>Text Message</returns>
        public MQMessageStatus CheckLocalQMsg(string strQueueName)
        {
            MQMessageStatus messageStatus = new MQMessageStatus();
            string strReturn = string.Empty;
            ReceiveQueueName = strQueueName;
            try
            {
                queue = queueManager.AccessQueue(ReceiveQueueName,
                  MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING);

                strReturn = Convert.ToString(queue.CurrentDepth);
                messageStatus.Message = strReturn;
                messageStatus.Status = true;
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
                messageStatus.Message = strReturn;
                messageStatus.Status = false;
            }
            return messageStatus;
        }
    }
}
