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
                if(x >= Integer(Consts.LogGamma.ApproxThreshold)) {
                    return RoundMantissa(Exp(LogGammaSterlingApprox(x).hi), Bits - Consts.LogGamma.RoundBits);
                }
                else{
                    MultiPrecision<N> x_int = Floor(x), x_frac = x - x_int;
                    MultiPrecision<N> z = x_frac + Integer(Consts.LogGamma.ApproxThreshold);
                    MultiPrecision<N> w = Exp(LogGammaSterlingApprox(z).hi);

                    MultiPrecision<N> s = One;
                    for(long i = (long)x_int; i < Consts.LogGamma.ApproxThreshold; i++) { 
                        z -= One;
                        s *= z;
                    }

                    return RoundMantissa(w / s, Bits - Consts.LogGamma.RoundBits);
                }
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

            if(x >= Integer(Consts.LogGamma.ApproxThreshold)) {
                return RoundMantissa(LogGammaSterlingApprox(x).hi, Bits - Consts.LogGamma.RoundBits);
            }

            MultiPrecision<N> x_int = Floor(x), x_frac = x - x_int;
            MultiPrecision<N> z = x_frac + Integer(Consts.LogGamma.ApproxThreshold);
            (MultiPrecision<N> w_hi, MultiPrecision<N> w_lo) = LogGammaSterlingApprox(z);

            (MultiPrecision<N> s_hi, MultiPrecision<N> s_lo) = (Zero, Zero);
            for(long i = (long)x_int; i < Consts.LogGamma.ApproxThreshold; i++) { 
                z -= One;
                (s_hi, s_lo) = MultiPrecisionUtil.KahanSum(s_hi, s_lo, Log2(z));
            }

            (w_hi, _) = MultiPrecisionUtil.KahanSum(w_hi, w_lo, -s_hi / Log2(E));

            return RoundMantissa(w_hi, Bits - Consts.LogGamma.RoundBits);
        }

        internal static (MultiPrecision<N> hi, MultiPrecision<N> lo) LogGammaSterlingApprox(MultiPrecision<N> x) {
            if (!Consts.LogGamma.Initialized) {
                Consts.LogGamma.Initialize();
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(x.Sign == Sign.Plus && x >= Consts.LogGamma.ApproxThreshold);
#endif

            MultiPrecision<N> w_hi = Consts.LogGamma.C0;
            MultiPrecision<N> w_lo = Zero;

            (w_hi, w_lo) = MultiPrecisionUtil.KahanSum(w_hi, w_lo, Ldexp((Ldexp(x, 1) - One) * Log(x), -1));
            (w_hi, w_lo) = MultiPrecisionUtil.KahanSum(w_hi, w_lo, -x);

            MultiPrecision<N> invx = One / x, z = invx, sq_invx = invx * invx;

            foreach (MultiPrecision<N> s in Consts.LogGamma.SterlingTable) {
                MultiPrecision<N> dw = s * z;
                (w_hi, w_lo) = MultiPrecisionUtil.KahanSum(w_hi, w_lo, dw);

                if (dw.IsZero || w_hi.Exponent - dw.Exponent > Bits) {
                    break;
                }

                z *= sq_invx;
            }

            return (w_hi, w_lo);
        }

        private static partial class Consts {
            public static class LogGamma {
                public static readonly long ApproxThreshold = (long)(7.2152 * Math.Exp(0.17 * Length));
                public static readonly long NearOneThreshold = 3;
                public static int RoundBits { private set; get; } = 0;

                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<N> C0 { private set; get; } = null;
                public static ReadOnlyCollection<MultiPrecision<N>> SterlingTable { private set; get; } = null;

                public static void Initialize() {
                    C0 = Ldexp(Log(Ldexp(PI, 1)), -1);

                    MultiPrecision<N>[] sterling_table = new MultiPrecision<N>[] {
                        Div(1L, 12L),
                        Div(-1L, 360L),
                        Div(1L, 1260L),
                        Div(-1L, 1680L),
                        Div(1L, 1188L),
                        Div(-691L, 360360L),
                        Div(1L, 156L),
                        Div(-3617L, 122400L),
                        Div(43867L, 244188L),
                        Div(-174611L, 125400L),
                        Div(77683L, 5796L),
                        Div(-236364091L, 1506960L),
                        Div(657931L, 300L),
                        Div(-3392780147L, 93960L),
                        Div(1723168255201L, 2492028L),
                        Div(-7709321041217L, 505920L),
                        Div(151628697551L, 396L),
                        Div("-26315271553053477373", 2418179400L),
                        Div(154210205991661L, 444L),
                        Div("-261082718496449122051", 21106800L),
                        Div("1520097643918070802691", 3109932L),
                        Div("-2530297234481911294093", 118680L),
                        Div("25932657025822267968607", 25380L),
                        Div("-5609403368997817686249127547", 104700960L),
                        Div("19802288209643185928499101", 6468L),
                        Div("-61628132164268458257532691681", 324360L),
                        Div("29149963634884862421418123812691", 2283876L),
                        Div("-354198989901889536240773677094747", 382800L),
                        Div("2913228046513104891794716413587449", 40356L),
                        Div("-1215233140483755572040304994079820246041491", 201025024200L),
                        Div("396793078518930920708162576045270521", 732L),
                        Div("-106783830147866529886385444979142647942017", 2056320L),
                        Div("133872729284212332186510857141084758385627191", 25241580L),
                        Div("-4633713579924631067171126424027918014373353", 8040L),
                        Div("43010895638096200108659330496510205957469661721", 646668L),
                        Div("-5827954961669944110438277244641067365282488301844260429", 716195647440L),
                        Div("923038305114085622008920911661422572613197507651", 876L),
                        Div("-1297636253996598563562484002136063152861329885729779", 9000L),
                        Div("31911258890415448330398387349964774884015336567107729499", 1532916L),
                        Div("-4603784299479457646935574969019046849794257872751288919656867", 1453663200L),
                        Div("40902784126466971629833036824055363419700878721225693045893", 80676L),
                        Div("-2024576195935290360231131160111731009989917391198090877281083932477", 23734849320L),
                        Div("15365456265527410548229019707587476657623969457055556552604581", 1020L),
                        Div("-119220589879456137090501038547210167576886388688366240390629080961277", 42741360L),
                        Div("235811455804216559976824670249843016755050989933929423246309043145584507", 435933036L),
                        Div("-56329823835110327303888166458198989115623542770410223609709405514345489", 513240L),
                        Div("25974761842122222757602163391770238382095923727716949392259895857120201", 1116L),
                        Div("-211600449597266513097597728109824233673043954389060234150638733420050668349987259", 41056142400L),
                        Div("1385882870875622359674512602987828685864126443438867092021917576530468307311", 1164L),
                        Div("-94598037819122125295227433069493721872702841533066936133385696204311395415197247711", 329967000L),
                        Div("188471730050641592837824751889190692675989246891336320628176617710991854755098715223", 2621556L),
                        Div("-24579510104910000868238719445859559590091508159867133717469497704834582181965363171081", 1310160L),
                        Div("686300059860705932229279625501762776777949030954371510168505969401544903271437780124017", 134820L),
                        Div("-3469342247847828789552088659323852541399766785760491146870005891371501266319724897592306597338057", 2417419400760L),
                        Div("1529198588096948578449626849344869500105750482682461581336718774151959521253917155995586043503", 3640164L),
                        Div("-378697086022157101907513887812166002063307357027501413778826918240138108073553645052397314793228587", 2968175520L),
                        Div("1144096437861534912279513741402789025037955614688991124252953163943873972854452297416009927292771", 28476L),
                        Div("-10674272985235964721121153725304876443758763036701029111913932611069274397399033388668558606750163871", 814200L),
                        Div("6219713897791747653134697484501440791264514594588122106797617638583296356708584099819860616178255861", 1404L),
                        Div("-51507486535079109061843996857849983274095170353262675213092869167199297474922985358811329367077682677803282070131", 33247494680400L),
                        Div("813666657037091506762830122557225531782340824771477701152611668740175134657384120148083830788492425714171", 1452L),
                        Div("-3092799204330552540347577519598143374857381613332172155469468152559954624336643368181675600331241054486922871", 14760L),
                        Div("5556330281949274850616324408918951380525567307126747246796782304333594286400508981287241419934529638692081513802696639", 68636578500L),
                        Div("-267754707742548082886954405585282394779291459592551740629978686063357792734863530145362663093519862048495908453718017", 8290560L),
                    };

                    SterlingTable = Array.AsReadOnly(sterling_table);

                    Initialized = true;

                    FixRoundBits();
                }

                public static void FixRoundBits() { 
                    MultiPrecision<N> y = Gamma(One);

                    while(RoundBits < Bits / 2) { 
                        if(RoundMantissa(y, Bits - RoundBits) != One) { 
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
