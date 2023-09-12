using System;
using System.ComponentModel.DataAnnotations; // Namespace for [Required] attribute

using System.ComponentModel.DataAnnotations.Schema; // Nampespace using for table relations

namespace employee_system.Models

{
	public class Task
	{
        public int Id { get; set; }

        public int Assigned_By { get; set; } // foriegn key to Leader primary ID

        [ForeignKey("Assigned_By")] //  foreign key relationship
        public Employee Employee_1 { get; set; } 

        public int Assigned_To { get; set; } // foriegn key to Employee primary ID

        [ForeignKey("Assigned_To")] //  foreign key relationship
        public Employee Employee_2 { get; set; } 

        public string? Subject { get; set; }

        public string? Details { get; set; }

        public DateOnly Due_Date { get; set; }

        public string? Status { get; set; }
    }
}

