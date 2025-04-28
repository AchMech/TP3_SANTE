//--------------------------------------------
// Professionnel.cs
// Achraf mehcmachi
// 2156548
// Projet Vision Santé
// 27 Avril 2025
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3_VisionSante
{
    class Professionnel : Citoyen
    {
        public string? CodePS { get; set; }
        public string? Titre { get; set; }
        public List<Citoyen> ListePatients { get; set; } = new();
        public List<Ressource> ListeInterventions { get; set; } = new();

        public Professionnel()
        {
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        public bool AfficherSommaire()
        {
            U.W("Code PS du professionnel désiré: ");
            string? codeRecherche = U.RL();

            Professionnel? ps = BD.Citoyens
                .OfType<Professionnel>()
                .FirstOrDefault(p => p.CodePS == codeRecherche);

            if (ps == null)
            {
                U.WL("\nCode PS invalide. Aucun professionnel trouvé.");
                U.P("Retour au menu principal.");
                return false;
            }

            U.Entete();
            U.WL("\n------------------------------------------------------------------");
            U.WL($"Nom:\t\t{ps.Nom}, {ps.Titre}");
            U.WL($"Né le:\t\t{ps.Naissance}");
            U.WL($"Code PS:\t{ps.CodePS}");
            U.WL("\n------------------------------------------------------------------");

            U.WL("Historique");
            U.WL($"\t{ps.ListePatients.Count} patients");
            U.WL($"\t{ps.ListeInterventions.Count} interventions");
            U.WL();

            Menu menuPS = new Menu("Consulter patients ou interventions de " + ps.Nom + "?", false);
            menuPS.AjouterOption(new MenuItem('P', "Patients", () => AfficherPatients(ps)));
            menuPS.AjouterOption(new MenuItem('I', "Interventions", () => AfficherInterventions(ps)));
            menuPS.SaisirOption();

            return true;
        }

        //----------------------------------------------
        //
        //----------------------------------------------


        private void AfficherPatients(Professionnel ps)
        {
            string tri = SaisirOptionTri();

            U.CLS();
            U.Entete();
            U.WL($"Patients de {ps.Nom}");
            U.WL("------------------------------------");
            Console.WriteLine("{0,-30} {1,9} {2,12} {3,10}", "Nom", "NAS", "Naissance", "Nb Interv");
            U.WL("_________________________________________________________________________________");

            IEnumerable<Citoyen> patients = ps.ListePatients;

            // Tri selon option
            switch (tri)
            {
                case "N": // Tri par naissance
                    patients = patients.OrderBy(c => c.Naissance);
                    break;
                case "A": // Tri par NAS
                    patients = patients.OrderBy(c => c.NAS);
                    break;
                case "O": // Tri par nom
                    patients = patients.OrderBy(c => c.Nom);
                    break;
                case "S": // Sans tri
                default:
                    break;
            }

            foreach (var patient in patients)
            {
                int nbInterventions = BD.Citoyens
                    .OfType<Citoyen>()
                    .FirstOrDefault(c => c.NAS == patient.NAS)?
                    .Ressources
                    .Count(r => r.CodePS == ps.CodePS) ?? 0;

                Console.WriteLine("{0,-30} {1,9} {2,12} {3,10}",
                    patient.Nom,
                    patient.NAS,
                    patient.Naissance,
                    nbInterventions);
            }

            U.P();
        }

        //----------------------------------------------
        //
        //----------------------------------------------
        private void AfficherInterventions(Professionnel ps)
        {
            string tri = SaisirOptionTriIntervention();

            U.CLS();
            U.Entete();
            U.WL($"Interventions de {ps.Nom}");
            U.WL("-----------------------------------------------------");
            Console.WriteLine("{0,-30} {1,9} {2,12} {3,-20}", "Patient", "NAS", "Date", "Établissement");
            U.WL("________________________________________________________________________________");

            IEnumerable<Ressource> interventions = ps.ListeInterventions;

            // Tri selon option
            switch (tri)
            {
                case "D": // Tri par date
                    interventions = interventions.OrderBy(i => i.Date);
                    break;
                case "E": // Tri par établissement
                    interventions = interventions.OrderBy(i => i.Etablissement);
                    break;
                case "N": // Tri par nom du patient
                    interventions = interventions.OrderBy(i =>
                    {
                        var patient = BD.Citoyens.FirstOrDefault(c => c.NAS == i.NAS.ToString());
                        return patient?.Nom ?? "";
                    });
                    break;
                case "A": // Tri par NAS
                    interventions = interventions.OrderBy(i => i.NAS);
                    break;
                case "S": // Sans tri
                default:
                    break;
            }

            foreach (var interv in interventions)
            {
                var patient = BD.Citoyens.FirstOrDefault(c => c.NAS == interv.NAS.ToString());
                string nomPatient = patient?.Nom ?? "Inconnu";

                Console.WriteLine("{0,-30} {1,9} {2,12} {3,-20}",
                    nomPatient,
                    interv.NAS,
                    interv.Date,
                    interv.Etablissement);
            }

            U.P();
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private string SaisirOptionTri()
        {
            U.Entete();
            U.WL("\t\t\tPatients de Louise Décarie triés par\n");
            U.WL("\t\t\tn - naissance");
            U.WL("\t\t\tN - Naissance");
            U.WL("\t\t\ta - nas");
            U.WL("\t\t\tA - NAS");
            U.WL("\t\t\to - nom");
            U.WL("\t\t\tO - Nom");
            U.WL("\t\t\ts - sans tri");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.KeyChar)
            {
                case 'n':
                case 'N':
                    return "N";
                case 'a':
                case 'A':
                    return "A";
                case 'o':
                case 'O':
                    return "O";
                case 's':
                case 'S':
                    return "S";
                default:
                    return "S";
            }
        }
        //----------------------------------------------
        //
        //----------------------------------------------
        private string SaisirOptionTriIntervention()
        {
            U.Entete();
            U.WL("\t\t\tInterventions de Louise Décarie triées par\n");
            U.WL("\t\t\td - date");
            U.WL("\t\t\tD - Date");
            U.WL("\t\t\te - établissement");
            U.WL("\t\t\tE - Établissement");
            U.WL("\t\t\tn - nom du patient");
            U.WL("\t\t\tN - Nom du patient");
            U.WL("\t\t\ta - nas du patient");
            U.WL("\t\t\tA - NAS du patient");
            U.WL("\t\t\ts - sans tri");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.KeyChar)
            {
                case 'd':
                case 'D':
                    return "D";
                case 'e':
                case 'E':
                    return "E";
                case 'n':
                case 'N':
                    return "N";
                case 'a':
                case 'A':
                    return "A";
                case 's':
                case 'S':
                    return "S";
                default:
                    return "S";
            }
        }
    }
}
