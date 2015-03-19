using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//handles player health, health ui features, and player attack collision events

public class Health : MonoBehaviour {
	public delegate void voidFuncDel();
	protected voidFuncDel CancelStartDel;
	protected SoundScript SS;
	[SerializeField] float health;
	protected Image guibar;
	protected RoundScript roundHandler;
	bool CanDamage = false;
	protected string enemy;
	SuperMeterScript SMS;
	public GameObject OtherPlayer;
	public bool Player1;
	public CharacterStateScript CSS;

	// Use this for initialization
	void Start () {
		health = 10;
		CSS = GetComponent<CharacterStateScript> ();
		SS = GetComponent<SoundScript> ();
		roundHandler = GameObject.Find ("MatchHandler").GetComponent<RoundScript>();
		SMS = GetComponent<SuperMeterScript> ();
		if (Player1){
			guibar = GameObject.Find("P1Health").GetComponent<Image>();
			OtherPlayer = GameObject.FindWithTag ("PlayerTwo");
			enemy = "Two";
		}else {
			guibar = GameObject.Find ("P2Health").GetComponent<Image> ();
			OtherPlayer = GameObject.FindWithTag ("PlayerOne");
			enemy = "One";
		}
	}
	
	// Update is called once per frame
	void Update () {
	

	}
	// public function meant to be called to find out the current health amount
	public float GetHealth(){
		return health;
	}

	// public function that sets health to full and adjusts ui accordingly
	public void resetHealth(){
		health = 10;
		guibar.fillAmount = 1f;
		SMS.useMeter ();
	}

	// public function to disable ability to take damage
	public void healthActive(bool x){
		CanDamage = x;
	}

	// function that handels a hit, decreases health and sets knockband and hitstun times, plays sound,  as well as disabling any moves that are starting
	// also checks if player is out of health and ends round if appropriate
	void SuccessfulHit(float hitstun, float knockback, float Damage, bool multihit){
		// func delegate
		CancelStartDel ();
		Debug.Log (transform.position.x);
		if (transform.position.x < -11f){
			OtherPlayer.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(knockback, 0));
		}else if (transform.position.x > 11f){
			OtherPlayer.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-knockback, 0));
		}
		if (hitstun != 0 && multihit){
			CSS.SetState (9, hitstun, knockback);
		}else if (multihit){
			CSS.SetState (9, hitstun);
		}else if (hitstun != 0){
			CSS.SetState (5, hitstun, knockback);
		}else{
			CSS.SetState (5, hitstun);
		}
		health -= Damage;
		SMS.addMeter (Damage);
		guibar.fillAmount -=  Damage * .1f;

		if (health <= 0)
			roundHandler.endRound ();
	}

	void chipDamage(float Damage){
		health -= Damage*.15f;
		SMS.addMeter (Damage*.15f);
		guibar.fillAmount -=  Damage * .015f;
		
		if (health <= 0)
			roundHandler.endRound ();
	}

	public void setCancelFunc(voidFuncDel x){
		CancelStartDel = x;
	}

	// trigger enter function to check for hitboxes and takes in move information and check if hit can land
	// calls appropriate functions if hit is successful
	void OnTriggerEnter2D(Collider2D other){
		HitboxInfoScript HB = other.GetComponent<HitboxInfoScript> ();
		Debug.Log (other.tag);
		// check for hitbox tag, and if player is in hitstun
		if (CanDamage && (other.tag == "Player"+  enemy + "HitBox")   && ( CSS.GetState() != 5) && ( CSS.GetState() != 10) && !HB.isAirThrow()){
			if (HB.isThrow() && (CSS.GetState() != 3) && (CSS.GetState() != 2)){
				SuccessfulHit(HB.getHitstun(), 0, HB.getDamage(), HB.isMultiHit());
				SS.playGrab ();
				CSS.throwAnim(HB.getKnockback(), HB.getAirKnockback(), HB.getHitstun());
			}
			// check if player is airborne
			else if ((CSS.GetState() == 3) && !HB.isThrow()){
				SS.playHit ();
				SuccessfulHit(HB.getAirHitstun(), HB.getAirKnockback(), HB.getDamage(), HB.isMultiHit());
			}
			// check for parry
			else if (CSS.GetState() == 8 && !HB.getFireball()){
				CSS.SetState(1, 0f);
				SS.playParry();
				other.GetComponentInParent<CharacterStateScript>().SetState(4, .75f);
			}
			// check if move is unblockable
			else if (HB.isUnblockable()){
				SS.playHit ();
				SuccessfulHit(HB.getHitstun(), HB.getKnockback(), HB.getDamage(), HB.isMultiHit());
			} 
			// check if character is blocking
			else if (( CSS.GetState() != 6) && ( CSS.GetState() != 2) && !HB.isThrow()){
				SS.playHit ();
					SuccessfulHit(HB.getHitstun(), HB.getKnockback(), HB.getDamage(), HB.isMultiHit());
				if (HB.getFireball())
					other.gameObject.SetActive(false);
				
			}
			// player is blocking
			else if (!HB.isThrow()){
				CancelStartDel ();

				if (transform.position.x < -11f){
					OtherPlayer.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(HB.getBlockPushback(), 0));
				}else if (transform.position.x > 11f){
					OtherPlayer.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-HB.getBlockPushback(), 0));
				}
				CSS.SetState(2, HB.getBlockstun(), HB.getBlockPushback());
				chipDamage(HB.getDamage());
			SS.playBlock();
			}
		}else if ((CSS.GetState() == 3) && HB.isAirThrow()  ){
			SuccessfulHit(HB.getHitstun(), 0, HB.getDamage(), HB.isMultiHit());
			SS.playGrab ();
			CSS.throwAnim(-HB.getKnockback(), HB.getAirKnockback(), HB.getHitstun());
		}
	}
}
