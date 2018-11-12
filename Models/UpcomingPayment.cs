using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Models.Validation;

namespace Models
{
    public class UpcomingPayment
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [CheckDateRange]
        public DateTime DueDate { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsOverDue { get; set; }

        public bool IsPaid { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

    }
}
