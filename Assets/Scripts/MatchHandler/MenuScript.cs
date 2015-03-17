using UnityEngine;
using System.Collections;

// handles menu buttons
public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Play(){
		Application.LoadLevel (2);
	}
	public void Quit(){
		Application.Quit ();
	}
}
