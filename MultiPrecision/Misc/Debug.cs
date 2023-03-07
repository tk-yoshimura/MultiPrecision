using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    internal static class Debug<T> where T : Exception, new() {

        [Conditional("DEBUG")]
        public static void Assert([DoesNotReturnIf(false)] bool condition, [AllowNull] string message = null) {
            if (!condition) {
                if (!string.IsNullOrEmpty(message)) {
                    Trace.WriteLine(message);
                }
                throw new T();
            }
        }
    }
}
