# ZooIds

## About
Generates Gfycat-like Ids

Based on https://github.com/bryanmylee/zoo-ids

## Nuget

[![Nuget](https://img.shields.io/nuget/v/ZooIds)](https://www.nuget.org/packages/ZooIds/)

## Usage

Basic usage

```csharp
var zoo = new ZooIdGenerator();

var id = zoo.GenerateId();

Console.WriteLine(id); // irritating-impolite-cattle
```

Optionally pass a config object

```csharp
uint numAdjectives = 2;
string delimiter = "_";
CaseStyle caseStyle = CaseStyle.UpperCase; // LowerCase, UpperCase, TitleCase, ToggleCase

var zooIdsConfig = new GeneratorConfig(numAdjectives, delimiter, caseStyle);

var zoo = new ZooIdGenerator(zooIdsConfig);

var id = zoo.GenerateId();

Console.WriteLine(id); // Easygoing Elk
```

It is possible to pass a seed value when generating ids

```csharp
var zooIdsConfig = new GeneratorConfig(2, "_", CaseStyle.UpperCase);
var seed = 42;
		
var zoo = new ZooIdGenerator(zooIdsConfig, seed);
		
Console.WriteLine(zoo.GenerateId()); // QUALIFIED_CONTENT_CATTLE
```

## Development

### Pack

`dotnet pack -c Release`

### Push

`dotnet nuget push bin/Release/ZooIds.0.1.0.nupkg -k _API_KEY_ -s https://api.nuget.org/v3/index.json`