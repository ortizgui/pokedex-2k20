# Pokedex 2k20 Proxy 

Web Api that acts as a proxy consuming an external API. 

External API: [PokéAPI](https://pokeapi.co/).

## Pre-Requisite

* [.NET Core 3.1](https://dotnet.microsoft.com/download)
* [Postman](https://www.postman.com/)

## Endpoints
* */api/Pokemon/{pokemonNumber}

## Libraries used

* MediatR
* MediatR.Extensions.Microsoft.DependencyInjection
* Mapster
* Restsharp
* Newtonsoft.JSON

## Running the project locally

In the directory  .\Pokedex.API and run the command:

```
dotnet restore
dotnet run
```

```restore``` will restore nuget's dependencies and ```run``` will execute the project.

## Unit test

In the directory .\Pokedex.Test.unit and run the command:

```
dotnet test
```

## Troubleshooting

If you are using Postman and are unable to access the URL provided for testing, check that the
SSL Certificate Verification is disabled in Settings.