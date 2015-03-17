using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// script to handle attack move implementation for zoner character
public class CharacterAttackScriptZoner: MonoBehaviour {
	bool airmove;
	public GameObject NormalMoveObj;
	public GameObject NormalMoveAirObj;
	public GameObject TrapObj;
	public GameObject ThrowObj;
	public GameObject SweepObj;
	public GameObject SuperObj;

	public float FireballStartup;
	public float FireballRecovery;
	public float FireballSpeed;
	public float NormalMoveRecovery;
	public float NormalMoveStartup;
	public float TrapMoveRecovery;
	public float TrapMoveStartup;
	public float ThrowRecovery;
	public float ThrowStartup;
	public float ParryRecovery;
	public float ParryStartup;
	public float SweepRecovery;
	public float SweepStartup;
	public float SuperRecovery;
	public float SuperStartup;
	GameObject FBContainer;
	public CharController CC;
	public bool Player1;
	public Rigidbody2D RB;
	SoundScript SS;
	FacingScript facing;
	CharacterStateScript CSS;
	Health HS;
	// Use this for initialization
	void Start () {
		airmove = true;
		SS = GetComponent<SoundScript> ();
		RB = GetComponentInParent<Rigidbody2D> ();
		facing = GameObject.Find ("MatchHandler").GetComponent<FacingScript> ();
		CC = GetComponent<CharController> ();
		CSS = GetComponent<CharacterStateScript> ();
		CC.SetAttackFunctions (FireBall, NormalMove, Trap, Throw, Parry, Sweep, Super,cancelStartup );
		CSS.setCanceler (cancelStartup);
		HS = GetComponent<Health>();
		HS.setCancelFunc (cancelStartup);
		if (Player1){
			FBContainer = GameObject.Find("PlayerOneFireBall");
			removeFB();
		}else {
			FBContainer = GameObject.Find("PlayerTwoFireBall");
			removeFB();
		}
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
	public void FireBall(){
		if ((CSS.GetState() == 1) && (FBContainer.activeSelf == false)){
			CSS.SetState(4, FireballRecovery);
			Invoke("ThrowFireBall", FireballStartup);
		}
	}
	void ThrowFireBall(){
		SS.playFireball ();
		FBContainer.SetActive (true);
		FBContainer.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		if (facing.getDirection (Player1)) {
			FBContainer.transform.rotation = Quaternion.Euler(0, 0, 0);
			FBContainer.GetComponent<Rigidbody2D>().AddForce(new Vector2(FireballSpeed, 0));
		}else {
			FBContainer.transform.rotation = Quaternion.Euler(0, 0, 180);
			FBContainer.GetComponent<Rigidbody2D>().AddForce(new Vector2(-FireballSpeed, 0));
		}
	}
	public void removeFB(){
		if (FBContainer != null)
			FBContainer.SetActive (false);

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
			NormalMoveAirObj.gameObject.SetActive (true);
			ResetForce();
			RB.gravityScale = 0;
			if (facing.getDirection (Player1)) {
				RB.AddForce(new Vector2(1000, 0));
			}else {
				RB.AddForce(new Vector2(-1000, 0));
			}
			Invoke("ResetForce", .5f);
			Invoke ("cancelNormalMoveAir", .4f);
		} 
		
	}
	//resets air move tracker
	public void cancelNormalMoveAir(){
		NormalMoveAirObj.gameObject.SetActive (false);
	}
	
	
	// trap functions
	public void Trap(){
		if (CSS.GetState() == 1){
			CSS.SetState(4, TrapMoveRecovery);
			Invoke("UseTrap", TrapMoveStartup);
		}
	}
	
	void UseTrap(){
		TrapObj.gameObject.SetActive (false);
		TrapObj.gameObject.SetActive (true);
		if (facing.getDirection (Player1)) {
			TrapObj.transform.position  = new Vector3( transform.position.x +6, -4.3f, 0);
		}else {
			TrapObj.transform.position  = new Vector3( transform.position.x -6, -4.3f, 0);
			
		}
		
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
			CSS.SetState(4, SuperRecovery);
			Invoke("UseSuper", SuperStartup);
	}
	void UseSuper(){
		SS.playFireball ();
		SuperObj.SetActive (true);
		SuperObj.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		if (facing.getDirection (Player1)) {
			SuperObj.transform.rotation = Quaternion.Euler(0, 0, 0);
			SuperObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 0));
		}else {
			SuperObj.transform.rotation = Quaternion.Euler(0, 0, 180);
			SuperObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 0));
		}

		Invoke ("TurnOffMoves", 3f);
	}
	
	// to be used to turn off throw and normamove objects
	void TurnOffMoves(){
		ThrowObj.gameObject.SetActive (false);
		TrapObj.gameObject.SetActive (false);
		NormalMoveObj.gameObject.SetActive (false);
		NormalMoveAirObj.gameObject.SetActive (false);
		SuperObj.gameObject.SetActive (false);
		SweepObj.gameObject.SetActive (false);
	}
	

	// called to stop everything that script may be doing and reset char to default
	public void cancelStartup(){
		CancelInvoke ();
		TurnOffMoves();
		RB.gravityScale = 5;
	}
	// generic physics reset
	void ResetForce(){
		RB.velocity = new Vector2(0, 0);
		RB.gravityScale = 5;
	}
	
	
}
