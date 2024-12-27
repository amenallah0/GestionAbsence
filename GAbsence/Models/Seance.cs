using System;
using System.Collections.Generic;
using GAbsence.Models;

namespace GAbsence.Models
{
    public class Seance
    {
        public required string CodeSeance { get; set; }
        public DateTime HeureDebut { get; set; }
        public DateTime HeureFin { get; set; }
        public required string CodeMatiere { get; set; }
        
        public virtual Matiere? Matiere { get; set; }
        public virtual ICollection<FicheAbsenceSeance>? FicheAbsenceSeances { get; set; }
    }
} 