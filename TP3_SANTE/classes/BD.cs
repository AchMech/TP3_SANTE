//--------------------------------------------
// BD.cs
// Achraf Mechmachi
// Projet Vision Santé
// 27 Avril 2025
//--------------------------------------------
using System.IO;

namespace Tp3_VisionSante
{
    static class BD
    {
        public static List<Citoyen> Citoyens = new();
        //----------------------------------------------
        //
        //----------------------------------------------
        public static void ChargerBD()
        {
            ChargerPopulation();
            ChargerProblemes();
            ChargerUtilisations();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private static void ChargerPopulation()
        {
            string[] lignes = File.ReadAllLines(@"C:\Users\light\OneDrive\Bureau\BD\population.txt");
            foreach (var ligne in lignes)
            {
                string[] parties = ligne.Split(';');

                if (parties.Length >= 3)
                {
                    // Vérifie si professionnel (5 champs) ou citoyen (3 champs)
                    if (parties.Length == 5)
                    {
                        Professionnel p = new Professionnel()
                        {
                            NAS = parties[0],
                            Nom = parties[1],
                            Naissance = parties[2],
                            CodePS = parties[3],
                            Titre = parties[4]
                        };
                        Citoyens.Add(p);
                    }
                    else if (parties.Length == 3)
                    {
                        Citoyen c = new Citoyen()
                        {
                            NAS = parties[0],
                            Nom = parties[1],
                            Naissance = parties[2]
                        };
                        Citoyens.Add(c);
                    }
                    else
                    {
                        
                        Console.WriteLine($"[Erreur] Ligne population invalide : {ligne}");
                    }
                }
            }
        }
        //----------------------------------------------
        //
        //----------------------------------------------

        private static void ChargerProblemes()
        {
            string[] lignes = File.ReadAllLines(@"C:\Users\light\OneDrive\Bureau\BD\problemes.txt");
            foreach (var ligne in lignes)
            {
                string[] parties = ligne.Split(';');
                int nas = int.Parse(parties[0]);
                Citoyen? c = TrouverCitoyen(nas);
                if (c != null)
                {
                    if (parties.Length == 5) // Blessure
                    {
                        c.Problemes.Add(new Blessure(
                            nas,
                            parties[1],
                            parties[2],
                            parties[3],
                            parties[4]
                        ));
                    }
                    else if (parties.Length == 6) // Maladie
                    {
                        c.Problemes.Add(new Maladie(
                            nas,
                            parties[1],
                            parties[2],
                            parties[3],
                            parties[4],
                            int.Parse(parties[5])
                        ));
                    }
                }
            }
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private static void ChargerUtilisations()
        {
            string[] lignes = File.ReadAllLines(@"C:\Users\light\OneDrive\Bureau\BD\utilisations.txt");
            foreach (var ligne in lignes)
            {
                string[] parties = ligne.Split(';');
                int nas = int.Parse(parties[0]);
                Citoyen? c = TrouverCitoyen(nas);
                if (c != null)
                {
                    if (parties.Length == 4) // RendezVous
                    {
                        c.Ressources.Add(new RendezVous(
                            nas,
                            parties[1],
                            parties[2],
                            parties[3]
                        ));
                    }
                    else if (parties.Length == 6) // Hospitalisation
                    {
                        c.Ressources.Add(new Hospitalisation(
                            nas,
                            parties[1],
                            parties[2],
                            parties[3],
                            parties[4],
                            int.Parse(parties[5])
                        ));
                    }
                }
            }
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private static Citoyen? TrouverCitoyen(int nas)
        {
            return Citoyens.FirstOrDefault(c => int.Parse(c.NAS) == nas);
        }
    }
}

