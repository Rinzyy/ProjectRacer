using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    public TopDownCarController topDownCarController;
    public bool isObjectInFront;
    public GameObject checkerBoxGO;
    public float extraTime;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (topDownCarController.velocityVsUp <= topDownCarController.maxSpeed / 1.5)
        {
            isObjectInFront = true;
        }
        else
        {
            isObjectInFront = false;
        }

    }

    public void DisableCollider()
    {
        checkerBoxGO.SetActive(false);
       
    }

    public void EnableCollider()
    {
        checkerBoxGO.SetActive(true);

    }



}

