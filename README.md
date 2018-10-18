# Builder Game

Gestion de ville, à la SimCity, Zeus, Banished

Très simpliste au début

Trello pour suivre les tâches et l'avancement: https://trello.com/b/s5jmRmqs/unity-builder



# Résumé
- Jour 1: 18/06: Définition du jeu et des tâches à réaliser
- Jour 2: 19/06: Recherche sur le système de sauvegarde/chargement et application sur une scène simple
- Jour 3: 20/06: Génération d'un terrain. Utilisation du diagramme de Voronoi. Affichage sur une texture de plan à l'aide d'un bouton.
3D et sauvegarde/chargement à ajouter
- Jour 4: 21/06: Changement des chances d'appaition des types de sol, passage a la 3D, creation de textures, creation d'un prefab d'arbre, sauvegarde et chargements
- Jour 5: 22/06: Déplacement d'un objet par rapport à la grille + suivi de la caméra lorsque le curseur approche des bords de l'écran
- Jour 6 et 7: 23-24/06: Mise à jour d'unity et galère avec Visual Studio + recherche de comment poser une route
- Jour 8: 25/06: Pose d'un bloc de route, puis pose d'une route entre deux points avec l'algorithme A*
- Jour 9: 26/06: Amélioration du pathfinding, texturisation des routes
- Jour 10: 27/06: Cliqué glissé pour créer une nouvelle route
- Jour 11: 28/06: Sauvegarde/Chargement des routes
- Jour 12: 02/07: Nouveau bloc, changement de bloc avec 1 et 2, ajout du bloc au clic, sauvegarde et chargement du bloc, placement de tous les blocs a la bonne hauteur
- Jour 13: 03/07: Correction du sens du premier item de route en fonction du sens des suivants + refactorisation du code + tests UI + maison avec blender
- Jour 14: 04/07: Rotation des blocs, sauvegarde/chargement des rotations, colorisation du bloc quand impossible de poser. + test UI
- Jour 15: 14/07: Ajout de l'or en ressource, affichage des ressource, coût des bâtiments, soustraction du cout a la pose, changement de couleur et empechement de pose si pas assez d'argent
- Jour 16: 28/09: Refactorisation du code, les arbres sont maintenant séparés du sol et pourront êtres enlevés
- Jour 17: 29/09: Amelioration de la ressource People, creation d'une classe Human et d'une HumanEntity qui se déplace simplement vers une maison.
- Jour 18: 17/10: Sauvegarde et chargement des humains et de leurs position, maison associees.

# Ressources
- Argent
- Habitants


# Batiments
- Maison
- Usine
- Centre des impots


# Interface
- Menu: Sauvegarde/ Charger/ Quitter	 - Chercher quel moment c'est le plus simple de l'implémenter?
- Selection des batiments				 - 3
- Création de la grille de placement	 - 2
- Placement des batiments / routes		 - 4
- Supprimer des batiments				 - 5
- Information des batiments et habitants - 6
- Liste des evenements					 - 9
- Bulle d'évènement						 - 9
- Etat des ressources					 - 3
- Contentement des habitants			 - 10
- Ecran economie						 - 8
- Cliquer pour améliorer un batiment?    - 11
- ecran commerce?						 - 14
- Ecran des armées?						 - 15


# Jeu
- Terrain à créer/générer   - 1
- Batiments, routes, personnages -> generation de ressources - 7
- Evenements aléatoires		- 9
- Ressources -> pose d'un batiment = consommation de ressources. Argent + habitants -> ne pas passer dans le rouge - 3
- Commerce -> batiment en plus		- 14
- Impots -> batiment + personnage qui se balade pour récupérer  (récupération d'argent pour la ville par tranche de temps) - 8  
- Contentement/Mécontentement -> change les taux de production - 10


# Annexes
- Animations: personnages qui vont d'un point à un autre - 13
- Créer les assets sous blender - 12
- Musique?


