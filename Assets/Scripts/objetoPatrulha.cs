using UnityEngine;
using System.Collections;

public class objetoPatrulha : MonoBehaviour {
	private bool andandoEsquerda = false;
	private bool andandoBaixo = false;
	private bool movePlayer = false;
	private float xMin;
	private float yMin;
	[SerializeField] private float xMax;
	[SerializeField] private float yMax;
	GameObject jogador;

	void Start () {
		xMin = transform.position.x;
		yMin = transform.position.y;
		xMax = xMin + xMax;
		yMax = yMin + yMax;
		jogador = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		if ((transform.position.x <= xMin) && (andandoEsquerda == true))
			andandoEsquerda = false;
		else if ((transform.position.x >= xMax) && (andandoEsquerda == false))
			andandoEsquerda = true;
		else if (andandoEsquerda == false)
			transform.Translate(Vector2.right * Time.deltaTime);
		else if (andandoEsquerda == true)
			transform.Translate(-Vector2.right * Time.deltaTime);

		if ((transform.position.y <= yMin) && (andandoBaixo == true))
			andandoBaixo = false;
		else if ((transform.position.y >= yMax) && (andandoBaixo == false))
			andandoBaixo = true;
		else if (andandoBaixo == false)
			transform.Translate(Vector2.up * Time.deltaTime);
		else if (andandoEsquerda == true)
			transform.Translate(-Vector2.up * Time.deltaTime);

		if (movePlayer) {
			if (((Input.GetKey(KeyCode.LeftShift)) && (Mathf.Abs(jogador.rigidbody2D.velocity.x) < 1f)) || (!Input.anyKey)){
				if (andandoEsquerda == false)
					jogador.transform.Translate(Vector2.right * Time.deltaTime);
				else
					jogador.transform.Translate(-Vector2.right * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") 
			movePlayer = true;				
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player")
			movePlayer = false;
	}

}
