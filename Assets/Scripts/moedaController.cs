using UnityEngine;
using System.Collections;

public class moedaController : MonoBehaviour {
	private Animator anim;
	public AudioClip somMoeda;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Player") {
			GetComponent<Collider2D>().enabled = false;
			GameObject jogador = obj.gameObject;
			jogador.GetComponent<KitControllerBasico>().variacaoMoedas(+1); 
			anim.SetBool("Desaparece", true);
			audio.PlayOneShot(somMoeda);
		}
	}

	void Desparecer(){
		Destroy(gameObject);
	}
}
