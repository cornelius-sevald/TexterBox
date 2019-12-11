using System.IO;
using System.Linq;
using System;

class Program
{
    static void Main(string[] args)
    {
        GameManager game = GameManager.Instance;
        game.GameLoop();
    }
}