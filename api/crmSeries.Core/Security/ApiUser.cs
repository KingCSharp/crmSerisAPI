namespace crmSeries.Core.Security
{
    public class ApiUser
    {
        public string DealerName { get; set; }

        public string DatabaseConnectionString { get; set; }

        public int DealerId { get; set; }

        public string UserEmail { get; set; }
    }

    public class IdentityUser
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";
    }
}