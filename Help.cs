using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace sänkaskepp
{
    public class Help
    {
        IGraphics ColorWriter { get; }

        public Help(IGraphics colorWriter)
        {
            this.ColorWriter = colorWriter;
        }

        public void PrintHelp() //Metoden skriver ut hela hjälpavsnittet.
        {
            ColorWriter.PrintColorString("green", "\tRULES\n");
            Console.WriteLine("\tEach player starts off with 7 ships of varying sizes.\n" +
                "\tThe ships are placed randomly across the grid.\n" +
                "\tThe players take turns firing at each other. One shot/turn.\n" +
                "\tThe player who first destroys all enemy ships wins.\n");

            ColorWriter.PrintColorString("green", "\tCOMMANDS\n");
            Console.WriteLine("\tShots are fired by entering a valid row from A-J + enter. \n" +
                "\tFollowed by a valid column number between 0-9 + enter.\n" +
                "\tAfter each shot you may go back to the main menu by typing -q + enter.\n" +
                "\tOr see remaining ships by typing -s + enter for status.\n");

            ColorWriter.PrintColorString("green", "\tDISPLAY\n");
            Console.WriteLine("\tThe ships are drawn with their ship-id and color depending on ship type:");
            ColorWriter.PrintColorString("red", "\t\tSubmarine\t\t#\n");
            ColorWriter.PrintColorString("cyan", "\t\tTorpedo boat\t\t##\n");
            ColorWriter.PrintColorString("yellow", "\t\tDestroyer\t\t###\n");
            ColorWriter.PrintColorString("blue", "\t\tAircraft carrier\t####\n");

            Console.WriteLine($"\n\tThe shots YOU fire are logged in your log grid:");
            ColorWriter.PrintColorString("dkgray", "\t\t0 = Unknown ocean\n");
            ColorWriter.PrintColorString("red", "\t\t1 = Missed shot\n");
            ColorWriter.PrintColorString("magenta", "\t\t8 = Hit!\n");

            Console.WriteLine($"\n\tEnemy fire is logged in your ship grid:");
            ColorWriter.PrintColorString("dkgray", "\t\t0 = Empty ocean\n");
            ColorWriter.PrintColorString("dkgray", "\t\t0 = Missed shot\n");
            ColorWriter.PrintColorString("dkgray", "\t\t9 = Destroyed ship\n");

            Console.Write("\n\tPress any key to go back: ");
        }
    }
}
