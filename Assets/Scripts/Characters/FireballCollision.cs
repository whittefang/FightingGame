using UnityEngine;
using System.Collections;

// checks if collision is a firebal then disables this object and other object
// only use on fireball objects or obejcst that should be inactive if it touches a fireball
public class FireballCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<HitboxInfoScript>() != null && other.GetComponent<HitboxInfoScript> ().IsFireball == true) {
			gameObject.SetActive(false);
			other.gameObject.SetActive(false);
		}
	}
}
