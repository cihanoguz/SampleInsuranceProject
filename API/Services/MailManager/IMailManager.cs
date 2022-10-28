using System;
using System.Collections.Generic;

namespace API.Services.MailManager
{
    public interface IMailManager
    {
        string ReadFileContent(string FileName);
        string Send(string Subject, string To, string FileName, Dictionary<string, string> Recipients, dynamic ExtraData);
        void SendSmpt(string Subject, string To, string FileName, Dictionary<string, string> Recipients, dynamic ExtraData);
    }
}

