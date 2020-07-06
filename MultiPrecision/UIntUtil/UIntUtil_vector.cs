using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MultiPrecision {
    internal static partial class UIntUtil {

        public static partial class Vector {

#pragma warning disable IDE1006
            // zmminitrin.h
            private const byte MM_PERM_CBAD = 0x93;
            private const byte MM_PERM_BADC = 0x4E;
            private const byte MM_PERM_ADCB = 0x39;
            private const byte MM_PERM_CDAB = 0xB1;
#pragma warning restore IDE1006

            private static readonly Vector256<UInt32> lower_mask;

            static unsafe Vector() {
                UInt32[] value = new UInt32[] { ~0u, 0u, ~0u, 0u, ~0u, 0u, ~0u, 0u };

                fixed (UInt32* v = value) {
                    lower_mask = Avx.LoadVector256(v);
                }
            }

            public static UInt32[] Mul(UInt32[] v1, UInt32[] v2) {
                Vector256<UInt64>[] ws = AllocMulBuffer(checked(v1.Length + v2.Length));

                Vector256<UInt32>[] vs = ToVector(v1);

                for (int dig2 = 0; dig2 < v2.Length; dig2++) {
                    if (v2[dig2] == 0) {
                        continue;
                    }

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

                Vector256<UInt32>[] v = new Vector256<UInt32>[(digits + Vector256<UInt64>.Count - 1) / Vector256<UInt64>.Count];

                fixed (UInt32* p = arr) {
                    fixed (Vector256<UInt32>* pv = v) {

                        if (digits % Vector256<UInt64>.Count == 0) {
                            for (int i = 0, j = 0; i < v.Length; i++, j += Vector256<UInt64>.Count) {
                                pv[i] = Avx2.ConvertToVector256Int64(Avx.LoadVector128(p + j)).AsUInt32();
                            }
                        }
                        else {
                            int i, j;
                            for (i = 0, j = 0; i < v.Length - 1; i++, j += Vector256<UInt64>.Count) {
                                pv[i] = Avx2.ConvertToVector256Int64(Avx.LoadVector128(p + j)).AsUInt32();
                            }

                            pv[i] = Avx2.ConvertToVector256Int64(Avx2.MaskLoad(p + j, Mask128.LSV((uint)(digits - j)))).AsUInt32();
                        }

                    }
                }

                return v;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static unsafe (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) Mul(Vector256<UInt32>[] v, UInt32 n) {

                Vector256<UInt32>[] w_hi = new Vector256<UInt32>[v.Length], w_lo = new Vector256<UInt32>[v.Length];
                Vector256<UInt32> u = Avx2.ConvertToVector256Int64(Vector128.Create(n)).AsUInt32();

                fixed (Vector256<UInt32>* pv = v, pw_hi = w_hi, pw_lo = w_lo) {

                    for (int i = 0; i < v.Length; i++) {
                        Vector256<UInt32> c = Avx2.Multiply(pv[i], u).AsUInt32();

                        pw_hi[i] = Avx2.And(Avx2.Shuffle(c, MM_PERM_CDAB), lower_mask);
                        pw_lo[i] = Avx2.And(c, lower_mask);
                    }

                }

                return (w_hi, w_lo);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static unsafe void Add(Vector256<UInt64>[] s, Vector256<UInt32>[] v, int shift) {
                int shift_sets = shift / Vector256<UInt64>.Count;
                int shift_rems = shift % Vector256<UInt64>.Count;

                if (shift_rems == 0) {
#if DEBUG
                    Debug<OverflowException>.Assert(checked(shift_sets + v.Length) <= s.Length);
#endif

                    fixed (Vector256<UInt64>* ps = s) {
                        fixed (Vector256<UInt32>* pv = v) {

                            for (int i = 0, store_idx = shift_sets; i < v.Length; i++, store_idx++) {
                                ps[store_idx] = Avx2.Add(ps[store_idx], pv[i].AsUInt64());
                            }

                        }
                    }
                }
                else {
#if DEBUG
                    Debug<OverflowException>.Assert(checked(shift_sets + v.Length) < s.Length);
#endif

                    Vector256<UInt64> ml = Mask256.MSV(checked((uint)(shift_rems * 2))).AsUInt64();
                    Vector256<UInt64> mh = Mask256.LSV(checked((uint)(shift_rems * 2))).AsUInt64();

                    byte mm_perm = shift_rems switch
                    {
                        1 => MM_PERM_CBAD,
                        2 => MM_PERM_BADC,
                        3 => MM_PERM_ADCB,
                        _ => throw new ArgumentException(nameof(shift_rems))
                    };

                    int store_idx = shift_sets;
                    Vector256<UInt64> uh, ul, u;

                    fixed (Vector256<UInt64>* ps = s) {
                        fixed (Vector256<UInt32>* pv = v) {

                            u = Avx2.Permute4x64(pv[0].AsUInt64(), mm_perm);
                            ul = Avx2.And(u, ml);

                            ps[store_idx] = Avx2.Add(ps[store_idx], ul);
                            store_idx++;

                            for (int i = 1; i < v.Length; i++) {
                                uh = Avx2.And(u, mh);
                                u = Avx2.Permute4x64(pv[i].AsUInt64(), mm_perm);
                                ul = Avx2.And(u, ml);

                                ps[store_idx] = Avx2.Add(ps[store_idx], Avx2.Or(uh, ul));
                                store_idx++;
                            }

                            uh = Avx2.And(u, mh);
                            ps[store_idx] = Avx2.Add(ps[store_idx], uh);

                        }
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static unsafe UInt32[] Carry(Vector256<UInt64>[] us, int length) {
                UInt32[] vs = new UInt32[length], u = new UInt32[Vector256<UInt32>.Count];

                UInt32 carry = 0u, v;

                for (int i = 0, k = 0; i < us.Length; i++) {
                    fixed (UInt32* p = u) {
                        Avx2.Store(p, us[i].AsUInt32());
                    }

                    for (int j = 0; j < Vector256<UInt64>.Count && k < length; j++, k++) {
                        UInt32 n = u[j * 2], c = u[j * 2 + 1];

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
