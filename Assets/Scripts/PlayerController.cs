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
			//when we have two players
			/*
			//Press the Space Button to switch cameras
	        if (Input.GetKeyDown(KeyCode.Space))
	        {
	            //Check that the Main Camera is enabled in the Scene, then switch to the other Camera on a key press
	            if (m_MainCamera.enabled)
	            {
	                //Enable the second Camera
	                m_CameraTwo.enabled = true;

	                //The Main first Camera is disabled
	                m_MainCamera.enabled = false;
	            }
	            //Otherwise, if the Main Camera is not enabled, switch back to the Main Camera on a key press
	            else if (!m_MainCamera.enabled)
	            {
	                //Disable the second camera
	                m_CameraTwo.enabled = false;

	                //Enable the Main Camera
	                m_MainCamera.enabled = true;
	            }
	        }
			*/
		}
	}
	//"Activation" is when the player object is triggered by 
	public void onActivation(){
	}
}