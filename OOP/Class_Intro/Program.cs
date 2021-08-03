using System;

namespace Class_Intro
{
    public class Robot
    {
        public string Name;
        public string Color;
        private int oilLevel;

        public Robot(string name){
            Name = name;
            Color = "silver";
            oilLevel = 90;
        }

        public Robot(string name, string color)
        {
            Console.WriteLine("more detailed constructor");
            Name = name;
            Color = color;
            oilLevel = 90;
        }

        public Robot()
        {
            Console.WriteLine("less detailed constructor");
            Name = "Bender";
            Color = "silver";
            oilLevel = 90;
        }

        public void ServeDinner()
        {
            oilLevel -= 30;
            Console.WriteLine("Here I served you up a platter of nuts and bolts");
        }

        public void KillAllHumans()
        {
            oilLevel -= 50;
            Console.WriteLine("Here I served you up a PAIN");
        }

        public void PrintStatus(){
            if(oilLevel > 60)
            {
                Console.WriteLine("Internal status is all good");
            }
            else if (oilLevel > 30){
                Console.WriteLine("Maintenance needed");
            }
            else{
                Console.WriteLine("beep boop bop, pls help :( the light is fading");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Robot sbeve = new Robot("Sbeve", "orange");
            sbeve.PrintStatus();

            sbeve.ServeDinner();
            sbeve.PrintStatus();

            Robot bender = new Robot();
        }
    }
}
