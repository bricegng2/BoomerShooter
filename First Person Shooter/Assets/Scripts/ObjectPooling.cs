using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();

    public void AddObjectToPool(GameObject obj)
    {
        pooledObjects.Add(obj);
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeSelf == false)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
