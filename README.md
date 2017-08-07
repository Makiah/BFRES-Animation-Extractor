# BFRES Animation Extractor
A C#-based BFRES animation extractor for Unity, based on the NuGet packages provided by RayKoopa and RTB's BFRES importer.  

## About ##
This project builds off of [my SBFRES extractor](https://github.com/Makiah/BotW-SBFRES-to-FBX) and the Syroot libraries.  Although my previous project does extract the model successfully into a useable FBX format, it unfortunately does not export the animations for the given model along with it since the animations are not currently supported in [RTB's MAXScript](https://www.vg-resource.com/thread-29836.html).  Until these are supported, this library applies the animations within Unity itself.  RTB's MAXScript was chosen over RayKoopa's io_scene_bfres, since the MAXScript is being actively developed.  

## Credits/Dependencies ##
RayKoopa's NintenTools packages, notably [Yaz0](https://github.com/syroot/nintentools.yaz0) and [Bfres](https://github.com/syroot/nintentools.bfres).  It is important to note that these libraries do 99% of the heavy lifting here, I simply take the values and files provided by each library and employ them through Unity's API.  
AlexZZZZ's [Unity C# 5.0/6.0 integration plugin](https://bitbucket.org/alexzzzz/unity-c-5.0-and-6.0-integration/src), which makes RayKoopa's libraries useable in Unity 2017.1 (with the currently experimental .NET 4.6 runtime).  No idea how he did it but this wouldn't work without it!

## Current Project State ##
Nothing is currently functional, hopefully will change at some point :P  
