using MultiPrecision;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Windows.Markup;
using System.Xml.Schema;

namespace MultiPrecisionSandbox {

    class Program {

        static void Main(string[] args) {

            UInt32[] arr = new UInt32[] { 
                0x93C467E3u, 0x7DB0C7A4u, 0xD1BE3F81u, 0x0152CB56u, 0xA1CECC3Au, 0xF65CC019u, 0x0C03DF34u, 0x709AFFBDu,
                0x8E4B59FAu, 0x03A9F0EEu, 0xD0649CCBu, 0x621057D1u, 0x1056AE91u, 0x32135A08u, 0xE43B4673u, 0xD74BAFEAu 
            };
                        
            Vector256<UInt32>[] vs = ToVector(arr);
            Vector256<UInt64>[] us = new Vector256<UInt64>[9];

            for(uint i = 0; i <= 16; i++) { 
                Add(us, vs, i);

                Console.WriteLine(i);
            }

            UInt32[] n = FinalizeAdd(us, 36);

            Console.WriteLine("END");
            Console.Read();
        }

        unsafe static Vector256<UInt32>[] ToVector(UInt32[] arr) { 
            Vector256<UInt32>[] vs = new Vector256<UInt32>[(arr.Length + Vector256<UInt64>.Count - 1) / Vector256<UInt64>.Count];

            fixed(UInt32* p = arr) { 
                fixed(Vector256<UInt32> *v = vs) { 

                    if(arr.Length % Vector256<UInt64>.Count == 0) { 
                        for(int i = 0, j = 0; i < vs.Length; i++, j += Vector256<UInt64>.Count) {
                            v[i] = Avx2.ConvertToVector256Int64(Avx.LoadVector128(p + j)).AsUInt32();
                        }
                    }
                    else { 
                        int i, j;
                        for(i = 0, j = 0; i < vs.Length - 1; i++, j += Vector256<UInt64>.Count) {
                            v[i] = Avx2.ConvertToVector256Int64(Avx.LoadVector128(p + j)).AsUInt32();
                        }

                        v[i] = Avx2.ConvertToVector256Int64(Avx2.MaskLoad(p + j, UIntUtil.Mask128.LSV((uint)(arr.Length - j)))).AsUInt32();
                    }
                }
            }

            return vs;
        }

        unsafe static Vector256<UInt32>[] ToVector(UInt32 n, int length) {
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

                    v[i] = Avx2.ConvertToVector256Int64(Avx2.And(Vector128.Create(n), UIntUtil.Mask128.LSV((uint)(length - j)))).AsUInt32();
                }
            }

            return vs;
        }

        static (Vector256<UInt32>[] hi, Vector256<UInt32>[] lo) Mul(Vector256<UInt32>[] a, Vector256<UInt32>[] b) {

#if DEBUG
            if(a.Length != b.Length) { 
                throw new ArgumentException($"{nameof(a)},{nameof(b)}");
            }
#endif

            Vector256<UInt32>[] c_hi = new Vector256<UInt32>[a.Length], c_lo = new Vector256<UInt32>[a.Length];

            for(int i = 0; i < a.Length; i++) { 
                Vector256<UInt64> c = Avx2.Multiply(a[i], b[i]);

                c_hi[i] = Avx2.ShiftRightLogical(c, UIntUtil.UInt32Bits).AsUInt32();
                c_lo[i] = Avx2.ShiftRightLogical(Avx2.ShiftLeftLogical(c, UIntUtil.UInt32Bits), UIntUtil.UInt32Bits).AsUInt32();
            }

            return (c_hi, c_lo);
        }

        static void Add(Vector256<UInt64>[] s, Vector256<UInt32>[] v, uint shift) {
            uint shift_sets = shift / (uint)Vector256<UInt64>.Count;
            uint shift_rems = shift % (uint)Vector256<UInt64>.Count;

            if(shift_rems == 0) { 
                for(uint i = 0, j = shift_sets; i < v.Length; i++, j++) {
                    s[j] = Avx2.Add(s[j], v[i].AsUInt64());
                }
            }
            else{
                Vector256<UInt64> ml = UIntUtil.Mask256.MSV(shift_rems * 2).AsUInt64();
                Vector256<UInt64> mr = UIntUtil.Mask256.LSV(shift_rems * 2).AsUInt64();

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

        static void Sub(Vector256<UInt64>[] s, Vector256<UInt32>[] v, uint shift) {
            uint shift_sets = shift / (uint)Vector256<UInt64>.Count;
            uint shift_rems = shift % (uint)Vector256<UInt64>.Count;

            if(shift_rems == 0) { 
                for(uint i = 0, j = shift_sets; i < v.Length; i++, j++) {
                    s[j] = Avx2.Subtract(s[j], v[i].AsUInt64());
                }
            }
            else{
                Vector256<UInt64> ml = UIntUtil.Mask256.MSV(shift_rems * 2).AsUInt64();
                Vector256<UInt64> mr = UIntUtil.Mask256.LSV(shift_rems * 2).AsUInt64();

                byte mm_perm = shift_rems switch{
                    1 => 0x93,
                    2 => 0x4E,
                    3 => 0x39,
                    _ => throw new ArgumentException(nameof(shift_rems))
                };

                for(uint i = 0, j = shift_sets; i < v.Length; i++, j++) {
                    Vector256<UInt64> u = Avx2.Permute4x64(v[i].AsUInt64(), mm_perm);

                    s[j] = Avx2.Subtract(s[j], Avx2.And(u, ml));
                    s[j + 1] = Avx2.Subtract(s[j + 1], Avx2.And(u, mr));
                }
            }
        }

        static UInt32[] FinalizeAdd(Vector256<UInt64>[] us, int length) {
            UInt32[] vs = new UInt32[length];

            UInt32 carry = 0u, v;

            for(int i = 0, k = 0; i < us.Length; i++) {
                Vector256<UInt32> u = us[i].AsUInt32();

                for(int j = 0; j < Vector256<UInt64>.Count && k < length; j++, k++) { 
                    UInt32 n = u.GetElement(j * 2), c = u.GetElement(j * 2 + 1);

                    (carry, v) = UIntUtil.Unpack((UInt64)n + (UInt64)carry);

                    vs[k] = v;
                    carry += c;
                }
            }

            return vs;
        }
    }
}
