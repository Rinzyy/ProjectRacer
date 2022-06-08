using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TypeOfObstacle
{
    public static TypeOfObstacle typeOfObstacle;
    public bool isObject;
    public string objectWaveName;
    public bool isRandom;

}
[CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable objects/New Obstacle", order = 1)]
public class ObstacleSO : ScriptableObject
{
    public float extraTime;
    public TypeOfObstacle[] typeOfObstacle;


}
