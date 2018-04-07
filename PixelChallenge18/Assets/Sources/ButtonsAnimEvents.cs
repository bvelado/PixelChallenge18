using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAnimEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayClicked ()
    {
        UIManager.s_Singleton.OnClickPlay();
    }

    public void CreditsClicked()
    {
        UIManager.s_Singleton.OnClickCredits();
    }

    public void ExitClicked()
    {
        UIManager.s_Singleton.OnClickExit();
    }

    public void ReplayClicked()
    {
        UIManager.s_Singleton.OnClickReplay();
    }

    public void QuitClicked()
    {
        UIManager.s_Singleton.OnClickQuit();
    }
}
