using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheckingBox : MonoBehaviour
{
    public ObstacleChecker obstacleChecker;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.GetComponent<EnemyScript>().objectPool.ReturnObject(col.gameObject);
            // obstacleChecker.DisableCollider();
        }
    }
}
