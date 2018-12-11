using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardController : MonoBehaviour {
	//list of ObjectTracker coordinates - each ObjectTracker should correspond to a MOVING Player (or Hazard) in play
	private List<ObjectTracker> coordinateList;
	// Use this for initialization

	//This is first Camera in the Scene - typically the Main Camera, 
	//but in the event we want to mess with the player, it can be something different in a different level
	public Camera m_CameraOne;
	//This is the second Camera and is assigned in inspector
	public Camera m_CameraTwo;


	//the board controller's variables for tracking the 2D mode and 3D mode player objects
	private GameObject twoDPlayer;
	private GameObject threeDPlayer;

	private AudioSource[] sounds;
	private AudioSource deny_shift;
	private AudioSource allow_shift;
	private AudioSource damage;

	//time limit that we set depending on what level we're on
	public float timeLimit;

	//public Text timeLimitDisplay;
	public Canvas timeLimitCanvas;

	private bool timeStillMoving;

	public string nextLevel;


	void Start () {
		twoDPlayer = GameObject.Find ("Player_2D_Mode");
		threeDPlayer = GameObject.Find ("Player_3D_Mode"); 
		sounds = GetComponents<AudioSource>();
		deny_shift = sounds[0];
		allow_shift = sounds[1];
		damage = sounds[2];

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

		//now redundant, we just fnd the 2D player above a different and easier way
		//GameObject two_dee_player = GameObject.FindGameObjectsWithTag("2DPlayer")[0];

		//we find the 2D Player's start coordinate by searching for all objects with tag "2DPlayer"
		//and then add those coordinates to the new initialized objectTracker
		playerObjectTracker.init ("Player",twoDPlayer.transform.position.x,twoDPlayer.transform.position.z);


		//we will operate under the assumption only ONE object in the list is ever "playerObjectTracker".
		//this is important, because both the 2DPlayer and 3DPlayer object want to follow it
		coordinateList.Add(playerObjectTracker);

		//timer text settings:
		timeStillMoving = true;
		updateTimeLimit();
	}

	// Update is called once per frame
	void Update () {
		//subtract time from timer
		if(timeStillMoving == true){
			timeLimit -= Time.deltaTime;
			updateTimeLimit ();
		}
		//
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//FIRST WE MUST CHECK THAT THE SWITCH DOESN'T RESULT IN THE 2D MODE PLAYER COLLIDING WITH AN OBJECT
			//How do we accomplish this?
			//We could project a bounding box in that location
			//if that box returns a collision, we do not swap

			//we'll check the 2D player's location at (X, 0.5, Z), 
			//where X and Z are the current recorded X and Z and Y is 0.5 (our standard Y location for 2d plane objects)
			//all planes have radius 0.45f - We make the bounding box slightly smaller than the player box itself otherwise it
			//detects the floor and gives false positive
			// for more information, see Unity Physics.BoxCheck documentation.
			Vector2 currentPosition = returnCurrentPlayerPosition();
			//this bit-based layermask ensures that we only look at the "Players" layer
			//we want the opposite of that - to check every non "Players" layer
			int layermask_player = 1 << LayerMask.NameToLayer("Players");
			//so we negate the layermask so that the player mask is now 0 (do not check)
			//and every bit corresponding to another layer is 1 (check)
			layermask_player = ~layermask_player;

			//our final modification is to check the layer the 
			//box currently is NOT active on
			//we determine this by checking which camera is active.
			//this feature allows us to set up deceiving obstacles
			//in 2D -and- 3D!
			bool boxcast;
			if(m_CameraOne.enabled==true){ //if the 2D layer is enabled....
				//we check the 3D layer
				boxcast = Physics.CheckBox (new Vector3 ((float)currentPosition [0], -14.5f, (float)(currentPosition [1])), 
					               new Vector3 (0.45f, 0.45f, 0.45f), Quaternion.identity, layermask_player);
			}
			else{ //else we just assume the 3D layer is enabled
				//and we check the 2D layer
				boxcast = Physics.CheckBox (new Vector3 ((float)currentPosition [0], 0.5f, (float)(currentPosition [1])), 
					new Vector3 (0.45f, 0.45f, 0.45f), Quaternion.identity, layermask_player);
			}

			if(boxcast){
				//we make the player red and play a sound to alert them
				//that they can't swap
				Material player_2D_material = twoDPlayer.GetComponent<Renderer>().material;
				Material player_3D_material = threeDPlayer.GetComponent<Renderer>().material;

				player_2D_material.color = Color.red;
				player_3D_material.color = Color.red;

				deny_shift.Play();
				StartCoroutine(returnToCyan(0.2f));
				StartCoroutine(returnToRed(0.4f));
				StartCoroutine(returnToCyan(0.6f));
			} else {
				allow_shift.Play();
				//Check that the Main Camera is enabled in the Scene, then switch to the other Camera on a key press
				if (m_CameraOne.enabled==true)
				{

					//disable 2D mode player
					twoDPlayer.GetComponent<PlayerController>().setActivatedStatus(false);
					//enable 3D mode player
					threeDPlayer.GetComponent<PlayerController>().setActivatedStatus(true);
					//update 3D player's position
					threeDPlayer.GetComponent<PlayerController>().activateAndUpdate();


					//Enable the second Camera
					m_CameraTwo.enabled=true;
					//The Main first Camera is disabled
					m_CameraOne.enabled=false;
				}
				//Otherwise, if the Main Camera is not enabled, switch back to the Main Camera on a key press
				else if (m_CameraOne.enabled==false)
				{

					//enable 2D mode player
					twoDPlayer.GetComponent<PlayerController>().setActivatedStatus(true);
					//disable 3D mode player
					threeDPlayer.GetComponent<PlayerController>().setActivatedStatus(false);
					//update 2D player's position
					twoDPlayer.GetComponent<PlayerController>().activateAndUpdate();

					//Disable the second camera
					m_CameraTwo.enabled=false;
					//Enable the Main Camera
					m_CameraOne.enabled=true;
				}
			}
		}
	}

	//when the player's changed position happens, we want the boardcontroller to check it as well
	public void recordChangedPlayerPosition(float x, float z){
		ObjectTracker playerObjectTracker = coordinateList.Find(otracker => otracker.getName().Contains("Player"));
		playerObjectTracker.setCoordinates (x,z);
		//un-comment if you want to track positions
		//Debug.Log ("Position new X successfully set: " + x);
		//Debug.Log ("Position new Z successfully set: " + z);
	}
	//return a Vector2 with the current recorded x and z
	public Vector2 returnCurrentPlayerPosition(){
		ObjectTracker playerObjectTracker = coordinateList.Find(otracker => otracker.getName().Contains("Player"));
		Vector2 returnVector = new Vector2 (playerObjectTracker.currentX,playerObjectTracker.currentZ);
		return returnVector;
	}

	// after a delay, player objects turns cyan again
	private IEnumerator returnToCyan(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Material player_2D_material = twoDPlayer.GetComponent<Renderer>().material;
		Material player_3D_material = threeDPlayer.GetComponent<Renderer>().material;

		player_2D_material.color = Color.cyan;
		player_3D_material.color = Color.cyan;

	}
	// after a delay, player objects turns red again
	private IEnumerator returnToRed(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Material player_2D_material = twoDPlayer.GetComponent<Renderer>().material;
		Material player_3D_material = threeDPlayer.GetComponent<Renderer>().material;

		player_2D_material.color = Color.red;
		player_3D_material.color = Color.red;

	}

	private void updateTimeLimit(){
		string minutes = Mathf.Floor((float)(timeLimit / 60f)).ToString();
		string seconds = Mathf.Floor((float)(timeLimit % 60f)).ToString();

		//timeLimitCanvas.transform.FindChild ("TextTimer").text = "Time Left: " + minutes + " : " + seconds;
		Text tempDisplay = timeLimitCanvas.transform.Find ("TextTimer").GetComponent<Text>();
		tempDisplay.text = "Time Left: " + minutes + " : " + seconds;
		//timeLimitDisplay.text = "Time Left: " + minutes + " : " + seconds;
		//Debug.Log ("Time Left: " + timeLimit.ToString());
		//Debug.Log (timeLimitDisplay.text);

	}

	public void setTimeMovingFalse(){
		timeStillMoving = false;
	}

	private void OnGUI()
	{
		GUIStyle customButtonTemplate = new GUIStyle("button");
		customButtonTemplate.fontSize = 24;
		//onGUI is constantly checking to see if it needs to create a menu, so we'll control all pop up menus from here
		if(timeStillMoving == false)
		{
			RectTransform  tempPanelRT = timeLimitCanvas.transform.Find ("MenuPanel").GetComponent<RectTransform>();
			tempPanelRT.sizeDelta = new Vector2(700,1000);
			timeLimitCanvas.transform.Find ("YouWinText").gameObject.SetActive (true);

			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 10, 100, 50), "Next Level!")) {
				SceneManager.LoadSceneAsync (nextLevel);
				//Debug.Log("Clicked the button with text");
			}
		}
	}
}
