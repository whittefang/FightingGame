using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {
	public bool playerOne;
	public int character;
	CharacterSelect CS;
	GameObject eventsystemP1;
	GameObject eventsystemP2;
	// Use this for initialization
	void Awake () {
		CS = GameObject.Find ("CharacterSelectedObj").GetComponent<CharacterSelect> ();
		eventsystemP1 = GameObject.Find ("EventSystemP1");
		eventsystemP2 = GameObject.Find ("EventSystemP2");
			
	}
	void Start(){
		eventsystemP2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetPlayerChar(){
		if (playerOne) {
			CS.setP1(character);
			eventsystemP1.SetActive(false);
			eventsystemP2.SetActive(true);
		}else{
			CS.setP2(character);
			Application.LoadLevel(3);
		}
	}
}
