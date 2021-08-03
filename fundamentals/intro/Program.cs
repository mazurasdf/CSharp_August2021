using System;
using System.Collections.Generic;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            //int, double, char, bool, string
            // int faveNum = 23;
            // double pi = 3.14159;
            // char ampersand = '&';
            // string phrase = "Did I shine my shoes today?";
            // string singleChar = "t";
            // bool isWhatever = true;


            // int[] bestNums = {4,8,15,16,23,42};
            // for(int i = 0; i < bestNums.Length; ++i){
            //     if(bestNums[i] > 20){
            //         Console.WriteLine(bestNums[i]);
            //     }
            // }

            List<string> faveIceCreams = new List<string>();

            faveIceCreams.Add("pistachio");
            faveIceCreams.Add("vaniller");
            faveIceCreams.Add("double chocolate chunk");
            faveIceCreams.Add("oreo");
            faveIceCreams.Insert(2, "cookie dough");
            faveIceCreams.RemoveAt(1);
            faveIceCreams.Remove("pistachio");

            
            foreach(string iceCream in faveIceCreams){
                Console.WriteLine(iceCream);
            }


            Console.WriteLine("hey it's me!");
        }
    }
}