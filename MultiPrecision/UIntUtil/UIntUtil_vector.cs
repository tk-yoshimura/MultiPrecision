using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector256<UInt64>[] AllocMulBuffer(int length) { 
            return new Vector256<UInt64>[(length + Vector256<UInt64>.Count - 1) / Vector256<UInt64>.Count + 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Vector256<UInt32>[] ToVector(UInt32[] arr) { 
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
        public static unsafe Vector256<UInt32>[] ToVector(UInt32 n, int length) {
            Vector256<UInt32>[] vs = new Vector256<UInt32>[(length + Vector256<UInt64>.Count - 1) / Vector256<UInt64>.Count];

            fixed(Vector256<UInt32> *v = vs) { 

                if(length % Vector256<UInt64>.Count == 0) { 
                    for(int i = 0, j = 0; i < vs.Length; i++, j += Vector256<UInt64>.Count) {
                        v[i] = Avx2.ConvertToVector256Int64(Vector128.Create(n)).AsUInt32();
                    }
                }
                else { 
                    int i, j;
                    for(i = 0, j = 0; i < vs.Length - 1; i++, j += Vector256<UInt64>.Count) {
                        v[i] = Avx2.ConvertToVector256Int64(Vector128.Create(n)).AsUInt32();
                    }

                    v[i] = Avx2.ConvertToVector256Int64(Avx2.And(Vector128.Create(n), Mask128.LSV((uint)(length - j)))).AsUInt32();
                }
            }

            return vs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static  (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) Mul(Vector256<UInt32>[] a, Vector256<UInt32>[] b) {

#if DEBUG
            if(a.Length != b.Length) { 
                throw new ArgumentException($"{nameof(a)},{nameof(b)}");
            }
#endif

            Vector256<UInt32>[] c_hi = new Vector256<UInt32>[a.Length], c_lo = new Vector256<UInt32>[a.Length];

            for(int i = 0; i < a.Length; i++) { 
                Vector256<UInt64> c = Avx2.Multiply(a[i], b[i]);

                c_hi[i] = Avx2.ShiftRightLogical(c, UInt32Bits).AsUInt32();
                c_lo[i] = Avx2.ShiftRightLogical(Avx2.ShiftLeftLogical(c, UInt32Bits), UInt32Bits).AsUInt32();
            }

            return (c_hi, c_lo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(Vector256<UInt64>[] s, Vector256<UInt32>[] v, uint shift) {
            uint shift_sets = shift / (uint)Vector256<UInt64>.Count;
            uint shift_rems = shift % (uint)Vector256<UInt64>.Count;

            if(shift_rems == 0) { 
                for(uint i = 0, j = shift_sets; i < v.Length; i++, j++) {
                    s[j] = Avx2.Add(s[j], v[i].AsUInt64());
                }
            }
            else{
                Vector256<UInt64> ml = Mask256.MSV(shift_rems * 2).AsUInt64();
                Vector256<UInt64> mr = Mask256.LSV(shift_rems * 2).AsUInt64();

                byte mm_perm = shift_rems switch{
                    1 => 0x93,
                    2 => 0x4E,
                    3 => 0x39,
                    _ => throw new ArgumentException(nameof(shift_rems))
                };

                for(uint i = 0, j = shift_sets; i < v.Length; i++, j++) {
                    Vector256<UInt64> u = Avx2.Permute4x64(v[i].AsUInt64(), mm_perm);

                    s[j] = Avx2.Add(s[j], Avx2.And(u, ml));
                    s[j + 1] = Avx2.Add(s[j + 1], Avx2.And(u, mr));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32[] FinalizeAdd(Vector256<UInt64>[] us, int length) {
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
