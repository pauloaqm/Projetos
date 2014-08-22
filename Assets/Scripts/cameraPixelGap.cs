using UnityEngine;
using System.Collections;

public class cameraPixelGap : MonoBehaviour {
	private Transform player;
	public Vector2 maxXAndY = new Vector2 (503,500);		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

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
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
		transform.position = new Vector3(targetX,targetY, transform.position.z);
	}
}
