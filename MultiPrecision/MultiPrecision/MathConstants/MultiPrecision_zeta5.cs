﻿using System;
using System.Diagnostics;
using System.Linq;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> zeta5 = null;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> Zeta5 {
            get {
                if (Consts.zeta5 is null) {
                    Consts.zeta5 = GenerateZeta5();
                }

                return Consts.zeta5;
            }
        }

        private static MultiPrecision<N> GenerateZeta5() {

            if (Length > Consts.Zeta5.Table.Length - 1) {
                throw new ArgumentOutOfRangeException(nameof(Length));
            }

            return new MultiPrecision<N>(
                Sign.Plus, exponent: 0,
                new Mantissa<N>(Consts.Zeta5.Table[..Length].Reverse().ToArray(), enable_clone: false),
                round: Consts.Zeta5.Table[Length] > UIntUtil.UInt32Round
            );
        }

        private static partial class Consts {
            public static class Zeta5 {
                public static readonly UInt32[] Table = {
                    0x84BA0C76u, 0x53E66DBDu, 0x118271E8u, 0xCCFFA309u, 0x85FF4442u, 0x764FCBB5u, 0x1A7F1C89u, 0xA8016931u, 0xEA4AA855u, 0xE1F262AFu, 0xC0BB694Bu, 0x36E974D1u, 0x427C1EEAu, 0xE07933D9u, 0x5F5AA7C7u, 0xB41F68BFu,
                    0xACAE6E24u, 0xB3228141u, 0x8F87DDF7u, 0xBFC0DE30u, 0x0DBF21E3u, 0xB2FB822Au, 0x9DEB2B64u, 0xBE06AA10u, 0x315398BBu, 0x98DFC2D5u, 0xB6F1B204u, 0xD07E6E6Eu, 0x300222DCu, 0xB74637B3u, 0x977021CEu, 0x7560E7B1u,
                    0x855DDDF3u, 0x2795A65Au, 0x374690B3u, 0x273ECF25u, 0x204103EBu, 0xF038A6EFu, 0x0842E074u, 0x23318AB2u, 0x4D783C27u, 0x16146ADCu, 0xF805AA3Eu, 0x35039002u, 0x15CDEDDCu, 0x69D61F87u, 0x353328BFu, 0x9B7AEA3Cu,
                    0xF9A13668u, 0x62F46606u, 0xCC2A1AB0u, 0xEC321ED9u, 0x16E8CAD6u, 0xA4F5EC98u, 0x13156CEEu, 0x5FC87AC6u, 0x508EA84Eu, 0x3AAD3478u, 0x47BCA786u, 0x2941FE65u, 0x8F615FE4u, 0x0040F296u, 0xF0DC7185u, 0xBF8BAF3Bu,
                    0xA6C9CAB7u, 0x63812B22u, 0x7D6D9956u, 0x9C625EA5u, 0x42B15CDBu, 0x7C60FA36u, 0x7BF00B7Au, 0x0E71E3E4u, 0xF75F8D98u, 0xC5A3C572u, 0xA1C980FCu, 0x40B113CBu, 0x18B42F6Eu, 0x6DB9DC15u, 0xD88499F5u, 0x98AFA72Bu,
                    0xFDCF090Du, 0x98B49893u, 0xFDA543F6u, 0xA0B5CBB7u, 0xF782CD91u, 0xDD3F7AEEu, 0x43452A7Au, 0xA349013Du, 0xF71369C9u, 0x999B374Du, 0xD57E0E1Du, 0xDE7A8094u, 0x36363729u, 0x438DC39Du, 0x7E56A457u, 0x82ADA1CBu,
                    0xA68E0FC9u, 0xEDA1675Cu, 0x93473DE1u, 0x08ACA292u, 0x73AC90DCu, 0x9E020C94u, 0x40051FE3u, 0x7C0C7058u, 0xAB7A451Bu, 0x729263B3u, 0x31913DFBu, 0xED6380EEu, 0xEC5EC2F2u, 0xF7426547u, 0x0701DD74u, 0xB66486E3u,
                    0x67F6FC33u, 0xE4B2EFB5u, 0xC1107557u, 0xCEFA5107u, 0x62D1EE13u, 0xAE6DAFF5u, 0x26B419E9u, 0x29F88029u, 0xA2BD0794u, 0xFD31E01Eu, 0x7E3402FAu, 0x6A5D8C9Bu, 0xAA95F78Fu, 0x6D45AEC6u, 0xAEA8059Bu, 0xEA00F2FFu,
                    0xE7ADD247u, 0xADAAC96Cu, 0xC1EFBA97u, 0x26AE513Fu, 0x4EBCC945u, 0xF255AA28u, 0x68B2FB05u, 0x71374C3Du, 0x4C030D6Eu, 0x3AFF6119u, 0x7DC09F25u, 0x73DE5B92u, 0x77F21487u, 0x5F251F99u, 0x1BE7BE9Eu, 0x4ADD1A19u,
                    0x52E6EF62u, 0x64702A9Eu, 0x63ACF1F1u, 0xE037D2E3u, 0x00D2D665u, 0x82531A79u, 0xD51F7999u, 0x4EECB9EAu, 0x8C523859u, 0xCC04ACABu, 0xEEA5E869u, 0xF3F5202Bu, 0x5F6BD0D5u, 0x8329923Fu, 0x547C8D6Fu, 0xD0DEAFD8u,
                    0x3B2ABE36u, 0xE75B7ACBu, 0x5EEE48CFu, 0xA937E897u, 0x5BC5CFF2u, 0x2D5C6DB9u, 0xB0E777ADu, 0x59210D18u, 0x45289706u, 0xFFAE5280u, 0xE3155498u, 0xE85FEA40u, 0x9D44EB19u, 0x2C6AF86Eu, 0xC07BCE96u, 0xFBBC48C3u,
                    0x13A82EE5u, 0x2D225B85u, 0x17BA63C8u, 0xE605FFECu, 0x1EC91A9Du, 0xF1EA6A7Eu, 0x9E78BA01u, 0x6DC65760u, 0x3599364Eu, 0x4BC15C8Eu, 0xA5CD8B06u, 0x481EA580u, 0x22B938F7u, 0xA176893Fu, 0x37ECA4C6u, 0xF12F28FCu,
                    0x98ECE320u, 0x94462FB4u, 0xCED5E864u, 0xEABF3AC8u, 0xC8956CEFu, 0xE95A74C9u, 0x8DF3602Du, 0xAD7C8060u, 0x0D9C4414u, 0x522AC225u, 0xFDA74854u, 0x4634356Cu, 0xEA505666u, 0x91EFA37Eu, 0xA628556Cu, 0xDBA06B64u,
                    0xEF6F309Eu, 0x00E5DFAEu, 0xF4FAF2EFu, 0x5DFFD046u, 0x1E2C1FD9u, 0x38C97FE3u, 0xE18ADBE0u, 0xDF6154CCu, 0x1CE28EA5u, 0x59C6EA5Bu, 0xDA8D9119u, 0xDF97FF4Du, 0x114F181Bu, 0xBEC4D7D5u, 0xBD4CE1E3u, 0x69166657u,
                    0x7EDEE3ECu, 0x5E64DBA1u, 0x4F9C7DB0u, 0x312C3763u, 0x8B0C354Eu, 0xB3347E58u, 0xBAE18959u, 0xB5324BB5u, 0x673C215Fu, 0x23E2091Eu, 0x3B51E0D7u, 0xF0C5AAB2u, 0x5150ADAEu, 0xB0B84DD3u, 0x7D9474EDu, 0xBCC1B46Bu,
                    0x164C05BBu, 0x577FC5B4u, 0x07DB3F2Eu, 0x85D296C7u, 0x9BDB390Bu, 0x7FD3C9D7u, 0xD88578CFu, 0xF3E11AD2u, 0xC96961C1u, 0x836C216Cu, 0x3B9945B8u, 0x03C31EE5u, 0x81727C9Cu, 0x3F5AF8F7u, 0xA7042A85u, 0x6FAD8F94u,
                    0x61FAD3FAu, 0x5184F0FBu, 0x3E4F2432u, 0x4AAC369Du, 0x7B07F3AAu, 0x60425397u, 0xFE726217u, 0x16C318E2u, 0x07163D24u, 0x92EAC524u, 0xD78B49EDu, 0x19971874u, 0x42ECDA54u, 0x37498ECCu, 0x4B006254u, 0x97E39206u,
                    0x36A5F6D2u, 0xCFD802B6u, 0xA47B91C5u, 0x514F31CFu, 0x1146708Fu, 0x65ADC437u, 0x2FF21603u, 0x048F4CA6u, 0xFBFBA808u, 0x9F825948u, 0xEEA5951Bu, 0xC5E2D4BBu, 0x91B9564Du, 0x7AF873E3u, 0xAC8822E0u, 0x3D95E108u,
                    0xD4DB5671u, 0x6AF667EAu, 0x77B74B28u, 0xD0EFE359u, 0x181D3828u, 0xCA2DF5EFu, 0x900DF44Bu, 0xA5D25B17u, 0x5FA9CB2Fu, 0xCCD2BF01u, 0x5BE5A5E8u, 0x3FDF76A1u, 0x5E9C928Au, 0xBC142B14u, 0x328DE694u, 0x8BE10C70u,
                    0x1E939A62u, 0x0D69C7A2u, 0x995C522Du, 0xBDE1C961u, 0xD5336571u, 0x6E550997u, 0x1420340Bu, 0x2510F62Bu, 0x60F6D29Cu, 0xB3217790u, 0xE7C7331Bu, 0x168F3C5Cu, 0xF68B8032u, 0xC27FC030u, 0x38E9AC2Eu, 0x095DE601u,
                    0x044939F5u, 0x0602DC10u, 0x6EF6559Eu, 0x6FEE1592u, 0x0BFF75A9u, 0xC29D212Du, 0xD0F5C878u, 0x74441BB0u, 0xDB11A67Du, 0x0E9233D4u, 0xC9E2F76Au, 0xC63FC863u, 0xAC1F46DEu, 0x1DAE5A66u, 0x2AA3B561u, 0xF2CF35DBu,
                    0xA12780CEu, 0x8E8B9D27u, 0x228E046Au, 0xCF2309EBu, 0x274B5CF2u, 0xC8880B2Bu, 0x3F879565u, 0x28FE479Fu, 0xD26CD3BFu, 0xC6521497u, 0xAF326068u, 0x3133B63Bu, 0x16C2642Du, 0xF7EE90BAu, 0xD2340915u, 0x750079F7u,
                    0x0F7F4779u, 0xBD09C800u, 0x6369C0C2u, 0x89D0A985u, 0x111F6976u, 0x18F725DBu, 0xAA8ABA2Cu, 0x305127F6u, 0xD6A0C7D3u, 0x13BF38C0u, 0x8C983C37u, 0xECCA43AFu, 0x0C3DD444u, 0xB58E9B59u, 0x08DF845Cu, 0x97E82B1Au,
                    0x7201FF52u, 0x35102588u, 0x87157542u, 0x4F502902u, 0xBE74A08Au, 0x6086A40Fu, 0xB1ADECDDu, 0x4AA291CCu, 0x50C168ABu, 0xD7690F8Eu, 0xCE266CEBu, 0x05E557E4u, 0x46C518D2u, 0xD7722407u, 0x9113BAB4u, 0xF2773A78u,
                    0x26F99D4Au, 0xA6D9C778u, 0xC746CDE1u, 0x822EDD4Bu, 0x47218922u, 0xAE279A7Au, 0x2AEC35C7u, 0x137017A7u, 0xF5DEC002u, 0x638C6E76u, 0xADBF2A73u, 0x5B8AA78Fu, 0xE657562Au, 0x13E2D40Du, 0x99AFB65Fu, 0x7273948Au,
                    0xA2189E7Au, 0x568FC551u, 0x384E845Fu, 0x23041D7Cu, 0x7CFD6A2Eu, 0xBE721BC8u, 0xE62E32F6u, 0xEF362BF7u, 0x46F728B3u, 0x9DE847C3u, 0x2F674184u, 0x4ECDA3F0u, 0xC6AB670Du, 0x9D381F60u, 0xEAA1CB1Eu, 0xB0E684BBu,
                    0x4822EEEDu, 0x2E186706u, 0x3BA6E647u, 0xD458E6CBu, 0x1094ABD2u, 0xAA6AB917u, 0x7E975A59u, 0x693CE2FBu, 0xB462AE8Bu, 0x186CCEB7u, 0xCF8DCF76u, 0x4AE92308u, 0x66DEE4F3u, 0xA52D9CFBu, 0xF8D9EB5Bu, 0xD9A08160u,
                    0xE6B5A613u, 0x1122F080u, 0x35737B40u, 0x9FC0F693u, 0x3B318AD9u, 0xB5E18E95u, 0x08E2F4E4u, 0x170A6598u, 0xA1208C69u, 0x6CCD9762u, 0x6B6CE8D5u, 0xBA3E4A92u, 0x12956A77u, 0x4CC7D92Au, 0xBBCB4357u, 0x13A31F16u,
                    0x8D96F432u, 0xF51E346Cu, 0xD4A3FCF3u, 0xA1D7D2B4u, 0xD6B341B2u, 0x8A30513Bu, 0x9BD5FDE2u, 0x14D1BAB3u, 0x93CFE3A3u, 0x8F0128B1u, 0x289CEFB1u, 0x82BDB3FBu, 0xC38956B1u, 0xC211FF7Bu, 0x93249DDAu, 0x6ED8E518u,
                    0x7B06FAF5u, 0xEA47F40Du, 0x1CAD92A3u, 0x79C9222Fu, 0x43F1F909u, 0xA1DF782Cu, 0x1771F2DCu, 0xE54101D2u, 0x94CA070Du, 0xA28CEA30u, 0x1371E5B4u, 0xC804954Cu, 0xA3A5EC89u, 0x03B3C47Fu, 0x0336F01Cu, 0x02840829u,
                    0x5C736519u, 0xFC8FC344u, 0x09951150u, 0xDC0F2299u, 0x8AEB54E7u, 0xBBE3C0CDu, 0x06BF8BB6u, 0x58674DACu, 0x4968D202u, 0x028553E0u, 0x30BF83E1u, 0xE8C175F4u, 0x56C65421u, 0x48EE30F8u, 0x7330595Au, 0x365288E0u,
                    0x5D30E5C9u, 0x7DC8B0BEu, 0xE6835012u, 0x48BC196Cu, 0xCB52DFE6u, 0xF1E2AF0Bu, 0x7046CDC0u, 0xCA7053E9u, 0x0B39A70Eu, 0x2EEE5572u, 0xB90515FAu, 0xEA90318Bu, 0x68475D61u, 0xB9403468u, 0xB93DEF9Eu, 0x84C6A6EEu,
                    0x16CADB35u, 0xBDCB3A4Cu, 0xE5913674u, 0xACF9CE98u, 0xF387D1FBu, 0xCFFB80ACu, 0x4A89F26Du, 0x2F01111Eu, 0x349529FCu, 0x2CDDD615u, 0x175E9D95u, 0x71CF08A9u, 0xE9FA1968u, 0xF2C5A73Bu, 0x462EFED8u, 0xDBDE5657u,
                    0x6973CBDAu, 0xA6FD37CAu, 0xAD99DBDAu, 0xE6571BC2u, 0xF923CCD4u, 0xFF2EFD49u, 0x1DEEFAAAu, 0xCA4D5163u, 0xAE945DC6u, 0x4DE9904Cu, 0x8A73ABF8u, 0xE0A366A1u, 0x9A71B422u, 0x7E994CDEu, 0xAF27B76Cu, 0x7F650129u,
                    0xCCD4EB57u, 0x79B1867Fu, 0xB5D3F0A5u, 0x1BA8AB0Bu, 0x2C290036u, 0x7BA80F6Au, 0x6294E849u, 0x14352D74u, 0xC6489DD0u, 0xB3FC6186u, 0xBA5BD8C7u, 0x390CC089u, 0x84F1BAD0u, 0x1DF8D4FDu, 0x52314934u, 0x5D329F5Cu,
                    0x6D8D8794u, 0x8F921394u, 0x229602EEu, 0xC1698C62u, 0xC4E5F01Du, 0x4836034Du, 0x39022766u, 0x02BB81EAu, 0x07944311u, 0x72AE4950u, 0x999AB296u, 0x912CDEDFu, 0x344ECAEEu, 0xD4612C38u, 0x5DA03FF6u, 0xCC12F856u,
                    0x9262511Eu, 0x4527D680u, 0x17C46E6Bu, 0xE0ED3A7Au, 0x91303754u, 0xE9DBD4C8u, 0x24689C28u, 0x713BD204u, 0x3A22E220u, 0x8267F2D7u, 0xAEA3A0E4u, 0xF6A1CFA6u, 0x8F7383D7u, 0xFF1536DFu, 0x4AA0819Cu, 0xB76EA4DEu,
                    0x5FFBCFAFu, 0x9FCA63C7u, 0xECFD54E0u, 0x00AB7EB8u, 0x236FE6D1u, 0x3A8E2E56u, 0x702E662Au, 0xD020C102u, 0x87BA5FC1u, 0xE675A962u, 0x35181968u, 0x7CDF082Fu, 0x4403B100u, 0x81061001u, 0x552E3F1Eu, 0x474553F5u,
                    0x884BF584u, 0xC7E22FF4u, 0x21D185CDu, 0xF81BFA68u, 0x3904142Du, 0xF00A9BEFu, 0xCB921C64u, 0x60D0B543u, 0x59412CC5u, 0x9F95F7FEu, 0xD8F2C6CAu, 0xA037D9BEu, 0x9A081E86u, 0x289D8AB5u, 0x9FAA8C37u, 0x13278C98u,
                    0x8D87C639u, 0xAFE225A4u, 0xE5DC8F14u, 0x26D2EC86u, 0xBBEFF0CDu, 0xD14244F5u, 0x932E28C2u, 0x49C30B38u, 0xCC23A2D7u, 0x71CC869Bu, 0x632FC5EAu, 0x727B45A7u, 0xD2190840u, 0x49D60081u, 0x9CF0E8F3u, 0xB6BD984Cu,
                    0xBFD1F6C7u, 0x88FB4989u, 0xBE7209C3u, 0xB88E0888u, 0xDC041310u, 0x8B3D9CC0u, 0x049E5812u, 0x39454EF8u, 0xC29FB696u, 0xE18C8B4Du, 0xED35EAE9u, 0xD7A79D81u, 0x42C41ECBu, 0xCAAEF571u, 0x2742E500u, 0x97778742u,
                    0x31A4AD65u, 0xA0CF833Bu, 0x626CCE34u, 0x9D11E26Fu, 0x61F8DA8Eu, 0xF94AA67Du, 0xDE998161u, 0x5F133AA2u, 0x66F97A53u, 0x4E0522A2u, 0xBEB90F85u, 0xCB79D88Fu, 0xEEC5AD8Du, 0x930EB0DCu, 0x048F0363u, 0xCEE706D9u,
                    0x665C2179u, 0x13D611A1u, 0x1FB65A94u, 0x856FBB01u, 0xCD960CABu, 0x3A2F8647u, 0x68EC39DEu, 0xF3D19CDAu, 0xD2D4BF74u, 0x3CF827CDu, 0xD7868B72u, 0x792B6CB0u, 0x2F0655E7u, 0x75917FC2u, 0xAFB19B67u, 0xF14C1184u,
                    0x92EE9B56u, 0xDBF97C5Bu, 0x4109BE77u, 0x706C81BAu, 0x56595427u, 0x662DFCDEu, 0x29D3FE11u, 0x3351E2BBu, 0x98F896DCu, 0x08473B48u, 0x51CFDA9Du, 0xF49868AEu, 0x0B34FCD1u, 0xDDBC4118u, 0x28F0EB1Eu, 0xC5CCF8FBu,
                    0x8ECCD4DCu, 0xC8BC6AD7u, 0x3348A1E5u, 0x60D17B0Du, 0x3A474A00u, 0x5DF4E50Fu, 0x130316D8u, 0x84A1BB5Bu, 0x39DFCD51u, 0x24909E3Du, 0x6276BDDBu, 0xA287EFBEu, 0xED22AA48u, 0xE88144DAu, 0x4900780Au, 0x6E255BF9u,
                    0x58E1941Eu, 0x1369F5A6u, 0x448C9D4Cu, 0x19DFF812u, 0xEF9D07A6u, 0xDFF61148u, 0x9440391Cu, 0x81051160u, 0xF0B6410Fu, 0x1CE82F77u, 0xAA3722FFu, 0x927ADA5Au, 0x4D4566C9u, 0x6A9F1349u, 0x25F72505u, 0x812EC62Au,
                    0xA42096FCu, 0x58E9D024u, 0x00DC67EFu, 0x035483E7u, 0x2664FD9Fu, 0x102A9B3Eu, 0x34FA75B8u, 0x84B34ACCu, 0xA4BF3B92u, 0x1E971CA9u, 0xB546E9DEu, 0x5B870ADBu, 0x913D56A8u, 0xDEE08899u, 0xCA3FBADDu, 0x75573850u,
                    0x406275C9u, 0x0B86DA36u, 0xB30A75A6u, 0x42753632u, 0x44DBA550u, 0xD315FEC8u, 0x230D4EEFu, 0x095E2BDBu, 0x52FF1D3Fu, 0x430791F4u, 0xA7CDAF48u, 0xE3852FCEu, 0xA370B7CEu, 0x88891298u, 0xEF9A57FFu, 0x03B813E4u,
                    0xD013DF4Bu, 0xD1A86E9Eu, 0xE143B681u, 0x9B3EAA28u, 0x807DA1ECu, 0x46F7E152u, 0x39E979D8u, 0x8C84EE52u, 0xD2C0598Cu, 0xBB3ED986u, 0xD962230Du, 0x6E35C49Eu, 0x562029B1u, 0x6C3FB50Fu, 0x654D304Au, 0x10C47B10u,
                    0xF39E0CE5u, 0x66F46915u, 0x3A04FA47u, 0x8C845C79u, 0x7F0ED20Du, 0xDC510589u, 0xBC4AF68Bu, 0x355C1BCDu, 0x66A035B7u, 0xAFB5903Eu, 0x920599CDu, 0x5298C2EDu, 0xF42E4786u, 0x9E0EB02Bu, 0xFC55F65Au, 0xAE19563Du,
                    0x4750210Eu, 0xA57C95D4u, 0x2157CAC9u, 0x3303BA6Fu, 0x87B7568Cu, 0x9B70ACABu, 0xCD11CF77u, 0x23E0AE84u, 0xB4D8DBC8u, 0x6E920FE2u, 0x178706B9u, 0x29938C86u, 0x3D31785Cu, 0xA8B36A63u, 0xFCFB06C2u, 0x17ED86A3u,
                    0x1B087725u, 0x2F181BAFu, 0x23E6B966u, 0x2C427142u, 0xA8602B08u, 0x43125F8Eu, 0x8CEFC658u, 0x37632B1Cu, 0x1D20FC57u, 0xCFC7E297u, 0x2CB0A8DAu, 0x01818A5Cu, 0x6BA6A8CBu, 0xC780FDECu, 0x4D206612u, 0xD3D63BBFu,
                    0xA20A06E1u, 0xBC62357Au, 0xB70A0FFCu, 0x8B88F3A5u, 0xD64BDEC1u, 0x69028D6Au, 0xA09B533Au, 0x42689169u, 0xFEB2397Au, 0xC3DB2C1Eu, 0x193D7D98u, 0xFC6E7775u, 0x851D90C3u, 0x912C166Bu, 0x1998E6EBu, 0x7EA9CEADu,
                    0xE381B942u, 0xE16F237Eu, 0xB7D9ACA9u, 0xFF403171u, 0xA82174FAu, 0x6994179Fu, 0x67053820u, 0xAC196EC0u, 0x5C085C3Du, 0x7FA12F1Cu, 0x2B41E84Fu, 0x5AD6E12Cu, 0x67C08F35u, 0x2C08B3D8u, 0xAB1421E4u, 0x09274A23u,
                    0xD5D4A78Fu, 0x9E930E3Fu, 0x69ABA3B9u, 0xFAD840FCu, 0xE66EE096u, 0xC975B21Au, 0x7B84039Du, 0x7574660Au, 0xC9D52185u, 0x0AA32706u, 0x3B0D96D3u, 0xFB775B80u, 0x7F3849C4u, 0x50E4E4EFu, 0x8C597CD6u, 0xEF5C70EEu,
                    0x52AB5837u, 0x2EFCDED5u, 0x0B76DB6Cu, 0x29B3DED3u, 0xD242C2AEu, 0xE74E70C9u, 0xF8C3D64Bu, 0x587865C8u, 0xCFDB5351u, 0x6699857Fu, 0x765BCA27u, 0x2AEC119Fu, 0x2EB75005u, 0x911986CFu, 0x557F50A9u, 0xD8499E68u,
                    0x8194F6CAu, 0x508E7ED1u, 0x410E7106u, 0xB3A5AB56u, 0x4838A048u, 0xA65E08E1u, 0x4204F043u, 0xBFB73120u, 0x2264B016u, 0xCD9D4674u, 0xED89A065u, 0x9E7C0ED9u, 0xABFF6BCAu, 0xE7186DF7u, 0x280F1DDDu, 0xA92F2219u,
                    0x8D2A06F1u, 0x945328EAu, 0xAD0F72ABu, 0x183F8CBEu, 0x339E1FF4u, 0x4F84DA6Eu, 0x1B8FE5A7u, 0x7D82536Fu, 0x6928991Bu, 0xE131BD7Au, 0xD11E682Bu, 0x22E7C54Eu, 0xC263911Du, 0xE1F73542u, 0x23C977A0u, 0x93BA2203u,
                    0x33C81964u, 0x3ED3F2C3u, 0x961393A2u, 0x63B852AAu, 0xFD6F43F7u, 0x0B9B0DB4u, 0x32EBEE78u, 0xF60EAA43u, 0x73997930u, 0xB9FEA340u, 0xB733D212u, 0x53951841u, 0x15C2B031u, 0x7E03991Cu, 0x7BF32D59u, 0x03040F1Bu,
                    0x27DA1482u, 0x06954F2Bu, 0xD2539F07u, 0x6F5FEA53u, 0x782CC117u, 0x1726D595u, 0x0053842Cu, 0xA3773453u, 0x82DCE8B1u, 0x8345C68Cu, 0x32B84D6Bu, 0x23FBB85Fu, 0x6295A038u, 0x1B0100C3u, 0xCA9BE77Bu, 0x8DC7E298u,
                    0xB51843FEu, 0xCEF0B4ADu, 0x040407DBu, 0x8CD841BEu, 0x26E99617u, 0xE1986F34u, 0x51370657u, 0xF8F79335u, 0xF398B185u, 0xA4A12476u, 0x2FB82F25u, 0xF243CF97u, 0x9A5BF6ABu, 0xA7639FE8u, 0x99D945A4u, 0x63A5C8DBu,
                    0x1DBD3497u, 0x3CAC060Fu, 0xC2B8E7D2u, 0x27F2177Au, 0xA884CB05u, 0xF5760E15u, 0xEE24C633u, 0x207F485Du, 0x9E910B70u, 0x7F359EFDu, 0xE8E6E7A4u, 0x23F693FDu, 0x98C1D6CDu, 0x7A807A20u, 0x47C98DBCu, 0x9909AC58u,
                    0x1C4DA0D6u, 0xA54AECFFu, 0xF31DFE07u, 0x7EFAF654u, 0x6E1609F2u, 0x3ED70728u, 0xEEB1305Du, 0x95785BCEu, 0xFD7B312Au, 0xF2896EF5u, 0x795D7A5Eu, 0xBD648B5Au, 0xDAC6398Fu, 0x5A4A14ACu, 0x2ECC15DFu, 0xD06EDC7Au,
                    0xDE1D7ED8u, 0x7F30C4F7u, 0x7B320666u, 0x5AEA9B66u, 0x70DD9DC8u, 0x6037985Bu, 0xA0A7D0D1u, 0xDA2D0E9Cu, 0xD4CD80ABu, 0x60705C6Au, 0xDA1C0AC4u, 0x450CA5A1u, 0xD636FF87u, 0x35FC3122u, 0x738C4719u, 0xE36F247Cu,
                    0x037AC2D3u, 0x9FF19373u, 0xC71411A5u, 0x4E9ED9F1u, 0x63753452u, 0x68830120u, 0x57483935u, 0x337287FAu, 0x634F7BDEu, 0x948920DDu, 0xE9CDBB6Fu, 0x2165CC44u, 0xFDA08877u, 0x3F521C29u, 0x0AD0694Au, 0xD828AA3Au,
                    0x3D54EF1Eu
                };
            }
        }
    }
}
