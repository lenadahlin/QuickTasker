using SQLite;
namespace Quick_Tasker.Models
{
    [Table("tasks")]
    public class Tasks : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly AssignedDate { get; set; }
        public TimeOnly EstimatedTime { get; set; }
        public DateOnly CompletedDate { get; set; }
    }
}