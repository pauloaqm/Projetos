using UnityEngine;
using System.Collections;

public class titleScreen : MonoBehaviour {
	
	public GUISkin titleScreenGUI;
	private float janelaWidth = 250;
	private float janelaHeight = 180;
	private Rect janela, novoJogo, sair, opcoes;
	
	//Declaraçao do array que contem as opcoes do menu, do indice que acompanha qual opcao esta selecionada
	private string[] menuOptions = new string[3] {"Novo Jogo","Opções","Sair"};
	private int selectedIndex = 0; 
	
	//Variavel que vai dizer em qual opcao o mouse esta posicionado (hover)
	private string hover;
	
	
	private Resolution recommended;
	bool fullScreen = true;
	private string curRes;
	private Rect botaoRes;
	private bool fullscreenToggle = true;
	private Resolution resAtual;
	private float resolutionPointer = 0f;
	private float resModificada = 0f;
	private float janelaOpcoesWidth = 520;
	private float janelaOpcoesHeight = 230;
	private Rect janelaOpcoes;
	private bool opcoesAtivo = false;
	
	
	void Start() {
		resAtual = Screen.currentResolution;
		resolutionPointer = Screen.resolutions.Length-1;
	}
	
	
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
		
		//Da o foco para o item Opcoes se o mouse estiver em cima do botao
		if(hover=="Opcoes GUIContent"){
			selectedIndex = 1;
			GUI.FocusControl (menuOptions[selectedIndex]);
		}
		
		//Da o foco para o item Sair se o mouse estiver em cima do botao
		if(hover=="Sair GUIContent"){
			selectedIndex = 2;
			GUI.FocusControl (menuOptions[selectedIndex]);
		}
	}
	
	void OnGUI () {
		GUI.skin = titleScreenGUI;
		
		//Cria uma janela, no centro da tela, para os botoes
		janela = new Rect ((Screen.width-janelaWidth)/2, (Screen.height-janelaHeight)-40, janelaWidth, janelaHeight);
		janelaOpcoes = new Rect ((Screen.width-janelaOpcoesWidth)/2, (Screen.height-janelaOpcoesHeight)-40, janelaOpcoesWidth, janelaOpcoesHeight);
		
		if (opcoesAtivo)
			janelaOpcoes = GUI.Window (0, janelaOpcoes, WindowOpcoesFunction, "");
		else
			janela = GUI.Window (0, janela, WindowFunction, "");
		
		//da o foco para o item selecionado pelo teclado	
		GUI.FocusControl (menuOptions[selectedIndex]);
		
	}
	
	//Funcao que controla o que vai ser colocado na janela
	void WindowFunction (int windowID) {
		
		//Cria o botao Novo jogo 
		//O GUIContent serve para localizar o hover do mouse
		GUI.SetNextControlName ("Novo Jogo");
		novoJogo = new Rect ((janelaWidth-300)/2, 20, 300, 40);
		if (GUI.Button (novoJogo, new GUIContent ("Novo Jogo", "Novo Jogo GUIContent"))) {
			//Application.LoadLevel(1);
			CameraFade.StartAlphaFade( Color.black, false, 2f, 0f, () => { Application.LoadLevel(1); } );
		}
		
		//Cria o botao opcoes 
		//O GUIContent serve para localizar o hover do mouse
		GUI.SetNextControlName ("Opções");
		opcoes = new Rect ((janelaWidth-300)/2, 70, 300, 40);
		if (GUI.Button (opcoes, new GUIContent ("Opções", "Opcoes GUIContent"))) {
			opcoesAtivo = true;
		}
		
		//Cria o botao Sair 
		//O GUIContent serve para localizar o hover do mouse
		GUI.SetNextControlName ("Sair");
		sair = new Rect ((janelaWidth-300)/2, 120, 300, 40);
		if (GUI.Button (sair, new GUIContent ("Sair", "Sair GUIContent"))) {
			Application.Quit();
		}
		
		hover = GUI.tooltip;
	}
	
	void WindowOpcoesFunction (int windowID) {
		fullScreen = GUI.Toggle(new Rect(165, 70, 300, 40), fullScreen, " Fullscreen");
		if (fullScreen != fullscreenToggle) {
			fullscreenToggle = fullScreen;
		}
		GUI.Label(new Rect(50, 20, 110, 40),"Resolução: ");
		resolutionPointer=GUI.HorizontalSlider(new Rect(170, 28, 200, 40),resolutionPointer,0,Screen.resolutions.Length-1);
		
		if (resolutionPointer != resModificada) {
			resModificada = resolutionPointer;
		}
		
		//GUI.HorizontalSlider(new Rect(220, 258, 200, 40),1,0,16);
		
		GUI.Label(new Rect(390, 20, 220, 40),Screen.resolutions[(int)resModificada].width+"x"+Screen.resolutions[(int)resModificada].height);
		
		if(GUI.Button(new Rect((janelaOpcoesWidth-300)/2, 120, 300, 40),"Aplicar")) {
			Screen.SetResolution(Screen.resolutions[(int)resolutionPointer].width,Screen.resolutions[(int)resolutionPointer].height,fullScreen);
			resAtual.width = Screen.resolutions[(int)resolutionPointer].width;
			resAtual.height = Screen.resolutions[(int)resolutionPointer].height;
		}
		
		curRes = resAtual.width+"x"+resAtual.height;
		//GUILayout.Label("Resoluçao atual: "+curRes);
		
		sair = new Rect ((janelaOpcoesWidth-300)/2, 170, 300, 40);
		if(GUI.Button(sair,"Voltar"))
			opcoesAtivo = false;
	}
}
