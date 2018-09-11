using System;

namespace Spatastic.Models
{
    public class Appointment
    {
        public enum Services { Swedish, Deep, Aroma, Hot, Oxygen, Microbrasion, Collagen, LED, BodyWraps, Seaweed, Mineral, Cellulite, BodyScrub, Leg, Chest, Back, Bikini, Brazilian }
            
        public enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

        public enum Times { Nine, Ten, Eleven, Noon, One, Two, Three, Four, Five };

        public Appointment()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string ServiceProviderFullName { get; set; }

        public string CustomerFullName { get; set; }

        public Services Service { get; set; }
        public Days Day { get; set; }

        public Times Time { get; set; }
    }
}