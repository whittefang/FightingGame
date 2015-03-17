using UnityEngine;
using System.Collections;

// simple sound script that can be called to play different sounds
public class SoundScript : MonoBehaviour {
	public AudioClip hit;
	public AudioClip block;
	public AudioClip fireball;
	public AudioClip grab;
	public AudioClip Parry;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playHit(){
		GetComponent<AudioSource>().PlayOneShot (hit);
	}
	public void playParry(){
		GetComponent<AudioSource>().PlayOneShot (Parry);
	}
	public void playBlock(){
		GetComponent<AudioSource>().PlayOneShot (block);
	}
	public void playFireball(){
		GetComponent<AudioSource>().PlayOneShot (fireball);
	}
	public void playGrab(){
		GetComponent<AudioSource>().PlayOneShot (grab);
	}
}
