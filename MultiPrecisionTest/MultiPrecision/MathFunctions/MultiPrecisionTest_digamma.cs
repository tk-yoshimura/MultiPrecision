using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {

        [TestMethod]
        public void DigammaTest() {
            for (int i = 0; i < 200; i++) {
                MultiPrecision<Pow2.N4> x = i;
                MultiPrecision<Pow2.N4> y_expected = MultiPrecision<Pow2.N4>.HarmonicNumber(i) - MultiPrecision<Pow2.N4>.EulerGamma;
                MultiPrecision<Pow2.N4> y_actual = MultiPrecision<Pow2.N4>.Digamma(x + 1);

                Console.WriteLine(x);
                Console.WriteLine(y_expected);
                Console.WriteLine(y_actual);

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(y_expected, y_actual, 1));
            }

            for (int i = 0; i < 200; i++) {
                MultiPrecision<Pow2.N8> x = i;
                MultiPrecision<Pow2.N8> y_expected = MultiPrecision<Pow2.N8>.HarmonicNumber(i) - MultiPrecision<Pow2.N8>.EulerGamma;
                MultiPrecision<Pow2.N8> y_actual = MultiPrecision<Pow2.N8>.Digamma(x + 1);

                Console.WriteLine(x);
                Console.WriteLine(y_expected);
                Console.WriteLine(y_actual);

                Assert.IsTrue(MultiPrecision<Pow2.N8>.NearlyEqualBits(y_expected, y_actual, 1));
            }

            for (int i = 0; i < 200; i++) {
                MultiPrecision<Pow2.N16> x = i;
                MultiPrecision<Pow2.N16> y_expected = MultiPrecision<Pow2.N16>.HarmonicNumber(i) - MultiPrecision<Pow2.N16>.EulerGamma;
                MultiPrecision<Pow2.N16> y_actual = MultiPrecision<Pow2.N16>.Digamma(x + 1);

                Console.WriteLine(x);
                Console.WriteLine(y_expected);
                Console.WriteLine(y_actual);

                Assert.IsTrue(MultiPrecision<Pow2.N16>.NearlyEqualBits(y_expected, y_actual, 1));
            }

            for (int i = 0; i < 200; i++) {
                MultiPrecision<Pow2.N32> x = i;
                MultiPrecision<Pow2.N32> y_expected = MultiPrecision<Pow2.N32>.HarmonicNumber(i) - MultiPrecision<Pow2.N32>.EulerGamma;
                MultiPrecision<Pow2.N32> y_actual = MultiPrecision<Pow2.N32>.Digamma(x + 1);

                Console.WriteLine(x);
                Console.WriteLine(y_expected);
                Console.WriteLine(y_actual);

                Assert.IsTrue(MultiPrecision<Pow2.N32>.NearlyEqualBits(y_expected, y_actual, 1));
            }
        }

        [TestMethod]
        public void DigammaP75Test() {
            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEqualBits(
                    "-1.085860879786472169626886" +
                       "762817180693170075039333" +
                       "136450680334967211140389" +
                       "543644318440519631609944" +
                       "278149561847054877986358" +
                       "470142416985643391459506" +
                       "548485910863320540994877" +
                       "082382755815236352795801",
                    MultiPrecision<Pow2.N8>.Digamma(0.75),
                    2
                ));

            MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.Digamma(0.75);
            MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.Digamma(0.75);
            MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.Digamma(0.75);
            MultiPrecision<Pow2.N64> y64 = MultiPrecision<Pow2.N64>.Digamma(0.75);
            MultiPrecision<Pow2.N128> y128 = MultiPrecision<Pow2.N128>.Digamma(0.75);
            MultiPrecision<Pow2.N256> y256 = MultiPrecision<Pow2.N256>.Digamma(0.75);

            Console.WriteLine(y8);
            Console.WriteLine(y8.ToHexcode());
            Console.WriteLine(y16);
            Console.WriteLine(y16.ToHexcode());
            Console.WriteLine(y32);
            Console.WriteLine(y32.ToHexcode());
            Console.WriteLine(y64);
            Console.WriteLine(y64.ToHexcode());
            Console.WriteLine(y128);
            Console.WriteLine(y128.ToHexcode());
            Console.WriteLine(y256);
            Console.WriteLine(y256.ToHexcode());
        }

        [TestMethod]
        public void DigammaMP25Test() {
            Assert.IsTrue(
                MultiPrecision<Pow2.N8>.NearlyEqualBits(
                    "2.9141391202135278303731132" +
                      "3718281930682992496066686" +
                      "3549319665032788859610456" +
                      "3556815594803683900557218" +
                      "5043815294512201364152985" +
                      "7583014356608540493451514" +
                      "0891366794590051229176172" +
                      "4418476364720419880472602",
                    MultiPrecision<Pow2.N8>.Digamma(-0.25),
                    2
                ));

            MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.Digamma(-0.25);
            MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.Digamma(-0.25);
            MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.Digamma(-0.25);
            MultiPrecision<Pow2.N64> y64 = MultiPrecision<Pow2.N64>.Digamma(-0.25);
            MultiPrecision<Pow2.N128> y128 = MultiPrecision<Pow2.N128>.Digamma(-0.25);
            MultiPrecision<Pow2.N256> y256 = MultiPrecision<Pow2.N256>.Digamma(-0.25);

            Console.WriteLine(y8);
            Console.WriteLine(y8.ToHexcode());
            Console.WriteLine(y16);
            Console.WriteLine(y16.ToHexcode());
            Console.WriteLine(y32);
            Console.WriteLine(y32.ToHexcode());
            Console.WriteLine(y64);
            Console.WriteLine(y64.ToHexcode());
            Console.WriteLine(y128);
            Console.WriteLine(y128.ToHexcode());
            Console.WriteLine(y256);
            Console.WriteLine(y256.ToHexcode());
        }

        [TestMethod]
        public void DigammaZeropointTest() {
            const string zeropoint =
                "1.4616321449683623412626595423257213284681962040064463512959884085987864403538018102430749927337255927" +
                  "5055679336553305334161736577846698582917716838164502465254261879204438438197833355977396197607471943" +
                  "1934937175414059451930109963724166527772172791673250880463960076932978144901475185803414306536810631" +
                  "0107060169497854579337655771161136468526538644077372589890682262958196750529119944311972207258664056" +
                  "4820749522728080666492780264672546913947612363653574355170333094944302512419288581347767763803726821";

            Assert.IsTrue(MultiPrecision<Pow2.N8>.Digamma(zeropoint).Exponent < -256);

            MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.Digamma(zeropoint);
            MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.Digamma(zeropoint);
            MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.Digamma(zeropoint);

            Console.WriteLine(y8);
            Console.WriteLine(y8.ToHexcode());
            Console.WriteLine(y16);
            Console.WriteLine(y16.ToHexcode());
            Console.WriteLine(y32);
            Console.WriteLine(y32.ToHexcode());
        }

        [TestMethod]
        public void DigammaApproxBorderTest() {
            {
                List<MultiPrecision<Pow2.N4>> ys = new();

                foreach (MultiPrecision<Pow2.N4> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N4>)16, 4)) {

                    MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Digamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 14);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);
            }

            {
                List<MultiPrecision<Pow2.N8>> ys = new();

                foreach (MultiPrecision<Pow2.N8> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N8>)32, 4)) {

                    MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Digamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 14);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);
            }

            {
                List<MultiPrecision<Pow2.N16>> ys = new();

                foreach (MultiPrecision<Pow2.N16> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N16>)60, 4)) {

                    MultiPrecision<Pow2.N16> y = MultiPrecision<Pow2.N16>.Digamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 14);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);
            }

            {
                List<MultiPrecision<Pow2.N32>> ys = new();

                foreach (MultiPrecision<Pow2.N32> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N32>)116, 4)) {

                    MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.Digamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 14);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);
            }

            {
                List<MultiPrecision<Pow2.N64>> ys = new();

                foreach (MultiPrecision<Pow2.N64> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N64>)228, 4)) {

                    MultiPrecision<Pow2.N64> y = MultiPrecision<Pow2.N64>.Digamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 14);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);
            }

            {
                List<MultiPrecision<Pow2.N128>> ys = new();

                foreach (MultiPrecision<Pow2.N128> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N128>)456, 4)) {

                    MultiPrecision<Pow2.N128> y = MultiPrecision<Pow2.N128>.Digamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 14);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);
            }

            {
                List<MultiPrecision<Pow2.N256>> ys = new();

                foreach (MultiPrecision<Pow2.N256> x in TestTool.EnumerateNeighbor((MultiPrecision<Pow2.N256>)908, 4)) {

                    MultiPrecision<Pow2.N256> y = MultiPrecision<Pow2.N256>.Digamma(x);

                    Console.WriteLine(x);
                    Console.WriteLine(x.ToHexcode());
                    Console.WriteLine(y);
                    Console.WriteLine(y.ToHexcode());
                    Console.Write("\n");

                    ys.Add(y);
                }

                TestTool.NearlyNeighbors(ys, 14);
                TestTool.SmoothnessSatisfied(ys, 1);
                TestTool.MonotonicitySatisfied(ys);
            }
        }

        [TestMethod]
        public void DigammaUnnormalValueTest() {
            MultiPrecision<Pow2.N8>[] nans = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.NaN,
                0
                -1,
                -2,
                MultiPrecision<Pow2.N8>.NegativeInfinity
            };

            foreach (MultiPrecision<Pow2.N8> v in nans) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Digamma(v);

                Assert.IsTrue(y.IsNaN);
            }

            MultiPrecision<Pow2.N8>[] infs = new MultiPrecision<Pow2.N8>[] {
                MultiPrecision<Pow2.N8>.PositiveInfinity,
            };

            foreach (MultiPrecision<Pow2.N8> v in infs) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.Digamma(v);

                Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, y);
            }
        }
    }
}
