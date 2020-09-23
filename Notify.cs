using System;
using System.Collections.Generic;
using System.Text;

namespace DrowsyDoc
{
    class Notify
    {
        //Alarm Will Trigger While The User is Drowsy
        public static void Alarm()
        {
                Console.WriteLine("Beeping");
                Console.Beep(500,700);
        }
    }
}
