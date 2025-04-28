//--------------------------------------------
// Ressource.cs
// Achraf mehcmachi
// 2156548
// Projet Vision Santé
// 27 Avril 2025
//--------------------------------------------
namespace Tp3_VisionSante
{
    abstract class Ressource
    {
        public int NAS { get; set; }
        public string CodePS { get; set; }
        public string Etablissement { get; set; }
        public string Date { get; set; }

        public Ressource(int nas, string codePS, string etablissement, string date)
        {
            NAS = nas;
            CodePS = codePS;
            Etablissement = etablissement;
            Date = date;
        }
    }
}
