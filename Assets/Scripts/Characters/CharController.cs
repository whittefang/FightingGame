 using UnityEngine;
using System.Collections;

// handles user input and player movement
public class CharController : MonoBehaviour {
	// character walk speed variable
	public float Speed = 5;
	// delegate functions for scripts that inherit from this to set moves
	public delegate void voidFuncDel();
	protected voidFuncDel AttackA;
	protected voidFuncDel AttackB;
	protected voidFuncDel AttackC;
	protected voidFuncDel AttackD;
	protected voidFuncDel AttackE;
	protected voidFuncDel AttackF;
	protected voidFuncDel AttackSuper;
	protected voidFuncDel StartupCancelDel;
	protected FacingScript facing;
	SuperMeterScript SMS;
	// hold key bindings
	public string[] controls = new string[9];
	public bool ControlsEnabled = true;
	public float jumpSpeed;
	public CharacterStateScript CSS;
	public bool Player1;
	public bool bufferPeriod;
	public int bufferCount;
	/* 0 horizontal
1 vertical
2 fb
3 normal
4 dp
5 throw
6 block
	*/

	// Use this for initialization
	void Start () {
		bufferPeriod = false;
		bufferCount = 0;
		SMS = GetComponent<SuperMeterScript> ();
		CSS = GetComponent<CharacterStateScript> ();
		facing = GameObject.Find ("MatchHandler").GetComponent<FacingScript> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// check if player controls are on
		if (ControlsEnabled) {
			// left right movement
			if((Input.GetAxis(controls[0]) > 0)&& ( CSS.GetState() == 1)){
				if (facing.getDirection(Player1)){
					transform.Translate(new Vector3(Speed, 0, 0) * Time.deltaTime);
				}else {
					transform.Translate(new Vector3(-Speed, 0, 0) * Time.deltaTime);
				}
				// right movement
			}else if((Input.GetAxis(controls[0]) < 0)&& ( CSS.GetState() == 1)){
				if (facing.getDirection(Player1)){
					transform.Translate(new Vector3(-Speed, 0, 0) * Time.deltaTime);
				}else {
					transform.Translate(new Vector3(Speed, 0, 0) * Time.deltaTime);
				}
			}
			// jump angles
			if ((Input.GetAxis(controls[1]) > 0) && (Input.GetAxis(controls[0]) < 0) && ( CSS.GetState() == 1)){
				CSS.SetState(3);
				GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-jumpSpeed, 1000));
			}
			if ((Input.GetAxis(controls[1]) > 0) && (Input.GetAxis(controls[0]) > 0) && ( CSS.GetState() == 1)){
				CSS.SetState(3);
				GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(jumpSpeed, 1000));

			}
			if ((Input.GetAxis(controls[1]) > 0) && (Input.GetAxis(controls[0]) == 0) && ( CSS.GetState() == 1)){
				CSS.SetState(3);
				GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
				
			}
		}
		// turn off buffer
		if (bufferPeriod && bufferCount > 2) {
			bufferPeriod = false;
			bufferCount = 0;
		}else if (bufferPeriod){
			bufferCount++;
		}

	}
	void Update(){
		if (ControlsEnabled) {
			// attack functions 
			if (((Input.GetButtonDown (controls[2]) && Input.GetButtonDown(controls[3]))  
			    || (Input.GetButton (controls[2]) && Input.GetButtonDown(controls[3])) 
			    || (Input.GetButtonDown (controls[2]) && Input.GetButton(controls[3]))) 
			    && SMS.GetMeter() >= 5 && CSS.GetState() != 3 && (bufferPeriod || CSS.GetState() == 1))  {

				Debug.Log ("ATTACK SUPER USED");
				SMS.useMeter();
				AttackSuper();		
			}else if(Input.GetButtonDown(controls[2]) && (CSS.GetState() == 1)){
				//fireball
				Debug.Log ("attackA");
				bufferPeriod = true;
				AttackA();
			}else if(Input.GetButtonDown(controls[3])&& (CSS.GetState() == 1 || CSS.GetState() == 3)){
				Debug.Log ("attackB");
				bufferPeriod = true;
				AttackB();
			}
			if(Input.GetButtonDown(controls[4])&& (CSS.GetState() == 1)){
				Debug.Log ("attackC");
				AttackC();
			}
			if (Input.GetButtonDown (controls[5])&& (CSS.GetState() == 1)) {
				Debug.Log ("attackD");
				AttackD();		
			}
			if (Input.GetButtonDown (controls[7])  && (CSS.GetState() == 1)) {
				Debug.Log ("attackE");
				AttackE();		
			}
			if (Input.GetButtonDown (controls[8])&& (CSS.GetState() == 1)) {
				AttackF();		
			}

			if (Input.GetAxis (controls[6]) > .1f && ( CSS.GetState() == 1)) {
				Debug.Log ("block");
				if (CSS.GetState() != 6){
					CSS.SetState(6);
				}
			}
			if (Input.GetAxis (controls[6]) < .1f  ) {
				if (CSS.GetState() == 6){
					CSS.SetState(1);	
				}
			}
		}
	}	

	public void SetAttackFunctions(voidFuncDel a, voidFuncDel b, voidFuncDel c, voidFuncDel d, voidFuncDel e , voidFuncDel f , voidFuncDel super, voidFuncDel cancel){
		AttackA = a;
		AttackB = b;
		AttackC = c;
		AttackD = d;
		AttackE = e;
		AttackF = f;
		AttackSuper = super;
		StartupCancelDel = cancel;
	}
	// allows player controls to be turned off and on
	public void SetControlsEnabled(bool IsEnabled){
		ControlsEnabled = IsEnabled;
	}
}
