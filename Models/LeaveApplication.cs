namespace EmployeeManagementSystem.Models
{
    public class LeaveApplication
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }

}
