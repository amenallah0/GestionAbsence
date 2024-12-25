using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionAbscence.Models;
using GestionAbscence.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

public class AbsenceController : Controller
{
    private readonly ApplicationDbContext _context;

    public AbsenceController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult MarquerAbsence()
    {
        // Récupérer la liste des matières pour le dropdown
        ViewBag.Matieres = new SelectList(_context.Matieres, "CodeMatiere", "NomMatiere");
        // Récupérer la liste des classes pour le dropdown
        ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse");

        var viewModel = new MarquerAbsenceViewModel
        {
            DateSeance = DateTime.Today,
            Etudiants = new List<EtudiantAbsenceViewModel>()
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult MarquerAbsence(MarquerAbsenceViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Créer une nouvelle fiche d'absence
            var ficheAbsence = new FicheAbsence
            {
                CodeFicheAbsence = Guid.NewGuid().ToString(), // ou votre logique de génération de code
                DateJour = model.DateSeance,
                CodeMatiere = model.CodeMatiere,
                CodeClasse = model.CodeClasse
            };

            _context.FicheAbsences.Add(ficheAbsence);

            // Ajouter les lignes d'absence pour chaque étudiant absent
            foreach (var etudiant in model.Etudiants.Where(e => e.EstAbsent))
            {
                var ligneAbsence = new LigneFicheAbsence
                {
                    CodeFicheAbsence = ficheAbsence.CodeFicheAbsence,
                    CodeEtudiant = etudiant.CodeEtudiant
                };
                _context.LigneFicheAbsences.Add(ligneAbsence);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // En cas d'erreur, réafficher le formulaire avec les données
        ViewBag.Matieres = new SelectList(_context.Matieres, "CodeMatiere", "NomMatiere");
        ViewBag.Classes = new SelectList(_context.Classes, "CodeClasse", "NomClasse");
        return View(model);
    }

    [HttpGet]
    public IActionResult GetEtudiantsByClasse(string classeId)
    {
        var etudiants = _context.Etudiants
            .Where(e => e.CodeClasse == classeId)
            .Select(e => new EtudiantAbsenceViewModel
            {
                CodeEtudiant = e.CodeEtudiant,
                NomComplet = e.Nom + " " + e.Prenom,
                EstAbsent = false
            })
            .ToList();

        return Json(etudiants);
    }
}
