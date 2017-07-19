using System.Collections;
using System.Collections.Generic;
using Syroot.Maths;
using UnityEngine;

public class Syroot2Unity : MonoBehaviour 
{
    public static UnityEngine.Vector3 toUnityVector(Vector3F vector3f)
    {
        return new UnityEngine.Vector3(vector3f.X, vector3f.Y, vector3f.Z);
    }

    public static Quaternion toUnityQuaternion(Vector4F rotation)
    {
        return new Quaternion(rotation.X, rotation.Y, rotation.Z, rotation.W);
    }
}
