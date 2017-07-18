using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Syroot.NintenTools.Bfres;
using Syroot.NintenTools.Yaz0;
using UnityEngine;

public class Importer : MonoBehaviour
{	
	// Use this for initialization
	void Start ()
	{
		//Determine the paths of each of the components for the extraction.  
		string sbfresFolder = Path.Combine(Application.dataPath, "SBFRES");
		string sbfresFile = Path.Combine(sbfresFolder, "Animal_Bass.sbfres");
		string bfresFile = Path.Combine(sbfresFolder, "Animal_Bass.bfres");

		//Make sure that the specified sbfresFile path exists.  
		if (!File.Exists(sbfresFile))
		{
			Debug.Log("SBFRES file not found! :(");
			return;
		}

		//Extract the SBFRES file to a loadable BFRES.  
		if (!File.Exists(bfresFile))
		{
			Yaz0Compression.Decompress(sbfresFile, bfresFile);
			Debug.Log("Extracted "  + sbfresFile + " to " + bfresFile);
		}
		else
		{
			Debug.Log(bfresFile + " already exists, not extracting.");
		}
		
		//Load the model into a ResFile instance.  
		ResFile loadedFile;
		try
		{
			loadedFile = new ResFile(bfresFile);
		}
		catch (Exception e)
		{
			Debug.Log("Error while attempting to load " + bfresFile + " into a ResFile instance: " + e);
			return;
		}
		Debug.Log("Loaded " + loadedFile.Name + " successfully!");
	}
}
