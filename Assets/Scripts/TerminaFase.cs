using UnityEngine;
using System.Collections;

public class TerminaFase : MonoBehaviour {

	public AudioClip somTerminaFase;

	void OnTriggerEnter2D(){
		GameObject jogador = GameObject.FindGameObjectWithTag("Player");
		Debug.Log ("WIN");
		jogador.GetComponent<KitControllerBasico>().Parado = true;
		jogador.rigidbody2D.velocity = Vector2.zero; //zera velocidades do player
		jogador.rigidbody2D.angularVelocity = 0f;
		audio.PlayOneShot(somTerminaFase);
		jogador.GetComponent<Animator> ().SetFloat ("Velocidade", 0f);
	}
}
