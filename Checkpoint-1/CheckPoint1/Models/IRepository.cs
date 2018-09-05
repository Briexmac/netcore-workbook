using System.Collections.Generic;

namespace CheckPoint1.Models
{
    public interface IRepository
    {
        List<Customer> Customers { get; }
        List<ServiceProvider> ServiceProviders { get; }
        List<Appointment> Appointments { get; set; }

        void AddCustomer(Customer customer);

        void AddServiceProvider(ServiceProvider serviceProvider);

        void AddAppointment(Appointment appointment);

        void BookAppointment(Appointment appointment);
    }
}