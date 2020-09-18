# BlazorForms Sample

This is a sample project whose purpose is to show off both Blazor EditForms in a real working example as well as provide a use case to develop the [SpiralAngle.Blazor.Extensions](https://github.com/SpiralAngle/SpiralAngle.Blazor.Extensions) library.

## TODO

- Add Auth
- Add Configuration
- Add bunches of screens!
- Add print media so you can print a creature.

## Working with git Submodules

**Note**: This uses a submodule for the SpiralAngle.Blazor.Extensions folder. See <https://git-scm.com/book/en/v2/Git-Tools-Submodules> for more information.

When you clone this use:

``` shell
git clone --recurse-submodules [url to BlazorFormsSample git repo].
```

To get updates from the master branch for all submodules:

``` shell
git submodule update --remote
```

## Working with the Database

From the BlazorFormSample/Data/Migrations folder:

### Updating Local First Time

```shell
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef database update
```

### Updating Local Subsequently

```shell
dotnet ef database update
```

### Adding Migrations

```shell
dotnet ef migrations add [descriptive name]
dotnet ef migrations script --idempotent --output idempotent.sql
```

The first line adds a new migration, the second regenerates a sql file that can be used to stand the whole DB up w/out requiring dotnet ef installed.

#### Notes on the structure for DB Projects

This project separated out the Entity Framework Migrations from the server project that has classes for `DbContext`. This was to show path to allow the updating of database schema to be wholly separate and unknown to the application code. This is just to separate concerns and not require the actual app code to be changed if the deployment mechanism changes.

While EF does document the method you can use to run the migration to update the DB schema, etc, from within an app, **YOU SHOULD ALMOST NEVER USE IT**. Why? Security and reliability. Unless the app is one which it's ok, and even expected, that the user can modify the schema, you simply don't want the app to have that permission. This is particularly true for web apps and APIs. If you run the migration when a web app/api starts up then then you have to give that app the rights to modify the database. Bad guys have more time to think about being bad guys than we have time to think about being good guys, so if your app runs with that additional permission and the bad guys manage to find a tiny seam they can exploit, now they have permissions to do whatever they want to your db.

In terms of reliability, running the migration at startup means its running at an unpredictable time. If you're deploying to a load balanced cluster, which instance is running it? You need to manage that too.

Because this is a sample project intended to show a practical project, it should use a practical db migration, so it's not using the self-update. Unfortunately, the documentation for how you do this is terrible and it turns out the tools just fight it all the way.

So, to get around a gnarly amount of command line syntax, the workaround was to put `CreatureMigrationDbContext` inside the `BlazorFormSample.Data.Migrations` project. This simply derives from `CreatureDbContext` in the `BlazorFormSample.Server` project so that the `dotnet ef` command line tools can find the context to generate the migrations.

Still need to work on the config settings for the connection string though , this still in development!
