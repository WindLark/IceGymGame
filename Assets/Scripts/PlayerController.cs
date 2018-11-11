using UnityEngine;

public class PlayerController : MonoBehaviour
{
		//This is Main Camera in the Scene
    Camera m_MainCamera;
    //This is the second Camera and is assigned in inspector
    public Camera m_CameraTwo;
	void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
        //This enables Main Camera
        m_MainCamera.enabled = true;
        //Use this to disable secondary Camera
        m_CameraTwo.enabled = false;
    }
    void Update()
    {
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
    }
}