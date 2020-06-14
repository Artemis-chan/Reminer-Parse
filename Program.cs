using System;
using System.Text.RegularExpressions;

namespace reminder_parse
{
    class Program
    {
        static void Main(string[] args) //test sentence: remind me to something lipsum ipsum etc in 2 hours 5 minute
        {
            if (args.Length < 1)
            {
                return;
            }

            ulong timeSeconds = 0;

            string q = string.Join(' ', args).ToLower();
            if(Regex.IsMatch(q, @"remind me to .+ in (\d+ (hours?|minutes?|days?)(\s?)+)+"))
            {
                //Time
                var matches = Regex.Matches(q, @"\d+ (hours?|minutes?|days?)");
                foreach (Match m in matches)
                {
                    if(m.Success)
                    {
                        var temp = m.Value.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                        if(ulong.TryParse(temp[0], out ulong time))
                        {
                            timeSeconds += time * TimeModifier(temp[1]);
                        }
                    }
                }

                Console.WriteLine($"Time: {timeSeconds} Seconds");

                //Note
                //var match = Regex.Match(q, @"");
            }
        }

        static ulong TimeModifier(string str)
        {
            
            if(Regex.IsMatch(str, "minutes?"))
                return 60;

            else if(Regex.IsMatch(str, "hours?"))
                return 3600;
            
            else if(Regex.IsMatch(str, "days?"))
                return 86400;
            
            return 1;
        }
    }
}
