using GestionAbscence.ViewModels;
using Microsoft.EntityFrameworkCore; // Ajout de la directive using pour ToListAsync
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionAbscence.Models;

namespace GestionAbscence.Services
{
    public class EnseignantService : IEnseignantService
    {
        private readonly ApplicationDbContext _context;

        public EnseignantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EnseignantViewModel>> GetAllEnseignants()
        {
            return await _context.Enseignants
                .Select(e => new EnseignantViewModel
                {
                    CodeEnseignant = e.CodeEnseignant,
                    Nom = e.Nom,
                    Prenom = e.Prenom,
                    DateRecrutement = e.DateRecrutement,
                    Mail = e.Mail,
                    Tel = e.Tel,
                    CodeDepartement = e.CodeDepartement,
                    CodeGrade = e.CodeGrade
                })
                .ToListAsync();
        }

        public async Task<EnseignantViewModel> GetEnseignantById(string id)
        {
            var enseignant = await _context.Enseignants.FindAsync(id);
            if (enseignant == null)
            {
                return null;
            }

            return new EnseignantViewModel
            {
                CodeEnseignant = enseignant.CodeEnseignant,
                Nom = enseignant.Nom,
                Prenom = enseignant.Prenom,
                DateRecrutement = enseignant.DateRecrutement,
                Mail = enseignant.Mail,
                Tel = enseignant.Tel,
                CodeDepartement = enseignant.CodeDepartement,
                CodeGrade = enseignant.CodeGrade
            };
        }

        public async Task<bool> CreateEnseignant(EnseignantViewModel model)
        {
            var enseignant = new Enseignant
            {
                CodeEnseignant = model.CodeEnseignant,
                Nom = model.Nom,
                Prenom = model.Prenom,
                DateRecrutement = model.DateRecrutement,
                Mail = model.Mail,
                Tel = model.Tel,
                CodeDepartement = model.CodeDepartement,
                CodeGrade = model.CodeGrade
            };

            _context.Enseignants.Add(enseignant);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEnseignant(EnseignantViewModel model)
        {
            var enseignant = await _context.Enseignants.FindAsync(model.CodeEnseignant);
            if (enseignant == null)
            {
                return false;
            }

            enseignant.Nom = model.Nom;
            enseignant.Prenom = model.Prenom;
            enseignant.DateRecrutement = model.DateRecrutement;
            enseignant.Mail = model.Mail;
            enseignant.Tel = model.Tel;
            enseignant.CodeDepartement = model.CodeDepartement;
            enseignant.CodeGrade = model.CodeGrade;

            _context.Enseignants.Update(enseignant);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEnseignant(string id)
        {
            var enseignant = await _context.Enseignants.FindAsync(id);
            if (enseignant == null)
            {
                return false;
            }

            _context.Enseignants.Remove(enseignant);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
