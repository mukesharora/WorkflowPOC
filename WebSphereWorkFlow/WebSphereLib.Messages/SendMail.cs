using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace WebSphereLib.Messages
{
    public class SendMail
    {
        public string SendMailMessage(string toAddress, string fromAddress, string subject, string body, string host, int port, string addressDelimeter)
        {
            string returnStr = string.Empty;

            MailMessage message = new MailMessage();

            try
            {
                if (!string.IsNullOrWhiteSpace(toAddress))
                {
                    string[] strArr = toAddress.Split(new char[] { char.Parse(addressDelimeter) }, StringSplitOptions.RemoveEmptyEntries);

                    if (strArr != null && strArr.Length > 0)
                    {
                        foreach (var item in strArr)
                        {
                            message.To.Add(item);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(fromAddress))
                {
                    message.From = new MailAddress(fromAddress);
                }

                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Port = port;
                client.UseDefaultCredentials = true;               

                client.Send(message);

                returnStr = "Message Sent successfully";
            }
            catch (SmtpException ex)
            {
                File.AppendAllText(@"C:\Files\Log.txt", string.Format("Mail failure details : " + Environment.NewLine + "Message : {0}" + Environment.NewLine + "Inner Exception : {1}" + Environment.NewLine + "Stack trace : {2}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : string.Empty, (!string.IsNullOrWhiteSpace(ex.StackTrace)) ? ex.StackTrace : string.Empty));
                returnStr = string.Format("Some error occured while sending the message : {0}", (!string.IsNullOrWhiteSpace(ex.Message)) ? ex.Message : string.Empty);
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Files\Log.txt", string.Format("Mail failure details : " + Environment.NewLine + "Message : {0}" + Environment.NewLine + "Inner Exception : {1}" + Environment.NewLine + "Stack trace : {2}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : string.Empty, (!string.IsNullOrWhiteSpace(ex.StackTrace)) ? ex.StackTrace : string.Empty));
                returnStr = string.Format("Some error occured while sending the message : {0}", (!string.IsNullOrWhiteSpace(ex.Message)) ? ex.Message : string.Empty);
            }

            return returnStr;
        }
    }
}
