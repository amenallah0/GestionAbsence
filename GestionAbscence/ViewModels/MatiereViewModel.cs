using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestionAbscence.ViewModels
{
    public class MatiereViewModel
    {
        public string CodeMatiere { get; set; } = string.Empty;
        public string NomMatiere { get; set; } = string.Empty;
        public int NbHeures { get; set; }
    }

    public class MatiereCreateEditViewModel
    {
        public string CodeMatiere { get; set; } = string.Empty;
        public string NomMatiere { get; set; } = string.Empty;
        public int NbHeures { get; set; }
        public string CodeDepartement { get; set; } = string.Empty;
    }

    public class MatiereStatisticsViewModel
    {
        public string CodeMatiere { get; set; } = string.Empty;
        public string NomMatiere { get; set; } = string.Empty;
        public int TotalAbsences { get; set; }
        public double TauxAbsenteisme { get; set; }
    }
}
