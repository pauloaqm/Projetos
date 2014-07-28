using UnityEngine;
using System.Collections;

public class PlayerImpulsoVertical : MonoBehaviour {

		/*****************************************************************
		 * 				IMPULSO DE PULO APOS PISAR EM ALGO
		 * **************************************************************/
		
		public void Impulso(float impulso){
			rigidbody2D.AddForce (new Vector2 (0, impulso));
		}
}
