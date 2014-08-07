using UnityEngine;
using System.Collections;

public class escMenuGUI : MonoBehaviour {
	bool menu = false;

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape))
			menu = !menu;
	}

	void OnGUI () {
		if (menu == true) {
			// Faz uma caixa que toma quase a tela toda
			GUI.Box(new Rect(20,20, Screen.width-40, Screen.height-40), "Pause Menu");
			
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(120,120, Screen.width-240, 150), "Restart Level")) {
				Application.LoadLevel(Application.loadedLevel);
			}
			
			// Make the second button.
			if(GUI.Button(new Rect(120,300, Screen.width-240, 150), "Exit Game")) {
				Application.Quit();
			}
		}
	}

}
