using System;
using Core.Entity;

namespace Entity.Logs
{
    public class Log : BaseEntity
    {
        public string Message { get; set; }

        public string MessageTemplate { get; set; }

        public string Level { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Exception { get; set; }

        public string Xml { get; set; }

        public string LogEvent { get; set; }

    }
}

