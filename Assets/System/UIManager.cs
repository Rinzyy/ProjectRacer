using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TopDownCarController topDownCarController;

    public GameObject retryPanelGO;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()
    {
        topDownCarController.gameObject.SetActive(false);
        topDownCarController.gameObject.SetActive(true);
        retryPanelGO.SetActive(false);
    }
}
