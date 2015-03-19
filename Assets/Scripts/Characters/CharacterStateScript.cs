using UnityEngine;
using System.Collections;


// handles all character states such as jumping and hitstun
public class CharacterStateScript : MonoBehaviour {
/* 
states chart
 1 = standing
 2 = blockStun
 3 = airborne
 4 = moveRecovery
5 = hitStun
6 = usingblock
7 = airmove
*/
	public delegate void voidFuncDel();
	protected voidFuncDel NormalMoveCancelDel;
	public bool Player1;
	protected FacingScript facing;
	protected Animator anim;
	[SerializeField] int CurrentState = 1;
	
	// Use this for initialization
	void Start () {
		facing = GameObject.Find ("MatchHandler").GetComponent<FacingScript> ();
	}

	// Update is called once per frame
	void Update () {


	}

	// takes in float and pushes player back that distance
	void pushback(float x){
		if (facing.getDirection(Player1))
			GetComponentInParent<Rigidbody2D>().AddForce (new Vector2(-x, 0));
		else
			GetComponentInParent<Rigidbody2D>().AddForce (new Vector2(x, 0));
	}

	// changes color of character to red
	void colorchange(){

		GetComponent<Renderer>().material.color = Color.red;
	}

	// moves player distance x and y and resets physics after reset
	public void throwAnim(float x, float y, float reset){
		if (facing.getDirection(Player1))
			GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(x, y));
		else 
			GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-x, y));
			StartCoroutine(TimedrVelocityReset (reset));
	}

	// returns the current state of player
	public int GetState(){
		return (CurrentState);
	}

	// allows other scripts to change character state directly
	public void SetState(int newState){
		CurrentState = newState;
	}
	// setState overload for only being in a state for set time period held in AmountOfFrames
	public void SetState(int newState, float AmountOfFrames){
		CurrentState = newState;
		NormalMoveCancelDel ();
		CancelInvoke ();
		Invoke("resetToNeutral", AmountOfFrames);
	}

	// SetState overload allowing timed state and pushback
	public void SetState(int newState, float AmountOfFrames, float pushAmount){
		CurrentState = newState;
		NormalMoveCancelDel ();
		CancelInvoke ();
		pushback(pushAmount);
		StartCoroutine(TimedrVelocityReset (AmountOfFrames));
		Invoke("resetToNeutral", AmountOfFrames);
		if (CurrentState == 5 || CurrentState == 9){
			colorchange();
			Invoke("ColorReset", AmountOfFrames);
			//StartCoroutine(TimedrColorChange (AmountOfFrames));
		}


	}

	// resets character state to default
	void resetToNeutral(){
		CurrentState = 1;
		GetComponentInParent<Rigidbody2D>().velocity = new Vector2 (0, 0);
	}
	void ColorReset(){
		GetComponent<Renderer>().material.color = Color.white;
	}
	// resets state to default through Enumerator
	IEnumerator TimedrStateChange(float duration){
		yield return new WaitForSeconds(duration);
		CurrentState = 1;
	}

	//resets character color
	IEnumerator TimedrColorChange(float duration){
		yield return new WaitForSeconds(duration);
		GetComponent<Renderer>().material.color = Color.white;
	}

	// resets character speed
	IEnumerator TimedrVelocityReset(float duration){
		yield return new WaitForSeconds (duration);
		GetComponentInParent<Rigidbody2D>().velocity = new Vector2 (0, 0);
	}
	public void setCanceler(voidFuncDel x){
		NormalMoveCancelDel = x;
	}
}
