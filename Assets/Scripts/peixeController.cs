using UnityEngine;
using System.Collections;

public class peixeController : MonoBehaviour {
	//para dano e knockback no player
	float danoBasico = 50f;
	float knockback = 500f;

	private Animator anim;

	//para a movimentacao do peixe
	public float alturaPulo;
	Vector2 posicaoInicial;
	Vector2 posicaoDestino;
	public float velocidadePulo = 0.1f;
	public float velocidadeVolta = 0.13f;
	bool subindo = true; //o peixe comeca subindo e tem essa booleana invertida toda vez que eh rodada a coroutine Esperar.
	bool esperando = false;


	void Start(){
		posicaoInicial = new Vector2 (transform.position.x, transform.position.y);
		posicaoDestino = new Vector2 (posicaoInicial.x, (posicaoInicial.y + alturaPulo));
		anim = GetComponent<Animator> ();
		anim.SetBool("Subindo", true);
	}

	void Update(){
		//se o peixe estiver subindo e nao estiver no meio da coroutine Esperar, move-se em direcao a posicao de destino:
		if (subindo == true && esperando == false){
			anim.SetBool("Subindo", true);
			anim.SetBool("Descendo", false);
			transform.position = Vector2.MoveTowards(transform.position, posicaoDestino, velocidadePulo);
			//ao chegar (ou passar) da posicao de destino, seta a bool de que esta esperando e comeca a coroutine Esperar. Ao final da Coroutine,-
			//a bool esperando eh setada devolta para false, permitindo que o inimigo volte a se mover:
			if (transform.position.y >= posicaoDestino.y){
				esperando = true;
				StartCoroutine(Esperar (1f));
			}
		}
		//se o peixe nao estiver subindo nem na coroutine Esperar, move-se de volta para a posicao inicial
		else if (subindo == false && esperando == false){
			anim.SetBool("Subindo", false);
			anim.SetBool("Descendo", true);
			transform.position = Vector2.MoveTowards(transform.position, posicaoInicial, velocidadeVolta);
			//quando chega ou passa da posicao inicial, roda a coroutine Esperar e inverte a scale.y do peixe. Na subida isso eh feito-
			//dentro da coroutine Esperar, para evitar que o peixe vire de cabeca para baixo antes de comecar a descer:
			if (transform.position.y <= posicaoInicial.y){
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*(-1), transform.localScale.z);
				esperando = true;
				StartCoroutine (Esperar (2f));
			}
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
		//seta a bool 'Descendo' para falso, para que o peixe volte para a animation idle. No animator a transicao de descendo para idle eh feita-
		//quando essa bool eh falsa:
		anim.SetBool("Descendo", false);
		//espera um determinado tempo
		yield return new WaitForSeconds(tempo); 
		//se a coroutine foi chamada na subida do peixe, agora inverte a scale.y para o peixe apontar para baixo. Isso soh eh feito na subida, pq-
		//queremos que o peixe fique parado olhando para cima, antes de descer olhando para baixo.
		if (subindo == true)
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*(-1), transform.localScale.z);
		//inverte a bool subindo, fazendo o peixe mudar de direcao de movimento
		subindo = !subindo;
		//a bool abaixo controla quando o peixe pode se mover. todo o movimento esta condicionado a essa bool ser false:
		esperando = false;
	}
}
