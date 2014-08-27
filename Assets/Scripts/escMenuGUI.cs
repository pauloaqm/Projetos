using UnityEngine;
using System.Collections;

public class escMenuGUI : MonoBehaviour {
	bool menu = false;

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			menu = !menu;
			if (menu)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}
	}

	void OnGUI () {
		if (menu == true) {

			// Faz uma caixa que toma quase a tela toda
			GUI.Box(new Rect(20,20, Screen.width-40, Screen.height-40), "Pause Menu");
			
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(120,120, Screen.width-240, 50), "Reiniciar Level")) {
				Time.timeScale = 1;
				Application.LoadLevel(Application.loadedLevel);
			}

			if(GUI.Button(new Rect(120,200, Screen.width-240, 50), "Voltar ao jogo")) {
				Time.timeScale = 1;
				menu = !menu;

			}
			
			// Make the second button.
			if(GUI.Button(new Rect(120,280, Screen.width-240, 50), "Sair do jogo")) {
				Application.Quit();
			}
		}
	}

}
