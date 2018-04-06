using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject titleScreen;
    public GameObject creditsScreen;
    public GameObject selectionScreen;
    public GameObject gameScreen;
    public List<Transform> pZones;

    private bool onCredits = false;
    private bool onSelection = false;

    public static UIManager s_Singleton;

    void Awake ()
    {
        if (s_Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            s_Singleton = this;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel") && onCredits)
        {
            BackFromCredits();
        }
        else if (Input.GetButtonDown("Cancel") && onSelection)
        {
            BackFromSelection();
        }

        if (Input.GetButtonDown("p2_kick") && onSelection)
        {
            JoinPlayer(1);
        }
        if (Input.GetButtonDown("p3_kick") && onSelection)
        {
            JoinPlayer(2);
        }
        if (Input.GetButtonDown("p4_kick") && onSelection)
        {
            JoinPlayer(3);
        }
    }

    public void OnClickPlay()
    {
        titleScreen.SetActive(false);
        selectionScreen.SetActive(true);
        onSelection = true;
        JoinPlayer(0);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickCredits()
    {
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
        onCredits = true;
    }

    public void BackFromCredits()
    {
        creditsScreen.SetActive(false);
        titleScreen.SetActive(true);
        onCredits = false;
    }

    public void BackFromSelection()
    {
        selectionScreen.SetActive(false);
        titleScreen.SetActive(true);
        onSelection = false;
    }

    void JoinPlayer (int idx)
    {
        pZones[idx].GetChild(0).gameObject.SetActive(false);
        pZones[idx].GetChild(1).gameObject.SetActive(true);
    }
}
