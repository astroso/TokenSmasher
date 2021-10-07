using System;

namespace EasyConsoleClass {
    class EasyConsole {

        public static void Watermark ( ) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ( @"


████████╗ ██████╗ ██╗  ██╗███████╗███╗   ██╗    ███████╗███╗   ███╗ █████╗ ███████╗██╗  ██╗███████╗██████╗ 
╚══██╔══╝██╔═══██╗██║ ██╔╝██╔════╝████╗  ██║    ██╔════╝████╗ ████║██╔══██╗██╔════╝██║  ██║██╔════╝██╔══██╗
   ██║   ██║   ██║█████╔╝ █████╗  ██╔██╗ ██║    ███████╗██╔████╔██║███████║███████╗███████║█████╗  ██████╔╝
   ██║   ██║   ██║██╔═██╗ ██╔══╝  ██║╚██╗██║    ╚════██║██║╚██╔╝██║██╔══██║╚════██║██╔══██║██╔══╝  ██╔══██╗
   ██║   ╚██████╔╝██║  ██╗███████╗██║ ╚████║    ███████║██║ ╚═╝ ██║██║  ██║███████║██║  ██║███████╗██║  ██║
   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝    ╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝
                                                                                                           " );
            Console.ForegroundColor = ConsoleColor.White;
            string version = "1.0 [ edited by @astroso ]";
            Console.WriteLine ( $" >> version: {version}" );

        }

        public static void WriteLine ( object value, ConsoleColor Color, bool read ) {
            Console.ForegroundColor = Color;
            Console.WriteLine ( value );
            Console.ForegroundColor = ConsoleColor.White;
            if ( read ) Console.ReadLine ( );
        }

        public static void Write ( object value, ConsoleColor Color, bool read ) {
            Console.ForegroundColor = Color;
            Console.Write ( value );
            Console.ForegroundColor = ConsoleColor.White;
            if ( read ) Console.ReadLine ( );
        }
    }
}
