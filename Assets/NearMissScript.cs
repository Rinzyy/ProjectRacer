using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMissScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Touch");
            //hit the froncollider which is the child of an object that contain the script
            var playerCol = col.gameObject.GetComponentInParent<TopDownCarController>();
            playerCol.statManager.coin++;
            if (playerCol.overtake == 0)
            {
                playerCol.overtake = 1;
            }
            else if (playerCol.overtake == 1)
            {
                playerCol.overtake = 2;
            }
            if (playerCol.overtake == 2)
            {
                if (playerCol.velocityVsUp <= playerCol.maxSpeed + 2)
                    playerCol.carRigidbody2D.AddForce(playerCol.transform.position.normalized * playerCol.speedBoost, ForceMode2D.Impulse);
            }

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var playerCol = col.gameObject.GetComponentInParent<TopDownCarController>();
            playerCol.overtake = 0;
        }
    }
}
