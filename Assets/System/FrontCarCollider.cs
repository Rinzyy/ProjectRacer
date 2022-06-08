using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCarCollider : MonoBehaviour
{
    public TopDownCarController topDownCarController;
    public int knockbackForce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            var enemyCol = col.gameObject.GetComponent<EnemyScript>();
            topDownCarController.knockBackDirection = col.transform.position - topDownCarController.transform.position;
            // boxCollider.enabled = false;
            if (topDownCarController.velocityVsUp > enemyCol.maxSpeed + enemyCol.maxSpeed / 4)//topDownCarController.maxSpeed / 2)
            {
                topDownCarController.carCrash = true;
                // topDownCarController.carCrash = true;
                // topDownCarController.crashRotation = topDownCarController.knockBackDirection.x * 360;
                topDownCarController.carRigidbody2D.AddTorque(topDownCarController.knockBackDirection.x * 180);
                if (topDownCarController.knockBackDirection.x > 0)
                {
                    topDownCarController.knockBackDirection.x = 0;
                }
                topDownCarController.carRigidbody2D.AddForce(-topDownCarController.knockBackDirection.normalized * knockbackForce, ForceMode2D.Impulse);
                topDownCarController.carRigidbody2D.angularDrag = 3;
                topDownCarController.carRigidbody2D.drag = 3;


                topDownCarController.isDead = true;
                topDownCarController.DeathAnim();
            }


        }

    }
}
