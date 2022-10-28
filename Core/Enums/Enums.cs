using System;
namespace Core.Enums
{
    public class Enums
    {
        public enum RecordStatus
        {
            Active = 0,
            Deleted = 1,
            InActive = 2,
        }
        public enum Role
        {
            Customer = 0,
            Agent = 1,
            Admin = 2
        }

        public enum Gender
        {
            Male = 0,
            Female = 1,
            Other = 2,
        }
    }
}

