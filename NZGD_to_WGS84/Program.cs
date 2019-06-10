using System;

using System.Threading;


namespace NZGD_to_WGS84
{
    unsafe class Program
    {
        public const double PI = 3.1415926535898;
        public const double TWOPI = 2.0 * PI;
        public const double rad2deg = 180 / PI;
        static void Main(string[] args)
        {
            double e, n, lt, ln, e1, n1;
            NZTM nztm = new NZTM();
            Console.WriteLine("Enter NZTM easting, northing: ");
            string line = Console.ReadLine();
            var parts = line.Split(' ');
            e = Convert.ToDouble(parts[0]);
            n = Convert.ToDouble(parts[1]);
            nztm.nztm_geod(n, e, &lt, &ln);
            nztm.geod_nztm(lt, ln, &n1, &e1);
            Console.WriteLine("Output Lat/Long: " + lt * rad2deg + " " + ln * rad2deg);
            Console.WriteLine("Output NZTM e,n: " + e1 + " " + n1);
            Console.WriteLine("Difference: " + (e1 - e) + " " + (n1 - n));
            Thread.Sleep(20000);

        }
    }
}
