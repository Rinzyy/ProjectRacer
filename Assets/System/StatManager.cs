using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatManager : MonoBehaviour
{
    public TopDownCarController topDownCarController;
    public PlayerStat playerStat;
    public TMP_Text meterText;
    public TMP_Text speedText;
    public TMP_Text coinText;

    public float distanceTravel;
    public float speedKM;
    public int coin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceTravel = topDownCarController.transform.position.y * 6;
        speedKM = topDownCarController.velocityVsUp * 6;
        meterText.text = distanceTravel.ToString("n0") + "m";
        speedText.text = speedKM.ToString("n0");
        coinText.text = coin.ToString();
    }
}
