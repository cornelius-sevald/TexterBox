using System.IO;
using System.Linq;
using System;

namespace TexterBox{
    class Program
    {
       static void Main(string[] args)
       {
           GameManager game = GameManager.Instance;
           game.GameLoop();
       }
    }
}