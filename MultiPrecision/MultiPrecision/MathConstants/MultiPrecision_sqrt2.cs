﻿using System.Diagnostics;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> sqrt2 = null;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> Sqrt2 {
            get {
                Consts.sqrt2 ??= GenerateSqrt2();

                return Consts.sqrt2;
            }
        }

        private static MultiPrecision<N> GenerateSqrt2() {
            if (Length < Consts.Sqrt2.Table.Length) {
                return new MultiPrecision<N>(
                    Sign.Plus, exponent: 0,
                    new Mantissa<N>(Consts.Sqrt2.Table[..Length].Reverse().ToArray(), enable_clone: false),
                    round: Consts.Sqrt2.Table[Length] > UIntUtil.UInt32Round
                );
            }

            BigUInt<Plus2<N>> x = 1, y = 0;

            while (x.LeadingZeroCount >= 2) {
                (x, y) = (x + (y << 1), x + y);
            }

            BigUInt<Double<Plus2<N>>> q = BigUInt<Plus2<N>>.Div(x.Convert<Double<Plus2<N>>>(offset: Length + 2), y);
            BigUInt<N> n = BigUInt<Double<Plus2<N>>>.RightRoundShift(q, UIntUtil.UInt32Bits * 2 + 1, enable_clone: false).Convert<N>();

            return new MultiPrecision<N>(Sign.Plus, exponent: 0, new Mantissa<N>(n), round: false);
        }

        private static partial class Consts {
            public static class Sqrt2 {
                public static readonly UInt32[] Table = {
                    0xB504F333u, 0xF9DE6484u, 0x597D89B3u, 0x754ABE9Fu, 0x1D6F60BAu, 0x893BA84Cu, 0xED17AC85u, 0x83339915u, 0x4AFC8304u, 0x3AB8A2C3u, 0xA8B1FE6Fu, 0xDC83DB39u, 0x0F74A85Eu, 0x439C7B4Au, 0x78048736u, 0x3DFA2768u,
                    0xD2202E87u, 0x42AF1F4Eu, 0x53059C60u, 0x11BC337Bu, 0xCAB1BC91u, 0x1688458Au, 0x460ABC72u, 0x2F7C4E33u, 0xC6D5A8A3u, 0x8BB7E9DCu, 0xCB2A6343u, 0x31F3C84Du, 0xF52F120Fu, 0x836E582Eu, 0xEAA4A089u, 0x9040CA4Au,
                    0x81394AB6u, 0xD8FD0EFDu, 0xF4D3A02Cu, 0xEBC93E0Cu, 0x4264DABCu, 0xD528B651u, 0xB8CF341Bu, 0x6F8236C7u, 0x0104DC01u, 0xFE32352Fu, 0x332A5E9Fu, 0x7BDA1EBFu, 0xF6A1BE3Fu, 0xCA221307u, 0xDEA06241u, 0xF7AA81C2u,
                    0xC1FCBDDEu, 0xA2F7DC33u, 0x18838A2Eu, 0xAFF5F3B2u, 0xD24F4A76u, 0x3FACB882u, 0xFDFE170Fu, 0xD3B1F780u, 0xF9ACCE41u, 0x797F2805u, 0xC246785Eu, 0x92957023u, 0x5FCF8F7Bu, 0xCA3EA33Bu, 0x4D7C60A5u, 0xE633E3E1u,
                    0x485F3B49u, 0x4D82BC60u, 0x85AC27DAu, 0x43E4927Au, 0xDB8FC16Eu, 0x69481B04u, 0xEF744894u, 0xC1EA7556u, 0x8775190Fu, 0xBA44FA35u, 0x3F48185Fu, 0x107DBB4Au, 0x77DAC64Cu, 0xC266EB85u, 0x0ED4822Eu, 0x1E899D03u,
                    0x4211EB71u, 0xC181EC80u, 0xDD4ED1A3u, 0xB3423CB6u, 0x2E6ACB96u, 0xE07F9AA0u, 0x61A094A1u, 0x6B203080u, 0xF7B7E36Fu, 0x488A515Au, 0x79246344u, 0xE3005DA0u, 0x545AB582u, 0x0FEAEF37u, 0x06E86336u, 0xA418FF3Fu,
                    0xFFABABF2u, 0x3884C066u, 0xDEAE1342u, 0x42ED2F48u, 0xD9F17902u, 0xDB9392DCu, 0xB8EB050Fu, 0xC4478450u, 0x53708066u, 0x76E1672Du, 0xECC57738u, 0xF2171346u, 0x9BD30397u, 0x91011A30u, 0x9FFE1122u, 0x9A1CF54Bu,
                    0xD4CCDB64u, 0xF1E738FCu, 0xA6B04956u, 0x709055C7u, 0x2A8706AAu, 0x88B44318u, 0xBBC67B01u, 0xA86817F4u, 0x2F94F645u, 0xF2E395C0u, 0x3D7ABB8Du, 0xC12D9850u, 0x73C1BB54u, 0x8E046353u, 0xF87C7991u, 0xD9B140E9u,
                    0x12B44E05u, 0xAD41023Eu, 0xDCC4FB1Du, 0x45327428u, 0xCDE06860u, 0xF1114024u, 0x26CA7A7Cu, 0xDD9EA598u, 0xEA44EBA9u, 0x18DAE319u, 0xE24064B5u, 0xF2A4DFAEu, 0xCB33C5A6u, 0x9626ED43u, 0x3DEC7240u, 0x14FF5464u,
                    0x0B9AB3E1u, 0x5D1E9E74u, 0xEFF05E8Du, 0xA6EBB88Bu, 0xC02BDB4Au, 0xDBF578D8u, 0x2E11646Au, 0xFF75E83Bu, 0xFF64B6DCu, 0x7BBC7E0Eu, 0x15DDE70Du, 0xA4F5FAD7u, 0xA2304414u, 0xAC56E80Eu, 0x53F8DB5Eu, 0x05BF60DEu,
                    0x3705376Du, 0xE33FC2D9u, 0x3A70430Du, 0x9D09BAB8u, 0xD8B2A4C3u, 0x9E908E35u, 0x5734EC00u, 0xF2BCA22Du, 0xE3051F05u, 0x27EC4B47u, 0x5BCA5EE3u, 0x816B4A2Eu, 0xD4A58252u, 0x20655E4Au, 0x1C3E1E93u, 0x7E879DF4u,
                    0x57B182D2u, 0x9D0BB944u, 0x56509FDAu, 0x0364C148u, 0xAEC1DD06u, 0x9AAC6C0Eu, 0xE88ACF4Bu, 0x21F5513Fu, 0x5BBABD10u, 0x294BADB7u, 0xA5444F1Eu, 0x8495D8E3u, 0x42A406E1u, 0x74CCA3D9u, 0xB96F6D02u, 0xF10C97CAu,
                    0xD8DC9328u, 0x95A02619u, 0x8C0EB251u, 0xACD44DB7u, 0xC43279EAu, 0xBA98C9ACu, 0xD51C312Bu, 0xD49D2BD2u, 0xCBBAA3FDu, 0x07B03661u, 0x97754277u, 0xB881E969u, 0x6ABDB60Au, 0x9D2F9BE1u, 0x63BFF149u, 0x46990D94u,
                    0xA3875720u, 0xAC59E76Eu, 0x3256F0F2u, 0x18D09C5Du, 0xBDF3982Du, 0xA37BFF05u, 0xE3AA7F9Fu, 0x60215EE4u, 0x2D992CCBu, 0x291543DCu, 0x321F1E8Bu, 0xC387C612u, 0xDACBC030u, 0x6AADC17Eu, 0xA769F9F0u, 0xAC495ADAu,
                    0x91B0C165u, 0x9418FA26u, 0x513D68D3u, 0x37EAB7EEu, 0x14D3E3D4u, 0x9F213CD5u, 0x096A3050u, 0x6A4A4ADBu, 0x5F26E39Eu, 0xEFCB7740u, 0x54EFE116u, 0xC69C2F51u, 0x766400EBu, 0xA060CC86u, 0x3DF9422Cu, 0x240293DDu,
                    0xC5D4084Eu, 0x93B861FFu, 0xEF090D3Du, 0xF21A7C1Eu, 0x8555FDEFu, 0x298FBA5Du, 0x7EDE16D7u, 0xDF670173u, 0x2DDBBDC7u, 0xDDC66DA5u, 0x6C725CF7u, 0x80F16487u, 0xC034E91Fu, 0x67D86471u, 0x713EB23Eu, 0xD521BB15u,
                    0x325CE3EBu, 0xA29A27EDu, 0xA8B7E2B7u, 0xD0F57354u, 0xAC3A7DA8u, 0x0FB4D8BDu, 0xB35C0EFDu, 0xD08F6A92u, 0x0AEBF71Fu, 0x29FCAC2Cu, 0xA35D9040u, 0xB0BC02CCu, 0xB402DDA7u, 0xCED3CB7Bu, 0x19E4E89Fu, 0xB60B961Du,
                    0x7F1A00D1u, 0x974CEEB5u, 0x123F13CBu, 0x0051FAE0u, 0x4CC8D32Au, 0xA51A8007u, 0xFF04855Fu, 0xDE16C525u, 0x440A2CF6u, 0x893D2A66u, 0x765B1E7Bu, 0x1B9D388Eu, 0x4E7C8AB3u, 0x61CBD941u, 0x3C53F85Du, 0xC5DE228Bu,
                    0x6EE5587Bu, 0x74AABA24u, 0x7C9F75C0u, 0x7B4F9A5Au, 0x92E3B079u, 0x9C077CF6u, 0xDC56777Du, 0x46D9E3E7u, 0x13721552u, 0xCC26A6DCu, 0xAB097629u, 0x45122FE7u, 0xA1ED9BABu, 0xF85050FEu, 0x127F10C7u, 0xA5B18390u,
                    0x48202259u, 0x1979C620u, 0x1978E1D5u, 0xC4B8EEA7u, 0xBCB36ECBu, 0xB0A5D746u, 0xBCBD0424u, 0x58EAE7EDu, 0x89BE850Cu, 0x0D90673Bu, 0xAEA6DD72u, 0x89C9DEABu, 0x9513662Eu, 0x2F05EF37u, 0x2119F7F5u, 0x6D69D046u,
                    0x857108CDu, 0x60652703u, 0xE9A0A841u, 0x7225661Fu, 0x87D9A565u, 0x83E4B7E5u, 0xAE87A5BEu, 0xC4BCF54Eu, 0x473FB1BEu, 0xB30B0C5Cu, 0xBC53AA25u, 0xD817F042u, 0xA8D14692u, 0xEDE6C972u, 0x51942814u, 0xFEDB1663u,
                    0xF2F7164Du, 0x8D182145u, 0x2BA48BB7u, 0x042D14DAu, 0xEF195EB5u, 0x6C22DDF1u, 0x7D0B2906u, 0x755445ADu, 0x859CE4B0u, 0xE616D1B4u, 0x1BA1BB3Fu, 0xCB99AE1Fu, 0xE2CAB894u, 0x1699C61Cu, 0xF9BF094Bu, 0x08F08BA3u,
                    0x7F5BAA67u, 0x59BB0B1Eu, 0x29508516u, 0x91F34515u, 0xE3E9943Eu, 0x8D3938E2u, 0xF7C82A85u, 0x8382A531u, 0x9AA6435Bu, 0x649C7A2Du, 0x74697EC6u, 0x11AB6BE2u, 0xD510D922u, 0xBB1B97F1u, 0x8DEA0FCCu, 0x1B57B3A2u,
                    0x0FE83923u, 0x6C20EF2Eu, 0x57423086u, 0x2AD09676u, 0xB17A65A8u, 0xFF9D1F0Du, 0xEA8C3F10u, 0x4720700Fu, 0x66185131u, 0xE2A69DCDu, 0x9E4D0EBDu, 0x956CE199u, 0x83158EE6u, 0x04CEC8C7u, 0x195DFF54u, 0x879041A2u,
                    0x1110374Fu, 0x816FEBA8u, 0xD239E533u, 0x6A3A5886u, 0x5BE0BB19u, 0x205AAA8Au, 0xCB82005Fu, 0xC3054C4Au, 0x8A58A237u, 0x4672165Du, 0x8D37B86Eu, 0x97EFA469u, 0x817B1AADu, 0xD8E8D285u, 0x028A134Du, 0x3FD20475u,
                    0x76F36103u, 0x8A853420u, 0x5463693Bu, 0xC1F7D479u, 0x89E4DE3Eu, 0x567D8F5Du, 0xE4EC79C3u, 0x349AF47Du, 0xF2489052u, 0xFE88EA32u, 0x46FD69F4u, 0x09C96B9Bu, 0x6F0D14D2u, 0x16C732D3u, 0x9A57A42Eu, 0x94AFD860u,
                    0x8E489D03u, 0x57A04D89u, 0x3C37647Au, 0x6C73C0F9u, 0xF5DC2AF3u, 0x4398C69Eu, 0xB89ED6A3u, 0x69D5ED43u, 0xBCE5DF3Du, 0x009510DDu, 0x4D24FC9Cu, 0xEBC7D367u, 0x66E64384u, 0xF5BCACFAu, 0x92715640u, 0x44BD7BECu,
                    0xE0102ADFu, 0x3391F012u, 0x17BBCA05u, 0x5635F48Fu, 0x86E0DB6Eu, 0x00A6F033u, 0xCBAC03D2u, 0x232B0B3Cu, 0x6BCC93DFu, 0xB1D834B1u, 0xA44133ADu, 0x1EA4A982u, 0x9FB6B3A8u, 0x50E7060Cu, 0xB3382968u, 0xF677F139u,
                    0xC8A5EA12u, 0xBC792BDDu, 0x69F5E0B3u, 0x9B0748F8u, 0x8A414770u, 0xC9565EA4u, 0x6D0DE29Bu, 0x9DB05BFCu, 0x082A8CB8u, 0x0D80A5B4u, 0x5F22889Cu, 0xC1C1C5B7u, 0x63291276u, 0xD7FE3F60u, 0x543E6664u, 0x6DD8A55Fu,
                    0x6617C8D2u, 0x4D4B3CA2u, 0x83A6F713u, 0xAB431784u, 0x2AFFF7A3u, 0xBF6C2369u, 0x79A1E34Du, 0x5159E771u, 0xFDD49055u, 0xE8BB07AAu, 0x249B63D5u, 0x9C95AF56u, 0xB0CC6FC9u, 0x411D4487u, 0x73C3DD6Du, 0x5F153974u,
                    0xCAB7AB5Fu, 0xA6F6F4B8u, 0x5062AD1Fu, 0xA71285C7u, 0x6CF0B4D8u, 0xE9AFCC4Cu, 0xC7A76BC6u, 0xECCD53D1u, 0x1323909Du, 0xCD8F496Au, 0x7BD5878Bu, 0x4698BF02u, 0xC6EA6C30u, 0x8701D7BEu, 0x31E1406Cu, 0xAB0A1E98u,
                    0x535385F4u, 0xD748BE0Bu, 0xD0C40DEAu, 0xD2528E5Du, 0xEA3ED177u, 0x64B46BCDu, 0xF46E5D2Fu, 0x6848D15Cu, 0x227CE658u, 0xB0274A0Eu, 0xDA2EA3DCu, 0x76531813u, 0x7DDB21F0u, 0xCD97BE8Eu, 0xFC439218u, 0xFB9DBA8Bu,
                    0xE3BB8786u, 0x4B9A3A20u, 0x6A7C9C52u, 0xB7DAD89Fu, 0x2D0E1AABu, 0xB25ED9A8u, 0xDEB4E87Du, 0xBF51BFA1u, 0x6B145091u, 0xC729E73Du, 0xC4FDD55Fu, 0x815B1EDEu, 0xCEC5BF20u, 0x948C402Au, 0x215DCB46u, 0x34AAB44Eu,
                    0xF8FCF212u, 0x95602130u, 0x4F4D7EBDu, 0x5DD97A6Bu, 0xA9C87569u, 0x1AD35883u, 0xAA9D5C6Bu, 0x8D2DA2B1u, 0x6E4947E1u, 0xDB7BE21Eu, 0x51597C9Du, 0x745C8DBFu, 0x8331CFEAu, 0xAF7131B9u, 0x3D128B70u, 0x71C13D21u,
                    0x30E54678u, 0x76856425u, 0xABDD8190u, 0x57C6A537u, 0xB0488F59u, 0xC552673Cu, 0x15D27F4Fu, 0xE24C495Cu, 0x83F40151u, 0xD506B47Du, 0xCC12A7B2u, 0xA5742520u, 0x7783C551u, 0x1A62628Eu, 0x7AE2E929u, 0x2E8F1CC2u,
                    0xFB874385u, 0x2671B4ADu, 0xF85856C0u, 0x4B036839u, 0x7869E95Eu, 0x18097421u, 0xBED0978Au, 0x8843B524u, 0x5096C26Eu, 0x08667549u, 0x15133F49u, 0x9591F713u, 0x5095829Fu, 0x6DAD7554u, 0x2B1267E9u, 0xAA00F80Fu,
                    0x650AD878u, 0x907C8D78u, 0xE9DA154Au, 0x06DAED75u, 0xBFA670A9u, 0xDFF46D2Bu, 0xC26D41C3u, 0x5AB7719Fu, 0x4992DB8Au, 0xDCEE5F9Cu, 0x96FCE597u, 0xC25AEBDFu, 0x35D120D5u, 0xABCC04CFu, 0xF54A1BBDu, 0x23117AAFu,
                    0xA46F3A76u, 0x84170ACAu, 0x46166476u, 0xED7973EEu, 0x4CD554E9u, 0x22E8EB86u, 0xC8F576ACu, 0x8A7B8734u, 0x71E3A706u, 0x58860D68u, 0x3E7942A5u, 0xCDAF2D28u, 0x34C256B0u, 0xBC0C4836u, 0x3E26F9D7u, 0x9B563B1Du,
                    0x4A0C4F9Eu, 0x33DA3F5Fu, 0xEF16B40Fu, 0xCA8E1DABu, 0x266ED26Bu, 0x75F45B9Fu, 0x1645589Au, 0xDC14C9E0u, 0xD206D5D5u, 0xE7419B6Au, 0x19837987u, 0x82B7F93Eu, 0xCB047D0Cu, 0xA4856156u, 0x7CEF9E12u, 0x1945C606u,
                    0x97E043D8u, 0x6108D75Au, 0x1666BE0Fu, 0xF7D9A5C0u, 0xC015FEE9u, 0xA44C8F63u, 0xA2B96157u, 0x1708673Cu, 0x64A9F5C4u, 0xA0D5FBA5u, 0xC4639C57u, 0xBC452B8Du, 0x71EDF456u, 0x1AC3EAD1u, 0xD2ABD5EDu, 0x75CEC38Bu,
                    0x38EB6C34u, 0x8EE0BFD2u, 0xC783AD6Au, 0x361CC336u, 0xA906CB25u, 0xA5B6D9B1u, 0x51418F75u, 0xC9B68470u, 0x850288E7u, 0x02385F10u, 0x3EE33259u, 0xEB444428u, 0xC7F0E0C6u, 0x481BCC48u, 0x0AE9EABBu, 0x38A7E701u,
                    0xC8755FD8u, 0x8BEE099Cu, 0x555B4F45u, 0xC3ED09A8u, 0xDE53BAD2u, 0x095D38C8u, 0x8BB4441Cu, 0x8985C7D6u, 0xF95204C9u, 0x7AA06FE0u, 0x1377125Fu, 0x79FA680Bu, 0x4CEE975Cu, 0x0B434398u, 0x30C3A48Du, 0x93EDD393u,
                    0x7D6F284Cu, 0xF557999Cu, 0xC7C0C57Cu, 0x34B8E0F2u, 0xFD8CA549u, 0x3EA375F4u, 0xF9622B3Bu, 0x8DE83EC4u, 0x91721C14u, 0x5AFF819Du, 0xBA308EC1u, 0x5756B742u, 0xE303D3B1u, 0x2DF5D285u, 0xD012D8A0u, 0x90FC4862u,
                    0x9D9457F5u, 0xD3C84CA9u, 0xB7BC4716u, 0x311AF971u, 0x6AA0A42Cu, 0xD8A03CF0u, 0x070EA1D9u, 0x9CCD71B0u, 0xC69E81ABu, 0xEB35462Fu, 0xBC766414u, 0x8A5D16D9u, 0x0A9153CFu, 0xA4020838u, 0x76002190u, 0x0E204C51u,
                    0x58E496B4u, 0x0CE22DB8u, 0x05D1839Cu, 0x54549570u, 0xF957AEC0u, 0x3077B7BCu, 0x8F6383C3u, 0x53ED4574u, 0x23B8407Du, 0x86A8240Bu, 0xD9AEA69Au, 0xC3510BB1u, 0x84CC6DA7u, 0x3ADA80E2u, 0x5F51CA98u, 0x52370B4Eu,
                    0x31B50558u, 0x9C29FB65u, 0x3126EB1Fu, 0xB6E90FE6u, 0x466D0D9Cu, 0x2FD63158u, 0x5E541B42u, 0x11587E23u, 0x96E3C4ECu, 0x1EFA7CFCu, 0x92513006u, 0x3BD22282u, 0x4E238165u, 0x4E6EA85Du, 0x7FA14D86u, 0x62D93083u,
                    0x0D72B337u, 0x88E66913u, 0x8381CCE1u, 0xD8D1D217u, 0xD31F931Du, 0x26BA8148u, 0x516FDDE9u, 0x703B6E14u, 0xF8F52D0Cu, 0x70BC771Au, 0x1C35336Du, 0x3505760Fu, 0x575C2BA5u, 0x09A7309Fu, 0x80001FBAu, 0xD5A6ADA7u,
                    0x3EE4897Cu, 0xC8AB0797u, 0x89A2F8D1u, 0xB4334C05u, 0x095D6F35u, 0x669D92FDu, 0x2A0D3BCCu, 0xA24F1BB1u, 0x88139692u, 0xF083CB99u, 0x8FA856A6u, 0xD591B98Bu, 0x03016683u, 0x6C78EFF8u, 0x667D8168u, 0xCE562235u,
                    0x16EA0FC7u, 0xE1F1AE99u, 0x5931535Eu, 0xED4A19A5u, 0x021EBA6Cu, 0xAEF2C436u, 0x59DC417Cu, 0x2B06E41Eu, 0xFAE3664Eu, 0x77804BF3u, 0x80E563A5u, 0xF31C94CFu, 0xE3C3F18Du, 0xF256E9FAu, 0xF3CD6A3Cu, 0x931D6A42u,
                    0x41CAADC9u, 0x7D61F683u, 0x7FEB9086u, 0xA5ACDE9Eu, 0x5B81D72Cu, 0x2727FDECu, 0x789D2A07u, 0x4619F9D1u, 0x1891B1ECu, 0x784F56DEu, 0x63A01A76u, 0x18C5E622u, 0x745D4349u, 0x662D8EE7u, 0x0EEA3D8Au, 0x2E3EBA86u,
                    0xD46DC4E0u, 0x74840BE8u, 0x4CE31C2Fu, 0x059D8C8Cu, 0x8F409B56u, 0x2898E526u, 0xBCDB6537u, 0x1CA20BCFu, 0x8F51F395u, 0xB39C7E3Fu, 0x126537F7u, 0x39E33A61u, 0xA8F044E8u, 0xC2C83524u, 0x09AE107Cu, 0xDB62A4B5u,
                    0x5AC301DBu, 0xDB165294u, 0xF4F0EE7Bu, 0x906144F0u, 0xE249410Du, 0x5537F357u, 0x1BB8256Du, 0x2F92B09Du, 0xF6746221u, 0x6DEED461u, 0x2D0C18ACu, 0xC269B51Bu, 0x1AA740BFu, 0x0E62DFD1u, 0x7124B447u, 0x7CCC146Au,
                    0xF7B38732u, 0x35D41203u, 0x57D6B1E2u, 0xB234FB38u, 0xB72DA71Au, 0xDCCE7D01u, 0xDFF8099Au, 0x9BD95143u, 0x454ADFE7u, 0xA08592E9u, 0x5FA753E3u, 0x92B8BA0Fu, 0xB6C2ABD8u, 0x8E97F746u, 0x0870D3E4u, 0x876006E6u,
                    0xB3F572EBu, 0x7CF2BCCBu, 0x27338D00u, 0x703CE935u, 0x8B7C42FCu, 0xFFEC03A7u, 0xD5E94596u, 0xEE41B6B8u, 0x54620D6Du, 0xE29A23AAu, 0xD7FCD612u, 0x1A86DB02u, 0x75F47223u, 0x21D264BDu, 0x49EFC842u, 0x493E9233u,
                    0x57B44440u, 0x86376AE9u, 0x58D83CEEu, 0xB87FB2A5u, 0xC2552D43u, 0x5B4DE79Cu, 0x80827C83u, 0xE3B57929u, 0xE7B16401u, 0xE5DFE14Au, 0xE3BC16BAu, 0x15FC9581u, 0x22F822BDu, 0xB1702073u, 0xE58AB0DBu, 0x317873EFu,
                    0x39E32D97u, 0xF5069FB8u, 0x2B441D9Bu, 0xDBC95018u, 0x299DF01Eu, 0xCD0F9ACDu, 0x6C2046F1u, 0xE414CD2Cu, 0x35DF7718u, 0x5AD9F102u, 0xDD09DD21u, 0x5324DA8Eu, 0x70023D4Du, 0xDD846020u, 0x0DAAAC97u, 0x93A7809Au,
                    0x0DE843ECu, 0xE5AC8932u, 0xC8A57DA3u, 0x995A199Du, 0x23EC08E4u, 0xFDE963CEu, 0x55684D48u, 0x3C0EA5CBu, 0xB3B99B90u, 0xF2A2F7F5u, 0x10547F24u, 0x75AD8EA4u, 0xA1A510E5u, 0x01483B55u, 0xAA5DA77Fu, 0x378F4384u,
                    0xB606200Fu, 0x1B14C6E5u, 0xF9D870A7u, 0x82263C1Bu, 0x83F37EE4u, 0xD3A27FC0u, 0xD85B593Du, 0x7B79938Fu, 0x4A00F829u, 0x8B324EA6u, 0x934E9125u, 0x1C029F02u, 0x03DD6A80u, 0x2DE843D4u, 0xB254B6F7u, 0xCBB5CF26u,
                    0x7F7622DFu, 0x65856606u, 0x8C1EBC15u, 0xDAA2F3D9u, 0x09840588u, 0x72ADE54Bu, 0x829040E5u, 0xE215C9C5u, 0x3E80F98Bu, 0x18879AC5u, 0x7B960B24u, 0x2423673Du, 0x34F40EE6u, 0x783BAD0Fu, 0xB610D693u, 0xE5E421EEu,
                    0x4452D730u, 0xA290424Cu, 0x1128A005u, 0x75C98FBAu, 0xBC9A9AFCu, 0xBCD880D9u, 0x70B5F4B7u, 0x78707042u, 0x9D5C5445u, 0x39553327u, 0xC591CA57u, 0x59F6F702u, 0x5D4E5C90u, 0xCA80B6FCu, 0x0D57676Eu, 0xEF63BA9Au,
                    0x31E1324Eu, 0x6AC64349u, 0x064EE498u, 0xE55441CCu, 0x5DA1DE84u, 0xBE1B44F7u, 0xB9EE4796u, 0x4A790CE3u, 0x72B9476Bu, 0x75864FEAu, 0xE29E7A78u, 0xFF5B9D48u, 0x4794118Fu, 0x1A8FCD67u, 0xAF63DE8Cu, 0x8D4E44E8u,
                    0x97D20F4Cu, 0xB046F847u, 0xAC3448CCu, 0x1C7013A6u, 0x555B2617u, 0x64BF9754u, 0xE9DAB2D1u, 0xDA549A4Fu, 0x3AE38754u, 0x0728B04Du, 0x1AD73FC7u, 0x712E2E8Cu, 0x9A452690u, 0xDD3E7528u, 0xBF4708B8u, 0x72C8DAC0u,
                    0x3B7C1154u, 0x63F31C7Au, 0xD83DF5E8u, 0xBBAE5E5Au, 0x79CC5C3Du, 0x73C27194u, 0x686713F6u, 0x8DFBA555u, 0x82F102A1u, 0x03386E5Cu, 0x179F47F0u, 0xFEED22B8u, 0x54F8D9B5u, 0x249705C6u, 0x6C8CF653u, 0x2F0538F6u,
                    0x5AF1CC00u, 0x0E64598Du, 0x6BE47ED3u, 0xB2B2D3DEu, 0xC52B2308u, 0x52CC90DFu, 0xF2491CE3u, 0xB9BC6838u, 0x0148D610u, 0xC921CE59u, 0x66E06769u, 0x2A650429u, 0x9AB3779Bu, 0x5B99CE08u, 0xD6844675u, 0x03E66EC3u,
                    0x43CDD324u, 0xCEA1EAA4u, 0x978EB25Au, 0x4266DF24u, 0x0D2FA51Bu, 0xE7C89B52u, 0x2E0FA39Au, 0x3F3A7DECu, 0x0767E012u, 0x44CCA3BCu, 0x44632C12u, 0x62A68F88u, 0x8AAA7C08u, 0xD27B91B8u, 0xE3FFE4D9u, 0xC44D7410u,
                    0x7CB69DB7u
                };
            }
        }
    }
}
