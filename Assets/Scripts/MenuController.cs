using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			//GameObject help = GameObject.Find("HelpCanvas");
			//help.transform.GetChild (0).gameObject.SetActive(false);
			GameObject LS = GameObject.Find ("LevelSelectCanvas");
			LS.transform.GetChild (0).gameObject.SetActive (false);
			//help.GetComponent ("Panel").enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(SceneManager.GetActiveScene().name!="MainMenu"){
				SceneManager.LoadSceneAsync ("MainMenu");
			}
		}
	}

	public void startTheGame(){
		SceneManager.LoadSceneAsync ("Level1");
	}
	public void loadLevel(string levelname){
		SceneManager.LoadSceneAsync (levelname);
	}
	public void displayHelpMenu(){
		GameObject help = GameObject.Find("HelpCanvas");
		if (help.transform.GetChild (0).gameObject.activeSelf == false) {
			help.transform.GetChild (0).gameObject.SetActive (true);
		} else {
			help.transform.GetChild (0).gameObject.SetActive (false);
		}
	}
	public void displayLSMenu(){
		GameObject LS = GameObject.Find("LevelSelectCanvas");
		if (LS.transform.GetChild (0).gameObject.activeSelf == false) {
			LS.transform.GetChild (0).gameObject.SetActive (true);
		} else {
			LS.transform.GetChild (0).gameObject.SetActive (false);
		}
	}
	public void quit(){
		Application.Quit ();
	}
}
