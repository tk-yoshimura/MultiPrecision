using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using System;

namespace MultiPrecisionTest.Common {
    public partial class MultiPrecisionTest {
        [TestMethod]
        public void ConvertRoundTest() {
            const int cases = 32;

            MultiPrecision<Pow2.N8>[] tests = new MultiPrecision<Pow2.N8>[cases] {
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000, 0xFFFFFFFF }),
            };

            MultiPrecision<Plus1<Pow2.N4>>[] expects = new MultiPrecision<Plus1<Pow2.N4>>[cases] {
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000001 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000001 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000001 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x80000000, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x80000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x80000000, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x80000000, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000001 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000001 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000001 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000001 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x80000001, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x80000001, 0xFFFFFFFF, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x80000001, 0xFFFFFFFF, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x80000001, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 1, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x00000000, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x80000001, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x80000001, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x80000001, 0xFFFFFFFF, 0xFFFFFFFF }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x80000001, 0xFFFFFFFF }),
            };

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Plus1<Pow2.N4>> actual = tests[i].Convert<Plus1<Pow2.N4>>();

                Console.WriteLine(tests[i].ToHexcode());
                Console.WriteLine(expects[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects[i], actual);

                Console.WriteLine("");
            }
        }

        [TestMethod]
        public void ConvertSizeTest() {
            const int cases = 8;

            MultiPrecision<Pow2.N8>[] tests = new MultiPrecision<Pow2.N8>[cases] {
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x00000000, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0x00000000, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0x00000000, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0x00000000, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0x00000000, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0x00000000, 0x80000000 }),
            };

            MultiPrecision<Pow2.N4>[] expects_n4 = new MultiPrecision<Pow2.N4>[cases] {
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xCCCCCCCD, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xCCCCCCCD, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xCCCCCCCD, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xCCCCCCCD, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0x00000001, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xCCCCCCCD, 0x00000000, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N4>(Sign.Plus, 0, new uint[]{ 0xCCCCCCCD, 0xDDDDDDDD, 0x00000000, 0x80000000 }),
            };

            MultiPrecision<Plus1<Pow2.N4>>[] expects_n5 = new MultiPrecision<Plus1<Pow2.N4>>[cases] {
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xBBBBBBBC, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xBBBBBBBC, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xBBBBBBBC, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0x00000001, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xBBBBBBBC, 0x00000000, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xBBBBBBBC, 0xCCCCCCCC, 0x00000000, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N4>>(Sign.Plus, 0, new uint[]{ 0xBBBBBBBC, 0xCCCCCCCC, 0xDDDDDDDD, 0x00000000, 0x80000000 }),
            };

            MultiPrecision<Plus1<Plus1<Pow2.N4>>>[] expects_n6 = new MultiPrecision<Plus1<Plus1<Pow2.N4>>>[cases] {
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0xAAAAAAAB, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0xAAAAAAAB, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0x00000001, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0xAAAAAAAB, 0x00000000, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0xAAAAAAAB, 0xBBBBBBBB, 0x00000000, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0xAAAAAAAB, 0xBBBBBBBB, 0xCCCCCCCC, 0x00000000, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N4>>>(Sign.Plus, 0, new uint[]{ 0xAAAAAAAB, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0x00000000, 0x80000000 }),
            };

            MultiPrecision<Pow2.N8>[] expects_n8 = new MultiPrecision<Pow2.N8>[cases] {
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x00000000, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0x00000000, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0x00000000, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0x00000000, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0x00000000, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Pow2.N8>(Sign.Plus, 0, new uint[]{ 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0x00000000, 0x80000000 }),
            };

            MultiPrecision<Plus1<Pow2.N8>>[] expects_n9 = new MultiPrecision<Plus1<Pow2.N8>>[cases] {
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x88888888, 0x00000000, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x88888888, 0x99999999, 0x00000000, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0x00000000, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0x00000000, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0x00000000, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Pow2.N8>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0x00000000, 0x80000000 }),
            };

            MultiPrecision<Plus1<Plus1<Pow2.N8>>>[] expects_n10 = new MultiPrecision<Plus1<Plus1<Pow2.N8>>>[cases] {
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x00000000, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x88888888, 0x00000000, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x88888888, 0x99999999, 0x00000000, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0x00000000, 0xCCCCCCCC, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0x00000000, 0xDDDDDDDD, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0x00000000, 0xEEEEEEEE, 0x80000000 }),
                new MultiPrecision<Plus1<Plus1<Pow2.N8>>>(Sign.Plus, 0, new uint[]{ 0x00000000, 0x00000000, 0x88888888, 0x99999999, 0xAAAAAAAA, 0xBBBBBBBB, 0xCCCCCCCC, 0xDDDDDDDD, 0x00000000, 0x80000000 }),
            };

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Pow2.N4> actual = tests[i].Convert<Pow2.N4>();

                Console.WriteLine(tests[i].ToHexcode());
                Console.WriteLine(expects_n4[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects_n4[i], actual);

                Console.WriteLine("");
            }

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Plus1<Pow2.N4>> actual = tests[i].Convert<Plus1<Pow2.N4>>();

                Console.WriteLine(tests[i].ToHexcode());
                Console.WriteLine(expects_n5[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects_n5[i], actual);

                Console.WriteLine("");
            }

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Plus1<Plus1<Pow2.N4>>> actual = tests[i].Convert<Plus1<Plus1<Pow2.N4>>>();

                Console.WriteLine(tests[i].ToHexcode());
                Console.WriteLine(expects_n6[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects_n6[i], actual);

                Console.WriteLine("");
            }

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Pow2.N8> actual = tests[i].Convert<Pow2.N8>();

                Console.WriteLine(tests[i].ToHexcode());
                Console.WriteLine(expects_n8[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects_n8[i], actual);

                Console.WriteLine("");
            }

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Plus1<Pow2.N8>> actual = tests[i].Convert<Plus1<Pow2.N8>>();

                Console.WriteLine(tests[i].ToHexcode());
                Console.WriteLine(expects_n9[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects_n9[i], actual);

                Console.WriteLine("");
            }

            for (int i = 0; i < cases; i++) {
                MultiPrecision<Plus1<Plus1<Pow2.N8>>> actual = tests[i].Convert<Plus1<Plus1<Pow2.N8>>>();

                Console.WriteLine(tests[i].ToHexcode());
                Console.WriteLine(expects_n10[i].ToHexcode());
                Console.WriteLine(actual.ToHexcode());

                Assert.AreEqual(expects_n10[i], actual);

                Console.WriteLine("");
            }
        }
    }
}
