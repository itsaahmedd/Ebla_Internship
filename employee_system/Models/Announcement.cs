using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace employee_system.Models
{
	public class Announcement
	{
        public int Id { get; set; }

        public int Published_By { get; set; } // foriegn key to HR primary ID

        [ForeignKey("Published_By")] //  foreign key relationship
        public Employee Employee_1 { get; set; }

        public string? Subject { get; set; }

        public DateOnly Date { get; set; }

        //  Date = new DateOnly(2023, 09, 15), // Assign a DateOnly value


        public string? Details { get; set; }





    }
}

