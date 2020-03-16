using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;
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

            for(int i = 0; i < 4; i++) { 
                v -= MultiPrecision<Pow2.N8>.One;
            }

            for(int i = 0; i < 9; i++) { 
                v -= MultiPrecision<Pow2.N8>.MinusOne;

                Console.WriteLine((double)v);
            }
        }

        [TestMethod]
        public void MulTest() {
            MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.Zero;
            List<MultiPrecision<Pow2.N8>> vs = new List<MultiPrecision<Pow2.N8>>();

            for(int i = 0; i < 6; i++) { 
                v -= MultiPrecision<Pow2.N8>.One;
            }

            for(int i = 0; i < 12; i++) { 
                v -= MultiPrecision<Pow2.N8>.MinusOne;

                vs.Add(v);
            }

            foreach(var a in vs) { 
                foreach(var b in vs) {
                    var m = a * b;

                    Console.WriteLine($"{(double)a} * {(double)b} = {(double)m}");
                }
            }
        }

        [TestMethod]
        public void DivTest() {
            MultiPrecision<Pow2.N8> v = MultiPrecision<Pow2.N8>.Zero;
            List<MultiPrecision<Pow2.N8>> vs = new List<MultiPrecision<Pow2.N8>>();

            for(int i = 0; i < 6; i++) { 
                v -= MultiPrecision<Pow2.N8>.One;
            }

            for(int i = 0; i < 12; i++) { 
                v -= MultiPrecision<Pow2.N8>.MinusOne;

                vs.Add(v);
            }

            foreach(var a in vs) { 
                foreach(var b in vs) {
                    var m = a / b;

                    Console.WriteLine($"{(double)a} / {(double)b} = {(double)m}");
                }
            }
        }
    }
}
