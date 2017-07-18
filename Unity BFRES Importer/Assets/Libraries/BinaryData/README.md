# BinaryData
.NET library extending binary reading and writing functionality.

## Updated from 1.x.x to 2.0.0?
- Please read the [release notes](https://github.com/Syroot/BinaryData/releases/tag/2.0.0) for breaking changes.
- More information on the newest feature [on the wiki](https://github.com/Syroot/BinaryData/wiki/Object-Values).

## Introduction

When parsing or storing data in binary file formats, the functionality offered by the default .NET `BinaryReader` and `BinaryWriter` classes is often not sufficient. It lacks support for a different byte order than the one of the system and specific string or date formats (most prominently, 0-terminated strings instead of the default variable-length prefixed .NET strings).

Further, navigating in binary files is slightly tedious when it becomes required to skip to another chunk in the file and then navigate back. Also, aligning to specific block sizes might be a common task.

This NuGet package adds all this functionality by offering two new .NET 4.5 and .NET Standard 1.5 compatible classes, `BinaryDataReader` and  `BinaryDataWriter`, which extend the aforementioned .NET reader and writer, usable in a similar way so that they are easy to implement into existing projects - in fact, they can be used directly without requiring any changes to existing code.

The usage is described in detail [on the wiki](https://github.com/Syroot/BinaryData/wiki).

The library is available as a [NuGet package](https://www.nuget.org/packages/Syroot.IO.BinaryData).
