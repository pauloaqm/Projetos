using UnityEngine;
using System.Collections;

public class inimigoPatrulhandoChecaChao : MonoBehaviour {
	bool andandoEsquerda = true;
	public Transform checaChaoPatrulha;
	public float velocidadePatrulha = 0.05f;
	float chaoRaio = 0.2f;
	bool noChao = false;
	public LayerMask ehChao;
	bool bateuParede = false;
	public Transform checaParede;
	bool parado = false;


	void FixedUpdate(){
		if(parado == false){
			float esquerda = (transform.position.x)+10f;
			float direita = (transform.position.x)+10f;
			Vector2 andaEsquerda = new Vector2(esquerda,transform.position.y);
			Vector2 andaDireita = new Vector2(direita,transform.position.y);
			bateuParede = Physics2D.OverlapCircle(checaParede.position,0.2f,ehChao);//checa se bateu em uma parede pra poder voltar
			noChao = Physics2D.OverlapCircle(checaChaoPatrulha.position, chaoRaio, ehChao);//checa se esta tocando no chao, quando nao tocar mais, volta
			if (noChao == true){ 
			    if (andandoEsquerda == true)
					transform.position = Vector2.MoveTowards(transform.position, -andaEsquerda, velocidadePatrulha);
				else
					transform.position = Vector2.MoveTowards(transform.position, andaDireita, velocidadePatrulha);
			}
			if (noChao == false || bateuParede == true){ //se for cair da plataforma ou bater numa parede, muda de direcao
				andandoEsquerda = !andandoEsquerda;
				Flip();
				bateuParede = false;
			}
		}
	}

	
	void OnCollisionEnter2D(Collision2D coll) { //se colidir com o player, espera um pouco antes de continuar o movimento
		if(coll.gameObject.tag == "Player")
			StartCoroutine (Esperar(1.5f));
	}
	
	
	IEnumerator Esperar(float tempo) {
		noChao = false;
		parado = true;
		rigidbody2D.velocity = Vector3.zero; //zera velocidades
		rigidbody2D.angularVelocity = 0f;
		//rigidbody2D.AddForce (new Vector2(5f,0));
		yield return new WaitForSeconds(tempo); //espera um determinado tempo
		parado = false;
		noChao = true;
	}
	
	
	
	void Flip (){
		Vector2 aEscala = transform.localScale;
		aEscala.x *= -1;
		transform.localScale = aEscala;
	}
}
