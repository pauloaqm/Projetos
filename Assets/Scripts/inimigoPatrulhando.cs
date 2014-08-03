using UnityEngine;
using System.Collections;

public class inimigoPatrulhando : MonoBehaviour {
	bool andandoEsquerda = true;
	float xMin;
	float xMax;
	public float velocidadePatrulha = 0.1f;
	bool noChao = false;
	public bool parado = false;

	void FixedUpdate(){
		if(parado == false)
			Patrulha();
	}

	void Patrulha(){
		Vector2 alvoEsquerda = (new Vector2 (xMin, transform.position.y));
		Vector2 alvoDireita = (new Vector2 (xMax,transform.position.y));
		if ((transform.position.x) - (xMin) > 0 && andandoEsquerda == true && noChao == true && parado == false)
			transform.position = Vector2.MoveTowards(transform.position, alvoEsquerda, velocidadePatrulha);

		if ((transform.position.x) - (xMax) < 0 && andandoEsquerda == false && noChao == true && parado == false)
			transform.position = Vector2.MoveTowards(transform.position, alvoDireita, velocidadePatrulha);

				
		if ((transform.position.x) - (xMin) < 0.5 && andandoEsquerda == true && noChao == true && parado ==false){
			Flip();
			andandoEsquerda = false;
		}
		if ((transform.position.x) - (xMax) > -0.5 && andandoEsquerda == false && noChao == true && parado == false){
			Flip();
			andandoEsquerda = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag != "Player"){
			xMin = coll.gameObject.renderer.bounds.min.x;
			xMax = coll.gameObject.renderer.bounds.max.x;
			xMin = (float)(xMin + 0.3);
			xMax = (float)(xMax - 0.3);
			noChao = true;
		}
		if(coll.gameObject.tag == "Player"){
			Debug.Log("entrou");
				transform.position = Vector2.MoveTowards(transform.position, transform.position,1f);
				parado = true;
				StartCoroutine (Esperar(1f));
				}
	}


		IEnumerator Esperar(float tempo) {
			noChao = false;
			rigidbody2D.velocity = Vector3.zero; //zera velocidades
			rigidbody2D.angularVelocity = 0f;
			
			//rigidbody2D.AddForce (new Vector2(5f,0));
			yield return new WaitForSeconds(tempo); //espera um determinado tempo
			noChao = true;
			parado = false;
	}
	


	void Flip (){
		Vector2 aEscala = transform.localScale;
		aEscala.x *= -1;
		transform.localScale = aEscala;
	}
}
