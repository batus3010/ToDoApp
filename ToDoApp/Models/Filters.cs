namespace ToDoApp.Models
{
    public class Filters
    {
        public string FilterString { get; }
        public string CategoryId { get; }
        public string Due { get; }
        public string StatusId { get; } 
        public string PriorityId { get; }
        
        public Filters(string filterstring) 
        {
            FilterString = filterstring ?? "all-all-all-all";
            string[] filters = FilterString.Split('-');
            CategoryId = filters[0];
            Due = filters[1];
            StatusId = filters[2];
            PriorityId = filters[3];
        }
        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasDue => Due.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";
        public bool HasPriority => PriorityId.ToLower() != "all";
        public static Dictionary<string, string> DueFilterValues =>
            new Dictionary<string, string>
            {
                { "past", "Overdue" },
                { "today", "Today" },
                { "future", "Future" }

            };
        public bool IsPast => Due.ToLower() == "past";
        public bool IsToday => Due.ToLower() == "today";
        public bool IsFuture => Due.ToLower() == "future";
    }
}
