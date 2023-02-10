using Day24;
using System.Security.Cryptography.X509Certificates;

namespace Day24_Tests
{
    [TestClass]
    public class Day24_Tests
    {
        [TestMethod]
        public void TestConvertNibble()
        {
            // arrange
            ALU alu = new();

            // act
            alu.QueueInputDigits(9);
            alu.ParseInstruction("inp w");
            alu.ParseInstruction("add z w");
            alu.ParseInstruction("mod z 2");
            alu.ParseInstruction("div w 2");
            alu.ParseInstruction("add y w");
            alu.ParseInstruction("mod y 2");
            alu.ParseInstruction("div w 2");
            alu.ParseInstruction("add x w");
            alu.ParseInstruction("mod x 2");
            alu.ParseInstruction("div w 2");
            alu.ParseInstruction("mod w 2");

            string result = $"{alu.W}{alu.X}{alu.Y}{alu.Z}";

            // assert
            Assert.AreEqual("1001", result);
        }

        [TestMethod]
        public void TestDigit13()   // most significant digit
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(9);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 1");
                    alu.ParseInstruction("add x 11");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 5");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}");
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit12()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(61);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 1");
                    alu.ParseInstruction("add x 13");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 5");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
        // assert
        }

        [TestMethod]
        public void TestDigit11()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(98);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");		// 11
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 1");
                    alu.ParseInstruction("add x 12");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit10()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(1);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 10
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 1");
                    alu.ParseInstruction("add x 15");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 15");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit9()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(81);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 9
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 1");
                    alu.ParseInstruction("add x 10");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 2");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit8()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(92);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 8
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 26");
                    alu.ParseInstruction("add x -1");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 2");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit7()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(94);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 7
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 1");
                    alu.ParseInstruction("add x 14");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 5");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit6()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(61);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 6
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 26");
                    alu.ParseInstruction("add x -8");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 8");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit5()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(9);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 5
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 26");
                    alu.ParseInstruction("add x -7");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 14");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit4()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(21);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 4
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 26");
                    alu.ParseInstruction("add x -8");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 12");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}"); // digit0 z = 14 - 22 results in 0.
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit3()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(41);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 3
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 1");
                    alu.ParseInstruction("add x 11");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 7");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}");
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit2()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(96);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");      // 2
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 26");
                    alu.ParseInstruction("add x -2");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 14");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}");
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit1()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.QueueInputDigits(94);
                while (alu.Q)
                {
                    alu.Z = z;
                    alu.ParseInstruction("inp w");       // 1
                    alu.ParseInstruction("mul x 0");
                    alu.ParseInstruction("add x z");
                    alu.ParseInstruction("mod x 26");
                    alu.ParseInstruction("div z 26");
                    alu.ParseInstruction("add x -2");
                    alu.ParseInstruction("eql x w");
                    alu.ParseInstruction("eql x 0");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y 25");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add y 1");
                    alu.ParseInstruction("mul z y");
                    alu.ParseInstruction("mul y 0");
                    alu.ParseInstruction("add y w");
                    alu.ParseInstruction("add y 13");
                    alu.ParseInstruction("mul y x");
                    alu.ParseInstruction("add z y");

                    Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}");
                }
                Console.WriteLine();
            }
            // assert
        }

        [TestMethod]
        public void TestDigit0()
        {
            // arrange
            ALU alu = new();

            // act
            for (int z = 0; z < 26; z++)
            {
                alu.Z = z;
                alu.QueueInputDigits(1);
                alu.ParseInstruction("inp w");      // 0
                alu.ParseInstruction("mul x 0");
                alu.ParseInstruction("add x z");
                alu.ParseInstruction("mod x 26");
                alu.ParseInstruction("div z 26");
                alu.ParseInstruction("add x -13");
                alu.ParseInstruction("eql x w");
                alu.ParseInstruction("eql x 0");
                alu.ParseInstruction("mul y 0");
                alu.ParseInstruction("add y 25");
                alu.ParseInstruction("mul y x");
                alu.ParseInstruction("add y 1");
                alu.ParseInstruction("mul z y");
                alu.ParseInstruction("mul y 0");
                alu.ParseInstruction("add y w");
                alu.ParseInstruction("add y 6");
                alu.ParseInstruction("mul y x");
                alu.ParseInstruction("add z y");

                Console.WriteLine($"w:{alu.W}  x:{alu.X}  y:{alu.Y}  z:{alu.Z}  preset z:{z}");
            }
            // assert
        }
    }
}