using System;

namespace TravelApp.Model
{
    //public class LoginRequest
    //{
    //    public string UserId { get; set; }
    //    public string HashedPassword { get; set; }
    //    public string Company { get; set; }
    //}

    public class LoginResponse
    {
        public string AuthToken { get; set; }

        public string ProfileId { get; set; }
        public string CompanyId { get; set; }
    }

    public class Profile
    {
        public string ProfileId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }
        public string ManagerName { get; set; }
        public string ManagerProfileId { get; set; }
        public string ManagerDesignation { get; set; }

        public string ManagerEmail { get; set; }

        public string ManagerContactNo { get; set; }
    }
    public class CompanyPolicy
    {
        public string CompanyId { get; set; }
        public string AccountId { get; set; }
        public string FareAllowanceLimit { get; set; }
        public string AccomadationAllowanceLimit { get; set; }
        public string DailyAllowanceLimit { get; set; }
        public string CurrencyCode { get; set; }
    }
}
