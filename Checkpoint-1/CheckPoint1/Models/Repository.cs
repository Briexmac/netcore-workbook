using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPoint1.Models
{
    public class Repository : IRepository
    {
        public List<Customer> Customers { get; } = new List<Customer>();
        public List<ServiceProvider> ServiceProviders { get; } = new List<ServiceProvider>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public class InvalidAppointmentException : Exception
        {
            public InvalidAppointmentException(string message) : base(message)
            {
            }
        }

        public class InvalidCustomerException : Exception
        {
            public InvalidCustomerException(string message) : base(message)
            {
            }
        }

        public class InvalidServiceProviderException : Exception
        {
            public InvalidServiceProviderException(string message) : base(message)
            {
            }
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void AddServiceProvider(ServiceProvider serviceProvider)
        {
            ServiceProviders.Add(serviceProvider);
        }

        public void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
            Appointments = Appointments.OrderBy(a => a.Day).ThenBy(a => a.Time).ToList();
        }

        public void BookAppointment(Appointment appointment)
        {
            List<Appointment> appointments = this.Appointments;

            var isInvalidAppointment = appointments.Any(a => ((a.CustomerFullName == appointment.CustomerFullName
                || a.ServiceProviderFullName == appointment.ServiceProviderFullName)
                && a.Time == appointment.Time && a.Day == appointment.Day));
            if (isInvalidAppointment)
                throw new InvalidAppointmentException("Invalid Appointment");

            var isValidCustomer = Customers.Any(c => c.FullName == appointment.CustomerFullName);
            if (!isValidCustomer)
            {
                throw new InvalidCustomerException("Invalid Customer");
            }

            var isValidServiceProvider = ServiceProviders.Any(c => c.FullName == appointment.ServiceProviderFullName);
            if (!isValidServiceProvider)
            {
                throw new InvalidServiceProviderException("Invalid Service Provider");
            }
            this.AddAppointment(appointment);
        }
    }
}