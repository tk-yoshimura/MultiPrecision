using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Gamma(MultiPrecision<N> x) {
            MultiPrecision<Pow2.N8> xn8 = MultiPrecisionUtil.Convert<Pow2.N8, N>(x);
            MultiPrecision<Pow2.N8> yn8;

            if(x.Sign == Sign.Plus && x.Exponent >= Consts.LogGamma.ExponentThreshold) {
                yn8 = MultiPrecision<Pow2.N8>.Exp(LogGammaSterlingApprox(xn8));
            }
            else if(x.Exponent < Consts.LogGamma.ExponentThreshold){
                MultiPrecision<Pow2.N8> x_int = MultiPrecision<Pow2.N8>.Floor(xn8), x_frac = xn8 - x_int;
                MultiPrecision<Pow2.N8> z = x_frac + MultiPrecision<Pow2.N8>.Integer(Consts.LogGamma.ApproxThreshold);
                MultiPrecision<Pow2.N8> w = MultiPrecision<Pow2.N8>.Exp(LogGammaSterlingApprox(z));

                MultiPrecision<Pow2.N8> s = MultiPrecision<Pow2.N8>.One;
                for(Int64 i = (Int64)x_int; i < Consts.LogGamma.ApproxThreshold; i++) { 
                    z -= MultiPrecision<Pow2.N8>.One;
                    s *= z;
                }

                yn8 = w / s;
            }
            else { 
                throw new NotImplementedException();
            }

            return MultiPrecisionUtil.Convert<N, Pow2.N8>(MultiPrecision<Pow2.N8>.RoundMantissa(yn8, MultiPrecision<Pow2.N8>.Bits - 12));
        }

        internal static MultiPrecision<Pow2.N8> LogGammaSterlingApprox(MultiPrecision<Pow2.N8> x) {
            if (!Consts.LogGamma.Initialized) {
                Consts.LogGamma.Initialize();
            }

#if DEBUG
            Debug<ArithmeticException>.Assert(x.Sign == Sign.Plus && x.Exponent >= Consts.LogGamma.ExponentThreshold);
#endif

            MultiPrecision<Pow2.N8> w = (x + MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.MinusOne, -1))
                                       * MultiPrecision<Pow2.N8>.Log(x) - x + Consts.LogGamma.C0;
            MultiPrecision<Pow2.N8> invx = MultiPrecision<Pow2.N8>.One / x, z = invx, sq_invx = invx * invx;

            foreach (MultiPrecision<Pow2.N8> s in Consts.LogGamma.SterlingTable) {
                MultiPrecision<Pow2.N8> dw = s * z;
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
                public const int ExponentThreshold = 5, ApproxThreshold = 1 << ExponentThreshold;

                public static bool Initialized { private set; get; } = false;
                public static MultiPrecision<Pow2.N8> C0 { private set; get; } = null;
                public static ReadOnlyCollection<MultiPrecision<Pow2.N8>> SterlingTable { private set; get; } = null;

                public static void Initialize() {
                    C0 = MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.Log(MultiPrecision<Pow2.N8>.Ldexp(MultiPrecision<Pow2.N8>.PI, 1)), -1);

                    MultiPrecision<Pow2.N8>[] sterling_table = new MultiPrecision<Pow2.N8>[] {
                        MultiPrecision<Pow2.N8>.Div(1L, 12L),
                        MultiPrecision<Pow2.N8>.Div(-1L, 360L),
                        MultiPrecision<Pow2.N8>.Div(1L, 1260L),
                        MultiPrecision<Pow2.N8>.Div(-1L, 1680L),
                        MultiPrecision<Pow2.N8>.Div(1L, 1188L),
                        MultiPrecision<Pow2.N8>.Div(-691L, 360360L),
                        MultiPrecision<Pow2.N8>.Div(1L, 156L),
                        MultiPrecision<Pow2.N8>.Div(-3617L, 122400L),
                        MultiPrecision<Pow2.N8>.Div(43867L, 244188L),
                        MultiPrecision<Pow2.N8>.Div(-174611L, 125400L),
                        MultiPrecision<Pow2.N8>.Div(77683L, 5796L),
                        MultiPrecision<Pow2.N8>.Div(-236364091L, 1506960L),
                        MultiPrecision<Pow2.N8>.Div(657931L, 300L),
                        MultiPrecision<Pow2.N8>.Div(-3392780147L, 93960L),
                        MultiPrecision<Pow2.N8>.Div(1723168255201L, 2492028L),
                        MultiPrecision<Pow2.N8>.Div(-7709321041217L, 505920L),
                        MultiPrecision<Pow2.N8>.Div(151628697551L, 396L),
                        MultiPrecision<Pow2.N8>.Div("-26315271553053477373", 2418179400L),
                        MultiPrecision<Pow2.N8>.Div(154210205991661L, 444L),
                        MultiPrecision<Pow2.N8>.Div("-261082718496449122051", 21106800L),
                        MultiPrecision<Pow2.N8>.Div("1520097643918070802691", 3109932L),
                        MultiPrecision<Pow2.N8>.Div("-2530297234481911294093", 118680L),
                        MultiPrecision<Pow2.N8>.Div("25932657025822267968607", 25380L),
                        MultiPrecision<Pow2.N8>.Div("-5609403368997817686249127547", 104700960L),
                        MultiPrecision<Pow2.N8>.Div("19802288209643185928499101", 6468L),
                        MultiPrecision<Pow2.N8>.Div("-61628132164268458257532691681", 324360L),
                        MultiPrecision<Pow2.N8>.Div("29149963634884862421418123812691", 2283876L),
                        MultiPrecision<Pow2.N8>.Div("-354198989901889536240773677094747", 382800L),
                        MultiPrecision<Pow2.N8>.Div("2913228046513104891794716413587449", 40356L),
                        MultiPrecision<Pow2.N8>.Div("-1215233140483755572040304994079820246041491", 201025024200L),
                        MultiPrecision<Pow2.N8>.Div("396793078518930920708162576045270521", 732L),
                        MultiPrecision<Pow2.N8>.Div("-106783830147866529886385444979142647942017", 2056320L),
                        MultiPrecision<Pow2.N8>.Div("133872729284212332186510857141084758385627191", 25241580L),
                        MultiPrecision<Pow2.N8>.Div("-4633713579924631067171126424027918014373353", 8040L),
                        MultiPrecision<Pow2.N8>.Div("43010895638096200108659330496510205957469661721", 646668L),
                        MultiPrecision<Pow2.N8>.Div("-5827954961669944110438277244641067365282488301844260429", 716195647440L),
                        MultiPrecision<Pow2.N8>.Div("923038305114085622008920911661422572613197507651", 876L),
                        MultiPrecision<Pow2.N8>.Div("-1297636253996598563562484002136063152861329885729779", 9000L),
                        MultiPrecision<Pow2.N8>.Div("31911258890415448330398387349964774884015336567107729499", 1532916L),
                        MultiPrecision<Pow2.N8>.Div("-4603784299479457646935574969019046849794257872751288919656867", 1453663200L),
                        MultiPrecision<Pow2.N8>.Div("40902784126466971629833036824055363419700878721225693045893", 80676L),
                        MultiPrecision<Pow2.N8>.Div("-2024576195935290360231131160111731009989917391198090877281083932477", 23734849320L),
                        MultiPrecision<Pow2.N8>.Div("15365456265527410548229019707587476657623969457055556552604581", 1020L),
                        MultiPrecision<Pow2.N8>.Div("-119220589879456137090501038547210167576886388688366240390629080961277", 42741360L),
                        MultiPrecision<Pow2.N8>.Div("235811455804216559976824670249843016755050989933929423246309043145584507", 435933036L),
                        MultiPrecision<Pow2.N8>.Div("-56329823835110327303888166458198989115623542770410223609709405514345489", 513240L),
                        MultiPrecision<Pow2.N8>.Div("25974761842122222757602163391770238382095923727716949392259895857120201", 1116L),
                        MultiPrecision<Pow2.N8>.Div("-211600449597266513097597728109824233673043954389060234150638733420050668349987259", 41056142400L),
                        MultiPrecision<Pow2.N8>.Div("1385882870875622359674512602987828685864126443438867092021917576530468307311", 1164L),
                        MultiPrecision<Pow2.N8>.Div("-94598037819122125295227433069493721872702841533066936133385696204311395415197247711", 329967000L),
                        MultiPrecision<Pow2.N8>.Div("188471730050641592837824751889190692675989246891336320628176617710991854755098715223", 2621556L),
                        MultiPrecision<Pow2.N8>.Div("-24579510104910000868238719445859559590091508159867133717469497704834582181965363171081", 1310160L),
                        MultiPrecision<Pow2.N8>.Div("686300059860705932229279625501762776777949030954371510168505969401544903271437780124017", 134820L),
                        MultiPrecision<Pow2.N8>.Div("-3469342247847828789552088659323852541399766785760491146870005891371501266319724897592306597338057", 2417419400760L),
                        MultiPrecision<Pow2.N8>.Div("1529198588096948578449626849344869500105750482682461581336718774151959521253917155995586043503", 3640164L),
                        MultiPrecision<Pow2.N8>.Div("-378697086022157101907513887812166002063307357027501413778826918240138108073553645052397314793228587", 2968175520L),
                        MultiPrecision<Pow2.N8>.Div("1144096437861534912279513741402789025037955614688991124252953163943873972854452297416009927292771", 28476L),
                        MultiPrecision<Pow2.N8>.Div("-10674272985235964721121153725304876443758763036701029111913932611069274397399033388668558606750163871", 814200L),
                        MultiPrecision<Pow2.N8>.Div("6219713897791747653134697484501440791264514594588122106797617638583296356708584099819860616178255861", 1404L),
                        MultiPrecision<Pow2.N8>.Div("-51507486535079109061843996857849983274095170353262675213092869167199297474922985358811329367077682677803282070131", 33247494680400L),
                        MultiPrecision<Pow2.N8>.Div("813666657037091506762830122557225531782340824771477701152611668740175134657384120148083830788492425714171", 1452L),
                        MultiPrecision<Pow2.N8>.Div("-3092799204330552540347577519598143374857381613332172155469468152559954624336643368181675600331241054486922871", 14760L),
                        MultiPrecision<Pow2.N8>.Div("5556330281949274850616324408918951380525567307126747246796782304333594286400508981287241419934529638692081513802696639", 68636578500L),
                        MultiPrecision<Pow2.N8>.Div("-267754707742548082886954405585282394779291459592551740629978686063357792734863530145362663093519862048495908453718017", 8290560L),
                    };

                    SterlingTable = Array.AsReadOnly(sterling_table);

                    Initialized = true;
                }
            }
        }
    }
}
