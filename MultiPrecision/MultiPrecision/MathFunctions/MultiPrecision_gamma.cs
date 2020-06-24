using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Gamma(MultiPrecision<N> x) {

            if (x.IsZero) { 
                return NaN;
            }

            if(x.Sign == Sign.Plus){
                MultiPrecision<Next<N>> y;
                
                if(x >= Integer(Consts.LogGamma.ApproxThreshold)) {
                    y = MultiPrecision<Next<N>>.Exp(LogGammaSterlingApprox(x));
                }
                else{
                    MultiPrecision<N> x_int = Floor(x), x_frac = x - x_int;
                    MultiPrecision<N> z = x_frac + Consts.LogGamma.ApproxThreshold;
                    MultiPrecision<Next<N>> w = MultiPrecision<Next<N>>.Exp(LogGammaSterlingApprox(z));

                    MultiPrecision<Next<N>> z_next = MultiPrecisionUtil.Convert<Next<N>, N>(z);

                    MultiPrecision<Next<N>> s = MultiPrecision<Next<N>>.One;

                    for(long i = (long)x_int; i < Consts.LogGamma.ApproxThreshold; i++) { 
                        z_next -= MultiPrecision<Next<N>>.One;
                        s *= z_next;
                    }

                    y = w / s;
                }

                return RoundMantissa(MultiPrecisionUtil.Convert<N, Next<N>>(y), Bits - Consts.LogGamma.RoundBits);
            }
            else { 
                if(x == Truncate(x)) { 
                    return NaN;
                }

                MultiPrecision<N> w = Gamma(-x);

                return -PI / (w * SinPI(x) * x);
            }
        }

        public static MultiPrecision<N> LogGamma(MultiPrecision<N> x) {
            if(!(x > Zero)) { 
                return NaN;
            }

            if(x < Integer(Consts.LogGamma.NearOneThreshold)) {
                return Log(Gamma(x));
            }

            MultiPrecision<Next<N>> y;

            if (x >= Integer(Consts.LogGamma.ApproxThreshold)) {
                y = LogGammaSterlingApprox(x);
            }
            else {

                MultiPrecision<N> x_int = Floor(x), x_frac = x - x_int;
                MultiPrecision<N> z = x_frac + Consts.LogGamma.ApproxThreshold;
                MultiPrecision<Next<N>> w = LogGammaSterlingApprox(z);

                MultiPrecision<Next<N>> z_next = MultiPrecisionUtil.Convert<Next<N>, N>(z);

                MultiPrecision<Next<N>> s = MultiPrecision<Next<N>>.Zero;

                for (long i = (long)x_int; i < Consts.LogGamma.ApproxThreshold; i++) {
                    z_next -= MultiPrecision<Next<N>>.One;
                    s += MultiPrecision<Next<N>>.Log2(z_next);
                }

                y = w - s / MultiPrecision<Next<N>>.Log2(MultiPrecision<Next<N>>.E);
            }

            return RoundMantissa(MultiPrecisionUtil.Convert<N, Next<N>>(y), Bits - Consts.LogGamma.RoundBits);
        }

        internal static MultiPrecision<Next<N>> LogGammaSterlingApprox(MultiPrecision<N> x) {
            if (!Consts.LogGamma.Initialized) {
                Consts.LogGamma.Initialize();
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(x.Sign == Sign.Plus && x >= Consts.LogGamma.ApproxThreshold);
#endif

            MultiPrecision<Next<N>> x_next = MultiPrecisionUtil.Convert<Next<N>, N>(x);
            MultiPrecision<Next<N>> w = Consts.LogGamma.C0;
            w += MultiPrecision<Next<N>>.Ldexp((MultiPrecision<Next<N>>.Ldexp(x_next, 1) - MultiPrecision<Next<N>>.One) * MultiPrecision<Next<N>>.Log(x_next), -1);
            w -= x_next;

            MultiPrecision<Next<N>> invx = MultiPrecision<Next<N>>.One / x_next, z = invx, sq_invx = invx * invx;

            foreach (MultiPrecision<Next<N>> s in Consts.LogGamma.SterlingTable) {
                MultiPrecision<Next<N>> dw = s * z;
                w += dw;

                if (dw.IsZero || w.Exponent - dw.Exponent > Bits) {
                    break;
                }

                z *= sq_invx;
            }

            return w;
        }

        private static partial class Consts {
            public static class LogGamma {
                public static readonly long ApproxThreshold = (long)(8.5 * Math.Exp(0.170 * Length));
                public static readonly long NearOneThreshold = 3;
                public static int RoundBits { private set; get; } = 0;

                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<Next<N>> C0 { private set; get; } = null;
                public static ReadOnlyCollection<MultiPrecision<Next<N>>> SterlingTable { private set; get; } = null;

                public static void Initialize() {
                    C0 = MultiPrecision<Next<N>>.Ldexp(MultiPrecision<Next<N>>.Log(MultiPrecision<Next<N>>.Ldexp(MultiPrecision<Next<N>>.PI, 1)), -1);

                    MultiPrecision<Next<N>>[] sterling_table = new MultiPrecision<Next<N>>[] {
                        MultiPrecision<Next<N>>.Div(1L, 12L),
                        MultiPrecision<Next<N>>.Div(-1L, 360L),
                        MultiPrecision<Next<N>>.Div(1L, 1260L),
                        MultiPrecision<Next<N>>.Div(-1L, 1680L),
                        MultiPrecision<Next<N>>.Div(1L, 1188L),
                        MultiPrecision<Next<N>>.Div(-691L, 360360L),
                        MultiPrecision<Next<N>>.Div(1L, 156L),
                        MultiPrecision<Next<N>>.Div(-3617L, 122400L),
                        MultiPrecision<Next<N>>.Div(43867L, 244188L),
                        MultiPrecision<Next<N>>.Div(-174611L, 125400L),
                        MultiPrecision<Next<N>>.Div(77683L, 5796L),
                        MultiPrecision<Next<N>>.Div(-236364091L, 1506960L),
                        MultiPrecision<Next<N>>.Div(657931L, 300L),
                        MultiPrecision<Next<N>>.Div(-3392780147L, 93960L),
                        MultiPrecision<Next<N>>.Div(1723168255201L, 2492028L),
                        MultiPrecision<Next<N>>.Div(-7709321041217L, 505920L),
                        MultiPrecision<Next<N>>.Div(151628697551L, 396L),
                        MultiPrecision<Next<N>>.Div("-26315271553053477373", 2418179400L),
                        MultiPrecision<Next<N>>.Div(154210205991661L, 444L),
                        MultiPrecision<Next<N>>.Div("-261082718496449122051", 21106800L),
                        MultiPrecision<Next<N>>.Div("1520097643918070802691", 3109932L),
                        MultiPrecision<Next<N>>.Div("-2530297234481911294093", 118680L),
                        MultiPrecision<Next<N>>.Div("25932657025822267968607", 25380L),
                        MultiPrecision<Next<N>>.Div("-5609403368997817686249127547", 104700960L),
                        MultiPrecision<Next<N>>.Div("19802288209643185928499101", 6468L),
                        MultiPrecision<Next<N>>.Div("-61628132164268458257532691681", 324360L),
                        MultiPrecision<Next<N>>.Div("29149963634884862421418123812691", 2283876L),
                        MultiPrecision<Next<N>>.Div("-354198989901889536240773677094747", 382800L),
                        MultiPrecision<Next<N>>.Div("2913228046513104891794716413587449", 40356L),
                        MultiPrecision<Next<N>>.Div("-1215233140483755572040304994079820246041491", 201025024200L),
                        MultiPrecision<Next<N>>.Div("396793078518930920708162576045270521", 732L),
                        MultiPrecision<Next<N>>.Div("-106783830147866529886385444979142647942017", 2056320L),
                        MultiPrecision<Next<N>>.Div("133872729284212332186510857141084758385627191", 25241580L),
                        MultiPrecision<Next<N>>.Div("-4633713579924631067171126424027918014373353", 8040L),
                        MultiPrecision<Next<N>>.Div("43010895638096200108659330496510205957469661721", 646668L),
                        MultiPrecision<Next<N>>.Div("-5827954961669944110438277244641067365282488301844260429", 716195647440L),
                        MultiPrecision<Next<N>>.Div("923038305114085622008920911661422572613197507651", 876L),
                        MultiPrecision<Next<N>>.Div("-1297636253996598563562484002136063152861329885729779", 9000L),
                        MultiPrecision<Next<N>>.Div("31911258890415448330398387349964774884015336567107729499", 1532916L),
                        MultiPrecision<Next<N>>.Div("-4603784299479457646935574969019046849794257872751288919656867", 1453663200L),
                        MultiPrecision<Next<N>>.Div("40902784126466971629833036824055363419700878721225693045893", 80676L),
                        MultiPrecision<Next<N>>.Div("-2024576195935290360231131160111731009989917391198090877281083932477", 23734849320L),
                        MultiPrecision<Next<N>>.Div("15365456265527410548229019707587476657623969457055556552604581", 1020L),
                        MultiPrecision<Next<N>>.Div("-119220589879456137090501038547210167576886388688366240390629080961277", 42741360L),
                        MultiPrecision<Next<N>>.Div("235811455804216559976824670249843016755050989933929423246309043145584507", 435933036L),
                        MultiPrecision<Next<N>>.Div("-56329823835110327303888166458198989115623542770410223609709405514345489", 513240L),
                        MultiPrecision<Next<N>>.Div("25974761842122222757602163391770238382095923727716949392259895857120201", 1116L),
                        MultiPrecision<Next<N>>.Div("-211600449597266513097597728109824233673043954389060234150638733420050668349987259", 41056142400L),
                        MultiPrecision<Next<N>>.Div("1385882870875622359674512602987828685864126443438867092021917576530468307311", 1164L),
                        MultiPrecision<Next<N>>.Div("-94598037819122125295227433069493721872702841533066936133385696204311395415197247711", 329967000L),
                        MultiPrecision<Next<N>>.Div("188471730050641592837824751889190692675989246891336320628176617710991854755098715223", 2621556L),
                        MultiPrecision<Next<N>>.Div("-24579510104910000868238719445859559590091508159867133717469497704834582181965363171081", 1310160L),
                        MultiPrecision<Next<N>>.Div("686300059860705932229279625501762776777949030954371510168505969401544903271437780124017", 134820L),
                        MultiPrecision<Next<N>>.Div("-3469342247847828789552088659323852541399766785760491146870005891371501266319724897592306597338057", 2417419400760L),
                        MultiPrecision<Next<N>>.Div("1529198588096948578449626849344869500105750482682461581336718774151959521253917155995586043503", 3640164L),
                        MultiPrecision<Next<N>>.Div("-378697086022157101907513887812166002063307357027501413778826918240138108073553645052397314793228587", 2968175520L),
                        MultiPrecision<Next<N>>.Div("1144096437861534912279513741402789025037955614688991124252953163943873972854452297416009927292771", 28476L),
                        MultiPrecision<Next<N>>.Div("-10674272985235964721121153725304876443758763036701029111913932611069274397399033388668558606750163871", 814200L),
                        MultiPrecision<Next<N>>.Div("6219713897791747653134697484501440791264514594588122106797617638583296356708584099819860616178255861", 1404L),
                        MultiPrecision<Next<N>>.Div("-51507486535079109061843996857849983274095170353262675213092869167199297474922985358811329367077682677803282070131", 33247494680400L),
                        MultiPrecision<Next<N>>.Div("813666657037091506762830122557225531782340824771477701152611668740175134657384120148083830788492425714171", 1452L),
                        MultiPrecision<Next<N>>.Div("-3092799204330552540347577519598143374857381613332172155469468152559954624336643368181675600331241054486922871", 14760L),
                        MultiPrecision<Next<N>>.Div("5556330281949274850616324408918951380525567307126747246796782304333594286400508981287241419934529638692081513802696639", 68636578500L),
                        MultiPrecision<Next<N>>.Div("-267754707742548082886954405585282394779291459592551740629978686063357792734863530145362663093519862048495908453718017", 8290560L),
                    };

                    SterlingTable = Array.AsReadOnly(sterling_table);

                    Initialized = true;
                    FixRoundBits();
                }

                public static void FixRoundBits() { 
                    MultiPrecision<N> y1 = Gamma(One), y2 = Gamma(2), y3 = Gamma(3);

                    while(RoundBits < Bits / 2) { 
                        if(RoundMantissa(y1, Bits - RoundBits) != One
                        || RoundMantissa(y2, Bits - RoundBits) != One
                        || RoundMantissa(y3, Bits - RoundBits) != 2) { 

                            RoundBits++;
                        }
                        else {
                            break;
                        }
                    }

#if DEBUG
                    Trace.WriteLine($"Gamma round bits : {RoundBits}bits @{Length}length");
#endif

                }
            }
        }
    }
}
