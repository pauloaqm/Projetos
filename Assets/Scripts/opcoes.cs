using UnityEngine;
using System.Collections;

public class opcoes : MonoBehaviour {
		
	private Resolution[] resolutions;
	private Resolution recommended;
	bool fullScreen = true;
	private int posRes = 0;
	private int totalRes = 0;
	private string curRes;
	
	void Start() {
		
		// get all the resolutions that the machine can run
		resolutions = Screen.resolutions;
		totalRes = resolutions.Length;
		// get the desktop resolution and set as the resolution of the game in full screen mode (note that this will only work if the player start in the windowed mode)
		recommended = Screen.currentResolution;
		
		// Switch to the desktop screen resolution in fullscreen mode and adjusts the display info
		Screen.SetResolution (recommended.width, recommended.height, fullScreen);
		curRes = Screen.currentResolution.width+"x"+Screen.currentResolution.height;
		
	}
	
	void OnGUI() {
		
		GUILayout.Label("Actual Resolution: "+curRes);
		fullScreen = GUILayout.Toggle(fullScreen, " use fullscreen");
		if(GUILayout.Button("Change Resolution"))
		{
			if(posRes < totalRes)
				posRes++;
			else
				posRes = 0;
			
			Screen.SetResolution(resolutions[posRes].width, resolutions[posRes].height, fullScreen);
			curRes = resolutions[posRes].width+"x"+resolutions[posRes].height;
		}
		
		if(GUILayout.Button("Exit"))
			Application.Quit();
		
	}
}
