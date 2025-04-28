//--------------------------------------------
// Blessure.cs
// Achraf Mechmachi
// 2156548
// Projet Vision Santé
// 27 Avril 2025
//--------------------------------------------
namespace Tp3_VisionSante
{
    class Blessure : Probleme
    {
        public string Type { get; set; }

        public Blessure(int nas, string type, string dateDebut, string? dateFin, string description)
            : base(nas, dateDebut, dateFin, description)
        {
            Type = type;
        }
    }
}
