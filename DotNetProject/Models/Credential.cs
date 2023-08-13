using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetProject.Models
{
    public class UserCred
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }

    }
    public class AdminCred
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
