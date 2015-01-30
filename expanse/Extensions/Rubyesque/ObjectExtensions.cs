using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions.Rubyesque
{
    public static class ObjectExtensions
    {
        public static bool IsDBNull(this object obj)
        {
            return DBNull.Value.Equals(obj);
        }

        public static object NullIfDBNull(this object obj)
        {
            return DBNull.Value.Equals(obj) ? null : obj;
        }


    }
}
