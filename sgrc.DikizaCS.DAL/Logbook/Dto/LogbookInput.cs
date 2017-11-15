using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sgrc.DikizaCS.DAL.Logbook.Dto
{
   public class LogbookInput
    {
        public long Id { get; set; }
        public long ProgramId { get; set; }
        public long UserId { get; set; }
        public DateTime DateOfActivity { get; set; }
        public string DateOfActivityS => DateOfActivity.ToString("dd MMM yyyy");
        public DateTime? WeekStartDate { get; set; }
        public string WeekStartS => WeekStartDate?.ToString("dd MMM yyyy") ?? "";
        public DateTime? WeekEndDate { get; set; }
        public string WeekEndDateS => WeekEndDate?.ToString("dd MMM yyyy") ?? "";
        public decimal TimeHours { get; set; }
        public decimal? TimeMinutes { get; set; }
        public string Time => TimeHours + "Hr" + " " + TimeMinutes + "Min";
        public int? WeekNumber { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityType { get; set; }
        public string Remarks { get; set; }
        public bool IsSubmited { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string DeletionDateS => DeletionDate?.ToString("dd MMM yyyy") ?? "";
        public DateTime? LastdateModified { get; set; }
        public string LastdateModifiedS => LastdateModified?.ToString("dd MMM yyyy") ?? "";
        public long? ModifiedByUser { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
