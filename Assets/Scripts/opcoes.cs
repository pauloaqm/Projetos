using UnityEngine;
using System.Collections;

public class opcoes : MonoBehaviour {		

	private Resolution recommended;
	bool fullScreen = true;
	private string curRes;
	private Rect botaoRes;
	private bool fullscreenToggle = true;
	private Resolution resAtual;
	private float resolutionPointer = 0f;
	private float resModificada = 0f;
	private float janelaOpcoesWidth = 520;
	private float janelaOpcoesHeight = 520;
	private Rect janelaOpcoes;
	
	void Start() {
		resAtual = Screen.currentResolution;
	}

	void OnGUI() {

		janelaOpcoes = new Rect ((Screen.width-janelaOpcoesWidth)/2, (Screen.height-janelaOpcoesHeight)/2, janelaOpcoesWidth, janelaOpcoesHeight);
		janelaOpcoes = GUI.Window (0, janelaOpcoes, WindowOpcoesFunction, "");


	}

	void WindowOpcoesFunction (int windowID) {
		fullScreen = GUILayout.Toggle(fullScreen, " Fullscreen");
		if (fullScreen != fullscreenToggle) {
			fullscreenToggle = fullScreen;
		}
		resolutionPointer=GUI.HorizontalSlider(new Rect(100, 150, 100, 30),resolutionPointer,0,Screen.resolutions.Length-1);
		
		if (resolutionPointer != resModificada) {
			resModificada = resolutionPointer;
		}
		
		GUI.Label(new Rect(100, 250, 100, 30),Screen.resolutions[(int)resModificada].width+"x"+Screen.resolutions[(int)resModificada].height);
		
		if(GUI.Button(new Rect(100, 350, 100, 30),"Aplicar")) {
			Screen.SetResolution(Screen.resolutions[(int)resolutionPointer].width,Screen.resolutions[(int)resolutionPointer].height,fullScreen);
			resAtual.width = Screen.resolutions[(int)resolutionPointer].width;
			resAtual.height = Screen.resolutions[(int)resolutionPointer].height;
		}
		
		curRes = resAtual.width+"x"+resAtual.height;
		GUILayout.Label("Resoluçao atual: "+curRes);
		
		if(GUILayout.Button("Voltar"))
			Application.Quit();
	}
}
