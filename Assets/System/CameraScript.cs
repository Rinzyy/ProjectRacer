using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public SpriteRenderer rink;
    [SerializeField] private float unitOrthoSize;
    [SerializeField] bool isMainMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (isMainMenu)
        {
            float orthSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;
            unitOrthoSize = orthSize;
            Camera.main.orthographicSize = unitOrthoSize;
        }
        else
        {
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = rink.bounds.size.x / rink.bounds.size.y;
            if (screenRatio >= targetRatio)
            {
                unitOrthoSize = rink.bounds.size.y / 2;
                Camera.main.orthographicSize = unitOrthoSize;

            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                unitOrthoSize = rink.bounds.size.y / 2 * differenceInSize;
                Camera.main.orthographicSize = unitOrthoSize;
            }
        }


    }


}
