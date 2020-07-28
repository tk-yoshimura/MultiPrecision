﻿using System;
using System.Linq;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> pi = null, inv_pi = null;
        }

        public static MultiPrecision<N> PI {
            get {
                if (Consts.pi is null) {
                    Consts.pi = GeneratePI();
                }

                return Consts.pi;
            }
        }

        public static MultiPrecision<N> InvertPI {
            get {
                if (Consts.inv_pi is null) {
                    Consts.inv_pi = MultiPrecisionUtil.Convert<N, Plus1<N>>(MultiPrecision<Plus1<N>>.One / MultiPrecision<Plus1<N>>.PI);
                }

                return Consts.inv_pi;
            }
        }

        private static MultiPrecision<N> GeneratePI() {
            if (Length < Consts.PI.Table.Length) {
                return new MultiPrecision<N>(
                    Sign.Plus, exponent: 1,
                    new Mantissa<N>(Consts.PI.Table[..Length].Reverse().ToArray(), enable_clone: false),
                    round: Consts.PI.Table[Length] > UIntUtil.UInt32Round
                );
            }

            MultiPrecision<Plus1<N>> a = MultiPrecision<Plus1<N>>.One;
            MultiPrecision<Plus1<N>> b = MultiPrecision<Plus1<N>>.Ldexp(MultiPrecision<Plus1<N>>.Sqrt2, -1);
            MultiPrecision<Plus1<N>> t = MultiPrecision<Plus1<N>>.Ldexp(MultiPrecision<Plus1<N>>.One, -2);
            MultiPrecision<Plus1<N>> p = MultiPrecision<Plus1<N>>.One;

            for (long i = 1; i < Bits; i *= 2) {
                MultiPrecision<Plus1<N>> a_next = MultiPrecision<Plus1<N>>.Ldexp(a + b, -1);
                MultiPrecision<Plus1<N>> b_next = MultiPrecision<Plus1<N>>.Sqrt(a * b);
                MultiPrecision<Plus1<N>> t_next = t - p * (a - a_next) * (a - a_next);
                MultiPrecision<Plus1<N>> p_next = MultiPrecision<Plus1<N>>.Ldexp(p, 1);

                a = a_next;
                b = b_next;
                t = t_next;
                p = p_next;
            }

            MultiPrecision<Plus1<N>> c = a + b;
            MultiPrecision<Plus1<N>> y = c * c / MultiPrecision<Plus1<N>>.Ldexp(t, 2);

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(y);
        }

        private static partial class Consts {
            public static class PI {
                public static readonly UInt32[] Table = {
                    0xC90FDAA2u, 0x2168C234u, 0xC4C6628Bu, 0x80DC1CD1u, 0x29024E08u, 0x8A67CC74u, 0x020BBEA6u, 0x3B139B22u, 0x514A0879u, 0x8E3404DDu, 0xEF9519B3u, 0xCD3A431Bu, 0x302B0A6Du, 0xF25F1437u, 0x4FE1356Du, 0x6D51C245u, 
                    0xE485B576u, 0x625E7EC6u, 0xF44C42E9u, 0xA637ED6Bu, 0x0BFF5CB6u, 0xF406B7EDu, 0xEE386BFBu, 0x5A899FA5u, 0xAE9F2411u, 0x7C4B1FE6u, 0x49286651u, 0xECE45B3Du, 0xC2007CB8u, 0xA163BF05u, 0x98DA4836u, 0x1C55D39Au, 
                    0x69163FA8u, 0xFD24CF5Fu, 0x83655D23u, 0xDCA3AD96u, 0x1C62F356u, 0x208552BBu, 0x9ED52907u, 0x7096966Du, 0x670C354Eu, 0x4ABC9804u, 0xF1746C08u, 0xCA18217Cu, 0x32905E46u, 0x2E36CE3Bu, 0xE39E772Cu, 0x180E8603u, 
                    0x9B2783A2u, 0xEC07A28Fu, 0xB5C55DF0u, 0x6F4C52C9u, 0xDE2BCBF6u, 0x95581718u, 0x3995497Cu, 0xEA956AE5u, 0x15D22618u, 0x98FA0510u, 0x15728E5Au, 0x8AAAC42Du, 0xAD33170Du, 0x04507A33u, 0xA85521ABu, 0xDF1CBA64u, 
                    0xECFB8504u, 0x58DBEF0Au, 0x8AEA7157u, 0x5D060C7Du, 0xB3970F85u, 0xA6E1E4C7u, 0xABF5AE8Cu, 0xDB0933D7u, 0x1E8C94E0u, 0x4A25619Du, 0xCEE3D226u, 0x1AD2EE6Bu, 0xF12FFA06u, 0xD98A0864u, 0xD8760273u, 0x3EC86A64u, 
                    0x521F2B18u, 0x177B200Cu, 0xBBE11757u, 0x7A615D6Cu, 0x770988C0u, 0xBAD946E2u, 0x08E24FA0u, 0x74E5AB31u, 0x43DB5BFCu, 0xE0FD108Eu, 0x4B82D120u, 0xA9210801u, 0x1A723C12u, 0xA787E6D7u, 0x88719A10u, 0xBDBA5B26u, 
                    0x99C32718u, 0x6AF4E23Cu, 0x1A946834u, 0xB6150BDAu, 0x2583E9CAu, 0x2AD44CE8u, 0xDBBBC2DBu, 0x04DE8EF9u, 0x2E8EFC14u, 0x1FBECAA6u, 0x287C5947u, 0x4E6BC05Du, 0x99B2964Fu, 0xA090C3A2u, 0x233BA186u, 0x515BE7EDu, 
                    0x1F612970u, 0xCEE2D7AFu, 0xB81BDD76u, 0x2170481Cu, 0xD0069127u, 0xD5B05AA9u, 0x93B4EA98u, 0x8D8FDDC1u, 0x86FFB7DCu, 0x90A6C08Fu, 0x4DF435C9u, 0x34028492u, 0x36C3FAB4u, 0xD27C7026u, 0xC1D4DCB2u, 0x602646DEu, 
                    0xC9751E76u, 0x3DBA37BDu, 0xF8FF9406u, 0xAD9E530Eu, 0xE5DB382Fu, 0x413001AEu, 0xB06A53EDu, 0x9027D831u, 0x179727B0u, 0x865A8918u, 0xDA3EDBEBu, 0xCF9B14EDu, 0x44CE6CBAu, 0xCED4BB1Bu, 0xDB7F1447u, 0xE6CC254Bu, 
                    0x33205151u, 0x2BD7AF42u, 0x6FB8F401u, 0x378CD2BFu, 0x5983CA01u, 0xC64B92ECu, 0xF032EA15u, 0xD1721D03u, 0xF482D7CEu, 0x6E74FEF6u, 0xD55E702Fu, 0x46980C82u, 0xB5A84031u, 0x900B1C9Eu, 0x59E7C97Fu, 0xBEC7E8F3u, 
                    0x23A97A7Eu, 0x36CC88BEu, 0x0F1D45B7u, 0xFF585AC5u, 0x4BD407B2u, 0x2B4154AAu, 0xCC8F6D7Eu, 0xBF48E1D8u, 0x14CC5ED2u, 0x0F8037E0u, 0xA79715EEu, 0xF29BE328u, 0x06A1D58Bu, 0xB7C5DA76u, 0xF550AA3Du, 0x8A1FBFF0u, 
                    0xEB19CCB1u, 0xA313D55Cu, 0xDA56C9ECu, 0x2EF29632u, 0x387FE8D7u, 0x6E3C0468u, 0x043E8F66u, 0x3F4860EEu, 0x12BF2D5Bu, 0x0B7474D6u, 0xE694F91Eu, 0x6DBE1159u, 0x74A3926Fu, 0x12FEE5E4u, 0x38777CB6u, 0xA932DF8Cu, 
                    0xD8BEC4D0u, 0x73B931BAu, 0x3BC832B6u, 0x8D9DD300u, 0x741FA7BFu, 0x8AFC47EDu, 0x2576F693u, 0x6BA42466u, 0x3AAB639Cu, 0x5AE4F568u, 0x3423B474u, 0x2BF1C978u, 0x238F16CBu, 0xE39D652Du, 0xE3FDB8BEu, 0xFC848AD9u, 
                    0x22222E04u, 0xA4037C07u, 0x13EB57A8u, 0x1A23F0C7u, 0x3473FC64u, 0x6CEA306Bu, 0x4BCBC886u, 0x2F8385DDu, 0xFA9D4B7Fu, 0xA2C087E8u, 0x79683303u, 0xED5BDD3Au, 0x062B3CF5u, 0xB3A278A6u, 0x6D2A13F8u, 0x3F44F82Du, 
                    0xDF310EE0u, 0x74AB6A36u, 0x4597E899u, 0xA0255DC1u, 0x64F31CC5u, 0x0846851Du, 0xF9AB4819u, 0x5DED7EA1u, 0xB1D510BDu, 0x7EE74D73u, 0xFAF36BC3u, 0x1ECFA268u, 0x359046F4u, 0xEB879F92u, 0x4009438Bu, 0x481C6CD7u, 
                    0x889A002Eu, 0xD5EE382Bu, 0xC9190DA6u, 0xFC026E47u, 0x9558E447u, 0x5677E9AAu, 0x9E3050E2u, 0x765694DFu, 0xC81F56E8u, 0x80B96E71u, 0x60C980DDu, 0x98A573EAu, 0x4472065Au, 0x139CD290u, 0x6CD1CB72u, 0x9EC52A52u, 
                    0x86D44014u, 0xA694CA45u, 0x7583D5CFu, 0xEF26F1B9u, 0x0AD8291Du, 0xA0799D00u, 0x022E9BEDu, 0x55C6FA47u, 0xFCA5BB1Au, 0xCA837645u, 0x6D98D948u, 0x79EE7E6Du, 0xBFCD014Bu, 0xB1615599u, 0x14EC0B57u, 0x6A67E3E8u, 
                    0x422E91E6u, 0x5BA141DAu, 0x92DE9C3Au, 0x6D6CCA51u, 0x36DD424Bu, 0xB1064988u, 0xEB5BA9ACu, 0x1269F7DFu, 0x673B982Eu, 0x23FB6C99u, 0xBB2AA31Cu, 0x5A6685FFu, 0xD599149Bu, 0x30AC67B8u, 0x464D80A9u, 0x5D42530Au, 
                    0x681644D0u, 0x39060E8Fu, 0x8FD52626u, 0x96D0A759u, 0x5AE3F935u, 0xA67DCFF5u, 0xA874A701u, 0xFBFA0C3Du, 0x534B4E39u, 0xBC095770u, 0x53374821u, 0xA11C3AC9u, 0x98E0BA71u, 0x8087B317u, 0x825A1ACFu, 0xCFAEBBF2u, 
                    0x4F25C605u, 0x1ADA9C28u, 0x5A1FCD61u, 0x14A838A1u, 0xADE714C1u, 0x6A9401CDu, 0xCF81E107u, 0x1FF7AB97u, 0x239F513Bu, 0x15C5BCAEu, 0x2C0EB68Du, 0xFC140303u, 0x7C0707C1u, 0x00802CFFu, 0xEB833D46u, 0x8F2D5D2Cu, 
                    0x8960DE96u, 0x3702486Fu, 0x746444FEu, 0x5F2A4BFDu, 0xA50C91DCu, 0xC8BD51C0u, 0x4EB97960u, 0x4DF0B6B7u, 0x322D5D8Du, 0x26BCF769u, 0xEA511851u, 0x83F400C3u, 0xBB3231CFu, 0xA91D4790u, 0x788E3366u, 0x4EFA838Bu, 
                    0xCCA02EE8u, 0x460FACCCu, 0x539522CEu, 0x13DB6E42u, 0x1BD08340u, 0xFD82812Fu, 0xCB2E04A4u, 0x0925DF1Eu, 0x559E6C1Cu, 0xAF2BE26Bu, 0xF7A69DC7u, 0xF664C204u, 0x2CE2EB84u, 0xB733CFCBu, 0x95449C87u, 0xCB9ADC49u, 
                    0x1406B779u, 0xA7E13361u, 0xDE9611C6u, 0x1D023685u, 0xEF27E6AFu, 0x3A52DF63u, 0x3B1EBB0Eu, 0xB6E1477Eu, 0x98C250D9u, 0xB11930F4u, 0xBBC70611u, 0xCC857642u, 0x3750CECDu, 0xC930AE85u, 0x84A85350u, 0xCA997114u, 
                    0x54250000u, 0x84CEB937u, 0x5C77FE27u, 0x840C5395u, 0x606B1DF5u, 0x97C44666u, 0xC10D55BCu, 0x75E8F1DAu, 0xCF04460Eu, 0xD6492942u, 0x7CA3F9BBu, 0x65FC7EFEu, 0xA7AEAFCBu, 0x07854F1Bu, 0xA1B8D15Cu, 0x3ABA5BECu, 
                    0x61839782u, 0x968F8AACu, 0xDDC7F9C7u, 0x138F41BEu, 0x8A59772Eu, 0x6679C743u, 0xE00FA275u, 0x9499B209u, 0x4B93325Eu, 0x27042CDAu, 0xB18543AEu, 0xA538BA9Eu, 0x297F0F14u, 0xC7828B7Du, 0x3CBDD3A9u, 0xCD874ACFu, 
                    0x464E4983u, 0xC6709E58u, 0x1488E9C2u, 0x3DC4C4ADu, 0xBAEB7F9Bu, 0xBAB0C7D9u, 0xB8EF1165u, 0x699EF220u, 0xEC5FCDF4u, 0x40633FCAu, 0x30CCB77Bu, 0xEF9B16A9u, 0x59560861u, 0x5A2AE600u, 0xBBB3A943u, 0xF6CBE54Eu, 
                    0xCABBDF6Bu, 0x56DB8BE1u, 0x05486D8Au, 0x0A41D85Cu, 0x3B3751DDu, 0x5867C544u, 0x04F32A0Cu, 0x3AD86F65u, 0x80CD3F87u, 0xAA80D8F3u, 0xED5CD724u, 0x131C288Eu, 0x7567A782u, 0xF2EAB785u, 0x3BB321AFu, 0x18188B29u, 
                    0xE72AD72Au, 0xECBCE11Bu, 0x9922C7ABu, 0xC66F7C32u, 0xA808DA6Eu, 0x5956AED4u, 0x101A168Cu, 0x8F0AAD2Cu, 0xCC67BA75u, 0x70086E3Du, 0xE6D502C6u, 0x61D7E826u, 0x657DE65Fu, 0x988F5F6Au, 0x3E0DE226u, 0xA5F8CB5Du, 
                    0xC47B64D7u, 0xC59A04A0u, 0x438D620Au, 0x71F987F5u, 0xA5B7B7E8u, 0x5E162EA6u, 0x55FD6129u, 0x46C89C98u, 0xE6E0F0FFu, 0xC6B091A5u, 0xB36CC2BAu, 0xD4CB8C15u, 0x23F65239u, 0x1B6F0C4Au, 0x163AFCBBu, 0xCD31BFFAu, 
                    0xBF8A3B58u, 0x7B9F0F1Cu, 0xD7528536u, 0x7A192DF8u, 0xD0841745u, 0x080F84F8u, 0x117BB8ADu, 0xA8EAAAFAu, 0xB6DB13C5u, 0x7EB2D3F4u, 0x31D0BD10u, 0xBBDAAEEDu, 0x5953CEC7u, 0x50734841u, 0x76079E67u, 0xA1A15371u, 
                    0xF912D1DAu, 0x8F605894u, 0x33D8A87Cu, 0x96E34991u, 0xBF2220E8u, 0x3071EDA8u, 0xDFC54930u, 0xDA72DD24u, 0x91E12282u, 0xD5A4ACA1u, 0x4256EFC0u, 0x2B465227u, 0x4518AC5Du, 0x08E08380u, 0x1610A34Au, 0x83157D7Au, 
                    0x876B7D0Fu, 0x88CFDC18u, 0x4CDCBC24u, 0xA364DF90u, 0x7597FB3Cu, 0x5B088EF6u, 0xDF378DD6u, 0x72FB9D18u, 0x10217CA9u, 0xF39DCC9Bu, 0xA981E021u, 0x067E1427u, 0xBA3BF615u, 0x587665CDu, 0x6A5A69EAu, 0xB14301B0u, 
                    0x96812AFFu, 0x2002F2B7u, 0x27911E8Bu, 0xB0D14D21u, 0x3F7559C1u, 0x4387A7B2u, 0x76DCF6F4u, 0xC4156233u, 0x59D7F69Eu, 0x78D9D0D0u, 0x31710D19u, 0x5C4F8E36u, 0x0F4A3E27u, 0xBC5B7FC8u, 0x054F8879u, 0xE3EC0F52u, 
                    0xB9B8E7CAu, 0xF6E0EB7Du, 0xFA4F569Au, 0x2520503Du, 0xFD930987u, 0x251A4A4Du, 0x1045483Du, 0xDD80B53Du, 0xEF3D1ACBu, 0xB528801Au, 0x3502091Cu, 0x4CC83D1Au, 0x90EDF52Du, 0xD400186Bu, 0xC78E7D8Bu, 0xA5C91151u, 
                    0x850853DDu, 0x2FE2E210u, 0x13657F07u, 0x65AD646Bu, 0xDC3D3774u, 0xD9A80BD1u, 0x6FEF027Bu, 0x00EF65E1u, 0x5FEB1B74u, 0x0C72E141u, 0x25BAC9ECu, 0xD57F4E50u, 0x768951F9u, 0xAAF282A6u, 0x8A141E09u, 0x54C10A7Du, 
                    0x028B21B6u, 0xBA6D9B7Eu, 0xDA370518u, 0xB5D21A40u, 0x1A03B029u, 0x09E8637Bu, 0x93CFFFA8u, 0xBA21EB63u, 0x2D633801u, 0x9EBD35ADu, 0xAAB3879Fu, 0x34CDD7FBu, 0x339E28E6u, 0x501ACA90u, 0x883FA78Du, 0x767CE16Eu, 
                    0x7B8E75EAu, 0xCEC493A2u, 0xC7727EBDu, 0xD2DB4615u, 0x89A8D98Cu, 0x7AB8E5ECu, 0x8E9BBE9Du, 0x3756D0CCu, 0x9A1079FDu, 0xF29E083Eu, 0xFEC2BD53u, 0xB63FACE5u, 0xD15015ABu, 0x2E922549u, 0xD554CE8Eu, 0x8820E361u, 
                    0xFF9AEA6Du, 0xF425A552u, 0xD56A19EFu, 0x28456696u, 0x332A4A58u, 0xE67876CCu, 0xE98A9295u, 0x8FCC497Eu, 0x57BD1F87u, 0x240A4C5Fu, 0x3F7E3A00u, 0x8109CBDCu, 0x202EC557u, 0x014A0B38u, 0xE5704552u, 0x39319B48u, 
                    0x923044CFu, 0xF1C3E1B7u, 0x01FE727Bu, 0x904107C3u, 0xD011DE69u, 0x17621B85u, 0xCC97D47Au, 0xF566F034u, 0x7CAF3063u, 0xD0444D59u, 0x095EDE0Du, 0x180AA718u, 0x37FE3A28u, 0xC7D8DB06u, 0xC384AD30u, 0x80B84CA7u, 
                    0xABD993F4u, 0x72B46045u, 0x5AC8E578u, 0x0CCFA4B8u, 0x4EC902D8u, 0xBBAFAE48u, 0xA16CA883u, 0xB9AE8366u, 0x779C8323u, 0x0B68BDCAu, 0x34049E11u, 0x656DE53Fu, 0x591F4218u, 0xB9F33D7Cu, 0x151268DBu, 0xE1DF523Eu, 
                    0xB0E77F49u, 0xFCCFA347u, 0x8291D8D0u, 0x664BBFDDu, 0x0E9BDBAAu, 0xFD3E3F4Du, 0xEA04B718u, 0x287AF77Eu, 0x2646F853u, 0x36DB9AC3u, 0x719ED544u, 0x1B59CB0Du, 0xC9D9750Eu, 0xF7343A01u, 0x3C4A4371u, 0xF3003FE8u, 
                    0xED4E43E4u, 0x9A43FB42u, 0xD99EE7FEu, 0xF3B6DF67u, 0x282473C2u, 0xF64557A8u, 0xEEC4CBE2u, 0x1456EB49u, 0x1EE51E6Fu, 0xDD8EF5BAu, 0xCDCE4BACu, 0xF304565Eu, 0x6009B8A5u, 0xFD0B8C4Bu, 0x5A10AB69u, 0xF19A8ACEu, 
                    0xC49D5333u, 0x1E0BBC47u, 0x1A84908Du, 0xEDE49479u, 0xC1A86EF9u, 0x92FED8D4u, 0x069AC406u, 0x0472BB7Eu, 0x8F496F76u, 0x38B870F2u, 0x51108596u, 0x428484E1u, 0xB6433B1Bu, 0xB56AFA8Au, 0x992BD9D3u, 0xB6A1AA17u, 
                    0xEFAFFA62u, 0x193930FFu, 0xA76F2015u, 0xFC3DF021u, 0x981E1EFEu, 0x1800D813u, 0x747F60D1u, 0xBD8E07ECu, 0x1DD16B81u, 0x35CDBF33u, 0x20D09ACCu, 0xFC07AADCu, 0x6C201061u, 0xCF001797u, 0xDDE815EFu, 0xAF7A2B89u, 
                    0x155190A6u, 0x6FD60B98u, 0x53963D23u, 0xFCB77F68u, 0xBD1D3BCEu, 0x21E26F70u, 0x94D9BE70u, 0xF22CE39Du, 0x2D1D7C95u, 0x51BF366Eu, 0x5EBAC998u, 0x62C777E1u, 0x211A839Eu, 0x6457E578u, 0x919B9663u, 0x882D15DCu, 
                    0x23355564u, 0x7240B793u, 0x2E42EB38u, 0x6EE08174u, 0x046A1892u, 0x1D5D2A67u, 0xADDFC66Du, 0xB82A7702u, 0x598B4268u, 0x710C918Cu, 0xFA1687C0u, 0x827C2FA3u, 0x12A66809u, 0x475BBF84u, 0x06AE4F47u, 0x42E96937u, 
                    0xE861BC83u, 0xCA1A3C5Au, 0x772DF6A0u, 0xD5CE41BFu, 0xA878B3A6u, 0xD3F35FD4u, 0x94045780u, 0x69C1A0FEu, 0xA800AD71u, 0x0379B409u, 0xE6BE2309u, 0xDDCFE190u, 0x70D81301u, 0x986A01ADu, 0x7C05DE8Au, 0x303D61B8u, 
                    0x0018162Au, 0x8C371F58u, 0x8479A7B5u, 0xC8CE3A98u, 0xD4F0B765u, 0x30B0858Du, 0x2EF2FB95u, 0xA42F2DB7u, 0xBAFF1F68u, 0x7396475Du, 0x9BC17902u, 0x52DF0062u, 0x0E5C828Fu, 0x5F249F09u, 0x21B8DC97u, 0xDC93676Eu, 
                    0x46B056EDu, 0x34E7AE3Fu, 0x3B55155Eu, 0x023F296Du, 0x760F5F34u, 0xD36B43F1u, 0x07943BD7u, 0xAC5879BEu, 0x28A14536u, 0x5B1444CFu, 0x1BF571F9u, 0xD5B853B1u, 0x0D8AAFF3u, 0xB771B20Du, 0xF5E68C8Du, 0x2498E084u, 
                    0x99C3BEA3u, 0x90180038u, 0x0E8E738Du, 0xF4FEBD73u, 0xEAF09DCDu, 0xD6B14B46u, 0xD72C19E7u, 0x93E8CDD0u, 0xB4E089D0u, 0x266F26EFu, 0xB54463A7u, 0x6FC3DCC5u, 0x758B471Fu, 0xB1C0311Eu, 0xEDE306DAu, 0xC8686411u, 
                    0x6C9BAC6Fu, 0x9A8D9BADu, 0x15D22ACBu, 0xEF251B9Eu, 0x71A8DDB4u, 0x995270B2u, 0x14C3FE3Bu, 0x91A3779Fu, 0x755CC287u, 0x53341371u, 0x8A4E6EF6u, 0xEA6E9194u, 0x2B2549BAu, 0x2F97B8C1u, 0x287EB57Cu, 0x1A8B5466u, 
                    0x98FBE338u, 0xA6A1BB88u, 0xB02270AEu, 0x10C90BBDu, 0xA94780EAu, 0xA73CB429u, 0x20F0186Eu, 0xA6FA5A93u, 0x63F94554u, 0x2E9916F5u, 0x8A09A8BEu, 0x69CE8EB8u, 0x52EA6561u, 0xBBD558BAu, 0x71CBFBF4u, 0xFDD4BDF6u, 
                    0x8FC11BDAu, 0x5DFE8296u, 0x60392A45u, 0x61EC2180u, 0x66C279ABu, 0x4ECFB964u, 0xFA643F56u, 0xA78D35E5u, 0xCB3C2DF6u, 0x408AE2D4u, 0x65B56B0Eu, 0x805F699Fu, 0x7473CFB5u, 0x9F1F4B4Au, 0x07E7C973u, 0xEB7CAE26u, 
                    0xD6B5AD1Cu, 0x96A23D53u, 0x380A6B1Cu, 0x78066979u, 0x91EC2B3Fu, 0x7B64FEA6u, 0xFA34F123u, 0x4A0ED5F3u, 0x3E35598Au, 0x5E44CB8Au, 0x1E17C064u, 0x7B5D5815u, 0x7DE58391u, 0x38F4D7A3u, 0x05415B75u, 0x223D1B6Eu, 
                    0x80E85849u, 0x41593C2Fu, 0x70FAE785u, 0x4F2415E8u, 0xA5C9C6BBu, 0x2A4E81CAu, 0x86CFDB66u, 0xC798C87Du, 0x7D6719BEu, 0xC9B73CC6u, 0x5D4CF64Au, 0x2C557F7Du, 0x40D58D20u, 0xA2AE8F2Eu, 0xCA145DC4u, 0x7082B67Eu, 
                    0x2AF31459u, 0xF32B6497u, 0xD37A05D4u, 0x4E0C3723u, 0x8DE75618u, 0xA4C83E64u, 0x7A9EA430u, 0xBECF9EF3u, 0x94487399u, 0x1DD3EF8Cu, 0xAA2DB8DFu, 0xB0CA4F51u, 0x923794DAu, 0x5904F9A0u, 0x28AB8204u, 0x375B6C89u, 
                    0x1A614B7Fu, 0x4241C859u, 0xACE69182u, 0x99117037u, 0x561B37B3u, 0xC708322Bu, 0x96EFBDF7u, 0x46D62350u, 0x3334805Fu, 0xDAED38EEu, 0xF7689A9Fu, 0x8E967FD1u, 0x4F8D4291u, 0x2F2D3375u, 0x5CBAB3AAu, 0x3E99212Eu, 
                    0xE35984ABu, 0xAFCF1BD1u, 0xF4A6F918u, 0xD50BD767u, 0xABB09DC6u, 0xFD9398DCu, 0x1D038363u, 0x79D6C4D5u, 0xFE1C859Cu, 0x6BD4DF57u, 0x501032C2u, 0x13AD38B3u, 0x0D34919Au, 0x80456BE1u, 0x386C010Au, 0x25660E87u, 
                    0x41AE27EDu, 0x339BA812u, 0x1BCFCEE0u, 0x8D482AE0u, 0x80468752u, 0xC9DC89FEu, 0x1845582Cu, 0x79E4CFF7u, 0x2ECE9E4Au, 0xCD11496Fu, 0x68220E78u, 0x54739E52u, 0xCBCCB26Du, 0xE807EEB2u, 0x7807321Fu, 0xAF31F47Du, 
                    0xB3C04470u, 0xE87A2AB1u, 0xC6A421D2u, 0x7513EF66u, 0xB436B7B2u, 0xF542B68Eu, 0x00CE70CAu, 0xB1A44D99u, 0xE37E4C5Fu, 0x382C4AD3u, 0xFDE7966Du, 0xD0FD6ECEu, 0xBCB5467Fu, 0xC9F65167u, 0x2FE5C88Bu, 0x0579BF0Au, 
                    0x83E47F1Cu, 0x66E50549u, 0x7EB964D8u, 0x73ADA73Au, 0xF0AA1916u, 0x44AEAA34u, 0x6DB041D7u, 0xB8C15A83u, 0x04349419u, 0x72C0E910u, 0xB83B1B83u, 0x85A636CEu, 0xD326282Fu, 0x8C9E3A59u, 0x27C7E54Cu, 0xB834E4B7u, 
                    0xF4E80D0Au, 0xE25C7C87u, 0x86C29D10u, 0x52E8CD23u, 0x316F9C48u, 0x30DD8CB6u, 0x37CD67E3u, 0x66E64BCBu, 0xB982DBD1u, 0xC3F8FC47u, 0x79533695u, 0x07B6B624u, 0x73989E73u, 0xF34F9F9Bu, 0xC5862C59u, 0xBF4B0741u, 
                    0x6123F4B1u, 0x7DBEC8A6u, 0x7D48FCD5u, 0xE98C9D88u, 0xE4EA0D4Cu, 0x55B33340u, 0xAB3C2058u, 0x969D7AEDu, 0x5B858DA5u, 0xE2349CF3u, 0x37A598A4u, 0xA06E5274u, 0x13142406u, 0xDC719585u, 0x39B1B1EFu, 0x4C9E8502u, 
                    0x91787401u, 0xB0FC9EE6u, 0xB26A94FFu, 0x58AA03C0u, 0x2EC96FF8u, 0x8D6F74BDu, 0x9C449A41u, 0x6C810088u, 0xADB2F3DFu, 0x335DA70Au, 0xD4C44FB0u, 0x059038F4u, 0xCE2AEF58u, 0x0951EB7Cu, 0x2E8E0827u, 0x3DD1B39Du, 
                    0x9DEBE871u,
                };
            }
        }
    }
}
