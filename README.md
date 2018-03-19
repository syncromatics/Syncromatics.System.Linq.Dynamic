# System.Linq.Dynamic.NETCore

Prologue:

> December 12, 2011
> 
> King Wilder
> info@kingwilder.com
> 
> This is the Microsoft assembly for the .Net 4.0 Dynamic language functionality.
>
> I am in no way taking ownership of this assembly!  I'm am simply making it available since it seems very hard to find.  I've also created [a NuGet package](https://www.nuget.org/packages/System.Linq.Dynamic/) so it's easy to import into your Visual Studio projects.  You can install it via NuGet with this command in the Nuget Package Manager[.]

**Now targeting .NET Standard 2.0!**

## Quickstart

Add the `System.Linq.Dynamic.NETCore` package to your project:

```bash
dotnet add package System.Linq.Dynamic.NETCore
```

### Documentation and Samples

You can find sample code and documentation on usage from this link, just Accept the terms and you will download a Visual Studio file with C# code and HTML documentation.

http://msdn.microsoft.com/en-US/vstudio/bb894665.aspx
(Special thanks dradovic)

## Building

[![Travis](https://img.shields.io/travis/syncromatics/System.Linq.Dynamic.NETCore.svg)](https://travis-ci.org/syncromatics/System.Linq.Dynamic.NETCore)
[![NuGet](https://img.shields.io/nuget/v/System.Linq.Dynamic.NETCore.svg)](https://www.nuget.org/packages/System.Linq.Dynamic.NETCore/)

This is built using Cake and .NET Core.

To build and produce a NuGet package:

```bash
./build.sh -t Package
```

To run the tests:

```bash
./build.sh -t Test
```

## Code of Conduct

We are committed to fostering an open and welcoming environment. Please read our [code of conduct](CODE_OF_CONDUCT.md) before participating in or contributing to this project.

## Contributing

We welcome contributions and collaboration on this project. Please read our [contributor's guide](CONTRIBUTING.md) to understand how best to work with us.

## License and Authors

[![Syncromatics Engineering logo](https://en.gravatar.com/userimage/100017782/89bdc96d68ad4b23998e3cdabdeb6e13.png?size=16) Syncromatics Engineering](https://github.com/syncromatics)

[![license](https://img.shields.io/github/license/syncromatics/System.Linq.Dynamic.NETCore.svg)](https://github.com/syncromatics/System.Linq.Dynamic.NETCore/blob/master/LICENSE)
[![GitHub contributors](https://img.shields.io/github/contributors/syncromatics/System.Linq.Dynamic.NETCore.svg)](https://github.com/syncromatics/System.Linq.Dynamic.NETCore/graphs/contributors)

This software is made available by Syncromatics under the MS-PL license.