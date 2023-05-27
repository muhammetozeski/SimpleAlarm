
using System.Timers;
using Timer = System.Timers.Timer;
namespace SimpleAlarm
{
    internal class Program
    {
        static double minutes = 0;
        static DateTime EndTime;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter time (as minutes) for alarm. Eg: 1,2 means 72 seconds.");
            
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out minutes))
                    break;
                Console.WriteLine("Bad input. Please enter time (as minutes) for alarm:");
            }

            Console.CursorVisible = false;

            EndTime = DateTime.Now.AddMinutes(minutes);
            Console.WriteLine(EndTime - DateTime.Now);

            string format = @"hh\:mm\:ss";
            while (true)
            {
                var difference = EndTime - DateTime.Now;
                if ((int)difference.TotalSeconds == 0)
                    TimeUp();

                if (difference < TimeSpan.FromHours(1))
                    format = @"mm\:ss";

                Console.Clear();
                Console.WriteLine(difference.ToString(format));
                Thread.Sleep(1000);
                
            }
            void TimeUp()
            {
                Timer timer = new Timer(1000);
                timer.Elapsed += (_obj, _eventArgs) =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.Beep(1300,100);
                    }
                };
                timer.Start();


                while (true)
                {
                    Console.WriteLine("00:00:00");
                    Thread.Sleep(750);
                    Console.Clear();
                    Thread.Sleep(750);
                }
            }
        }
    }
}