using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("5.0.6.*")]

[assembly: InternalsVisibleTo("MultiPrecisionTest")]
[assembly: InternalsVisibleTo("MultiPrecisionBesselTest")]
[assembly: InternalsVisibleTo("MultiPrecisionSandbox")]