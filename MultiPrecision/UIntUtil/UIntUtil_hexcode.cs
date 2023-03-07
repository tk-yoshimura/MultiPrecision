namespace MultiPrecision {
    internal static partial class UIntUtil {

        public static string ToHexcode(IReadOnlyList<UInt32> value) {
            return string.Join(' ', value.Reverse().Select((u) => $"{u:X8}"));
        }
    }
}
