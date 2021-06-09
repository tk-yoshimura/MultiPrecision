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
        public void EllipticPiNp0p75Test() {
            double[] expecteds = {
                3.1415926535897932385,
                3.1418483560726018612,
                3.1426159320758088177,
                3.1438967897945466495,
                3.1456932846842749928,
                3.1480087322532735523,
                3.1508474261682432953,
                3.1542146618734756057,
                3.1581167659876726345,
                3.1625611318111328568,
                3.1675562613510513202,
                3.1731118143557101178,
                3.1792386649411772652,
                3.1859489664989194697,
                3.1932562256919716716,
                3.2011753864839688504,
                3.2097229253029926726,
                3.2189169586251031414,
                3.2287773644758053714,
                3.2393259195978678428,
                3.2505864543286041722,
                3.2625850275784849382,
                3.2753501247175462612,
                3.2889128816711607482,
                3.3033073391206442670,
                3.3185707314198897725,
                3.3347438157057980757,
                3.3518712477346150029,
                3.3700020122655308183,
                3.3891899173976591274,
                3.4094941642252061665,
                3.4309800056104157847,
                3.4537195109187421402,
                3.4777924573928000931,
                3.5032873736969831863,
                3.5303027673603218389,
                3.5589485758110457425,
                3.5893478910206903226,
                3.6216390212716696193,
                3.6559779713663769937,
                3.6925414463143309533,
                3.7315305154653584796,
                3.7731751175349224917,
                3.8177396468966465920,
                3.8655299452241844825,
                3.9169021411830680170,
                3.9722739516316432942,
                4.0321393078890053824,
                4.0970875439657078367,
                4.1678289528199471623,
                4.2452294050111630343,
                4.3303581474589396883,
                4.4245552493491150464,
                4.5295291733497540499,
                4.6475020701793147647,
                4.7814336173499506888,
                4.9353801252930626431,
                5.1150997270183485183,
                5.3291366385467808713,
                5.5909219970929636704,
                5.9232920958049332938,
                6.3697471607338164723,
                7.0296869407148159625,
                8.2268676774526782488
            };

            const double n = 0.75;

            for (int i = 0; i < 64; i++) {
                decimal k = i / 64m;

                double expected = expecteds[i];

                MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticPi(n, k);
                MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticPi(n, k);
                MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticPi(n, k);
                MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticPi(n, k);

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
        public void EllipticPiNp0p50Test() {
            double[] expecteds = {
                2.2214414690791831235,
                2.2216003410570760156,
                2.2220772339786470905,
                2.2228729802610421715,
                2.2239889721266155124,
                2.2254271689483003520,
                2.2271901076435573860,
                2.2292809162297448349,
                2.2317033306895185006,
                2.2344617153334123285,
                2.2375610868888429298,
                2.2410071425913019951,
                2.2448062926054568603,
                2.2489656971624453505,
                2.2534933088662065763,
                2.2583979206978715189,
                2.2636892203350019960,
                2.2693778515041665333,
                2.2754754832038232354,
                2.2819948877732059697,
                2.2889500289461163053,
                2.2963561612214158322,
                2.3042299421110235875,
                2.3125895590993377907,
                2.3214548734751877389,
                2.3308475835911868131,
                2.3407914105814777092,
                2.3513123101473506378,
                2.3624387147266141685,
                2.3742018112296975059,
                2.3866358605956340455,
                2.3997785667494415014,
                2.4136715042011946407,
                2.4283606156116170379,
                2.4438967932861427808,
                2.4603365619193069145,
                2.4777428842243981443,
                2.4961861166643762825,
                2.5157451497836923984,
                2.5365087772331889925,
                2.5585773503374668149,
                2.5820647921987568626,
                2.6071010686339033932,
                2.6338352453019253975,
                2.6624393050775576335,
                2.6931129629446943902,
                2.7260898065117209107,
                2.7616452230124855111,
                2.8001067714281046382,
                2.8418679592329887411,
                2.8874068518111981029,
                2.9373116916416496782,
                2.9923169378489206516,
                3.0533552376635152779,
                3.1216345613215174448,
                3.1987566227510081874,
                3.2869061707936835409,
                3.3891687711925063916,
                3.5100978334772656781,
                3.6568085169187737344,
                3.8413194446057135034,
                4.0863584014324193546,
                4.4434293507572777644,
                5.0786088799425931837
            };

            const double n = 0.50;

            for (int i = 0; i < 64; i++) {
                decimal k = i / 64m;

                double expected = expecteds[i];

                MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticPi(n, k);
                MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticPi(n, k);
                MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticPi(n, k);
                MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticPi(n, k);

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
        public void EllipticPiNm8Test() {
            double[] expecteds = {
                0.52359877559829887308,
                0.52361475639377532997,
                0.52366272074128880844,
                0.52374273462815274607,
                0.52385490838114581024,
                0.52399939719209866599,
                0.52417640186118694473,
                0.52438616976555954257,
                0.52462899606334110795,
                0.52490522514563837006,
                0.52521525235200198986,
                0.52555952596790562569,
                0.52593854952626698859,
                0.52635288443892738952,
                0.52680315298841529070,
                0.52729004171535052445,
                0.52781430524262411711,
                0.52837677058416400379,
                0.52897834199385032548,
                0.52962000641919511826,
                0.53030283963501802994,
                0.53102801314486087814,
                0.53179680195269445973,
                0.53261059332508282122,
                0.53347089668500800162,
                0.53437935480380414133,
                0.53533775648808807655,
                0.53634805099544981306,
                0.53741236445756644652,
                0.53853301864435841984,
                0.53971255247043596415,
                0.54095374672878443102,
                0.54225965264085163513,
                0.54363362494277720172,
                0.54507936039220636429,
                0.54660094278934898288,
                0.54820289587370921307,
                0.54989024580334764637,
                0.55166859537297942914,
                0.55354421271730801107,
                0.55552413802833870482,
                0.55761631286354090184,
                0.55982973804182544789,
                0.56217466807193891665,
                0.56466285276448362514,
                0.56730784049447278680,
                0.57012536304555373922,
                0.57313382992762836141,
                0.57635497187872458145,
                0.57981469118170348280,
                0.58354420423768448898,
                0.58758160614621523694,
                0.59197405975158949141,
                0.59678093501819213678,
                0.60207844231460657556,
                0.60796670501020061368,
                0.61458099891081150171,
                0.62211050868074325640,
                0.63083159131603928834,
                0.64117154563336814677,
                0.65384423879725213546,
                0.67018408727482935680,
                0.69317792535594728369,
                0.73232569786838977337
            };

            const double n = -8;

            for (int i = 0; i < 64; i++) {
                decimal k = i / 64m;

                double expected = expecteds[i];

                MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticPi(n, k);
                MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticPi(n, k);
                MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticPi(n, k);
                MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticPi(n, k);

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
        public void EllipticPiNm16Test() {
            double[] expecteds = {
                0.38097406892397619970,
                0.38098314754086177820,
                0.38101039532045358360,
                0.38105584810542028882,
                0.38111956582031698510,
                0.38120163275372443937,
                0.38130215795723625446,
                0.38142127576537016045,
                0.38155914644176689072,
                0.38171595695842382836,
                0.38189192191621758443,
                0.38208728461663012041,
                0.38230231829644162900,
                0.38253732753923039144,
                0.38279264987987244046,
                0.38306865762091791181,
                0.38336575988280300376,
                0.38368440491341630798,
                0.38402508268667225715,
                0.38438832782456945401,
                0.38477472288287031056,
                0.38518490204720559458,
                0.38561955529429864669,
                0.38607943308238611057,
                0.38656535164611732401,
                0.38707819898465896277,
                0.38761894164793849050,
                0.38818863244559176712,
                0.38878841922707987807,
                0.38941955491068760549,
                0.39008340897510269613,
                0.39078148067180717558,
                0.39151541427194962716,
                0.39228701673081825824,
                0.39309827824062298463,
                0.39395139625354005693,
                0.39484880369932717617,
                0.39579320230543788792,
                0.39678760216643525605,
                0.39783536902308613957,
                0.39894028112720089648,
                0.40010659812510402977,
                0.40133914514694661335,
                0.40264341632346655677,
                0.40402570338908765656,
                0.40549325705620916156,
                0.40705449174638346894,
                0.40871924848961337235,
                0.41049913707580003354,
                0.41240798805166135253,
                0.41446245991277721587,
                0.41668287034659028686,
                0.41909435895045648028,
                0.42172855429869097232,
                0.42462603368819289781,
                0.42784007695174618246,
                0.43144263037859565014,
                0.43553425691628467050,
                0.44026177805293553049,
                0.44585208702517352752,
                0.45268404643879583524,
                0.46146549411780511216,
                0.47377998736883166354,
                0.49466160925454855009
            };

            const double n = -16;

            for (int i = 0; i < 64; i++) {
                decimal k = i / 64m;

                double expected = expecteds[i];

                MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticPi(n, k);
                MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticPi(n, k);
                MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticPi(n, k);
                MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticPi(n, k);

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
        public void EllipticPikp50Test() {
            double[] expecteds = {
                0.39151541427194962716,
                0.39225307898379602412,
                0.39299488668739780067,
                0.39374087616413515348,
                0.39449108670451362898,
                0.39524555811677923107,
                0.39600433073571222311,
                0.39676744543160401816,
                0.39753494361942167695,
                0.39830686726816466182,
                0.39908325891041862864,
                0.39986416165211117591,
                0.40064961918247461291,
                0.40143967578422095536,
                0.40223437634393450876,
                0.40303376636268755661,
                0.40383789196688483225,
                0.40464679991934262079,
                0.40546053763060851046,
                0.40627915317052799134,
                0.40710269528006428456,
                0.40793121338337797601,
                0.40876475760017322611,
                0.40960337875831753185,
                0.41044712840674222853,
                0.41129605882863113712,
                0.41215022305490498991,
                0.41300967487800950069,
                0.41387446886601518832,
                0.41474466037703731318,
                0.41562030557398454567,
                0.41650146143964525444,
                0.41738818579212058076,
                0.41828053730061375323,
                0.41917857550158539569,
                0.42008236081528489050,
                0.42099195456266817976,
                0.42190741898271271877,
                0.42282881725014064052,
                0.42375621349356154631,
                0.42468967281404670753,
                0.42562926130414684684,
                0.42657504606736606480,
                0.42752709523810488997,
                0.42848547800208585850,
                0.42945026461727547299,
                0.43042152643531685069,
                0.43139933592348784916,
                0.43238376668719995364,
                0.43337489349305372514,
                0.43437279229246714336,
                0.43537754024589373355,
                0.43638921574764794344,
                0.43740789845135583574,
                0.43843366929604978407,
                0.43946661053292650755,
                0.44050680575278845168,
                0.44155433991418922248,
                0.44260929937230450786,
                0.44367177190855067617,
                0.44474184676097402852,
                0.44581961465543449936,
                0.44690516783760845150,
                0.44799860010583609737,
                0.44910000684484000092,
                0.45020948506034207441,
                0.45132713341460748459,
                0.45245305226294492378,
                0.45358734369119378641,
                0.45473011155422992181,
                0.45588146151552281204,
                0.45704150108777825119,
                0.45821033967470188299,
                0.45938808861392028803,
                0.46057486122109770426,
                0.46177077283528791689,
                0.46297594086556236901,
                0.46419048483895712601,
                0.46541452644978297759,
                0.46664818961034468524,
                0.46789160050311718294,
                0.46914488763442841921,
                0.47040818188970049355,
                0.47168161659030279297,
                0.47296532755207298061,
                0.47425945314556393201,
                0.47556413435807706096,
                0.47687951485754493110,
                0.47820574105832861724,
                0.47954296218899796770,
                0.48089133036216573151,
                0.48225100064644945977,
                0.48362213114063817459,
                0.48500488305014403010,
                0.48639942076582257540,
                0.48780591194524877756,
                0.48922452759653968269,
                0.49065544216481849335,
                0.49209883362141893254,
                0.49355488355593305658,
                0.49502377727121018479,
                0.49650570388141934249,
                0.49800085641329258031,
                0.49950943191067174837,
                0.50103163154248678417,
                0.50256766071429933247,
                0.50411772918355156966,
                0.50568205117866647248,
                0.50726084552215246747,
                0.50885433575787244453,
                0.51046275028264453488,
                0.51208632248234986200,
                0.51372529087273069873,
                0.51537989924507112718,
                0.51705039681696142915,
                0.51873703838835705981,
                0.52044008450315320806,
                0.52215980161650665409,
                0.52389646226814793402,
                0.52565034526193874846,
                0.52742173585194214686,
                0.52921092593528632390,
                0.53101821425211692305,
                0.53284390659294760421,
                0.53468831601373434818,
                0.53655176305901559602,
                0.53843457599347791547,
                0.54033709104232551285,
                0.54225965264085163513,
                0.54420261369363080708,
                0.54616633584377300016,
                0.54815118975270431795,
                0.55015755539096369842,
                0.55218582234053157307,
                0.55423639010923449276,
                0.55630966845779954205,
                0.55840607774016404068,
                0.56052604925767970322,
                0.56267002562788623745,
                0.56483846116856746138,
                0.56703182229784357349,
                0.56925058795109639733,
                0.57149525001557043447,
                0.57376631378354160508,
                0.57606429842499786013,
                0.57838973748083165542,
                0.58074317937760385111,
                0.58312518796500222587,
                0.58553634307718578420,
                0.58797724111927872467,
                0.59044849568035569256,
                0.59295073817434316048,
                0.59548461851035089674,
                0.59805080579404296588,
                0.60064998906176006946,
                0.60328287804921483854,
                0.60595020399669953998,
                0.60865272049287222239,
                0.61139120435932332978,
                0.61416645657827104561,
                0.61697930326589096394,
                0.61983059669395506894,
                0.62272121636263747800,
                0.62565207012754110922,
                0.62862409538421162136,
                0.63163826031363401888,
                0.63469556519245472555,
                0.63779704377193936973,
                0.64094376472996582054,
                0.64413683320066518293,
                0.64737739238666272300,
                0.65066662525923850669,
                0.65400575635212660514,
                0.65739605365510504857,
                0.66083883061399961411,
                0.66433544824423669223,
                0.66788731736563796958,
                0.67149590096675702510,
                0.67516271670772019679,
                0.67888933957125684357,
                0.68267740467239364325,
                0.68652861023815079786,
                0.69044472076952273483,
                0.69442757039906078974,
                0.69847906645851015894,
                0.70260119327219901891,
                0.70679601619324635229,
                0.71106568590116043605,
                0.71541244298105758788,
                0.71983862280655803802,
                0.72434666075043232641,
                0.72893909774929959168,
                0.73361858625114359763,
                0.73838789657714174111,
                0.74324992373232882859,
                0.74820769470297770912,
                0.75326437628231457300,
                0.75842328347034535482,
                0.76368788849820542975,
                0.76906183053261966124,
                0.77454892612184486982,
                0.78015318045094345387,
                0.78587879948149891441,
                0.79173020305903936064,
                0.79771203908060827881,
                0.80382919882525699475,
                0.81008683356189728132,
                0.81649037256213929476,
                0.82304554266067503682,
                0.82975838952271390907,
                0.83663530079724217667,
                0.84368303135682261894,
                0.85090873084969754269,
                0.85831997381860602196,
                0.86592479267356303705,
                0.87373171384357052461,
                0.88174979747566483527,
                0.88998868109983569125,
                0.89845862773635315787,
                0.90717057898931811435,
                0.91613621374849809513,
                0.92536801321276081183,
                0.93487933305513503866,
                0.94468448367470165185,
                0.95479881962778670259,
                0.96523883950474394881,
                0.97602229772443872744,
                0.98716832996311040700,
                0.99869759422592885941,
                1.0106324299186293446,
                1.0229970376960460493,
                1.0358176833704014299,
                1.0491229297753223260,
                1.0629439012277066659,
                1.0773145861418566953,
                1.0922721844711713307,
                1.1078575080369107147,
                1.1241154435222068440,
                1.1410954900555225259,
                1.1588523860034766843,
                1.1774468430005661827,
                1.1969464095796224804,
                1.2174264923240208789,
                1.2389715696379424894,
                1.2616766425694954149,
                1.2856489793787537301,
                1.3110102267770174743,
                1.3378989824739080544,
                1.3664739530045968945,
                1.3969178608922344119,
                1.4294423206281103030,
                1.4642939805979271642,
                1.5017623383916399836,
                1.5421897960602253947,
                1.5859847552927187373,
                1.6336389011844131988,
                1.6857503548125960429,
                1.7430552034230875194,
                1.8064712412205729527,
                1.8771599299166708295,
                1.9566162791192362073,
                2.0468028359333301478,
                2.1503558644945609607,
                2.2709146622933868486,
                2.4136715042011946407,
                2.5863411458761695616,
                2.8009892401268232472,
                3.0777924131600616977,
                3.4537195109187421402,
                4.0061518665671908603,
                4.9359533239463205273,
                7.0434078792100644325
            };

            const double k = 0.5;

            for (int i = -256; i < 16; i++) {
                decimal n = i / 16m;

                double expected = expecteds[i + 256];

                MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticPi(n, k);
                MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticPi(n, k);
                MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticPi(n, k);
                MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticPi(n, k);

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
        public void EllipticPiNearOneTest() {
            const int exponent = -32;

            foreach (double n in new double[] { -1, -0.5, 0.5, 0.75, 0.99609375 }) {
                {
                    MultiPrecision<Pow2.N4>[] ks = {
                    MultiPrecision<Pow2.N4>.BitDecrement(1),
                    MultiPrecision<Pow2.N4>.BitDecrement(1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent)),
                    1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent),
                    MultiPrecision<Pow2.N4>.BitIncrement(1 - MultiPrecision<Pow2.N4>.Ldexp(1, exponent)),
                };

                    foreach (var k in ks) {
                        MultiPrecision<Pow2.N4> y4 = MultiPrecision<Pow2.N4>.EllipticPi(n, k);
                        MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticPi(n, k.Convert<Pow2.N8>());

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
                        MultiPrecision<Pow2.N8> y8 = MultiPrecision<Pow2.N8>.EllipticPi(n, k);
                        MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticPi(n, k.Convert<Pow2.N16>());

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
                        MultiPrecision<Pow2.N16> y16 = MultiPrecision<Pow2.N16>.EllipticPi(n, k);
                        MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticPi(n, k.Convert<Pow2.N32>());

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
                        MultiPrecision<Pow2.N32> y32 = MultiPrecision<Pow2.N32>.EllipticPi(n, k);
                        MultiPrecision<Pow2.N64> y64 = MultiPrecision<Pow2.N64>.EllipticPi(n, k.Convert<Pow2.N64>());

                        Console.WriteLine(k.ToHexcode());
                        Console.WriteLine(y32);
                        Console.WriteLine(y32.ToHexcode());
                        Console.WriteLine(y64.ToHexcode());

                        Assert.IsTrue(MultiPrecision<Pow2.N32>.NearlyEqualBits(y32, y64.Convert<Pow2.N32>(), 256));
                    }
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

        [TestMethod]
        public void EllipticPiUnnormalValueTest() {
            MultiPrecision<Pow2.N8> inf = MultiPrecision<Pow2.N8>.EllipticPi(0.5, 1);

            Assert.AreEqual(MultiPrecision<Pow2.N8>.PositiveInfinity, inf);

            MultiPrecision<Pow2.N8>[] nans = new MultiPrecision<Pow2.N8>[] {
                -1,
                2,
                MultiPrecision<Pow2.N8>.PositiveInfinity,
                MultiPrecision<Pow2.N8>.NegativeInfinity,
                MultiPrecision<Pow2.N8>.NaN
            };

            foreach (MultiPrecision<Pow2.N8> v in nans) {
                MultiPrecision<Pow2.N8> y = MultiPrecision<Pow2.N8>.EllipticPi(0.5, v);

                Assert.IsTrue(y.IsNaN);
            }

            Assert.IsTrue(MultiPrecision<Pow2.N8>.EllipticPi(2, 0.5).IsNaN);
        }
    }
}
