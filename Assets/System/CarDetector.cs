using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDetector : MonoBehaviour
{
    public EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            var sameSpeed = col.GetComponent<EnemyScript>();
            enemyScript.AIBraking();
            // enemyScript.maxSpeed = sameSpeed.maxSpeed - 2;
            // Vector2 SpeedSlow = sameSpeed.carRigidbody2D.velocity;
            // enemyScript.carRigidbody2D.velocity = new Vector2(enemyScript.gameObject.transform.position.x, SpeedSlow.y - 2);
        }
    }

    // void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "Enemy")
    //     {
    //         var sameSpeed = col.GetComponent<EnemyScript>();
    //         enemyScript.maxSpeed = enemyScript.initialSpeed;
    //         // Vector2 SpeedSlow = sameSpeed.carRigidbody2D.velocity;
    //         // enemyScript.carRigidbody2D.velocity = new Vector2(enemyScript.gameObject.transform.position.x, SpeedSlow.y - 2);
    //     }
    // }
}
