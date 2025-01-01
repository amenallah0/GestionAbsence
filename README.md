# 📚 GAbsence - Gestion des Absences

Application web de gestion des absences développée avec ASP.NET Core MVC pour les établissements d'enseignement.

## ⚡ Démarrage Rapide

1. Cloner le projet
2. Restaurer les packages : `dotnet restore`
3. Mettre à jour la base de données : `dotnet ef database update`
4. Lancer l'application : `dotnet run`

## 🔑 Connexion

- **Email** : admin@gabsence.com
- **Mot de passe** : Admin@123456

## 🎯 Fonctionnalités

### 👥 Gestion des Utilisateurs
- Authentification personnalisée
- Gestion des rôles (Admin, Enseignant, Étudiant)
- Interface de connexion moderne

### 📊 Administration
- Départements
- Filières
- Grades
- Matières
- Classes

### 👨‍🎓 Gestion
- Enseignants
- Étudiants
- Absences
- Justifications

## 🛠️ Technologies

- ASP.NET Core MVC (.NET 7)
- Entity Framework Core
- SQL Server
- Bootstrap 5
- Font Awesome 5

## 📋 Prérequis

- .NET 7 SDK
- SQL Server
- Visual Studio 2022 / VS Code

## 🗃️ Base de données

Configuration dans `appsettings.json` :
json
{
"ConnectionStrings": {
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GAbsence;Trusted_Connection=True"
}
}

## 📱 Interface

- Design responsive
- Thème moderne
- Notifications en temps réel
- Navigation intuitive
- Compatible mobile

## 🔒 Sécurité

- Authentification sécurisée
- Protection CSRF
- Validation des données
- Gestion des rôles

## 📂 Structure

GAbsence/
├── Controllers/ # Logique métier
├── Models/ # Modèles de données
├── Views/ # Interface utilisateur
├── Data/ # Base de données
└── wwwroot/ # Fichiers statiques

## 🌐 Navigateurs Supportés

- Chrome
- Firefox
- Edge
- Safari



Développé pour une gestion efficace des absences
