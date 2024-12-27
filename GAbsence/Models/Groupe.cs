namespace GAbsence.Models
{
    public class Groupe
    {
        public required string CodeGroupe { get; set; }
        public required string NomGroupe { get; set; }
        public required string CodeClasse { get; set; }
        public virtual Classe? Classe { get; set; }
    }
} 