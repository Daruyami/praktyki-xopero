using System;

namespace OOPGameTest
{
    public class Player
    {
        public string Name;
        public uint Hitpoints;

        public Player(string name)
        {
            Name = name;
        }

        public string Greet()
        {
            return ("Hello, my names "+this.Name);
        }
    }
}