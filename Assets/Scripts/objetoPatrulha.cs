using UnityEngine;
using System.Collections;

public class objetoPatrulha : MonoBehaviour {
	[SerializeField] private bool andandoEsquerda = false;
	private bool andandoBaixo = false;
	private bool movePlayer = false;
	private float posInicialX;
	private float posInicialY;
	[SerializeField] private float posFinalX;
	[SerializeField] private float posFinalY;
	GameObject jogador;

	void Start () {
		if (andandoEsquerda == false) {
			posInicialX = transform.position.x;
			posFinalX = posInicialX + posFinalX;
		}else {
			posInicialX = transform.position.x - posFinalX;
			posFinalX = transform.position.x;

		}

		//TODO plataforma vertical
		posInicialY = transform.position.y;
		posFinalY = posInicialY + posFinalY;
		jogador = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		if ((transform.position.x <= posInicialX) && (andandoEsquerda == true))
			andandoEsquerda = false;
		else if ((transform.position.x >= posFinalX) && (andandoEsquerda == false))
			andandoEsquerda = true;
		else if (andandoEsquerda == false)
			transform.Translate(Vector2.right * Time.deltaTime);
		else if (andandoEsquerda == true)
			transform.Translate(-Vector2.right * Time.deltaTime);

		if ((transform.position.y <= posInicialY) && (andandoBaixo == true))
			andandoBaixo = false;
		else if ((transform.position.y >= posFinalY) && (andandoBaixo == false))
			andandoBaixo = true;
		else if (andandoBaixo == false)
			transform.Translate(Vector2.up * Time.deltaTime);
		else if (andandoEsquerda == true)
			transform.Translate(-Vector2.up * Time.deltaTime);

		if (movePlayer) {
			if (((Input.GetButton("Run")) && (Mathf.Abs(jogador.rigidbody2D.velocity.x) < 1f)) || (!Input.anyKey)){
				if (andandoEsquerda == false) 
					jogador.transform.Translate(Vector2.right * Time.deltaTime);				
				else
					jogador.transform.Translate(-Vector2.right * Time.deltaTime);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			movePlayer = true;				
			cameraFollow.xMargin = 0.01f;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			movePlayer = false;
			cameraFollow.xMargin = 1.0f;
		}
	}

}
