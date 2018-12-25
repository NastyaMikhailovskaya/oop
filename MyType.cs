using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12
{
    class MyType : ICloneable
    {
        int field1;

        public int Property { get => field1; set => field1 = value; }

        private string GetName(string name)
        {
            return name;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
