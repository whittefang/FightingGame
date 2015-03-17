using UnityEngine;
using System.Collections;

public class MultihitScript : MonoBehaviour {
	BoxCollider2D hitbox;
	// Use this for initialization
	void Start () {
		hitbox = GetComponent<BoxCollider2D> ();
		Invoke ("flicker", .1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void flicker(){
		hitbox.enabled = false;
		hitbox.enabled = true;
		Invoke ("flicker", .1f);
	}
}
