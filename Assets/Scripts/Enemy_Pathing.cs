using UnityEngine;
using System.Collections;

public class Enemy_Pathing : MonoBehaviour
{
    // a starting and ending marker for the enemy to traverse (start is always its own starting point)
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed;

    void Update() 
    {
        transform.position = Vector3.Lerp (startPosition, endPosition, Mathf.PingPong(Time.time*speed, 1.0f));
    }
}