using System;
namespace SecondDayProject.Models
{
    public class Greeting
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string HomeTown { get; set; }
        public object CurrentTime { get; internal set; }
    }
}
