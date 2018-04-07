using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

    public List<GameObject> playersPrefab;
    public List<Transform> spawnPoints;
    public Map mapGenerated;
    public VegetablesLookup vegeLu;
    private List<GameObject> playersToDestroy = new List<GameObject>();
    public List<PlayerData> playersData;
    public GameObject lightningPrefab;
    public ParticleSystem rainInScene;
    public GameObject windInScene;
    private int nbVegetables = 72;
    private int stormSteps = 1;
    private List<int> scores = new List<int>();
    private List<int> vegePerPlayer = new List<int>();
    private Dictionary<int, int> playersScores = new Dictionary<int, int>();
    public int vpp = 2;
    public StormManager stormManager;
    public Light flashes;

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
    void Start()
    {
        playersToDestroy.Clear();
        for (var i = 0; i < 4; i++)
        {
            playersToDestroy.Add(null);
        }
    }

    public void InitGame ()
    {
        StartFlashes();
        playersScores.Clear();
        vegePerPlayer.Clear();
        for (var i = 0; i < 4; i++)
        {
            playersScores.Add(i, 0);
            vegePerPlayer.Add(vpp);
        }
        nbVegetables = 72;
        InitStorm();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartFlashes ()
    {
        var rndFlashes = Random.Range(5.0f, 14.0f);
        StartCoroutine("Flash", rndFlashes);
       
    }

    IEnumerator Flash (float gap)
    {
        var rnd = Random.Range(0.05f, 0.11f);
        yield return new WaitForSeconds(gap);
        flashes.enabled = true;
        yield return new WaitForSeconds(rnd);
        flashes.enabled = false;
        yield return new WaitForSeconds(rnd);
        flashes.enabled = true;
        yield return new WaitForSeconds(rnd);
        flashes.enabled = false;
        StartFlashes();

        AkSoundEngine.PostEvent("Play_SFX_Thunder_Crack_Random", gameObject);
    }


    public void SetupPause ()
    {
        for (var i = 0; i < 4; i++)
        {
            if (playersToDestroy[i] != null)
            {
                playersToDestroy[i].GetComponent<CharacterInputs>().enabled = false;
            }
        }
    }

    public void UnsetupPause()
    {
        for (var i = 0; i < 4; i++)
        {
            if (playersToDestroy[i] != null)
            {
                playersToDestroy[i].GetComponent<CharacterInputs>().enabled = true;
            }
        }
    }

    public void SpawnPlayer (int idx)
    {
        spawnedPlayer = Instantiate(playersPrefab[idx], spawnPoints[idx].position, spawnPoints[idx].rotation);
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
                vegePerPlayer[0] -= 1;
                break;
            case "p2_":
                idx = 1;
                vegePerPlayer[1] -= 1;
                break;
            case "p3_":
                idx = 2;
                vegePerPlayer[2] -= 1;
                break;
            case "p4_":
                idx = 3;
                vegePerPlayer[3] -= 1;
                break;
        }
        Debug.Log(playersToDestroy[idx]);
        playersToDestroy[idx].GetComponent<CharacterModel>().SetVictory();
        playersScores[idx] += 1;
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

    public void InitStorm()
    {
        stormManager.Initialize();
        stormManager.SetStormStep(EStormStep.Small);
    }

    public void StartStorm()
    {
        stormManager.BeginStorm();
    }

    public void StopStorm()
    {
        stormManager.EndStorm();
    }

    void TriggerStormStepOne()
    {
        var rainEmission = rainInScene.emission;
        rainEmission.rateOverTime = 250;
        stormSteps = 1;
        stormManager.SetStormStep(EStormStep.Small);
    }

    void TriggerStormStepTwo ()
    {
        var rainEmission = rainInScene.emission;
        rainEmission.rateOverTime = 2500;
        stormSteps = 2;
        stormManager.SetStormStep(EStormStep.Medium);
    }

    void TriggerStormStepThree()
    {
        var rainEmission = rainInScene.emission;
        rainEmission.rateOverTime = 12000;
        stormSteps = 3;
        stormManager.SetStormStep(EStormStep.Large);
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
        foreach (var player in playersToDestroy)
        {
            if (player != null)
            {
                player.SetActive(false);
            }
        }
        var items = from pair in playersScores
                    orderby pair.Value descending
                    select pair;
        foreach (KeyValuePair<int, int> pair in items)
        {
            UIManager.s_Singleton.DisplayAPlayerScore(pair.Value, pair.Key);
        }
    }

    public void Resetup ()
    {
        for (var i = 0; i < 4; i++)
        {
            if (playersToDestroy[i] != null)
            {
                playersToDestroy[i].transform.position = spawnPoints[i].position;
                playersToDestroy[i].SetActive(true);
            }
        }
        ResetMap();
        InitGame();
        TriggerStormStepOne();
    }

    public void RespawnPlayer (int idx)
    {
        playersToDestroy[idx].transform.position = spawnPoints[idx].position;
    }

    public void ClearScene()
    {
        for (var i = 0; i < 4; i++)
        {
            if (playersToDestroy[i] != null)
            {
                Destroy(playersToDestroy[i]);
            }
        }
        ResetMap();
        StopStorm();
    }

    void ResetMap ()
    {
        var veges = vegeLu.GetVegetables();
        for (var j = 0; j < veges.Length; j++)
        {
            Destroy(veges[j].gameObject);
        }
        mapGenerated.ClearMap();
        mapGenerated.GenerateMap();
    }
}
