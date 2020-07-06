using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        public static partial class Vector { 

            public static unsafe UInt32[] Mul(UInt32[] v1, UInt32[] v2) { 
                Vector256<UInt64>[] ws = AllocMulBuffer(checked(v1.Length + v2.Length));

                Vector256<UInt32>[] vs = ToVector(v1);

                int v2_digits = Digits(v2);

                for (int dig2 = 0; dig2 < v2_digits; dig2++) {
                    (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) = Mul(vs, v2[dig2]);

                    Add(ws, lo, dig2);
                    Add(ws, hi, dig2 + 1);
                }

                UInt32[] w = Carry(ws, checked(v1.Length + v2.Length));

                return w;
            }        

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Vector256<UInt64>[] AllocMulBuffer(int length) { 
                return new Vector256<UInt64>[(length + Vector256<UInt64>.Count - 1) / Vector256<UInt64>.Count + 1];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static unsafe Vector256<UInt32>[] ToVector(UInt32[] arr) { 
                int digits = Digits(arr);

                Vector256<UInt32>[] vs = new Vector256<UInt32>[(digits + Vector256<UInt64>.Count - 1) / Vector256<UInt64>.Count];

                fixed(UInt32* p = arr) { 
                    fixed(Vector256<UInt32> *v = vs) { 

                        if(digits % Vector256<UInt64>.Count == 0) { 
                            for(int i = 0, j = 0; i < vs.Length; i++, j += Vector256<UInt64>.Count) {
                                v[i] = Avx2.ConvertToVector256Int64(Avx.LoadVector128(p + j)).AsUInt32();
                            }
                        }
                        else { 
                            int i, j;
                            for(i = 0, j = 0; i < vs.Length - 1; i++, j += Vector256<UInt64>.Count) {
                                v[i] = Avx2.ConvertToVector256Int64(Avx.LoadVector128(p + j)).AsUInt32();
                            }

                            v[i] = Avx2.ConvertToVector256Int64(Avx2.MaskLoad(p + j, Mask128.LSV((uint)(digits - j)))).AsUInt32();
                        }
                    }
                }

                return vs;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) Mul(Vector256<UInt32>[] v, UInt32 n) {

                Vector256<UInt32>[] w_hi = new Vector256<UInt32>[v.Length], w_lo = new Vector256<UInt32>[v.Length];
                Vector256<UInt32> u = Avx2.ConvertToVector256Int64(Vector128.Create(n)).AsUInt32();

                for(int i = 0; i < v.Length; i++) { 
                    Vector256<UInt64> c = Avx2.Multiply(v[i], u);

                    w_hi[i] = Avx2.ShiftRightLogical(c, UInt32Bits).AsUInt32();
                    w_lo[i] = Avx2.ShiftRightLogical(Avx2.ShiftLeftLogical(c, UInt32Bits), UInt32Bits).AsUInt32();
                }

                return (w_hi, w_lo);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static void Add(Vector256<UInt64>[] s, Vector256<UInt32>[] v, int shift) {
                int shift_sets = shift / Vector256<UInt64>.Count;
                int shift_rems = shift % Vector256<UInt64>.Count;

                if(shift_rems == 0) { 
                    for(int i = 0, j = shift_sets; i < v.Length; i++, j++) {
                        s[j] = Avx2.Add(s[j], v[i].AsUInt64());
                    }
                }
                else{
                    Vector256<UInt64> ml = Mask256.MSV(checked((uint)(shift_rems * 2))).AsUInt64();
                    Vector256<UInt64> mr = Mask256.LSV(checked((uint)(shift_rems * 2))).AsUInt64();

                    byte mm_perm = shift_rems switch{
                        1 => 0x93,
                        2 => 0x4E,
                        3 => 0x39,
                        _ => throw new ArgumentException(nameof(shift_rems))
                    };

                    for(int i = 0, j = shift_sets; i < v.Length; i++, j++) {
                        Vector256<UInt64> u = Avx2.Permute4x64(v[i].AsUInt64(), mm_perm);

                        s[j] = Avx2.Add(s[j], Avx2.And(u, ml));
                        s[j + 1] = Avx2.Add(s[j + 1], Avx2.And(u, mr));
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static UInt32[] Carry(Vector256<UInt64>[] us, int length) {
                UInt32[] vs = new UInt32[length];

                UInt32 carry = 0u, v;

                for(int i = 0, k = 0; i < us.Length; i++) {
                    Vector256<UInt32> u = us[i].AsUInt32();

                    for(int j = 0; j < Vector256<UInt64>.Count && k < length; j++, k++) { 
                        UInt32 n = u.GetElement(j * 2), c = u.GetElement(j * 2 + 1);

                        (carry, v) = Unpack((UInt64)n + (UInt64)carry);

                        vs[k] = v;
                        carry += c;
                    }
                }

                return vs;
            }
        }
    }
}
