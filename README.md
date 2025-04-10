# simple-recip-application

Voici une application de gestion de recettes
Elle rassemble un ensemble de pratique de d�veloppement moderne tel que :
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
Elle utilise SQLite pour la base de donn�es et interagit au travers EFCore
Elle utilise OpenAI pour la g�n�ration de recette

## Installation
Pour pouvoir installer l'application, il faut d'abord :
- Cloner le d�p�t
- Installer les d�pendances
	- EFCore
	- Devtools sur le navigateur
	- .Net 9
- Configurer l'application
	- Ajout des informations de connexion � la base de donn�es
	- Ajout des informations de connexion � Google
	- Ajout des informations de connexion � OpenAI
	- Activer/D�sactiver les features flag
- Lancer l'application

## Fonctionnalit�s
- Gestion des recettes
	- Creer une recette
	- Modifier une recette
	- Supprimer une recette
- Gestion des ingr�dients
	- Creer un ingr�dient
	- Modifier un ingr�dient
	- Supprimer un ingr�dient
- Importation des recettes
- Gestion des notifications
- Planification des repas
- Gestion des produits m�nagers
	- Creer un produit
	- Modifier un produit
	- Supprimer un produit
- Gestion du placard
	- Ajout un produit
	- Modifier un produit
	- Supprimer un produit
- G�n�rer la liste de course

## Description de l'architecture fichier
Chaque fonctionnalit� est regroup�e dans un dossier
- Dossier `Features` : Contient les fonctionnalit�s de l'application
	- Dossier `NomFonctionnalit�` : Contient la fonctionnalit� `NomFonctionnalit�`
		- Dossier `ApplicationCore` : Interfaces m�tier
			- Dossier `Entities` : Interfaces des entit�s
			- Dossier `Factories` : Interfaces des factories
			- Dossier `Services` : Interfaces des services
			- Dossier `Repositories` : Interfaces pour les repositories
			- Dossier `EqualityComparers` : EqualityComparers pour les entit�s
		- Dossier `Extensions` : Extensions comme par exemple l'extension pour ajouter les DI
		- Dossier `Persistence` : Impl�mentations des interfaces m�tiers
			- Dossier `Entities` : Impl�mentations des entit�s
			- Dossier `Factories` : Impl�mentations des factories
			- Dossier `Services` : Impl�mentations des services
			- Dossier `Repositories` : Impl�mentations des repositories
		- Dossier `Store` : Contient le store de la fonctionnalit�
			- Dossier `Actions` : Actions li� au store
		- Dossier `UserInterfaces` : Contient les IHM de la fonctionnalit�
			- Dossier `Components` : Composants de la fonctionnalit�
			- Dossier `Pages` : Pages de la fonctionnalit�
- Dossier `Data` : Contient les classes utilis�es pour la base de donn�es
	- Dossier `Migrations` : Migrations de la base de donn�es
	- Dossier `ApplicationCore` : Interfaces m�tier globale
		- Dossier `Entities` : Entit�s globale
		- Dossier `Repositories` : Repositories globale
		- Dossier `ValidationAttributes` : ValidationAttributes globale
	- Dossier `Persistence` : Impl�mentation m�tier globale
		- Dossier `Entities` : Impl�mentation entit�s globale
		- Dossier `Repositories` : Impl�mentation repositories globale
- Dossier `Store` : Contient le store global
	- Dossier `Actions` : Actions globales
- Dossier `Settings` : Contient les classes settings utilis� pour le pattern Options
- Dossier `Enums` : Contient les enums de l'application
- Dossier `Extensions` : Contient les extensions de l'application
- Dossier `Components` : Contient les composants global de l'application
- Dossier `Dtos` : Contient les dtos de l'application
- Dossier `Services` : Contient les services de l'application

## Contribuer
Pour contribuer � l'application, il faut d'abord :
- Forker le d�p�t
- Cr�er une branche
- Faire une pull request
- Attendre la validation de la pull request