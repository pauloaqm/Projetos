using UnityEngine;
using System.Collections;

public class inimigoPatrulhandoChecaChao : MonoBehaviour {
	bool andandoEsquerda = true;
	public Transform checaChaoPatrulha;
	public float velocidadePatrulha = 0.05f;
	float chaoRaio = 0.2f;
	bool noChao = false;
	public LayerMask ehChao;


	void FixedUpdate(){
		float esquerda = (transform.position.x)+10f;
		float direita = (transform.position.x)+10f;
		Vector2 andaEsquerda = new Vector2(esquerda,transform.position.y);
		Vector2 andaDireita = new Vector2(direita,transform.position.y);

		noChao = Physics2D.OverlapCircle(checaChaoPatrulha.position, chaoRaio, ehChao);
		if (noChao == true){ 
		    if (andandoEsquerda == true)
				transform.position = Vector2.MoveTowards(transform.position, -andaEsquerda, velocidadePatrulha);
			else
				transform.position = Vector2.MoveTowards(transform.position, andaDireita, velocidadePatrulha);
		}
		else if (noChao == false){
			andandoEsquerda = !andandoEsquerda;
			Flip();
		}
	}

	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Player")
			StartCoroutine (Esperar(1f));
	}
	
	
	IEnumerator Esperar(float tempo) {
		noChao = false;
		//rigidbody2D.AddForce (new Vector2(5f,0));
		yield return new WaitForSeconds(tempo); //espera um determinado tempo
		rigidbody2D.velocity = Vector3.zero; //zera velocidades
		rigidbody2D.angularVelocity = 0f;
		noChao = true;
	}
	
	
	
	void Flip (){
		Vector2 aEscala = transform.localScale;
		aEscala.x *= -1;
		transform.localScale = aEscala;
	}
}
