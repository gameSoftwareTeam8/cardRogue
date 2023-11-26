using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDDOL : MonoBehaviour
{
    public static GameObject[] GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            Object.DontDestroyOnLoad( temp );
            UnityEngine.SceneManagement.Scene dontDestroyOnLoad = temp.scene;
            Object.DestroyImmediate( temp );
            temp = null;
 
            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if( temp != null )
                Object.DestroyImmediate( temp );
        }
    }
    public void DestroyAllDontDestroyOnLoadObjects()
    {
        foreach (GameObject obj in GetDontDestroyOnLoadObjects())
        {
            Destroy(obj);
        }
    }
}
