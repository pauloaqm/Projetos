using UnityEngine;
using System.Collections;

public class SkyBG : MonoBehaviour {
	public Transform cameraPrincipal;

	void Start () {
		transform.position = new Vector2(cameraPrincipal.position.x, cameraPrincipal.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(cameraPrincipal.position.x, cameraPrincipal.position.y);
	}
}
