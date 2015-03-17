using UnityEngine;
using System.Collections;


// simple generic data structure that goes onto all move hitboxes
// holds appropriate data to be used in the trigger check in Health script
public class HitboxInfoScript : MonoBehaviour {
	public float Damage;
	public float Knockback;
	public float Hitstun;
	public float BlockStun;
	public float BlockPushback;
	public float AirKnockback;
	public float AirHitstun;
	public bool IsFireball;
	public bool Throw;
	public bool Unblockable;
	public bool AirThrow;
	public bool MultiHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public float getHitstun(){
		return Hitstun;
	}
	public float getDamage(){
		return Damage;
	}
	public float getKnockback(){
		return Knockback;
	}
	public float getAirHitstun(){
		return Hitstun;
	}
	public float getAirKnockback(){
		return AirKnockback;
	}
	public float getBlockstun(){
		return BlockStun;
	}
	public float getBlockPushback(){
		return BlockPushback;
	}
	public bool isUnblockable(){
		return Unblockable;
	} 
	public bool isThrow(){
		return Throw;
	}
	public bool isMultiHit(){
		return MultiHit;
	}
	public bool isAirThrow(){
		return AirThrow;
	}
	public bool getFireball(){
		return IsFireball;
	}
}
