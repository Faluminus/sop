using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Security;

namespace projekt1
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Inventory inv = new Inventory();

            while (true)
            {
                Console.WriteLine("/Add for adding item");
                Console.WriteLine("/Rem for removing item");
                Console.WriteLine("/Use for testing item");
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "/add":
                        int input = ReadIntInput("(0) for weapon , (1) for consumable , (2) for armor");
                        switch (input)
                        {

                            case 0:
                                Console.WriteLine("Enter melee name:");
                                string name = Console.ReadLine();

                                int damage = ReadIntInput($"Enter {name} damage:");
                                int durability = ReadIntInput($"Enter {name} durability:");
                                Weapon weapon = new Weapon(name, damage, durability);
                                inv.Add(weapon);

                                break;
                            case 1:
                                Console.WriteLine("Enter consumable name:");
                                name = Console.ReadLine();
                                int heal = ReadIntInput("Enter heal amout:");
                                int expiration = ReadIntInput($"Enter {name} expiration:");
                                Consumables consumable = new Consumables(name, expiration, heal);
                                inv.Add(consumable);

                                break;
                            case 2:
                                Console.WriteLine("Enter armor name:");
                                name = Console.ReadLine();
                                int protection = ReadIntInput($"Enter {name} protection rate");
                                durability = ReadIntInput($"Enter {name} durability");
                                Armor armor = new Armor(name, protection, durability);
                                inv.Add(armor);
                                break;
                        }
                        break;
                    case "/rem":
                        WriteInventory(inv);
                        Console.WriteLine();
                           string userinput = Console.ReadLine();
                            foreach(string x in inv.ShowInventory())
                            {
                                 if(x.Equals(userinput))
                                {
                                    inv.Remove(x);
                                }
                            
                            }
                   
                        break;
                    case "/use":
                        WriteInventory(inv);
                        userinput = Console.ReadLine();
                        Console.WriteLine();
                        int chill = inv.Use(userinput);
                        if (!chill.Equals(0))
                        {
                            Console.WriteLine($"You can use {userinput} {chill} times");
                        }
                        else
                        {
                            Console.WriteLine("Item eaten/destroyed");
                            Console.WriteLine();
                        }
                        break;
                }
            }
        }
        private static int ReadIntInput(string message)
        {
            int value;
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
        private static void WriteInventory(Inventory inv)
        {
            
            Console.WriteLine("Select item you want to interact with");
            Console.WriteLine("-------------------------------");
            foreach (string x in inv.ShowInventory())
            {
                Console.Write(x + " ");
            }
        }
    }
}
