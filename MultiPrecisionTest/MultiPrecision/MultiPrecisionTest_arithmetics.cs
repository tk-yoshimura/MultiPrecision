using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;

namespace MultiPrecisionTest {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void AddTest() {
            MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.Zero;

            for(int i = 0; i < 8; i++) { 
                v += MultiPrecision<Pow2.N8>.One;

                Console.WriteLine((double)v);
            }

            for(int i = 0; i < 10; i++) { 
                v += MultiPrecision<Pow2.N8>.MinusOne;

                Console.WriteLine((double)v);
            }
        }

        [TestMethod]
        public void SubTest() {
            MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.Zero;

            for(int i = 0; i < 8; i++) { 
                v -= MultiPrecision<Pow2.N8>.One;

                Console.WriteLine((double)v);
            }

            for(int i = 0; i < 10; i++) { 
                v -= MultiPrecision<Pow2.N8>.MinusOne;

                Console.WriteLine((double)v);
            }
        }
    }
}
