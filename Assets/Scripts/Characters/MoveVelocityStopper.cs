using UnityEngine;
using System.Collections;

public class MoveVelocityStopper : MonoBehaviour {
	public bool Player1;
	public float Delay;
	public Rigidbody2D RB;
	// Use this for initialization
	void Start () {
		if (Player1)
			RB = GameObject.FindWithTag("PlayerOne").GetComponentInParent<Rigidbody2D> ();
		else
			RB = GameObject.FindWithTag("PlayerTwo").GetComponentInParent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void physicsStop(){
		Debug.Log ("stop me");
		RB.velocity = new Vector2 (0,0);
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerTwo" && Player1){
			Invoke("physicsStop", Delay);
		}else if (other.tag == "PlayerOne" && !Player1){
			Invoke("physicsStop", Delay);
		}
	}
}
