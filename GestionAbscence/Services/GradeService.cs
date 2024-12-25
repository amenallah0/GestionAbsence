using GestionAbscence.Models;
using GestionAbscence.ViewModels;
using Microsoft.EntityFrameworkCore; // Ajout de la directive using pour ToListAsync et FirstOrDefaultAsync
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAbscence.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GradeViewModel>> GetAllGrades()
        {
            return await _context.Grades
                .Select(g => new GradeViewModel
                {
                    CodeGrade = g.CodeGrade,
                    NomGrade = g.NomGrade,
                    Description = g.Description,
                    NombreEnseignants = g.Enseignants.Count
                })
                .ToListAsync();
        }

        public async Task<GradeViewModel> GetGradeById(string id)
        {
            return await _context.Grades
                .Where(g => g.CodeGrade == id)
                .Select(g => new GradeViewModel
                {
                    CodeGrade = g.CodeGrade,
                    NomGrade = g.NomGrade,
                    Description = g.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateGrade(GradeViewModel model)
        {
            try
            {
                var grade = new Grade
                {
                    CodeGrade = Guid.NewGuid().ToString(),
                    NomGrade = model.NomGrade,
                    Description = model.Description
                };

                _context.Grades.Add(grade);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateGrade(GradeViewModel model)
        {
            try
            {
                var grade = await _context.Grades.FindAsync(model.CodeGrade);
                if (grade == null) return false;

                grade.NomGrade = model.NomGrade;
                grade.Description = model.Description;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteGrade(string id)
        {
            try
            {
                var grade = await _context.Grades.FindAsync(id);
                if (grade == null) return false;

                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
