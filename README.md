![UnityWiiUIcon](logo.png)

# Unity BFRES Importer
A C#-based BFRES importer for Unity, based on the NuGet Packages provided by RayKoopa.  

## About ##
Traditionally, BFRES importing scripts are written for 3D modeling programs (such as 3DSMax and Blender) and employ their specific APIs to model the file in question.  These files are then exported and employed in game development software such as Unity.  Rather than taking this approach, given Unity's existent mesh generating tools, why not just create models directly in Unity?  

## Credits/Dependencies ##
RayKoopa's NintenTools packages, notably [Yaz0](https://github.com/syroot/nintentools.yaz0) and [Bfres](https://github.com/syroot/nintentools.bfres).  It is important to note that these libraries do 99% of the heavy lifting here, I simply take the values and files provided by each library and employ them through Unity's API.  
AlexZZZZ's [Unity C# 5.0/6.0 integration plugin](https://bitbucket.org/alexzzzz/unity-c-5.0-and-6.0-integration/src), which makes RayKoopa's libraries useable in Unity 2017.1 (with the currently experimental .NET 4.6 runtime).  No idea how he did it but this wouldn't work without it!

## Current Project State ##
Still figuring out how to access model/mesh data and then construct them.  
