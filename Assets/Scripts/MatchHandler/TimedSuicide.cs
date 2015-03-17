using UnityEngine;
using System.Collections;
// generic script that turns object off after set time
public class TimedSuicide : MonoBehaviour {
	public float activeTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable(){
		CancelInvoke ();
		Invoke ("disableSelf", activeTime);
	}
	void disableSelf(){
		gameObject.SetActive (false);
	}

}
