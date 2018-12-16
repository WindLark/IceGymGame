using System;
using UnityEngine;
using System.Collections;

public class Enemy_Pathing : MonoBehaviour
{
    // a starting and ending marker for the enemy to traverse (start is always its own starting point)
    public Vector3 startPosition;
    public Vector3 endPosition;
	[SerializeField] private string rotatePattern = "No Pattern";
    public float speed;
	private bool rotated = false;

    void Update() 
	{	

		//Debug.Log(new Vector3((float)Math.Ceiling(transform.position.x),(float)Math.Ceiling(transform.position.y),(float)Math.Ceiling(transform.position.z)));

		//use Z Axis if the enemy starts on the RIGHT and moves to the LEFT
		if(rotatePattern=="Z Axis" && (float)(transform.position.z + 0.2f) >= endPosition.z && rotated == false){
			rotated = true;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("end position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}
		if(rotatePattern=="Z Axis" && (float)(transform.position.z - 0.2f) <= startPosition.z && rotated == true){
			rotated = false;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("start position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}


		//use Z Axis Reverse if the enemy starts on the LEFT and moves to the RIGHT
		if(rotatePattern=="Z Axis Reverse" && (float)(transform.position.z - 0.2f) <= endPosition.z && rotated == false){
			rotated = true;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("end position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}
		if(rotatePattern=="Z Axis Reverse" && (float)(transform.position.z + 0.2f) >= startPosition.z && rotated == true){
			rotated = false;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("start position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}

		//use X Axis if the enemy starts on the BOTTOM and moves to the TOP
		if(rotatePattern=="X Axis" && (float)(transform.position.x + 0.2f) >= endPosition.x && rotated == false){
			rotated = true;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("end position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}
		if(rotatePattern=="X Axis" && (float)(transform.position.x - 0.2f) <= startPosition.x && rotated == true){
			rotated = false;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("start position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}


		//use X Axis Reverse if the enemy starts on the TOP and moves to the BOTTOM
		if(rotatePattern=="X Axis Reverse" && (float)(transform.position.x - 0.2f) <= endPosition.x && rotated == false){
			rotated = true;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("end position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}
		if(rotatePattern=="X Axis Reverse" && (float)(transform.position.x + 0.2f) >= startPosition.x && rotated == true){
			rotated = false;
			gameObject.transform.RotateAround(transform.position, transform.up, 180f);
			Debug.Log("start position");
			//gameObject.transform.localRotation = Quaternion.Euler(0, 30, 0);
		}

        transform.position = Vector3.Lerp (startPosition, endPosition, Mathf.PingPong(Time.time*speed, 1.0f));
    }
}