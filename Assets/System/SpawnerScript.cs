using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
internal class SpawnerScript : MonoBehaviour
{
    public ObstacleChecker obstacleChecker;
    [SerializeField] internal List<ObstacleSO> variation;
    [SerializeField] internal ObstacleSO selectedEnemy;

    [SerializeField] internal int lengthDifficulty; // size of variation
    [SerializeField] internal int numberE;
    [SerializeField] internal bool canSpawn = true;
    [SerializeField] internal float randomNumber;

    internal float nextSpawnTime;
    [SerializeField] internal float spawnInterval;
    [SerializeField] internal float plusExtra;


    [Header("ObstacleSpawn")]
    [SerializeField] internal bool isNoObjectInFront;
    [SerializeField] internal string objectSpawnName;
    [SerializeField] internal bool Spawnable;
    [SerializeField] internal string[] spawnNameTest;
    [SerializeField] internal Transform[] spawnPoint3;

    private Vector3 randomSpawn;
    private ObjectPool obstaclePool;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        //check if it can spawn the next object and the nextSpawnTime is the duration left for the object to spawn
        if (canSpawn && nextSpawnTime < Time.time && !obstacleChecker.isObjectInFront)
        {

            //select the enemy wave from the variation list randomly by nubmerE
            selectedEnemy = variation[numberE];
            /*
            if(numberE >numBlock && numberE <variation.Count)
            {
                eventExtraTime = 0.5f;
            }
            */
            //
            for (int i = 0; i < selectedEnemy.typeOfObstacle.Length; i++)
            {

                GenerateWaveInfo(i); //generate info
                spawnNameTest[i] = objectSpawnName; //name of the object might get remove soon
                SpawnObjects(i, Spawnable, objectSpawnName); //spawn the object 

            }

            //Instantiate(selectedEnemy, SpawnPoints.position, Quaternion.identity);

            //get extra time if the object is big or event
            plusExtra = selectedEnemy.extraTime;
            //random number generator to select from the variation table
            numberE = Random.Range(0, lengthDifficulty);
            //spawntime
            nextSpawnTime = Time.time + spawnInterval + plusExtra; /*+ eventExtraTime;*/
            //reset time
            plusExtra = 0;
        }
    }

    //choose the name for the object 
    [SerializeField]
    internal void GenerateWaveInfo(int i)

    {

        switch (selectedEnemy.typeOfObstacle[i].objectWaveName)
        {
            case "":
                selectedEnemy.typeOfObstacle[i].isObject = false;
                Spawnable = false;
                objectSpawnName = selectedEnemy.typeOfObstacle[i].objectWaveName;

                break;

            case null:
                selectedEnemy.typeOfObstacle[i].isObject = false;
                Spawnable = false;
                objectSpawnName = selectedEnemy.typeOfObstacle[i].objectWaveName;

                break;


            default:
                objectSpawnName = selectedEnemy.typeOfObstacle[i].objectWaveName;
                Spawnable = true;

                break;
        }

        //sometime it work sometime it dont
        //if error is mean that your object in scene is wrong  or spawnable is false 
    }

    [SerializeField]
    internal void SpawnObjects(int i, bool isObject, string objectName)
    {
        if (i == 0)
        {
            randomNumber = Random.Range(0, 0.3f);
        }
        else if (i == 1)
        {
            randomNumber = Random.Range(-0.3f, 0.3f);
        }
        else if (i == 2)
        {
            randomNumber = Random.Range(-0.3f, 0.3f);
        }
        else if (i == 3)
        {
            randomNumber = Random.Range(-0.3f, 0f);
        }
        else
        {
            randomNumber = 0;
        }

        if (isObject)
        {

            randomSpawn = new Vector2(spawnPoint3[i].position.x + randomNumber, spawnPoint3[i].position.y);



            obstaclePool = GameObject.Find(objectName).GetComponent<ObjectPool>();

            obstaclePool.SpawnObject(randomSpawn, spawnPoint3[i].transform.rotation);

        }

    }

}
