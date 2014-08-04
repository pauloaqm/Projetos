using UnityEngine;
using System.Collections;

public class SkyBG : MonoBehaviour {
	public Transform cameraPrincipal;
	Vector2 posicaoOriginalCeu;
	Vector2 posicaoOriginalCamera;
	Vector2 posicaoNovaCamera;
	public float offset = 1f;

	void Start () {
		transform.position = new Vector2(cameraPrincipal.position.x, cameraPrincipal.position.y);
		posicaoOriginalCeu = transform.position;
		posicaoOriginalCamera = Camera.main.transform.position;
	}
	

	void Update () {
		posicaoNovaCamera = Camera.main.transform.position;
		transform.position = new Vector2(transform.position.x+((posicaoNovaCamera.x-posicaoOriginalCamera.x)/offset), cameraPrincipal.position.y);

		posicaoOriginalCeu = transform.position;
		posicaoOriginalCamera = Camera.main.transform.position;
		//TODO repeticao da caixa de ceu
		/*if(transform.position)*/
	}
}
