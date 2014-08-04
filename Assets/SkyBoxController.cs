using UnityEngine;
using System.Collections;

public class SkyBoxController : MonoBehaviour {
		
		public float scrollSpeed = 0.0002f;
		private Vector2 savedOffset; //para resetar o offset (scroll) da textura quando desabilitar o play
		Vector2 posicaoOriginalCamera; //posicao da camera no frame anterior
		Vector2 posicaoNovaCamera; //posicao da camera no frame atual

		void Start () {
			savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex"); // guarda o offset (scroll da textura), para resetar quando tirar o play
			posicaoOriginalCamera = Camera.main.transform.position; //guarda a posicao da camera no primeiro frame do jogo
		}
		
		void Update () {
			posicaoNovaCamera = Camera.main.transform.position; //guarda a posicao do frame atual
			Vector2 offsetAtual = renderer.sharedMaterial.GetTextureOffset("_MainTex"); //guarda o valor offset da textura em uma variavel

			//agora precisamos construir um novo vetor para jogar no offset da textura e fazer ela dar o scroll.
			//ele eh composto de duas partes:
			//1 - offset atual da textura + uma velocidade de scroll - isso faz a textura dar scroll automaticamente, mesmo a camera estando parada, pois adiciona um valor fixo ao offset em cada frame
			//2 - a diferença entre a posicao atual da camera e a posicao no frame passado dividido por 100. Isso incrementa o scroll para se mover levando em conta tambem o movimento da camera
			Vector2 offset = new Vector2 (offsetAtual.x+scrollSpeed+((posicaoNovaCamera.x-posicaoOriginalCamera.x)/100),offsetAtual.y);
			//aplica o vetor acima ao offset da textura
			renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
			//depois de feito o movimento e finalizado tudo, guarda a posicao atual da camera como sendo a original, para usar no proximo frame
			posicaoOriginalCamera = Camera.main.transform.position;
		}
		
		void OnDisable () {
			//reseta o offset quando desliga o play
			renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
		}
	}