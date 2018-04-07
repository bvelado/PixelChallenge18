using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public List<Transform> spawnPoints;
    public List<GameObject> playersToDestroy = new List<GameObject>();
    public List<PlayerData> playersData;
    public GameObject lightningPrefab;
    public ParticleSystem rainInScene;
    public GameObject windInScene;
    private int nbVegetables = 72;
    private int stormSteps = 1;
    private List<int> scores = new List<int>();
    private List<int> vegePerPlayer = new List<int>();
    private Dictionary<int, int> playersScores = new Dictionary<int, int>();

    private GameObject spawnedPlayer;

    public static GameManager s_Singleton;

    void Awake()
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
		for (var i = 0; i < 4; i++)
        {
            playersToDestroy.Add(null);
            scores.Add(0);
            playersScores.Add(i, 0);
            vegePerPlayer.Add(18);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayer (int idx)
    {
        spawnedPlayer = Instantiate(playerPrefab, spawnPoints[idx].position, spawnPoints[idx].rotation);
        spawnedPlayer.GetComponent<Player>().Initialize(playersData[idx]);
        playersToDestroy[idx] = spawnedPlayer;
    }

    public void DestroyPlayer (int idx)
    {
        Destroy(playersToDestroy[idx]);
        playersToDestroy[idx] = null;
    }

    public void DestroyedVegetable (string vegeId)
    {
        nbVegetables--;
        switch (vegeId)
        {
            case "p1_":
                vegePerPlayer[0] -= 1;
                break;
            case "p2_":
                vegePerPlayer[1] -= 1;
                break;
            case "p3_":
                vegePerPlayer[2] -= 1;
                break;
            case "p4_":
                vegePerPlayer[3] -= 1;
                break;
        }
        CheckStormState();
        CheckEndGame();
    }

    public void SecuredVegetable(string pId)
    {
        var idx = -1;
        switch (pId)
        {
            case "p1_":
                idx = 0;
                break;
            case "p2_":
                idx = 1;
                break;
            case "p3_":
                idx = 2;
                break;
            case "p4_":
                idx = 3;
                break;
        }
        scores[idx] += 1;
        nbVegetables--;
        CheckStormState();
        CheckEndGame();
    }

    void CheckStormState ()
    {
        if (nbVegetables <= 56 && nbVegetables > 32 && stormSteps == 1)
        {
            TriggerStormStepTwo();
        }
        else if (nbVegetables <= 32 && nbVegetables >= 0 && stormSteps == 2)
        {
            TriggerStormStepThree();
        }
    }

    void TriggerStormStepTwo ()
    {
        var rainEmission = rainInScene.emission;
        rainEmission.rateOverTime = 2500;
        stormSteps = 2;
    }

    void TriggerStormStepThree()
    {
        var rainEmission = rainInScene.emission;
        rainEmission.rateOverTime = 12000;
        stormSteps = 3;
    }

    void CheckEndGame()
    {
        for (var i = 0; i < 4; i++)
        {
            if (vegePerPlayer[i] == 0)
            {
                SendScores();
            }
        }
    }

    void SendScores ()
    {
        var sortedScores = playersScores.Values.ToList();
        sortedScores.Sort();
        sortedScores.Reverse();
        foreach (var value in sortedScores)
        {
            UIManager.s_Singleton.DisplayAPlayerScore(value, sortedScores[value]);
        }
    }
}
