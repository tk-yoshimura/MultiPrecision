using System;
using System.Numerics;

namespace MultiPrecision {
    internal class Fraction {
        public BigInteger Numer { private set; get; }
        public BigInteger Denom { private set; get; }

        public Fraction(long n) : this(new BigInteger(n)) { }

        public Fraction(BigInteger n) {
            this.Numer = n;
            this.Denom = 1;
        }

        public Fraction(long numer, long denom) : this(new BigInteger(numer), new BigInteger(denom)) { }

        public Fraction(BigInteger numer, BigInteger denom) {
            this.Numer = numer;
            this.Denom = denom;

            Reduce();
        }

        public static implicit operator Fraction(long n) {
            return new Fraction(n);
        }

        public static implicit operator Fraction(BigInteger n) {
            return new Fraction(n);
        }

        public static Fraction operator +(Fraction v1, Fraction v2) {
            return new Fraction(v1.Numer * v2.Denom + v2.Numer * v1.Denom, v1.Denom * v2.Denom);
        }

        public static Fraction operator -(Fraction v1, Fraction v2) {
            return new Fraction(v1.Numer * v2.Denom - v2.Numer * v1.Denom, v1.Denom * v2.Denom);
        }

        public static Fraction operator *(Fraction v1, Fraction v2) {
            return new Fraction(v1.Numer * v2.Numer, v1.Denom * v2.Denom);
        }

        public static Fraction operator *(Fraction v, BigInteger n) {
            return new Fraction(v.Numer * n, v.Denom);
        }

        public static Fraction operator /(Fraction v1, Fraction v2) {
            return new Fraction(v1.Numer * v2.Denom, v1.Denom * v2.Numer);
        }

        public static Fraction operator /(Fraction v, BigInteger n) {
            return new Fraction(v.Numer, v.Denom * n);
        }

        public static Fraction Abs(Fraction v) {
            return new Fraction(BigInteger.Abs(v.Numer), v.Denom);
        }

        public void Reduce() {
            if (Denom == 0) {
                throw new DivideByZeroException();
            }

            if (Numer == 0) {
                Denom = 1;
                return;
            }

            int sign = ((Numer < 0) ^ (Denom < 0)) ? -1 : +1;
            BigInteger n = Numer, d = Denom;

            d = BigInteger.Abs(d);
            n = BigInteger.Abs(n);

            if (n > d) {
                BigInteger temp = n;
                n = d;
                d = temp;
            }

            BigInteger rem;
            while ((rem = d % n) != 0) {
                d = n;
                n = rem;
            }

            Numer = (sign > 0 ? BigInteger.Abs(Numer) : -BigInteger.Abs(Numer)) / n;
            Denom = BigInteger.Abs(Denom) / n;
        }

        public override string ToString() {
            return Denom != 1 ? $"{Numer}/{Denom}" : $"{Numer}";
        }

        public override int GetHashCode() {
            return Numer.GetHashCode() ^ Denom.GetHashCode();
        }

        public override bool Equals(object obj) {
            return obj is Fraction v && Numer == v.Numer && Denom == v.Denom;
        }

        public MultiPrecision<N> ToMultiPrecision<N>() where N : struct, IConstant {
            MultiPrecision<Plus1<N>> n = $"{Numer}", d = $"{Denom}";

            return MultiPrecisionUtil.Convert<N, Plus1<N>>(n / d);
        }
    }
}
