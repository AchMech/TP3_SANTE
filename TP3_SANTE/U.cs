using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3_VisionSante
{
    internal class U
    {
       
        public static Random Rdm = new Random();

        //----------------------------------------------
        //
        //----------------------------------------------
        static public void Entete()
        {
            CLS();

            W("\t\t\t");
            for (int i = 0; i < 33; i++)
                W("_");

            W("\t\t\t");
            WL("\n\t\t\t|\t\t\t\t|\n\t\t\t|\tVotre prénom nom \t|\n\t\t\t|\tprésente\t\t|\n\t\t\t|\tVISION SANTÉ\t\t|");
            W("\t\t\t");
            W("|");
            for (int i = 0; i < 31; i++)
                W("_");

            W("|");
            WL("\n");
        }
        public static void WL(string s= "")
        {
            Console.WriteLine(s);
        }
        public static void W(string s)
        {
            Console.Write(s);
        }
        public static void P(string msg="")
        {
            WL("\n\n" + msg);
            W("Appuyez sur une touche...");
            Console.ReadLine();
        }

        public static void CLS()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
        }
        public static void Titre(string t, bool cls=true)
        {
            if (cls)
            {
                U.CLS();
                Entete();
            }
            foreach (Char c in t)
            {
                W("-");
            }
            WL("\n" + t);
            foreach (Char c in t)
            {
                W("-");
            }
            WL();
        }

        public static string? RL()
        {
            return Console.ReadLine();
        }
        public static char RC()
        {
            ConsoleKeyInfo cle = Console.ReadKey();
            return cle.KeyChar;
        }

        public static void Sep(string texte)
        {
            WL(texte);
            foreach(char c in texte)
            {
                W("_");
            }
            WL();
        }

    }
}
