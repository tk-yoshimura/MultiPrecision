using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using MultiPrecision;

namespace MultiPrecisionSandbox {
    public static class LogGammaApprox<N> where N : struct, IConstant {
        public static readonly ReadOnlyCollection<MultiPrecision<N>> SterlingTable;

        public static (MultiPrecision<N> hi, MultiPrecision<N> lo) SterlingApprox(MultiPrecision<N> x, int n) {
            MultiPrecision<N> w_hi = MultiPrecision<N>.Log(2 * MultiPrecision<N>.PI) / 2;
            MultiPrecision<N> w_lo = MultiPrecision<N>.Zero;

            (w_hi, w_lo) = MultiPrecisionUtil.KahanSum(w_hi, w_lo, MultiPrecision<N>.Ldexp((MultiPrecision<N>.Ldexp(x, 1) + 1) * MultiPrecision<N>.Log(x), -1));
            (w_hi, w_lo) = MultiPrecisionUtil.KahanSum(w_hi, w_lo, -x);

            MultiPrecision<N> invx = MultiPrecision<N>.One / x, z = invx, sq_invx = invx * invx;

            for(int i = 0; i < n; i++){
                MultiPrecision<N> s = SterlingTable[i]; 

                MultiPrecision<N> dw = s * z;
                (w_hi, w_lo) = MultiPrecisionUtil.KahanSum(w_hi, w_lo, dw);

                if (dw.IsZero || w_hi.Exponent - dw.Exponent > MultiPrecision<N>.Bits) {
                    break;
                }
                z *= sq_invx;
            }

            return (w_hi, w_lo);
        }

        public static long ConvergenceTimes(int n, long t_max = 100) { 
            MultiPrecision<N> p0 = 2, p1 = 6, p2 = 24, p3 = 120, ph = MultiPrecision<N>.Sqrt(MultiPrecision<N>.PI) * 105 / 16;
            MultiPrecision<N> lp0 = MultiPrecision<N>.Log(p0), lp1 = MultiPrecision<N>.Log(p1);
            MultiPrecision<N> lp2 = MultiPrecision<N>.Log(p2), lp3 = MultiPrecision<N>.Log(p3);
            MultiPrecision<N> lph = MultiPrecision<N>.Log(ph);

            (MultiPrecision<N> hi, MultiPrecision<N> lo) v0 = SterlingApprox(2, n);
            (MultiPrecision<N> hi, MultiPrecision<N> lo) v1 = SterlingApprox(6, n);
            (MultiPrecision<N> hi, MultiPrecision<N> lo) v2 = SterlingApprox(24, n);
            (MultiPrecision<N> hi, MultiPrecision<N> lo) v3 = SterlingApprox(120, n);
            (MultiPrecision<N> hi, MultiPrecision<N> lo) vh = SterlingApprox(3.5, n);

            for(long t = 3; t < t_max; t++) { 
                MultiPrecision<N> e0 = MultiPrecisionUtil.KahanSum(v0.hi, v0.lo, -lp0).hi;
                MultiPrecision<N> e3 = MultiPrecisionUtil.KahanSum(v3.hi, v3.lo, -lp3).hi;

                MultiPrecision<N> y00 = MultiPrecision<N>.Pow(t - 1, -n * 2), y03 = y00 / (t - 1);
                MultiPrecision<N> y30 = MultiPrecision<N>.Pow(t + 2, -n * 2), y33 = y30 / (t + 2);

                MultiPrecision<N> s = 1 / (y03 * y30 - y00 * y33);
                MultiPrecision<N> a = s * (e3 * y03 - e0 * y33), b = s * (e0 * y30 - e3 * y00);

                MultiPrecision<N> u0 = v0.hi - (a * MultiPrecision<N>.Pow(t - 1, -n * 2) + b * MultiPrecision<N>.Pow(t - 1, -n * 2 - 1));
                MultiPrecision<N> u1 = v1.hi - (a * MultiPrecision<N>.Pow(t, -n * 2) + b * MultiPrecision<N>.Pow(t, -n * 2 - 1));
                MultiPrecision<N> u2 = v2.hi - (a * MultiPrecision<N>.Pow(t + 1, -n * 2) + b * MultiPrecision<N>.Pow(t + 1, -n * 2 - 1));
                MultiPrecision<N> u3 = v3.hi - (a * MultiPrecision<N>.Pow(t + 2, -n * 2) + b * MultiPrecision<N>.Pow(t + 2, -n * 2 - 1));
                MultiPrecision<N> uh = vh.hi - (a * MultiPrecision<N>.Pow(t + 0.5, -n * 2) + b * MultiPrecision<N>.Pow(t + 0.5, -n * 2 - 1));

                MultiPrecision<N> em0 = lp0 - u0;
                MultiPrecision<N> e1 = lp1 - u1;
                MultiPrecision<N> e2 = lp2 - u2;
                MultiPrecision<N> em3 = lp3 - u3;
                MultiPrecision<N> eh = lph - uh;

                //Console.WriteLine($"{lp1},{v1.hi},{u1}");
                //Console.WriteLine($"{lp2},{v2.hi},{u2}");
                //Console.WriteLine($"{lph},{vh.hi},{uh}");

                Console.WriteLine($"{t}:{e0:E5}->{e1:E5}");
                     
                p0 = p1; p1 = p2; p2 = p3; p3 *= t + 3;
                lp0 = lp1; lp1 = lp2; lp2 = lp3; lp3 = MultiPrecision<N>.Log(p3);
                ph = MultiPrecision<N>.Ldexp(ph * (t * 2 + 3), -1);
                lph = MultiPrecision<N>.Log(ph);

                v0 = v1; v1 = v2; v2 = v3; v3 = SterlingApprox(t + 3, n);
                vh = SterlingApprox(0.5 * (t * 2 + 3), n);
            }

            return t_max;
        }

        static LogGammaApprox() { 
            MultiPrecision<N>[] sterling_table = new MultiPrecision<N>[] {
                MultiPrecision<N>.Div(1L, 12L),
                MultiPrecision<N>.Div(-1L, 360L),
                MultiPrecision<N>.Div(1L, 1260L),
                MultiPrecision<N>.Div(-1L, 1680L),
                MultiPrecision<N>.Div(1L, 1188L),
                MultiPrecision<N>.Div(-691L, 360360L),
                MultiPrecision<N>.Div(1L, 156L),
                MultiPrecision<N>.Div(-3617L, 122400L),
                MultiPrecision<N>.Div(43867L, 244188L),
                MultiPrecision<N>.Div(-174611L, 125400L),
                MultiPrecision<N>.Div(77683L, 5796L),
                MultiPrecision<N>.Div(-236364091L, 1506960L),
                MultiPrecision<N>.Div(657931L, 300L),
                MultiPrecision<N>.Div(-3392780147L, 93960L),
                MultiPrecision<N>.Div(1723168255201L, 2492028L),
                MultiPrecision<N>.Div(-7709321041217L, 505920L),
                MultiPrecision<N>.Div(151628697551L, 396L),
                MultiPrecision<N>.Div("-26315271553053477373", 2418179400L),
                MultiPrecision<N>.Div(154210205991661L, 444L),
                MultiPrecision<N>.Div("-261082718496449122051", 21106800L),
                MultiPrecision<N>.Div("1520097643918070802691", 3109932L),
                MultiPrecision<N>.Div("-2530297234481911294093", 118680L),
                MultiPrecision<N>.Div("25932657025822267968607", 25380L),
                MultiPrecision<N>.Div("-5609403368997817686249127547", 104700960L),
                MultiPrecision<N>.Div("19802288209643185928499101", 6468L),
                MultiPrecision<N>.Div("-61628132164268458257532691681", 324360L),
                MultiPrecision<N>.Div("29149963634884862421418123812691", 2283876L),
                MultiPrecision<N>.Div("-354198989901889536240773677094747", 382800L),
                MultiPrecision<N>.Div("2913228046513104891794716413587449", 40356L),
                MultiPrecision<N>.Div("-1215233140483755572040304994079820246041491", 201025024200L),
                MultiPrecision<N>.Div("396793078518930920708162576045270521", 732L),
                MultiPrecision<N>.Div("-106783830147866529886385444979142647942017", 2056320L),
                MultiPrecision<N>.Div("133872729284212332186510857141084758385627191", 25241580L),
                MultiPrecision<N>.Div("-4633713579924631067171126424027918014373353", 8040L),
                MultiPrecision<N>.Div("43010895638096200108659330496510205957469661721", 646668L),
                MultiPrecision<N>.Div("-5827954961669944110438277244641067365282488301844260429", 716195647440L),
                MultiPrecision<N>.Div("923038305114085622008920911661422572613197507651", 876L),
                MultiPrecision<N>.Div("-1297636253996598563562484002136063152861329885729779", 9000L),
                MultiPrecision<N>.Div("31911258890415448330398387349964774884015336567107729499", 1532916L),
                MultiPrecision<N>.Div("-4603784299479457646935574969019046849794257872751288919656867", 1453663200L),
                MultiPrecision<N>.Div("40902784126466971629833036824055363419700878721225693045893", 80676L),
                MultiPrecision<N>.Div("-2024576195935290360231131160111731009989917391198090877281083932477", 23734849320L),
                MultiPrecision<N>.Div("15365456265527410548229019707587476657623969457055556552604581", 1020L),
                MultiPrecision<N>.Div("-119220589879456137090501038547210167576886388688366240390629080961277", 42741360L),
                MultiPrecision<N>.Div("235811455804216559976824670249843016755050989933929423246309043145584507", 435933036L),
                MultiPrecision<N>.Div("-56329823835110327303888166458198989115623542770410223609709405514345489", 513240L),
                MultiPrecision<N>.Div("25974761842122222757602163391770238382095923727716949392259895857120201", 1116L),
                MultiPrecision<N>.Div("-211600449597266513097597728109824233673043954389060234150638733420050668349987259", 41056142400L),
                MultiPrecision<N>.Div("1385882870875622359674512602987828685864126443438867092021917576530468307311", 1164L),
                MultiPrecision<N>.Div("-94598037819122125295227433069493721872702841533066936133385696204311395415197247711", 329967000L),
                MultiPrecision<N>.Div("188471730050641592837824751889190692675989246891336320628176617710991854755098715223", 2621556L),
                MultiPrecision<N>.Div("-24579510104910000868238719445859559590091508159867133717469497704834582181965363171081", 1310160L),
                MultiPrecision<N>.Div("686300059860705932229279625501762776777949030954371510168505969401544903271437780124017", 134820L),
                MultiPrecision<N>.Div("-3469342247847828789552088659323852541399766785760491146870005891371501266319724897592306597338057", 2417419400760L),
                MultiPrecision<N>.Div("1529198588096948578449626849344869500105750482682461581336718774151959521253917155995586043503", 3640164L),
                MultiPrecision<N>.Div("-378697086022157101907513887812166002063307357027501413778826918240138108073553645052397314793228587", 2968175520L),
                MultiPrecision<N>.Div("1144096437861534912279513741402789025037955614688991124252953163943873972854452297416009927292771", 28476L),
                MultiPrecision<N>.Div("-10674272985235964721121153725304876443758763036701029111913932611069274397399033388668558606750163871", 814200L),
                MultiPrecision<N>.Div("6219713897791747653134697484501440791264514594588122106797617638583296356708584099819860616178255861", 1404L),
                MultiPrecision<N>.Div("-51507486535079109061843996857849983274095170353262675213092869167199297474922985358811329367077682677803282070131", 33247494680400L),
                MultiPrecision<N>.Div("813666657037091506762830122557225531782340824771477701152611668740175134657384120148083830788492425714171", 1452L),
                MultiPrecision<N>.Div("-3092799204330552540347577519598143374857381613332172155469468152559954624336643368181675600331241054486922871", 14760L),
                MultiPrecision<N>.Div("5556330281949274850616324408918951380525567307126747246796782304333594286400508981287241419934529638692081513802696639", 68636578500L),
                MultiPrecision<N>.Div("-267754707742548082886954405585282394779291459592551740629978686063357792734863530145362663093519862048495908453718017", 8290560L),
            };

            SterlingTable = Array.AsReadOnly(sterling_table);
        }
    }
}
