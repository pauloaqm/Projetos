using UnityEngine;
using System.Collections;

public class KitControllerBasico : MonoBehaviour {
	/****************************************************************
	 * 						DECLARACOES INICIAIS
	 * *************************************************************/
	//para que o personagem fique sem apanhar por um tempo apos apanhar
	private bool invencivel = false;
	private float tempoInvencivel;

	//limitacao de velocidade vertical
	public float vMaxY;

	//para movimento horizontal
	public float maxSpeed = 10f;
	bool olhandoDireita = true;

	//para correr horizontalmente enquanto segura um botao
	public float runSpeed = 15f;

	//para checar o chao pro pulo
	public Transform checaChao;
	bool noChao = false;
	float chaoRaio = 0.2f;
	public LayerMask ehChao;

	//para o pulo
	public float forcaPulo = 700f;

	//para animacoes
	private Animator anim;

	//para plataformas atravessaveis
	public LayerMask LayerPlataforma;
	bool naPlataforma = false;

	//para o life do player
	private float life = 100f;

	//PARA WALL JUMP
	bool tocandoParede = false;
	public Transform checaParede;
	float paredeRaio = 0.2f;
	public float forcaPuloParede = 500f;
	public bool parado = false;

	//PARA DOIS PULOS
	//bool puloDuplo = false;

	/******************************************************************/


	void Start () {
		anim = GetComponent<Animator>(); //inicializa o componente Animator do objeto ao qual esse script esta anexado.
	}



	void Update () {
		/****************************************************************
		 * 					INVENCIBILIDADE APOS APANHAR				*
		 * **************************************************************/
		if (invencivel){
			tempoInvencivel += Time.deltaTime; //faz piscar o renderizador
			if (tempoInvencivel < 3f) {
				float remainder = tempoInvencivel % .3f;
				renderer.enabled = remainder > .15f; 
			}
			else { // volta ao normal
				renderer.enabled = true;
				invencivel = false;
			}
		}
		/***************************************************************/

		/****************************************************************
		 * 						MOVIMENTO VERTICAL	parte 1				*
		 * **************************************************************/
		if (noChao && Input.GetButtonDown ("Jump")) {
						anim.SetBool ("noChao", false);
						rigidbody2D.AddForce (new Vector2 (0, forcaPulo));
				}

		/*---------PARA PLATAFORMAS ATRAVESSAVEIS----------*/
		if (naPlataforma && Input.GetButtonDown ("Jump") && Input.GetAxis("Vertical") >= 0) {
			anim.SetBool ("noChao", false);
			rigidbody2D.AddForce (new Vector2 (0, forcaPulo));
		}
		/*----------------PARA DOIS PULOS-------------------

		if ((noChao || !puloDuplo) && Input.GetButtonDown ("Jump")) {
			anim.SetBool ("noChao", false);
			rigidbody2D.AddForce (new Vector2 (0, forcaPulo));
		if (!puloDuplo && !noChao)
			puloDuplo = true;
		}*/

		//PARA WALL JUMP
		if (tocandoParede && (noChao == false) && (naPlataforma == false) && Input.GetButtonDown ("Jump")) //noChao tem que ser falso para evitar dois pulos acumulados
		{																		//quando o personagem estiver tocando em parede e chao ao mesmo tempo
			//pega o valor arredondado para inteiro do eixo horizontal
			float hori = Mathf.Floor(-Input.GetAxis("Horizontal"));

			if(hori!=0){ //se o eixo horizontal estiver sendo pressionado
				rigidbody2D.velocity = Vector3.zero; //zera velocidades
				rigidbody2D.angularVelocity = 0f; //zera velocidades
				parado = true; //retira o controle horizontal do personagem para nao interferir no wall jump
				StartCoroutine(Esperar(0.4f)); //espera um tempo para retornar o controle ao jogador
				rigidbody2D.AddForce (new Vector2(600*hori,forcaPulo)); //enquanto isso, aplica a forca do wall jump
				}
		}
		//---------------------------------------------------*/

	}
		/***************************************************************/

	IEnumerator Esperar(float tempo) {

		yield return new WaitForSeconds(tempo); //espera um determinado tempo
		anim.SetBool ("apanhou", false); //reseta a animaçao de paanhar para idle
		parado = false; //retorna o movimento horizontal
		GetComponent<CircleCollider2D>().enabled = true; //para uso nas plataformas atravessaveis
		GetComponent<BoxCollider2D>().enabled = true; 
	}



	void FixedUpdate () {
		//limitacao de velocidade vertical
		if(rigidbody2D.velocity.y < vMaxY)
			rigidbody2D.velocity = (new Vector2 (rigidbody2D.velocity.x, vMaxY));


		/****************************************************************
		 * 						MOVIMENTO VERTICAL	parte 2				*
		 * **************************************************************/
		//Physics2D.OverlapCircle pode ser substituido por OverlapCircleNonAlloc para melhorar performance, mas requer trabalho
		noChao = Physics2D.OverlapCircle(checaChao.position, chaoRaio, ehChao); //checaChao eh um gameobject criado para customizar facilmente a altura para checar se esta tocando no chao. o raioChao eh o raio para detectar a colisao. ehChao eh um campo no inspector para determinar uma camada na qual o chao eh sempre desenhado.
		anim.SetBool ("noChao", noChao); //coloca o valor da bool noChao dentro da bool noChao no animator
		anim.SetFloat ("velocidadeVertical", rigidbody2D.velocity.y); //dependendo da velocidade, muda entre as animacoes de pulo no animator

		/*----------------PARA DOIS PULOS-------------------
		 * - reseta o uso dos dois pulos
		//if (noChao)
			//puloDuplo = false;
		---------------------------------------------------*/


		/*-----------------PARA WALL JUMP------------------*/
		//usa o objeto checaParede para verificar se esta encostando em uma parede e pode dar o wall jump
		tocandoParede = Physics2D.OverlapCircle(checaParede.position, paredeRaio, ehChao);
		/*--------------------------------------------------*/
		/****************************************************************/




		/****************************************************************
		 * 						MOVIMENTO HORIZONTAL					*
		 * **************************************************************/

		if (!parado){ //se nao tiver parado por levar dano, pode se movimentar
			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Velocidade", Mathf.Abs (move));
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
			if (move > 0 && !olhandoDireita) //se estiver indo pra direita, mas olhando pra esquerda
				Flip (); //vira o personagem horizontalmente
			else if (move < 0 && olhandoDireita) //vice-versa
				Flip ();
			else if (move !=0 && Input.GetButton("Run")) //para correr quando segura um botao:
				rigidbody2D.velocity = new Vector2(move * runSpeed, rigidbody2D.velocity.y);
		}
		/****************************************************************/


		/****************************************************************
		 * 					PLATAFORMAS ATRAVESSAVEIS					*
		 * **************************************************************/
		//checa se esta pisando em uma plataforma
		naPlataforma = Physics2D.OverlapCircle(checaChao.position, chaoRaio, LayerPlataforma);
		//permite passar por dentro das plataformas se estiver subindo ou se nao ja estiver em uma (para evitar cair dela)
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), (LayerMask.NameToLayer("PlataformaAtravessavel")), (rigidbody2D.velocity.y > 0 || !naPlataforma));
		//liga a animacao de idle quando esta em pe na plataforma, para evitar ficar na animacao de caindo
		anim.SetBool ("naPlataforma", naPlataforma);
		//para descer de uma plataforma
		if(Input.GetAxis("Vertical") <= -0.5 && Input.GetButton("Jump") && naPlataforma){
			GetComponent<CircleCollider2D>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = false; 
			//rigidbody2D.AddForce (new Vector2(0,-20f));
			StartCoroutine(Esperar(0.2f)); //precisa esperar para que o personagem passe por dentro da plataforma por completo
			}
		/****************************************************************/
		}




		/*-----------PARA MUDAR O LADO QUE O PERSONAGEM OLHA-------*/
		void Flip() {
			olhandoDireita = !olhandoDireita; //inverte o estado da booleana, ja que agora o personagem esta invertido tb
			Vector3 aEscala = transform.localScale; //pega a scale do componente transform
			aEscala.x *= -1; //inver a scale
			transform.localScale = aEscala; //aplica no transform
		}
		/*---------------------------------------------------------*/
		
		



		/*****************************************************************
		 *	RECEBE FORCA NOS EIXOS X E Y E DA UM IMPULSO NO PLAYER
		*****************************************************************/
		//funcao para ser chamada pelos inimigos para dar knockback ou knockup
		public void Impulso(float x, float y){
			rigidbody2D.AddForce (new Vector2 (x,y));			
		}
		/****************************************************************/		
		
		
		
		
		
		/*****************************************************************
		* 				VARIAÇAO DE LIFE DO PLAYER
		* **************************************************************/
		
		public void VariarLife(float variacaoLife){
			

		if (variacaoLife < 0 && invencivel == false) { //se a variaçao for um dano e o player nao estiver invencivel por ter apanhado a pouco tempo
			life += variacaoLife;
			invencivel = true; //seta a invencibilidade
			tempoInvencivel = 0f; //zera o cronometro da invencibilidade
			parado = true; //impede movimento horizontal
			anim.SetBool ("apanhou", true); //play na animation de levar dano
			StartCoroutine (Esperar (0.3f)); //espera um tempo e volta ao normal
		}
		else if (variacaoLife > 0)
			life += variacaoLife;
		/*----------------PARA VERIFICAR LIFE--------------*/
		if (life <= 0)
			Debug.Log ("Game over. Life = " + life);
		//TODO Chamar tela de Game Over
		/*-------------------------------------------------*/
			//TODO Diminuir ou aumentar a barra de life ou tirar e colocar coraçoes
		}
		/****************************************************************/
}