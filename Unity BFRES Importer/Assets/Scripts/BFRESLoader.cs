using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syroot.Maths;
using Syroot.NintenTools.Bfres;
using Syroot.NintenTools.Bfres.Helpers;
using Syroot.NintenTools.Yaz0;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BFRESLoader : MonoBehaviour
{
	[SerializeField] private string sbfresToLoad;
	
	private void Start()
	{
		ImportSBFRES();
	}
	
	// Use this for initialization
	public void ImportSBFRES ()
	{
		//Determine the paths of each of the components for the extraction.  
		string sbfresFolder = Path.Combine(Application.dataPath, "SBFRES");
		string sbfresFile = Path.Combine(sbfresFolder, sbfresToLoad);
		string bfresFile = Path.Combine(sbfresFolder, "Animal_Bass.bfres");

		//Make sure that the specified sbfresFile path exists.  
		if (!File.Exists(sbfresFile) || Path.GetExtension(sbfresFile).Equals("sbfres"))
		{
			Debug.Log(sbfresFile + " was invalid!");
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
		
		//Now begin to load model components.  
		foreach (var model in loadedFile.SkeletalAnims)
		{
		}
	}
}

[CustomEditor(typeof(BFRESLoader))]
public class CustomEditorForSBFRESImporter : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
        
		var importer = (BFRESLoader)target;
		if(GUILayout.Button("Import SBFRES"))
		{
			importer.ImportSBFRES();
		}
	}
}
