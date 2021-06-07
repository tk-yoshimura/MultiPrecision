using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void EllipticKTest() {
            double[] expecteds = {
                1.5707963267948966192,
                1.5708922137626348384,
                1.5711800327950414382,
                1.5716602590854044625,
                1.5723336873167065823,
                1.5732014357326469747,
                1.5742649518971571344,
                1.5755260202037926323,
                1.5769867712158131421,
                1.5786496929386844702,
                1.5805176441495632799,
                1.5825938699335270894,
                1.5848820196044282199,
                1.5873861672199078079,
                1.5901108349360414470,
                1.5930610194881743919,
                1.5962422221317835101,
                1.5996604824319273643,
                1.6033224163535283547,
                1.6072352591792112210,
                1.6114069138689479181,
                1.6158460055790900447,
                1.6205619431809133463,
                1.6255649887647804235,
                1.6308663362907171768,
                1.6364782007562008756,
                1.6424139195055974161,
                1.6486880676135124582,
                1.6553165896497853013,
                1.6623169505942065935,
                1.6697083092365505436,
                1.6775117181011207095,
                1.6857503548125960429,
                1.6944497909214828361,
                1.7036383055993221024,
                1.7133472533849584282,
                1.7236114974339794600,
                1.7344699226581490085,
                1.7459660469667769180,
                1.7581487538531659485,
                1.7710731762515814444,
                1.7848017705587752735,
                1.7994056318875859810,
                1.8149661183461979700,
                1.8315768754231192499,
                1.8493463844468008136,
                1.8684012062736826855,
                1.8888901602273695618,
                1.9109897807518291966,
                1.9349115498481339540,
                1.9609116453188370344,
                1.9893043310480998110,
                2.0204807505101105943,
                2.0549359644344846108,
                2.0933089821785136758,
                2.1364440658135019426,
                2.1854884692782236869,
                2.2420560837698999062,
                2.3085186232456553532,
                2.3885657507906773322,
                2.4884004914010351610,
                2.6196934131113362507,
                2.8087438358654412952,
                3.1398110351826142968,
            };

            for (int i = 0; i < 64; i++) {
                decimal k = i / 64m;

                double expected = expecteds[i];

                MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticK(k);
                MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticK(k);
                MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticK(k);
                MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticK(k);

                Console.WriteLine(k);
                Console.WriteLine(y4);
                Console.WriteLine(y4.ToHexcode());
                Console.WriteLine(y8.ToHexcode());
                Console.WriteLine(y16.ToHexcode());
                Console.WriteLine(y32.ToHexcode());

                Assert.AreEqual(expected, (double)y4, expected * 1e-15);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(y4, y8.Convert<Pow2.N4>(), 1));
                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y8, y16.Convert<Pow2.N8>(), 1));
                Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(y16, y32.Convert<Pow2.N16>(), 1));
            }
        }

        [TestMethod]
        public void EllipticETest() {
            double[] expecteds = {
                1.5707963267948966192,
                1.5704127613492695165,
                1.5692612206533624497,
                1.5673391612665478615,
                1.5646423092625568944,
                1.5611646068271051886,
                1.5568981352002313413,
                1.5518330115106139179,
                1.5459572561054650350,
                1.5392566258222057728,
                1.5317144071889349200,
                1.5233111616582831849,
                1.5140244125021051094,
                1.5038282596752396589,
                1.4926929044313435533,
                1.4805840591964266491,
                1.4674622093394271555,
                1.4532816807130160277,
                1.4379894480673063924,
                1.4215235911932390135,
                1.4038112620302649031,
                1.3847659565934378968,
                1.3642837714465253786,
                1.3422381292201173264,
                1.3184721079946209974,
                1.2927868476159125057,
                1.2649231666855462759,
                1.2345305754464217204,
                1.2011106307369146978,
                1.1639009595942138950,
                1.1215931702323738421,
                1.0714005291435245968,
                1.0000000000000000000
            };

            for (int i = 0; i <= 32; i++) {
                decimal k = i / 32m;

                double expected = expecteds[i];

                MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticE(k);
                MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticE(k);
                MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticE(k);
                MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticE(k);

                Console.WriteLine(k);
                Console.WriteLine(y4);
                Console.WriteLine(y4.ToHexcode());
                Console.WriteLine(y8.ToHexcode());
                Console.WriteLine(y16.ToHexcode());
                Console.WriteLine(y32.ToHexcode());

                Assert.AreEqual(expected, (double)y4, expected * 1e-15);
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(y4, y8.Convert<Pow2.N4>(), 1));
                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y8, y16.Convert<Pow2.N8>(), 1));
                Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(y16, y32.Convert<Pow2.N16>(), 1));
            }
        }

        [TestMethod]
        public void EllipticKNearOneTest() {
            const int exponent = -32;
            
            {
                MultiPrecision<Pow2.N4>[] ks = {
                    MultiPrecision<Pow2.N4>.BitDecrement(1),
                    MultiPrecision<Pow2.N4>.BitDecrement(1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N4>.BitIncrement(1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticK(k);
                    MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticK(k.Convert<Pow2.N8>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y4);
                    Console.WriteLine(y4.ToHexcode());
                    Console.WriteLine(y8.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(y4, y8.Convert<Pow2.N4>(), 32));
                }
            }

            {
                MultiPrecision<Pow2.N8>[] ks = {
                    MultiPrecision<Pow2.N8>.BitDecrement(1),
                    MultiPrecision<Pow2.N8>.BitDecrement(1 - MultiPrecision<Pow2.N8>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N8>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N8>.BitIncrement(1 - MultiPrecision<Pow2.N8>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticK(k);
                    MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticK(k.Convert<Pow2.N16>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y8);
                    Console.WriteLine(y8.ToHexcode());
                    Console.WriteLine(y16.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y8, y16.Convert<Pow2.N8>(), 64));
                }
            }

            {
                MultiPrecision<Pow2.N16>[] ks = {
                    MultiPrecision<Pow2.N16>.BitDecrement(1),
                    MultiPrecision<Pow2.N16>.BitDecrement(1 - MultiPrecision<Pow2.N16>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N16>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N16>.BitIncrement(1 - MultiPrecision<Pow2.N16>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticK(k);
                    MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticK(k.Convert<Pow2.N32>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y16);
                    Console.WriteLine(y16.ToHexcode());
                    Console.WriteLine(y32.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(y16, y32.Convert<Pow2.N16>(), 128));
                }
            }

            {
                MultiPrecision<Pow2.N32>[] ks = {
                    MultiPrecision<Pow2.N32>.BitDecrement(1),
                    MultiPrecision<Pow2.N32>.BitDecrement(1 - MultiPrecision<Pow2.N32>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N32>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N32>.BitIncrement(1 - MultiPrecision<Pow2.N32>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticK(k);
                    MultiPrecision<Pow2.N64> y64 = MultiPrecision<Pow2.N64>.EllipticK(k.Convert<Pow2.N64>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y32);
                    Console.WriteLine(y32.ToHexcode());
                    Console.WriteLine(y64.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N32>.NearlyEqualBits(y32, y64.Convert<Pow2.N32>(), 256));
                }
            }
        }

        [TestMethod]
        public void EllipticENearOneTest() {
            const int exponent = -32;
            
            {
                MultiPrecision<Pow2.N4>[] ks = {
                    MultiPrecision<Pow2.N4>.BitDecrement(1),
                    MultiPrecision<Pow2.N4>.BitDecrement(1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N4>.BitIncrement(1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticE(k);
                    MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticE(k.Convert<Pow2.N8>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y4);
                    Console.WriteLine(y4.ToHexcode());
                    Console.WriteLine(y8.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(y4, y8.Convert<Pow2.N4>(), 32));
                }
            }

            {
                MultiPrecision<Pow2.N8>[] ks = {
                    MultiPrecision<Pow2.N8>.BitDecrement(1),
                    MultiPrecision<Pow2.N8>.BitDecrement(1 - MultiPrecision<Pow2.N8>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N8>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N8>.BitIncrement(1 - MultiPrecision<Pow2.N8>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticE(k);
                    MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticE(k.Convert<Pow2.N16>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y8);
                    Console.WriteLine(y8.ToHexcode());
                    Console.WriteLine(y16.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y8, y16.Convert<Pow2.N8>(), 64));
                }
            }

            {
                MultiPrecision<Pow2.N16>[] ks = {
                    MultiPrecision<Pow2.N16>.BitDecrement(1),
                    MultiPrecision<Pow2.N16>.BitDecrement(1 - MultiPrecision<Pow2.N16>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N16>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N16>.BitIncrement(1 - MultiPrecision<Pow2.N16>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticE(k);
                    MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticE(k.Convert<Pow2.N32>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y16);
                    Console.WriteLine(y16.ToHexcode());
                    Console.WriteLine(y32.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(y16, y32.Convert<Pow2.N16>(), 128));
                }
            }

            {
                MultiPrecision<Pow2.N32>[] ks = {
                    MultiPrecision<Pow2.N32>.BitDecrement(1),
                    MultiPrecision<Pow2.N32>.BitDecrement(1 - MultiPrecision<Pow2.N32>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N32>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N32>.BitIncrement(1 - MultiPrecision<Pow2.N32>.Ldexp(1, exponent)),
                };

                foreach (var k in ks) {
                    MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticE(k);
                    MultiPrecision<Pow2.N64> y64 = MultiPrecision<Pow2.N64>.EllipticE(k.Convert<Pow2.N64>());

                    Console.WriteLine(k.ToHexcode());
                    Console.WriteLine(y32);
                    Console.WriteLine(y32.ToHexcode());
                    Console.WriteLine(y64.ToHexcode());

                    Assert.IsTrue(MultiPrecision<Pow2.N32>.NearlyEqualBits(y32, y64.Convert<Pow2.N32>(), 256));
                }
            }
        }

        [TestMethod]
        public void EllipticKUnnormalValueTest() {
            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.EllipticK(1);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);

            MultiPrecision<Pow2.N8>[] nans = new MultiPrecision<Pow2.N8>[] {
                -1,
                2,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
                MultiPrecision<Pow2.N8>.NaN
            };

            foreach (MultiPrecision<Pow2.N8> v in nans) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.EllipticK(v);

                Assert.IsTrue(y.IsNaN);
            }
        }

        [TestMethod]
        public void EllipticEUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] nans = new MultiPrecision<Pow2.N8>[] {
                -1,
                2,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
                MultiPrecision<Pow2.N8>.NaN
            };

            foreach (MultiPrecision<Pow2.N8> v in nans) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.EllipticE(v);

                Assert.IsTrue(y.IsNaN);
            }
        }
    }
}
