using SQLite;
namespace Quick_Tasker.Models
{
    [Table("tasks")]
    public class Tasks : ObservableObject
    {
        //task table stuff
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? AssignedDate { get; set; }
        public TimeSpan? EstimatedTime { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool CompletedStatus { get; set; }

        //formatting dates and times for pages
        [Ignore] //formatting Due Date
        public string FormattedDueDate
        {
            get
            {
                if (DueDate.HasValue)
                {
                    return DueDate.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        [Ignore] //formatted Assigned Date
        public string FormattedAssignedDate
        {
            get
            {
                if (AssignedDate.HasValue)
                {
                    return AssignedDate.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        [Ignore] //formatted Estimated Time
        public string FormattedEstimatedTime
        {
            get
            {
                if (EstimatedTime.HasValue)
                {
                    return EstimatedTime.Value.ToString(@"hh\:mm");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        [Ignore] //formatted Completed Date
        public string FormattedCompletedDate
        {
            get
            {
                if (CompletedDate.HasValue)
                {
                    return CompletedDate.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    return string.Empty;
                }
            }
        }


    }
}