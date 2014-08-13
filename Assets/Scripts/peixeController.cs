using UnityEngine;
using System.Collections;

public class peixeController : MonoBehaviour {
	//para dano e knockback no player
	float danoBasico = 50f;
	float knockback = 500f;

	//para a movimentacao do peixe
	public float alturaPulo;
	Vector2 posicaoInicial;
	Vector2 posicaoDestino;
	public float velocidadePulo = 0.07f;
	bool subindo = true;


	void Start(){
		posicaoInicial = new Vector2 (transform.position.x, transform.position.y);
		posicaoDestino = new Vector2 (posicaoInicial.x, (posicaoInicial.y + alturaPulo));
	}

	void Update(){
		//Debug.Log (gameObject.transform.position.y);
		if (subindo == true){
			transform.position = Vector2.MoveTowards(transform.position, posicaoDestino, velocidadePulo);
			if (transform.position.y >= posicaoDestino.y)
				subindo = false;
			}
		else if (subindo == false){
			transform.position = Vector2.MoveTowards(transform.position, posicaoInicial, velocidadePulo);
			if (transform.position.y <= posicaoInicial.y)
				subindo = true;//StartCoroutine (Esperar (2f));
			}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") { //checa a tag de quem colidiu pra ver se eh player
			coll.rigidbody.velocity = Vector2.zero; //zera velocidades do player
			coll.rigidbody.angularVelocity = 0f;
			var posicaoRelativa = coll.contacts; //daqui em diante eh a parte que usa a normal para knockback 
			GameObject jogador = coll.gameObject;
			Debug.Log ("apanhou");
			jogador.GetComponent<KitControllerBasico>().VariarLife ((ConfiguracoesGlobais.dificuldade * danoBasico)*-1);
			jogador.GetComponent<KitControllerBasico>().Impulso(knockback*(-posicaoRelativa[0].normal[0]), 0);
		}
	}

	IEnumerator Esperar(float tempo){
		yield return new WaitForSeconds(tempo); //espera um determinado tempo
		subindo = true;
		//Debug.Log ("esperar");
	}
}
