using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour {
	//string name of next level scene
	public string nextLevel;
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("2DPlayer") || other.gameObject.CompareTag ("3DPlayer") )
		{
			Debug.Log ("Hello");
			SceneManager.LoadSceneAsync (nextLevel);
		}
	}
}
