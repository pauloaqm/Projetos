using UnityEngine;
using System.Collections;

public class inimigoControllerRaycast : MonoBehaviour {
	
	private float knockback = 300f;
	private float danoBasico = 50f;
	public LayerMask camadaPlayer;

	void FixedUpdate() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.8f, camadaPlayer);
		if (hit.collider != null){
		GameObject jogador = GameObject.FindGameObjectWithTag("Player");
		jogador.GetComponent<KitControllerBasico>().Impulso(0, ConfiguracoesGlobais.forcaImpulsoInimigo);
		Morrer ();
		}
	}
	//Se a collision for com o player, ele checa a direçao relativa do player ao inimigo. Se a collision veio por cima do inimigo, 
	//ele sera destruido e o player ganha um impuslo. Se foi pelos lados ou por baixo, o player leva dano e um knockback na direçao oposta
	void OnCollisionEnter2D(Collision2D coll) {
			if (coll.gameObject.tag == "Player") {
				var posicaoRelativa = coll.contacts;
				GameObject jogador = GameObject.FindGameObjectWithTag("Player");
			Debug.Log ("apanhou");
				jogador.GetComponent<KitControllerBasico>().VariarLife ((ConfiguracoesGlobais.dificuldade * danoBasico)*-1);
				jogador.GetComponent<KitControllerBasico>().Impulso(knockback*(-posicaoRelativa[0].normal[0]), 0);
				}
		}
		

	void Morrer() {
		Destroy(gameObject);
	}
}