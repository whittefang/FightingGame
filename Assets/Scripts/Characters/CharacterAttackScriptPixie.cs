using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterAttackScriptPixie :  MonoBehaviour {
	bool airmove;
	public GameObject NormalMoveObj;
	public GameObject NormalMoveAirObj;
	public GameObject UppercutObj;
	public GameObject ThrowObj;
	public GameObject SweepObj;
	public GameObject SuperObj;
	public GameObject SlideObj;
	public float SlideStartup;
	public float SlideRecovery;
	public float SlideSpeed;
	public float NormalMoveRecovery;
	public float NormalMoveStartup;
	public float UppercutMoveRecovery;
	public float UppercutMoveStartup;
	public float ThrowRecovery;
	public float ThrowStartup;
	public float ParryRecovery;
	public float ParryStartup;
	public float SweepRecovery;
	public float SweepStartup;
	public float SuperRecovery;
	public float SuperStartup;
	public CharController CC;
	public bool Player1;
	public Rigidbody2D RB;
	GameObject FBContainer;
	FacingScript facing;
	CharacterStateScript CSS;
	Health HS;
	// Use this for initialization
	void Start () {
		airmove = true;
		RB = GetComponentInParent<Rigidbody2D> ();
		facing = GameObject.Find ("MatchHandler").GetComponent<FacingScript> ();
		CC = GetComponent<CharController> ();
		CSS = GetComponent<CharacterStateScript> ();
		CC.SetAttackFunctions (Slide, NormalMove, Uppercut, Throw, Parry, Sweep, Super ,cancelStartup);
		CSS.setCanceler (cancelStartup);
		HS = GetComponent<Health>();
		HS.setCancelFunc (cancelStartup);


		facing = GameObject.Find ("MatchHandler").GetComponent<FacingScript> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// check for ground until no longer airborne

		if ((transform.position.y <= -3) ){

			if ((CSS.GetState() == 3) && (NormalMoveObj.activeSelf)){
			
				cancelNormalMoveAir();
				airmove = true;
				CSS.SetState(4, .2f);
				Debug.Log ("cancel");
			}else if (CSS.GetState() == 3){
				CSS.SetState(1);
			}

			if (airmove == false){
				airmove=true;
			}
		}
	}
	// general attack function info:
	// all functions have two parts, a basice wrapper that checks character state then calles move with same name after set amount of time 
	
	
	// fireball controll functions
	public void Slide(){
		if (CSS.GetState() == 1){
			CSS.SetState(4, SlideRecovery);
			Invoke("UseSlide", SlideStartup);
		}
	}
	void UseSlide(){

		SlideObj.gameObject.SetActive (true);
		ResetForce();
		//rigidbody2D.gravityScale = 0;
		if (facing.getDirection (Player1)) {
			RB.AddForce(new Vector2(SlideSpeed, 0));
		}else {
			RB.AddForce(new Vector2(-SlideSpeed, 0));
		}
		Invoke("ResetForce", .25f);
		Invoke ("TurnOffMoves", .25f);
	}

	
	
	// normal move functions
	public void NormalMove(){
		if (CSS.GetState() == 1){
			CSS.SetState(4, NormalMoveRecovery);
			Invoke("UseNormalMove", NormalMoveStartup);
		}else if (CSS.GetState() == 3 && airmove == true){
			Invoke("UseNormalMoveAir", NormalMoveStartup);
		}
	}
	void UseNormalMove(){
		NormalMoveObj.gameObject.SetActive (true);
		Invoke ("TurnOffMoves", .05f);
	}
	// air version of normal move
	void UseNormalMoveAir(){
		if (NormalMoveAirObj.gameObject.activeSelf == false) {
			airmove = false;
			if (facing.getDirection(Player1)){
				RB.AddForce(new Vector2(750, -750));
			} else{
				RB.AddForce(new Vector2(-750, -750));
			}
			NormalMoveAirObj.gameObject.SetActive (true);
			Invoke("ResetForce", .5f);
			Invoke ("cancelNormalMoveAir", .4f);
		} 
		
	}
	//resets air move tracker
	public void cancelNormalMoveAir(){
		CancelInvoke ();
		NormalMoveAirObj.gameObject.SetActive (false);	

		ResetForce();
	}
	
	
	// uppercut functions
	public void Uppercut(){
		if ((CSS.GetState() == 1) && (UppercutObj.activeSelf == false)){
			CSS.SetState(4, UppercutMoveRecovery);
			Invoke("UseUppercut", UppercutMoveStartup);
		}
	}
	
	void UseUppercut(){
		if (facing.getDirection (Player1)) {
			UppercutObj.transform.position = new Vector3 (transform.position.x + 3, transform.position.y, -5f);
		} else {
			UppercutObj.transform.position = new Vector3 (transform.position.x - 3, transform.position.y, -5f);
		}
		UppercutObj.gameObject.SetActive (true);
	}
	
	// throw functions
	public void Throw(){
		if (CSS.GetState() == 1){
			CSS.SetState(4, ThrowRecovery);
			Invoke("UseThrow", ThrowStartup);
			
		}
	}
	
	void UseThrow(){
		ThrowObj.gameObject.SetActive (true);
		Invoke ("TurnOffMoves", .05f);
		
	}

	// sweep functions
	public void Sweep(){
		if (CSS.GetState() == 1){
			CSS.SetState(4, SweepRecovery);
			Invoke("UseSweep", SweepStartup);
		}
	}
	void UseSweep(){
		SweepObj.gameObject.SetActive (true);
		Invoke ("TurnOffMoves", .05f);
	}
	
	// Parry functions
	public void Parry(){
		if (CSS.GetState() == 1){
			CSS.SetState(4);
			Invoke("UseParry", ParryStartup);
		}
	}
	void UseParry(){
		CSS.SetState(8, .2f);
		Invoke("parryRecover", .2f);
		GetComponent<Renderer>().material.color = Color.blue;
		StartCoroutine (TimedrColorChange (.2f));
	}
	// maybe temporary
	void parryRecover(){
		Debug.Log ("parry recovery");
		CSS.SetState(4, ParryRecovery);
	}
	IEnumerator TimedrColorChange(float duration){
		yield return new WaitForSeconds(duration);
		GetComponent<Renderer>().material.color = Color.white;
	}
	
	// Super functions
	public void Super(){
		if (CSS.GetState() == 1){
			CSS.SetState(4, SuperRecovery);
			Invoke("UseSuper", SuperStartup);
		}
	}
	void UseSuper(){
		SuperObj.gameObject.SetActive (true);
		Invoke ("TurnOffMoves", .05f);
	}
	
	// to be used to turn off throw and normamove objects
	void TurnOffMoves(){
		ThrowObj.gameObject.SetActive (false);
		UppercutObj.gameObject.SetActive (false);
		NormalMoveObj.gameObject.SetActive (false);
		NormalMoveAirObj.gameObject.SetActive (false);
		SlideObj.gameObject.SetActive (false);
		SuperObj.gameObject.SetActive (false);
		SweepObj.gameObject.SetActive (false);
	}
	
	// called to stop everything that script may be doing and reset char to default
	public void cancelStartup(){
		CancelInvoke ();
		TurnOffMoves();
	}
	
	// generic physics reset with faster falling
	void ResetForce(){
		RB.velocity = new Vector2(0, 0);
		RB.gravityScale = 5;
	}
	
	
}