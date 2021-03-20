using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        public static string ToHexcode(IEnumerable<UInt32> value) {
            return string.Join(' ', value.Reverse().Select((u) => $"{u:X8}"));
        }
    }
}
