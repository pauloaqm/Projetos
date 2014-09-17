using UnityEngine;
using System.Collections;

public class controllerBolha : MonoBehaviour {
	float danoBasico = 50f;
	float knockback = 200f;
	GameObject player;

	void Start(){
		player = GameObject.Find ("player");
		Vector2 direcaoPerseguir = player.transform.position - transform.position;
		rigidbody2D.AddForce (direcaoPerseguir * 37f);
	}

	void FixedUpdate(){
		rigidbody2D.AddForce (Vector2.up * 1f);
		if (rigidbody2D.velocity.y > 5f)
			rigidbody2D.velocity = (new Vector2 (rigidbody2D.velocity.x, 5f));
		if (transform.position.y > 20)
			Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") { //checa a tag de quem colidiu pra ver se eh player
			coll.rigidbody.velocity = Vector2.zero; //zera velocidades do player
			coll.rigidbody.angularVelocity = 0f;
			var posicaoRelativa = coll.contacts; //daqui em diante eh a parte que usa a normal para knockback 
			GameObject jogador = coll.gameObject;
			Debug.Log ("apanhou");
			Vector2 direcaoKnockback = coll.transform.position - transform.position;
			coll.rigidbody.AddForce(direcaoKnockback*knockback);
			jogador.GetComponent<KitControllerBasico>().VariarLife ((ConfiguracoesGlobais.dificuldade * danoBasico)*-1);
			//jogador.GetComponent<KitControllerBasico>().Impulso(knockback*(-posicaoRelativa[0].normal[1]), 0); // usa normal[1] para jogar o player para os lados e evitar que o player possa ficar em cima do peixe
			Destroy(gameObject);
		}
	}
}
