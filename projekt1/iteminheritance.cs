using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using System.Security.Policy;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.ComponentModel.Design;
using System.Security;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.ComponentModel;

internal class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void Add(Item item)
    {
        items.Add(item);
    }
    public void Remove(string name)
    {

        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[items.Count - 1] ;
            if (item.Name == name)
            {
                items.RemoveAt(i);
            }
        }
    }
    public int Use(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            
            if (items[i].Name == name)
            {
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                switch (Convert.ToString(items[i].GetType()))
                {
                    case "Weapon":
                        int x = (((Weapon)items[i]).Durability = ((Weapon)items[i]).Durability - 1);
                        if (x.Equals(0))
                        {
                            items.RemoveAt(i);
                        }
                        return x;
                    case "Armor":
                        x = (((Armor)items[i]).Durability = ((Armor)items[i]).Durability -1);
                        if(x.Equals(0))
                        {
                            items.RemoveAt(i);
                        }
                        return x;
                    case "Consumables":
                        if (((Consumables)items[i]).Expiration.Equals(0))
                        {
                           
                            wplayer.URL = "C:\\Users\\ja\\source\\repos\\projekt1\\projekt1\\Sounds\\hit.mp3";
                            wplayer.controls.play();
                        }
                            items.RemoveAt(i);
                        return 0;
                        
                }
            }
        }
        return 0;
    }
    public string[] ShowInventory()
    {
        string[] names = new string[items.Count];
        int i = 0;
        foreach(Item item in items)
        {
            names[i] = item.Name;
            i++;
        }
        return names;
    }
  
}
public class Item
{
    public string Name;

    public Item(string name)
    {
        this.Name = name;
    }
}
public class Weapon : Item
{
    public int Damage { get; set; }
    public int Durability { get; set; }
    

    public Weapon(string name, int damage, int durability) : base(name)
    {
        this.Damage = damage;
        this.Durability = durability;
    }
}
public class Consumables : Item
{
    public int Heal { get; set; }
    public int Expiration { get; set; }

    public Consumables(string name, int expiration, int heal) : base(name)
    {
        this.Heal = heal;
        this.Expiration = expiration;
        new Thread(Timer).Start();
    }

    private void Timer()
    {
        for (int i = this.Expiration; i >= 0; i--)
        {
            this.Expiration = i;
            Thread.Sleep(999);
        }
    }
}
public class Armor : Item
{
    public int Protection { get; set; }
    public int Durability { get; set; }

    public Armor(string name ,int protection, int  durability):base(name)
    {
        this.Protection = protection;
        this.Durability = durability;
    }
}

