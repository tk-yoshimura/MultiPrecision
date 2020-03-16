using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest {
    [TestClass]
    public class MultiPrecisionTest {
        [TestMethod]
        public void AccumulatorAddTest() {
            Mantissa<Pow2.N4> a = new Mantissa<Pow2.N4>(new UInt32[4] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());
            Mantissa<Pow2.N4> b = new Mantissa<Pow2.N4>(new UInt32[4] { 0xC0000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());

            for(int b_exponent = -10; b_exponent <= 10; b_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Add((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int b_exponent = -130; b_exponent <= 130; b_exponent += 10) { 
                for(int a_exponent = -130; a_exponent <= 130; a_exponent += 10) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Add((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Add((a, a_exponent), (a, a_exponent));

                Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                Console.Write("\n");
            }

            for(int a2_exponent = -10; a2_exponent <= 10; a2_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Add((a, a_exponent), (a, a2_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{a2_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }
        }

        [TestMethod]
        public void AccumulatorDiffTest() {
            Mantissa<Pow2.N4> a = new Mantissa<Pow2.N4>(new UInt32[4] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());
            Mantissa<Pow2.N4> b = new Mantissa<Pow2.N4>(new UInt32[4] { 0xC0000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());

            for(int b_exponent = -10; b_exponent <= 10; b_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Diff((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int b_exponent = -130; b_exponent <= 130; b_exponent += 10) { 
                for(int a_exponent = -130; a_exponent <= 130; a_exponent += 10) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Diff((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Diff((a, a_exponent), (a, a_exponent));

                Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                Console.Write("\n");
            }

            for(int a2_exponent = -10; a2_exponent <= 10; a2_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Diff((a, a_exponent), (a, a2_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{a2_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }
        }

        [TestMethod]
        public void AccumulatorMulTest() {
            Mantissa<Pow2.N4> a = new Mantissa<Pow2.N4>(new UInt32[4] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());
            Mantissa<Pow2.N4> b = new Mantissa<Pow2.N4>(new UInt32[4] { 0xC0000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());

            for(int b_exponent = -10; b_exponent <= 10; b_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Mul((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int b_exponent = -130; b_exponent <= 130; b_exponent += 10) { 
                for(int a_exponent = -130; a_exponent <= 130; a_exponent += 10) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Mul((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Mul((a, a_exponent), (a, a_exponent));

                Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                Console.Write("\n");
            }

            for(int a2_exponent = -10; a2_exponent <= 10; a2_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Mul((a, a_exponent), (a, a2_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{a2_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }
        }

        [TestMethod]
        public void AccumulatorDivTest() {
            Mantissa<Pow2.N4> a = new Mantissa<Pow2.N4>(new UInt32[4] { 0x80000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());
            Mantissa<Pow2.N4> b = new Mantissa<Pow2.N4>(new UInt32[4] { 0xC0000000u, 0x00000000u, 0x00000000u, 0x00000000u }.Reverse().ToArray());

            for(int b_exponent = -10; b_exponent <= 10; b_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Div((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int b_exponent = -130; b_exponent <= 130; b_exponent += 10) { 
                for(int a_exponent = -130; a_exponent <= 130; a_exponent += 10) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Div((a, a_exponent), (b, b_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{b_exponent} : {string.Join(' ', b.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }

            for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Div((a, a_exponent), (a, a_exponent));

                Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                Console.Write("\n");
            }

            for(int a2_exponent = -10; a2_exponent <= 10; a2_exponent++) { 
                for(int a_exponent = -10; a_exponent <= 10; a_exponent++) { 
                    (Mantissa<Pow2.N4> c, Int64 c_exponent) = MultiPrecision<Pow2.N4>.Div((a, a_exponent), (a, a2_exponent));

                    Console.WriteLine($"{a_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{a2_exponent} : {string.Join(' ', a.arr.Reverse().Select((n) => $"{n:X8}"))}");
                    Console.WriteLine($"{c_exponent} : {string.Join(' ', c.arr.Reverse().Select((n) => $"{n:X8}"))}");

                    Console.Write("\n");
                }
            }
        }
    }
}
