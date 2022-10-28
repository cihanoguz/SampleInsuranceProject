using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Model.Base;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace API.Services.MailManager
{
    public class MailManager : IMailManager
    {
        private readonly IHostEnvironment env;
     // private readonly IOptions<ApplicationSettings> settings;
        private readonly IConfiguration configuration;

        public MailManager(
            IHostEnvironment env,
         //  IOptions<ApplicationSettings> settings,
            IConfiguration  configuration
            )
        {
            this.env = env;
          //this.settings = settings;
            this.configuration = configuration;
        }
        public string ReadFileContent(string FileName)
        {

            using (StreamReader reader = File.OpenText("./wwwroot/Mailing/" + FileName))
            {
                string fileContent = reader.ReadToEnd();
                if (fileContent != null && fileContent != "")
                {
                    return fileContent;
                }
            }

            return "";
        }

        /// <summary>
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="FromName"></param>
        /// <param name="FileName"></param>
        /// <param name="Recipients"></param>
        /// <returns></returns>
        public string Send(string Subject, string To, string FileName, Dictionary<string, string> Recipients, dynamic ExtraData = null)
        {
            try
            {
                SendSmpt(Subject,To,FileName,Recipients);
                // log, when,to,content,which user if login

            }
            catch (Exception ex)
            {
                // log ex.Message,when,which user if login
            }
            return "";
        }

        public void SendSmpt(string Subject, string To, string FileName, Dictionary<string, string> Recipients, dynamic ExtraData = null)
        {
            MailAddress fromAdress = new MailAddress(configuration["MailSettingsForSmtp:FromMail"].ToString());
            string mailHost = configuration["MailSettingsForSmtp:SmtpHost"].ToString();
            string mailPassword = configuration["MailSettingsForSmtp:MailAdressPasword"].ToString();
            var toAdress = new MailAddress(To);

            string MailContent = ReadFileContent(FileName);
            MailContent = MailContent.Replace("%recipient.FullName%", Recipients["recipient.FullName"]);
            foreach (var item in Recipients)
            {
                MailContent = MailContent.Replace("%" + item.Key + "%", item.Value);
            }

            using (var smtp = new System.Net.Mail.SmtpClient
            {
                Host = mailHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAdress.Address, mailPassword)
            })

            using (var message = new MailMessage(fromAdress, toAdress) { Subject = Subject, Body = MailContent, IsBodyHtml = true })
            {
                smtp.Send(message);
            }

        }


    }
}

