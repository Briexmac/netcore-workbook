using System;

namespace BaseProject.Models
{
    public class WorkTicket
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsExpat { get; set; }
        public decimal Cost { get; set; }
    }
}
