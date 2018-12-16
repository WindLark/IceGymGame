using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject help = GameObject.Find("HelpCanvas");
		help.transform.GetChild (0).gameObject.SetActive(false);
		//help.GetComponent ("Panel").enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
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
	public void quit(){
		Application.Quit ();
	}
}
