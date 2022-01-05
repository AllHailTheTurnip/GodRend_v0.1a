#define USECONSOLE

using System;
using System.Collections.Generic;
using static System.Console;


namespace GodRendSource
{
    public static class Message
    {
        public static string VerticalList<T>(List<T> list, string bulletpoint = " - ")
        {
            string output = string.Empty;

            foreach (var item in list)
            {
                output += bulletpoint + item + "\n";
            }

            return output;
        }

        public static string VerticalList<T>(T[] list, string bulletpoint = " - ")
        {
            string output = string.Empty;

            foreach (var item in list)
            {
                output += bulletpoint + item + "\n";
            }

            return output;
        }

        public static void ErrorAnyKey(string message)
        {
            Message.Narrate("Error!" + message);
            Message.Narrate("Press any key to continue.");
            ReadKey(true);
        }

        public static void Narrate(Object narration)
        {
#if USECONSOLE
            WriteLine(narration);
#elif USENOTHING
// Nothing, lol!
#endif
        }

        public static void Narrate(List<String> narration)
        {
#if USECONSOLE
            foreach (String item in narration)
            {
                WriteLine(item);
            }
#elif USENOTHING
// Nothing, lol!
#endif
        }

        public static void NarrateMiss()
        {
            Narrate("Missed.");
        }

        public static void NarrateMiss(CheckHitResult result, Protection protection)
        {
            Narrate("Missed. Check: " + result.check + " >> " + protection.dodge);
        }

        public static void NarrateHitCheck(CheckHitResult hitResult, Protection protection)
        {
            Narrate("Check: " + hitResult.check + " >> " + protection.dodge);
        }

        public static void NarrateHitSuccessDegree(CheckHitResult checkHit, AbilityResult result, int finalDamage)
        {
            if (checkHit.wasCritical)
            {
                Narrate("Critical success!");
            }
            else
            {
                Narrate("Success");
            }

            if (checkHit.wasBypass)
            {
                Narrate("Bypassed armor for " + finalDamage + " direct damage.");
            }
            else
            {
                Narrate("Hit for " + finalDamage + " damage.");
            }
        }
    }
}