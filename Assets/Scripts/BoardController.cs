using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
	//list of ObjectTracker coordinates - each ObjectTracker should correspond to a MOVING Player (or Hazard) in play
	private List<ObjectTracker> coordinateList;
	// Use this for initialization

	//This is first Camera in the Scene - typically the Main Camera, 
	//but in the event we want to mess with the player, it can be something different in a different level
	public Camera m_CameraOne;
	//This is the second Camera and is assigned in inspector
	public Camera m_CameraTwo;

	void Start () {

		//First Camera Enabled
		//m_CameraOne.gameObject.SetActive(true);
		m_CameraOne.enabled = true;
		//Second Camera Disabled
		//m_CameraTwo.gameObject.SetActive(false);
		m_CameraTwo.enabled = false;

		//initialize the coordinate list
		//instantiate coordinate objectTracker named playerObjectTracker for playerObject
		coordinateList = new List<ObjectTracker>();
		ObjectTracker playerObjectTracker = ScriptableObject.CreateInstance<ObjectTracker>();

		GameObject two_dee_player = GameObject.FindGameObjectsWithTag("2DPlayer")[0];
		//we find the 2D Player's start coordinate by searching for all objects with tag "2DPlayer"
		//and then add those coordinates to the new initialized objectTracker
		playerObjectTracker.init ("Player",two_dee_player.transform.position.x,two_dee_player.transform.position.z);

		//we will operate under the assumption only ONE object in the list is ever "playerObjectTracker".
		//this is important, because both the 2DPlayer and 3DPlayer object want to follow it
		coordinateList.Add(playerObjectTracker);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//Check that the Main Camera is enabled in the Scene, then switch to the other Camera on a key press
			if (m_CameraOne.enabled==true)
			{
				//Enable the second Camera
				m_CameraTwo.enabled=true;

				//The Main first Camera is disabled
				m_CameraOne.enabled=false;
			}
			//Otherwise, if the Main Camera is not enabled, switch back to the Main Camera on a key press
			else if (m_CameraOne.enabled==false)
			{
				//Disable the second camera
				m_CameraTwo.enabled=false;

				//Enable the Main Camera
				m_CameraOne.enabled=true;
			}
		}
	}

	//when the player's changed position happens, we want the boardcontroller to check it as well
	public void recordChangedPlayerPosition(float x, float z){
		ObjectTracker playerObjectTracker = coordinateList.Find(otracker => otracker.getName().Contains("Player"));
		playerObjectTracker.setCoordinates (x,z);
		Debug.Log ("Position new X successfully set: " + x);
		Debug.Log ("Position new Z successfully set: " + z);
	}
	//return a Vector2 with the current recorded x and z
	public Vector2 returnCurrentPlayerPosition(){
		ObjectTracker playerObjectTracker = coordinateList.Find(otracker => otracker.getName().Contains("Player"));
		Vector2 returnVector = new Vector2 (playerObjectTracker.currentX,playerObjectTracker.currentZ);
		return returnVector;
	}

}
