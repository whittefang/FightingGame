using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {
	int p1Char;
	int p2Char;
	public GameObject P1JAT;
	public GameObject P2JAT;
	public GameObject P1ZONER;
	public GameObject P2ZONER;
	public GameObject P1PIXIE;
	public GameObject P2PIXIE;
	public GameObject P1GRAP;
	public GameObject P2GRAP;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnLevelWasLoaded(int level) {
		if (level == 3) {
			switch(p1Char){
			case 1:
				Instantiate(P1JAT, new Vector3(-6F, -3, 0), Quaternion.identity);
				break;
			case 2:
				Instantiate(P1ZONER, new Vector3(-6F, -3, 0), Quaternion.identity);
				break;
			case 3:
				Instantiate(P1PIXIE, new Vector3(-6F, -3, 0), Quaternion.identity);
				break;
			case 4:
				Instantiate(P1GRAP, new Vector3(-6F, -3, 0), Quaternion.identity);
				break;
			}		
			switch(p2Char){
			case 1:
				Instantiate(P2JAT, new Vector3(6F, -3, 0), Quaternion.identity);
				break;
			case 2:
				Instantiate(P2ZONER, new Vector3(6F, -3, 0), Quaternion.identity);
				break;
			case 3:
				Instantiate(P2PIXIE, new Vector3(6F, -3, 0), Quaternion.identity);
				break;
			case 4:
				Instantiate(P2GRAP, new Vector3(6F, -3, 0), Quaternion.identity);
				break;
			}
			GameObject.Find("MatchHandler").GetComponent<RoundScript>().InitializeChars();
		}
	}
	public void setP1(int x){
		p1Char = x;
	}
	public void setP2(int x){
		p2Char = x;
	}
}
