namespace MultiPrecision {
    public interface IConstant {
        int Value { get; }
    }
    public static class Pow2 {

        internal struct N1 : IConstant { public int Value => 1; }
        internal struct N2 : IConstant { public int Value => 2; }
        public struct N4 : IConstant { public int Value => 4; }
        public struct N8 : IConstant { public int Value => 8; }
        public struct N16 : IConstant { public int Value => 16; }
        public struct N32 : IConstant { public int Value => 32; }
        public struct N64 : IConstant { public int Value => 64; }
        public struct N128 : IConstant { public int Value => 128; }
        public struct N256 : IConstant { public int Value => 256; }
        public struct N512 : IConstant { public int Value => 512; }
        public struct N1024 : IConstant { public int Value => 1024; }
    }
}
