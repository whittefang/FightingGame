using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// script to handle attack move implementation for jack of all trades character
public class CharacterAttackScriptJAT : MonoBehaviour {
	bool airmove;
	delegate void voidFuncDel();
	public GameObject NormalMoveObj;
	public GameObject NormalMoveAirObj;
	public GameObject UppercutObj;
	public GameObject ThrowObj;
	public GameObject SweepObj;
	public GameObject SuperObj;
	public float FireballStartup;
	public float FireballRecovery;
	public float FireballSpeed;
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
	SoundScript SS;
	Health HS;
	int superCounter;

	FacingScript facing;
	CharacterStateScript CSS;
	// Use this for initialization
	void Start () {
		airmove = true;
		superCounter = 0;
		RB = GetComponentInParent<Rigidbody2D> ();
		SS = GetComponent<SoundScript> ();
		facing = GameObject.Find ("MatchHandler").GetComponent<FacingScript> ();
		CC = GetComponent<CharController> ();
		CSS = GetComponent<CharacterStateScript> ();
		CC.SetAttackFunctions (FireBall, NormalMove, Uppercut, Throw, Parry, Sweep, Super ,cancelStartup);
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
				CSS.SetState(4, .2f);
				airmove = true;
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
			Invoke ("cancelNormalMoveAir", .4f);
		} 
	}
	//resets air move tracker
	public void cancelNormalMoveAir(){
		NormalMoveAirObj.gameObject.SetActive (false);	

	}
	
	
	// uppercut functions
	public void Uppercut(){
		if (CSS.GetState() == 1){
			CSS.SetState(4, UppercutMoveRecovery);
			Invoke("UseUppercut", UppercutMoveStartup);
		}
	}
	
	void UseUppercut(){
		
		UppercutObj.gameObject.SetActive (true);
		if (facing.getDirection (Player1)) {
			RB.AddForce(new Vector2(600, 2000));
		}else {
			RB.AddForce(new Vector2(-600, 2000));
		}
		Invoke ("TurnOffMoves", .3f);
		Invoke ("ResetForce", .20f);
		
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
		SuperObj.gameObject.SetActive (false);
		SuperObj.gameObject.SetActive (true);
		if (superCounter < 10) {
			Invoke("UseSuper", .05f);
			superCounter++;
		} else {
			superCounter = 0;
			Invoke ("TurnOffMoves", .05f);
		}
	}

	// to be used to turn off throw and normamove objects
	void TurnOffMoves(){
		ThrowObj.gameObject.SetActive (false);
		UppercutObj.gameObject.SetActive (false);
		NormalMoveObj.gameObject.SetActive (false);
		NormalMoveAirObj.gameObject.SetActive (false);
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
		RB.AddForce(new Vector2(0, -200));
	}
	
	
}