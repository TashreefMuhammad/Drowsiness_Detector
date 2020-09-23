using System;
using System.Collections.Generic;
using System.Text;

namespace DrowsyDoc
{
    class Notify
    {
        public static void Alarm()
        {
                Console.WriteLine("Beeping");
                Console.Beep(500,500);
        }
    }
}
