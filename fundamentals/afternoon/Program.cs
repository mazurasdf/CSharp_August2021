using System;
using System.Collections.Generic;

namespace afternoon
{
    class Program
    {
        public static void printValue(string phrase = "nobody gave me a phrase :(", int times = 2){
            
            for(int i = 0; i < times; ++i){
                Console.WriteLine(phrase);
            }
        }

        public static Dictionary<string, string> randomSundae(){
            Dictionary<string, string> sundae = new Dictionary<string, string>();

            Random rand = new Random();

            string[] flavors = {"vanilla", "chocolate", "pumpkin", "banana", "garlic", "rocky road", "actual road rocks", "goat cheese and mint", "corn on the cob"};
            string[] sauces = {"hot fudge", "hot sauce", "strawberry syrup", "ranch", "caramel", "nutella", "sauerkraut"};
            string[] toppings = {"pecans", "capn crunch", "reeses cups", "wafer rolls", "kit kats", "mini m&ms"};

            sundae.Add("iceCream",flavors[rand.Next(flavors.Length)]);
            sundae.Add("sauce", sauces[rand.Next(sauces.Length)]);
            sundae.Add("topping", toppings[rand.Next(toppings.Length)]);

            return sundae;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // printValue();

            Dictionary<string, string> sundae = randomSundae();

            foreach(var feature in sundae){
                Console.WriteLine($"The {feature.Key} in the sundae is {feature.Value}");
            }

            // float faveNumber = 23.0f;
            // int lessSpecificFaveNumber = (int)faveNumber;
            // string faveString = faveNumber.ToString();

            // Console.WriteLine(faveString);
        }
    }
}
