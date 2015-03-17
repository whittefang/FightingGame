using UnityEngine;
using System.Collections;


// script that positions explosion part of the trap move
public class FlamePillar : MonoBehaviour {
	public GameObject pillar;
	public float Yoffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDisable(){
		pillar.transform.position = new Vector3 (transform.position.x, transform.position.y + Yoffset, 0);
		pillar.SetActive(true);
	}
}
