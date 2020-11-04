using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace sänkaskepp
{
    public interface IGraphics
    {
        void PrintColorString(string color, string text);
        void PrintColorGrid(int colorId, int padding);
        void PrintIntroGraphic();
        void PrintDefeatGraphic();
        void PrintVictoryGraphic();
    }

    public class Graphics : IGraphics  //Detta är en hjälpklass för att skriva ut grafik och färger
    {
        public void PrintColorGrid(int cellValue, int padding) //Metod för att skriva ut båtarna i rätt färg.
        {
            List<ConsoleColor> colors = new List<ConsoleColor> {
                {ConsoleColor.DarkGray },//0 Ocean
                {ConsoleColor.Red },     //1 Submarine & miss
                {ConsoleColor.Red },     //2 Submarine
                {ConsoleColor.Cyan },    //3 Torpedo boat
                {ConsoleColor.Cyan },    //4 Torpedo boat
                {ConsoleColor.Yellow },  //5 Destroyer
                {ConsoleColor.Yellow },  //6 Destroyer
                {ConsoleColor.Blue },    //7 Aircraft carrier
                {ConsoleColor.Magenta }, //8 Träff
                {ConsoleColor.DarkGray },//9 Vrak
                {ConsoleColor.Green },   //10
            };

            try
            {
                Console.ForegroundColor = colors[cellValue];
                Console.Write($"{cellValue}".PadLeft(padding));
                Console.ResetColor();
            }
            catch (Exception)
            {
                Debug.WriteLine("Color was not in list");
                Console.Write(cellValue);
            }
        }

        public void PrintColorString(string color, string text) //Metod för att skriva ut färgad text
        {
            Dictionary<string, ConsoleColor> colors = new Dictionary<string, ConsoleColor>()
            {
                {"white", ConsoleColor.White },
                {"green", ConsoleColor.Green },
                {"magenta", ConsoleColor.Magenta },
                {"blue", ConsoleColor.Blue },
                {"gray", ConsoleColor.Gray },
                {"dkgray", ConsoleColor.DarkGray },
                {"cyan", ConsoleColor.Cyan },
                {"yellow", ConsoleColor.Yellow },
                {"red", ConsoleColor.Red },
            };

            try
            {
                Console.ForegroundColor = colors[color.ToLower()];
                Console.Write(text);
                Console.ResetColor();
            }
            catch (Exception)
            {
                Debug.WriteLine("Color was not in list");
                Console.Write(text);
            }
        }

        public void PrintIntroGraphic() //Jag har använt http://patorjk.com/software/taag/ för att generera denna ascii-art.
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"
    __________         __    __  .__                .__    .__              
    \______   \_____ _/  |__/  |_|  |   ____   _____|  |__ |__|_____  ______
     |    |  _/\__  \\   __\   __\  | _/ __ \ /  ___/  |  \|  \____ \/  ___/
     |    |   \ / __ \|  |  |  | |  |_\  ___/ \___ \|   Y  \  |  |_> >___ \ 
     |______  /(____  /__|  |__| |____/\___  >____  >___|  /__|   __/____  >
            \/      \/                     \/     \/     \/   |__|       \/ 
            ");
            Console.ResetColor();
        }

        public void PrintDefeatGraphic() //Jag har använt http://patorjk.com/software/taag/ för att generera denna ascii-art.
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
      _________                           ._.
     /   _____/ __________________ ___.__.| |
     \_____  \ /  _ \_  __ \_  __ <   |  || |
     /        (  <_> )  | \/|  | \/\___  | \|
    /_______  /\____/|__|   |__|   / ____| __
            \/                     \/      \/
            ");
            Console.ResetColor();
        }

        public void PrintVictoryGraphic() //Jag har använt http://patorjk.com/software/taag/ för att generera denna ascii-art.
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
    _________                                     __        .__          __  .__                     ._.
    \_   ___ \  ____   ____    ________________ _/  |_ __ __|  | _____ _/  |_|__| ____   ____   _____| |
    /    \  \/ /  _ \ /    \  / ___\_  __ \__  \\   __\  |  \  | \__  \\   __\  |/  _ \ /    \ /  ___/ |
    \     \___(  <_> )   |  \/ /_/  >  | \// __ \|  | |  |  /  |__/ __ \|  | |  (  <_> )   |  \\___ \ \|
     \______  /\____/|___|  /\___  /|__|  (____  /__| |____/|____(____  /__| |__|\____/|___|  /____  >__
            \/            \//_____/            \/                     \/                    \/     \/ \/
            ");
            Console.ResetColor();
        }

    }
}
