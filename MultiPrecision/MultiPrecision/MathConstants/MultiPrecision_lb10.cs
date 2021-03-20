﻿using System;
using System.Linq;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> lb_10 = null;
        }

        public static MultiPrecision<N> Lb10 {
            get {
                if (Consts.lb_10 is null) {
                    Consts.lb_10 = GenerateLb10();
                }

                return Consts.lb_10;
            }
        }

        private static MultiPrecision<N> GenerateLb10() {
            if (Length < Consts.Lb10.Table.Length) {
                return new MultiPrecision<N>(
                    Sign.Plus, exponent: 1,
                    new Mantissa<N>(Consts.Lb10.Table[..Length].Reverse().ToArray(), enable_clone: false),
                    round: Consts.Lb10.Table[Length] > UIntUtil.UInt32Round
                );
            }

            return MultiPrecision<Plus1<N>>.Log2(10).Convert<N>();
        }

        private static partial class Consts {
            public static class Lb10 {
                public static readonly UInt32[] Table = {
                    0xD49A784Bu, 0xCD1B8AFEu, 0x492BF6FFu, 0x4DAFDB4Cu, 0xD96C55FEu, 0x37B3AD4Eu, 0x91B6AC80u, 0x82E7859Du, 0x06650FDEu, 0x9DD51F3Au, 0x3E24BEABu, 0x63AD0BD6u, 0x1435EE48u, 0x0565CA54u, 0xCFF162CDu, 0x4F603381u,
                    0xF64F96C1u, 0x7D9B5D4Du, 0xFEE6C26Bu, 0x8F8C9BD0u, 0x385E5F90u, 0x1CC2D1ACu, 0x8E8F7577u, 0x5109530Bu, 0x8658C6DFu, 0xEA3EED71u, 0x304F88B4u, 0x3EB2B86Fu, 0xAA9363CFu, 0x9C1988C4u, 0xD69C4BC2u, 0x089E1F61u,
                    0x6BBB72C3u, 0xE9A54FDDu, 0x1A5E3F11u, 0x9211B15Du, 0xEFA3FA97u, 0x8C421F86u, 0x631FC105u, 0xA48D30ECu, 0xB11BB454u, 0xF2AFC692u, 0x0316D7ECu, 0xEEA2F19Du, 0x9302A515u, 0xA26BEECFu, 0x28D04370u, 0x1752B1BEu,
                    0x45A72E9Au, 0x5CBD3538u, 0x5100E14Fu, 0x4CB3C6F1u, 0x71ADFAF9u, 0x78C96688u, 0xF1721F8Fu, 0x78DE6E07u, 0x570B3806u, 0xA8F95259u, 0x17E0EC22u, 0x349FE285u, 0x1C70C438u, 0x006DD6C9u, 0x34927C54u, 0x00B04EDBu,
                    0xFD697D00u, 0x87DD3520u, 0x4BECB585u, 0x1425E145u, 0x1796C0DDu, 0x437A66C9u, 0xC347E1A3u, 0x9DCCD400u, 0x4B06503Du, 0x4706C266u, 0x84B0C4CCu, 0xCCF6077Cu, 0xBEBDF057u, 0x0DF542A8u, 0x4E6D5F96u, 0xCBFA8472u,
                    0x8DCEF996u, 0xA499D28Fu, 0x1CD7206Fu, 0x4E05F23Au, 0x02BCEE0Du, 0x72AEC8ABu, 0x73268140u, 0xAC8644E0u, 0x7DEC175Au, 0xC16DE00Eu, 0x98EA1C65u, 0xA6AC1DF1u, 0x11BBA405u, 0xF90C1FD0u, 0xACFF9907u, 0x14925323u,
                    0x0236329Bu, 0xEB346D35u, 0xED0F9BFBu, 0xD90C40A7u, 0x620ED1E1u, 0xB77093D1u, 0x7590BF76u, 0x7CA87246u, 0x2EA23EDEu, 0x5B6C16B3u, 0x80A18D4Cu, 0xD55999FFu, 0xFCB5CF26u, 0xD9C1A02Eu, 0xE76A09D2u, 0x529F4533u,
                    0x18E19934u, 0xC0518CB6u, 0xCBCF414Eu, 0x570EEA0Eu, 0x38EBBAD5u, 0x5A8D1586u, 0xFAE8ADE2u, 0xF4A6BB8Bu, 0x72AED240u, 0xC2C05605u, 0xB5272C45u, 0x5D55EAC6u, 0xF91663AAu, 0x3AEB6320u, 0x4C95F1EEu, 0x0FD6C2E7u,
                    0xD3D3F612u, 0x8AFDE8B5u, 0xEF7C9B6Bu, 0x4762B166u, 0x90B2A5ECu, 0x33C2AC28u, 0x17A58BFCu, 0x1C5515CDu, 0x98F12D33u, 0x761419FEu, 0x9D01AC26u, 0xA724364Fu, 0xDE133793u, 0xFF95D909u, 0xB4FCE6CCu, 0xA9965366u,
                    0xDB14C639u, 0xC284E917u, 0x50761AE7u, 0x88B4C27Fu, 0x3EF3B275u, 0xE503242Fu, 0xA61353F8u, 0x7D73B7D7u, 0x9A8A195Cu, 0xAE77C497u, 0x3517ED3Fu, 0xAB6BAA76u, 0x61615483u, 0xB2D8AD09u, 0x5A0935EEu, 0x78AA761Eu,
                    0x3A6F2676u, 0x58CE9597u, 0x2FD70301u, 0x3B553870u, 0x1BEFBFFDu, 0xE2B2E7D0u, 0x685B247Fu, 0x3D6FB238u, 0xFE0BC3BAu, 0x65578454u, 0x584B4948u, 0x6E40BF2Bu, 0x6CA117BEu, 0x9970D886u, 0x9B99705Du, 0xA2A147EEu,
                    0x4EEE5D45u, 0xF91A4C8Au, 0x1163F07Cu, 0xBE21C754u, 0xAE67DBD6u, 0xC7C031D2u, 0xB161369Cu, 0x6B72A93Eu, 0x05AD0858u, 0x38E9F961u, 0x2B289EB4u, 0x5A6F9C82u, 0xBF497EAFu, 0xE2837629u, 0x221D4178u, 0x93C140C1u,
                    0xFD221E87u, 0x2454B670u, 0xA8B904A7u, 0x82F54D58u, 0x583AE01Du, 0x131FE6F3u, 0x23C5AD1Du, 0x3874E7DDu, 0x56DF98FFu, 0xA2D82DD2u, 0x5C661350u, 0x0DDB22E2u, 0x6184C04Bu, 0xE32DA0CDu, 0xCD026526u, 0x39324AA9u,
                    0xEA73B898u, 0x74223061u, 0x393C996Fu, 0x67FF8747u, 0x76DDA7F2u, 0xB66BF22Au, 0x70BB9754u, 0x948FDF47u, 0x986AE3E0u, 0x5E0B7DF9u, 0x45B4C2FDu, 0x8CA7579Cu, 0x4A500113u, 0x87E02497u, 0x287FC06Au, 0x3806452Fu,
                    0xD6C118FEu, 0x081EE2C6u, 0xF721F92Cu, 0x36699BDEu, 0x5316C6B5u, 0x427CFB61u, 0x1D1BED6Au, 0xD4019A5Bu, 0x48F853C8u, 0xBE0F588Cu, 0x44F9B037u, 0x9FEC78C2u, 0xCF9374DDu, 0x26556416u, 0xA721E7E2u, 0xB40F7F59u,
                    0xAB133D7Bu, 0x7D32D1CBu, 0x5244BD4Cu, 0x57F323D3u, 0xC8A189DDu, 0xAC63C89Au, 0xC2A4C68Au, 0x8568B6D3u, 0x4543637Fu, 0x76D12213u, 0x539286CBu, 0xB7375563u, 0xB5F6DBCAu, 0x19962203u, 0xD63327D1u, 0x8200ACD5u,
                    0xE5BF57D4u, 0x6EFC0489u, 0x83603D36u, 0x161CD544u, 0xB3E6F949u, 0x89082642u, 0x9ED9D48Eu, 0x552B3EC5u, 0x00F11831u, 0x7D39D5F7u, 0x61F7CBA9u, 0xC72E4C46u, 0x2F215E7Au, 0x5574E1BEu, 0xC501EED5u, 0x20B7E7EDu,
                    0x468623B5u, 0x5DE1F24Cu, 0xA9E00138u, 0x846186F5u, 0xE44A63A0u, 0x8DBF7130u, 0xE938F77Du, 0xCC644B0Du, 0xA7E2838Bu, 0x98F4CA0Fu, 0xE6AA31B5u, 0x4DBA95E9u, 0x7F67112Du, 0xEBC895ACu, 0x0E492295u, 0xFFD29D1Au,
                    0x93DA4B6Fu, 0xA691FC41u, 0x9A6E3010u, 0x0F1F53A7u, 0x33911E78u, 0x1ADD3978u, 0x7F34CA77u, 0xFEBFDF92u, 0x5690752Au, 0x8F299DD7u, 0x79A0A17Du, 0x551746E6u, 0x84CEDF35u, 0x685D28BDu, 0xC395698Bu, 0xD11CBECFu,
                    0x4C934F08u, 0x24F773E6u, 0xAFBEFECAu, 0xE5F502E0u, 0x762EE14Eu, 0x24050E67u, 0x344EC3A6u, 0xBB2C6E8Cu, 0x1EF69E3Cu, 0xA211089Bu, 0x1A15AD9Bu, 0xACBB3B95u, 0xC0858FFAu, 0x96B279FFu, 0x04382639u, 0x3A5BFDC5u,
                    0x75FA3DCEu, 0xDDDA14AAu, 0x3D630790u, 0xF19A38B0u, 0xB2C2CF3Du, 0x71EDD8DFu, 0xF96C5612u, 0x95F40CF7u, 0x49234FDCu, 0x28D046CAu, 0xDA99C855u, 0x51799E55u, 0x1C596E21u, 0x503EC269u, 0x3BFFCD76u, 0xCF726BCCu,
                    0xEBC27DF0u, 0x78EAFA1Eu, 0xDDB66305u, 0xD9F8D119u, 0xCE590BA7u, 0xFA46E1B5u, 0x5B71BFD9u, 0x25E46693u, 0xB4A0257Au, 0x74E7EE8Eu, 0x5CD3E016u, 0x230FA3D2u, 0xB9153E6Au, 0x4491C31Au, 0xBC838BD5u, 0x09B1B105u,
                    0xA5A4CBCBu, 0x60DBEEF1u, 0x1D5BEF81u, 0xD71FB551u, 0x2E0ADFF9u, 0xF31F2009u, 0xB8BAF908u, 0xDE7B1DB1u, 0x384A93DBu, 0x269B18E5u, 0xAC405992u, 0x9B334C72u, 0x8A255D80u, 0xFB009A63u, 0x6C7B95DDu, 0x975C51BFu,
                    0x25CE7C71u, 0xCC06CAF2u, 0xF378F356u, 0xE51DD0BCu, 0x53BC0F4Bu, 0x84C2CE2Bu, 0x420C29FCu, 0x3CFBA8B9u, 0xC5B9BD75u, 0x9618F0D2u, 0x3FDD6112u, 0xD1A21CF9u, 0x3A63D150u, 0x2BCFCCACu, 0x11FF5A84u, 0x58BAEE73u,
                    0x22B100EAu, 0x997EB2F0u, 0x28C1627Bu, 0xCE8D2EA6u, 0x1C941F3Eu, 0x10D6468Bu, 0xA363116Du, 0x4D7AD283u, 0x7DBE53FEu, 0x2D458DDEu, 0x6B58C13Au, 0x3D60FCF5u, 0x337AA728u, 0x74B71948u, 0xB8CC15A8u, 0x8D403812u,
                    0xBEEFCDF2u, 0x225AD80Cu, 0x61DA2B61u, 0xBCB4693Bu, 0x3B292E85u, 0x10333180u, 0x13D49867u, 0x0A41AE6Du, 0x37E07F3Cu, 0x07402D96u, 0x35C1FE5Eu, 0xAABC162Du, 0xB6650E0Cu, 0xF6ECA7A6u, 0xFF5B8C03u, 0x6CDBDAC1u,
                    0xD2384721u, 0xB34B0E8Eu, 0x937D7192u, 0xC191398Fu, 0xE15AAD82u, 0xD5A308B6u, 0x9276E85Du, 0x239A48D7u, 0xA096E76Fu, 0x40270A1Cu, 0x528F5A79u, 0xFB68B17Bu, 0x224BBA66u, 0x1C16277Au, 0x92F20377u, 0x0763A080u,
                    0x393D4B78u, 0x28F594F8u, 0x2C512359u, 0x46A62E9Du, 0x64FA7D76u, 0x049610D4u, 0x7E0788C5u, 0x4CB1AD96u, 0xC0D345ADu, 0x2FCCB06Bu, 0x64904B95u, 0xB5D2A707u, 0x69890D59u, 0x73A1ED3Fu, 0x18C9974Fu, 0x2CB7A49Au,
                    0xC9EC669Cu, 0x126742FAu, 0x7B5F5571u, 0xD4B05040u, 0x6CCC53A8u, 0x78E55646u, 0xF36D6A7Bu, 0x4B25CF58u, 0x10623E04u, 0x3CE406C1u, 0x0DA1771Eu, 0xF4AED8A7u, 0x7E5F09FFu, 0xDFF505A4u, 0xA57B3697u, 0x9D62247Bu,
                    0xDD6EC560u, 0xFA17FD42u, 0xCA2CBF3Eu, 0x0F80D6FCu, 0x3A2ACC25u, 0x91229719u, 0xAB99CF58u, 0x4682F31Cu, 0x6504A4ECu, 0x8B1AEA0Cu, 0xDBC77693u, 0x95F37FCCu, 0xC9E1ABF2u, 0x17AC7EBEu, 0x62818BCDu, 0x5A1B6C30u,
                    0xFA2091D3u, 0x83EADCA2u, 0xEC9EDC32u, 0x7DC12811u, 0xAE01478Eu, 0xC9AD0744u, 0x57D20B9Du, 0x2D6723B7u, 0x008F9C04u, 0x33D2E9E3u, 0x96176E02u, 0xE16CE85Fu, 0x1382C237u, 0x0D3B15ECu, 0x0630642Fu, 0x03A778ACu,
                    0xFDB6F73Cu, 0x9A53B0B9u, 0x5D76519Bu, 0x7FE2901Du, 0xFD7E6670u, 0x6AC9AEF2u, 0xDE52111Du, 0x9BA0601Cu, 0x782040B3u, 0xAA608974u, 0xA5F4559Du, 0x88402D96u, 0xB678BD41u, 0x19C91433u, 0x2B294C1Eu, 0x315B74AAu,
                    0x5883D2D7u, 0x90E1C43Cu, 0xBB4F3425u, 0x88FC9C3Eu, 0xEB58E97Fu, 0x4A19D3A1u, 0x045063B3u, 0x0825C5CDu, 0x5A5541C3u, 0xEA278F24u, 0x54A7D115u, 0x859100F8u, 0x871935FDu, 0xB0F4CEA9u, 0x8813F31Au, 0x347D2754u,
                    0x291E7F3Du, 0x09035A5Fu, 0x1C7694C0u, 0x778CD587u, 0xFE8CD301u, 0xEF658296u, 0x3046BE5Du, 0x70B17C4Eu, 0x84E65411u, 0xE6DBABFAu, 0xE494EC8Cu, 0x7F4CD69Fu, 0x07A0DBFEu, 0xC4F75F30u, 0x09B7F3EAu, 0xC2C49EB0u,
                    0xB9F1B007u, 0x3F8306CAu, 0x25E37E48u, 0xC66436E0u, 0x5A2AF43Du, 0xDE501553u, 0xCCB2D13Cu, 0x54844E83u, 0x37AA7B22u, 0x6893457Du, 0x556EDBCFu, 0xADC0388Eu, 0x3D60979Du, 0x0EE0262Eu, 0x0DD39D8Du, 0x439DA2E1u,
                    0xE8D6F7B2u, 0x76EF4383u, 0x02EF84E1u, 0x8380F8F4u, 0x8CAB21E7u, 0x17089116u, 0x989DBB41u, 0x5610D763u, 0x8C505CACu, 0x31F7B1A0u, 0x7A90C7EDu, 0x0C32B344u, 0x18DB15B8u, 0xB7F75A05u, 0x4E59BE05u, 0xF2117E94u,
                    0xF6A1141Eu, 0x0642FC43u, 0x0DCD7F45u, 0xC210BB6Cu, 0x10CB08ADu, 0x18D9FE70u, 0xA6D68F0Au, 0x08F8023Cu, 0x228D4C52u, 0x2D66D574u, 0xAA2D9848u, 0xFAE3E101u, 0xFC308830u, 0xD2F0E35Bu, 0xB7A1C1B8u, 0x051B3C68u,
                    0xAAC2B908u, 0xBA9A72CCu, 0x2B86F9B3u, 0x010E574Eu, 0xC5D2F6AFu, 0x55627BB0u, 0x51134833u, 0x7508D76Cu, 0x30FF4B6Bu, 0x0F5072E9u, 0x24B0B48Du, 0x425FA567u, 0x34E70E96u, 0xA91BD4F4u, 0xC9441020u, 0xD92CBD82u,
                    0x984A9068u, 0xD2617213u, 0xFF0F18A6u, 0x2AD77257u, 0x88552552u, 0x8EE4BFD4u, 0x3EA64F0Eu, 0xFAEEA5D4u, 0x4D0FB93Fu, 0x616E1A5Bu, 0x54B5B4F9u, 0xD6F38428u, 0xF64D1183u, 0xAE09A4BFu, 0x9C3F6CD9u, 0x7CB2CC4Cu,
                    0x3A36FF54u, 0xDA0D6A13u, 0x8331AAB3u, 0x00DC7A23u, 0x830490EAu, 0x5BD058F3u, 0x60CD2B22u, 0x2834E6F1u, 0x9B9E2530u, 0xCAE2061Bu, 0x1D06C1B0u, 0x32E3A6E1u, 0x44498C23u, 0x59864D85u, 0xBEE1009Au, 0xA4D5486Au,
                    0xED53AF2Eu, 0x635F2868u, 0xF5734BF9u, 0xAA44D0A4u, 0x018CC7A7u, 0x8C1A9E27u, 0xD7116A40u, 0xCC954ACEu, 0xEDE0B43Eu, 0xDA311C0Fu, 0x3307E4E4u, 0xAA42CA54u, 0xFDB2E269u, 0x926167F3u, 0x7650A2CCu, 0x1A5894D2u,
                    0x3E80BA4Du, 0xD89D8436u, 0x40E50746u, 0x962C6470u, 0x9CDF49A1u, 0x2CFDA16Bu, 0x505AE685u, 0x7A600615u, 0xC3DAEFBCu, 0x84FB2F8Bu, 0x4ECD7B8Du, 0xAB0B82D9u, 0x7CB0C3D2u, 0x00A60762u, 0x7EEADEA5u, 0x7816099Bu,
                    0x8584CBE3u, 0xBD289914u, 0xB9A9665Eu, 0x62FC9A08u, 0x01FD60C1u, 0xC643F47Du, 0xC7B37673u, 0xD317B0D6u, 0x4ACA5B42u, 0x73F5FD6Du, 0x892D071Cu, 0x3AA42534u, 0x409243BAu, 0xBADE227Du, 0x6D1A4602u, 0x1DE9FB3Du,
                    0x0ADC4273u, 0xA7DEC99Au, 0xF6194655u, 0x3B3B463Bu, 0xEF1C62BAu, 0xB76BC7CDu, 0x7E850A9Bu, 0x83A1B722u, 0xE3EB8DF4u, 0x252C1E90u, 0xBFAD434Fu, 0x239A0C31u, 0xE8A3667Fu, 0x8D920780u, 0xA886C625u, 0x20A7C25Fu,
                    0x88019064u, 0x2E1A2B48u, 0x5820EE06u, 0x787D0C00u, 0x109C5745u, 0x0F00C7E1u, 0x53C345ACu, 0xC467EEACu, 0x32FB4D5Cu, 0x68E661EEu, 0x253029ABu, 0xBA28F906u, 0x2331B738u, 0xF1B198ABu, 0x85204578u, 0xFA263600u,
                    0xD3A6E5B0u, 0xA9CFE685u, 0x37349DB2u, 0x0E63A350u, 0xDD4CCCFCu, 0xA350283Bu, 0xF37BA9F6u, 0xE457159Bu, 0xE50B2B7Eu, 0xA39C5A4Du, 0x6A102C76u, 0xC6489D51u, 0x04349678u, 0xFEFC7FAAu, 0xCC4D04A1u, 0xD57BE40Du,
                    0x474A8897u, 0xC63CC762u, 0x282F8856u, 0x92BE2787u, 0xB18DDA78u, 0xC7F59598u, 0xE44CF62Fu, 0x39120FBCu, 0x8539D11Fu, 0x703248F7u, 0x7F253321u, 0x16317D8Fu, 0x41728D99u, 0x7882DBEFu, 0x65D8D64Au, 0x42A68EBBu,
                    0xAF350B4Bu, 0xF08801BDu, 0xFC2F0095u, 0xD67FE51Fu, 0x57DAB9BFu, 0x63C21365u, 0x55D304C4u, 0xAAFC278Eu, 0xC5BB3695u, 0xE08321C3u, 0x4D45FD23u, 0x8E68E061u, 0x905A5BFBu, 0x1E26C377u, 0x57D1D5FBu, 0x4944A7FBu,
                    0x330C6B13u, 0xA91E861Cu, 0x14DD904Au, 0xB8557B29u, 0x77AFA03Cu, 0x18D406C6u, 0x2E17532Cu, 0x0500B46Au, 0xB6F05910u, 0xAC3382E4u, 0x518C569Du, 0xD666E39Fu, 0x88D32CBEu, 0x98B939BAu, 0x3598A3B2u, 0x4DAB4BDCu,
                    0xEFAF8B4Cu, 0xA0BA0845u, 0x5D3CD92Du, 0x5AF9E4DBu, 0xDD74E1FEu, 0x53205783u, 0x68DD48A7u, 0x8590657Du, 0x46A70503u, 0x1DB97E3Bu, 0xD4DD62E4u, 0x738ED5E5u, 0x2F346848u, 0x72F33FF5u, 0xF67778A7u, 0xE0B69392u,
                    0xB49468DCu, 0xD9BFEB6Du, 0xB40F5075u, 0xA9F829B2u, 0x6D3A3652u, 0x393264E1u, 0x6A544780u, 0x6F5DFBBBu, 0x4816F180u, 0x9ED78B60u, 0x2476AF4Du, 0xA40094D8u, 0x68B91316u, 0x4A02B80Du, 0x85D10E90u, 0x72C668B5u,
                    0x5F26CE9Du, 0x92894AC6u, 0xE3A1982Fu, 0x142D14E2u, 0x88C6A571u, 0x887A78E0u, 0x496D2652u, 0x8CF5182Cu, 0x270F57D0u, 0x0CD33549u, 0xB73407C9u, 0x2EFC1FE2u, 0x6E948B7Au, 0x5C4E5F42u, 0x790C9C3Eu, 0xCC01DA04u,
                    0xA697B613u, 0x04C97868u, 0xC0F54167u, 0xA5AAFADBu, 0x77D9C389u, 0xF273847Fu, 0xCA3A0AF7u, 0x993F99CEu, 0x778CB037u, 0x04CA9F52u, 0x4444F930u, 0x99B85720u, 0xEDC553A7u, 0x1C45E3AEu, 0xF8113DA8u, 0xF46C7FB5u,
                    0x86F13A8Bu, 0xA4DFE9E6u, 0xE90DB5BDu, 0x53F6880Cu, 0x51DCC9ACu, 0x0B33BBACu, 0x761B7E52u, 0xFE05A2EBu, 0x0299E9B3u, 0x08FBE5AFu, 0x8036FA99u, 0x7040589Bu, 0x8D12E545u, 0x295BC74Eu, 0xD09352F1u, 0xFA621AB0u,
                    0xE0BDF244u, 0x15ED7EB7u, 0x1A33C9D7u, 0xCA3ABC62u, 0x4BDA82E7u, 0x2DD2E3ABu, 0x8BD5B25Cu, 0x9ADB2E5Eu, 0x14D5E747u, 0x9B3534D3u, 0xC185C61Du, 0x4503439Du, 0x0D0E3CD3u, 0x9406CB31u, 0x4FDF032Bu, 0xAA28075Du,
                    0xF71E8D57u, 0x033452ABu, 0x947DC6BDu, 0x363FC8A0u, 0xCCF1D260u, 0x994AF882u, 0x832F861Fu, 0x0425E580u, 0xFAC56E83u, 0xD68C1063u, 0x6A85E01Cu, 0xE5A3BAB6u, 0xB5192EA5u, 0x9AD11A66u, 0x39E8F901u, 0x5B837897u,
                    0x8CF21A12u, 0x86FDCD62u, 0x40733A5Bu, 0x40892828u, 0xC43B47F9u, 0x72D5FFE4u, 0x6CBED142u, 0x0A925CDEu, 0xD0E49EC2u, 0x6DA136FBu, 0x486BBD8Cu, 0x589E71E7u, 0xBBE7AE12u, 0xBEA5E7A5u, 0xB7386A8Fu, 0x4558FE20u,
                    0x74F3CD4Eu, 0x739DFF8Cu, 0x210738BBu, 0x425BB0F5u, 0x59D348AAu, 0x7CC5351Eu, 0x04A45626u, 0x315F2042u, 0x42A47C82u, 0x4ED128A1u, 0xEE205B89u, 0xCA06FACFu, 0x5C8893C3u, 0x540783A0u, 0xA51EF4B5u, 0xA3FF12E8u,
                    0xE8CA6901u, 0x8FAC33C7u, 0xE0E2FEE0u, 0x9C168C0Fu, 0x0D951AB2u, 0xDD3BE51Eu, 0x692B878Fu, 0x3E1A83F9u, 0x3731D204u, 0x275A76CDu, 0xF27315DEu, 0xA59AA7CFu, 0x86F80590u, 0x46F24116u, 0x4D5623F7u, 0x061EC84Cu,
                    0x2F7B3702u, 0x08849939u, 0x15502E46u, 0xBBE75119u, 0x86BECC2Au, 0x98F0E208u, 0x3F350FA7u, 0xB3843FF9u, 0xEE283BC7u, 0xDCB45A1Au, 0x124AD895u, 0xF31A5B10u, 0x18C328E0u, 0xCE8E27DDu, 0xA578B5C8u, 0x4F5971FDu,
                    0xB69452ABu, 0xEAE633B6u, 0xF8ABAC39u, 0xAEFE0127u, 0x8972CC6Bu, 0x9BA85908u, 0xA1A320EBu, 0xF7127EB9u, 0x581807A6u, 0xC9725D3Bu, 0x6282371Du, 0xC22BEFBEu, 0xAADCB4BFu, 0xF93D0A8Eu, 0x7A6A0579u, 0x643B69E1u,
                    0xC3FBBFB0u, 0x2E5643A5u, 0xA3F3331Cu, 0x4222CA58u, 0x44B7BB02u, 0x95A396E6u, 0x2BAF81D2u, 0x5EFBF5AFu, 0x620F45D0u, 0xAE3A1464u, 0xC915D46Bu, 0xA71CE9BDu, 0x6FA72722u, 0xEAAE1621u, 0xFF57DAEDu, 0x1DC8E664u,
                    0xD54D6613u, 0xE4810082u, 0xB8A7921Bu, 0x6028B9A7u, 0x62212F62u, 0xE8F3DEC7u, 0x81BDFD33u, 0x7A3957A6u, 0x4814F169u, 0xC0018B78u, 0xE15E90A1u, 0x59E4E737u, 0x438D31DCu, 0x66D7B366u, 0xDC2916B2u, 0x1BA9121Bu,
                    0xA11C8BF7u, 0x7EF02123u, 0xEFD394CAu, 0xB049EB1Au, 0x064240E5u, 0x2161484Cu, 0x0C8A07D3u, 0x06A19DAFu, 0xFFF4BC3Bu, 0x12CE55DDu, 0xA5A6CC13u, 0x708FDA73u, 0x28665526u, 0xC9BA4878u, 0x71823364u, 0x815B0C4Au,
                    0xFDA13731u,
                };
            }
        }
    }
}
