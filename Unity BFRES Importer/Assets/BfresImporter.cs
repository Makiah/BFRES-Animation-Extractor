using System.Collections;
using System.Collections.Generic;
using Syroot.NintenTools.Bfres;
using Syroot.NintenTools.Yaz0;
using UnityEditor;
using UnityEngine;

public class BfresImporter : MonoBehaviour
{
    [SerializeField] private string bfresPath;
    
    public void ImportBfres()
    {
        Debug.Log("Would not load BFRES!");
    }
}

[CustomEditor(typeof(BfresImporter))]
public class CustomEditorForMaterialConverter : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        var materialConverter = (BfresImporter)target;
        if(GUILayout.Button("Import BFRES"))
        {
            materialConverter.ImportBfres();
        }
    }
}