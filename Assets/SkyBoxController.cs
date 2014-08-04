using UnityEngine;
using System.Collections;

public class SkyBoxController : MonoBehaviour {
		
		public float scrollSpeed;
		private Vector2 savedOffset;
		
		void Start () {
			savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");
		}
		
		void Update () {
			float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
			Vector2 offset = new Vector2 (x, savedOffset.y);
			renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
		}
		
		void OnDisable () {
			renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
		}
	}