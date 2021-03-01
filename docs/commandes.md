# Commandes

## Ajouter une migration

### Actions

- Création d'une nouvelle classe dans le projet .Models
- Ajouter un `DbSet<$$NOM_MODEL$$>` dans `BattleCalculator.Data\Contexts\ApplicationDbContext.cs`
- _Optionnel_
  - Ajouter une méthode `ConfigureModelBuilderFor$$NOM_MODEL$$` avec les régles de validation coté SQL
- Lancer la commande

### Commandes

```sh
Add-Migration $$NOM_MIGRATION$$ -Project BattleCalculator.Data -Context ApplicationDbContext -OutputDir Migrations\MySql
```

## Mettre à jour la base de données

```sh
Update-Database -Project BattleCalculator.Data -Context ApplicationDbContext
```
