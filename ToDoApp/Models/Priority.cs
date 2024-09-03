using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
	public class Priority
	{
		public string PriorityId { get; set; } = string.Empty;
		public string PriorityName { get; set; } = string.Empty;
		[NotMapped]
		public string? PriorityNameWithColor { 
			get
			{
				return PriorityId switch
				{
					"low" => "🟩 Low",
					"normal" => "🟨 Normal",
					"high" => "🟥 High",
					_ => null
				};
			}
		} 
	}
}
