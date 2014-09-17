using UnityEngine;
using System.Collections;

public class stage1BossController : MonoBehaviour {
	private bool olhandoEsquerda = true;
	private GameObject player;
	private Animator anim;
	private bool agindo = false;
	public GameObject bolhas;
	private float knockback = 200f;
	private float danoBasico = 50f;


	void Start () {
		player = GameObject.Find ("player");
		anim = GetComponent<Animator> ();
	}

	//se nao estiver no meio de uma acao, escolhe uma acao para fazer aleatoriamente
	void Update () {
		if (agindo == false)
			Ataques(Mathf.FloorToInt(Random.Range(1f,3f)));
			agindo = true;
	}

	//adicione novos ataques aqui
	public void Ataques(int variacao){
		switch (variacao) {
		case 1:
			StartCoroutine(Charge (Random.Range (1f,2f))); //TODO qdo as animacoes estiverem prontas: chamar animacoes com eventos para chamar os metodos, ao inves de chamar o metodo diretamente
			break;
		case 2:
			StartCoroutine(Bolhas(Random.Range (1f,2f)));
			break;
		default:
			agindo = false;
			break;
		}
	}

	//ataque que solta tres bolhas em direcao ao player para fazer dano
	IEnumerator Bolhas(float tempo){
		yield return new WaitForSeconds (tempo);
		Vector2 spawnBolha = new Vector2 (transform.position.x - 2, transform.position.y);
		Instantiate(bolhas, spawnBolha, Quaternion.identity);
		yield return new WaitForSeconds (1f);
		Instantiate(bolhas, spawnBolha, Quaternion.identity);
		yield return new WaitForSeconds (1f);
		Instantiate(bolhas, spawnBolha, Quaternion.identity);
		agindo = false;
	}

	//ataque que causa um charge de um lado ao outro da tela
	IEnumerator Charge(float tempo){
		yield return new WaitForSeconds (tempo);
		float tempoInicio = Time.time;
		Vector2 alvoCharge = new Vector2 (transform.position.x-12f, transform.position.y);
		while (transform.position.x - alvoCharge.x > 0f){
			transform.position = Vector2.Lerp (transform.position, alvoCharge, (Time.time - tempoInicio)/5f);
			yield return null;
			if (transform.position.x - alvoCharge.x <=0)
				Flip();
		}

		//agindo = false;
	}

	//para o player apanhar quando entra em contato com o chefe
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
		}
	}

	void Flip() {
		olhandoEsquerda = !olhandoEsquerda; //inverte o estado da booleana, ja que agora o personagem esta invertido tb
		Vector3 aEscala = transform.localScale; //pega a scale do componente transform
		aEscala.x *= -1; //inver a scale
		transform.localScale = aEscala; //aplica no transform
	}
}
