using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public ObjectPool objectPool;
    public Rigidbody2D carRigidbody2D;
    public BoxCollider2D boxCollider;
    [SerializeField] internal float initialSpeed;
    [SerializeField] internal float timeTakenForCollider;
    [SerializeField] internal float velocityVsUp;
    [SerializeField] internal float maxSpeed;
    [SerializeField] internal float driftFactor;
    [SerializeField] internal float acceleratorFactor;

    [SerializeField] internal float randomNum;

    // Start is called before the first frame update
    void Start()
    {
        randomNum=  Random.Range(maxSpeed-1, maxSpeed+1);
        maxSpeed = randomNum;
        carRigidbody2D.velocity = new Vector2(this.transform.position.x, initialSpeed);
        // boxCollider.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        KIllOrthogonalVelocity();
    }
    public void ApplyEngineForce()
    {
        //how much forward we going
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed)
        {
            return;
        }
        if (carRigidbody2D.drag >= 1 && velocityVsUp < maxSpeed -1.5f)
        {   
            carRigidbody2D.drag = 0;
        }

        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            return;


        // }
        // else //user is clicking/holding
        // {
        //     carRigidbody2D.drag = 0;
        // }
        //engine forward
        Vector2 engineForceVector = transform.up * acceleratorFactor;
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }
    public void KIllOrthogonalVelocity()
    {
        //steering force
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

    }

    public void AIBraking()
    {
        carRigidbody2D.drag = 1.5f;

    }

    void OnEnable()
    {
        Invoke("EnableCollider", timeTakenForCollider);
    }

    void EnableCollider()
    {
        boxCollider.enabled = true;

    }
    void OnTriggerEnter2D(Collider2D col)

    {
       if (col.gameObject.tag == "Destroyer" )
        {
            // boxCollider.enabled = false;
            objectPool.ReturnObject(this.gameObject);
        }  
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            objectPool.ReturnObject(this.gameObject);

        }
    }


}
