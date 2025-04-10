# simple-recip-application

Voici une application de gestion de recettes
Elle rassemble un ensemble de pratique de développement moderne tel que :
- Principe SOLID
- Clean Architecture
- Feature Folder
- Flux-Redux
- Feature Flag
- Localization
- Design Pattern
	- Repository Pattern
	- Factory Pattern
	- Options Pattern
	- Result Pattern
- ...

C'est une application Blazor server en .Net 9
Elle utilise SQLite pour la base de données et interagit au travers EFCore
Elle utilise OpenAI pour la génération de recette

## Installation
Pour pouvoir installer l'application, il faut d'abord :
- Cloner le dépôt
- Installer les dépendances
	- EFCore
	- Devtools sur le navigateur
	- .Net 9
- Configurer l'application
	- Ajout des informations de connexion à la base de données
	- Ajout des informations de connexion à Google
	- Ajout des informations de connexion à OpenAI
	- Activer/Désactiver les features flag
- Lancer l'application

## Fonctionnalités
- Gestion des recettes
	- Creer une recette
	- Modifier une recette
	- Supprimer une recette
- Gestion des ingrédients
	- Creer un ingrédient
	- Modifier un ingrédient
	- Supprimer un ingrédient
- Importation des recettes
- Gestion des notifications
- Planification des repas
- Gestion des produits ménagers
	- Creer un produit
	- Modifier un produit
	- Supprimer un produit
- Gestion du placard
	- Ajout un produit
	- Modifier un produit
	- Supprimer un produit
- Générer la liste de course

## Description de l'architecture fichier
Chaque fonctionnalité est regroupée dans un dossier
- Dossier `Features` : Contient les fonctionnalités de l'application
	- Dossier `NomFonctionnalité` : Contient la fonctionnalité `NomFonctionnalité`
		- Dossier `ApplicationCore` : Interfaces métier
			- Dossier `Entities` : Interfaces des entités
			- Dossier `Factories` : Interfaces des factories
			- Dossier `Services` : Interfaces des services
			- Dossier `Repositories` : Interfaces pour les repositories
			- Dossier `EqualityComparers` : EqualityComparers pour les entités
		- Dossier `Extensions` : Extensions comme par exemple l'extension pour ajouter les DI
		- Dossier `Persistence` : Implémentations des interfaces métiers
			- Dossier `Entities` : Implémentations des entités
			- Dossier `Factories` : Implémentations des factories
			- Dossier `Services` : Implémentations des services
			- Dossier `Repositories` : Implémentations des repositories
		- Dossier `Store` : Contient le store de la fonctionnalité
			- Dossier `Actions` : Actions lié au store
		- Dossier `UserInterfaces` : Contient les IHM de la fonctionnalité
			- Dossier `Components` : Composants de la fonctionnalité
			- Dossier `Pages` : Pages de la fonctionnalité
- Dossier `Data` : Contient les classes utilisées pour la base de données
	- Dossier `Migrations` : Migrations de la base de données
	- Dossier `ApplicationCore` : Interfaces métier globale
		- Dossier `Entities` : Entités globale
		- Dossier `Repositories` : Repositories globale
		- Dossier `ValidationAttributes` : ValidationAttributes globale
	- Dossier `Persistence` : Implémentation métier globale
		- Dossier `Entities` : Implémentation entités globale
		- Dossier `Repositories` : Implémentation repositories globale
- Dossier `Store` : Contient le store global
	- Dossier `Actions` : Actions globales
- Dossier `Settings` : Contient les classes settings utilisé pour le pattern Options
- Dossier `Enums` : Contient les enums de l'application
- Dossier `Extensions` : Contient les extensions de l'application
- Dossier `Components` : Contient les composants global de l'application
- Dossier `Dtos` : Contient les dtos de l'application
- Dossier `Services` : Contient les services de l'application

## Contribuer
Pour contribuer à l'application, il faut d'abord :
- Forker le dépôt
- Créer une branche
- Faire une pull request
- Attendre la validation de la pull request