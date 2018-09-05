using System;

namespace CheckPoint1.Models
{
    public class ServiceProvider
    {
        public ServiceProvider()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}