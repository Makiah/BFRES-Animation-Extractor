using System.Collections;
using System.Collections.Generic;
using Syroot.NintenTools.Yaz0;
using UnityEngine;

public class Importer : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
	{
		Yaz0Compression.Decompress("/Users/makiah/Desktop/Animal_Bass.sbfres", "/Users/makiah/Desktop/Animal_Bass.bfres");
		Debug.Log("Should have decompressed!");
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
