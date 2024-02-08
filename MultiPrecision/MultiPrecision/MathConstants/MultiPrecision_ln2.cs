﻿using System.Diagnostics;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> ln_2 = null, lb_e = null;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> Ln2 {
            get {
                Consts.ln_2 ??= GenerateLn2();

                return Consts.ln_2;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> LbE {
            get {
                Consts.lb_e ??= GenerateLbE();

                return Consts.lb_e;
            }
        }

        private static MultiPrecision<N> GenerateLn2() {
            if (Length < Consts.Ln2.Table.Length) {
                return new MultiPrecision<N>(
                    Sign.Plus, exponent: -1,
                    new Mantissa<N>(Consts.Ln2.Table[..Length].Reverse().ToArray(), enable_clone: false),
                    round: Consts.Ln2.Table[Length] > UIntUtil.UInt32Round
                );
            }

            return (1 / MultiPrecision<Plus1<N>>.Log2(MultiPrecision<Plus1<N>>.E)).Convert<N>();
        }

        private static MultiPrecision<N> GenerateLbE() {
            if (Length < Consts.LbE.Table.Length) {
                return new MultiPrecision<N>(
                    Sign.Plus, exponent: 0,
                    new Mantissa<N>(Consts.LbE.Table[..Length].Reverse().ToArray(), enable_clone: false),
                    round: Consts.LbE.Table[Length] > UIntUtil.UInt32Round
                );
            }

            return Log2(E);
        }

        private static partial class Consts {
            public static class Ln2 {
                public static readonly UInt32[] Table = [
                    0xB17217F7u, 0xD1CF79ABu, 0xC9E3B398u, 0x03F2F6AFu, 0x40F34326u, 0x7298B62Du, 0x8A0D175Bu, 0x8BAAFA2Bu, 0xE7B87620u, 0x6DEBAC98u, 0x559552FBu, 0x4AFA1B10u, 0xED2EAE35u, 0xC1382144u, 0x27573B29u, 0x1169B825u,
                    0x3E96CA16u, 0x224AE8C5u, 0x1ACBDA11u, 0x317C387Eu, 0xB9EA9BC3u, 0xB136603Bu, 0x256FA0ECu, 0x7657F74Bu, 0x72CE87B1u, 0x9D6548CAu, 0xF5DFA6BDu, 0x38303248u, 0x655FA187u, 0x2F20E3A2u, 0xDA2D97C5u, 0x0F3FD5C6u,
                    0x07F4CA11u, 0xFB5BFB90u, 0x610D30F8u, 0x8FE551A2u, 0xEE569D6Du, 0xFC1EFA15u, 0x7D2E23DEu, 0x1400B396u, 0x17460775u, 0xDB8990E5u, 0xC943E732u, 0xB479CD33u, 0xCCCC4E65u, 0x9393514Cu, 0x4C1A1E0Bu, 0xD1D6095Du,
                    0x25669B33u, 0x3564A337u, 0x6A9C7F8Au, 0x5E148E82u, 0x074DB601u, 0x5CFE7AA3u, 0x0C480A54u, 0x17350D2Cu, 0x955D5179u, 0xB1E17B9Du, 0xAE313CDBu, 0x6C606CB1u, 0x078F735Du, 0x1B2DB31Bu, 0x5F50B518u, 0x5064C18Bu,
                    0x4D162DB3u, 0xB365853Du, 0x7598A195u, 0x1AE273EEu, 0x5570B6C6u, 0x8F969834u, 0x96D4E6D3u, 0x30AF889Bu, 0x44A02554u, 0x731CDC8Eu, 0xA17293D1u, 0x228A4EF9u, 0x8D6F5177u, 0xFBCF0755u, 0x268A5C1Fu, 0x9538B982u,
                    0x61AFFD44u, 0x6B1CA3CFu, 0x5E9222B8u, 0x8C66D3C5u, 0x422183EDu, 0xC9942109u, 0x0BBB16FAu, 0xF3D949F2u, 0x36E02B20u, 0xCEE886B9u, 0x05C128D5u, 0x3D0BD2F9u, 0x62136319u, 0x6AF50302u, 0x0060E499u, 0x08391A0Cu,
                    0x57339BA2u, 0xBEBA7D05u, 0x2AC5B61Cu, 0xC4E9207Cu, 0xEF2F0CE2u, 0xD7373958u, 0xD7622658u, 0x901E646Au, 0x95184460u, 0xDC4E7487u, 0x156E0C29u, 0x2413D5E3u, 0x61C1696Du, 0xD24AAEBDu, 0x473826FDu, 0xA0C238B9u,
                    0x0AB111BBu, 0xBD67C724u, 0x972CD18Bu, 0xFBBD9D42u, 0x6C472096u, 0xE76115C0u, 0x5F6F7CEBu, 0xAC9F45AEu, 0xCECB72F1u, 0x9C38339Du, 0x8F682625u, 0x0DEA891Eu, 0xF07AFFF3u, 0xA892374Eu, 0x175EB4AFu, 0xC8DAADD8u,
                    0x85DB6AB0u, 0x3A49BD0Du, 0xC0B1B31Du, 0x8A0E23FAu, 0xC5E5767Du, 0xF95884E0u, 0x6425A415u, 0x26FAC51Cu, 0x3EA8449Fu, 0xE8F70EDDu, 0x062B1A63u, 0xA6C4C60Cu, 0x52AB3316u, 0x1E238438u, 0x897A39CEu, 0x78B63C9Fu,
                    0x364F5B8Au, 0xEF22EC2Fu, 0xEE6E0850u, 0xECA42D06u, 0xFB0C75DFu, 0x5497E00Cu, 0x554B03D7u, 0xD2874A00u, 0x0CA8F58Du, 0x94F0341Cu, 0xBE2EC921u, 0x56C9F949u, 0xDB4A9316u, 0xF281501Eu, 0x53DAEC3Fu, 0x64F1B783u,
                    0x154C6032u, 0x0E2FF793u, 0x33CE3573u, 0xFACC5FDCu, 0xF1178590u, 0x3155BBD9u, 0x0F023B22u, 0x0224FCD8u, 0x471BF4F4u, 0x45F0A88Au, 0x14F0CD97u, 0x6EA354BBu, 0x20CDB5CCu, 0xB3DB2392u, 0x88D58655u, 0x4E2A0E8Au,
                    0x6FE51A8Cu, 0xFAA72EF2u, 0xAD8A43DCu, 0x4212B210u, 0xB779DFE4u, 0x9D7307CCu, 0x846532E4u, 0xB9694EDAu, 0xD162AF05u, 0x3B1751F3u, 0xA3D091F6u, 0x56658154u, 0x12B5E8C2u, 0x02461069u, 0xAC14B958u, 0x784934B8u,
                    0xD6CCE1DAu, 0xA5053701u, 0x1AA4FB42u, 0xB9A3DEF4u, 0x1BDA1F85u, 0xEF6FDBF2u, 0xF2D89D2Au, 0x4B183527u, 0x8FD94057u, 0x89F45681u, 0x2B552879u, 0xA6168695u, 0xC12963B0u, 0xFF01EAABu, 0x73E5B5C1u, 0x585318E7u,
                    0x624F14A5u, 0x1A4A026Bu, 0x68082920u, 0x57FD99B6u, 0x6DC085A9u, 0x8AC8D8CAu, 0xF9EEEEA9u, 0x8A2400CAu, 0xC95F260Fu, 0xD10036F9u, 0xF91096ACu, 0x3195220Au, 0x1A356B2Au, 0x73B7EAADu, 0xAF6D6058u, 0x71EF7AFBu,
                    0x80BC4234u, 0x33562E94u, 0xB12DFAB4u, 0x14451579u, 0xDF59EAE0u, 0x51707062u, 0x4012A829u, 0x62C59CABu, 0x347F8304u, 0xD889659Eu, 0x5A9139DBu, 0x14EFCC30u, 0x852BE3E8u, 0xFC99F14Du, 0x1D822DD6u, 0xE2F76797u,
                    0xE30219C8u, 0xAA9CE884u, 0x8A886EB3u, 0xC87B7295u, 0x988012E8u, 0x314186EDu, 0xBAF86856u, 0xCCD3C3B6u, 0xEE94E62Fu, 0x110A6783u, 0xD2AAE89Cu, 0xCC3B76FCu, 0x435A0CE1u, 0x34C2838Fu, 0xD571EC6Cu, 0x1366A992u,
                    0xCBB9AC40u, 0x7DDB6C13u, 0xA4B8D1ECu, 0xF7567EB0u, 0x971CC90Bu, 0x5518569Fu, 0x144E67EBu, 0xE9B42698u, 0xFEA79D89u, 0xD5C5ED40u, 0xAC5E3701u, 0xD7D77253u, 0x77CF0656u, 0x907FB9B1u, 0xB16EA891u, 0x1AFBF1AEu,
                    0x5A66203Du, 0x62FD1E70u, 0x93435B9Cu, 0x277736A7u, 0x0FA8601Cu, 0xF6868A05u, 0x5B223867u, 0x7A2BFBBDu, 0x843BFA18u, 0x73F0C446u, 0xB01B2AE0u, 0xE98E0E15u, 0x27A900B1u, 0xAF5E75F8u, 0x7C7CD17Au, 0xF804D933u,
                    0xD6F7E1B9u, 0xE1903D71u, 0xC7BBA028u, 0x11376090u, 0xDD617335u, 0xDFDD424Fu, 0x2B661CD8u, 0x5063034Eu, 0x341B06E2u, 0x11977B07u, 0x5A8B7808u, 0xDF43BD8Eu, 0xEF1FD967u, 0x8CC0B5E9u, 0xF60A3EB8u, 0x1747F87Eu,
                    0x5709468Du, 0x78EBD2DAu, 0x0116E6B6u, 0x5AEB3BE7u, 0x7EE236FDu, 0xC33BC8E7u, 0xDF1FFC2Eu, 0x2288F8CAu, 0x9AEE5B60u, 0x899D5224u, 0x7F4CE0B6u, 0xFFD8B947u, 0x817642C6u, 0xF224F70Fu, 0x88255E1Cu, 0x0839CBCBu,
                    0xDD826C5Du, 0x3770067Eu, 0x3E08EB67u, 0x9C1DB7FFu, 0xC1684223u, 0xD7C6E681u, 0x15A28322u, 0x91FF14FCu, 0x9DB25DA9u, 0xBECCB4C2u, 0x7533E747u, 0xD9047F55u, 0xC901AFF6u, 0x4A09C45Bu, 0xE2C73E6Fu, 0xDC29696Eu,
                    0x2268A5E0u, 0xC92F5181u, 0x95DBBD18u, 0xC4EF702Du, 0x6C550131u, 0x6DED1F3Eu, 0x5585F6DDu, 0xD13A0595u, 0x4694870Eu, 0x71C194CBu, 0xC43962E1u, 0x12EE6799u, 0x0C363121u, 0x77D1B7B1u, 0xA769F887u, 0x324B53DEu,
                    0x1A4C99C9u, 0xA092C106u, 0xC3CE35B9u, 0x48CB5253u, 0xD66AE99Du, 0x7EFBEA3Du, 0x9E7F5862u, 0x17AEB406u, 0x8AEEDE38u, 0x4FA88B87u, 0xFD036381u, 0xA74342DAu, 0x7A6ED265u, 0x8ADEA5EEu, 0x414CBC1Bu, 0xB5F505D2u,
                    0x20A7043Au, 0x0742281Bu, 0x03F81FC6u, 0x473930FFu, 0x7C847E90u, 0xA373F5A9u, 0xA527BD44u, 0x47ADB7D1u, 0x9C45E271u, 0x5D3D15C2u, 0x98570B15u, 0x2CEC2FBDu, 0x582DC46Au, 0xE726E49Eu, 0x3C7C334Bu, 0x5EFDB4C6u,
                    0xFA4507BAu, 0x0026761Au, 0x680B77F6u, 0xCAB5ED37u, 0x6D105839u, 0xF0319222u, 0xBE8FDB67u, 0x92689FF1u, 0x58A59908u, 0xF1670776u, 0x738AF20Fu, 0x3CCF6D58u, 0x2BA8C612u, 0x0DA6A649u, 0xA55783CEu, 0x134A42BBu,
                    0xAFAE7DACu, 0x1D5387BFu, 0x018D2B31u, 0x01223478u, 0x5928DCDBu, 0x041E9325u, 0xB4AF621Au, 0x8A555E27u, 0x6A3141F6u, 0x01E3BD2Bu, 0xCB9856B2u, 0x9FADC8ECu, 0x1B2F45A1u, 0x6299E5ACu, 0xFA4D768Eu, 0xE2FE5271u,
                    0x57FFCD4Au, 0x70AA1EDFu, 0x6410110Bu, 0xD615E257u, 0xA203F76Eu, 0xD0E79070u, 0xD20A751Fu, 0xCC43A7C4u, 0x1179057Fu, 0xC450DCA7u, 0xA9024784u, 0xBE09F609u, 0xEEC90443u, 0x850ACA3Bu, 0xED5F3D14u, 0xAF707BB0u,
                    0x95AF931Cu, 0xF8317D02u, 0xA3734AD2u, 0xCC114BFFu, 0x03D0604Au, 0x998E0195u, 0x0B82E686u, 0xCE94A838u, 0xEE116896u, 0x88D0FD96u, 0x7D06BDA5u, 0xB72AA613u, 0xFA220BBDu, 0xEC995C53u, 0xEFADE356u, 0xED90B7A9u,
                    0x43FD280Fu, 0xCE7DF51Du, 0x607B1B85u, 0x535B4CE9u, 0x343F671Du, 0x4877FC71u, 0x94CCC4CDu, 0xFEFC08EAu, 0x6DA24640u, 0xBBE2BA0Fu, 0x2B9F73ADu, 0x3DDA7560u, 0x932247FFu, 0x56563467u, 0xF32018E1u, 0x4FE46C75u,
                    0xA44A4D5Du, 0x7E8DAB6Bu, 0xB6DBF8BCu, 0x4E6D07F9u, 0xE7D993D8u, 0x99572381u, 0x57FBEFFAu, 0x0345B5AAu, 0xB3D1FEEDu, 0xCB09F14Cu, 0x40257B2Bu, 0x7F308EB7u, 0x99AB4C95u, 0xD1A1ED94u, 0xBDDA0B62u, 0x5F39C4C8u,
                    0x6FC5EE57u, 0x48041D64u, 0x7AEFCB7Au, 0xF226FE13u, 0x82997D87u, 0xB22199E9u, 0x12C3E3F5u, 0x3DB5A720u, 0x8E0352BBu, 0x66B37977u, 0x8CEE2EC9u, 0xAF2D8731u, 0x119F38BDu, 0x9D90D43Eu, 0x054D8B71u, 0xFC0C0D10u,
                    0x990E84C3u, 0x3202ECC9u, 0xFED51F62u, 0x6393CCC5u, 0x66840367u, 0xAE0B74CBu, 0x4DBFE4CEu, 0xBD7FE74Bu, 0x8DE2AA91u, 0x76BDB500u, 0x613EE2ACu, 0x34E4E9E7u, 0x87B4C29Eu, 0x7FB208A9u, 0xDD9C92F0u, 0x02CA30F8u,
                    0x33F56093u, 0xC1848BDEu, 0x22A0BDADu, 0x10222180u, 0x95F2B863u, 0xB06CCA87u, 0x9F0BC8D7u, 0x73A5281Eu, 0xEFA81177u, 0x4A6664B9u, 0xF1B12A25u, 0xC54EB9D9u, 0x07735D44u, 0x1C77E842u, 0x7CDA02B7u, 0x508D4549u,
                    0x5B4EC1B2u, 0xE6B85E8Du, 0x751A8F7Du, 0xD271EE0Eu, 0xC8DCE8B6u, 0x3596533Fu, 0xC241FE15u, 0xA9CFA5ABu, 0xD5735AEDu, 0x99937F74u, 0x09369A1Bu, 0x26321BD1u, 0x350ECBF2u, 0x1C454C1Du, 0xA119C712u, 0x16D73E8Eu,
                    0xC5DD5C59u, 0x279D16F2u, 0xD3FF8447u, 0xCD6ACD1Fu, 0x8D6B5471u, 0x9549A7C2u, 0xBB38AEF7u, 0xE67C2008u, 0xBCD78924u, 0xBA5575DBu, 0xAE31F654u, 0x9F337C9Cu, 0x24490704u, 0xA3440C51u, 0x3EF2B9C7u, 0x30C6F64Cu,
                    0x031C913Bu, 0x8186C5EEu, 0x43A2627Eu, 0xF452A943u, 0x58B26965u, 0xA848D412u, 0x9AF72ED3u, 0xB4EC0D1Au, 0x20606200u, 0x67862B47u, 0xDEF36EA6u, 0x6BC481A4u, 0x3268778Cu, 0x0E176D3Au, 0x2F7C12A6u, 0x9DD65431u,
                    0x4AFCA207u, 0xA0C1F900u, 0xA2C7ACB9u, 0xAA695870u, 0x193103D8u, 0x87B8D0D1u, 0x6BFF6622u, 0x9B1D5C71u, 0xE40BB196u, 0xE5A5A87Au, 0xEA56001Fu, 0x36AA98B3u, 0x4A9353F8u, 0x875CA66Fu, 0xCF6E80EBu, 0xEB0C22FCu,
                    0x511040D7u, 0x66F38A91u, 0xD18EB2C5u, 0xF02E9AAAu, 0xAED8DAF4u, 0x9BDC90A7u, 0xC8117AA4u, 0xACC8A602u, 0x89CA3CB5u, 0xB54457E1u, 0x391C8F7Du, 0x31054E5Bu, 0x58F50AC7u, 0x6EC56620u, 0xCCA64897u, 0x2F6DC08Eu,
                    0x00CE59D5u, 0xDB3A8908u, 0x5024A9B4u, 0xBF7FB013u, 0xECA92E5Cu, 0x1C8368F1u, 0xD8C13D87u, 0xB377E6F3u, 0xCA18060Au, 0xFE2542BEu, 0x41C483BEu, 0x5DCA1A3Fu, 0xC10CC465u, 0xDA627404u, 0x38279676u, 0x04F409A1u,
                    0xFBAEB8A5u, 0xE4F1FE54u, 0x41D28643u, 0xFD2D6D64u, 0xDA55B746u, 0xA4F3D218u, 0x7EFADE66u, 0xC0BF8EDBu, 0x271E34D7u, 0x1AD43D43u, 0xE5BACA7Fu, 0x769E2BA1u, 0xC272A49Du, 0xE81910E8u, 0xB602E967u, 0x1BE82031u,
                    0x491D74FBu, 0x17051BDBu, 0x411A86C5u, 0x897C133Au, 0x47485597u, 0xEEA69D95u, 0xA1268BFEu, 0x491409E6u, 0xE3F9D2AAu, 0x2DFB5570u, 0x097EE1D4u, 0x2C07EE5Au, 0x2D673DD9u, 0xACAD6B02u, 0x69394056u, 0xC80DC2B5u,
                    0xB6C239B3u, 0x55A0BFAAu, 0xA4D8B12Bu, 0x86F427B5u, 0xBFC159F1u, 0x2D2732FBu, 0x9D536B06u, 0x2E904D70u, 0xA78AE88Fu, 0x142D2757u, 0xADEF783Bu, 0x74A76C7Eu, 0x2AD84562u, 0xA478E08Au, 0x978DCC2Cu, 0x0EE3B60Du,
                    0x5AB43FEDu, 0x286D8AC9u, 0x92C9918Eu, 0x5F2322E2u, 0x2EA48D7Du, 0xE9504546u, 0xFE523CF5u, 0x9F15D57Eu, 0x0C6A27C0u, 0x09CBA92Fu, 0xDD34515Au, 0xD0219648u, 0x651FEC89u, 0x51EF24CDu, 0x15F0768Du, 0x8E8BFC8Bu,
                    0xFDB1CC86u, 0x754BD032u, 0x3DE06922u, 0xB8AABF3Fu, 0xE7582036u, 0x0A5429D9u, 0xF25BD5FCu, 0x8E8EAC20u, 0x1F375627u, 0xAA36B8F2u, 0x7DE0CA84u, 0xDC4336E0u, 0xD447DFA2u, 0x110DC089u, 0x06A14E9Du, 0x85949AF3u,
                    0x598F5E65u, 0x5E163CABu, 0x1591DAD6u, 0xF6E8647Cu, 0xB9C1D83Fu, 0xBBF59224u, 0xA0DD883Cu, 0x2B04532Bu, 0xA544AB77u, 0x907C6A64u, 0xE0B3D670u, 0xAC712338u, 0x41A34334u, 0x23980558u, 0x59B97D52u, 0x9A9860DDu,
                    0xF109CF8Bu, 0x7CAA505Du, 0x4DB83C22u, 0xA3891B37u, 0x57F5B437u, 0x86250EDCu, 0x717FF1C7u, 0xEDD3C810u, 0x3FA00175u, 0x3FDE36B8u, 0x34E14D3Cu, 0xC3B34F47u, 0x23245A9Eu, 0xDB3AC225u, 0xE3ED7F4Bu, 0xB3ED1887u,
                    0xD9C4EF8Fu, 0xCD34F8B4u, 0xA6F972C2u, 0xF06BA8D4u, 0x921D51FFu, 0xB02831A6u, 0x313260EDu, 0xA6004216u, 0xE17946DFu, 0xAFE1C52Cu, 0xBA834173u, 0x142B53FDu, 0x25789B0Bu, 0x07450D41u, 0x77F4B304u, 0x961F5822u,
                    0xE8B45DB0u, 0x975F0BDFu, 0x4E5C8F91u, 0xD544C989u, 0x814A7E88u, 0x6A72643Bu, 0xD5140510u, 0xDCB924C1u, 0xC93AEFE6u, 0x0F539C86u, 0x4F95518Eu, 0x85C4710Cu, 0x085E8F77u, 0x8C24D2CEu, 0x70B69E4Cu, 0x61B715D8u,
                    0x97BB3063u, 0xFAB3122Du, 0x0BF7E148u, 0x8B57EE58u, 0x22A55D8Du, 0x0AFA46CDu, 0xEEC391D6u, 0x881A0DC4u, 0xD3EA6523u, 0xBA5B9803u, 0x9CA963A1u, 0xE0F72CB6u, 0x259A13D6u, 0xE3E52EC8u, 0xB7E418B7u, 0xD1E4FCF1u,
                    0xCBE3B949u, 0x4FA8DC54u, 0xE1C496DBu, 0x3214339Bu, 0xB3B04CE2u, 0x7377EAAAu, 0xCFA12105u, 0x1D16DD79u, 0x852674BAu, 0x8AAA5703u, 0xA92F2A86u, 0xC8F57E20u, 0xC72517C5u, 0xA999DF3Du, 0xA59D3DA4u, 0xCBEDC47Bu,
                    0x5798F4ABu, 0xE75B7CE9u, 0x81B0D4FCu, 0x5B51A7E5u, 0xA16967E3u, 0xEC50B037u, 0x7AD88153u, 0xF9197EA0u, 0xB96E8586u, 0xC8BC30F8u, 0x8236E76Bu, 0xB77E2882u, 0x2C6A0030u, 0x94FB7003u, 0xADA1B6DBu, 0xB54C2FC7u,
                    0x9F611BF4u, 0x30F88482u, 0xA4485160u, 0x43B72EA3u, 0x257FFF0Cu, 0x7B9D8E6Bu, 0xCE016D58u, 0x76F7FD61u, 0x33F89ACCu, 0x5FBF2E81u, 0xD6A65696u, 0x78F423C0u, 0x5F0FAFDFu, 0x232AA786u, 0xF52F3D0Au, 0x435F2318u,
                    0x6F9FF891u, 0x794BCE3Fu, 0x9D928E1Eu, 0x810200C7u, 0xF46C5B7Au, 0xC56182BEu, 0xCBDBD743u, 0xFAB4E700u, 0xE2F455AEu, 0xF8A032E9u, 0x2951AD98u, 0xBB991CDEu, 0x1A09B3CDu, 0x4F702E93u, 0xA3657199u, 0xCD1F0086u,
                    0xDC8B1CCCu, 0x4F2A8091u, 0xEA1DF0FDu, 0x889FDF05u, 0xD2C0FD18u, 0xE61E782Du, 0xF072BB8Bu, 0xE511A7E8u, 0x412B533Au, 0x9AD63E28u, 0xD3DEC760u, 0xECC1EEB7u, 0x56F936C3u, 0x63BE7135u, 0x01EC05A1u, 0x334597AFu,
                    0x6496E00Eu, 0x3648B6BFu, 0x940BEDFAu, 0x10E693FBu, 0x8955A344u, 0x869FD9F1u, 0x05A457EBu, 0xA920581Fu, 0x48B47AD4u, 0x18DF5268u, 0x1771FCB6u, 0x34AFEDA9u, 0x530ECC68u, 0xBB837CD0u, 0x330A3943u, 0x60C1D83Du,
                    0x14C26EF2u, 0xEA025684u, 0xAC9D6A48u, 0xC81B0D75u, 0x9D4FCA8Cu, 0x60AFF6A8u, 0x87ED393Fu, 0x8FF9BB07u, 0x05FECEF8u, 0x7E65B037u, 0x393FB80Fu, 0x75D078A3u, 0xCCAF86D0u, 0x3A30E360u, 0x7BB1EA6Bu, 0x3C485412u,
                    0x70DCB3AEu, 0x8418A628u, 0x282302C6u, 0x8619F325u, 0x9A191074u, 0x21058FA6u, 0x3FF2790Cu, 0xAF190CACu, 0xDE244BF8u, 0x434CEA9Fu, 0x2F9CEDCAu, 0x29B23C93u, 0xC75C33B6u, 0x322187C3u, 0x76AD3AE1u, 0x9B172964u,
                    0x1D838322u, 0xAE3AF980u, 0x84D0703Fu, 0xC9B346D2u, 0x8FA89C4Du, 0x2BEE8F26u, 0xBAF16AA5u, 0x39752F84u, 0x5EC63BCAu, 0x48C57847u, 0x3EADAFDFu, 0x9437FB6Bu, 0xEED15C18u, 0x53CC7801u, 0x085166D8u, 0xA03BA013u,
                    0x4E87142Fu, 0xAF194EFEu, 0x304744F4u, 0x8B1F9472u, 0x479835DBu, 0xC84E3757u, 0x512EB743u, 0x546CF372u, 0xB2E0A672u, 0x4E7C9F6Fu, 0xA76CC36Cu, 0xBAD44AC2u, 0x59A13BE4u, 0xB84D0BDCu, 0xAD365766u, 0x1DE36DB9u,
                    0xC16B5C79u, 0xBB6F43CEu, 0xC931C935u, 0x31412C64u, 0xD4153A86u, 0xF3121911u, 0x0EE4B764u, 0x6EA71F40u, 0x7A738854u, 0x26DEA481u, 0x8ECAD493u, 0x24371579u, 0xD857AF30u, 0x96B73957u, 0xE24BD0A8u, 0x926F63D5u,
                    0x01676AC4u, 0x638B1891u, 0x321B01B8u, 0x124A56AFu, 0xBC591D7Cu, 0xA3046B99u, 0x36F87861u, 0x3ED1988Eu, 0xD5F12BD8u, 0x44003280u, 0x53A09FF2u, 0x067D4FBDu, 0xBBC74333u, 0xD91B1322u, 0xAC406EF8u, 0xB4BDC41Du,
                    0xBD088BF2u, 0x9BB469D2u, 0x23E4B80Cu, 0x40C1174Fu, 0x7EAED29Fu, 0xC4B66879u, 0xF95FAAF7u, 0x42701BEEu, 0x1C8647D8u, 0x8A79E758u, 0x31E541EAu, 0x66F5C4ABu, 0x1A2C6769u, 0x9502C366u, 0xA0FFEF29u, 0x88C32F65u,
                    0x23CC49CBu, 0x2509192Eu, 0xF76CBE8Bu, 0x3DB822C0u, 0xDDB5769Fu, 0xFDA69BC6u, 0xF976A074u, 0x2CB9822Au, 0x321E157Fu, 0xD4D806A3u, 0x9E3B2A1Fu, 0x48961E49u, 0xF5FEA5BAu, 0xF8CADFB7u, 0x5AD5F953u, 0xB125A9B7u,
                    0x7FAE489Au, 0x5B43084Du, 0xF444C80Eu, 0xEB776457u, 0x25610ED3u, 0xE0E71AEFu, 0xDE4B3774u, 0x004DB1F3u, 0xD4FB5863u, 0x240017FFu, 0x11FD8F77u, 0x55AC30DAu, 0x6A488ECFu, 0x1B7F6185u, 0xE35DFF88u, 0x651FBB15u,
                    0xBF8575CFu, 0x715B71C5u, 0x452DCCB1u, 0x32FDE409u, 0x1175DA81u, 0x3A814C48u, 0x95950B70u, 0xD1AE8B11u, 0xF02FD922u, 0x93600363u, 0x293EB35Fu, 0x0C17C616u, 0x462A0424u, 0xBE6516F7u, 0x104E0848u, 0xF7523334u,
                    0xD425782Fu
                ];
            }

            public static class LbE {
                public static readonly UInt32[] Table = [
                    0xB8AA3B29u, 0x5C17F0BBu, 0xBE87FED0u, 0x691D3E88u, 0xEB577AA8u, 0xDD695A58u, 0x8B25166Cu, 0xD1A13247u, 0xDE1C43F7u, 0x55176CD6u, 0x24D92F75u, 0xC16BE0B3u, 0xEA90B9E6u, 0x0C4A909Fu, 0xC4BFAF03u, 0x53DF39B3u,
                    0x2FE29493u, 0x2617D9D5u, 0xB21B43D5u, 0x79D5A206u, 0x0B5EBBBFu, 0x3A828546u, 0x8D1CF457u, 0xAB63253Cu, 0x199A9483u, 0x6F5B4967u, 0x278CCF08u, 0x4679C940u, 0xCE7E2035u, 0x8CD5DB8Fu, 0x612F08FBu, 0xAE30A173u,
                    0x2650B6D1u, 0x058EBA50u, 0x9638C84Cu, 0x5A02065Fu, 0x411A8DECu, 0x5EA11213u, 0x918FCF71u, 0x2DE86238u, 0x7F12325Cu, 0x49412609u, 0x079683EDu, 0xA1A245B5u, 0xEE9AC0B2u, 0xF8D159ECu, 0x33AFCF70u, 0xEC68CE71u,
                    0x17670EC7u, 0x0E7976CAu, 0x812E39D0u, 0x5B047658u, 0x564E5C21u, 0xD0E29D81u, 0x0B4C6075u, 0xD10BB3BFu, 0xBE85CE25u, 0xB7002652u, 0x8B4D76EAu, 0xD87DFBC9u, 0x103AAF04u, 0x13D7210Du, 0x79C61A86u, 0xDFE9007Au,
                    0xA1BD6C44u, 0x2A50C2DDu, 0xE25DDAFCu, 0x918BCBEFu, 0x13E0CC17u, 0x0AE37C3Du, 0x185A3461u, 0xCD952C37u, 0x28FDC908u, 0xD28DFE8Fu, 0x083AF2A2u, 0xF7F936B5u, 0xFE05A8D9u, 0xEDC06DE5u, 0x9212A4BBu, 0xB99C326Du,
                    0x6E5B0B6Bu, 0xA4F20AF5u, 0xB065ED21u, 0x79EFAEA5u, 0x293EBB17u, 0xDCBF7ED9u, 0x43F0B630u, 0x6472E5EBu, 0xFE2DCBA2u, 0x2FA52853u, 0x147C1365u, 0x334B25C8u, 0x1E7D2ED0u, 0x4CA303C8u, 0x604838E0u, 0xF11E5520u,
                    0xF6806C3Eu, 0x69F50831u, 0xCEC4B58Au, 0x7F8CD79Au, 0x92CE29BAu, 0xF817B287u, 0x2720AA50u, 0x7856A0A8u, 0x64F91E15u, 0x620D0A9Eu, 0x0AA609A1u, 0x02AAD0EEu, 0x91ACDA1Eu, 0xC30FD513u, 0x8107C492u, 0x06670B94u,
                    0x5E20D836u, 0x0CE408EAu, 0xBD221799u, 0x4B23AA5Fu, 0xA9C9E347u, 0xE73E08A1u, 0xDF668175u, 0xEDCECEB6u, 0x6B6703B9u, 0x3B2D204Du, 0x996A717Fu, 0xAF23CF54u, 0xE7B0379Cu, 0x458A5F88u, 0x207EBD61u, 0xE2B97D4Bu,
                    0x3CCD7D44u, 0x6CDC8477u, 0x19E60A26u, 0xEBD9D481u, 0xA5F7E8BCu, 0x36FFE8D9u, 0xBA61D582u, 0xBE3C1841u, 0xC151765Au, 0x54574F5Fu, 0xF12006DBu, 0xF21758D7u, 0xD7B186F0u, 0xF746C2C0u, 0x40B00496u, 0xBDFD93B7u,
                    0x41C9C879u, 0x6636EC00u, 0xEEC683C8u, 0xEDA54F30u, 0x161B8BCEu, 0xA6BC9FC2u, 0xF35BEF20u, 0xBE4EC963u, 0x66F686F6u, 0x1D0AAB45u, 0xEC620D9Au, 0xE8B59317u, 0xCE4DC37Cu, 0x77D8A36Bu, 0xAC0BD2D1u, 0x06ECF906u,
                    0x459B324Fu, 0xE3389F5Cu, 0xD2CB5BDBu, 0x1E485C17u, 0xA77D659Du, 0x7D1BEDEDu, 0x25E912CFu, 0x602DF823u, 0x2639E4C0u, 0x949A4D0Bu, 0xBE0DF563u, 0x811B186Bu, 0x34FF8CCFu, 0xADDBDC4Cu, 0x62FA102Bu, 0x9306CA91u,
                    0x91305C0Au, 0xF30458A5u, 0x6E9442C6u, 0x10DD7E5Bu, 0x830D1A2Eu, 0xDC075DECu, 0x460D466Bu, 0xEAAC61F2u, 0x4FEF3F33u, 0xF30DA3BAu, 0x96628D52u, 0xCD3D4484u, 0xD3C44515u, 0x298AC59Au, 0xF243FCE6u, 0x7C48A670u,
                    0x963A1549u, 0xF4E5B6B9u, 0xB052F27Du, 0x972566EBu, 0x879D3E2Du, 0x7DB919ECu, 0xAA39BC6Du, 0xECD07C77u, 0x740600A2u, 0x35932E01u, 0x85DE85DDu, 0x4D5122E0u, 0xD1A8D520u, 0x6474D360u, 0xD17C92CEu, 0xFC6BC019u,
                    0xDC72D8FDu, 0xDA3637F8u, 0x28A65378u, 0xBE3924C7u, 0xFC9BEE3Fu, 0x78938752u, 0xCD9B6A5Bu, 0x7A0FB699u, 0x3F1B1F02u, 0x23193CF2u, 0xD449EC42u, 0xB370B8DCu, 0x96057885u, 0xCF5980ADu, 0x0C4F0BF5u, 0xC09440A1u,
                    0xF2E75044u, 0xD22E545Au, 0xE34D75A1u, 0x9F26E210u, 0x2C48C8D3u, 0xB3FC2F2Du, 0x50A88456u, 0x2C6F2187u, 0x926EE4C3u, 0x78D0C1BBu, 0xB1CEE640u, 0xCDBFF5C9u, 0x020D7591u, 0xC640956Fu, 0x94223A7Cu, 0x0EB94ECBu,
                    0x10A4FB4Fu, 0x1D510005u, 0x28D66F09u, 0xFBEC604Bu, 0xE6EF145Cu, 0x95B991F7u, 0x4AFB359Eu, 0x08DDC50Au, 0x613ED683u, 0xB42FC75Fu, 0x20CA9107u, 0x78C3789Au, 0x9B242EEAu, 0x69FB68D0u, 0x0D3FAEE3u, 0x18A2FDABu,
                    0x443A17D1u, 0xB6D0F7B0u, 0xB6003B07u, 0x8652750Bu, 0x28A7F6A6u, 0x75CD058Du, 0x7D7F39D5u, 0x53D34B93u, 0xC2AF7FD4u, 0x3315D8B3u, 0xCA77092Eu, 0xE0B62BDAu, 0x115F8D58u, 0x0DFABB1Cu, 0x96EC409Eu, 0xE0BBB8F1u,
                    0x724AB645u, 0xFAB40933u, 0x09F240A4u, 0xA2F9AC1Du, 0x815661B1u, 0x24E8EDC8u, 0xAD7655A3u, 0xB2C167B6u, 0xE4323015u, 0xD1BC24EEu, 0xDA066308u, 0x297EEA80u, 0x0F1F66BDu, 0x6070ECE8u, 0x08E80FA9u, 0xC9555799u,
                    0xDCE9548Fu, 0x80F2EF6Cu, 0xD3761FADu, 0x8F00F22Fu, 0x4B7DAB29u, 0x8036C543u, 0x5C4C375Au, 0x5FE4DB99u, 0xA91A86C1u, 0x5DEF4E92u, 0x3E5EA5ABu, 0x6516C7EBu, 0xC6012259u, 0x001A0C77u, 0x7997FF64u, 0x151ADCE3u,
                    0xE3276B7Bu, 0x3D009294u, 0x56CFE6C6u, 0x3880B8A1u, 0xF821D916u, 0x5671DFD2u, 0x3987CE11u, 0x5E3B0D20u, 0x7AF562B7u, 0xF82EC80Bu, 0x4CE11079u, 0x92ECC09Fu, 0xAC91B485u, 0x95CD3D85u, 0xEE0EAAF2u, 0x7348F73Cu,
                    0xE176E16Du, 0xD801AC41u, 0xAE9B40ABu, 0x2DA09F01u, 0xF91BC700u, 0xEFE37313u, 0x0F55C2F6u, 0x603CC66Bu, 0x2B8392DAu, 0x339CC753u, 0xEE10A7EEu, 0x6A4E27C8u, 0x26AD7411u, 0xDE19D90Cu, 0x21115D04u, 0x113336E2u,
                    0x7703B823u, 0xDA674BF5u, 0xFD332CB6u, 0xF195FBAFu, 0x1932284Du, 0x2D67E760u, 0x44837B6Fu, 0x3E9B4329u, 0x260D0DF0u, 0x23043527u, 0x9CE71020u, 0x360157A9u, 0xDD48BE50u, 0x90BE0AF0u, 0x6BEF67CDu, 0xEAFECCE6u,
                    0x1421B357u, 0xC1CA0BF4u, 0xCA73EB5Bu, 0xFB5BE07Bu, 0x0D7F62ACu, 0x0B35762Fu, 0x2FB45C44u, 0x48996A6Eu, 0xF4EBEFCDu, 0xE26DF8EDu, 0x19DDA71Fu, 0x8540C678u, 0xD3D1D3B1u, 0x8B9539C9u, 0x7A98FE92u, 0xAFACC8A0u,
                    0xC44D807Cu, 0x5EC0B3CCu, 0x3874E2C6u, 0xAE474BBEu, 0xF01DD77Cu, 0xB6028E55u, 0x62A0C7AEu, 0xCA6F60BDu, 0x16C601B8u, 0x6CAC4B3Cu, 0x83E30103u, 0x716C63E1u, 0xA020CA59u, 0xBAC70F1Du, 0xE17DCB0Bu, 0x96B1AA84u,
                    0xF666B7C1u, 0xE0082D07u, 0x91212D4Fu, 0x98609C30u, 0xB61DC8CAu, 0x964F0401u, 0xA0A1C0DAu, 0xE4DABD76u, 0x987D3F23u, 0x4ABFB09Eu, 0x29F4E6CAu, 0xACB23D86u, 0x604DC701u, 0x5B6944FCu, 0xB22C4E87u, 0x5432CA39u,
                    0x9B4715E9u, 0xD43575CEu, 0x5A596A00u, 0x3BA23B1Au, 0x143740D4u, 0x8CFBB12Cu, 0xA9458725u, 0xA75E9E57u, 0xB992DB18u, 0x1B5372A6u, 0xB0F36CFBu, 0x36AB8631u, 0x7087CCC6u, 0xBE474A15u, 0x63E99CEBu, 0x9B2C3801u,
                    0x6F209A15u, 0x2D9A2863u, 0xD61398C4u, 0x3BA1BE2Du, 0xE9F80101u, 0xEB511E5Bu, 0x2AACD5FCu, 0xF992CC81u, 0x6EDA1B99u, 0xA80328E7u, 0x98BC5467u, 0xC0A09682u, 0x7BE66BCBu, 0xCAD1BADDu, 0xE225A373u, 0x6D34844Fu,
                    0x00CE450Fu, 0x1CE73BEEu, 0x8EFC915Du, 0x38C0CE70u, 0x66430517u, 0x4681FB89u, 0x78F81661u, 0x8FFD97B1u, 0xBFC26EE9u, 0xFE8B0ADDu, 0x7D26F567u, 0x6AC5D1DBu, 0xD974CA9Cu, 0x46D4F661u, 0x24B31018u, 0x426FCCFCu,
                    0xE61A1850u, 0x3303E235u, 0x8DF356A0u, 0xE58C2471u, 0x5F184623u, 0xA113D804u, 0x660051F8u, 0xEDD01B3Au, 0xC9D18F3Au, 0x7DBD21DEu, 0xD769EBD2u, 0x661CDFC3u, 0x34696CA7u, 0x80CC8AAAu, 0x58977EA2u, 0x8975781Eu,
                    0x0D7AE247u, 0x82AA0A64u, 0x81C8C65Fu, 0x570596DCu, 0xB625D9DAu, 0x68A52D4Fu, 0x1F6F68E3u, 0x36CCAACEu, 0xA453D3A8u, 0x97CA8C3Eu, 0x42F03153u, 0x745A4754u, 0x45F6D239u, 0x71A24798u, 0x3C3BEA80u, 0x5C417B9Bu,
                    0x3B558857u, 0xC6730838u, 0xDBE1C54Au, 0x050FC90Bu, 0xA74E4920u, 0xAAFC33CEu, 0xDC2BCA24u, 0x60DBCD88u, 0xFCE021E9u, 0x46D0CB18u, 0x61362BB1u, 0x4C9B8B5Cu, 0x1D4D5374u, 0xD7A2642Du, 0x387DA12Eu, 0xABADC905u,
                    0x23819476u, 0x1CFE3FF6u, 0x89C94295u, 0x1C637B36u, 0x8AB81CF8u, 0x50BEE4C3u, 0xA17550F1u, 0x412285A4u, 0x0AC18791u, 0xD1864AF5u, 0x339D208Fu, 0xD099D173u, 0x1C8BBB5Fu, 0x5AFD139Bu, 0x0591EC84u, 0xB5213E24u,
                    0xDFDDEF12u, 0x3D50BFB3u, 0xAAF388AAu, 0x19C9B39Bu, 0xE4B3ABADu, 0x6A6B9EA9u, 0xAE89A108u, 0x58A0F7A0u, 0x8FD73FF0u, 0xDD84E2A0u, 0x648F8D94u, 0x6C067DC8u, 0x8355326Au, 0x1A6A8B69u, 0xA3F0870Fu, 0x3B012466u,
                    0x6BB92B34u, 0xBACD9E80u, 0x6BB79011u, 0xC0BCEACBu, 0xF4098836u, 0x24627C02u, 0xEDAC2363u, 0x0B67548Au, 0xE016052Du, 0x9AAEB3C5u, 0xE2A42970u, 0x1528A66Fu, 0x199D7362u, 0xFC85D17Au, 0xC8C2787Eu, 0xFDEAE408u,
                    0x43A3DB2Eu, 0x3C1A2A0Du, 0x44EC2657u, 0x21D6379Du, 0x5C73F01Fu, 0x25788D67u, 0xB5D6CB8Fu, 0xD2517939u, 0x59247047u, 0xCC5D89C0u, 0x5E140D31u, 0x4B443478u, 0x89F3B2EEu, 0xDE0614AAu, 0xA21F52ADu, 0x6A71CAB6u,
                    0xBF4DB75Au, 0x56A57215u, 0xF43D950Au, 0xF274748Eu, 0x2CC68624u, 0xAC2B20E3u, 0xC211B263u, 0xF651BBD4u, 0xD4C10BB8u, 0x0F512597u, 0x821820D9u, 0x7F583248u, 0x65B58AACu, 0xBD449DC6u, 0xCDCF0219u, 0x8AFFE7CFu,
                    0x2891A167u, 0x0C2C4751u, 0x5913FD2Eu, 0x3AD0CAF3u, 0xA71BD6CAu, 0xADCB51F5u, 0x88DA9466u, 0x464CC786u, 0x34262EE0u, 0xBBA07670u, 0xE78A1A6Fu, 0x8141D9A9u, 0xAEA46D56u, 0x82D6A4EBu, 0x5F60CAE0u, 0x44869EEAu,
                    0x81AB78E4u, 0xAA053BEEu, 0x1E97D21Au, 0xE6902783u, 0x30241A01u, 0xCA7C6A1Au, 0x8CEAB99Du, 0x850F0244u, 0x537766DDu, 0x8254683Fu, 0x7C09E279u, 0xA35F0F8Fu, 0x407EB33Du, 0x34DA1644u, 0x077B8401u, 0xA5D33DA7u,
                    0xA74AAA66u, 0x8C87E35Eu, 0x76517E8Cu, 0x0C19AD76u, 0x3E81A541u, 0xF6B444E2u, 0x55C0219Cu, 0xE6343FE7u, 0x38667668u, 0xC31E3AD5u, 0x0E9B8DB3u, 0x237C0C5Cu, 0x11091C2Cu, 0x0052F899u, 0xF4CE262Eu, 0xA2552D59u,
                    0x77BEFCD4u, 0x882377C2u, 0xFBA17DDEu, 0xCF09A397u, 0xCB02DBD6u, 0xA59E9421u, 0xA47E4C02u, 0x3DFDE749u, 0x55080561u, 0x52EAD3EDu, 0xEB9DCC29u, 0x1637A2F0u, 0x226982FFu, 0x5EBF6B72u, 0x5CD28896u, 0xAC16B4F8u,
                    0x1F3E1283u, 0x5C68285Eu, 0xE0C140B1u, 0xE7ABAEEAu, 0x2796725Eu, 0x4BFEFB7Eu, 0x897AA1C6u, 0x0C91EE8Au, 0x0C5C30B5u, 0xA19FA5C2u, 0xBFD9E793u, 0xC58E5452u, 0xA931016Du, 0xA48B9C50u, 0x9AC5B9BAu, 0x24A2701Fu,
                    0xCBE9AB29u, 0xD5A3E33Eu, 0x80AAA7C0u, 0xC9A02004u, 0xDFBEB557u, 0xD086EFEDu, 0x9520A950u, 0x3F3369C9u, 0xA6A2190Eu, 0x2D31540Fu, 0x0C06BE2Cu, 0x3F9F1DDBu, 0x15E9C3CAu, 0x9B11BC5Fu, 0x6CFA6628u, 0x62828879u,
                    0x51A2D93Fu, 0xED0CA28Fu, 0x2050E389u, 0xA0676AECu, 0xFC065C17u, 0x2A518767u, 0xBAEEAC4Du, 0xE30C8198u, 0x8A8F61D5u, 0xBE77FC07u, 0x60978716u, 0x37FD961Au, 0x88293EB0u, 0xC967FDC7u, 0x6E5C4AB6u, 0x962441D1u,
                    0x7F8953EEu, 0xD342EA22u, 0xDDED8103u, 0x2F0D6F45u, 0xF11A7064u, 0x9A0FF126u, 0x6FFFC8E4u, 0xADDA32EBu, 0xE9E8C3A1u, 0xC0E29601u, 0xA0F41170u, 0xAC09AF65u, 0x5A53E44Eu, 0x701CC386u, 0xD0102AB3u, 0x067A31C2u,
                    0xF51EAED9u, 0x7EFA5EA7u, 0x23289B88u, 0x84C4A85Au, 0x359CDA1Eu, 0x93761D04u, 0xF6219F0Eu, 0xE82FAB62u, 0x8C9D6230u, 0x96ECB03Eu, 0x636AA9E1u, 0x7978D494u, 0x857B8067u, 0x64FDDBDBu, 0xDF17CAFEu, 0x97611297u,
                    0xFCF55312u, 0xEE15D473u, 0x4B90AB17u, 0x8BE5B48Fu, 0x02A8A666u, 0x761A3FFDu, 0x5538989Du, 0x15A17FDBu, 0xEB30AC50u, 0x25A218A2u, 0x677396A3u, 0x5CE79460u, 0x01B2E22Du, 0x7601BCEEu, 0xED403EA0u, 0x00352C85u,
                    0x811999F7u, 0xBB14AC3Bu, 0x0438F6DAu, 0x2D4D4A86u, 0x0C120381u, 0xFB92A800u, 0xE7607DF1u, 0x971BD68Bu, 0xFD744935u, 0xFA8B4FAEu, 0x129E53A7u, 0xFCB81EDBu, 0xA9F20A70u, 0xE4CBA81Eu, 0x469261A4u, 0x3E966597u,
                    0x9C184EEDu, 0x458B5F20u, 0x4DB387BBu, 0x4F69735Bu, 0x93646FB8u, 0x2EB802BBu, 0x7985FAF4u, 0x633D4E10u, 0x496E9012u, 0x2F6CFA32u, 0x54E08AD5u, 0x25DFB426u, 0x84890357u, 0x323F1AE9u, 0xDBFBD33Fu, 0x9CEFB539u,
                    0xFA25C1EFu, 0xBE38B9C3u, 0x84027A9Cu, 0x8D070741u, 0x304FB87Bu, 0x0EE9DCFDu, 0x0A372C49u, 0xD6154E78u, 0x47E1EF79u, 0x1F9E410Fu, 0x0D8A55AAu, 0x51C8412Fu, 0x2A94F0DCu, 0x0F786B74u, 0x9969BC18u, 0x50B1D7A3u,
                    0xB12E5387u, 0x088DAD37u, 0x231B97DFu, 0xC972DAC6u, 0xA88F16C1u, 0x32057110u, 0x8FD6AFD0u, 0xAC88F03Eu, 0x4F10D9C2u, 0x9CF71DFAu, 0x7682FB24u, 0x856E02AEu, 0x102A94D6u, 0x2CDBB938u, 0xDE4C82B9u, 0x00E0E660u,
                    0x5999CA3Bu, 0xB8FA3E49u, 0xDAE9415Au, 0xBE0A636Eu, 0x8334332Du, 0x422CCFA1u, 0x5CE9F0A9u, 0xC3390AC9u, 0xD862A488u, 0x32F0E9A4u, 0x0109D5B5u, 0xC15F9D92u, 0x47001494u, 0x211C655Du, 0x16039153u, 0x28600FEEu,
                    0xFD86D742u, 0xBA2F7496u, 0x3FD7AC31u, 0x4365D3CAu, 0x51214138u, 0x8320B6CFu, 0x63BEED85u, 0x591EDED5u, 0x7D368A09u, 0xD47F1FF9u, 0x02F9749Eu, 0x68D532D1u, 0x897BAFA9u, 0x339A3198u, 0xB08C2715u, 0x2E5FFCB1u,
                    0xD11B086Fu, 0x2EDC4582u, 0x16F17125u, 0x5A6F7F31u, 0x94E5D3EAu, 0xB5B4446Bu, 0x9F9088B8u, 0x743ABA9Au, 0xCA915C1Du, 0x4EE5A3C1u, 0x873061E5u, 0x4FB5D033u, 0xD1D5EE5Fu, 0x0D0E970Bu, 0x64A3CBE4u, 0x76723F1Au,
                    0x68DE3B0Fu, 0x504DD5E3u, 0x3540E509u, 0x73BB5E00u, 0xF14C871Au, 0x19480CD4u, 0xA20FE57Cu, 0xA0D13D04u, 0x6F600791u, 0x991FFCE8u, 0xEE71E356u, 0x52335F8Eu, 0x9E99F3ABu, 0x6CFCFEBFu, 0x2E19C866u, 0x1920D3B7u,
                    0x5AF259A4u, 0xA4EF6E2Bu, 0xF07B10ADu, 0x1E1392EBu, 0x2F61E938u, 0xDAF734AEu, 0xAD51BB89u, 0xD88C8214u, 0x1E25B6C2u, 0xAF5EE727u, 0x49370FE7u, 0x665FA1F6u, 0x9D45D318u, 0x3AED1FAAu, 0x36A04127u, 0x10715B08u,
                    0x1E98C0A1u, 0x10394341u, 0x51DF5121u, 0xCE92630Au, 0xBEEBCD9Du, 0x2DACC7B9u, 0x9EDCB5F3u, 0xBE9A2992u, 0x6A8F91D3u, 0x78612833u, 0x2794F3A4u, 0x3E88E9D0u, 0xE2CAABBBu, 0xAD742E30u, 0x563EDD2Eu, 0x575A897Du,
                    0x327BF264u, 0x2EA175B4u, 0x27B225C8u, 0x1FCC0D5Du, 0x4FA4FDA7u, 0xE4CAABD8u, 0x55C65E90u, 0x284C43D8u, 0x5B215425u, 0xC585AE55u, 0x02835855u, 0xDC8DB21Eu, 0xAF5E54A4u, 0x03777B1Cu, 0xD30681B7u, 0x27C77738u,
                    0x8F71D471u, 0x995F9C73u, 0xB742AB7Du, 0xE4799266u, 0xB1E8070Au, 0xEA27C7FCu, 0xC4FC0EACu, 0x93CCAC55u, 0x6600B756u, 0x23DC6C71u, 0x1CFB3EA1u, 0x98593745u, 0xFB63D28Cu, 0x3FCC99A5u, 0xA2810028u, 0x662CB7C9u,
                    0xC8FC05E5u, 0x61786F49u, 0x9973AA7Cu, 0xAA5E5DA3u, 0x334BB127u, 0x3CD33A41u, 0x52DB72DAu, 0x3BABBF17u, 0xC56257E3u, 0xA05418F3u, 0x68B1C123u, 0x0051DC83u, 0x71D74B9Eu, 0x7583AE58u, 0x0890CFE0u, 0xA2A4A87Eu,
                    0xB7A601A7u, 0x583E87A2u, 0xB8C42D20u, 0xA3FF97B4u, 0x2B9D9696u, 0xC85CBFB4u, 0x72458FE1u, 0x6D769DA0u, 0x573B8460u, 0x27699128u, 0x6EB8D535u, 0x5FE7AE12u, 0x487D8A9Cu, 0xA49AFD6Eu, 0xA6FFEC02u, 0x5325A16Bu,
                    0x6C16864Cu, 0x014EB2ACu, 0x500A3F2Du, 0x1C357FC2u, 0xDFA77F0Cu, 0x6027A2A1u, 0xC1B40481u, 0x6C995794u, 0x44A0330Eu, 0x2D4FE6B3u, 0x103FCF98u, 0x2E2D9FB9u, 0x7ABF1FE4u, 0x06EA5725u, 0x5E84923Du, 0xE3E138CBu,
                    0x4CF94D99u, 0xC7FEE94Fu, 0x51A395CEu, 0xA270DD93u, 0x90BEDFF8u, 0x3BDADEBEu, 0x35A8A50Du, 0x2C8384BBu, 0xF5CC1A47u, 0xFEE4E01Au, 0x638B001Fu, 0x65427025u, 0xB2CFF135u, 0x03B4C012u, 0x59908B74u, 0x6D5B9898u,
                    0x3C969D73u, 0xC3AA1C66u, 0x4EB16EA0u, 0xD49700CCu, 0x3B8FDC78u, 0x5D71A91Fu, 0x21391D33u, 0xAB1560D1u, 0x83F6BB2Eu, 0x695CE005u, 0xF0C12B59u, 0x67747CC5u, 0x93164A56u, 0x68CD5F61u, 0x93979E3Au, 0x79B54815u,
                    0x409CBBD5u, 0x5C0B2784u, 0x23C3E4A9u, 0x23D3901Eu, 0x274A1BC2u, 0x8BCC883Cu, 0x38B0875Bu, 0xA9B34738u, 0x48D653FBu, 0x1660DAEBu, 0x4694EA3Cu, 0x82DF779Eu, 0xD78A0653u, 0x04E23326u, 0x865509F0u, 0x2A0FE1A4u,
                    0x2C139507u, 0x454E4A9Au, 0xB21DB665u, 0xC00EA8D3u, 0xA81806A3u, 0x04A1ED96u, 0x3D8F32DBu, 0x0568042Eu, 0x053D908Bu, 0xB07E8CC3u, 0x01826B4Eu, 0x8C5C4F5Bu, 0x6EE593A3u, 0x72BE9B5Bu, 0x7DE6062Du, 0x663E7EB3u,
                    0xBCB4E20Cu
                ];
            }
        }
    }
}
