using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] rootsFromDontDestroyOnLoad;
    
    public void DestroyAll()
    {
        rootsFromDontDestroyOnLoad = DontdestroyOnLoadAccessor.Instance.GetAllRootsOfDontDestroyOnLoad();
        Debug.Log(rootsFromDontDestroyOnLoad);
    }
}
