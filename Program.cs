
using System.Media;
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

            while (true)
            {
                Console.WriteLine("Enter time (as minutes) for setting timer. Eg: 1,2 means 72 seconds.");
                var input = Console.ReadLine();
                //if(input.Contains('.'))
                if (double.TryParse(input, out minutes))
                    break;
                Console.WriteLine("Bad input.");
            }

            Console.CursorVisible = false;

            EndTime = DateTime.Now.AddMinutes(minutes);
            //Console.WriteLine(EndTime - DateTime.Now);

            string format = @"hh\:mm\:ss";
            while (true)
            {
                var difference = EndTime - DateTime.Now;
                if ((int)difference.TotalSeconds == 0)
                    TimeUp();

                if (difference < TimeSpan.FromHours(1))
                    format = @"mm\:ss";

                Console.Clear();
                Console.Title = difference.ToString(format);
                Console.WriteLine(difference.ToString(format));
                Thread.Sleep(1000);
                
            }
            void TimeUp()
            {
                Timer timer = new Timer(1000);
                timer.Elapsed += (_obj, _eventArgs) =>
                {
                    SoundPlayer soundPlayer = new SoundPlayer(GetEmbeddedResourceByName("Wowpulse.wav"));
                    while (true)
                        soundPlayer.PlaySync();
                };
                timer.Start();


                while (true)
                {
                    Console.Title = "00:00:00";
                    Console.WriteLine("00:00:00");
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.Title = " ";
                    Thread.Sleep(750);
                }
            }
        }

        public static Stream? GetEmbeddedResourceByName(string NameWithExtension)
        {
            return System.Reflection.Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
                + "." + NameWithExtension);
        }
    }
}