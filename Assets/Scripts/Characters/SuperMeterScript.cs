using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuperMeterScript : MonoBehaviour {
	[SerializeField] float currentMeter;
	Image GUIMeter;
	public bool Player1;
	// Use this for initialization
	void Start () {
		currentMeter = 0;
		if (Player1) {
			GUIMeter = GameObject.Find ("P1Meter").GetComponent<Image> ();
		} else {
			GUIMeter = GameObject.Find ("P2Meter").GetComponent<Image> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void addMeter(float x){
		currentMeter += x;
		if (currentMeter > 5){
			currentMeter = 5;
		}
		GUIMeter.fillAmount = currentMeter * .2f;
	}
	public void useMeter(){
		currentMeter = 0;
		GUIMeter.fillAmount = 0;
	}
	public float GetMeter(){
		return currentMeter;
	}
}
