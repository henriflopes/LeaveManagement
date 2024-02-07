using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class LeaveRequestCreateVM : IValidatableObject
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
		[Required]
		[Display(Name = "End Date")]
		public DateTime? EndDate { get; set; }
		[Display(Name = "Number Of Days")]
		public int NumberOfDays { get; set; }
        public int AvailableDays { get; set; }
        [Required]
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        public SelectList? LeaveTypes  { get; set; }
        [Display(Name = "Request Comments")]
        public string? RequestComments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if(StartDate > EndDate)
            {
                yield return new ValidationResult("The Start Date Must Be Before End Date", new[] { nameof(StartDate), nameof(EndDate) });
            }

            if(RequestComments?.Length > 250)
            {
                yield return new ValidationResult("Comments Are Too Long", new[] { nameof(RequestComments) });
            }

            if (AvailableDays > NumberOfDays)
            {
                yield return new ValidationResult("You Haven't Available Days For This Request.", new[] { nameof(StartDate), nameof(EndDate) });
            }

        }
    }
}
