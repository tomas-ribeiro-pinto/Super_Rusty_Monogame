using System;
using App05_Super_Rusty;

namespace Super_Rusty_App05
{
    /// <summary>
    /// This class is part of the Super Rusty game.
    /// </summary>
    /// <author>Tomás Pinto</author>
    /// <version>19th May 2022</version>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
