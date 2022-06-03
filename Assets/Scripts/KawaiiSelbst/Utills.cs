using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utills
{
    public static Vector3[] ListVector2_Vector3Array(List<Vector2> vector2)
    {
        List<Vector3> list = new List<Vector3>();
        foreach (Vector2 vector in vector2)
        {
            list.Add(vector);
        }
        return list.ToArray();
    }
}
