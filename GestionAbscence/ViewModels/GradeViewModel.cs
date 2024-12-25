using System.ComponentModel.DataAnnotations;

namespace GestionAbscence.ViewModels
{
    public class GradeViewModel
    {
        public string CodeGrade { get; set; } = string.Empty;
        public string NomGrade { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NombreEnseignants { get; set; }
    }
}
