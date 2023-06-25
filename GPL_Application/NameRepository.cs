using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Application
{
    public class NameRepository : Container
    {
        private static string[] arithmenticOperators = { "+", "-", "*", "/" };

        public virtual iterator getIterator()
        {
            return new NameIterator();
        }

        private class NameIterator : iterator
        {
            int index;
            public virtual bool HasNext()
            {
                if (index < arithmenticOperators.Length)
                {
                    return true;
                }
                return false;
            }

            public object next()
            {
                if (HasNext())
                {
                    return arithmenticOperators[index++];
                }
                return null;
            }
        }
    }
}
