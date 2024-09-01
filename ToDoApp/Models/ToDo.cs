using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; } 
        [Required(ErrorMessage = "Please provide a description.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please provide a due date.")]
        public DateTime DueTime { get; set; }
        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; } = string.Empty;
        [ValidateNever]
        public Category Category { get; set; } = null!;
        public string StatusId { get; set; } = "open";
        [ValidateNever]
        public Status Status { get; set; } = null!;
        [Required(ErrorMessage = "Please select a priority.")]
        public string PriorityId { get; set; } = string.Empty;
        [ValidateNever]
        public Priority Priority { get; set; } = null!;

        public bool Overdue => StatusId == "open" && DueTime < DateTime.Today;
    }
}
