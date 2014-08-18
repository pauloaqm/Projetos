﻿using UnityEngine;
using System.Collections;

public class titleScreen : MonoBehaviour {
	
	public GUISkin titleScreenGUI;
	private float janelaWidth = 520;
	private float janelaHeight = 520;
	private Rect janela, novoJogo, sair;

	//Declaraçao do array que contem as opcoes do menu, do indice que acompanha qual opcao esta selecionada
	private string[] menuOptions = new string[2] {"Novo Jogo","Sair"};
	private int selectedIndex = 0; 

	//Variavel que vai dizer em qual opcao o mouse esta posicionado (hover)
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
	
	void Update ()
	{
		//Chama a funcao que incrementa o item selecionado
		if (Input.GetKeyUp("s")) {

			selectedIndex = menuSelection(menuOptions, selectedIndex, "down");
		}

		//Chama a funcao que decrementa o item selecionado
		if (Input.GetKeyUp("w")) {

			selectedIndex = menuSelection(menuOptions, selectedIndex, "up");
		}

		//Da o foco para o item Novo Jogo se o mouse estiver em cima do botao
		if(hover=="Novo Jogo GUIContent") {
			selectedIndex = 0;
			GUI.FocusControl (menuOptions[selectedIndex]);
		}

		//Da o foco para o item Sair se o mouse estiver em cima do botao
		if(hover=="Sair GUIContent"){
			selectedIndex = 1;
			GUI.FocusControl (menuOptions[selectedIndex]);
		}
	}

	void OnGUI () {
		GUI.skin = titleScreenGUI;

		//Cria uma janela, no centro da tela, para os botoes
		janela = new Rect ((Screen.width-janelaWidth)/2, (Screen.height-janelaHeight)/2, janelaWidth, janelaHeight);
		janela = GUI.Window (0, janela, WindowFunction, "");

		//da o foco para o item selecionado pelo teclado	
		GUI.FocusControl (menuOptions[selectedIndex]);

	}

	//Funcao que controla o que vai ser colocado na janela
	void WindowFunction (int windowID) {

		//Cria o botao Novo jogo 
		//O GUIContent serve para localizar o hover do mouse
		GUI.SetNextControlName ("Novo Jogo");
		novoJogo = new Rect ((janelaWidth-300)/2, 200, 300, 70);
		if (GUI.Button (novoJogo, new GUIContent ("Novo Jogo", "Novo Jogo GUIContent"))) {
			//Application.LoadLevel(1);
			CameraFade.StartAlphaFade( Color.black, false, 2f, 0f, () => { Application.LoadLevel(1); } );
		}

		//Cria o botao Sair 
		//O GUIContent serve para localizar o hover do mouse
		GUI.SetNextControlName ("Sair");
		sair = new Rect ((janelaWidth-300)/2, 300, 300, 70);
		if (GUI.Button (sair, new GUIContent ("Sair", "Sair GUIContent"))) {
			Application.Quit();
		}

		hover = GUI.tooltip;
	}

	void loadLevelOnComplete( int levelIndex )
	{
		Application.LoadLevel(levelIndex);
	}
}
