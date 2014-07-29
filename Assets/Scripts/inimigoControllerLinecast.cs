using UnityEngine;
using System.Collections;

public class inimigoControllerLinecast : MonoBehaviour {

	float xMin;
	float xMax;
	float yMax;
	Vector2 origemLinecast;
	Vector2 destinoLinecast;
	private float knockback = 300f;
	private float danoBasico = 50f;
	public LayerMask camadaPlayer;
	
	void FixedUpdate() {
		//as coordenadas para construir os vetores de inicio e fim do linecast. precisam ser menores do que as bordas do renderer-
		//porque o renderer eh bem maior do que o collider, mas precisam ser antes do collider, senao o player leva dano
		xMin = renderer.bounds.min.x+(float)0.20;
		xMax = renderer.bounds.max.x-(float)0.20;
		yMax = renderer.bounds.max.y -(float)0.20;
		//vetores de origem e destino do linecast construidos com as coordenadas ajustadas das bordas do renderer
		origemLinecast = (new Vector2 (xMin,yMax));
		destinoLinecast = (new Vector2 (xMax, yMax));
		//linecast em cima da cabeça do inimigo, checando apenas por colisoes com a camadaPlayer, que eh setada no Inspector
		RaycastHit2D hit = Physics2D.Linecast(origemLinecast, destinoLinecast, camadaPlayer);
		//se houver alguma colisao (com a camada setada na camadaPlayer)
		if (hit.collider != null){
			GameObject jogador = GameObject.FindGameObjectWithTag("Player"); //pega o jogador
			jogador.rigidbody2D.velocity = Vector2.zero; //zera velocidades do player
			jogador.rigidbody2D.angularVelocity = 0f;
			jogador.GetComponent<KitControllerBasico>().Impulso(0, ConfiguracoesGlobais.forcaImpulsoInimigo); //impulsiona ele pra cima, usando a funcao prevista no controller do player
			Morrer (); //mata o inimigo
		}
	}

	//quando o player colide com o inimigo
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") { //checa a tag de quem colidiu pra ver se eh player
			coll.rigidbody.velocity = Vector2.zero; //zera velocidades do player
			coll.rigidbody.angularVelocity = 0f;
			var posicaoRelativa = coll.contacts; //daqui em diante eh a parte que usa a normal para knockback 
			GameObject jogador = GameObject.FindGameObjectWithTag("Player");
			Debug.Log ("apanhou");
			jogador.GetComponent<KitControllerBasico>().VariarLife ((ConfiguracoesGlobais.dificuldade * danoBasico)*-1);
			jogador.GetComponent<KitControllerBasico>().Impulso(knockback*(-posicaoRelativa[0].normal[0]), 0);
		}
	}
	
	
	void Morrer() {
		Destroy(gameObject);
	}
}