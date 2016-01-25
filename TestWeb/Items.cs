using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb
{
    public class Items
    {
        private int idx, count;

        public Items()
        {
            idx = 0;
            count = 0;
        }

        public Items(int index)
        {
            idx = index;
        }

        public Items(int index, int count)
        {
            idx = index;
            this.count = count;
        }

        public int getIndex()
        {
            return idx;
        }

        public int getCount()
        {
            return count;
        }

        public void setCount(int count)
        {
            this.count = count;
        }



    }
}
