using UnityEngine;
using System.Collections;

public class ZonerCharacterAttackScript : MonoBehaviour {
	CharacterStateScript CharacterStateMachine;
	bool airmove;
	int NextFB;
	public GameObject NormalMoveObj;
	public GameObject TrapObj;
	public GameObject ThrowObj;
	public float FireballStartup;
	public float FireballRecovery;
	public float FireballSpeed;
	public float NormalMoveRecovery;
	public float NormalMoveStartup;
	public float TrapMoveRecovery;
	public float TrapMoveStartup;
	public float ThrowRecovery;
	public float ThrowStartup;
	public FacingScript facing;
	public bool Player1;
	public GameObject[] FBContainer = new GameObject[3];
	SoundScript SS;
	
	// Use this for initialization
	void Start () {
		NextFB = 0;
		airmove = true;
		SS = GetComponent<SoundScript> ();
		CharacterStateMachine = GetComponent<CharacterStateScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// fireball controll functions
	public void FireBall(){
		if (CharacterStateMachine.GetState() == 1){
			CharacterStateMachine.SetState(4, FireballRecovery);
			Invoke("ThrowFireBall", FireballStartup);
		}
	}
	void ThrowFireBall(){
		SS.playFireball ();
		FBContainer [NextFB].SetActive (true);
		FBContainer[NextFB].transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		if (facing.getDirection (Player1)) {
			FBContainer[NextFB].transform.rotation = Quaternion.Euler(0, 0, 0);
			FBContainer[NextFB].GetComponent<Rigidbody2D>().AddForce(new Vector2(FireballSpeed, 0));
		}else {
			FBContainer[NextFB].transform.rotation = Quaternion.Euler(0, 0, 180);
			FBContainer[NextFB].GetComponent<Rigidbody2D>().AddForce(new Vector2(-FireballSpeed, 0));
		}
		switch (NextFB) {
		case 0:
			NextFB = 1;
			break;
		case 1:
			NextFB = 2;
			break;
		case 2:
			NextFB = 0;
			break;
		}
	}
	public void removeFB(){
		FBContainer [0].SetActive (false);
		FBContainer [1].SetActive (false);
		FBContainer [2].SetActive (false);
	}
	
	
	// normal move functions
	public void NormalMove(){
		if (CharacterStateMachine.GetState() == 1){
			CharacterStateMachine.SetState(4, NormalMoveRecovery);
			Invoke("UseNormalMove", NormalMoveStartup);
		}else if (CharacterStateMachine.GetState() == 3 && airmove == true){
			Invoke("UseNormalMoveAir", NormalMoveStartup);
		}
	}
	void UseNormalMove(){
		NormalMoveObj.transform.localPosition = new Vector3 (3f, 0, 0);
		NormalMoveObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		NormalMoveObj.gameObject.SetActive (true);
		Invoke ("TurnOffMoves", .2f);
	}
	void UseNormalMoveAir(){
		
		if (NormalMoveObj.gameObject.activeSelf == false) {
			airmove = false;
			NormalMoveObj.transform.localPosition= new Vector3 (1.75f, -2.7f, 0f);
			if (facing.getDirection(Player1)){
				NormalMoveObj.transform.rotation = Quaternion.Euler(0f, 0f, -30f);
			} else{
				NormalMoveObj.transform.rotation = Quaternion.Euler(0f, 0f, 30f);
			}
			NormalMoveObj.gameObject.SetActive (true);
			Invoke ("cancelNormalMoveAir", .4f);
		} 
		
	}
	public void cancelNormalMoveAir(){
		NormalMoveObj.gameObject.SetActive (false);	
		airmove = true;
	}
	
	
	// uppercut functions
	public void Uppercut(){
		if (CharacterStateMachine.GetState() == 1){
			CharacterStateMachine.SetState(4, TrapMoveRecovery);
			Invoke("UseUppercut", TrapMoveStartup);
		}
	}
	
	void UseUppercut(){
		TrapObj.gameObject.SetActive (false);
		TrapObj.gameObject.SetActive (true);
		if (facing.getDirection (Player1)) {
			TrapObj.transform.position  = new Vector3( transform.position.x +10, -40.5f, 0);
		}else {
			TrapObj.transform.position  = new Vector3( transform.position.x -10, -40.5f, 0);

		}
		
	}
	
	// throw functions
	public void Throw(){
		if (CharacterStateMachine.GetState() == 1){
			CharacterStateMachine.SetState(4, ThrowRecovery);
			Invoke("UseThrow", ThrowStartup);
			
		}
	}
	
	void UseThrow(){
		ThrowObj.gameObject.SetActive (true);
		Invoke ("TurnOffMoves", .1f);
		
	}
	
	void TurnOffMoves(){
		ThrowObj.gameObject.SetActive (false);
		//TrapObj.gameObject.SetActive (false);
		NormalMoveObj.gameObject.SetActive (false);
	}
	public void cancelStartup(){
		CancelInvoke ();
		TurnOffMoves();
	}
	// generic physics reset with faster falling
	void ResetForce(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -100));
	}
	
	
}