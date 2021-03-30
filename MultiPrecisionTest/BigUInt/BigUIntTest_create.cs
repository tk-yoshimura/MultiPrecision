using Microsoft.VisualStudio.TestTools.UnitTesting;

using MultiPrecision;

using System;

namespace MultiPrecisionTest.BigUInt {
    [TestClass]
    public partial class BigUIntTest {
        [TestMethod]
        public void CreateTest() {
            UInt32[] value1 = new UInt32[] { 0x1234u, 0x5678u, 0x9ABCu, 0xDEF0u };
            UInt32[] value2 = new UInt32[] { 0x3478u, 0xFEDCu, 0x2341u, 0x6785u, 0xABC9u, 0xEF0Du };

            BigUInt<Pow2.N4> n1 = new BigUInt<Pow2.N4>();
            BigUInt<Pow2.N4> n2 = new BigUInt<Pow2.N4>(2u);
            BigUInt<Pow2.N4> n3 = new BigUInt<Pow2.N4>(0x12345678ABCDEFul);
            BigUInt<Pow2.N4> n4 = new BigUInt<Pow2.N4>(value1);
            BigUInt<Pow2.N4> n5 = 2u;
            BigUInt<Pow2.N4> n6 = 0x12345678ABCDEFul;
            BigUInt<Pow2.N4> n7 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value1));
            BigUInt<Pow2.N4> n8 = new BigUInt<Pow2.N4>(value2, -2);
            BigUInt<Pow2.N4> n9 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 2, carry: false);
            BigUInt<Pow2.N4> n10 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 2, carry: true);
            BigUInt<Pow2.N4> n11 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 1, carry: false);
            BigUInt<Pow2.N4> n12 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 1, carry: true);
            BigUInt<Pow2.N4> n13 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 3, carry: false);
            BigUInt<Pow2.N4> n14 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 3, carry: true);
            BigUInt<Pow2.N4> n15 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 0, carry: false);
            BigUInt<Pow2.N4> n16 = new BigUInt<Pow2.N4>(Array.AsReadOnly(value2), 0, carry: true);
            BigUInt<Pow2.N4> n17 = new BigUInt<Pow2.N4>(value1, 0);
            BigUInt<Pow2.N8> n18 = new BigUInt<Pow2.N8>(value1, 1);
            BigUInt<Pow2.N8> n19 = new BigUInt<Pow2.N8>(value1, 0);

            value1[1] = 0xFFFFu;

            Console.WriteLine(n1);
            Console.WriteLine(n2);
            Console.WriteLine(n3);
            Console.WriteLine(n4);
            Console.WriteLine(n5);
            Console.WriteLine(n6);
            Console.WriteLine(n7);
            Console.WriteLine(n8);
            Console.WriteLine(n9);
            Console.WriteLine(n10);
            Console.WriteLine(n11);
            Console.WriteLine(n12);
            Console.WriteLine(n13);
            Console.WriteLine(n14);
            Console.WriteLine(n15);
            Console.WriteLine(n16);
            Console.WriteLine(n17);
            Console.WriteLine(n18);
            Console.WriteLine(n19);

            CollectionAssert.AreEqual(new UInt32[] { 0u, 0u, 0u, 0u }, n1.Value);
            CollectionAssert.AreEqual(new UInt32[] { 2u, 0u, 0u, 0u }, n2.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x78ABCDEFu, 0x123456u, 0u, 0u }, n3.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x1234u, 0x5678u, 0x9ABCu, 0xDEF0u }, n4.Value);
            CollectionAssert.AreEqual(new UInt32[] { 2u, 0u, 0u, 0u }, n5.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x78ABCDEFu, 0x123456u, 0u, 0u }, n6.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x1234u, 0x5678u, 0x9ABCu, 0xDEF0u }, n7.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x2341u, 0x6785u, 0xABC9u, 0xEF0Du }, n8.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x2341u, 0x6785u, 0xABC9u, 0xEF0Du }, n9.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x2342u, 0x6785u, 0xABC9u, 0xEF0Du }, n10.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0xFEDCu, 0x2341u, 0x6785u, 0xABC9u }, n11.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0xFEDDu, 0x2341u, 0x6785u, 0xABC9u }, n12.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x6785u, 0xABC9u, 0xEF0Du, 0u }, n13.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x6786u, 0xABC9u, 0xEF0Du, 0u }, n14.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x3478u, 0xFEDCu, 0x2341u, 0x6785u }, n15.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x3479u, 0xFEDCu, 0x2341u, 0x6785u }, n16.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x1234u, 0x5678u, 0x9ABCu, 0xDEF0u }, n17.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x0000u, 0x1234u, 0x5678u, 0x9ABCu, 0xDEF0u, 0x0000u, 0x0000u, 0x0000u }, n18.Value);
            CollectionAssert.AreEqual(new UInt32[] { 0x1234u, 0x5678u, 0x9ABCu, 0xDEF0u, 0x0000u, 0x0000u, 0x0000u, 0x0000u }, n19.Value);
        }

        [TestMethod]
        public void BadCreateTest() {
            UInt32[] value1 = new UInt32[] { 0x1234u, 0x5678u, 0x9ABCu };
            UInt32[] value2 = new UInt32[] { 0x3478u, 0xFEDCu, 0x2341u, 0x6785u, 0xABC9u, 0xEF0Du };
            UInt32[] value_full = new UInt32[] { ~0u, ~0u, ~0u, ~0u };

            Assert.ThrowsException<OverflowException>(() => {
                BigUInt<Pow2.N4> n = new BigUInt<Pow2.N4>(Array.AsReadOnly(value_full), 0, carry: true);
            });

            Assert.ThrowsException<ArgumentException>(() => {
                BigUInt<Pow2.N4> n = new BigUInt<Pow2.N4>(value1);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                BigUInt<Pow2.N4> n = new BigUInt<Pow2.N4>(value2, -1);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                BigUInt<Pow2.N4> n = new BigUInt<Pow2.N4>(value1, 2);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                BigUInt<Pow2.N4> n = new BigUInt<Pow2.N4>(Array.AsReadOnly(value_full), -1, carry: true);
            });
        }
    }
}
