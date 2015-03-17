using UnityEngine;
using System.Collections;


// tracks position of player one and two and moves camera inbetween them
public class CameraMoveScript : MonoBehaviour {
	Transform p1;
	Transform p2;
	// Use this for initialization
	void Start () {
		p1 = GameObject.FindWithTag ("PlayerOne").GetComponent<Transform> ();
		p2 = GameObject.FindWithTag ("PlayerTwo").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (((p1.position.x + p2.position.x)/2) >-3.3 && ((p1.position.x + p2.position.x)/2) <3.3 && (Mathf.Abs(p1.position.x) + Mathf.Abs(p2.position.x)) <16){
			transform.position = new Vector3((p1.position.x + p2.position.x)/2 , 0,  -10);
		}
	}
}
