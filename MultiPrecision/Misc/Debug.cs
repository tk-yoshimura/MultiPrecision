using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MultiPrecision {
    public static class Debug<T> where T : Exception, new() {

        [Conditional("DEBUG")]
        public static void Assert([DoesNotReturnIf(false)] bool condition) {
            if (!condition) { 
                throw new T();
            }
        }
    }
}
