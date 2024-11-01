using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;
using System.Collections.Generic;

namespace MultiPrecisionTest.Functions {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void SincTest() {
            MultiPrecision<Pow2.N4>[] expecteds = [
                "1.000000000000000000000000000000000000000",
                "0.9896158370180917183873948193975567835736",
                "0.9588510772084060005465758704311427761636",
                "0.9088516800311122223109892703731919137845",
                "0.8414709848078965066525023216302989996226",
                "0.7591876954844689714787926776288394003024",
                "0.6649966577360362872944822474276582151378",
                "0.5622776839279639421323788211245631963075",
                "0.4546487134128408476980099329558724213511",
                "0.3458103097279649961826518558167810314686",
                "0.2393888576415825976207418808744649086814",
                "0.1387858152917569813005677717737382913026",
                "0.04704000268662240736691493426937009328231",
                "-0.03329081062464873139564025924948461569813",
                "-0.1002237793398913851772482285838958814529",
                "-0.1524163516646250059824414862267449327783",
                "-0.1892006238269820628431597736279572735340",
                "-0.2105857313479020057566477243384179307955",
                "-0.2172289150366882345309188921108066173032",
                "-0.2103774292579743041545833169678061606837",
                "-0.1917848549326276937786308812311987946705",
                "-0.1636065701764937200629953694138491715913",
                "-0.1282800591946167102239853046403763779722",
                "-0.08839636130421883999683120161103187746279",
                "-0.04656924969982097880192590776864912660467",
                "-0.005308674647609090700391307326286829974107",
                "0.03309538278274084989183780754995872916696",
                "0.06667319611564705368489317680145044712503",
                "0.09385522838839844148528558451337645399098",
                "0.1135283971050352356443684390913181400986",
                "0.1250666635699651810597951730865396315242",
                "0.1283353263369259510476142969857794974880",
                "0.1236697808279227222260154497806610840146",
                "0.1118308133623442741447748561077476888673",
                "0.09393966030864591607846037180460148069290",
                "0.07139702328619341853023728009053462307960",
                "0.04579094280463961886180806292804835326043",
                "0.01879886328437119442422552966870390327563",
                "-0.007910644259137821819313947845684940803622",
                "-0.03277119934587421947717374501820943976425",
                "-0.05440211108893698134047476618513772816836",
                "-0.07167789564924833444627698321585367161443",
                "-0.08378054856873048552557225569771390806617",
                "-0.09023235980657474521027616007439807550014",
                "-0.09090820059551849609559680900232019153118",
                "-0.08602737755655657152010684167893301185288",
                "-0.07612627605986335017005961100924583797374",
                "-0.06201404049592936368474399955785501864277",
                "-0.04471440983336958097211451902020014935964",
                "-0.02539749836580631204763232205139942327119",
                "-0.005305751788096055114352785589076754181881",
            ];

            for ((int i, MultiPrecision<Pow2.N4> x) = (0, 0); i < expecteds.Length; i++, x += 1d / 4) {
                MultiPrecision<Pow2.N4> expected = expecteds[i];

                MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Sinc(x, normalized: false);
                MultiPrecision<Pow2.N4> y_neg = MultiPrecision<Pow2.N4>.Sinc(-x, normalized: false);
                MultiPrecision<Pow2.N4> y_dec = MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.BitDecrement(x), normalized: false);
                MultiPrecision<Pow2.N4> y_inc = MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.BitIncrement(x), normalized: false);

                MultiPrecision<Pow2.N4> y_normed = MultiPrecision<Pow2.N4>.Sinc(x * MultiPrecision<Pow2.N4>.RcpPi, normalized: true);

                Console.WriteLine(x);
                Console.WriteLine(y);

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y, 16));
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_neg, 16));

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_normed, 16));

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_dec, 16));
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_inc, 16));
            }

            foreach (bool normalized in new[] { false, true }) {
                Assert.AreEqual(1, MultiPrecision<Pow2.N4>.Sinc(0, normalized));

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                    MultiPrecision<Pow2.N4>.Sinc(-Math.ScaleB(1, -64), normalized),
                    MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.BitDecrement(-Math.ScaleB(1, -64)), normalized),
                    4)
                );

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                    MultiPrecision<Pow2.N4>.Sinc(-Math.ScaleB(1, -64), normalized),
                    MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.BitIncrement(-Math.ScaleB(1, -64)), normalized),
                    4)
                );

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                    MultiPrecision<Pow2.N4>.Sinc(+Math.ScaleB(1, -64), normalized),
                    MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.BitDecrement(+Math.ScaleB(1, -64)), normalized),
                    4)
                );

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                    MultiPrecision<Pow2.N4>.Sinc(+Math.ScaleB(1, -64), normalized),
                    MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.BitIncrement(+Math.ScaleB(1, -64)), normalized),
                    4)
                );

                Assert.IsTrue(1 == MultiPrecision<Pow2.N4>.Sinc(-MultiPrecision<Pow2.N4>.Epsilon, normalized));
                Assert.IsTrue(1 == MultiPrecision<Pow2.N4>.Sinc(+MultiPrecision<Pow2.N4>.Epsilon, normalized));

                Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.NaN, normalized)), "nan");
                Assert.AreEqual(0, MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.PositiveInfinity, normalized), "+inf");
                Assert.AreEqual(0, MultiPrecision<Pow2.N4>.Sinc(MultiPrecision<Pow2.N4>.NegativeInfinity, normalized), "-inf");
            }
        }

        [TestMethod]
        public void SinhcTest() {
            MultiPrecision<Pow2.N4>[] expecteds = [
                "1.000000000000000000000000000000000000000",
                "1.010449267232673231656500602168231622079",
                "1.042190610987494723244851252822983118212",
                "1.096422309247773307604882179262551798110",
                "1.175201193643801456882381850595600815156",
                "1.281535264240660510322264241209771151943",
                "1.419519636729878331222924996451754432555",
                "1.594522495015795802910223556050377529974",
                "1.813430203923509383834106991400630852443",
                "2.084963691510369196392700301171342294778",
                "2.420081792415914928580129455340161275069",
                "2.832491640542084370456412367381583108314",
                "3.339291642469967299658197873155276020059",
                "3.961779339901744627568185820632250112830",
                "4.726464939324285035701923294003570921057",
                "5.666341900560903192970936191533790037881",
                "6.822479299281938112227067897698454645072",
                "8.246017425032806923112775652851532373103",
                "10.00066914488706347151325706792082559113",
                "12.16585608757731973676181006658650822785",
                "14.84064211555775179540189439921291311992",
                "18.14866866097436331358277925962661914959",
                "22.24434959025290216801777134628678449235",
                "27.32065021781719030240014848282780309250",
                "33.61885956171320468749701128132978857817",
                "41.44087153713646385843570469655277784511",
                "51.16462535424375870165493164148478581287",
                "63.26352530715042678563974909664583636058",
                "78.33087475332093176767944536800264451140",
                "97.11063020898667120489812016643222378453",
                "120.5361240914462039380145587185811281114",
                "149.7788376689365271396847988624746663580",
                "186.3098532236937732644845798789851808059",
                "231.9773066776575106447069850778789224895",
                "289.1040374606332567521881945537881234205",
                "360.6107399787256503546885799697292441069",
                "450.1713224536433289461359550997816241654",
                "562.4089524567065527776788124887408976054",
                "703.1435134110548625369033948276443323294",
                "879.7040385126318622657416010320749373758",
                "1101.323287470339337723652455484636440290",
                "1379.636189511096499487012847198175775296",
                "1729.309649843342304245946144947881946024",
                "2168.838531724600971943130551470571008895",
                "2721.551895386187166594583021801012011531",
                "3416.885322296465146273684172035636885830",
                "4291.990043505669731716502495033855217615",
                "5393.768467993791243220781545605344323724",
                "6781.449642202487852278208130825186457975",
                "8529.848525099095678349661297939492400866",
                "10733.49146068591215137604011176902204999",
                "13511.83906411456394246150490816046810771",
                "17015.89969256385284304314313935780786515",
                "21436.60528380116684816012460390357688961",
                "27015.42110541964343609994480265931930212",
                "34057.78757205406370847821750960927617384",
                "42950.15300585518746536190010299269515250",
            ];

            for ((int i, MultiPrecision<Pow2.N4> x) = (0, 0); i < expecteds.Length; i++, x += 1d / 4) {
                MultiPrecision<Pow2.N4> expected = expecteds[i];

                MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Sinhc(x);
                MultiPrecision<Pow2.N4> y_neg = MultiPrecision<Pow2.N4>.Sinhc(-x);
                MultiPrecision<Pow2.N4> y_dec = MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.BitDecrement(x));
                MultiPrecision<Pow2.N4> y_inc = MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.BitIncrement(x));

                Console.WriteLine(x);
                Console.WriteLine(y);

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y, 16));
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_neg, 16));

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_dec, 16));
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_inc, 16));
            }

            Assert.AreEqual(1, MultiPrecision<Pow2.N4>.Sinhc(0));

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Sinhc(-Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.BitDecrement(-Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Sinhc(-Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.BitIncrement(-Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Sinhc(+Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.BitDecrement(+Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Sinhc(+Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.BitIncrement(+Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(1 == MultiPrecision<Pow2.N4>.Sinhc(-MultiPrecision<Pow2.N4>.Epsilon));
            Assert.IsTrue(1 == MultiPrecision<Pow2.N4>.Sinhc(+MultiPrecision<Pow2.N4>.Epsilon));

            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.NaN)), "nan");
            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsPositiveInfinity(MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.PositiveInfinity)), "+inf");
            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsPositiveInfinity(MultiPrecision<Pow2.N4>.Sinhc(MultiPrecision<Pow2.N4>.NegativeInfinity)), "-inf");
        }

        [TestMethod]
        public void JincTest() {
            MultiPrecision<Pow2.N4>[] expecteds = [
                "0.5",
                "0.4961039092909076909262999066552695967425",
                "0.4845369153497477727679091522830632816013",
                "0.4656581362331495900310413552350017806349",
                "0.4400505857449335159596822037189149131274",
                "0.4084986082559043736555798698199283002823",
                "0.3719576719400664279934141421040595996863",
                "0.3315178272222814270367415500494596909627",
                "0.2883624038784366936012241211345685434602",
                "0.2437237140653156276453855897310085521186",
                "0.1988376409857096152043265105057688970085",
                "0.1548990192574190331780268266629688549679",
                "0.1130196528419788196418381990688262989910",
                "0.07419067323544735022651367808651558624711",
                "0.03925072210352205306175197062399162723340",
                "0.008861159767914594267657442632989077671678",
                "-0.01651083200588728403579635520081875718186",
                "-0.03660075128902181318758816380857321366043",
                "-0.05134676264963791866846589436561181287975",
                "-0.06088143129412850752166763703430544845394",
                "-0.06551582751829304440754686438203382655217",
                "-0.06571694830084527290935457138605186593557",
                "-0.06207967553255333639631472718992256773019",
                "-0.05529469981205785938353831574661590388434",
                "-0.04611397635459426802879580050769229312350",
                "-0.03531534040627796319447764825139136968363",
                "-0.02366789252461105186304361758052960830094",
                "-0.01189967188967069859940987248748407803429",
                "-0.0006689747831922618141591151709018570790596",
                "0.009459544917673344073182744410795780079121",
                "0.01803312367729406735763206580736861719232",
                "0.02472291512117649104102857571644597870756",
                "0.02932954335673932804765958144880682644359",
                "0.03178224872639319014819441220960084229574",
                "0.03213199572635926403117692268111365089344",
                "0.03053918759843218431707472263752905201411",
                "0.02725686517481391914695995236822095256341",
                "0.02261044869947255297303765445685240283907",
                "0.01697520323763472115269902072045884899292",
                "0.01075266679574333437774891906251039479197",
                "0.004347274616886143666974876802585928830631",
                "-0.001855654214328640681762925823445074878417",
                "-0.007509525164507760776468283888367294346032",
                "-0.01232280023655722820129143012318055268115",
                "-0.01607139081424740919433918904393399178540",
                "-0.01860668263330849912512283610968069018134",
                "-0.01985901049263682387950804178073816770526",
                "-0.01983664581055015231373912071354970158949",
                "-0.01862059204088563436397480969702476322379",
                "-0.01635568969435550977474918400100814140730",
                "-0.01323870436918077747670098067393695553008",
                "-0.009504200064564067929847617185345547050326",
                "-0.005409080932444490088982261454464157313177",
                "-0.001216715036555995937459518898257913842028",
            ];

            for ((int i, MultiPrecision<Pow2.N4> x) = (0, 0); i < expecteds.Length; i++, x += 1d / 4) {
                MultiPrecision<Pow2.N4> expected = expecteds[i];

                MultiPrecision<Pow2.N4> y = MultiPrecision<Pow2.N4>.Jinc(x);
                MultiPrecision<Pow2.N4> y_neg = MultiPrecision<Pow2.N4>.Jinc(-x);
                MultiPrecision<Pow2.N4> y_dec = MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.BitDecrement(x));
                MultiPrecision<Pow2.N4> y_inc = MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.BitIncrement(x));

                Console.WriteLine(x);
                Console.WriteLine(y);

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y, 16));
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_neg, 16));

                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_dec, 16));
                Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(expected, y_inc, 16));
            }

            Assert.AreEqual(0.5, MultiPrecision<Pow2.N4>.Jinc(0));

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Jinc(-Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.BitDecrement(-Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Jinc(-Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.BitIncrement(-Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Jinc(+Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.BitDecrement(+Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(MultiPrecision<Pow2.N4>.NearlyEqualBits(
                MultiPrecision<Pow2.N4>.Jinc(+Math.ScaleB(1, -64)),
                MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.BitIncrement(+Math.ScaleB(1, -64))),
                4)
            );

            Assert.IsTrue(0.5 == MultiPrecision<Pow2.N4>.Jinc(-MultiPrecision<Pow2.N4>.Epsilon));
            Assert.IsTrue(0.5 == MultiPrecision<Pow2.N4>.Jinc(+MultiPrecision<Pow2.N4>.Epsilon));

            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsNaN(MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.NaN)), "nan");
            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsZero(MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.PositiveInfinity)), "+inf");
            Assert.IsTrue(MultiPrecision<Pow2.N4>.IsZero(MultiPrecision<Pow2.N4>.Jinc(MultiPrecision<Pow2.N4>.NegativeInfinity)), "-inf");
        }
    }
}
