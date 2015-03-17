using UnityEngine;
using System.Collections;

// allows player one and two to ignore each others physics
public class PhysicsIgnore : MonoBehaviour {
	public Transform other;
	public bool Player1;
	// Use this for initialization
	void Start () {
		if (Player1) {
			other = GameObject.FindWithTag("PlayerTwo").GetComponent<Transform>();
		}else {
			other = GameObject.FindWithTag("PlayerOne").GetComponent<Transform>();
		}

		Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
