using System.Data;
using System;

namespace game
{
    class Program
    {
        static void Main()
        {
            /*int[,] array = new int[4,4]{
                {2,4,2,2},
                {2,0,0,2},
                {4,4,0,2},
                {0,2,0,2}
            };*/
            Console.Title = "2048";
            StartGame();
            /*foreach (var item in RemoveZero(GetArrayUpDown(array,false)))
            {
                Console.WriteLine(item);
            }*/

            /*
            getLength(a,b) {a=a ; b=b}
            a0\b1(0)(1)(2)(3)
            (0)  2  ,4  ,2 ,2
            (1)  2  ,0  ,0 ,2
            (2)  4  ,4  ,0 ,2
            (3)  0  ,2  ,0 ,2
            */
        }

        public static void StartGame()
        {
            string str = "";
            start:
            Console.Clear();
            Console.WriteLine("EltanceX");
            Console.WriteLine("2048 Game!\r\nSize(Default 4):");
            str = Console.ReadLine();
            if (IsNumberInt(str))
            {
                Console.WriteLine("Control:wasd   Enter to start...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Illegal Input String!");
                goto start;
            }
            Console.WriteLine("Game Start!");
            int[,] array = new int[int.Parse(str), int.Parse(str)];
            Console.Clear();
            RandomArray(array);
            PrintArray(array);
            while (true)
            {
                string writeIn = Console.ReadLine();
                if (writeIn == "w" || writeIn == "W")
                {
                    Up(array);
                    array = RemoveZeroDownToUp(array);
                }
                else if (writeIn == "a" || writeIn == "A")
                {
                    Left(array);
                    array = RemoveZeroRightToLeft(array);
                }
                else if (writeIn == "s" || writeIn == "S")
                {
                    Down(array);
                    array = RemoveZeroUpToDown(array);
                }
                else if (writeIn == "d" || writeIn == "D")
                {
                    Right(array);
                    array = RemoveZeroLeftToRight(array);
                }
                else
                {
                    Console.WriteLine("Illegal String!");
                    continue;
                }
                Console.Clear();
                RandomArray(array);
                PrintArray(array);
            }
        }

        public static void Right(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0) * 2; i++)
            {
                MoveRight(array);
                array = RemoveZeroLeftToRight(array);
            }
        }

        public static void Left(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0) * 2; i++)
            {
                MoveLeft(array);
                array = RemoveZeroRightToLeft(array);
            }
        }

        public static void Down(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0) * 2; i++)
            {
                MoveDown(array);
                array = RemoveZeroUpToDown(array);
            }
        }

        public static void Up(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0) * 2; i++)
            {
                MoveUp(array);
                array = RemoveZeroDownToUp(array);
            }
        }

        static Random random = new Random();

        public static void RandomArray(int[,] array)
        {
            int arrayLength = array.GetLength(0);
            int randomCount = arrayLength / 4;
            int EmptyCount = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 0)
                    {
                        EmptyCount++;
                    }
                }
            }
            if (EmptyCount != 0 && EmptyCount >= randomCount)
            {
                for (int i = 0; i < randomCount; i++)
                {
                    int num = 0;
                    while (num == 0)
                    {
                        int arrayX = random.Next(0, arrayLength);
                        int arrayY = random.Next(0, arrayLength);
                        int arrayNum = random.Next(0, 2);
                        if (arrayNum == 0)
                        {
                            arrayNum = 2;
                        }
                        else
                        {
                            arrayNum = 4;
                        }
                        if (array[arrayX, arrayY] == 0)
                        {
                            array[arrayX, arrayY] = arrayNum;
                            num = 1;
                        }
                    }
                }
            }
        }

        public static bool IsNumberInt(string str)
        {
            return IsNumber(str, 32, 0);
        }

        public static bool IsNumber(string s, int precision, int scale)
        {
            if ((precision == 0) && (scale == 0))
            {
                return false;
            }
            string pattern = @"(^\d{1," + precision + "}";
            if (scale > 0)
            {
                pattern += @"\.\d{0," + scale + "}$)|" + pattern;
            }
            pattern += "$)";
            return System.Text.RegularExpressions.Regex.IsMatch(s, pattern);
        }

        public static void MoveDown(int[,] array)
        { //?
            for (int i = array.GetLength(1) - 1; i > 0; i--)
            { //i=b1
                for (int j = 0; j < array.GetLength(0); j++)
                { //j=a0
                    if (array[i, j] == array[i - 1, j])
                    {
                        array[i, j] = array[i - 1, j] * 2;
                        array[i - 1, j] = 0;
                    }
                }
            }
        }

        public static void MoveUp(int[,] array)
        {
            for (int i = 0; i < array.GetLength(1) - 1; i++)
            { //i=b1
                for (int j = 0; j < array.GetLength(0); j++)
                { //j=a0
                    if (array[i, j] == array[i + 1, j])
                    {
                        array[i, j] = array[i + 1, j] * 2;
                        array[i + 1, j] = 0;
                    }
                }
            }
        }

        public static void MoveRight(int[,] array)
        { //?
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = array.GetLength(1) - 1; j > 0; j--)
                {
                    if (array[i, j] == array[i, j - 1])
                    {
                        array[i, j] = array[i, j - 1] * 2;
                        array[i, j - 1] = 0;
                    }
                }
            }
        }

        public static void MoveLeft(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1) - 1; j++)
                {
                    if (array[i, j] == array[i, j + 1])
                    {
                        array[i, j] = array[i, j + 1] * 2;
                        array[i, j + 1] = 0;
                    }
                }
            }
        }

        public static void PrintArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static int[,] RemoveZeroUpToDown(int[,] array)
        {
            int[,] returnArray = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int count = array.GetLength(0) - 1;
                for (int j = array.GetLength(1) - 1; j >= 0; j--)
                {
                    if (array[j, i] != 0)
                    {
                        returnArray[count, i] = array[j, i];
                        count--;
                    }
                }
            }
            return returnArray;
        }

        public static int[,] RemoveZeroDownToUp(int[,] array)
        {
            int[,] returnArray = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int count = 0;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[j, i] != 0)
                    {
                        returnArray[count, i] = array[j, i];
                        count++;
                    }
                }
            }
            return returnArray;
        }

        public static int[,] RemoveZeroLeftToRight(int[,] array)
        {
            int[,] returnArray = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int count = array.GetLength(0) - 1;
                for (int j = array.GetLength(1) - 1; j >= 0; j--)
                {
                    if (array[i, j] != 0)
                    {
                        returnArray[i, count] = array[i, j];
                        count--;
                    }
                }
            }
            return returnArray;
        }

        public static int[,] RemoveZeroRightToLeft(int[,] array)
        {
            int[,] returnArray = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int count = 0;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != 0)
                    {
                        returnArray[i, count] = array[i, j];
                        count++;
                    }
                }
            }
            return returnArray;
        }

        /// <summary>
        /// array flip
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[,] GetArrayUpDown(int[,] array)
        {
            int[,] returnArray = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(1); i++)
            { //0,1,2,3 i=b1
                int count = 0;
                for (int j = array.GetLength(0) - 1; j >= 0; j--)
                { //3,2,1,0 j=a0
                    returnArray[count, i] = array[j, i];
                    count++;
                }
            }
            return returnArray;
        }
    }
}
