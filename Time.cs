using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace real
{
    class Time
    {
        private int minutes = 0;
        private int seconds = 0;
        private int hours = 0;
        public void Increase(Object source, ElapsedEventArgs e)
        {
           seconds++;
           if (seconds==60)
           {
             seconds = 0;
             minutes++;
           }
            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }
        }
        public string GetTotal()
        {
            return $"{Convert(hours)}:{Convert(minutes)}:{Convert(seconds)}";
        }
        private string Convert(int elem)
        {
            return (elem < 10) ? $"0{elem}" : $"{elem}";
        }
    }
}
