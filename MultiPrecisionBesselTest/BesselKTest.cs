using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.IO;

namespace MultiPrecisionBesselTest {
    [TestClass]
    public class BesselKTest {
        private const string outdir = "../../../results_bessel/bessel_k/";

        static BesselKTest() {
            Directory.CreateDirectory(outdir);
        }

        private static void CheckGridPoints<N>(string filepath) where N : struct, IConstant {
            using StreamWriter sw = new(filepath);
            sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");

            MultiPrecision<N> z_threshold = MultiPrecision<N>.BesselIKApproxThreshold;

            sw.WriteLine($"z threshold: {z_threshold}");

            for (decimal nu = 0; nu <= 64; nu += 1 / 8m) {
                sw.WriteLine($"nu: {nu}");

                for (MultiPrecision<N> z = 0; z < MultiPrecision<N>.Min(4 + 1 / 8m, z_threshold - 4); z += 1 / 8m) {
                    Check(sw, nu, z);
                }
                for (MultiPrecision<N> z = MultiPrecision<N>.Max(0, z_threshold - 4); z <= z_threshold + 4; z += 1 / 8m) {
                    Check(sw, nu, z);
                }
            }
        }

        private static void CheckNearlyThreshold<N>(string filepath) where N : struct, IConstant {
            using StreamWriter sw = new(filepath);
            sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");

            MultiPrecision<N> z_threshold = MultiPrecision<N>.BesselIKApproxThreshold;

            sw.WriteLine($"z threshold: {z_threshold}");

            for (decimal nu = 0; nu <= 64; nu += 1 / 8m) {
                sw.WriteLine($"nu: {nu}");

                Check(sw, nu, MultiPrecision<N>.BitDecrement(z_threshold));
                Check(sw, nu, z_threshold);
                Check(sw, nu, MultiPrecision<N>.BitIncrement(z_threshold));
            }
        }

        private static void CheckNearlyZero<N>(string filepath) where N : struct, IConstant {
            using StreamWriter sw = new(filepath);
            sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");

            MultiPrecision<N> z = MultiPrecision<N>.Ldexp(1, -0xFFFFFF);

            sw.WriteLine($"z threshold: {z}");

            for (decimal nu = -64; nu <= 64; nu += 1 / 4m) {
                sw.WriteLine($"nu: {nu}");

                Check(sw, nu, MultiPrecision<N>.BitDecrement(z));
                Check(sw, nu, z);
                Check(sw, nu, MultiPrecision<N>.BitIncrement(z));
            }
        }

        private static void CheckNearlyIntegerNu<N>(string filepath) where N : struct, IConstant {
            using StreamWriter sw = new(filepath);
            sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");

            MultiPrecision<N> z_threshold = MultiPrecision<N>.BesselIKApproxThreshold;

            sw.WriteLine($"z threshold: {z_threshold}");

            MultiPrecision<N>[] test_dnu = new MultiPrecision<N>[] {
                    MultiPrecision<N>.Ldexp(1, -2),
                    MultiPrecision<N>.Ldexp(1, -32),
                    MultiPrecision<N>.Ldexp(1, -64),
                    MultiPrecision<N>.Ldexp(1, -96),
                    MultiPrecision<N>.Ldexp(1, -97),
                    MultiPrecision<N>.Ldexp(1, -128),
                    MultiPrecision<N>.Ldexp(1, -272),
                };

            for (int n = 0; n <= 64; n += n < 4 ? 1 : 4) {
                foreach (MultiPrecision<N> dnu in test_dnu) {
                    MultiPrecision<N> nu_pos = n + dnu;

                    if (nu_pos <= 64 && n != n + dnu) {
                        sw.WriteLine($"nu: {nu_pos}");

                        Check(sw, nu_pos, MultiPrecision<N>.BitDecrement(z_threshold));
                        Check(sw, nu_pos, z_threshold);
                        Check(sw, nu_pos, MultiPrecision<N>.BitIncrement(z_threshold));
                    }

                    MultiPrecision<N> nu_neg = n - dnu;

                    if (nu_neg >= 0 && n != n + dnu) {
                        sw.WriteLine($"nu: {nu_neg}");

                        Check(sw, nu_neg, MultiPrecision<N>.BitDecrement(z_threshold));
                        Check(sw, nu_neg, z_threshold);
                        Check(sw, nu_neg, MultiPrecision<N>.BitIncrement(z_threshold));
                    }
                }

                if ((n - MultiPrecision<N>.BitDecrement(n)).Exponent >= -272 && MultiPrecision<N>.BitDecrement(n) > 0) {
                    sw.WriteLine($"nu: {MultiPrecision<N>.BitDecrement(n).ToHexcode()}");

                    Check(sw, MultiPrecision<N>.BitDecrement(n), MultiPrecision<N>.BitDecrement(z_threshold));
                    Check(sw, MultiPrecision<N>.BitDecrement(n), z_threshold);
                    Check(sw, MultiPrecision<N>.BitDecrement(n), MultiPrecision<N>.BitIncrement(z_threshold));
                }
                if ((n - MultiPrecision<N>.BitIncrement(n)).Exponent >= -272 && MultiPrecision<N>.BitIncrement(n) < 64) {
                    sw.WriteLine($"nu: {MultiPrecision<N>.BitIncrement(n).ToHexcode()}");

                    Check(sw, MultiPrecision<N>.BitIncrement(n), MultiPrecision<N>.BitDecrement(z_threshold));
                    Check(sw, MultiPrecision<N>.BitIncrement(n), z_threshold);
                    Check(sw, MultiPrecision<N>.BitIncrement(n), MultiPrecision<N>.BitIncrement(z_threshold));
                }
            }
        }

        private static void CheckNearlyOne<N>(string filepath) where N : struct, IConstant {
            using StreamWriter sw = new(filepath);
            sw.WriteLine($"bits: {MultiPrecision<N>.Bits}");

            for (decimal n = -64; n <= 64; n++) {
                sw.WriteLine($"nu: {n}");

                for (int exp = -16; exp >= -MultiPrecision<N>.Bits / 2; exp *= 2) {
                    Check(sw, n, 1 - MultiPrecision<N>.Ldexp(1, exp));
                    Check(sw, n, 1 + MultiPrecision<N>.Ldexp(1, exp));
                }

                for (int exp = -MultiPrecision<N>.Bits + 4; exp >= -MultiPrecision<N>.Bits; exp--) {
                    Check(sw, n, 1 - MultiPrecision<N>.Ldexp(1, exp));
                    Check(sw, n, 1 + MultiPrecision<N>.Ldexp(1, exp));
                }

                Check<N>(sw, n, 1);
            }
        }

        private static void Check<N>(StreamWriter sw, MultiPrecision<N> nu, MultiPrecision<N> z) where N : struct, IConstant {
            MultiPrecision<N> t = MultiPrecision<N>.BesselK(nu, z);
            MultiPrecision<Plus1<N>> s = MultiPrecision<Plus1<N>>.BesselK(nu.Convert<Plus1<N>>(), z.Convert<Plus1<N>>());

            sw.WriteLine($"  z: {z}");
            sw.WriteLine($"  {z.ToHexcode()}");
            sw.WriteLine($"  f: {t}");
            sw.WriteLine($"  {t.ToHexcode()}");
            sw.WriteLine($"  {s.ToHexcode()}");

            if (MultiPrecision<N>.IsNaN(t) && MultiPrecision<Plus1<N>>.IsNaN(s)) {
                return;
            }
            if (!MultiPrecision<N>.IsFinite(t) && !MultiPrecision<Plus1<N>>.IsFinite(s) && t.Sign == s.Sign) {
                return;
            }

            Assert.IsTrue(t.Sign == Sign.Plus);
            Assert.IsTrue(s.Sign == Sign.Plus);

            sw.Flush();
            Assert.IsTrue(MultiPrecision<N>.NearlyEqualBits(t, s.Convert<N>(), 1));
        }

        [TestMethod]
        public void Length4GridPointsTest() {
            CheckGridPoints<Pow2.N4>(outdir + "n4_grid.txt");
        }

        [TestMethod]
        public void Length4NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N4>(outdir + "n4_near_threshold.txt");
        }

        [TestMethod]
        public void Length4NearlyIntegerNuTest() {
            CheckNearlyIntegerNu<Pow2.N4>(outdir + "n4_near_integer_nu.txt");
        }

        [TestMethod]
        public void Length4NearlyOne() {
            CheckNearlyOne<Pow2.N4>(outdir + "n4_near_one.txt");
        }

        [TestMethod]
        public void Length5GridPointsTest() {
            CheckGridPoints<Plus1<Pow2.N4>>(outdir + "n5_grid.txt");
        }

        [TestMethod]
        public void Length5NearlyThresholdTest() {
            CheckNearlyThreshold<Plus1<Pow2.N4>>(outdir + "n5_near_threshold.txt");
        }

        [TestMethod]
        public void Length5NearlyIntegerNuTest() {
            CheckNearlyIntegerNu<Plus1<Pow2.N4>>(outdir + "n5_near_integer_nu.txt");
        }

        [TestMethod]
        public void Length5NearlyOne() {
            CheckNearlyOne<Plus1<Pow2.N4>>(outdir + "n5_near_one.txt");
        }

        [TestMethod]
        public void Length8GridPointsTest() {
            CheckGridPoints<Pow2.N8>(outdir + "n8_grid.txt");
        }

        [TestMethod]
        public void Length8NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N8>(outdir + "n8_near_threshold.txt");
        }

        [TestMethod]
        public void Length8NearlyZeroTest() {
            CheckNearlyZero<Pow2.N8>(outdir + "n8_near_zero.txt");
        }

        [TestMethod]
        public void Length8NearlyIntegerNuTest() {
            CheckNearlyIntegerNu<Pow2.N8>(outdir + "n8_near_integer_nu.txt");
        }

        [TestMethod]
        public void Length8NearlyOne() {
            CheckNearlyOne<Pow2.N8>(outdir + "n8_near_one.txt");
        }

        [TestMethod]
        public void Length16GridPointsTest() {
            CheckGridPoints<Pow2.N16>(outdir + "n16_grid.txt");
        }

        [TestMethod]
        public void Length16NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N16>(outdir + "n16_near_threshold.txt");
        }

        [TestMethod]
        public void Length16NearlyIntegerNuTest() {
            CheckNearlyIntegerNu<Pow2.N16>(outdir + "n16_near_integer_nu.txt");
        }

        [TestMethod]
        public void Length16NearlyOne() {
            CheckNearlyOne<Pow2.N16>(outdir + "n16_near_one.txt");
        }

        [TestMethod]
        public void Length32GridPointsTest() {
            CheckGridPoints<Pow2.N32>(outdir + "n32_grid.txt");
        }

        [TestMethod]
        public void Length32NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N32>(outdir + "n32_near_threshold.txt");
        }

        [TestMethod]
        public void Length32NearlyIntegerNuTest() {
            CheckNearlyIntegerNu<Pow2.N32>(outdir + "n32_near_integer_nu.txt");
        }

        [TestMethod]
        public void Length32NearlyOne() {
            CheckNearlyOne<Pow2.N32>(outdir + "n32_near_one.txt");
        }

        [TestMethod]
        public void Length64GridPointsTest() {
            CheckGridPoints<Pow2.N64>(outdir + "n64_grid.txt");
        }

        [TestMethod]
        public void Length64NearlyThresholdTest() {
            CheckNearlyThreshold<Pow2.N64>(outdir + "n64_near_threshold.txt");
        }

        [TestMethod]
        public void Length64NearlyIntegerNuTest() {
            CheckNearlyIntegerNu<Pow2.N64>(outdir + "n64_near_integer_nu.txt");
        }

        [TestMethod]
        public void Length64NearlyOne() {
            CheckNearlyOne<Pow2.N64>(outdir + "n64_near_one.txt");
        }

        [TestMethod]
        public void ExpectedTest() {
            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N4> nu = (MultiPrecision<Pow2.N4>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N4>.BesselK(nu, 2);
                double expected = expected_x2[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N4>.Bits} nu={nu}, x=2");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N4> nu = (MultiPrecision<Pow2.N4>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N4>.BesselK(nu, 16);
                double expected = expected_x16[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N4>.Bits} nu={nu}, x=16");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N4> nu = (MultiPrecision<Pow2.N4>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N4>.BesselK(nu, 128);
                double expected = expected_x128[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N4>.Bits} nu={nu}, x=128");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N8> nu = (MultiPrecision<Pow2.N8>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N8>.BesselK(nu, 2);
                double expected = expected_x2[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N8>.Bits} nu={nu}, x=2");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N8> nu = (MultiPrecision<Pow2.N8>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N8>.BesselK(nu, 16);
                double expected = expected_x16[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N8>.Bits} nu={nu}, x=16");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N8> nu = (MultiPrecision<Pow2.N8>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N8>.BesselK(nu, 128);
                double expected = expected_x128[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N8>.Bits} nu={nu}, x=128");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N16> nu = (MultiPrecision<Pow2.N16>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N16>.BesselK(nu, 2);
                double expected = expected_x2[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N16>.Bits} nu={nu}, x=2");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N16> nu = (MultiPrecision<Pow2.N16>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N16>.BesselK(nu, 16);
                double expected = expected_x16[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N16>.Bits} nu={nu}, x=16");
            }

            for (int i = 0; i <= 512; i++) {
                MultiPrecision<Pow2.N16> nu = (MultiPrecision<Pow2.N16>)i / 8;

                double actual = (double)MultiPrecision<Pow2.N16>.BesselK(nu, 128);
                double expected = expected_x128[i];

                Assert.AreEqual(expected, actual, Math.Abs(expected) * 1e-15, $"bits {MultiPrecision<Pow2.N16>.Bits} nu={nu}, x=128");
            }
        }

        static readonly double[] expected_x2 = {
            0.11389387274953343565,
            0.11426333558821678800,
            0.11537827684085675697,
            0.11725853380051725448,
            0.11993777196806144737,
            0.12346441275286677661,
            0.12790297862917902633,
            0.13333590453459638310,
            0.13986588181652242728,
            0.14761882148312348160,
            0.15674754783939321557,
            0.16743636292806074704,
            0.17990665795209217105,
            0.19442379177105898987,
            0.21130551081274102672,
            0.23093225205598862321,
            0.25375975456605586294,
            0.28033450975673070480,
            0.31131271164009827644,
            0.34748353282660078167,
            0.38979775889619970395,
            0.43940307438083763515,
            0.49768762255147582309,
            0.56633387713957505162,
            0.64738539094863415316,
            0.74332965471617622929,
            0.85720114902961433756,
            0.99270975339123760351,
            1.1544010551925914309,
            1.3478568620207577821,
            1.5799464728292995402,
            1.8591421488322668966,
            2.1959159274119583224,
            2.6032396807447814213,
            3.0972164459863448735,
            3.6978789505220276935,
            4.4302014520702697122,
            5.3253841992060845954,
            6.4224868956613490989,
            7.7705097038646092760,
            9.4310491005964674428,
            11.481693337788399592,
            14.020371044471580050,
            17.170930161925108763,
            21.090307589508805136,
            25.977758783348899036,
            32.086759227220707760,
            39.740376955172237117,
            49.351161430394295537,
            61.446918036910329332,
            76.704164429462140136,
            95.991628570869487293,
            120.42689319436869796,
            151.45027735554364167,
            190.92135245218041872,
            241.24522431550150234,
            305.53801768296224066,
            387.84406631386416675,
            493.42139872860995590,
            629.11756230121809026,
            803.86511335290534186,
            1029.3358462638255251,
            1320.8058882794385341,
            1698.3012941242450657,
            2188.1172852111299802,
            2824.8358905231925174,
            3654.0093052118843204,
            4735.7336505423529029,
            6149.4152433411587619,
            8000.1361051172132706,
            10427.166986617829058,
            13615.367915543931395,
            17810.476299372002082,
            23339.635676814803371,
            30638.998166726655599,
            40290.886885593423652,
            53073.894681752754818,
            70030.509752899789984,
            92558.517021185442792,
            122534.69154457663619,
            162482.40397955914872,
            215799.01144145827328,
            287064.74234743344861,
            382462.79820298069964,
            510351.41471999232954,
            682043.79247677769187,
            912872.70794317589628,
            1.2236454469182382138e6,
            1.6426345160949634893e6,
            2.2083046265215798203e6,
            2.9730526072279195039e6,
            4.0083424182415181824e6,
            5.4117637492416722149e6,
            7.3167458048186627661e6,
            9.9059401274103263278e6,
            1.3429678926780417211e7,
            1.8231462081024157531e7,
            2.4783187981494033774e7,
            3.3733906573661527867e7,
            4.5977357805700250025e7,
            6.2745634530999222801e7,
            8.5739213773493732348e7,
            1.1730766920501451025e8,
            1.6070108270243569260e8,
            2.2042017948838485386e8,
            3.0270445890213673933e8,
            4.1621340813458163588e8,
            5.7297814526378211224e8,
            7.8973219538673195723e8,
            1.0897743196951770337e9,
            1.5055787224913453320e9,
            2.0824561187206399594e9,
            2.8836937954300272577e9,
            3.9977792110720387375e9,
            5.5485615643568682032e9,
            7.7095600507087860013e9,
            1.0724130272251880645e10,
            1.4933914319620280816e10,
            2.0819015103461012825e10,
            2.9054779729951315130e10,
            4.0592133315508766461e10,
            5.6771335815294683906e10,
            7.9483215700219953532e10,
            1.1139790387420258088e11,
            1.5628962114303900132e11,
            2.1949827124414178397e11,
            3.0858605149854128450e11,
            4.3427230460174645251e11,
            6.1176569352806152418e11,
            8.6266423341740413282e11,
            1.2176676009927111596e12,
            1.7204523321165734670e12,
            2.4332132579893564010e12,
            3.4445944025093356553e12,
            4.8810493262054862437e12,
            6.9231276152826762487e12,
            9.8288432297644931533e12,
            1.3967232099670936326e13,
            1.9866581731831776296e13,
            2.8283804842283093104e13,
            4.0304308377967419619e13,
            5.7485880212961847053e13,
            8.2066162265440435867e13,
            1.1726205081249690815e14,
            1.6770210059952444513e14,
            2.4005151394028218871e14,
            3.4391620247509085227e14,
            4.9315156146678531614e14,
            7.0775860987241919973e14,
            1.0166332331559618900e15,
            1.4615554295377732229e15,
            2.1029822858886649094e15,
            3.0284666540212045055e15,
            4.3649009222672856067e15,
            6.2963372769022398303e15,
            9.0899437467944632772e15,
            1.3133838591017722615e16,
            1.8992279847742752048e16,
            2.7486230466098688365e16,
            3.9811052696961047074e16,
            5.7708568527002410050e16,
            8.3718781652302119417e16,
            1.2154840878284320759e17,
            1.7661081165560951131e17,
            2.5681761113471801018e17,
            3.7374012524510747083e17,
            5.4431460713498686843e17,
            7.9334765463798947550e17,
            1.1571998371940694055e18,
            1.6892053816748474389e18,
            2.4676516151294771934e18,
            3.6075352312298382563e18,
            5.2778948668527369314e18,
            7.7273823630280843378e18,
            1.1322014328517076208e19,
            1.6600943343264991348e19,
            2.4358905149602459925e19,
            3.5768182469533454265e19,
            5.2559145230284233568e19,
            7.7287676379193402239e19,
            1.1373155724846856203e20,
            1.6747838372572743128e20,
            2.4679812625238139440e20,
            3.6393898328855967522e20,
            5.3705311312844818777e20,
            7.9306024252010252306e20,
            1.1719086329889536741e21,
            1.7329192942156822134e21,
            2.5642379329573953827e21,
            3.7969258141576112170e21,
            5.6259793865701937988e21,
            8.3417051860690675619e21,
            1.2376580507103910779e22,
            1.8375286290746904300e22,
            2.7299434862223457156e22,
            4.0584276178670765139e22,
            6.0373322981747260056e22,
            8.9869850743199292432e22,
            1.3386380855729448412e23,
            1.9952215030068754772e23,
            2.9757498528362230687e23,
            4.4409684200678916876e23,
            6.6318320404190778971e23,
            9.9097465114931558248e23,
            1.4817106509857652667e24,
            2.2168420003654401874e24,
            3.3187552411796086757e24,
            4.9714551939156718170e24,
            7.4517512125976615826e24,
            1.1176308441711324769e25,
            1.6772675336920395147e25,
            2.5186566049092553671e25,
            3.7843994923118761562e25,
            5.6896446110107604093e25,
            8.5591811268932217882e25,
            1.2883592529286869581e26,
            1.9404310651282282345e26,
            2.9242515488171514877e26,
            4.4094591079820228041e26,
            6.6528665419596541865e26,
            1.0043475761136329467e27,
            1.5170847196819803992e27,
            2.2928997066851164370e27,
            3.4674369474397618718e27,
            5.2466156270588138948e27,
            7.9432086346082347352e27,
            1.2032548744587932536e28,
            1.8237408724663645889e28,
            2.7657402338048024795e28,
            4.1966361827324816131e28,
            6.3713558671780913345e28,
            9.6783640835176230871e28,
            1.4709928066415961188e29,
            2.2369516800323831708e29,
            3.4036044794540729643e29,
            5.1815175921652691752e29,
            7.8924031421048233960e29,
            1.2028041920268548422e30,
            1.8340577115203863751e30,
            2.7980950660631534283e30,
            4.2711257548876875584e30,
            6.5230649767289242196e30,
            9.9675756511477513531e30,
            1.5238945335710141848e31,
            2.3310246671547277043e31,
            3.5675040550622899515e31,
            5.4626930476403275573e31,
            8.3689873739471884901e31,
            1.2828087192729478636e32,
            1.9673102759196208043e32,
            3.0185952389516488573e32,
            4.6340111633141208555e32,
            7.1175176379640243215e32,
            1.0937509210548531525e33,
            1.6816121698609211102e33,
            2.5867229467722575997e33,
            3.9809781555010260648e33,
            6.1297762987765486777e33,
            9.4430776973750504303e33,
            1.4554448970233764326e34,
            2.2443490806258223890e34,
            3.4625547918910353847e34,
            5.3445813323560648526e34,
            8.2535483802105182877e34,
            1.2751958184796012886e35,
            1.9711579462578858835e35,
            3.0484111526424054126e35,
            4.7166368652764953214e35,
            7.3012520296718867885e35,
            1.1307522517755051474e36,
            1.7520319985164721603e36,
            2.7159407529409801447e36,
            4.2121271791381852785e36,
            6.5355904732780235378e36,
            1.0145410160233373047e37,
            1.5756329986830536900e37,
            2.4481637790207078965e37,
            3.8056170013870270936e37,
            5.9184525763254496060e37,
            9.2085028489677807584e37,
            1.4333984367254625960e38,
            2.2322414069523834181e38,
            3.4778513910325726742e38,
            5.4209550698382735546e38,
            8.4534662896511141298e38,
            1.3188256389820336363e39,
            2.0584143022716102102e39,
            3.2141813093304545196e39,
            5.0211066557182572712e39,
            7.8472835323935247798e39,
            1.2269571563550052049e40,
            1.9192384889539723236e40,
            3.0034286966051662240e40,
            4.7021219558748818564e40,
            7.3647495831973319512e40,
            1.1540083950071973370e41,
            1.8090317944952980802e41,
            2.8370634174841132101e41,
            4.4511975431779264406e41,
            6.9866509586399126007e41,
            1.0970968208898507832e42,
            1.7234709919781575135e42,
            2.7086038861272911023e42,
            4.2586201378983706347e42,
            6.6984387462883211541e42,
            1.0540445220942163817e43,
            1.6592980419901326043e43,
            2.6131800342806213068e43,
            4.1171165070335456031e43,
            6.4892617292736925265e43,
            1.0232344419713721243e44,
            1.6141063856240150752e44,
            2.5472157553840573366e44,
            4.0213818039016840686e44,
            6.3512662081554351380e44,
            1.0035065032510524178e45,
            1.5861869520288049080e45,
            2.5082008139239418959e45,
            3.9677420665251942728e45,
            6.2790971942512569755e45,
            9.9408398847441119340e45,
            1.5744196752986281082e46,
            2.4945312847429984243e46,
            3.9539200365852995162e46,
            6.2695555770208129321e46,
            9.9452349869028934550e46,
            1.5782007058857360956e47,
            2.5054041125933127341e47,
            3.9788831696530288309e47,
            6.3213803289396469683e47,
            1.0046839687298724093e48,
            1.5973987212745657321e48,
            2.5407561956454580424e48,
            4.0427599142432244080e48,
            6.4351356185508997837e48,
            1.0247118407419417058e49,
            1.6323361835462162319e49,
            2.6012420799517284438e49,
            4.1468159022954666867e49,
            6.6131911293101010161e49,
            1.0550407767505671689e50,
            1.6837933378024324492e50,
            2.6882473214508863958e50,
            4.2934862372194742056e50,
            6.8597908540637612027e50,
            1.0964053642125595716e51,
            1.7530344026885645475e51,
            2.8039371397664298713e51,
            4.4864640573855559259e51,
            7.1812118622971115390e51,
            1.1498692434821090242e52,
            1.8418569360485915073e52,
            2.9513424034309635334e52,
            4.7308493752466148812e52,
            7.5860206075303371348e52,
            1.2168690534866199668e53,
            1.9526669057394673949e53,
            3.1344874682649173413e53,
            5.0333661875556778672e53,
            8.0854407931504147127e53,
            1.2992766365950303308e54,
            2.0885836921917813759e54,
            3.3585671532348627467e54,
            5.4026603619866425324e54,
            8.6938541945980154633e54,
            1.3994831538994490747e55,
            2.2535812381746479546e55,
            3.6301834128622971938e55,
            5.8496962070810674522e55,
            9.4294647603906600736e55,
            1.5205102388995284266e56,
            2.4526740083049256691e56,
            3.9576563254478365032e56,
            6.3882763771345013208e56,
            1.0315167530836570070e57,
            1.6661551847298938791e57,
            2.6921595318938860583e57,
            4.3514292044223837403e57,
            7.0357184220635538358e57,
            1.1379678373876079433e58,
            1.8411795767527037755e58,
            2.9799333439928606899e58,
            4.8245944019042711557e58,
            7.8137326118342398556e58,
            1.2658999496108345542e59,
            2.0515539590600874036e59,
            3.3258974646639287158e59,
            5.3935753036320975570e59,
            8.7495606459007907703e59,
            1.4198320827143133537e60,
            2.3047753436623731339e60,
            3.7424906431003722248e60,
            6.0790119176638997461e60,
            9.8774548571810930137e60,
            1.6054490985425519608e61,
            2.6102800209694148011e61,
            4.2453780928386362274e61,
            6.9069134355423415430e61,
            1.1240604394755973299e62,
            1.8299236750764903488e62,
            2.9799817396049217101e62,
            4.8543512525492720304e62,
            7.9101627077867323355e62,
            1.2893651178840117678e63,
            2.1023371120197150116e63,
            3.4289756244706013041e63,
            5.5945054617347590892e63,
            9.1304868200870959871e63,
            1.4905987709942272450e64,
            2.4342313108260407145e64,
            3.9764622097613755505e64,
            6.4977870613616786951e64,
            1.0621047793792399445e65,
            1.7366096012317961443e65,
            2.8403355822698658351e65,
            4.6469650933943865738e65,
            7.6050337138101638714e65,
            1.2449861927850682425e66,
            2.0387278987734836429e66,
            3.3395274678924464414e66,
            5.4719419509151054291e66,
            8.9686760419836181965e66,
            1.4704331143708290456e67,
            2.4115261908803467447e67,
            3.9561081299522794404e67,
            6.4919247612029942548e67,
            1.0656329733301213410e68,
            1.7497272900148049915e68,
            2.8738316290098095902e68,
            4.7215023766951108720e68,
            7.7593750138883930813e68,
            1.2755591699373227799e69,
            2.0974978122460891198e69,
            3.4500800155818757661e69,
            5.6765343108816696243e69,
            9.3425089379219140888e69,
            1.5380471157153396413e70,
            2.5328025171069515669e70,
            4.1721345030793821102e70,
            6.8744865542282068237e70,
            1.1330444294258833526e71,
            1.8680050009098105578e71,
            3.0805854966266358925e71,
            5.0817389622850555908e71,
            8.3852306122776108548e71,
            1.3840155252073418045e72,
            2.2850195779373505447e72,
            3.7736500558026658173e72,
            6.2338418596546045286e72,
            1.0300827647530912576e73,
            1.7025911403173044976e73,
            2.8149472012591417248e73,
            4.6553410369297893641e73,
            7.7011191614829457393e73,
            1.2743156281503808669e74,
            2.1092144173351623461e74,
            3.4920844858360044194e74,
            5.7832075221776344937e74,
            9.5801557497814644348e74,
            1.5874346586060696529e75,
            2.6311062089265587518e75,
            4.3621427407149253667e75,
            7.2340262093313487700e75,
            1.1999930648649538509e76,
            1.9911115411124879795e76,
            3.3046873798087267958e76,
            5.4863417578902056934e76,
            9.1107213009535837754e76,
            1.5133516042364642612e77,
            2.5144548662531240371e77,
            4.1789244515170042955e77,
            6.9470690773232555745e77,
            1.1551939022938266285e78,
            1.9214278602660402135e78,
            3.1967520895460229628e78,
            5.3199709940902605985e78,
            8.8557379909922424868e78,
            1.4745353796149654593e79,
            2.4558415178871731585e79,
            4.0912869123389316733e79,
            6.8176351350746895964e79,
            1.1363746911202771489e80,
            1.8946242472318076261e80,
            3.1596438498711875887e80,
            5.2706774562446207439e80,
            8.7944316558204846751e80,
            1.4677831993827376626e81,
            2.4503527456706676650e81,
            4.0917362749471075845e81,
            6.8343742582209323981e81,
            1.1418307841661186970e82,
            1.9081669714591385328e82,
            3.1896454348270947743e82,
            5.3330987267207837997e82,
            8.9192387777680184737e82,
            1.4920613626182528342e83,
            2.4966408912312430955e83,
            4.1786476400286652055e83,
            6.9956081772647088267e83,
            1.1714534431180333932e84,
            1.9621590101642877483e84,
            3.2874015335072650651e84,
            5.5090977284711341451e84,
            9.2345800339461100794e84,
            1.5483265261908654300e85,
            2.5966682837936303521e85,
            4.3559079211314473633e85,
            7.3088490184201924289e85,
            1.2266683458961625522e86,
            2.0592685202315968254e86,
            3.4578507484934134779e86,
            5.8077342577062349653e86,
            9.7569537558936834518e86
        };

        static readonly double[] expected_x16 = {
            3.4994116639364989360e-8,
            3.5010706421695284807e-8,
            3.5060522093824139200e-8,
            3.5143702842976369652e-8,
            3.5260481355229332570e-8,
            3.5411184881242855045e-8,
            3.5596236737671314711e-8,
            3.5816158253737776758e-8,
            3.6071571175287796881e-8,
            3.6363200541576765583e-8,
            3.6691878053103319061e-8,
            3.7058545952007372372e-8,
            3.7464261439931165856e-8,
            3.7910201661823467703e-8,
            3.8397669287980824954e-8,
            3.8928098730697854140e-8,
            3.9503063036275963971e-8,
            4.0124281497854517467e-8,
            4.0793628039621532803e-8,
            4.1513140428477636779e-8,
            4.2285030375216426168e-8,
            4.3111694593800746922e-8,
            4.3995726894417120170e-8,
            4.4939931393745086322e-8,
            4.5947336934356787873e-8,
            4.7021212814444371786e-8,
            4.8165085939246875162e-8,
            4.9382759516711670791e-8,
            5.0678333432186299033e-8,
            5.2056226450414337786e-8,
            5.3521200407936710012e-8,
            5.5078386575324994537e-8,
            5.6733314386659759423e-8,
            5.8491942753496850196e-8,
            6.0360694202440575838e-8,
            6.2346492099590372894e-8,
            6.4456801251797931995e-8,
            6.6699672204144743731e-8,
            6.9083789585637452988e-8,
            7.1618524891168130551e-8,
            7.4313994127686667585e-8,
            7.7181120796716185168e-8,
            8.0231704734293431076e-8,
            8.3478497383675155967e-8,
            8.6935284136322635780e-8,
            9.0616974443435517756e-8,
            9.4539700474408947724e-8,
            9.8720925180880574092e-8,
            1.0317956071646392666e-7,
            1.0793609826389315632e-7,
            1.1301275043432063998e-7,
            1.1843360752924711831e-7,
            1.2422480909551974409e-7,
            1.3041473235968534215e-7,
            1.3703419930161888416e-7,
            1.4411670432087730215e-7,
            1.5169866466503461258e-7,
            1.5981969603000938297e-7,
            1.6852291601110643106e-7,
            1.7785527838354395337e-7,
            1.8786794152643242786e-7,
            1.9861667467879994173e-7,
            2.1016230613514988124e-7,
            2.2257121795663450563e-7,
            2.3591589229836921267e-7,
            2.5027551504062026303e-7,
            2.6573664306938584313e-7,
            2.8239394228907670032e-7,
            3.0035100427655014521e-7,
            3.1972125041291653661e-7,
            3.4062893337004533161e-7,
            3.6321024699693939363e-7,
            3.8761455696340382526e-7,
            4.1400576599313933762e-7,
            4.4256382917641058179e-7,
            4.7348643671742112402e-7,
            5.0699088357026695714e-7,
            5.4331614778022558276e-7,
            5.8272520200863696269e-7,
            6.2550758571886414543e-7,
            6.7198226888219851609e-7,
            7.2250084187654482000e-7,
            7.7745107055461057832e-7,
            8.3726086031730458003e-7,
            9.0240267851624215681e-7,
            9.7339849071100044087e-7,
            1.0508252733180716299e-6,
            1.1353211731186623231e-6,
            1.2275923930661519704e-6,
            1.3284208939931413754e-6,
            1.4386730133245053853e-6,
            1.5593091149414255012e-6,
            1.6913943991228347880e-6,
            1.8361110182557730433e-6,
            1.9947716630297957154e-6,
            2.1688348054270457410e-6,
            2.3599218093481574753e-6,
            2.5698361475857570452e-6,
            2.8005849955421962764e-6,
            3.0544035081246439646e-6,
            3.3337821272553171645e-6,
            3.6414973141139206444e-6,
            3.9806461533930840868e-6,
            4.3546853374244333449e-6,
            4.7674751070883881834e-6,
            5.2233288051778043971e-6,
            5.7270687877484934334e-6,
            6.2840895415717341340e-6,
            6.9004289729592678575e-6,
            7.5828489670918040603e-6,
            8.3389264700000234787e-6,
            9.1771565203444931553e-6,
            0.000010107068858366788273,
            0.000011139359968580592384,
            0.000012286042675250638525,
            0.000013560615710439886970,
            0.000014978256019124081674,
            0.000016556036961192149435,
            0.000018313176023705624441,
            0.000020271316177396913661,
            0.000022454845609230267662,
            0.000024891261249702912825,
            0.000027611582303038693307,
            0.000030650820896268406033,
            0.000034048518007621665892,
            0.000037849354036771202245,
            0.000042103844763707268542,
            0.000046869135037691879494,
            0.000052209904375673540139,
            0.000058199400768800161944,
            0.000064920621440418147642,
            0.000072467662120455729815,
            0.000080947259658891059339,
            0.000090480556564260903820,
            0.00010120512040225430938,
            0.00011327725601781673703,
            0.00012687465436057734794,
            0.00014219942842431573924,
            0.00015948159460388805570,
            0.00017898306679907622800,
            0.00020100224105408447578,
            0.00022587926064687589300,
            0.00025400206560592722881,
            0.00028581334695027405917,
            0.00032181854489190040451,
            0.00036259505223960104126,
            0.00040880280980505176775,
            0.00046119651032469941250,
            0.00052063966196470085011,
            0.00058812080267690935558,
            0.00066477220346540534831,
            0.00075189145310983533799,
            0.00085096638036735325809,
            0.00096370384365466184836,
            0.0010920630044716624009,
            0.0012382938014511201911,
            0.0014049814593474551916,
            0.0015950980043790554864,
            0.0018120619174779710139,
            0.0020598072441312918098,
            0.0023428636982643643925,
            0.0026664495534765270225,
            0.0030365794143149894199,
            0.0034601893107141311253,
            0.0039452819691241228797,
            0.0045010955946692798458,
            0.0051383000622391462889,
            0.0058692250752485134278,
            0.0067081256260282642393,
            0.0076714910016190501393,
            0.0087784046469564793700,
            0.010050963452176172901,
            0.011514766505228020071,
            0.013199485084385792589,
            0.015139527703910756701,
            0.017374816424857881536,
            0.019951693466588558021,
            0.022923980479626831297,
            0.026354216757793137747,
            0.030315107284497343094,
            0.034891216951924426821,
            0.040180953713399625437,
            0.046298891007246516069,
            0.053378488736969283252,
            0.061575282659146798796,
            0.071070624515587005444,
            0.082076070004568643728,
            0.094838529138504679368,
            0.10964631418251547050,
            0.12683624480509410828,
            0.14680199900576803677,
            0.17000393265712942583,
            0.19698063111163560456,
            0.22836250446999551891,
            0.26488779520857075858,
            0.30742143461106570312,
            0.35697726486306362143,
            0.41474423915344316395,
            0.48211732555562037523,
            0.56073397526058991347,
            0.65251717599405361247,
            0.75972630205791724215,
            0.88501719923054875567,
            1.0315132127190954936,
            1.2028891878256478111,
            1.4034708559492863473,
            1.6383524749434685727,
            1.9135361369830261880,
            2.2360968060930531435,
            2.6143779218706418575,
            3.0582233302799902687,
            3.5792524062044467218,
            4.1911865510573160194,
            4.9102368246160009952,
            5.7555643542799098435,
            6.7498274183150652404,
            7.9198317957964763289,
            9.2973042031667320582,
            10.919812501711766825,
            12.831860989424380779,
            15.086194638203007125,
            17.745352787874414659,
            20.883520784566917112,
            24.588737620385198480,
            28.965529116906160754,
            34.138049985732842742,
            40.253834664235696015,
            47.488276729390999702,
            56.049980619880153012,
            66.187158170244839116,
            78.195277075746403575,
            92.426210064344741194,
            109.29918370496803897,
            129.31388618425875732,
            153.06616615979406838,
            181.26684251398308285,
            214.76425059652605223,
            254.57127811578172962,
            301.89779777553055496,
            358.18958957137949873,
            425.17507004741530201,
            504.92141687529988113,
            599.90200468598275853,
            713.07746408542263203,
            847.99315469456997062,
            1008.8964224427490566,
            1200.8777125535006548,
            1430.0404586199650568,
            1703.7056963827754215,
            2030.6585966053098722,
            2421.4456213786663741,
            2888.7328389633221388,
            3447.7381528695740652,
            4116.7528952794304620,
            4917.7715054030486969,
            5877.2519845578071396,
            7027.0346435306700603,
            8405.4525192928336026,
            10058.673963196398282,
            12042.326566894701645,
            14423.462131618517852,
            17282.935211594247602,
            20718.283374621850171,
            24847.216334592477879,
            29811.844270505455663,
            35783.803869390594808,
            42970.475035514146949,
            51622.523164365317859,
            62043.053062569802841,
            74598.723055062221462,
            89733.244070261247834,
            107983.78160952377092,
            130000.89225936567467,
            156572.76541598826523,
            188654.71082955207154,
            227405.04039323126209,
            274228.74683112956725,
            330830.69305240264719,
            399280.40679603648947,
            482091.04158741969538,
            582315.63637280957301,
            703664.50634696553867,
            850648.45577453395323,
            1.0287535561040308971e6,
            1.2446545237718160616e6,
            1.5064753164224924288e6,
            1.8241075110905447490e6,
            2.2095994160296815055e6,
            2.6776318013904075566e6,
            3.2460997385956338335e6,
            3.9368244690860923030e6,
            4.7764246712907428854e6,
            5.7973831960685633406e6,
            7.0393535864963142442e6,
            8.5507608469819083364e6,
            1.0390763422845522696e7,
            1.2631658742629643578e7,
            1.5361833638442874568e7,
            1.8689384331320889701e7,
            2.2746559483379127454e7,
            2.7695215355834636303e7,
            3.3733515954600931078e7,
            4.1104165151913686169e7,
            5.0104524568208262684e7,
            6.1099053486052509103e7,
            7.4534608966994598625e7,
            9.0959270261798984973e7,
            1.1104550725770260396e8,
            1.3561870517654342101e8,
            1.6569229581047881822e8,
            2.0251104015757461495e8,
            2.4760437192545651634e8,
            3.0285216280137906182e8,
            3.7056582949677650864e8,
            4.5358839527054728660e8,
            5.5541797711017130583e8,
            6.8036023401794984992e8,
            8.3371663166456897530e8,
            1.0220170159151657273e9,
            1.2533070193934735218e9,
            1.5375033471427754355e9,
            1.8868331188496672112e9,
            2.3163773345579216099e9,
            2.8447433633468269998e9,
            3.4948973606401708050e9,
            4.2951949870240470240e9,
            5.2806580879860543226e9,
            6.4945565467148011015e9,
            7.9903688985152562391e9,
            9.8342131912271386148e9,
            1.2107861859765090386e10,
            1.4912482139001188821e10,
            1.8373278122258814550e10,
            2.2645253675146057769e10,
            2.7920369151594129933e10,
            3.4436431875264410400e10,
            4.2488143952033463661e10,
            5.2440835305001676653e10,
            6.4747540048971668914e10,
            7.9970236925993600432e10,
            9.8806277619857771284e10,
            1.2212128051551837885e11,
            1.5099008459786791991e11,
            1.8674775464380194929e11,
            2.3105312464669094046e11,
            2.8596798659412659224e11,
            3.5405580774339671684e11,
            4.3850483085637354111e11,
            5.4328162797775322449e11,
            6.7332270142189242010e11,
            8.3477363372145082696e11,
            1.0352876794193904824e12,
            1.2843986889279972686e12,
            1.5939870130389653379e12,
            1.9788617506617101693e12,
            2.4574886167253938936e12,
            3.0529001297488909295e12,
            3.7938341367067754822e12,
            4.7161583940120521298e12,
            5.8646536214595216260e12,
            7.2952459137506759179e12,
            9.0778026053871697844e12,
            1.1299634866090981091e13,
            1.4069887001755042795e13,
            1.7525038599705959020e13,
            2.1835803745239374407e13,
            2.7215784650226367428e13,
            3.3932329087293894603e13,
            4.2320156953775549307e13,
            5.2798467334050969903e13,
            6.5892421461988821965e13,
            8.2260128954804777245e13,
            1.0272655716557653285e14,
            1.2832615237127508265e14,
            1.6035642686249409086e14,
            2.0044535323141044849e14,
            2.5063614839796008443e14,
            3.1349396582827479313e14,
            3.9224019644321385808e14,
            4.9092157203663898589e14,
            6.1462315212461955623e14,
            7.6973666053407696546e14,
            9.6429865412792621089e14,
            1.2084168206775894912e15,
            1.5148075287917910453e15,
            1.8994738597912321732e15,
            2.3825610817047206712e15,
            2.9894363133592762718e15,
            3.7520515229055051480e15,
            4.7106647096132020176e15,
            5.9160140186885536621e15,
            7.4320647644738600601e15,
            9.3394813211487063565e15,
            1.1740016403634905835e16,
            1.4762061720474810406e16,
            1.8567669279381012692e16,
            2.3361435509739244516e16,
            2.9401745598651820479e16,
            3.7015009080511300089e16,
            4.6613687491135078296e16,
            5.8719130620827617479e16,
            7.3990512088361825816e16,
            9.3261503548377772236e16,
            1.1758677014703896398e17,
            1.4830093373941708059e17,
            1.8709336787625027593e17,
            2.3610310173811820109e17,
            2.9803927271701911381e17,
            3.7633404770132131560e17,
            4.7533681870727697071e17,
            6.0056088088372269226e17,
            7.5899686254901698900e17,
            9.5951107973980956574e17,
            1.2133519641905480978e18,
            1.5347940607316986383e18,
            1.9419571874259285052e18,
            2.4578486847167510044e18,
            3.1116898691395537168e18,
            3.9406046467097262695e18,
            4.9917697468290074402e18,
            6.3251535095644411601e18,
            8.0170052617933809697e18,
            1.0164302182096293767e19,
            1.2890417909932024265e19,
            1.6352350498360202844e19,
            2.0749941122319681657e19,
            2.6337634979706622390e19,
            3.3439489423820175074e19,
            4.2468330986256454679e19,
            5.3950214701925132289e19,
            6.8555661573929135667e19,
            8.7139563060882901379e19,
            1.1079217073635309485e20,
            1.4090426763454368972e20,
            1.7925048748038193155e20,
            2.2809586324637923801e20,
            2.9033211698015448182e20,
            3.6965203818563528334e20,
            4.7077265338193267403e20,
            5.9972091289806531324e20,
            7.6419950296874359307e20,
            9.7405537683727933240e20,
            1.2418800032060465957e21,
            1.5837785599265540585e21,
            2.0203558779135588621e21,
            2.5779805379382160514e21,
            3.2904060081565308736e21,
            4.2008501999710496594e21,
            5.3646637536464652972e21,
            6.8527545986539215896e21,
            8.7559842596221412334e21,
            1.1190813082737995611e22,
            1.4306551020005469088e22,
            1.8294672966037293766e22,
            2.3400789522722757199e22,
            2.9940034028320865278e22,
            3.8316845778494000674e22,
            4.9050411858018697984e22,
            6.2807394328674227143e22,
            8.0444039502345866882e22,
            1.0306037245670218429e23,
            1.3206996262567093599e23,
            1.6928975668497108976e23,
            2.1705577962006470125e23,
            2.7837218992541622010e23,
            3.5710335177231868688e23,
            4.5822139992419707923e23,
            5.8812540888862475966e23,
            7.5505297805255241870e23,
            9.6961112250407325454e23,
            1.2454612205923578114e24,
            1.6002029397172264384e24,
            2.0565152227377615964e24,
            2.6436294520787716574e24,
            3.3992319625267342103e24,
            4.3719216324865296600e24,
            5.6243852744659917460e24,
            7.2375014697258327544e24,
            9.3156455954100987670e24,
            1.1993549289101528021e25,
            1.5445171957616407993e25,
            1.9895177271041990467e25,
            2.5633783122387822324e25,
            3.3035981274999587080e25,
            4.2586418456893366030e25,
            5.4911614264054420502e25,
            7.0821689261311532977e25,
            9.1364423400336475220e25,
            1.1789530490486082155e26,
            1.5216832112975373126e26,
            1.9645366110301002109e26,
            2.5369034281724903840e26,
            3.2768417485472207235e26,
            4.2336457831451323812e26,
            5.4711784842349455231e26,
            7.0721970860103689415e26,
            9.1439687185718017229e26,
            1.1825562559030665934e27,
            1.5297321259423082963e27,
            1.9793165800760820536e27,
            2.5616585126525692972e27,
            3.3161418815930392079e27,
            4.2938875817801601087e27,
            5.5612668571114812034e27,
            7.2044710617980071568e27,
            9.3354567458770052455e27,
            1.2099681766091596451e28,
            1.5686174303097951300e28,
            2.0340641304952919707e28,
            2.6382537745160023288e28,
            3.4227298568011625405e28,
            4.4415301413711584477e28,
            5.7649606483516486532e28
        };

        static readonly double[] expected_x128 = {
            2.8466809676120979858e-57,
            2.8468540474489933388e-57,
            2.8473733499404199690e-57,
            2.8482390640666997507e-57,
            2.8494515049220685285e-57,
            2.8510111139053378910e-57,
            2.8529184589870471617e-57,
            2.8551742350533314118e-57,
            2.8577792643267962284e-57,
            2.8607344968647552269e-57,
            2.8640410111352519272e-57,
            2.8677000146713537098e-57,
            2.8717128448042721889e-57,
            2.8760809694759315660e-57,
            2.8808059881316744279e-57,
            2.8858896326938631042e-57,
            2.8913337686172041769e-57,
            2.8971403960266941142e-57,
            2.9033116509391553582e-57,
            2.9098498065694046155e-57,
            2.9167572747221686579e-57,
            2.9240366072709377159e-57,
            2.9316904977250226344e-57,
            2.9397217828861594324e-57,
            2.9481334445960838590e-57,
            2.9569286115765790549e-57,
            2.9661105613635816078e-57,
            2.9756827223370152092e-57,
            2.9856486758481069021e-57,
            2.9960121584460286208e-57,
            3.0067770642057964942e-57,
            3.0179474471594522974e-57,
            3.0295275238326456078e-57,
            3.0415216758888317634e-57,
            3.0539344528833997367e-57,
            3.0667705751301456519e-57,
            3.0800349366826120041e-57,
            3.0937326084329198057e-57,
            3.1078688413308310227e-57,
            3.1224490697258918958e-57,
            3.1374789148356242094e-57,
            3.1529641883428514147e-57,
            3.1689108961253698716e-57,
            3.1853252421213025097e-57,
            3.2022136323336030586e-57,
            3.2195826789773138411e-57,
            3.2374392047733191092e-57,
            3.2557902473924792192e-57,
            3.2746430640541787492e-57,
            3.2940051362834741619e-57,
            3.3138841748311839840e-57,
            3.3342881247614269174e-57,
            3.3552251707112810170e-57,
            3.3767037423274102801e-57,
            3.3987325198846839114e-57,
            3.4213204400919983866e-57,
            3.4444767020907034672e-57,
            3.4682107736512307778e-57,
            3.4925323975737276825e-57,
            3.5174515982987102690e-57,
            3.5429786887339675369e-57,
            3.5691242773041746709e-57,
            3.5958992752299068655e-57,
            3.6233149040429868584e-57,
            3.6513827033453494409e-57,
            3.6801145388188650883e-57,
            3.7095226104938328230e-57,
            3.7396194612841298586e-57,
            3.7704179857972928377e-57,
            3.8019314394281029654e-57,
            3.8341734477445554459e-57,
            3.8671580161754127852e-57,
            3.9008995400088721473e-57,
            3.9354128147122195098e-57,
            3.9707130465826983198e-57,
            4.0068158637401881997e-57,
            4.0437373274726704919e-57,
            4.0814939439458526096e-57,
            4.1201026762887328054e-57,
            4.1595809570673116782e-57,
            4.1999467011590970866e-57,
            4.2412183190415057606e-57,
            4.2834147305077384396e-57,
            4.3265553788241964894e-57,
            4.3706602453440173639e-57,
            4.4157498645918347056e-57,
            4.4618453398354170842e-57,
            4.5089683591604081418e-57,
            4.5571412120649810671e-57,
            4.6063868065918327258e-57,
            4.6567286870155783043e-57,
            4.7081910521042669275e-57,
            4.7607987739744233407e-57,
            4.8145774175597314181e-57,
            4.8695532607142130187e-57,
            4.9257533149715216554e-57,
            4.9832053469827657075e-57,
            5.0419379006561016836e-57,
            5.1019803200221955634e-57,
            5.1633627728505408066e-57,
            5.2261162750425465579e-57,
            5.2902727158282702952e-57,
            5.3558648837946671306e-57,
            5.4229264937742646989e-57,
            5.4914922146242496372e-57,
            5.5615976979270707401e-57,
            5.6332796076448266739e-57,
            5.7065756507609144663e-57,
            5.7815246089436707153e-57,
            5.8581663712680425506e-57,
            5.9365419680326818612e-57,
            6.0166936057112663116e-57,
            6.0986647030783164150e-57,
            6.1824999285513017377e-57,
            6.2682452387924135857e-57,
            6.3559479186150287907e-57,
            6.4456566222416020994e-57,
            6.5374214159615059163e-57,
            6.6312938222391886242e-57,
            6.7273268653249493876e-57,
            6.8255751184226313530e-57,
            6.9260947524706197564e-57,
            7.0289435865947000113e-57,
            7.1341811402935869485e-57,
            7.2418686874202836909e-57,
            7.3520693120248710510e-57,
            7.4648479661268698644e-57,
            7.5802715294879635326e-57,
            7.6984088714586206384e-57,
            7.8193309149750224223e-57,
            7.9431107027856819478e-57,
            8.0698234659902459678e-57,
            8.1995466949762020558e-57,
            8.3323602128425779502e-57,
            8.4683462514032230049e-57,
            8.6075895298659090919e-57,
            8.7501773362872865126e-57,
            8.8961996119076859527e-57,
            9.0457490384738770683e-57,
            9.1989211286621850380e-57,
            9.3558143197188357835e-57,
            9.5165300704390563388e-57,
            9.6811729616113071352e-57,
            9.8498508000580762814e-57,
            1.0022674726409931118e-56,
            1.0199759326755008703e-56,
            1.0381222748311844126e-56,
            1.0567186819279393859e-56,
            1.0757777173024321215e-56,
            1.0953123376772083700e-56,
            1.1153359064975108968e-56,
            1.1358622077538379616e-56,
            1.1569054603090079640e-56,
            1.1784803327492600527e-56,
            1.2006019587797176370e-56,
            1.2232859531853729759e-56,
            1.2465484283796178635e-56,
            1.2704060115632494759e-56,
            1.2948758625178233591e-56,
            1.3199756920582090582e-56,
            1.3457237811702298511e-56,
            1.3721390008603383469e-56,
            1.3992408327453963581e-56,
            1.4270493904117925329e-56,
            1.4555854415743469393e-56,
            1.4848704310667204163e-56,
            1.5149265046963704490e-56,
            1.5457765339984771027e-56,
            1.5774441419247047924e-56,
            1.6099537295041711344e-56,
            1.6433305035155657034e-56,
            1.6776005052110022393e-56,
            1.7127906401339008675e-56,
            1.7489287090749855476e-56,
            1.7860434402123497156e-56,
            1.8241645224834935820e-56,
            1.8633226402392736112e-56,
            1.9035495092318323347e-56,
            1.9448779139907990331e-56,
            1.9873417466443733589e-56,
            2.0309760472443292620e-56,
            2.0758170456565104548e-56,
            2.1219022050810361727e-56,
            2.1692702672692024481e-56,
            2.2179612995069550963e-56,
            2.2680167434378319220e-56,
            2.3194794658014294298e-56,
            2.3723938111667499566e-56,
            2.4268056567432353736e-56,
            2.4827624693559003763e-56,
            2.5403133646747492926e-56,
            2.5995091687926030507e-56,
            2.6604024822495855989e-56,
            2.7230477466068301971e-56,
            2.7875013136764745682e-56,
            2.8538215175197293001e-56,
            2.9220687493297360007e-56,
            2.9923055353210908672e-56,
            3.0645966177533064180e-56,
            3.1390090392211305393e-56,
            3.2156122303505496959e-56,
            3.2944781010454847111e-56,
            3.3756811354366561216e-56,
            3.4592984906908656080e-56,
            3.5454100998460249364e-56,
            3.6340987788446794796e-56,
            3.7254503379465357590e-56,
            3.8195536977086283970e-56,
            3.9165010097302690738e-56,
            4.0163877823688271247e-56,
            4.1193130116417178036e-56,
            4.2253793175397404689e-56,
            4.3346930859871365613e-56,
            4.4473646166944488620e-56,
            4.5635082771614829148e-56,
            4.6832426630994236607e-56,
            4.8066907655534715071e-56,
            4.9339801450202598460e-56,
            5.0652431128678294395e-56,
            5.2006169203800945903e-56,
            5.3402439557625736689e-56,
            5.4842719494617060569e-56,
            5.6328541881663743210e-56,
            5.7861497378773316921e-56,
            5.9443236764481398659e-56,
            6.1075473360199919423e-56,
            6.2759985557924733072e-56,
            6.4498619455929449910e-56,
            6.6293291607288674347e-56,
            6.8145991886300680779e-56,
            7.0058786478117467805e-56,
            7.2033820997139646126e-56,
            7.4073323739995326984e-56,
            7.6179609079196703675e-56,
            7.8355081003856008603e-56,
            8.0602236814144666859e-56,
            8.2923670976496474484e-56,
            8.5322079146888263495e-56,
            8.7800262369880574450e-56,
            9.0361131461467180700e-56,
            9.3007711584166781199e-56,
            9.5743147023193731837e-56,
            9.8570706172968299504e-56,
            1.0149378674367163099e-55,
            1.0451592119801751805e-55,
            1.0764078242890325587e-55,
            1.1087218968911664108e-55,
            1.1421411478481670899e-55,
            1.1767068854507350567e-55,
            1.2124620758034844073e-55,
            1.2494514134342305642e-55,
            1.2877213950694194485e-55,
            1.3273203967242669793e-55,
            1.3682987542634392304e-55,
            1.4107088475957333560e-55,
            1.4546051886742367898e-55,
            1.5000445134818676051e-55,
            1.5470858781910537587e-55,
            1.5957907596956158877e-55,
            1.6462231607227012348e-55,
            1.6984497197429003344e-55,
            1.7525398259074898347e-55,
            1.8085657392531122554e-55,
            1.8666027164261561161e-55,
            1.9267291421916688593e-55,
            1.9890266670048531885e-55,
            2.0535803509370995076e-55,
            2.1204788142631296434e-55,
            2.1898143950312085554e-55,
            2.2616833139545619782e-55,
            2.3361858469791618929e-55,
            2.4134265059009537010e-55,
            2.4935142274244468606e-55,
            2.5765625710744250379e-55,
            2.6626899263934058662e-55,
            2.7520197298794504825e-55,
            2.8446806921420485626e-55,
            2.9408070357781483741e-55,
            3.0405387444960296599e-55,
            3.1440218240416999696e-55,
            3.2514085755109062540e-55,
            3.3628578816597712263e-55,
            3.4785355068585706157e-55,
            3.5986144113663501174e-55,
            3.7232750806390316087e-55,
            3.8527058704204742822e-55,
            3.9871033684047404973e-55,
            4.1266727732986769266e-55,
            4.2716282921569737183e-55,
            4.4221935569072291679e-55,
            4.5786020610303529631e-55,
            4.7410976174120229106e-55,
            4.9099348384340114081e-55,
            5.0853796394301701913e-55,
            5.2677097666908651720e-55,
            5.4572153512618567624e-55,
            5.6541994898492039705e-55,
            5.8589788542109220652e-55,
            6.0718843304890439737e-55,
            6.2932616900126365764e-55,
            6.5234722931834287398e-55,
            6.7628938281412552705e-55,
            7.0119210859967607328e-55,
            7.2709667745140045534e-55,
            7.5404623722260447637e-55,
            7.8208590250725491940e-55,
            8.1126284877603043384e-55,
            8.4162641121654962238e-55,
            8.7322818852211766853e-55,
            9.0612215188647753405e-55,
            9.4036475947592670105e-55,
            9.7601507666480743009e-55,
            1.0131349023358418049e-54,
            1.0517889015631092972e-54,
            1.0920447450127038363e-54,
            1.1339732554143119664e-54,
            1.1776485614761792840e-54,
            1.2231482596362379451e-54,
            1.2705535840636160681e-54,
            1.3199495853474065310e-54,
            1.3714253183335080762e-54,
            1.4250740395956400952e-54,
            1.4809934150533527085e-54,
            1.5392857382780892111e-54,
            1.6000581600581970704e-54,
            1.6634229298253201680e-54,
            1.7294976495779382433e-54,
            1.7984055409730548586e-54,
            1.8702757262942843525e-54,
            1.9452435240439695912e-54,
            2.0234507599486014382e-54,
            2.1050460942108409023e-54,
            2.1901853658880071175e-54,
            2.2790319553261384423e-54,
            2.3717571656308189339e-54,
            2.4685406242110568021e-54,
            2.5695707054907839896e-54,
            2.6750449759442064927e-54,
            2.7851706626764747292e-54,
            2.9001651468401758100e-54,
            3.0202564832512016856e-54,
            3.1456839476448594284e-54,
            3.2766986130949177823e-54,
            3.4135639572048986647e-54,
            3.5565565017726113457e-54,
            3.7059664867259961304e-54,
            3.8620985802311179586e-54,
            4.0252726269819729327e-54,
            4.1958244367970082369e-54,
            4.3741066157692967726e-54,
            4.5604894423465647963e-54,
            4.7553617908541822747e-54,
            4.9591321051192572375e-54,
            5.1722294250076218540e-54,
            5.3951044688482849121e-54,
            5.6282307748924113718e-54,
            5.8721059051366682217e-54,
            6.1272527150344777362e-54,
            6.3942206928240148110e-54,
            6.6735873724193869037e-54,
            6.9659598240421006964e-54,
            7.2719762270144541665e-54,
            7.5923075293957524486e-54,
            7.9276591994171409737e-54,
            8.2787730739623478598e-54,
            8.6464293096507587878e-54,
            9.0314484424071064024e-54,
            9.4346935617498070763e-54,
            9.8570726063988592896e-54,
            1.0299540788195547546e-53,
            1.0763103151741375511e-53,
            1.1248817277604173429e-53,
            1.1757796137406774903e-53,
            1.2291211109609725700e-53,
            1.2850295165325969778e-53,
            1.3436346234064268148e-53,
            1.4050730759891280811e-53,
            1.4694887459131948394e-53,
            1.5370331291396355845e-53,
            1.6078657656431102580e-53,
            1.6821546830046957750e-53,
            1.7600768653175031612e-53,
            1.8418187488953810889e-53,
            1.9275767463652347224e-53,
            2.0175578008194018635e-53,
            2.1119799718064189234e-53,
            2.2110730550467591349e-53,
            2.3150792378751415378e-53,
            2.4242537925332251359e-53,
            2.5388658095663801959e-53,
            2.6591989737162602121e-53,
            2.7855523848476082575e-53,
            2.9182414266036794787e-53,
            3.0575986856504434439e-53,
            3.2039749245459813385e-53,
            3.3577401114588910688e-53,
            3.5192845101587804483e-53,
            3.6890198339138349897e-53,
            3.8673804671558136723e-53,
            4.0548247590125293096e-53,
            4.2518363930628439644e-53,
            4.4589258379404507075e-53,
            4.6766318837012838368e-53,
            4.9055232691764344522e-53,
            5.1462004058591568889e-53,
            5.3992972042222249047e-53,
            5.6654830087319122465e-53,
            5.9454646482187004343e-53,
            6.2399886086840263259e-53,
            6.5498433360686480019e-53,
            6.8758616769833174761e-53,
            7.2189234659083111551e-53,
            7.5799582679070214309e-53,
            7.9599482864724308616e-53,
            8.3599314467361972115e-53,
            8.7810046649207540690e-53,
            9.2243273156079274429e-53,
            9.6911249091359136621e-53,
            1.0182692992223081034e-52,
            1.0700401285755176415e-52,
            1.1245698074565588197e-52,
            1.1820114864990025580e-52,
            1.2425271326991259210e-52,
            1.3062880538730639768e-52,
            1.3734754552615467373e-52,
            1.4442810303079733934e-52,
            1.5189075877665442227e-52,
            1.5975697174368113729e-52,
            1.6804944969699105498e-52,
            1.7679222423505244864e-52,
            1.8601073048279774139e-52,
            1.9573189172504856846e-52,
            2.0598420929492611361e-52,
            2.1679785805247020931e-52,
            2.2820478781061906012e-52,
            2.4023883108909833763e-52,
            2.5293581760173434199e-52,
            2.6633369590934849890e-52,
            2.8047266269882475772e-52,
            2.9539530017929064183e-52,
            3.1114672211874858209e-52,
            3.2777472907907780932e-52,
            3.4532997344424948495e-52,
            3.6386613487602054523e-52,
            3.8344010687346795559e-52,
            4.0411219515767997602e-52,
            4.2594632865093298335e-52,
            4.4901028387096378674e-52,
            4.7337592361572567635e-52,
            4.9911945087253527554e-52,
            5.2632167894803756080e-52,
            5.5506831888221767657e-52,
            5.8545028528106983473e-52,
            6.1756402177881644681e-52,
            6.5151184742209841119e-52,
            6.8740232535569847908e-52,
            7.2535065528250854890e-52,
            7.6547909127003159588e-52,
            8.0791738658217312238e-52,
            8.5280326732891150643e-52,
            9.0028293684816241905e-52,
            9.5051161286432812135e-52,
            1.0036540996072468089e-51,
            1.0598853972241723961e-51,
            1.1193913509767098527e-51,
            1.1823693428850439461e-51,
            1.2490290286641221117e-51,
            1.3195931229915348278e-51,
            1.3942982363555907581e-51,
            1.4733957669554868842e-51,
            1.5571528513645750239e-51,
            1.6458533779236512513e-51,
            1.7397990671051518529e-51,
            1.8393106233824250235e-51,
            1.9447289634522530131e-51,
            2.0564165259950184685e-51,
            2.1747586685169445964e-51,
            2.3001651572044127868e-51,
            2.4330717561333215379e-51,
            2.5739419226187812620e-51,
            2.7232686159642717171e-51,
            2.8815762273770108135e-51,
            3.0494226393601532214e-51,
            3.2274014234751945399e-51,
            3.4161441859924376875e-51,
            3.6163230716166292656e-51,
            3.8286534361921681784e-51,
            4.0538967000611438773e-51,
            4.2928633945716565877e-51,
            4.5464164151174683854e-51,
            4.8154744950373972567e-51,
            5.1010159157186883177e-51,
            5.4040824693379208479e-51,
            5.7257836918412588684e-51,
            6.0673013850188561760e-51,
            6.4298944478722520985e-51,
            6.8149040389153819023e-51,
            7.2237590925966241737e-51,
            7.6579822146889123148e-51,
            8.1191959832757335320e-51,
            8.6091296838718460023e-51,
            9.1296265092684664182e-51,
            9.6826512568939551919e-51,
            1.0270298558843887605e-50,
            1.0894801682270932806e-50,
            1.1558541940548175835e-50,
            1.2264058758543409625e-50,
            1.3014060438481561428e-50,
            1.3811435676244020459e-50,
            1.4659265881574660578e-50,
            1.5560838359551601306e-50,
            1.6519660414861461992e-50,
            1.7539474444900831383e-50
        };
    }
}
