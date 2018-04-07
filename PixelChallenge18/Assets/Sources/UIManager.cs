using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject titleScreen;
    public GameObject creditsScreen;
    public GameObject selectionScreen;
    public GameObject gameScreen;
    public List<Transform> pZones;
    private Animator myAnim;
    public List<GameObject> stormFX;
    public GameObject winnerScreen;
    public List<Text> scoresText;

    private int pScoreIdx = 0;
    private bool onCredits = false;
    private bool onSelection = false;
    private List<bool> hasJoined = new List<bool>();

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
        myAnim = GetComponent<Animator>();

        for (var i = 0; i < 4; i ++)
        {
            hasJoined.Add(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("p1_cancel") && onCredits)
        {
            BackFromCredits();
        }

        if (Input.GetButtonDown("p1_kick") && onSelection && !hasJoined[0])
        {
            JoinPlayer(0);
        }
        else if (Input.GetButtonDown("p1_cancel") && onSelection && hasJoined[0])
        {
            RemovePlayer(0);
        }
        if (Input.GetButtonDown("p2_kick") && onSelection && !hasJoined[1])
        {
            JoinPlayer(1);
        }
        else if (Input.GetButtonDown("p2_cancel") && onSelection && hasJoined[1])
        {
            RemovePlayer(1);
        }
        if (Input.GetButtonDown("p3_kick") && onSelection)
        {
            JoinPlayer(2);
        }
        else if (Input.GetButtonDown("p3_cancel") && onSelection && hasJoined[2])
        {
            RemovePlayer(2);
        }
        if (Input.GetButtonDown("p4_kick") && onSelection)
        {
            JoinPlayer(3);
        }
        else if (Input.GetButtonDown("p4_cancel") && onSelection && hasJoined[3])
        {
            RemovePlayer(3);
        }

        if (Input.GetButtonDown("p1_start") && onSelection)
        {
            var j = 0;
            for (var i = 0; i < hasJoined.Count; i++)
            {
                if (hasJoined[i] == true)
                {
                    j++;
                }
            }
            if (j >= 2)
            {
                StartGame();
            }
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
        GameManager.s_Singleton.SpawnPlayer(idx);
        hasJoined[idx] = true;
    }

    void RemovePlayer(int idx)
    {
        pZones[idx].GetChild(0).gameObject.SetActive(true);
        pZones[idx].GetChild(1).gameObject.SetActive(false);
        hasJoined[idx] = false;
        GameManager.s_Singleton.DestroyPlayer(idx);
        for (var i = 0; i < hasJoined.Count; i++)
        {
            if (hasJoined[i] == true)
            {
                return;
            }
        }
        BackFromSelection();
    }

    void StartGame ()
    {
        onSelection = false;
        selectionScreen.SetActive(false);
        gameScreen.SetActive(true);
        myAnim.SetTrigger("ReadyGo");
        ActivateStormFX();
    }

    void ActivateStormFX()
    {
        foreach (var fx in stormFX)
        {
            fx.SetActive(true);
        }
    }

    void DectivateStormFX ()
    {
        foreach (var fx in stormFX)
        {
            fx.SetActive(false);
        }
    }

    public void DisplayAPlayerScore(int score, int pIdx)
    {
        if (!winnerScreen.activeSelf)
        {
            winnerScreen.SetActive(true);
        }
        scoresText[pScoreIdx].text = score.ToString();
        pScoreIdx++;
    }
}
