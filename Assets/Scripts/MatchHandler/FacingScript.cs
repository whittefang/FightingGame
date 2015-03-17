using UnityEngine;
using System.Collections;


// keeps track of player one and two and sets player facing appropriatly
public class FacingScript : MonoBehaviour {
	public bool p1FacingRight = false;
	public bool p2FacingRight = false;
	GameObject p1;
	GameObject p2;
	// Use this for initialization
	void Start () {
		p1 = GameObject.FindWithTag ("PlayerOne");
		p2 = GameObject.FindWithTag ("PlayerTwo");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if ((p1.transform.position.x > p2.transform.position.x) && ((p1FacingRight) || (!p2FacingRight))){

			if ((p1.transform.position.y <= -3)){
				p1.transform.rotation = Quaternion.Euler(0, 180, 0);
				p1FacingRight = false;
			}
			if ((p2.transform.position.y <= -3)){
				p2.transform.rotation = Quaternion.Euler (0, 0, 0);
				p2FacingRight = true;
			}

		}else if ((p1.transform.position.x < p2.transform.position.x) && ((!p1FacingRight) || (p2FacingRight))){

			if ((p1.transform.position.y <= -3)){
				p1.transform.rotation = Quaternion.Euler(0, 0, 0);
				p1FacingRight = true;
			}
			if ((p2.transform.position.y <= -3)){
				p2.transform.rotation = Quaternion.Euler (0, 180, 0);
				p2FacingRight = false;
			}
		}
		if ((p1.transform.position.y <= -3) && (p2.transform.position.y <= -3)){
			if ((p1.transform.position.x - p2.transform.position.x) < .75f && p2FacingRight) {

				p2.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-50, 0));
				p1.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(50, 0));
			}else if ((p2.transform.position.x - p1.transform.position.x) < .75f && p1FacingRight) {

				p1.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-50, 0));
				p2.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(50, 0));
			}
		}
	}

	// public function that returns the direction the player is facing
	// take a bool if the checked character is p1
	// pass true for player 1 facing and false for player 2
	public bool getDirection(bool player1){
		if (player1)
			return (p1FacingRight);
		else 
			return (!p1FacingRight);
	}
}
