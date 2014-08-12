using UnityEngine;
//using UnityEditor;
using System.Collections;

public class AutoTileTexture : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDrawGizmos()
	{
		
		this.gameObject.renderer.sharedMaterial.SetTextureScale("_MainTex",new Vector2(this.gameObject.transform.lossyScale.x,this.gameObject.transform.lossyScale.y))  ;
	}
}