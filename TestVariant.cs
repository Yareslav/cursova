using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace real
{
    class TestVariant
    {
        public string source;
        public string question;
        public int[] rightAnswers;
        public string type;
        public string[] variants;
        public TestVariant(string source,string question,int [] rightAnswers,string type,string[] variants)
        {
            this.source = $"C:/My/Programesoft/C#/cursova/real/real/images/{source}.png";
            this.question = question;
            this.rightAnswers = rightAnswers;
            this.type = type;
            this.variants = variants;
        }
    }
    static class TestVariants
    {
        private static TestVariant[] mass = new TestVariant[] { 
            new TestVariant("fruitsAndVegetables", "Choose not fruit", new int[] {2},"radio",new string[] {"1","2","3","4"}),
            new TestVariant("instruments", "Show me instrument", new int[] {0,2},"checkbox",new string[] { "hammer", "pencil", "saw", "scissors"})
        };
        public static TestVariant [] getVariants
        {
            get
            {
                var rand = new Random();
                var array =(TestVariant[])mass.Clone();
                for (int i = 0; i < array.Length - 1; i++)
                {
                    int j = rand.Next(i, array.Length);
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
                return array;
            }
        }
    }
}
