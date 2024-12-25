using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Ajout de la directive using pour List
using GestionAbscence.ViewModels; // Ajout de la directive using pour ClasseViewModel et EnseignantViewModel

namespace GestionAbscence.ViewModels
{
    public class DepartementViewModel
    {
        public string CodeDepartement { get; set; }
        public string NomDepartement { get; set; }
        public List<EnseignantViewModel> Enseignants { get; set; }
        public List<ClasseViewModel> Classes { get; set; }
    }
}
