namespace DB_CSharp.Models.Projects
{
    public class ProjectDetailSaveRequest
    {
        public long? Id { get; set; }
        public string ProjectName { get; set; }
        public bool Active { get; set; }
        public int ChangeCount { get; set; }
    }
}
