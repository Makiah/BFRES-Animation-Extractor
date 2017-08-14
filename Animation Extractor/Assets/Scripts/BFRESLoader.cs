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
using UnityEditor.Animations;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BFRESLoader : MonoBehaviour
{
	[SerializeField] private GameObject objectForAttach;
	[SerializeField] private string sbfresModel, sbfresAnimation;
	private RuntimeAnimatorController modelAnimatorController;
	
	private void Start()
	{	
		ImportSBFRES();
	}

	private ResFile GetResFileFromSBFRES(string sbfres)
	{
		//Determine the paths of each of the components for the extraction.  
		string sbfresFolder = Path.Combine(Application.dataPath, "SBFRES");
		// Get model.  
		string sbfresFile = Path.Combine(sbfresFolder, sbfres);

		//Make sure that the specified sbfresFile path exists.  
		if (!File.Exists(sbfresFile) || Path.GetExtension(sbfresFile).Equals("sbfres"))
		{
			Debug.Log(sbfresFile + " was invalid!");
			return null;
		}
		
		string bfresFile = Path.Combine(sbfresFolder, sbfres.Substring(0, sbfres.IndexOf(".", StringComparison.Ordinal)) + ".bfres");
		
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
			return null;
		}
		Debug.Log("Loaded " + loadedFile.Name + " (version " + loadedFile.Version + ") successfully!");

		return loadedFile;
	}
	
	// Use this for initialization
	public void ImportSBFRES ()
	{
		// Load both ResFiles.  
		ResFile
			modelFile = GetResFileFromSBFRES(sbfresModel),
			animationFile = GetResFileFromSBFRES(sbfresAnimation);
		
		//Now begin to load animations.  
		foreach (var anim in animationFile.SkeletalAnims)
		{
			// anim.key = anim.value.name
			Debug.Log("Found skeletal animation " + anim.Key + " which is " + anim.Value.FrameCount + " long");
			
			// Create new animation clip.  
//			AnimationClip clip = new AnimationClip(); // Will be saved as something.anim.  
//			clip.name = anim.Key;

			// Each bone anim applies to a single skeleton bone.  It has the curves already applied.  
			foreach (var boneAnim in anim.Value.BoneAnims) 
			{
				Debug.Log("Looking at individual bone " + boneAnim.Name);
				
				/*
				Since there are 6 max curves for each bone, I'm assuming that each has to indicate some 
				translation or rotation (not sure about scaling)
				
				Also I think that the 4 sub arrays for the 2d array is just different ways of formatting
				the data (see AnimCurve.cs, line 158).  
				*/
				
				foreach (AnimCurve curve in boneAnim.Curves)
				{
					AnimationCurve newCurve = new AnimationCurve();
					
					curve.KeyType = AnimCurveKeyType.Single;
					
					int currentIndex = 0;
					foreach (float frameTime in curve.Frames)
					{
						newCurve.AddKey(new Keyframe(frameTime, curve.Keys[currentIndex, 1]));
						Debug.Log("Added new Keyframe(" + frameTime + ", " + curve.Keys[currentIndex, 1] + ")");
						currentIndex++;
					}

					// Don't do more for now.  
					return;

					// Construct new curve.  
//					clip.SetCurve(boneAnim.Name, typeof(Transform), "local.position.x", newCurve);
				}
			}
			
			// Save animation once complete.  
//			AssetDatabase.CreateAsset(clip, "Assets/MyAnim.anim");
//			AssetDatabase.SaveAssets();
		}

//		foreach (var anim in animationFile.BoneVisibilityAnims)
//		{
//			Debug.Log("Found bone visibility animation " + anim.Key);
//		}
//
//		foreach (var anim in animationFile.ColorAnims)
//		{
//			Debug.Log("Found color animation " + anim.Key);
//		}
//
//		foreach (var anim in animationFile.MatVisibilityAnims)
//		{
//			Debug.Log("Found material visibility animation " + anim.Key);
//		}
//
//		foreach (var anim in animationFile.SceneAnims)
//		{
//			Debug.Log("Found scene animation " + anim.Key);
//		}
//
//		foreach (var anim in animationFile.ShapeAnims)
//		{
//			Debug.Log("Found shape animation " + anim.Key);
//		}
//
//		foreach (var anim in animationFile.TexPatternAnims)
//		{
//			Debug.Log("Found tex pattern animation " + anim.Key);
//		}
//
//		foreach (var anim in animationFile.TexSrtAnims)
//		{
//			Debug.Log("Found tex srt animation " + anim.Key);
//		}
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
