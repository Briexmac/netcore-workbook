using System;

namespace Spatastic.Models
{
    public class Customer
    {
        public Customer()
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