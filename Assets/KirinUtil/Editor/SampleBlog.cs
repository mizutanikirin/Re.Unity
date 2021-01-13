using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SampleBlog : Editor
{

    [MenuItem("KirinUtil/CheckPrefabPath", false, 1)]
    private static void CheckPrefabPath()
    {
        string path = GetPrefabPath(Selection.activeGameObject);
        Debug.Log("PrefabPath: " + path);
    }

    private static string GetPrefabPath(GameObject obj)
    {

        Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(obj);


        string path = "";
        if (prefab != null) path = UnityEditor.AssetDatabase.GetAssetPath(prefab);//Prefabのパスを取得

        return path;
    }
}
