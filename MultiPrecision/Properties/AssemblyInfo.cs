using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("MultiPrecision")]
[assembly: AssemblyDescription("Float MultiPrecision Arithmetic Implements")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("T.Yoshimura")]
[assembly: AssemblyProduct("MultiPrecision")]
[assembly: AssemblyCopyright("Copyright © T.Yoshimura 2020-2023")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("6.0.0.*")]

[assembly: InternalsVisibleTo("MultiPrecisionTest")]
[assembly: InternalsVisibleTo("MultiPrecisionBesselTest")]
[assembly: InternalsVisibleTo("MultiPrecisionSandbox")]