using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace real
{
    public class StatisticsClass
    {
        public int wins;
        public int loses;
        public DateTime timeSTart;
        public string totalTime;
        public StatisticsClass(int wins,int loses,DateTime timeSTart,string totalTime)
        {
            this.wins = wins;
            this.loses = loses;
            this.timeSTart = timeSTart;
            this.totalTime = totalTime;
        }
    }
}
