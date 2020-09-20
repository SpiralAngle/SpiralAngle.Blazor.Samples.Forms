# BlazorForms Sample

This is a sample project whose purpose is to show off both Blazor EditForms in a real working example as well as provide a use case to develop the [SpiralAngle.Blazor.Extensions](https://github.com/SpiralAngle/SpiralAngle.Blazor.Extensions) library.

## TODO

See <https://github.com/SpiralAngle/SpiralAngle.Blazor.Samples.Forms/projects/1>

## Setting up for development

There are three things you need to be aware of to be able to develop or deploy this code.

1. Working with git Submodules
2. Working with the Database
3. Setting Up Authentication

Especially for setting up Authentication, follow the instructions **EXACTLY**. It assumes Azure AD.

### Working with git Submodules

**Note**: This uses a submodule for the SpiralAngle.Blazor.Extensions folder. See <https://git-scm.com/book/en/v2/Git-Tools-Submodules> for more information.

When you clone this use:

``` shell
git clone --recurse-submodules [url to BlazorFormsSample git repo].
```

To get updates from the master branch for all submodules:

``` shell
git submodule update --remote
```

### Working with the Database

From the BlazorFormSample/Data/Migrations folder:

#### Updating Local First Time

```shell
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef database update -c CreatureMigrationDbContext
```

#### Updating Local Subsequently

```shell
dotnet ef database update -c CreatureMigrationDbContext
```

#### Adding Migrations

```shell
dotnet ef migrations add [descriptive name] -c CreatureMigrationDbContext
dotnet ef migrations script --idempotent --output idempotent.sql -c CreatureMigrationDbContext
```

The first line adds a new migration, the second regenerates a sql file that can be used to stand the whole DB up w/out requiring dotnet ef installed.

##### Notes on the structure for DB Projects

This project separated out the Entity Framework Migrations from the server project that has classes for `DbContext`. This was to show path to allow the updating of database schema to be wholly separate and unknown to the application code. This is just to separate concerns and not require the actual app code to be changed if the deployment mechanism changes.

In terms of reliability, running the migration at startup means its running at an unpredictable time. If you're deploying to a load balanced cluster, which instance is running it? You need to manage that too.

Because this is a sample project intended to show a practical project, it should use a practical db migration, so it's not using the self-update. Unfortunately, the documentation for how you do this is terrible and it turns out the tools just fight it all the way.

Still need to work on the config settings for the connection string though , this still in development! Right now, the migrations project is not very reliable with environment variables, the appsettings is more reliable.

To set the database, either provide an appsettings.json or set an environment variable.

**Note** This still seems to require a context that derives from the main one to be placed in the Migrations project, so you'll see `CreatureMigrationDbContext` in the Migrations project. That's just a shim for the `CreatureDbContext` in the `BlazorFormSample.Server` project.

### Setting Up Authentication

The only current provider is Azure Active Directory. Eventually we hope to support any OpenId Connect provider. Use these steps to set up a server registration and client registration. Still doing some testing, but if you're using B2C flavors, it looks like you set that up only for the client app registration. Leave server in Single Organization Mode. If you set the service app registration do this, you'll get an invalid issuer.

<https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-azure-active-directory?view=aspnetcore-3.1#server-app-configuration>

Follow the instructions in "Register apps in AAD and create solution" section **very** carefully! You don't need to run the command to actually create the new solution. If you run into problems, use that to debug your configuration. This is simple once you know it, but if you're not very familiar with OpenID Connect and JWTs, it's really easy to get a setting wrong and the errors that arise will be using terminology you're not familiar with. That will make it seem opaque and intimidating. Read the above link to the how-to like it is a university math text book: slowly and make sure you comprehend it fully.

If you are setting this up on your own, when you get to the part about updating index.html, note the Microsoft's docs had some bad formatting, make sure the javascript you're supposed to put in `index.html` doesn't have line breaks. e.g.:

```html
<script src="_content/Microsoft.Authentication.WebAssembly.Msal/AuthenticationService.js"></script>
```

I hope to see someone (maybe me) build out deployment automation that will streamline the deployment and authentication/authorization, but that will be provider specific and the expectation is many potential installations will use their own existing auth providers.

> Use .Net's User Secrets to manage local settings, especially for authentication configuration. Any setting that should vary with the environment should code to your server User Secrets and not be committed in any settings file. Exceptions can be made for .Development files where the setting is an alias or such as the configuration for `StorageConnectionString` that uses the local emulator.
>
> For the server project:
>
>```json
>{
>  "AzureAd": {
>    "Instance": "https://login.microsoftonline.com/",
>    "Domain": "{your domain e.g. contoso.onmicrosoft.com}",
>    "TenantId": "{the tenant id, also known as directory id.}",
>    "ClientId": "{the server app's client id also known application id.}"
>  }
>}
>```
>
>For the client, settings that can vary by environment should also be stored in the **server** User Secrets when developing. These will be served via `ClientConfigurationController`. They should be in a configuration section known as `ClientConfigNoSecretsAllowed`:
>
>```json
> "ClientConfigNoSecretsAllowed:{
>     "AzureAd": {
>         "Authority": "https://login.microsoftonline.com/{the tenant id, also known as directory id.}",
>         "ClientId": "{the client app's client id also known application id.}",
>         "ValidateAuthority": true
>     },
>     "scopeuri": "{the server app's client id also known application id.}/{The server app's API scope name. This is >API.Access in the tutorial}"
> }
>
>```
> So in the end the **server** user secrets should look something like this:
>
> ```json
>{
>  "AzureAd": {
>    "Instance": "https://login.microsoftonline.com/",
>    "Domain": "[somename].onmicrosoft.com",
>    "TenantId": "10000000-0000-0000-0000-000000000000",
>    "ClientId": "20000000-0000-0000-0000-000000000000"
>  },
>  "ClientConfigNoSecretsAllowed": {
>    "AzureAd": {
>      "Authority": "https://login.microsoftonline.com/10000000-0000-0000-0000-000000000000",
>      "ClientId": "30000000-0000-0000-0000-000000000000",
>      "ValidateAuthority": true
>    },
>    "ScopeUri": "20000000-0000-0000-0000-000000000000/API.Access"
>  }
>}
>```

If you have problems, make sure you followed the instructions EXACTLY.

