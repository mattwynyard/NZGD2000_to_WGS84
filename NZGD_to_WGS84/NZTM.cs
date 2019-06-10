using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NZGD_to_WGS84
{

    unsafe class NZTM
    {
        public const double PI = 3.1415926535898;
        public const double TWOPI = 2.0 * PI;
        public const double rad2deg = 180 / PI;


        public const double NZTM_A = 6378137;
        public const double NZTM_RF = 298.257222101;

        public const double NZTM_CM = 173.0;
        public const double NZTM_OLAT = 0.0;
        public const double NZTM_SF = 0.9996;
        public const double NZTM_FE = 1600000.0;
        public const double NZTM_FN = 10000000.0;

        public static tmprojection nztm_projection;
        public static Boolean initiallized = false;

        /* Structure used to define a TM projection */

        //[StructLayout(LayoutKind.Sequential)]
        public unsafe struct tmprojection
        {

        public double meridian;          /* Central meridian */
        public double scalef;            /* Scale factor */
        public double orglat;            /* Origin latitude */
        public double falsee;            /* False easting */
        public double falsen;            /* False northing */
        public double utom;              /* Unit to metre conversion */

        public double a, rf, f, e2, ep2;     /* Ellipsoid parameters */
        public double om;                /* Intermediate calculation */
    }
        
        //default constructor
        public NZTM()
        {

        }

        static void define_tmprojection(tmprojection* tm, double a, double rf, double cm, double sf, double lto, double fe, double fn, double utom)
        {

            double f;
            unsafe
            {
                tm->meridian = cm;
                tm->scalef = sf;
                tm->orglat = lto;
                tm->falsee = fe;
                tm->falsen = fn;
                tm->utom = utom;
                if (rf != 0.0) f = 1.0 / rf; else f = 0.0;
                tm->a = a;
                tm->rf = rf;
                tm->f = f;
                tm->e2 = 2.0 * f - f * f;
                tm->ep2 = tm->e2 / (1.0 - tm->e2);

                tm->om = meridian_arc(tm, tm->orglat);
            }
        }

        static tmprojection* get_nztm_projection()
        {
            if (!initiallized)
            {
                unsafe
                {
                    define_tmprojection(&nztm_projection, NZTM_A, NZTM_RF,
                        NZTM_CM / rad2deg, NZTM_SF, NZTM_OLAT / rad2deg, NZTM_FE, NZTM_FN,
                        1.0);
                }
                initiallized = true;
            }

                return &nztm_projection;

        }

        public void nztm_geod(double n, double e, ref double lt, ref double ln)
        {
            unsafe
            {
                tmprojection *nztm = get_nztm_projection();
            }
            
            //tm_geod(nztm, e, n, ln, lt);
        }

    } //end class
} //end namespace
