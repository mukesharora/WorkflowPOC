using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebSphereActivityLib
{
    public class FileOperations
    {
        public FileContent ReadFile(string path)
        {
            FileContent content = null;
            string readStr = string.Empty;

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    readStr = reader.ReadToEnd();
                }

                if (!string.IsNullOrWhiteSpace(readStr))
                {
                    content = new FileContent();

                    string[] strArr = readStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if (strArr != null && strArr.Length > 0)
                    {
                        for (int i = 0; i < strArr.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(strArr[i]))
                            {
                                string[] strContentArr = strArr[i].Split(new string[] { ":::" }, StringSplitOptions.RemoveEmptyEntries);

                                if (strContentArr != null && strContentArr.Length > 0)
                                {
                                    var itemName = strContentArr[0].Replace("\r", "").Replace("\n", "");

                                    if (itemName.Equals("QueueManagerName"))
                                    {
                                        content.QueueManagerName = strContentArr[1];
                                    }
                                    else if (itemName.Equals("QueueName"))
                                    {
                                        content.QueueName = strContentArr[1];
                                    }
                                    else if (itemName.Equals("ChannelName"))
                                    {
                                        content.ChannelName = strContentArr[1];
                                    }
                                    else if (itemName.Equals("HostName"))
                                    {
                                        content.HostName = strContentArr[1];
                                    }
                                    else if (itemName.Equals("Port"))
                                    {
                                        content.Port = strContentArr[1];
                                    }
                                    else if (itemName.Equals("UserID"))
                                    {
                                        content.UserID = strContentArr[1];
                                    }
                                    else if (itemName.Equals("Password"))
                                    {
                                        content.Password = strContentArr[1];
                                    }
                                    else if (itemName.Equals("PartReOrderMessage"))
                                    {
                                        content.PartReOrderMessage = strContentArr[1];
                                    }
                                    else if (itemName.Equals("PartDeliveredMessage"))
                                    {
                                        content.PartDeliveredMessage = strContentArr[1];
                                    }
                                    else if (itemName.Equals("AnnounceState"))
                                    {
                                        content.AnnounceState = int.Parse(strContentArr[1]);
                                    }
                                    else if (itemName.Equals("ToAddress"))
                                    {
                                        content.ToAddress = strContentArr[1];
                                    }
                                    else if (itemName.Equals("FromAddress"))
                                    {
                                        content.FromAddress = strContentArr[1];
                                    }
                                    else if (itemName.Equals("Subject"))
                                    {
                                        content.Subject = strContentArr[1];
                                    }
                                    else if (itemName.Equals("Body"))
                                    {
                                        content.Body = strContentArr[1];
                                    }
                                    else if (itemName.Equals("Host"))
                                    {
                                        content.Host = strContentArr[1];
                                    }
                                    else if (itemName.Equals("MailPort"))
                                    {
                                        content.MailPort = int.Parse(strContentArr[1]);
                                    }
                                    else if (itemName.Equals("AddressDelimeter"))
                                    {
                                        content.AddressDelimeter = strContentArr[1];
                                    }
                                    else if (itemName.Equals("MessageQueueName"))
                                    {
                                        content.MessageQueueName = strContentArr[1];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return content;
        }
    }

    public class FileContent
    {
        public string QueueManagerName
        {
            get;
            set;
        }

        public string QueueName
        {
            get;
            set;
        }

        public string ChannelName
        {
            get;
            set;
        }

        public string HostName
        {
            get;
            set;
        }

        public string Port
        {
            get;
            set;
        }

        public string UserID
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string PartReOrderMessage
        {
            get;
            set;
        }

        public string PartDeliveredMessage
        {
            get;
            set;
        }

        public int AnnounceState
        {
            get;
            set;
        }

        public string ToAddress
        {
            get;
            set;
        }

        public string FromAddress
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        public string Body
        {
            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        public int MailPort
        {
            get;
            set;
        }

        public string AddressDelimeter
        {
            get;
            set;
        }

        public string MessageQueueName
        {
            get;
            set;
        }
    }
}
