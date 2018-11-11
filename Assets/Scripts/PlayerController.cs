using UnityEngine;

public class PlayerController : MonoBehaviour
{
		//This is Main Camera in the Scene
    Camera m_MainCamera;
    //This is the second Camera and is assigned in inspector
	public Camera m_CameraTwo;

	//I moved all the camera code to BoardController.cs:
	//We don't want it to tied to the player when we have two players running around

	//in some cases, we want to determine whether we'll 'start' with the 2D or 3D object active.
	//for now, we can manually set this but if we introduce enemies we're going to need some kind of function
	//that deduces whether we're starting in 2D or 3D and then activate all the objects with the appropriate tag(s).
	//by default, the 2DPlayer is checked as active and the 3DPlayer is left as not active
	public bool activated;
	void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
        //This enables Main Camera
        //m_MainCamera.enabled = true;
        //Use this to disable secondary Camera
        //m_CameraTwo.enabled = false;
    }
    void Update()
    {	
		//THE PLAYER ONLY RESPONDS IF THEY'RE THE ACTIVE PLAYER
		if(activated == true ){
			//find board controller object
			GameObject boardControllerTemp = GameObject.Find ("BoardControllerObject");
			//Debug.Log (boardControllerTemp.GetComponent<BoardController>());
			boardControllerTemp.GetComponent<BoardController>().recordChangedPlayerPosition (gameObject.transform.position.x,gameObject.transform.position.z);
			// Vector3 horizontalVelocity = transform.velocity;
	        // horizontalVelocity = new Vector3(transform.velocity.x, 0, transform.velocity.z);

	        // // The speed on the x-z plane ignoring any speed
	        // float horizontalSpeed = horizontalVelocity.magnitude;

			Rigidbody rb = GetComponent<Rigidbody>();
	 		Vector3 v3Velocity = rb.velocity;

			if (v3Velocity == Vector3.zero) {
				var x = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
				var z = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;

				transform.Translate(x, 0, 0);
				transform.Translate(0, 0, -z);
			}

			//I moved the camera code to boardcontroller - we don't want it to tied to the player
		}
	}
	//"Activation" is when the player object is triggered by the space key in the BoardController
	//we use this to update the player's position accordingly
	//at some point we will need to check if swapping from 3D back to 2D will produce a collision on the 2d board,
	//and disable swaps if it does
	public void activateAndUpdate(){
		GameObject boardControllerTemp = GameObject.Find ("BoardControllerObject");
		//get the stored x and z coordinates and update the object.
		Vector2 current_position = boardControllerTemp.GetComponent<BoardController>().returnCurrentPlayerPosition();
		gameObject.transform.position = new Vector3(current_position[0],gameObject.transform.position.y,current_position[1]);
	}

	//change player's activated status
	public void setActivatedStatus(bool status){
		activated = status;
	}
}