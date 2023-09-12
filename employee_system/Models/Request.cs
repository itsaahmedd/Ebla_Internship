using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace employee_system.Models
{
	public class Request
	{

        public int Id { get; set; }

        public int Requested_By { get; set; } // foriegn key to User primary ID

        [ForeignKey("Requested_By")] //  foreign key relationship
        public Employee Employee_1 { get; set; }

        public string? Subject { get; set; }

        public DateOnly Date { get; set; }

        public string? Details { get; set; }

        public string? Status { get; set; }

        public string? Change { get; set; }


    }
}

