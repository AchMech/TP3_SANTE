using System;

namespace Tp3_VisionSante
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            
            U.WL("\nChargement de la base de données...");
            BD.ChargerBD();
            U.WL("Chargement terminé !");
            System.Threading.Thread.Sleep(1000); 

            Menu menu = new Menu("Profils offerts");

            menu.AjouterOption(new MenuItem('C', "Profil citoyen", ProfilCitoyen));
            menu.AjouterOption(new MenuItem('P', "Profil professionnel de la santé", ProfilProfessionnelSante));

            menu.SaisirOption();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private static void ProfilCitoyen()
        {
            U.Entete();
            Citoyen cit = new Citoyen();
            if (!cit.AfficherSommaire())
                return; 
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private static void ProfilProfessionnelSante()
        {
            U.Entete();
            Professionnel ps = new Professionnel();
            if (!ps.AfficherSommaire())
                return; 
        }
    }
}
 