using System;
using UnityEngine;
using System.Collections;

public class Enemy_Pathing : MonoBehaviour
{
    // a starting and ending marker for the enemy to traverse (start is always its own starting point)
    public Vector3 startPosition;
    public Vector3 endPosition;
	[SerializeField] private bool canRotate = false;
    public float speed;
	private bool rotated = false;

    void Update() 
	{	

		//Debug.Log(new Vector3((float)Math.Ceiling(transform.position.x),(float)Math.Ceiling(transform.position.y),(float)Math.Ceiling(transform.position.z)));
		if(canRotate==true && (float)(transform.position.z + 0.2f) >= endPosition.z && rotated == false){
			rotated = true;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("end position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}
		if(canRotate==true && (float)(transform.position.z - 0.2f) <= startPosition.z && rotated == true){
			rotated = false;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("start position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}

        transform.position = Vector3.Lerp (startPosition, endPosition, Mathf.PingPong(Time.time*speed, 1.0f));
    }
}