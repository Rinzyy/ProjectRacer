using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public ObjectPool coinpool;
    // Start is called before the first frame update
    void Start()
    {
        //anim.Play("Coin");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableCoin()
    {
        coinpool.ReturnObject(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {


    }
}
