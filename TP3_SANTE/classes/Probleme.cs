//--------------------------------------------
// Probleme.cs
// Achraf mehcmachi
// 2156548
// Projet Vision Santé
// 27 Avril 2025
//--------------------------------------------
namespace Tp3_VisionSante
{
    abstract class Probleme
    {
        public int NAS { get; set; }
        public string DateDebut { get; set; }
        public string? DateFin { get; set; }
        public string Description { get; set; }

        public Probleme(int nas, string dateDebut, string? dateFin, string description)
        {
            NAS = nas;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Description = description;
        }
    }
}
