using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataToTest
{
    public class ClassA
    {
        public ClassB classB { get; set; }
        public ClassC classC { get; set; }
        public int a;

        public ClassA()
        {
            this.a = 4;
        }
        public ClassA(int x)
        {
            this.a = x;
        }

        public ClassC getNumber(ClassB x)
        {
            ClassC y = new ClassC();
            return y;
        }
    }
}
