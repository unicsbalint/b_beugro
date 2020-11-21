using System;
using System.Collections.Generic;

namespace second_task
{
    class Program
    {
        static Random rnd = new Random();
         static List<int> randomNumbers(int b, byte db) //Visszaad "db" egyedi random számot [1 ; b[ intervallumon                                          
         {
            if (b <= db)
            {
                throw new Exception(string.Format("Nem tudok {0} db egyedi random számot generálni 1-{1} között!", db, b));
            }
           List<int> randomList = new List<int>();
           int num;
            for (int i = 0; i < db; i++)
            {
                num = rnd.Next(1, b);
                if (!randomList.Contains(num))
                {
                    randomList.Add(num);
                }
                else
                {
                    i--;
                }
            }
           
            return randomList;
         }
        static void Main(string[] args)
        {
            List<int> randomNums = randomNumbers(51, 10);

            Console.ReadKey();
        }
    }
}
