using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    List<Vector3> destinations = new List<Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] destinationObjects = GameObject.FindGameObjectsWithTag("NPCDestination");
        for (int i = 0; i < destinationObjects.Length; i++)
        {
            destinations.Add(destinationObjects[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 PickDestination()
    {
        int randomIndex = Random.Range(0, destinations.Count);
        return destinations[randomIndex];
    }
}
