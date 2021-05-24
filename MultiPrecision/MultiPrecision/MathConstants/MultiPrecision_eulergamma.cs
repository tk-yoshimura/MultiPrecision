﻿using System;
using System.Linq;

namespace MultiPrecision {
    public sealed partial class MultiPrecision<N> {

        private static partial class Consts {
            public static MultiPrecision<N> eulergamma = null;
        }

        public static MultiPrecision<N> EulerGamma {
            get {
                if (Consts.eulergamma is null) {
                    Consts.eulergamma = GenerateEulerGamma();
                }

                return Consts.eulergamma;
            }
        }

        private static MultiPrecision<N> GenerateEulerGamma() {

            if (Length > Consts.EulerGamma.Table.Length - 1) {
                throw new ArgumentOutOfRangeException(nameof(Length));
            }

            return new MultiPrecision<N>(
                Sign.Plus, exponent: -1,
                new Mantissa<N>(Consts.EulerGamma.Table[..Length].Reverse().ToArray(), enable_clone: false),
                round: Consts.EulerGamma.Table[Length] > UIntUtil.UInt32Round
            );
        }

        private static partial class Consts {
            public static class EulerGamma {
                public static readonly UInt32[] Table = {
                    0x93C467E3u, 0x7DB0C7A4u, 0xD1BE3F81u, 0x0152CB56u, 0xA1CECC3Au, 0xF65CC019u, 0x0C03DF34u, 0x709AFFBDu, 0x8E4B59FAu, 0x03A9F0EEu, 0xD0649CCBu, 0x621057D1u, 0x1056AE91u, 0x32135A08u, 0xE43B4673u, 0xD74BAFEAu,
                    0x58DEB878u, 0xCC86D733u, 0xDBE7BF38u, 0x154B36CFu, 0x8A96D156u, 0x7899AAAEu, 0x0C09D4C8u, 0xB6B7B86Fu, 0xD2A1EA1Du, 0xE62FF864u, 0x3EC7C271u, 0x82797722u, 0x5E6AC2F0u, 0xBD61C746u, 0x961542A3u, 0xCE3BEA5Du,
                    0xB54FE70Eu, 0x63E6D09Fu, 0x8FC28658u, 0xE80567A4u, 0x7CFDE60Eu, 0xE741E5D8u, 0x5A7BD469u, 0x31CED822u, 0x03655949u, 0x64B83989u, 0x6FCAABCCu, 0xC9B31959u, 0xC083F22Au, 0xD3EE591Cu, 0x32FAB2C7u, 0x448F2A05u,
                    0x7DB2DB49u, 0xEE52E018u, 0x2741E538u, 0x65F004CCu, 0x8E704B7Cu, 0x5C40BF30u, 0x4C4D8C4Fu, 0x13EDF604u, 0x7C555302u, 0xD2238D8Cu, 0xE11DF242u, 0x4F1B66C2u, 0xC5D238D0u, 0x744DB679u, 0xAF289048u, 0x7031F9C0u,
                    0xAEA1C4BBu, 0x6FE9554Eu, 0xE528FDF1u, 0xB05E5B25u, 0x6223B2F0u, 0x9215F371u, 0x9F9C7CCCu, 0x69DDF172u, 0xD0D62342u, 0x17FCC003u, 0x7F18B93Eu, 0xF5389130u, 0xB7A661E5u, 0xC26E5421u, 0x4068BBCAu, 0xFEA32A67u,
                    0x818BD307u, 0x5AD1F5C7u, 0xE9CC3D17u, 0x37FB2817u, 0x1BAF84DBu, 0xB6612B78u, 0x81C1A48Eu, 0x439CD03Au, 0x92BF5222u, 0x5A2B38E6u, 0x542E9F72u, 0x2BCE15A3u, 0x81B5753Eu, 0xA8427633u, 0x81CCAE83u, 0x512B3051u,
                    0x1B32E5E8u, 0xD8036214u, 0x9AD030AAu, 0xBA5F3A57u, 0x98BB22AAu, 0x7EC1B6D0u, 0xF17903F4u, 0xE1F3A067u, 0x31072B10u, 0xE0421838u, 0x0C3F5BE7u, 0xD44C6937u, 0xB6E79CF6u, 0x7655F072u, 0x30456F98u, 0x340336E1u,
                    0x166330FBu, 0xEF5F3CDBu, 0xE29B7929u, 0xC3BFBCF4u, 0x298C94ECu, 0xFA77DBB0u, 0x6AB26C11u, 0x890EA9E6u, 0x3440B109u, 0x21FB2536u, 0x1B34C7B9u, 0x342A13E3u, 0xFB6A91C3u, 0x5F67B951u, 0x63A91F91u, 0x6AA7B253u,
                    0xCC82EE52u, 0x0EC00686u, 0x6584424Eu, 0x7CDCE3C3u, 0xDD186CE3u, 0x4A330DA9u, 0xEE7B7082u, 0xEA725317u, 0x01CC5965u, 0xF761E1CEu, 0xADAF207Fu, 0x9827B450u, 0x5ADD7B66u, 0xEBB24471u, 0x6840DB4Bu, 0x294B8D99u,
                    0xD0CA6D32u, 0x2DA7905Cu, 0x98033652u, 0xDC225EB3u, 0x6B89901Cu, 0x13C9C153u, 0x80CCB048u, 0xB15BE0A6u, 0x13D075ECu, 0x0FBC1582u, 0x50E9BD62u, 0x9D799C0Eu, 0x15A205CAu, 0xEEAD2C1Au, 0x2AA22B0Eu, 0x2C64FA51u,
                    0xA68ED7F1u, 0x7D465557u, 0x45ACBD84u, 0xA03CBAF3u, 0x69DF9900u, 0xA8755A7Bu, 0x92AB8502u, 0x32ACB764u, 0x1DB7B5C7u, 0x7161F3C0u, 0x43D2D05Cu, 0xBF9EA7B7u, 0x9FB51315u, 0x3CDFE5AAu, 0x6F2B5F81u, 0xB3FB9DBFu,
                    0x4381B493u, 0x981CE589u, 0xF4CA5A73u, 0x05592647u, 0x909DFD95u, 0xFEEEB0FCu, 0xBE2ADFBDu, 0x5E3EFE49u, 0x97AEE382u, 0xC6BAC912u, 0x4B87D32Eu, 0xC33E16EEu, 0xB45BE4EEu, 0x4F90AB66u, 0x6FBDBCF9u, 0x9671AF23u,
                    0xDC68B6AEu, 0x362B362Au, 0x64B2AE85u, 0x49CA937Bu, 0x743A8758u, 0x575B0BDEu, 0xDD6A46A7u, 0x8962EF8Bu, 0x4E1369CAu, 0x581FA022u, 0x577752F2u, 0xC2ECE38Du, 0x34A71698u, 0xE114D7DEu, 0xA29660ACu, 0x422C5959u,
                    0x744ADC8Bu, 0x56AAC32Cu, 0x422FF6C4u, 0x1C4A60C1u, 0x825E4AD8u, 0x8DD5CDACu, 0x37A8E22Au, 0xED95E82Cu, 0xB52CD4E7u, 0x53AE38E1u, 0x880BC1E8u, 0xC174CA7Bu, 0x8B9CD955u, 0x6E6553C4u, 0xDC598129u, 0x42CA1A60u,
                    0xFB1F30F8u, 0xAC1B0D12u, 0x12EC32BBu, 0x8E1351C2u, 0x9AE3872Du, 0x9035D70Fu, 0x65CF9B0Cu, 0xE2E7917Cu, 0x79CEA3D8u, 0xFE9EAB29u, 0x0C504EBDu, 0xEEC14CACu, 0x94198385u, 0x43547083u, 0xC37EDDCAu, 0xAF790438u,
                    0x21D8B08Cu, 0x5EF1D180u, 0xE7239F50u, 0x61704D51u, 0xFED0AB5Fu, 0xBC2402EEu, 0x3F91E011u, 0xA17155F9u, 0xE4C0C0D5u, 0xA797DEA2u, 0x6198DF21u, 0x27915F3Au, 0x71883C11u, 0x6E1169ECu, 0x775D7F36u, 0x778FB170u,
                    0x521CCDE7u, 0x887B8287u, 0x3A318DBAu, 0xC628EBD3u, 0x715902AAu, 0x0D19DDEEu, 0x5AFBCD16u, 0xB78EC3EBu, 0x186933BCu, 0xB20D7DD8u, 0xD17FE0B1u, 0xE5BB292Eu, 0x7BBC0774u, 0x614DE12Fu, 0xD66EAEFBu, 0x59458510u,
                    0xA8278168u, 0x6D3C7E3Du, 0x760E8AACu, 0xCAEC24E8u, 0x74911B4Fu, 0xFD7E8398u, 0xA8512959u, 0xC2B53A90u, 0x7621D247u, 0xC0964758u, 0x25E8CE92u, 0xB5826A38u, 0x99243364u, 0xE18D85B1u, 0x2972642Bu, 0xB1574349u,
                    0x155E63A9u, 0x83A499A7u, 0xFA5B9573u, 0x47A74539u, 0x5676785Au, 0xA98B9DA5u, 0xE9E560E4u, 0x00928450u, 0x0C36D208u, 0x3F60B876u, 0x171976B6u, 0xB6A43F4Fu, 0xE377B543u, 0x6CAA3095u, 0xC8FCAF26u, 0x267DC9A4u,
                    0xA3BFE9ECu, 0x96262261u, 0x1AB47ECAu, 0x95A75698u, 0x29D4470Au, 0x915AB2E1u, 0x15D89425u, 0x530B3A83u, 0xCB8681C0u, 0xEED95805u, 0xD450A77Bu, 0x4C4E6093u, 0xD5BFC2ACu, 0x590FD52Cu, 0xD64C391Eu, 0x9EB240F8u,
                    0x0F2E85AFu, 0x0C655682u, 0x76BF3FA4u, 0x1E649F14u, 0x8F6F929Au, 0x2973CD65u, 0xC1F64BB7u, 0x38852A97u, 0x6C4853CFu, 0x8CC1FAE0u, 0xA7EE38F8u, 0xE74546BEu, 0x08244E4Eu, 0x3546D9ACu, 0x4A971A1Fu, 0x8CDA4627u,
                    0xF6B2BD8Au, 0xFB1E2A06u, 0x11E25742u, 0x9EA616F5u, 0x5D375EF8u, 0xEACC2A2Au, 0xA0A7FA74u, 0xAC5300E0u, 0x44A851BEu, 0xE1F3F99Bu, 0xD05DFDC7u, 0x750F35DEu, 0x9F1B0AA8u, 0x8DB5466Du, 0x3A82DBDBu, 0xE64C103Fu,
                    0x0A930313u, 0x6BDEADDEu, 0x61303E0Fu, 0xF30501D0u, 0x25E02101u, 0xBCC6D12Bu, 0xFF7AE68Fu, 0xA35BE89Eu, 0xFC24E30Du, 0x3EDAAD32u, 0x1946C9F2u, 0x5CCEF985u, 0xE124AB42u, 0xC700B1EAu, 0x16D06B46u, 0x462268D9u,
                    0x650EDF40u, 0x87C97C22u, 0x5A6258BEu, 0x5DAF194Du, 0xEB8DFE9Cu, 0xBD9EEFCAu, 0xB1F1DD4Au, 0xBC4DB5DAu, 0x207FA677u, 0x46A6A11Cu, 0x14548621u, 0x9E2190E6u, 0xF13A7924u, 0x1A865733u, 0x5D064221u, 0x8AC84633u,
                    0xC12B9334u, 0xB235E56Au, 0x7D2D233Eu, 0x0737E269u, 0x6EF57A5Au, 0xB2DD351Fu, 0x16CE0C7Fu, 0x8C43A2CBu, 0xF75C8ADFu, 0xCC74C5E9u, 0x9702D018u, 0xB770CA1Cu, 0xB14F099Bu, 0x666C5D49u, 0x6F7B7A4Au, 0x0CB046A9u,
                    0xBE81D566u, 0x4A3D4BB1u, 0x07591A7Cu, 0xDE5AEE17u, 0xED6B0F7Bu, 0x4B81B326u, 0x221E6FBAu, 0xA3104BA2u, 0xCB0693F0u, 0x878DE675u, 0xC0849EB3u, 0x66B44EE8u, 0xA2D72757u, 0x3D7E8998u, 0xB2E92BE0u, 0xAAFD9D56u,
                    0xECE4894Eu, 0x84AF8A2Eu, 0xDB533956u, 0xFFCDBA6Bu, 0xA4B79905u, 0xC788C7E9u, 0x5A086F76u, 0x6982AE81u, 0x59C49FB6u, 0x883EF3A1u, 0xECEF5193u, 0x4B8A6259u, 0xC225192Bu, 0xE61322B2u, 0x7185BED8u, 0x8B5B8210u,
                    0x6E082680u, 0xA1CCF3B2u, 0x4B357910u, 0xA5B889D0u, 0x8B50C8DBu, 0xBDFB0061u, 0xFB8E6DA2u, 0xF735B2A5u, 0x0C67A91Bu, 0x5FE7058Fu, 0x86707A36u, 0x574278F7u, 0x76BA464Fu, 0xCAECF16Cu, 0x3373B0ACu, 0x727A26B4u,
                    0x03EB11F0u, 0xC44EBA60u, 0xA9B5D621u, 0xF70E31E2u, 0xA8770199u, 0x65805D18u, 0xDDDC1449u, 0xAB92F220u, 0xA58F6463u, 0x915E5D50u, 0x9D0A8E3Eu, 0x37D064B5u, 0xBE2F1CE2u, 0xDC6F43C6u, 0xE556EE15u, 0xD428A1E1u,
                    0x0F21DD04u, 0x444E027Fu, 0xBF93157Fu, 0x515A6EA4u, 0x95C44811u, 0x9EDDF596u, 0x7FF8C213u, 0x9650D261u, 0xACCC75D7u, 0x4B159BD5u, 0xFB3CEA0Cu, 0x7458BD07u, 0x2A4C8974u, 0x935F2F22u, 0x341D6A9Eu, 0x9282A253u,
                    0x238ED53Bu, 0xCA1C77C4u, 0x48D5E3F7u, 0x19D5ED88u, 0xACB46B64u, 0x4EB1CCD5u, 0x20696F4Du, 0x7510AC17u, 0x12F3C81Cu, 0x5301020Fu, 0x43CC26ACu, 0x39438141u, 0x6B83D911u, 0xD21D3437u, 0x2DF972DBu, 0xD8D327FBu,
                    0x04AED377u, 0x38C3E3EFu, 0x111C73F1u, 0x525E5796u, 0x75C5E429u, 0xC5C25D57u, 0xEC95C4A0u, 0xBD65E7A6u, 0x47FBB3E0u, 0x4A188F05u, 0x4FA71CB5u, 0x9B63A97Eu, 0xF8EEF664u, 0xE8BD1246u, 0x5D36919Au, 0x3F40BF83u,
                    0xD76C8690u, 0xB36300B2u, 0xBE0E9081u, 0xF25C7B89u, 0x038ED883u, 0x1F898978u, 0x937C5540u, 0x7925AEC5u, 0x6276FDDDu, 0xF36D7D33u, 0x796A4972u, 0xD8ADBF6Cu, 0xA302FA11u, 0xF7C7BCC8u, 0xC9FD2585u, 0x44D3DBA0u,
                    0x78DB4118u, 0x8E41116Cu, 0xEBEEBE37u, 0x5C5FFAC3u, 0xC5CBA011u, 0xFB567B1Bu, 0xFFE8AC15u, 0x83F60572u, 0x2E2A4A5Eu, 0x48B75E41u, 0x06E40EB1u, 0x3BACC814u, 0x7965E7A8u, 0x2A9E6A7Fu, 0x446B0D0Bu, 0xE9484D55u,
                    0x20B58B29u, 0xF76E24CDu, 0x44DA5B38u, 0x95FCE710u, 0xF95C8D30u, 0xF5D22F8Au, 0xDB06CEE7u, 0x3AFB9874u, 0x49FFC5A7u, 0x94B9E324u, 0x88011E65u, 0x23F450DEu, 0xB18C097Au, 0x0674A36Au, 0x275907B4u, 0x86B143EAu,
                    0x920DC0C5u, 0x090E29EBu, 0x82E64D67u, 0xB3165DD7u, 0xBC374909u, 0xDF2CF3A0u, 0x18648434u, 0x1C9A73DCu, 0xFD2DE864u, 0x81ADA2A8u, 0x63061D6Du, 0x6A1A2D9Bu, 0x1306D95Eu, 0x48A9C0E4u, 0x755DF613u, 0xB09E5BF4u,
                    0xB888058Bu, 0xC68DB4A7u, 0x5FD1EF2Bu, 0xE57904E7u, 0xEB0462BDu, 0xC3BBC97Du, 0x00C58C1Du, 0x1E79C013u, 0x661BB113u, 0xABB64F12u, 0x10D99F7Au, 0xF925FBECu, 0x3B9DD6FAu, 0x7285098Au, 0x87CAC06Eu, 0xB715EBC8u,
                    0x3FAD7D58u, 0xD3CAE2B1u, 0xF835176Du, 0xB4BCBBE4u, 0x2E95E543u, 0x2161071Fu, 0xD18181D7u, 0x7614ABFBu, 0xB140243Au, 0x9361CF31u, 0xBA9EB589u, 0xA85C7381u, 0xC9595338u, 0xB4CDC263u, 0x05341F30u, 0x9688E5BEu,
                    0x38ABB5D6u, 0x9108F7C4u, 0x2DF377F3u, 0x813F20D1u, 0xE1F28E6Eu, 0xBBA5C470u, 0x33419D60u, 0x31B8E31Fu, 0xF278892Eu, 0x5E37BD43u, 0xE6A436E9u, 0x537EEA87u, 0xDC1CC1C6u, 0x4130E602u, 0xC8B4203Bu, 0x4CD74FD4u,
                    0x28F56A08u, 0x8FA85FFDu, 0xFB110D99u, 0xE8350F42u, 0xF682DAB6u, 0x9A02C69Cu, 0x7FD45E2Au, 0x3FA2DB9Fu, 0xA50EF294u, 0x8A2E6D73u, 0x4DFF87F4u, 0xBADE13CFu, 0x2FB913AFu, 0x97E21A7Cu, 0xBD6D7967u, 0x35151DFEu,
                    0x791C5274u, 0x9D918C66u, 0x7C65F8B8u, 0xE8A903A6u, 0xF1EA276Fu, 0xCD863076u, 0x7D136845u, 0xC07D460Cu, 0xCEB3887Fu, 0xCC754383u, 0xDC6B581Eu, 0xD0EA7ACBu, 0x134B85B1u, 0xD5488DD6u, 0x3A8057EAu, 0xC92B69BFu,
                    0x56766A00u, 0x3E565E8Bu, 0xFD5DB168u, 0x027D5550u, 0xED8D6B2Bu, 0xE5F02A2Bu, 0xF6655A4Du, 0xC37ABD22u, 0xF7271155u, 0x12EF58C3u, 0xE2A033CCu, 0x37053D22u, 0x8C877E65u, 0x22BA258Cu, 0x84C3774Au, 0x08A02E45u,
                    0x41008CDCu, 0xEB152C2Au, 0x181F3A80u, 0xECCD201Au, 0x0D2A1AF6u, 0x5BD7CC81u, 0xF4F0B218u, 0x8B7B0D24u, 0x3C1BAD04u, 0x76DABD5Cu, 0xD29DAA8Fu, 0x64DF68CDu, 0x1600DAA6u, 0xB35A48FCu, 0x57AAFF02u, 0xF00903CAu,
                    0xF4A079D2u, 0x14536241u, 0x0FC6DA67u, 0xEA6B4F5Cu, 0xB6D1CC4Du, 0xDA0F969Eu, 0x2DF53D5Bu, 0x146D9117u, 0x32CD8BEBu, 0x03BD3BF8u, 0x48D8CD0Fu, 0xA0C67D06u, 0x9321BED7u, 0x146011F6u, 0xEB924810u, 0xE8B6EF06u,
                    0xCFAE876Au, 0x208CA3D6u, 0x989565D3u, 0x5C8C9554u, 0x4C455BD2u, 0xDFE68E8Fu, 0x17AE0800u, 0x8AE035C2u, 0xC6848FAFu, 0x8086436Fu, 0x458AB48Du, 0x56CB481Cu, 0xB47B1873u, 0x37346CEBu, 0x1603D3C9u, 0xFFC2BBE7u,
                    0xF85F39CEu, 0x67CBB249u, 0x28BD5F40u, 0xBC728864u, 0xE2FABB67u, 0x39ED3BB7u, 0x6CCED4CFu, 0x81A759D6u, 0x2F89CC38u, 0xE7D79B5Eu, 0x48C27547u, 0xA966EF1Fu, 0x240DF4F4u, 0xCAB64956u, 0xCBC8992Eu, 0x275D253Du,
                    0x3C782357u, 0xE5E5C87Bu, 0x5A3DD93Cu, 0x91B6E987u, 0x7599ED33u, 0x4853C71Du, 0x03C21E3Bu, 0x4D6BECB3u, 0x48B631DFu, 0xE2B24323u, 0x17706F53u, 0x122C42EDu, 0x69E6B1A6u, 0xE799DB57u, 0xBA63EBBEu, 0x9E629A9Au,
                    0xCB012A38u, 0x1D1BF767u, 0xB2D05081u, 0x4D29F142u, 0x5D0C85BFu, 0xC88B94D6u, 0xB4CB5825u, 0x672C675Au, 0x029E3B61u, 0xF3A3C18Fu, 0x94DF95ACu, 0x0AD00B65u, 0xBAC670FDu, 0xC0589DCAu, 0x94B1A3C4u, 0x12986A9Cu,
                    0x4E24DEEDu, 0xBF6F739Au, 0x377B960Cu, 0xF1E3620Eu, 0x8855FFCAu, 0xFCCF63A6u, 0x4E418EB3u, 0x6EDC1BFBu, 0xFBE9FEE6u, 0xF2A9453Au, 0xC32FF291u, 0x1F52B503u, 0xC6A5D347u, 0xC37E2CC9u, 0xF64513CEu, 0x4C2A6BB6u,
                    0xA8AE1547u, 0xD407E06Cu, 0x19B82577u, 0x955D53F2u, 0x9A94DF9Eu, 0x17E6514Cu, 0x3882D145u, 0xD535BDD8u, 0x137486ABu, 0x683572ABu, 0x7BCE6826u, 0x0EF37325u, 0xE5EC8779u, 0x2743DAB2u, 0xFB52F19Bu, 0x58F59738u,
                    0xC30589B8u, 0xFAA66847u, 0x7C937E5Du, 0xEC6485A7u, 0x91619D40u, 0x3C363037u, 0x4A4EB866u, 0x1882F96Cu, 0x8EC74392u, 0x173A849Bu, 0xCE663FE3u, 0x8F0492DAu, 0x6225DAAFu, 0x0BC3367Bu, 0xD14AD7E0u, 0xCE44D604u,
                    0x3850C4A7u, 0xD6D020B7u, 0xCE8DB50Fu, 0x5D4381DDu, 0x02148438u, 0x4CDABE00u, 0x7CE386B8u, 0xF106ED60u, 0xF827EF7Au, 0x13789E99u, 0x64A93484u, 0x129C2578u, 0x0671ADDCu, 0xC63269F0u, 0x64696CF8u, 0x38E23B0Bu,
                    0x42B2A873u, 0xA2B52B32u, 0x1B75023Cu, 0x6E56D909u, 0xF4EE29C7u, 0x62ED99C3u, 0xEB801489u, 0x99E37DB7u, 0x975E43FBu, 0x00BBD7EDu, 0x4A7DF804u, 0x7D0308FBu, 0x2909C9CFu, 0x3BE57D10u, 0xB69E195Eu, 0xDEC996C6u,
                    0x32B7EE25u, 0x718B7DF4u, 0x92F82FC6u, 0x4C788431u, 0xC5728206u, 0xE47637D6u, 0x054AF075u, 0x25C559F2u, 0x6C2FDFC8u, 0x3658C339u, 0x9576C7C7u, 0xE7D6888Fu, 0xD4DC19BEu, 0x8E8BBC1Cu, 0xA22CAF42u, 0x68725FA2u,
                    0x9B1DE1C0u, 0x0E000293u, 0xC3F7A2F8u, 0xE505B23Fu, 0x6E4BEFF4u, 0x5D7F1D07u, 0x669F15D9u, 0x5CBD8EDFu, 0xC09AB586u, 0x13575973u, 0x16691003u, 0xFA0531BFu, 0xB0B013DDu, 0x83AAA1F6u, 0xEA7C4E87u, 0x289B37FFu,
                    0xF94EE03Du, 0xD125DEF1u, 0x8F57B625u, 0xB637B707u, 0xDC253DB9u, 0x7CC84259u, 0x19138FD5u, 0x90092F84u, 0x31DE2D47u, 0x03B156CEu, 0xF9FB3BF2u, 0xF9D1E1CAu, 0x176141B1u, 0xA2FCBF64u, 0xC49AFAE3u, 0x1B545F71u,
                    0x71D419D0u, 0xA02A1286u, 0x123889AFu, 0x922724FFu, 0x58F20DAAu, 0x43108032u, 0x8175B1F4u, 0xF04EE512u, 0x1FF0CBC3u, 0x99D143A1u, 0x1884E65Fu, 0x2A2B2A61u, 0xEC2098D0u, 0xFD5F07CEu, 0x5064BA38u, 0x4F36572Fu,
                    0xFA4F9F2Au, 0x3245E343u, 0x7B46A325u, 0x21E6C552u, 0x58BB2EF3u, 0xC958AA66u, 0x8C8A8D2Du, 0x6ECF6381u, 0xD14AC725u, 0x2673BBBFu, 0x79F39592u, 0xBAF542DCu, 0x02C40F0Du, 0x7A1F04D1u, 0xA51F2352u, 0x505AC22Au,
                    0x987117D2u, 0x7658E763u, 0xB868AFF7u, 0x3AA53769u, 0x560E1899u, 0xAD6C29F5u, 0xBCE21132u, 0xBBBA4B23u, 0xE016F275u, 0xE8013D34u, 0xE086FD45u, 0xD1E12054u, 0xEE3C8F57u, 0x106BE853u, 0x09B972ECu, 0x2F04A0EBu,
                    0x66B0B131u, 0x6BBB19DCu, 0xBB4076BAu, 0x509E70F4u, 0x3506AB58u, 0x0AF22EA2u, 0xE6B1D36Cu, 0x11615B73u, 0xAF1F39D0u, 0x1EFFFEA9u, 0xE622D1CBu, 0x2E8FDFD6u, 0x89CA13D4u, 0x8060EE58u, 0xCEF378E6u, 0xFF02669Du,
                    0xADEF182Eu, 0xBD613BD8u, 0x29428826u, 0xE71D1A38u, 0x16A6E81Eu, 0x41023D0Bu, 0x872C3EC2u, 0x781C72FEu, 0xCC154787u, 0xCC257815u, 0x818A2CE1u, 0x61296CC6u, 0x791679C5u, 0xD822FBD4u, 0x5F135035u, 0x932BC443u,
                    0x128F0971u, 0xD239CC95u, 0x2BC61757u, 0xE3130B77u, 0x983E8A3Au, 0xCDDA82CDu, 0x98F6435Au, 0x4F1C6E5Du, 0x0E22956Bu, 0x886207D0u, 0xE6C66E55u, 0x156CA6A8u, 0xB650B2E2u, 0x15FF9BD4u, 0xF2E0156Fu, 0xEEB5967Eu,
                    0x45657D3Cu, 0x458E1753u, 0x32C46CA1u, 0x84B86FE4u, 0x74F027A8u, 0xA0C22C26u, 0xF1568030u, 0xDCEF79D7u, 0x4FE97B74u, 0xC889DE9Au, 0x44BC8DE6u, 0xF9EF873Eu, 0x63FBF6C4u, 0x1B3733E4u, 0x523E27F5u, 0x5CD6F69Du,
                    0x78EC0E70u, 0xE84897AFu, 0x651B7376u, 0xBC9F4810u, 0x129BCFA9u, 0xBD79B3B4u, 0x2E9B5261u, 0x1A809E8Eu, 0xFC9F6622u, 0xE81494C8u, 0x46C52B69u, 0x0B7151FCu, 0x62CB5E7Fu, 0x7F94F42Cu, 0xE78DEFFAu, 0x98590C77u,
                    0x8051E4DFu, 0x81BA967Bu, 0x84D5860Cu, 0x682D247Du, 0x72F1D5CBu, 0x11298D4Fu, 0x2F078E22u, 0x02E51B61u, 0xFEFD2747u, 0xBD77B5FDu, 0xCC3B5931u, 0x99C88414u, 0xD373E965u, 0xB92250C5u, 0x56F8B435u, 0x45A1622Fu,
                    0xBD9CF9E7u
                };
            }
        }
    }
}
