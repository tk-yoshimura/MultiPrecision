using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void CreateTest() {
            Int64 min_exponent = (Int64)MultiPrecision<Pow2.N8>.ExponentMin - (Int64)MultiPrecision<Pow2.N8>.ExponentZero;
            Int64 one_exponent = min_exponent + 1;
            Int64 inf_exponent = (Int64)MultiPrecision<Pow2.N8>.ExponentMax - (Int64)MultiPrecision<Pow2.N8>.ExponentZero;
            Int64 max_exponent = inf_exponent - 1;

            { 
                MultiPrecision<Pow2.N8> n1  = new MultiPrecision<Pow2.N8>(Sign.Plus,   0, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n2  = new MultiPrecision<Pow2.N8>(Sign.Minus,  0, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n3  = new MultiPrecision<Pow2.N8>(Sign.Plus,  +1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n4  = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n5  = new MultiPrecision<Pow2.N8>(Sign.Plus,  -1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n6  = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n7  = new MultiPrecision<Pow2.N8>(Sign.Plus,  min_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n8  = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n9  = new MultiPrecision<Pow2.N8>(Sign.Plus,  one_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus,  max_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus,  inf_exponent, Mantissa<Pow2.N8>.Zero, round: false);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Zero, round: false);

                Console.WriteLine((double)n1);
                Console.WriteLine((double)n2);
                Console.WriteLine((double)n3);
                Console.WriteLine((double)n4);
                Console.WriteLine((double)n5);
                Console.WriteLine((double)n6);
                Console.WriteLine((double)n7);
                Console.WriteLine((double)n8);
                Console.WriteLine((double)n9);
                Console.WriteLine((double)n10);
                Console.WriteLine((double)n11);
                Console.WriteLine((double)n12);
                Console.WriteLine((double)n13);
                Console.WriteLine((double)n14);
                Console.Write("\n");
            }

            { 
                MultiPrecision<Pow2.N8> n1  = new MultiPrecision<Pow2.N8>(Sign.Plus,   0, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n2  = new MultiPrecision<Pow2.N8>(Sign.Minus,  0, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n3  = new MultiPrecision<Pow2.N8>(Sign.Plus,  +1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n4  = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n5  = new MultiPrecision<Pow2.N8>(Sign.Plus,  -1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n6  = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n7  = new MultiPrecision<Pow2.N8>(Sign.Plus,  min_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n8  = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n9  = new MultiPrecision<Pow2.N8>(Sign.Plus,  one_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus,  max_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus,  inf_exponent, Mantissa<Pow2.N8>.One, round: false);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.One, round: false);

                Console.WriteLine((double)n1);
                Console.WriteLine((double)n2);
                Console.WriteLine((double)n3);
                Console.WriteLine((double)n4);
                Console.WriteLine((double)n5);
                Console.WriteLine((double)n6);
                Console.WriteLine((double)n7);
                Console.WriteLine((double)n8);
                Console.WriteLine((double)n9);
                Console.WriteLine((double)n10);
                Console.WriteLine((double)n11);
                Console.WriteLine((double)n12);
                Console.WriteLine((double)n13);
                Console.WriteLine((double)n14);
                Console.Write("\n");
            }

            { 
                MultiPrecision<Pow2.N8> n1  = new MultiPrecision<Pow2.N8>(Sign.Plus,   0, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n2  = new MultiPrecision<Pow2.N8>(Sign.Minus,  0, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n3  = new MultiPrecision<Pow2.N8>(Sign.Plus,  +1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n4  = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n5  = new MultiPrecision<Pow2.N8>(Sign.Plus,  -1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n6  = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n7  = new MultiPrecision<Pow2.N8>(Sign.Plus,  min_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n8  = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n9  = new MultiPrecision<Pow2.N8>(Sign.Plus,  one_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus,  max_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus,  inf_exponent, Mantissa<Pow2.N8>.Full, round: false);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Full, round: false);

                Console.WriteLine((double)n1);
                Console.WriteLine((double)n2);
                Console.WriteLine((double)n3);
                Console.WriteLine((double)n4);
                Console.WriteLine((double)n5);
                Console.WriteLine((double)n6);
                Console.WriteLine((double)n7);
                Console.WriteLine((double)n8);
                Console.WriteLine((double)n9);
                Console.WriteLine((double)n10);
                Console.WriteLine((double)n11);
                Console.WriteLine((double)n12);
                Console.WriteLine((double)n13);
                Console.WriteLine((double)n14);
                Console.Write("\n");
            }

            { 
                MultiPrecision<Pow2.N8> n1  = new MultiPrecision<Pow2.N8>(Sign.Plus,   0, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n2  = new MultiPrecision<Pow2.N8>(Sign.Minus,  0, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n3  = new MultiPrecision<Pow2.N8>(Sign.Plus,  +1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n4  = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n5  = new MultiPrecision<Pow2.N8>(Sign.Plus,  -1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n6  = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n7  = new MultiPrecision<Pow2.N8>(Sign.Plus,  min_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n8  = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n9  = new MultiPrecision<Pow2.N8>(Sign.Plus,  one_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus,  max_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus,  inf_exponent, Mantissa<Pow2.N8>.Zero, round: true);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Zero, round: true);

                Console.WriteLine((double)n1);
                Console.WriteLine((double)n2);
                Console.WriteLine((double)n3);
                Console.WriteLine((double)n4);
                Console.WriteLine((double)n5);
                Console.WriteLine((double)n6);
                Console.WriteLine((double)n7);
                Console.WriteLine((double)n8);
                Console.WriteLine((double)n9);
                Console.WriteLine((double)n10);
                Console.WriteLine((double)n11);
                Console.WriteLine((double)n12);
                Console.WriteLine((double)n13);
                Console.WriteLine((double)n14);
                Console.Write("\n");
            }

            { 
                MultiPrecision<Pow2.N8> n1  = new MultiPrecision<Pow2.N8>(Sign.Plus,   0, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n2  = new MultiPrecision<Pow2.N8>(Sign.Minus,  0, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n3  = new MultiPrecision<Pow2.N8>(Sign.Plus,  +1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n4  = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n5  = new MultiPrecision<Pow2.N8>(Sign.Plus,  -1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n6  = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n7  = new MultiPrecision<Pow2.N8>(Sign.Plus,  min_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n8  = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n9  = new MultiPrecision<Pow2.N8>(Sign.Plus,  one_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus,  max_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus,  inf_exponent, Mantissa<Pow2.N8>.One, round: true);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.One, round: true);

                Console.WriteLine((double)n1);
                Console.WriteLine((double)n2);
                Console.WriteLine((double)n3);
                Console.WriteLine((double)n4);
                Console.WriteLine((double)n5);
                Console.WriteLine((double)n6);
                Console.WriteLine((double)n7);
                Console.WriteLine((double)n8);
                Console.WriteLine((double)n9);
                Console.WriteLine((double)n10);
                Console.WriteLine((double)n11);
                Console.WriteLine((double)n12);
                Console.WriteLine((double)n13);
                Console.WriteLine((double)n14);
                Console.Write("\n");
            }

            { 
                MultiPrecision<Pow2.N8> n1  = new MultiPrecision<Pow2.N8>(Sign.Plus,   0, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n2  = new MultiPrecision<Pow2.N8>(Sign.Minus,  0, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n3  = new MultiPrecision<Pow2.N8>(Sign.Plus,  +1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n4  = new MultiPrecision<Pow2.N8>(Sign.Minus, +1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n5  = new MultiPrecision<Pow2.N8>(Sign.Plus,  -1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n6  = new MultiPrecision<Pow2.N8>(Sign.Minus, -1, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n7  = new MultiPrecision<Pow2.N8>(Sign.Plus,  min_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n8  = new MultiPrecision<Pow2.N8>(Sign.Minus, min_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n9  = new MultiPrecision<Pow2.N8>(Sign.Plus,  one_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n10 = new MultiPrecision<Pow2.N8>(Sign.Minus, one_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n11 = new MultiPrecision<Pow2.N8>(Sign.Plus,  max_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n12 = new MultiPrecision<Pow2.N8>(Sign.Minus, max_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n13 = new MultiPrecision<Pow2.N8>(Sign.Plus,  inf_exponent, Mantissa<Pow2.N8>.Full, round: true);
                MultiPrecision<Pow2.N8> n14 = new MultiPrecision<Pow2.N8>(Sign.Minus, inf_exponent, Mantissa<Pow2.N8>.Full, round: true);

                Console.WriteLine((double)n1);
                Console.WriteLine((double)n2);
                Console.WriteLine((double)n3);
                Console.WriteLine((double)n4);
                Console.WriteLine((double)n5);
                Console.WriteLine((double)n6);
                Console.WriteLine((double)n7);
                Console.WriteLine((double)n8);
                Console.WriteLine((double)n9);
                Console.WriteLine((double)n10);
                Console.WriteLine((double)n11);
                Console.WriteLine((double)n12);
                Console.WriteLine((double)n13);
                Console.WriteLine((double)n14);
                Console.Write("\n");
            }
        }
    }
}
