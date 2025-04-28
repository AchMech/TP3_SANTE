//--------------------------------------------
// Citoyen.cs
// Achraf Mechmachi
// 2156548
// Projet Vision Santé
// 27 Avril 2025
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tp3_VisionSante
{
    class Citoyen
    {
        public string? NAS { get; set; }
        public string? Nom { get; set; }
        public string? Naissance { get; set; }
        public List<Probleme> Problemes { get; set; } = new();
        public List<Ressource> Ressources { get; set; } = new();

        public Citoyen()
        {
            NAS = "";
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        public bool AfficherSommaire()
        {
            U.W("NAS du citoyen désiré: ");
            string? nasRecherche = U.RL();

            Citoyen? citoyen = BD.Citoyens
                .OfType<Citoyen>()
                .FirstOrDefault(c => c.NAS == nasRecherche && c.GetType() == typeof(Citoyen));

            if (citoyen == null)
            {
                U.WL("\nNAS invalide. Aucun citoyen trouvé.");
                U.P("Retour au menu principal.");
                return false;
            }

            U.Entete();
            U.WL("\n------------------------------------------------------------------");
            U.WL($"Nom:\t\t{citoyen.Nom}");
            U.WL($"Né le:\t\t{citoyen.Naissance}");
            U.WL($"NAS:\t\t{citoyen.NAS}");
            U.WL("\n------------------------------------------------------------------");
            U.WL("Historique");
            U.WL($"\t{citoyen.Problemes.Count} problèmes");
            U.WL($"\t{citoyen.Ressources.Count} ressources utilisées");
            U.WL();

            Menu menuCitoyen = new Menu("Consulter problèmes ou ressources?", false);
            menuCitoyen.AjouterOption(new MenuItem('P', "Problèmes", () => AfficherSommaireProblemes(citoyen)));
            menuCitoyen.AjouterOption(new MenuItem('R', "Ressources", () => AfficherSommaireRessources(citoyen)));
            menuCitoyen.SaisirOption();

            return true;
        }
        //----------------------------------------------
        //
        //----------------------------------------------

        private void AfficherSommaireProblemes(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Problèmes médicaux de {citoyen.Nom}\n----------------------------------------\n");
            int blessures = citoyen.Problemes.OfType<Blessure>().Count();
            int maladies = citoyen.Problemes.OfType<Maladie>().Count();
            U.WL($"\t{maladies} maladies");
            U.WL($"\t{blessures} blessures");
            U.WL();

            // Menu blessures/maladies/tous
            Menu menuProb = new Menu("Consulter blessures ou maladies?", false);
            menuProb.AjouterOption(new MenuItem('B', "Blessures", () => AfficherBlessures(citoyen)));
            menuProb.AjouterOption(new MenuItem('M', "Maladies", () => AfficherMaladies(citoyen)));
            menuProb.AjouterOption(new MenuItem('T', "Tous problèmes", () => AfficherTousProblemes(citoyen)));
            menuProb.SaisirOption();
        }

        //----------------------------------------------
        //
        //----------------------------------------------

        private void AfficherSommaireRessources(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Ressources utilisées par {citoyen.Nom}\n----------------------------------------\n");
            int rendezVous = citoyen.Ressources.OfType<RendezVous>().Count();
            int hospitalisations = citoyen.Ressources.OfType<Hospitalisation>().Count();
            U.WL($"\t{rendezVous} rendez-vous");
            U.WL($"\t{hospitalisations} hospitalisations");
            U.WL();

            Menu menuRess = new Menu("Consulter rendez-vous ou hospitalisations?", false);
            menuRess.AjouterOption(new MenuItem('R', "Rendez-vous", () => AfficherRendezVous(citoyen)));
            menuRess.AjouterOption(new MenuItem('H', "Hospitalisations", () => AfficherHospitalisations(citoyen)));
            menuRess.AjouterOption(new MenuItem('T', "Toutes les ressources", () => AfficherToutesRessources(citoyen)));
            menuRess.SaisirOption();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private void AfficherBlessures(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Blessures de {citoyen.Nom}:\n");
            U.WL("Type            Début      Guérison   Description ");
            U.WL("_________________________________________________________________");

            foreach (var blessure in citoyen.Problemes.OfType<Blessure>())
            {
                Console.WriteLine("{0,-13} {1,11} {2,11} {3,-30}",
                    blessure.Type,
                    blessure.DateDebut,
                    blessure.DateFin ?? "En cours",
                    blessure.Description);
            }

            U.P();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private void AfficherMaladies(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Maladies de {citoyen.Nom}:\n");
            U.WL("Pathologie             Stade  Début    Guérison   Commentaire ");
            U.WL("_________________________________________________________________");

            foreach (var maladie in citoyen.Problemes.OfType<Maladie>())
            {
                Console.WriteLine("{0,-22} {1,3} {2,11} {3,11} {4,-30}",
                    maladie.Pathologie,
                    maladie.Stade,
                    maladie.DateDebut,
                    maladie.DateFin ?? "En cours",
                    maladie.Description);
            }

            U.P();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private void AfficherTousProblemes(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Problèmes médicaux de {citoyen.Nom}:\n");

            AfficherBlessures(citoyen);
            AfficherMaladies(citoyen);
        }

        //----------------------------------------------
        //
        //----------------------------------------------
        private void AfficherRendezVous(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Rendez-vous de {citoyen.Nom}:\n");
            Console.WriteLine("{0,-22} {1,-12} {2,8}", "Établissement", "Date", "Code PS");
            U.WL("_________________________________________________________________");

            foreach (var rv in citoyen.Ressources.OfType<RendezVous>())
            {
                Console.WriteLine("{0,-22} {1,12} {2,8}",
                    rv.Etablissement,
                    rv.Date,
                    rv.CodePS);
            }

            U.P();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private void AfficherHospitalisations(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Hospitalisations de {citoyen.Nom}:\n");
            Console.WriteLine("{0,-22} {1,12} {2,8} {3,8} {4,12}", "Établissement", "Arrivée", "Code PS", "Chambre", "Départ");
            U.WL("_________________________________________________________________");

            foreach (var hosp in citoyen.Ressources.OfType<Hospitalisation>())
            {
                Console.WriteLine("{0,-22} {1,12} {2,8} {3,8} {4,12}",
                    hosp.Etablissement,
                    hosp.Date,
                    hosp.CodePS,
                    hosp.Chambre,
                    hosp.DateFin ?? "En cours");
            }

            U.P();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private void AfficherToutesRessources(Citoyen citoyen)
        {
            U.Entete();
            U.WL($"Ressources utilisées par {citoyen.Nom}:\n----------------------------------------");

            U.WL("\nRendez-vous:");
            AfficherRendezVous(citoyen);

            U.WL("\nHospitalisations:");
            AfficherHospitalisations(citoyen);
        }
    
    }
}
