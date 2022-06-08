using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Car Settings")]
    public float acceleratorFactor = 30f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float maxSpeed = 20f;
    public float maxSteering = 45f;

    [Header("Local Variable")]
    //local Varialbe
    [SerializeField] bool canTurnLeft;
    [SerializeField] bool canTurnRight;
    [SerializeField] float accelerationInput = 0;
    [SerializeField] float steeringInput = 0;
    [SerializeField] float rotaionAngle = 0;
    [SerializeField] internal float velocityVsUp = 0;
    [SerializeField] float SteeringCal = 0;

    [SerializeField] Rigidbody2D carRigidbody2D;
    Vector2 inputVector;
    Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        SteeringCal = transform.rotation.z * 100;

        inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        SetInputFactor(inputVector);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        KIllOrthogonalVelocity();
        ApplySteeringForce();
    }

    public void ApplyEngineForce()
    {
        //how much forward we going
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
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
        Vector2 engineForceVector = transform.up * accelerationInput * acceleratorFactor;
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    public void ApplySteeringForce()
    {

        if (canTurnLeft == false && rotaionAngle > 0)
        {
            rotaionAngle = Mathf.Lerp(rotaionAngle, 0, Time.fixedDeltaTime * 5); //broken
            carRigidbody2D.MoveRotation(rotaionAngle);

            return;
        }
        if (canTurnRight == false && rotaionAngle < 0)
        {
            rotaionAngle = Mathf.Lerp(rotaionAngle, 0, Time.fixedDeltaTime * 5); //broken
            carRigidbody2D.MoveRotation(rotaionAngle);

            return;
        }
        if (steeringInput == 0)
        {
            rotaionAngle = Mathf.Lerp(rotaionAngle, 0, Time.fixedDeltaTime * 3);
            carRigidbody2D.MoveRotation(rotaionAngle);
            return;
        }

        // if (SteeringCal < -maxSteering && steeringInput > 0)
        //     return;
        // if (SteeringCal > maxSteering && steeringInput < 0)
        //     return;

        //if the car is moving slowly
        float minTurningSpeed = (carRigidbody2D.velocity.magnitude / 0.5f);
        minTurningSpeed = Mathf.Clamp01(minTurningSpeed);

        //steering
        rotaionAngle -= steeringInput * turnFactor * minTurningSpeed;
        if (rotaionAngle > maxSteering)
        {
            rotaionAngle = maxSteering;
            return;
        }
        if (rotaionAngle < -maxSteering)
        {
            rotaionAngle = -maxSteering;
            return;
        }

        carRigidbody2D.MoveRotation(rotaionAngle);
    }

    public void KIllOrthogonalVelocity()
    {
        //steering force
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

    }

    public void SetInputFactor(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "BoxLeft")
        {
            canTurnLeft = false;
        }
        else if (col.gameObject.name == "BoxRight")
        {
            canTurnRight = false;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "BoxLeft")
        {
            canTurnLeft = true;
        }
        else if (col.gameObject.name == "BoxRight")
        {
            canTurnRight = true;
        }
    }
}
