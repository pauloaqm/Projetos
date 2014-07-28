using UnityEngine;
using System.Collections;

public class InimigoControllerBasico : MonoBehaviour {
	
	private float knockback = 300f;
	private float danoBasico = 50f;

	//Se a collision for com o player, ele checa a direçao relativa do player ao inimigo. Se a collision veio por cima do inimigo, 
	//ele sera destruido e o player ganha um impuslo. Se foi pelos lados ou por baixo, o player leva dano e um knockback na direçao oposta
	void OnCollisionEnter2D(Collision2D coll) {
			if (coll.gameObject.tag == "Player") {
				var posicaoRelativa = coll.contacts;

			if (posicaoRelativa[0].normal[1] < 0 ) {
				GameObject jogador = GameObject.FindGameObjectWithTag("Player");
				jogador.GetComponent<KitControllerBasico>().Impulso(0, ConfiguracoesGlobais.forcaImpulsoInimigo);
				Morrer ();
			}
			else {
				GameObject jogador = GameObject.FindGameObjectWithTag("Player");
				jogador.GetComponent<KitControllerBasico>().VariarLife ((ConfiguracoesGlobais.dificuldade * danoBasico)*-1);
				jogador.GetComponent<KitControllerBasico>().Impulso(knockback*(-posicaoRelativa[0].normal[0]), 0);
				}
		}
		
	}

	private void Morrer() {
		Destroy(gameObject);
	}
}