//--------------------------------------------
// Maladie.cs
// Achraf Mechmachi
// 2156548
// Projet Vision Santé
// 27 Avril 2025
//--------------------------------------------
namespace Tp3_VisionSante
{
    class Maladie : Probleme
    {
        public string Pathologie { get; set; }
        public int Stade { get; set; }

        public Maladie(int nas, string pathologie, string dateDebut, string? dateFin, string description, int stade)
            : base(nas, dateDebut, dateFin, description)
        {
            Pathologie = pathologie;
            Stade = stade;
        }
    }
}
