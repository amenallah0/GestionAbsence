using GestionAbscence.Models;
using System.ComponentModel.DataAnnotations;

namespace GestionAbscence.ViewModels
{
    public class MarquerAbsenceViewModel
    {
        [Required]
        public DateTime DateSeance { get; set; }
        
        [Required]
        public string CodeMatiere { get; set; }
        
        [Required]
        public string CodeClasse { get; set; }
        
        public List<EtudiantAbsenceViewModel> Etudiants { get; set; }
    }

    public class EtudiantAbsenceViewModel
    {
        public int CodeEtudiant { get; set; }
        public string NomComplet { get; set; }
        public bool EstAbsent { get; set; }
    }
}
