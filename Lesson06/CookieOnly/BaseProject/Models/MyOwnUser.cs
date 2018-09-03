using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class MyOwnUser
    {
        [Key]
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
