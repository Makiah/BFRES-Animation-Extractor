using System.Collections;
using System.Collections.Generic;
using Syroot.Maths;
using UnityEngine;

public class Syroot2Unity : MonoBehaviour 
{
    public static UnityEngine.Vector3 ToUnityVector(Vector3F vector3F)
    {
        return new UnityEngine.Vector3(vector3F.X, vector3F.Y, vector3F.Z);
    }

    public static UnityEngine.Vector3 ToUnityVector(Vector4F vector4F)
    {
        if (!vector4F.W.NearlyEquals(1))
            Debug.Log("Losing accuracy on " + vector4F + " conversion, as w is not one!");
        
        return new UnityEngine.Vector3(vector4F.X, vector4F.Y, vector4F.Z);
    }

    public static Quaternion ToUnityQuaternion(Vector4F rotation)
    {
        return Quaternion.Inverse(new Quaternion(rotation.X, rotation.Y, rotation.Z, rotation.W));
    }
}
