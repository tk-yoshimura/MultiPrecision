using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void ToStringTest() {

            for(int i = 1; i <= 10000; i *= 10) { 
                MultiPrecision<Pow2.N8> v = i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = -1; i >= -10000; i *= 10) { 
                MultiPrecision<Pow2.N8> v = i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = 2; i <= 10000; i *= 2) { 
                MultiPrecision<Pow2.N8> v = i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = -2; i >= -10000; i *= 2) { 
                MultiPrecision<Pow2.N8> v = i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = 1; i <= 10000; i *= 10) { 
                MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.One / i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = -1; i >= -10000; i *= 10) { 
                MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.One / i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = 2; i <= 10000; i *= 2) { 
                MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.One / i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = -2; i >= -10000; i *= 2) { 
                MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.One / i;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }

            for(int i = 0; i <= 10; i++) { 
                MultiPrecision<Pow2.N8> v = (MultiPrecision<Pow2.N8>)i / 9;
                
                (Sign sign, Int64 exponent_dec, Accumulator<Pow2.N8> mantissa_dec) = v.ToStringCore();

                Console.WriteLine(sign);
                Console.WriteLine(exponent_dec);
                Console.WriteLine(mantissa_dec);
                Console.WriteLine(v.ToString());
            }
        }
    }
}
