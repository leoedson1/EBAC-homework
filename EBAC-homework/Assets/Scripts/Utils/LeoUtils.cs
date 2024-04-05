using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEditor;

public static class LeoUtils
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Ebac/Test")]
    public static void Test()
    {
        Debug.Log("Test");
    }

    [UnityEditor.MenuItem("Ebac/Test2")]
    public static void Test2()
    {
        Debug.Log("Test2");
    }

 [MenuItem("Ebac/Spawn Cylinder %g")]
    public static void SpawnCylinder()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = Vector3.zero;
        cylinder.name = "Cylinder";
    }
#endif

#region SCALE
    public static void Scale(this Transform t, float size = 1.2f)
    {
        t.localScale = Vector3.one * size;
    }

    public static void Scale(this GameObject t, float size = 1.2f)
    {
        t.transform.localScale = Vector3.one * size;
    }
#endregion

#region RANDOM
    public static T GetRandom<T>(this T[] array)
    {
        if (array.Length == 0)
            return default(T);

        return array[Random.Range(0, array.Length)];
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    
    public static T GetRandomUnique<T>(this List<T> list, T unique)
    {
        if (list.Count == 1)
            return unique;

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
#endregion
}
