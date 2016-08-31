using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.RegionService.Repository.Entities
{
    /// <summary>
    /// A US zipcode
    /// </summary>
    public class ZipCode:IComparable
    {
        private int value;

        public ZipCode() : this(int.MinValue)
        {
        }

        public ZipCode(int zip)
        {
            value = zip;
        }

        public ZipCode(string zip)
        {
            value = int.Parse(zip);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ZipCode compZipCode = obj as ZipCode;
            if (compZipCode != null)
                return this.value.CompareTo(compZipCode.value);
            else
                throw new ArgumentException("Object is not a ZipCode");
        }

        public bool IsDefault()
        {
            return value == int.MinValue;
        }

        public override string ToString()
        {
            return value.ToString("D5");
        }

        //Allow Implicit conversions to and from int (useful for range comparisons)
        static public implicit operator ZipCode(int value)
        {
            return new ZipCode(value);
        }

        static public implicit operator int(ZipCode zip)
        {
            return zip.value;
        }

    }
}
