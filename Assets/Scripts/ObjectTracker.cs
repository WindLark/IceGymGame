using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : ScriptableObject {
	public string objectName = "Object";
	//holds current X and Z positions - I didn't make this a vector2 in case we want to add a y coordinate field
	public float currentX;
	public float currentZ;

	/// <summary>
	/// Inits an object with the given name, X, and Z
	/// </summary>
	public void init(string name, float x, float z){
		this.objectName = name;
		this.currentX = x;
		this.currentZ = z;
	}
	public string getName(){
		return this.objectName;
	}
	public void setCoordinates(float x, float z){
		this.currentX = x;
		this.currentZ = z;
	}
}
