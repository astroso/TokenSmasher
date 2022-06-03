using System;
using System.Drawing;
using Console = Colorful.Console;

namespace TokenSmasher.Helpers {
    public class ConsoleHelpers {
        public static void PrintWatermark() {
            Console.ForegroundColor = Color.Magenta;
            Console.WriteLine(@"


████████╗ ██████╗ ██╗  ██╗███████╗███╗   ██╗    ███████╗███╗   ███╗ █████╗ ███████╗██╗  ██╗███████╗██████╗ 
╚══██╔══╝██╔═══██╗██║ ██╔╝██╔════╝████╗  ██║    ██╔════╝████╗ ████║██╔══██╗██╔════╝██║  ██║██╔════╝██╔══██╗
   ██║   ██║   ██║█████╔╝ █████╗  ██╔██╗ ██║    ███████╗██╔████╔██║███████║███████╗███████║█████╗  ██████╔╝
   ██║   ██║   ██║██╔═██╗ ██╔══╝  ██║╚██╗██║    ╚════██║██║╚██╔╝██║██╔══██║╚════██║██╔══██║██╔══╝  ██╔══██╗
   ██║   ╚██████╔╝██║  ██╗███████╗██║ ╚████║    ███████║██║ ╚═╝ ██║██║  ██║███████║██║  ██║███████╗██║  ██║
   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝    ╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
                                                                                                           ");
            Console.ForegroundColor = Color.FromArgb(138, 142, 255);
        }

        public static void PrintWithColor(string info, string text, Color color, bool newLine = false) {
            if (newLine && !text.Contains("\n"))
                text += "\n";

            Console.Write($" [{DateTime.Now:HH:mm:ss}] > ", Color.FromArgb(80, 80, 80));
            Console.Write($"[{info}] ", color);
            Console.Write(text);
        }
    }
}
