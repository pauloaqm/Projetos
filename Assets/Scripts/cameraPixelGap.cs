using UnityEngine;
using System.Collections;

public class cameraPixelGap : MonoBehaviour {
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float pixelsX = Mathf.Floor (player.position.x * 32);
		float pixelsY = Mathf.Floor (player.position.y * 32);
		float targetX = (pixelsX+0.5f)/32;
		float targetY = (pixelsY+0.5f)/32;
		transform.position = new Vector3(targetX,targetY, transform.position.z);
	}
}
