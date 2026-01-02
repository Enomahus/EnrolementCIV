# EnrolementCIV

Inscription sur la liste électorale

## Urls

- front: <http://localhost:4016/>
- Swagger: <http://localhost:40145/swagger/index.html>
- bdd: <localhost:43100> ; User ID: sa ; Password: @ce!gouv225 ; Database : EnrolmentDb, ReferentielDb
- Jeager: <http://localhost:43000/search>

### Local

Le projet est composé d'un frontend **Angular 20** et d'un backend en **ASP.NET Core** en **.NET 9**.
L'utilisation de **Docker** est obligatoire afin de pouvoir héberger la base de données **SQL Server 2022**.

#### Backend

Pour démarrer le backend depuis une console: `docker compose -f docker/dev/docker-compose.yml --project-directory ./docker/dev up --build`
depuis la racine du projet.

Depuis **Visual Studio Code**, la tâche `Start services (Debug)` permet de lancer le projet.

Si vous utilisez **Visual Studio 2022**, ouvrez la solution **EnrolomentCIV.sln** (src/back/EcoModulation.sln)
et mettez le projet **docker-compose** en projet de démarrage, puis lancer-le.
La compilation du projet web sera faite dans Visual Studio puis les fichiers seront remplacés dans le container directement.

Une fois le projet lancé, vérifiez bien que tous les containeurs sont bien démarrés.

Pour plus de détails, voir le Readme: [Lien](/docs/back.md)

#### Frontend

Nous conseillons l'utilisation de **Visual Studio Code** pour lancer le frontend.
Vous aurez accès aux tâches suivantes:

- ng serve (`npm start`): Lance le frontend
- ng lint (`npm lint`): Execute ESLint sur le frontend
- ng lint-fix (`npm lint-fix`): Execute ESLint sur le frontend en tentant de corriger les erreurs de façon sécurisée
- Generate nswag api client (`npm generate-api-client`): Génère les services pour interagir avec le backend

Pour plus de détails, voir le Readme: [Lien](/docs/front.md)

#### Base de données

Pour accéder à la base de données, nous conseillons l'utilisation des outils **SQL Server Management Studio** ou **Azure Data Studio**.

- **Host**: `localhost:43100`
- **User**: `sa`
- **Password**: `@ce!gouv225`
- **Database**: `EnrolmentDb`

#### Urls

- **Front** : <https://localhost:4016/>
- **Swagger** : <https://localhost:40145/swagger/index.html>
- **ReDoc** : <http://localhost:40200/docs/index.html>
- **Jeager** : <http://localhost:42201/search>

## Release notes

Les release notes sont générées automatiquement à chaque pull request.
