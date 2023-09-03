using SQLite;
namespace Quick_Tasker.Models
{
    [Table("tasks")]
    public class Tasks : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? AssignedDate { get; set; }
        public TimeSpan? EstimatedTime { get; set; }
        public DateTime? CompletedDate { get; set; }

    }
}