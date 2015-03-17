using UnityEngine;
using System.Collections;

public class ReflectorScript : MonoBehaviour {

	public GameObject fireball;
	public FacingScript facing;
	public bool Player1;
	public float FireballSpeed;
	string enemy;
	// Use this for initialization
	void Start () {
		if (Player1)
			enemy = "Two";
		else 
			enemy = "One";
		facing = GameObject.Find ("MatchHandler").GetComponent<FacingScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		HitboxInfoScript HB = other.GetComponent<HitboxInfoScript> ();
		if ( (HB != null) && (other.tag == "Player"+  enemy + "HitBox") && HB.getFireball()){
			HB.gameObject.SetActive(false);

			fireball.SetActive (true);
			fireball.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
			if (facing.getDirection (Player1)) {
				fireball.transform.rotation = Quaternion.Euler(0, 0, 0);
				fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(FireballSpeed, 0));
			}else {
				fireball.transform.rotation = Quaternion.Euler(0, 0, 180);
				fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-FireballSpeed, 0));
			}
		}
	}
}
