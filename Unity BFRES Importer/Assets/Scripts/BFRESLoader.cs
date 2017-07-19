using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syroot.NintenTools.Bfres;
using Syroot.NintenTools.Yaz0;
using UnityEditor;
using UnityEngine;

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
		foreach (var model in loadedFile.Models.Values)
		{
			Debug.Log("Now loading " + model.Name);
			var modelParent = new GameObject(model.Name).transform;
			
			var boneReferences = new List<Transform>();
			
			foreach (var bone in model.Skeleton.Bones.Values)
			{
				//Set bone properties.  
				var instantiatedBone = new GameObject(bone.Name).transform;
				instantiatedBone.SetParent(modelParent);
				
				//Concisely sets up bone hierarchy. 
				if (boneReferences.Count > 0)
					instantiatedBone.SetParent(boneReferences[bone.ParentIndex]);
				boneReferences.Add(instantiatedBone);
				
				instantiatedBone.localPosition = Syroot2Unity.toUnityVector(bone.Position);
				instantiatedBone.localRotation = Syroot2Unity.toUnityQuaternion(bone.Rotation);
				instantiatedBone.localScale = Syroot2Unity.toUnityVector(bone.Scale);
			}
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
