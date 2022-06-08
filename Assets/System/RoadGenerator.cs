using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public ObjectPool roadPool;
    [SerializeField] int roadLength = 60;
    [SerializeField] int highwayNum = 1;

    Vector3 highwayVector;

    // Start is called before the first frame update
    void Start()
    {
        highwayVector.y = roadLength;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateRoad()
    {
        highwayNum++;
        roadLength = 30 * highwayNum;
        highwayVector.y = roadLength;
        roadPool.SpawnObject(highwayVector, this.transform.rotation);
    }


}
