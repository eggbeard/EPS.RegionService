namespace EPS.RegionService.Repository.Entities
{
    /// <summary>
    /// Represents a contiguous range of zip code ids
    /// </summary>
    public class ZipCodeRange
    {
        private int _startRange = int.MinValue;
        private int _endRange = int.MinValue;

        public ZipCodeRange()
        {

        }

        public ZipCodeRange(int zipcode)
        {
            _startRange = zipcode;
            _endRange = zipcode;
        }

        public int Start
        {
            get { return _startRange; }
            set { _startRange = value; }
        }
        public int End
        {
            get { return _endRange; }
            set { _endRange = value; }
        }

        private bool Valid
        {
            get
            {
                return (Start <= End) && (Start >= 0);
            }

        }

        public bool TryAdd(int zipcode)
        {
            if (!Valid)
            {
                //Most likely not initialized so do that with this Zip
                Start = zipcode;
                End = zipcode;
                return true;
            }
            else if (zipcode == Start - 1)
            {
                Start = zipcode;
                return true;
            }
            else if (zipcode == End + 1)
            {
                End = zipcode;
                return true;
            }
            //Not contiguous with this ZipCode range
            return false;
        }
    }
}
