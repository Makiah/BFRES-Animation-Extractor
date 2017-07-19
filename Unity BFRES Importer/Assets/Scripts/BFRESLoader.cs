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
		foreach (var model in loadedFile.Models.Values)
		{
			Debug.Log("Now loading " + model.Name);
			var modelParent = new GameObject(model.Name).transform;

			//Set up skeleton.  
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
				
				instantiatedBone.localPosition = Syroot2Unity.ToUnityVector(bone.Position);
				instantiatedBone.localRotation = Syroot2Unity.ToUnityQuaternion(bone.Rotation);
				instantiatedBone.localScale = Syroot2Unity.ToUnityVector(bone.Scale);
			}
			
			//Set up renderers.  
			foreach (var shape in model.Shapes.Values)
			{
				//Set up the shape.  
				var instantiatedShape = new GameObject(shape.Name);
				instantiatedShape.transform.SetParent(modelParent);
				instantiatedShape.transform.localPosition = Vector3.zero;
				instantiatedShape.transform.localRotation = Quaternion.identity;
				instantiatedShape.transform.localScale = Vector3.one;

				//Will display the item and deform based on skeleton.  
				instantiatedShape.AddComponent<SkinnedMeshRenderer>();
				var smr = instantiatedShape.GetComponent<SkinnedMeshRenderer>();
				
				//Create the helper class to work with VertexBuffer data.  
				var helper = new VertexBufferHelper(shape.VertexBuffer, loadedFile.ByteOrder);

				VertexBufferHelperAttrib positions = helper["_p0"];
				VertexBufferHelperAttrib normals = helper[1];
				
				//Converted to a method group and a LINQ expression (through Rider magic)
				List<Vector3> vertices = positions.Data.Select(Syroot2Unity.ToUnityVector).ToList();
				
				//Create new mesh (not currently functional)
				UnityEngine.Vector3[] newVertices = vertices.ToArray();
//				UnityEngine.Vector2[] newUV = null;
				int[] newTriangles = new int[newVertices.Length - newVertices.Length % 3];
				for (int i = 0; i < newTriangles.Length; i++)
				{
					newTriangles[i] = i % 3; //Random for now.  
				}
				
//				//This is apparently called an object initializer.  
				var mesh = new UnityEngine.Mesh
				{
					vertices = newVertices,
//					uv = newUV,
					triangles = newTriangles
				};

				//Set the SMR mesh to this mesh.  
				smr.sharedMesh = mesh;
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
