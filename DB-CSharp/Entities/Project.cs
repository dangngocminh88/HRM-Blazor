using System;

namespace DB_CSharp.Entities
{
    public class Project
    {
        public short Id { get; set; }
        public string ProjectName { get; set; }
        public bool Active { get; set; }
        public DateTime ChangeDate { get; set; }
        public int ChangeCount { get; set; }
        public string ChangeBy { get; set; }
    }
}
