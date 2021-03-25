[![MIT License][license-shield]][license-url]
[![Kristen JESTIN][linkedin-kj-shield]][linkedin-kj-url]
[![Kilian MAGNIEZ][linkedin-mk-shield]][linkedin-mk-url]
[![Nathan LeVerge][linkedin-nl-shield]][linkedin-nl-url]

<h1 align="center">
	<b>BATTLE CALCULATOR</b>
	<br />
	<small align="center">API</small>
</h1>
	
<details open="open">
  <summary>Table des matières</summary>
<!-- TOC depthfrom:2 -->

- [À propos du projet](#%C3%A0-propos-du-projet)
  - [Routes](#routes)
  - [Bibliothèques](#biblioth%C3%A8ques)
  - [Dépendances](#d%C3%A9pendances)
- [Pour commencer](#pour-commencer)
  - [Prérequis](#pr%C3%A9requis)
  - [Installation](#installation)
  - [Démarrer](#d%C3%A9marrer)
- [License](#license)
- [Contact](#contact)

<!-- /TOC -->
</details>

## À propos du projet

Partie Back-end de l'application.

**Sources**

[Front-End](https://github.com/school-projects-cesi/battle-calculator-client-web)

### Routes

- **Auth**
  - Login : _Permet de se connecter avec email et mot de passe_
  - Register : _Permet de créer un nouveau compte utilisateur_
- **Users**
  - Me : _Permet de récupérer les infos de l'utilisateur connecté_
  - [Patch] : _Permet de modifier l'email et le mot de passe de l'utilisateur_
- **Games**
  - [Get] : _Permet de récupérer une game avec un id_
  - Best : _Permet de récupérer les meilleurs games des 10 meilleurs joueurs_
  - [Post] : _Permet de créer une nouvelle game_
  - End : _Permet de terminer une game, pour le calcul du total score_
  - **Score**
    - [Patch] : _Permet de modifier un score_
- **Levels**
  - [Get] : _Permet de récupérer toutes les difficultés_
  - [Get]{id} : _Permet de récupérer les infos d'une difficulté_

### Bibliothèques

- [EntityFramework Core](https://github.com/dotnet/efcore)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [AutoWrapper](https://github.com/proudmonkey/AutoWrapper)
- [bcrypt.net](https://github.com/BcryptNet/bcrypt.net)

### Dépendances

- [.NET Core 5](https://dotnet.microsoft.com/download/dotnet/5.0)

## Pour commencer

### Prérequis

- [.NET Core 5](https://dotnet.microsoft.com/download/dotnet/5.0)

### Installation

1. Cloner le projet
   ```sh
   git clone https://github.com/school-projects-cesi/battle-calculator-api.git
   ```
2. Cloner les clients
   - https://github.com/school-projects-cesi/battle-calculator-client-web#installation
   - https://github.com/school-projects-cesi/battle-calculator-presentation.git#installation

### Démarrer

Lancer le serveur

```sh
dotnet run
```

## License

Distribué sous la licence MIT. Voir `LICENSE` pour plus d'informations.

<!-- CONTACT -->

## Contact

Kristen JESTIN - [contact@kristenjestin.fr](mailto:contact@kristenjestin.fr)

Kilian MAGNIEZ - [kilian.magniez@gmail.com](mailto:kilian.magniez@gmail.com)

Lien du projet : [https://github.com/school-projects-cesi/battle-calculator-api](https://github.com/school-projects-cesi/battle-calculator-api)

<!-- MARKDOWN LINKS & IMAGES -->

[license-shield]: https://img.shields.io/github/license/school-projects-cesi/battle-calculator-api.svg?style=for-the-badge
[license-url]: https://github.com/school-projects-cesi/battle-calculator-api/blob/master/LICENSE
[linkedin-kj-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555&label=Kristen%20Jestin&color=0274b3
[linkedin-mk-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555&label=Kilian%20Magniez&color=0274b3
[linkedin-nl-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555&label=Nathan%20LeVerge&color=0274b3
[linkedin-kj-url]: https://linkedin.com/in/kristen-jestin
[linkedin-mk-url]: https://linkedin.com/in/kilian-magniez/
[linkedin-nl-url]: https://linkedin.com/in/nathan-le-verge-4b98b8165
