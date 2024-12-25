namespace GestionAbscence.Models
{
    public class Groupe
    {
        public string CodeGroupe { get; set; }
        public string NomGroupe { get; set; }

        // Relations
        public virtual ICollection<Classe> Classes { get; set; }
    }
}
