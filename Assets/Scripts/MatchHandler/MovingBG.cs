using UnityEngine;
using System.Collections;

// background moving based on offest algorithm
public class MovingBG : MonoBehaviour {

	public float scrollSpeed;
	
	void Start () {
	}
	
	void Update () {
		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (y, y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}
