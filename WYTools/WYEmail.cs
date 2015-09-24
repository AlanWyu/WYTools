using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WYTools
{
    public class WYEmail
    {
        public string from { get; set; }
        public string password { get; set; }
        public List<string> to { get; set; }
        public List<string> cc { get; set; }
        public List<string> atts { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 不带惨实例化时，在调用方法前必须先给邮件类属性赋值。
        /// </summary>
        public WYEmail()
        {

        }
        /// <summary>
        /// 通过实例化传参的方式，能够保证参数传递正确性。
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_password"></param>
        /// <param name="_to"></param>
        /// <param name="_cc"></param>
        /// <param name="_atts"></param>
        /// <param name="_subject"></param>
        /// <param name="_body"></param>
        /// <param name="_ssl"></param>
        /// <param name="_host"></param>
        /// <param name="_port"></param>
        public WYEmail(string _from,string _password,List<string> _to,List<string> _cc,List<string> _atts,string _subject,string _body,bool _ssl,string _host,int _port)
        {
            from = _from;
            password = _password;
            to = _to;
            cc = _cc;
            atts = _atts;
            subject = _subject;
            body = _body;
            EnableSsl = _ssl;
            host = _host;
            port = _port;
        }


        /// <summary>
        /// 创建一个MailMessage
        /// </summary>
        /// <param name="from">发送邮箱账号</param>
        /// <param name="password">发送邮箱密码</param>
        /// <param name="to">收件人集合</param>
        /// <param name="cc">抄送人集合</param>
        /// <param name="atts">附件路径</param>
        /// <param name="host">主机</param>
        /// <param name="port">端口号</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        private System.Net.Mail.MailMessage CreateMail()
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(from);
            foreach (string str in to)
            {
                mail.To.Add(new System.Net.Mail.MailAddress(str));
            }
            foreach (string str in cc)
            {
                mail.CC.Add(new System.Net.Mail.MailAddress(str));
            }
            if (atts != null)
            {
                foreach (string str in atts)
                {
                    mail.Attachments.Add(new System.Net.Mail.Attachment(str));
                }
            }
            mail.Subject = subject;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            return mail;
        }

        public string SendEmail()
        {
            string result = "";
            try
            {
                System.Net.Mail.MailMessage mail = CreateMail();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(host);
                smtp.Port = port;
                smtp.Timeout = 9999;
                smtp.Credentials = new System.Net.NetworkCredential(from, password);
                smtp.SendAsync(mail, null);
                smtp.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(smtp_SendCompleted);
                result = "ok";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           
        }
    }
}
