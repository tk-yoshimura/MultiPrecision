using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// 制御されます。アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("MultiPrecision")]
[assembly: AssemblyDescription("Float multi precision arithmetic implements")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("T.Yoshimura")]
[assembly: AssemblyProduct("MultiPrecision")]
[assembly: AssemblyCopyright("Copyright © T.Yoshimura 2020-2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
// ComVisible を false に設定すると、このアセンブリ内の型は COM コンポーネントから
// 参照できなくなります。COM からこのアセンブリ内の型にアクセスする必要がある場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]
// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      リビジョン
//
// すべての値を指定するか、次を使用してビルド番号とリビジョン番号を既定に設定できます
// 既定値にすることができます:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.5.*")]

[assembly: InternalsVisibleTo("MultiPrecisionTest")]
[assembly: InternalsVisibleTo("MultiPrecisionSandbox")]