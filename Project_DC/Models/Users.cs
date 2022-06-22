
namespace Project_DC.Models
{
    public class Users 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Roles Roles { get; set; }


    }
}
