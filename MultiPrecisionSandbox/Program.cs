using MultiPrecision;
using System.IO;

namespace MultiPrecisionSandbox {
    class Program {
        static void Main(string[] args) {
            using (StreamWriter writer = new StreamWriter("erfc_n.csv")) {
                for (int bit = 64; bit <= 10240; bit += 64) {
                    writer.Write($",{bit}");
                }
                writer.Write("\n");

                for (double z = 2; z <= 120; z += 0.0625) {
                    writer.Write($"{z}");

                    for (int bit = 64; bit <= 10240; bit += 64) {
                        long n = ErfcConvergenceTable.N(bit, z);

                        writer.Write($",{n}");
                    }
                    writer.Write("\n");
                }
            }

            int[] bits = new int[] { 128, 160, 192, 224, 256, 320, 384, 448, 512, 640, 768, 896, 1024, 1280, 1536, 1792, 2048, 2560, 3072, 3584, 4096, 5120, 6144, 7168, 8192 };

            using (StreamWriter writer = new StreamWriter("erfc_n2.csv")) {
                foreach (int bit in bits) {
                    writer.Write($",{bit}");
                }
                writer.Write("\n");

                for (double z = 2; z <= 100; z += 0.25) {
                    writer.Write($"{z}");

                    foreach (int bit in bits) {
                        long n = ErfcConvergenceTable.N(bit, z);

                        writer.Write($",{n}");
                    }
                    writer.Write("\n");
                }
            }
        }
    }
}
