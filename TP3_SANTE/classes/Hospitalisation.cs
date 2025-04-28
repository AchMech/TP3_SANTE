//--------------------------------------------
// Hospitalisation.cs
// Achraf Mechmachi
// 2156548
// Projet Vision Santé
// 27 Avril 2025
//--------------------------------------------
namespace Tp3_VisionSante
{
    class Hospitalisation : Ressource
    {
        public string? DateFin { get; set; }
        public int Chambre { get; set; }

        public Hospitalisation(int nas, string codePS, string etablissement, string date, string? dateFin, int chambre)
            : base(nas, codePS, etablissement, date)
        {
            DateFin = dateFin;
            Chambre = chambre;
        }
    }
}
