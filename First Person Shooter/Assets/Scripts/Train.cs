using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 nextPosition;
    Vector3 endPosition;
    float speed = 5f;
    bool isPlayerOnTrain = false;

    PlayerController player;

    public List<GameObject> trainPoints;
    int indexOfNextCurrentPoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject startPoint = GameObject.FindGameObjectWithTag("TrainStartingPoint");
        GameObject endPoint = GameObject.FindGameObjectWithTag("TrainEndPoint");
        
        startPosition = startPoint.transform.position;
        transform.position = startPosition;

        endPosition = endPoint.transform.position;

        indexOfNextCurrentPoint = 1;

        nextPosition = trainPoints[indexOfNextCurrentPoint].transform.position;

        player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnTrain == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            if (transform.position == endPosition)
            {
                return;
            }
            else if (transform.position == trainPoints[indexOfNextCurrentPoint].transform.position)
            {
                indexOfNextCurrentPoint++;

                if (indexOfNextCurrentPoint >= trainPoints.Count)
                {
                    indexOfNextCurrentPoint = 1;
                }

                nextPosition = trainPoints[indexOfNextCurrentPoint].transform.position;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTrain = true;

            player.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTrain = false;
            player.transform.SetParent(null);
        }
    }
}
