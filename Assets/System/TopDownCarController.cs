using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    public StatManager statManager;
    public UIManager uIManager;
    public CarSfxHandler carSfxHandler;
    [Header("Car Settings")]
    public bool isAccelerate;
    public float acceleratorFactor = 30f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float maxSpeed = 20f;
    public float maxSteering = 45f;
    internal Vector2 engineForceVector;
    [Header("Knock Back")]
    internal Vector2 knockBackDirection;
    [SerializeField] internal bool carCrash;
    [Header("Colliders")]
    [SerializeField] internal int overtake;
    [SerializeField] internal float speedBoost;

    // [SerializeField] internal float crashRotation;
    // public float crashLerp = 1;

    [Header("Guider")]
    Vector2 targetDir;

    [SerializeField] Vector3 guiderPosition;


    [Header("Local Variable")]
    [SerializeField] internal Rigidbody2D carRigidbody2D;
    [SerializeField] Transform guiderTF;
    [SerializeField] internal float angle;

    //local Varialbe
    [SerializeField] bool canTurnLeft;
    [SerializeField] bool canTurnRight;
    [SerializeField] internal float accelerationInput = 0;
    [SerializeField] internal float accelerationInputSpeed = 0;
    [SerializeField] float steeringInput = 0;
    [SerializeField] float rotaionAngle = 0;
    [SerializeField] internal float velocityVsUp = 0;
    [SerializeField] float SteeringCal = 0;

    Vector3 target;
    [SerializeField] float MovementSpeed;
    Vector2 inputVector;
    Quaternion initialRotation;

    [Header("Environment")]
    [SerializeField] RoadGenerator roadGenerator;

    [Header("Living")]
    internal bool isDead;

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        SteeringCal = transform.rotation.z * 100;

        // inputVector = Vector2.zero;
        // inputVector.x = Input.GetAxis("Horizontal");
        // inputVector.y = Input.GetAxis("Vertical");
        // SetInputFactor(inputVector);

        //first to make it infront of the player
        TouchMovement();
        guiderPosition.y = transform.position.y + 3; //alway infront of the object
        guiderTF.position = guiderPosition;

        Acceleration();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //MoveGuider();
        ApplyEngineForce();
        KIllOrthogonalVelocity();
        ApplySteeringForce();
    }


    public void ApplyEngineForce()
    {
        //how much forward we going
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {

            carRigidbody2D.velocity = Vector2.Lerp(carRigidbody2D.velocity, new Vector2(carRigidbody2D.velocity.x, maxSpeed), Time.fixedDeltaTime * 0.5f);
            return;
        }

        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0) //user not clicking
        {
            //slowing down car
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);

        }
        else //user is clicking/holding
        {
            carRigidbody2D.drag = 0;
        }
        //engine forward
        if (!carCrash)
        {
            engineForceVector = transform.up * accelerationInput * acceleratorFactor;
            carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
        }

    }

    public void ApplySteeringForce()
    {
        targetDir = guiderTF.position - transform.position;
        angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        // if (steeringInput == 0)
        // {
        //     rotaionAngle = Mathf.Lerp(rotaionAngle, 0, Time.fixedDeltaTime * 3);
        //     carRigidbody2D.MoveRotation(rotaionAngle);
        //     return;
        // }


        //if the car is moving slowly
        float minTurningSpeed = (carRigidbody2D.velocity.magnitude / 4f);
        minTurningSpeed = Mathf.Clamp01(minTurningSpeed);

        if (!carCrash)
        {
            //steering
            rotaionAngle = (angle - 90);//* minTurningSpeed;
            carRigidbody2D.MoveRotation(rotaionAngle);

        }


    }
    public void MoveGuider()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            guiderPosition.x = Mathf.Lerp(guiderPosition.x, 2f, 5 * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            guiderPosition.x = Mathf.Lerp(guiderPosition.x, -2f, 5 * Time.deltaTime);
        }
    }

    public void CrashedMovement()
    {
        //Engine with no control
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0) //user not clicking
        {
            //slowing down car
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 0.1f);

        }
        else //user is clicking/holding
        {
            carRigidbody2D.drag = 0;
        }

        rotaionAngle -= 5 * turnFactor * velocityVsUp;//* minTurningSpeed;
        carRigidbody2D.MoveRotation(rotaionAngle);


    }
    public void KIllOrthogonalVelocity()
    {
        //steering force
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

    }

    public float GetlateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
    {
        lateralVelocity = GetlateralVelocity();
        isBreaking = false;

        if (accelerationInput < 0.5f && velocityVsUp > 2)
        {
            isBreaking = true;
            return true;
        }

        if (Mathf.Abs(GetlateralVelocity()) > 3.0f)
        {
            return true;
        }

        return false;
    }

    public void TouchMovement()
    {
        if (Input.GetMouseButton(0))
        {
            isAccelerate = true;

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            //prevent out of bound
            if (touchPosition.x > 2)
            {
                return;
            }
            else if (touchPosition.x < -2)
            {
                return;
            }

            target = guiderTF.position;  // Your target is the current position
            target.x = touchPosition.x;        // Your target is moved to the x position of the touch

            guiderPosition.x = Mathf.Lerp(guiderPosition.x, target.x, MovementSpeed * Time.deltaTime); //lerp movemnt left and right

            // deltaMove = target - transform.position;
        }
        else
        {
            isAccelerate = false;
        }
    }

    void Acceleration()
    {
        if (isAccelerate)
        {

            accelerationInput = Mathf.Lerp(accelerationInput, 1, Time.deltaTime * accelerationInputSpeed);
        }
        else
        {
            accelerationInput = 0;
            // if (accelerationInput < 0.2)
            // {
            //     accelerationInput = 0;
            //     return;
            // }
            // accelerationInput = Mathf.Lerp(accelerationInput, 0, Time.deltaTime * accelerationInputSpeed);

        }

    }

    // public void SetInputFactor(Vector2 inputVector)
    // {

    //     steeringInput = inputVector.x;
    //     accelerationInput = inputVector.y;
    // }
    public void OnEnable()
    {
        guiderPosition.x = transform.position.x;
        carCrash = false;
    }

    public void DeathAnim()
    {
        Invoke("RetryPanel", 2);
    }
    public void RetryPanel()
    {
        uIManager.retryPanelGO.SetActive(true);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "LoadRoadBox")
        {
            roadGenerator.GenerateRoad();
        }
        else if (col.gameObject.tag == "Coin")
        {
            var coinCollect = col.gameObject.GetComponent<CoinScript>();
            coinCollect.DisableCoin();
            statManager.coin++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            knockBackDirection = col.transform.position - transform.position;
            // boxCollider.enabled = false;
            if (velocityVsUp > maxSpeed / 2)
                carRigidbody2D.AddForce(-knockBackDirection.normalized * 100, ForceMode2D.Force);

            carSfxHandler.CarHitSFX(col);
        }


    }
}
