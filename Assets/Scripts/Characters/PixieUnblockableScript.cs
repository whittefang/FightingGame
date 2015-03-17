using UnityEngine;
using System.Collections;

public class PixieUnblockableScript : MonoBehaviour {
	public float timeTillActivate;
	public GameObject Explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable(){
		Invoke ("BlowUp", timeTillActivate);
	}
	void BlowUp(){
		Explosion.SetActive (true);
	}
}
