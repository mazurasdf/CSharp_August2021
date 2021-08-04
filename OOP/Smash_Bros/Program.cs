using System;
using System.Collections.Generic;

namespace Smash_Bros
{
    abstract class Fighter
    {
        public string Name;
        public int PlayerNum;
        private int percentage;
        public int Percentage
        {
            get {return percentage;}
        }

        public Fighter(string name, int playerNum)
        {
            Name = name;
            PlayerNum = playerNum;
            percentage = 0;
        }

        public void TakeDamage(int amount)
        {
            if(amount > 0)
            {
                percentage += amount;
            }
        }

        public void Attack(Fighter opponent)
        {
            int damage = 5;
            opponent.TakeDamage(damage);

            Console.WriteLine($"{Name} attacked {opponent.Name} for {damage}%!!");
        }

        // public virtual void Special(Fighter opponent)
        // {
        //     int damage = 15;
        //     opponent.TakeDamage(damage);

        //     Console.WriteLine($"{Name} performed a special on {opponent.Name} for {damage}%!!");
        // }
        public abstract void Special(Fighter opponent);
    }

    class Samus : Fighter
    {
        private bool isCharged;

        public Samus(int playerNum) : base("Samus", playerNum)
        {
            isCharged = false;
        }

        public override void Special(Fighter opponent)
        {
            if(isCharged)
            {
                int damage = 30;
                opponent.TakeDamage(damage);
                isCharged = false;

                Console.WriteLine($"{Name} shot her laser arm thingy at {opponent.Name} for {damage}%!!");
            }
            else
            {
                isCharged = true;
                Console.WriteLine($"{Name} charged up her laser arm thingy!");
            }
        }
    }

    class Pikachu : Fighter
    {

        public Pikachu(int playerNum) : base("Pikachu", playerNum)
        {
        }

        public override void Special(Fighter opponent)
        {
            Random rand = new Random();

            int damage = 8;
            opponent.TakeDamage(damage);
            Console.WriteLine("pika");
            Console.WriteLine($"{Name} shocked {opponent.Name} for {damage}%!!");

            opponent.TakeDamage(damage);
            Console.WriteLine("chu!");
            Console.WriteLine($"{Name} shocked {opponent.Name} for {damage}%!!");
            
            if(rand.Next(2) == 0)
            {
                opponent.TakeDamage(damage);
                Console.WriteLine("ahhhhhhhhhhhhh!!!!!");
                Console.WriteLine($"{Name} shocked {opponent.Name} AGAIN for {damage}%!!");
            }
        }
    }

    class Ike : Fighter
    {

        public Ike(int playerNum) : base("Ike", playerNum)
        {
        }

        public override void Special(Fighter opponent)
        {
            Random rand = new Random();

            int damage = rand.Next(1,9);
            damage *= damage;

            if(damage < 16)
            {
                opponent.TakeDamage(damage);
                Console.WriteLine($"{Name} didn't hold his sword long enough and hit {opponent.Name} for {damage}%!!");
            }
            else
            {
                opponent.TakeDamage(damage);
                Console.WriteLine("ahhhhhhhhhhhhh!!!!!");
                Console.WriteLine($"{Name} exploded his sword in {opponent.Name}'s for {damage}%!!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Samus samus = new Samus(1);
            Pikachu pika = new Pikachu(2);
            Ike ike = new Ike(3);

            pika.Special(samus);
            Console.WriteLine(samus.Percentage);
            samus.Attack(ike);
            ike.Special(pika);

        }
    }
}
