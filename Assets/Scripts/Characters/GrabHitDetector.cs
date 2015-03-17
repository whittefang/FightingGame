using UnityEngine;
using System.Collections;

public class GrabHitDetector : MonoBehaviour {
	public bool player1;
	CharacterAttackScriptGrap GAS;
	// Use this for initialization
	void Start () {
		if (player1)
			GAS = GameObject.FindWithTag ("PlayerOne").GetComponent<CharacterAttackScriptGrap>();
		else
			GAS = GameObject.FindWithTag ("PlayerTwo").GetComponent<CharacterAttackScriptGrap>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if ((other.tag == "PlayerTwo") && player1) {
			GAS.GrabAnim();
		} else if ((other.tag == "PlayerOne") && !player1) {
	
		}
	}
}
