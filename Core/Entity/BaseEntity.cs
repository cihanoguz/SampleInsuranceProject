using System;
using static Core.Enums.Enums;

namespace Core.Entity
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }
}

