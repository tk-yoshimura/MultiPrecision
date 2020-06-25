using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Linq;
using System.Numerics;

namespace MultiPrecisionTest.BigUInt {
    public partial class BigUIntTest {

        [TestMethod]
        public void LeftShiftTest() {
            Random random = new Random(1234);

            for (int sft = 0; sft < BigUInt<Pow2.N32>.Bits; sft++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits - sft);

                BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(mantissa);
                BigInteger bi = v;

                BigUInt<Pow2.N32> v_sft = v << sft;
                BigInteger bi_sft = bi << sft;

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }

            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(1u);

                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.LeftBlockShift(v, BigUInt<Pow2.N32>.Length);
            });
        }

        [TestMethod]
        public void RightShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits);

            BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(mantissa);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<Pow2.N32>.Bits + 4; sft++) {
                BigUInt<Pow2.N32> v_sft = v >> sft;
                BigInteger bi_sft = bi >> sft;

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void LeftBlockShiftTest() {
            Random random = new Random(1234);

            for (int sft = 0; sft < BigUInt<Pow2.N32>.Length; sft++) {
                UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits - sft * UIntUtil.UInt32Bits);

                BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(mantissa);
                BigInteger bi = v;

                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.LeftBlockShift(v, sft);
                BigInteger bi_sft = bi << (sft * UIntUtil.UInt32Bits);

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }

            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(0x12345678u);

                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.LeftBlockShift(v, BigUInt<Pow2.N32>.Length);
            });
        }

        [TestMethod]
        public void RightBlockShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits);

            BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(mantissa);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<Pow2.N32>.Length + 4; sft++) {
                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.RightBlockShift(v, sft);
                BigInteger bi_sft = bi >> (sft * UIntUtil.UInt32Bits);

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void RightRoundShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits);

            BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(mantissa);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<Pow2.N32>.Bits + 4; sft++) {
                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.RightRoundShift(v, sft);
                BigInteger bi_sft;
                if (sft > 0) {
                    bi_sft = bi >> (sft - 1);

                    if ((bi_sft & 1) == 1) {
                        bi_sft = (bi_sft >> 1) + 1;
                        Console.WriteLine("RoundUp");
                    }
                    else {
                        bi_sft >>= 1;
                        Console.WriteLine("RoundDown");
                    }
                }
                else {
                    bi_sft = bi >> sft;

                    Console.WriteLine("RoundDown");
                }

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }

        [TestMethod]
        public void RightRoundBlockShiftTest() {
            Random random = new Random(1234);

            UInt32[] mantissa = UIntUtil.Random(random, BigUInt<Pow2.N32>.Length, BigUInt<Pow2.N32>.Bits);

            BigUInt<Pow2.N32> v = new BigUInt<Pow2.N32>(mantissa);
            BigInteger bi = v;

            for (int sft = 0; sft <= BigUInt<Pow2.N32>.Length + 4; sft++) {
                BigUInt<Pow2.N32> v_sft = BigUInt<Pow2.N32>.RightRoundBlockShift(v, sft);
                BigInteger bi_sft;
                if (sft > 0) {
                    bi_sft = bi >> (sft * UIntUtil.UInt32Bits - 1);

                    if ((bi_sft & 1) == 1) {
                        bi_sft = (bi_sft >> 1) + 1;
                        Console.WriteLine("RoundUp");
                    }
                    else {
                        bi_sft >>= 1;
                        Console.WriteLine("RoundDown");
                    }
                }
                else {
                    bi_sft = bi >> (sft * UIntUtil.UInt32Bits);

                    Console.WriteLine("RoundDown");
                }

                Console.WriteLine(sft);
                Console.WriteLine(v.ToHexcode());
                Console.WriteLine(v_sft.ToHexcode());
                Assert.AreEqual(bi_sft, v_sft);

                Console.Write("\n");
            }
        }
    }
}
