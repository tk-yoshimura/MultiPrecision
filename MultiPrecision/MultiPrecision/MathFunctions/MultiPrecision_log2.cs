﻿using System;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> {

        public static MultiPrecision<N> Log2(MultiPrecision<N> x) {
            if (!(x >= Zero)) {
                return NaN;
            }

            Accumulator<N> v = new Accumulator<N>(x.mantissa);

            Int64 exponent = x.Exponent;
            UInt32[] mantissa = new UInt32[Accumulator<N>.Length];

            for (int i = 0, init = Mantissa<N>.Bits; i < Accumulator<N>.Bits && i <= init + Mantissa<N>.Bits; i++) {
                v *= v;
                if (v.Value[Accumulator<N>.Length - 1] > UIntUtil.UInt32Round) {
                    UIntUtil.SetBit(mantissa, i);
                    v = Accumulator<N>.RightRoundBlockShift(v, Mantissa<N>.Length);

                    if (init >= Mantissa<N>.Bits) {
                        init = i;
                    }
                }
                else {
                    v = Accumulator<N>.RightRoundShift(v, Mantissa<N>.Bits - 1);
                }
            }

            Accumulator<N> mantissa_acc = new Accumulator<N>(mantissa);

            if (mantissa_acc.IsZero) {
                return exponent;
            }

            (Mantissa<N> n, int sft) = mantissa_acc.Mantissa;

            MultiPrecision<N> intpart = exponent;
            MultiPrecision<N> decpart = new MultiPrecision<N>(Sign.Plus, -(Int64)sft - 1, n, round: false);

            MultiPrecision<N> y = intpart + decpart;

            return y;
        }
    }
}