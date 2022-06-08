using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public TopDownCarController topDownCarController;
    public Transform cameraTF;
    public Transform playerTF;
    public float initialSmoothSpeed = 0.125f;
    public float smoothSpeed = 0.125f;

    public float turningAngle;
    public float cameraPowerx;
    public float absolutePlayerPos;

    public Vector3 offset;
    public Vector3 smoothPosition;
    public Vector3 desirePosition;
    public Vector3 desirePositionx; // for angle turning

    public float smoothFloat;
    public float smoothFloatx;

    public void Update()
    {
        turningAngle = topDownCarController.angle - 90;
        cameraPowerx = turningAngle / (90 * 8); // =1*5
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (topDownCarController.accelerationInput == 1 && topDownCarController.velocityVsUp >= topDownCarController.maxSpeed / 2)
        {
            smoothSpeed = Mathf.Lerp(smoothSpeed, 0.4f, Time.deltaTime * 0.3f);
        }
        else if (topDownCarController.accelerationInput <= 1 && topDownCarController.velocityVsUp < topDownCarController.maxSpeed)
            smoothSpeed = Mathf.Lerp(smoothSpeed, initialSmoothSpeed, Time.deltaTime * 0.4f);
        else if (topDownCarController.velocityVsUp < 2)
        {
            smoothSpeed = initialSmoothSpeed;
        }
        desirePosition = playerTF.position + offset;

        // prevent sign affecting the value of the camerapowerx
        absolutePlayerPos = Mathf.Abs(playerTF.position.x);
        //mirror image for - camerapowerx
        desirePositionx.x = absolutePlayerPos * cameraPowerx;

        smoothFloat = Mathf.Lerp(cameraTF.position.y, desirePosition.y, smoothSpeed);
        smoothFloatx = Mathf.Lerp(cameraTF.position.x, desirePositionx.x, smoothSpeed);
        smoothPosition.y = smoothFloat;
        smoothPosition.x = smoothFloatx;
        cameraTF.position = smoothPosition;


    }
}
