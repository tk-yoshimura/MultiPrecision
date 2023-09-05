using System.Numerics;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Globalization;

namespace MultiPrecision {

    public sealed partial class MultiPrecision<N> :
        INumber<MultiPrecision<N>>,
        ISignedNumber<MultiPrecision<N>>,

        IAdditiveIdentity<MultiPrecision<N>, MultiPrecision<N>>,
        IMultiplicativeIdentity<MultiPrecision<N>, MultiPrecision<N>>,

        IAdditionOperators<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>>,
        ISubtractionOperators<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>>,
        IMultiplyOperators<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>>,
        IDivisionOperators<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>>,
        IModulusOperators<MultiPrecision<N>, MultiPrecision<N>, MultiPrecision<N>>,

        IUnaryPlusOperators<MultiPrecision<N>, MultiPrecision<N>>,
        IUnaryNegationOperators<MultiPrecision<N>, MultiPrecision<N>>,

        IEquatable<MultiPrecision<N>>,
        IEqualityOperators<MultiPrecision<N>, MultiPrecision<N>, bool>,
        IEqualityComparer<MultiPrecision<N>>,

        IComparisonOperators<MultiPrecision<N>, MultiPrecision<N>, bool>,
        IMinMaxValue<MultiPrecision<N>> {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> AdditiveIdentity => Zero;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> MultiplicativeIdentity => One;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static MultiPrecision<N> NegativeOne => MinusOne;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        static int INumberBase<MultiPrecision<N>>.Radix => 2;

        public static bool IsCanonical(MultiPrecision<N> value) => value.IsFinite;

        public static bool IsRealNumber(MultiPrecision<N> value) => !value.IsNaN;
        public static bool IsImaginaryNumber(MultiPrecision<N> value) => false;
        public static bool IsComplexNumber(MultiPrecision<N> value) => false;

        public static bool IsPositive(MultiPrecision<N> value) => value.Sign == Sign.Plus;
        public static bool IsNegative(MultiPrecision<N> value) => value.Sign == Sign.Minus;

        public static bool IsInteger(MultiPrecision<N> value) => Truncate(value) == value;
        public static bool IsEvenInteger(MultiPrecision<N> value) => IsInteger(value) && (Abs(value % 2d) == 0d);
        public static bool IsOddInteger(MultiPrecision<N> value) => IsInteger(value) && (Abs(value % 2d) == 1d);

        public static MultiPrecision<N> MaxMagnitude(MultiPrecision<N> x, MultiPrecision<N> y) => (Abs(x) > Abs(y) || x.IsNaN) ? x : y;
        public static MultiPrecision<N> MaxMagnitudeNumber(MultiPrecision<N> x, MultiPrecision<N> y) => (Abs(x) > Abs(y) || y.IsNaN) ? x : y;
        public static MultiPrecision<N> MinMagnitude(MultiPrecision<N> x, MultiPrecision<N> y) => (Abs(x) < Abs(y) || x.IsNaN) ? x : y;
        public static MultiPrecision<N> MinMagnitudeNumber(MultiPrecision<N> x, MultiPrecision<N> y) => (Abs(x) < Abs(y) || y.IsNaN) ? x : y;

        public static MultiPrecision<N> Parse(string s, NumberStyles style, IFormatProvider provider) => Parse(s);
        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out MultiPrecision<N> result) => TryParse(s, out result);
        public static MultiPrecision<N> Parse(string s, IFormatProvider provider) => Parse(s);
        public static bool TryParse(string s, IFormatProvider provider, out MultiPrecision<N> result) => TryParse(s, out result);
        public static MultiPrecision<N> Parse(ReadOnlySpan<char> s, IFormatProvider provider) => Parse(s.ToString());
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, out MultiPrecision<N> result) => TryParse(s.ToString(), out result);
        public static MultiPrecision<N> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider) => Parse(s.ToString());
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out MultiPrecision<N> result) => TryParse(s.ToString(), out result);

        public static bool TryConvertFromChecked<TOther>(TOther value, out MultiPrecision<N> result) where TOther : INumberBase<TOther> {
            if (value is double vd) {
                result = vd;
                return true;
            }
            if (value is float vf) {
                result = vf;
                return true;
            }
            if (value is long vl) {
                result = vl;
                return true;
            }
            if (value is int vi) {
                result = vi;
                return true;
            }
            if (value is decimal vdec) {
                result = vdec;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, out MultiPrecision<N> result) where TOther : INumberBase<TOther> {
            return TryConvertFromChecked<TOther>(value, out result);
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, out MultiPrecision<N> result) where TOther : INumberBase<TOther> {
            return TryConvertFromChecked<TOther>(value, out result);
        }

        public static bool TryConvertToChecked<TOther>(MultiPrecision<N> value, out TOther result) where TOther : INumberBase<TOther> {
            if (typeof(TOther) == typeof(double)) {
                result = (TOther)(object)(double)value;
                return true;
            }
            if (typeof(TOther) == typeof(float)) {
                result = (TOther)(object)(float)value;
                return true;
            }
            if (typeof(TOther) == typeof(long)) {
                result = (TOther)(object)(long)value;
                return true;
            }
            if (typeof(TOther) == typeof(int)) {
                result = (TOther)(object)(int)value;
                return true;
            }
            if (typeof(TOther) == typeof(decimal)) {
                result = (TOther)(object)(decimal)value;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryConvertToSaturating<TOther>(MultiPrecision<N> value, out TOther result) where TOther : INumberBase<TOther> {
            if (typeof(TOther) == typeof(double)) {
                result = (TOther)(object)(double)value;
                return true;
            }

            if (typeof(TOther) == typeof(float)) {
                result = (TOther)(object)(float)Clamp(value, float.MinValue, float.MaxValue);
                return true;
            }
            if (typeof(TOther) == typeof(long)) {
                result = (TOther)(object)(long)Clamp(value, long.MinValue, long.MaxValue);
                return true;
            }
            if (typeof(TOther) == typeof(int)) {
                result = (TOther)(object)(int)Clamp(value, int.MinValue, int.MaxValue);
                return true;
            }
            if (typeof(TOther) == typeof(decimal)) {
                result = (TOther)(object)(decimal)Clamp(value, decimal.MinValue, decimal.MaxValue);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryConvertToTruncating<TOther>(MultiPrecision<N> value, out TOther result) where TOther : INumberBase<TOther> {
            return TryConvertToSaturating<TOther>(value, out result);
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider) {
            string str = format.IsEmpty ? ToString() : ToString(format.ToString());

            if (str.TryCopyTo(destination)) {
                charsWritten = str.Length;
                return true;
            }
            else {
                charsWritten = 0;
                return false;
            }
        }

        public bool Equals(MultiPrecision<N> x, MultiPrecision<N> y) => x == y;
        public bool Equals(MultiPrecision<N> other) => this == other;
        public int GetHashCode([DisallowNull] MultiPrecision<N> obj) => obj.GetHashCode();
    }
}
