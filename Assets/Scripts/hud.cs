using UnityEngine;
using System.Collections;

public class hud : MonoBehaviour {

	public GUIStyle moedaStyle;
	public Texture2D moedaImg;	
	public Texture2D lifeCheio;
	public Texture2D lifeVazio;

	void OnGUI () {

		//GUI.Label (new Rect (Screen.width-90,10,30,150), moedaImg, moedaStyle);
		GUI.DrawTexture(new Rect(Screen.width-130,10,50,50), moedaImg, ScaleMode.ScaleToFit, true, 0F);
		GUI.Label (new Rect (Screen.width-85,25,30,50),"x"+KitControllerBasico.moedas.ToString(), moedaStyle);

		if (KitControllerBasico.life/50 == 2) {
			GUI.DrawTexture(new Rect(30,10,40,40), lifeCheio, ScaleMode.ScaleToFit, true, 0F);
			GUI.DrawTexture(new Rect(70,10,40,40), lifeCheio, ScaleMode.ScaleToFit, true, 0F);
		}else if (KitControllerBasico.life/50 == 1) {
			GUI.DrawTexture(new Rect(30,10,40,40), lifeCheio, ScaleMode.ScaleToFit, true, 0F);
			GUI.DrawTexture(new Rect(70,10,40,40), lifeVazio, ScaleMode.ScaleToFit, true, 0F);
		} else {
			GUI.DrawTexture(new Rect(30,10,40,40), lifeVazio, ScaleMode.ScaleToFit, true, 0F);
			GUI.DrawTexture(new Rect(70,10,40,40), lifeVazio, ScaleMode.ScaleToFit, true, 0F);
		}

	}
}
