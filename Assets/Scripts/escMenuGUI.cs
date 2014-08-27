using UnityEngine;
using System.Collections;

public class escMenuGUI : MonoBehaviour {

	private bool menu = false;
	public GUISkin titleScreenGUI;
	private float janelaWidth = 300;
	private float janelaHeight = 180;
	private Rect janela, reiniciarFase, sairJogo, voltarJogo;
	private string[] menuOptions = new string[3] {"Reiniciar Fase","Voltar ao jogo","Sair do jogo"};
	private int selectedIndex = 0; 
	private string hover;

	//Funcao que incrementa ou decrementa o item selecionado de acordo com a direcao (cima ou baixo) apertada pelo teclado
	int menuSelection (string[] menuItems,int  selectedItem, string direction) {
		if (direction == "up") {
			if (selectedItem == 0) {
				selectedItem = menuItems.Length - 1;
			} else {
				selectedItem -= 1;
			}
		}
		
		if (direction == "down") {
			if (selectedItem == menuItems.Length - 1) {
				selectedItem = 0;
			} else {
				selectedItem += 1;
			}
		}
		
		return selectedItem;
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			menu = !menu;
			if (menu)  
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}
		if (menu)  {
			if (Input.GetKeyUp("s")) {
				
				selectedIndex = menuSelection(menuOptions, selectedIndex, "down");
			}
			
			//Chama a funcao que decrementa o item selecionado
			if (Input.GetKeyUp("w")) {
				
				selectedIndex = menuSelection(menuOptions, selectedIndex, "up");
			}
			
			//Da o foco para o item Novo Jogo se o mouse estiver em cima do botao
			if(hover=="Reiniciar Fase GUIContent") {
				selectedIndex = 0;
				GUI.FocusControl (menuOptions[selectedIndex]);
			}
			
			//Da o foco para o item Opcoes se o mouse estiver em cima do botao
			if(hover=="Voltar ao jogo GUIContent"){
				selectedIndex = 1;
				GUI.FocusControl (menuOptions[selectedIndex]);
			}
			
			//Da o foco para o item Sair se o mouse estiver em cima do botao
			if(hover=="Sair do jogo GUIContent"){
				selectedIndex = 2;
				GUI.FocusControl (menuOptions[selectedIndex]);
			}
		}
	}
	
	void OnGUI () {
		if (menu == true) {
			GUI.skin = titleScreenGUI;

			janela = new Rect ((Screen.width-janelaWidth)/2, (Screen.height-janelaHeight)/2, janelaWidth, janelaHeight);
			janela = GUI.Window (0, janela, janelaFunc, "");

			GUI.FocusControl (menuOptions[selectedIndex]);
		}
	}

	void janelaFunc (int windowID) {

		GUI.SetNextControlName ("Reiniciar Fase");
		reiniciarFase = new Rect (((janelaWidth - 300) / 2) + 10, 20, 280, 40);
		if(GUI.Button(reiniciarFase, new GUIContent ("Reiniciar Fase", "Reiniciar Fase GUIContent"))) {
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}

		GUI.SetNextControlName ("Voltar ao jogo");
		voltarJogo = new Rect (((janelaWidth - 300) / 2) + 10, 70, 280, 40);
		if(GUI.Button(voltarJogo, new GUIContent ("Voltar ao jogo", "Voltar ao jogo GUIContent"))) {
			Time.timeScale = 1;
			menu = !menu;
			
		}

		GUI.SetNextControlName ("Sair do jogo");
		sairJogo = new Rect (((janelaWidth - 300) / 2) + 10, 120, 280, 40);
		if(GUI.Button(sairJogo, new GUIContent ("Sair do jogo", "Sair do jogo GUIContent"))) {
			Application.Quit();
		}

		hover = GUI.tooltip;
	}


}
