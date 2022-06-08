using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailHandler : MonoBehaviour
{
    public TopDownCarController topDownCarController;
    public TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBreaking))
        {
            trailRenderer.emitting = true;
        }
        else
            trailRenderer.emitting = false;
    }
}
