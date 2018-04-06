using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public List<Transform> spawnPoints;
    public List<PlayerData> playersData;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayer (int idx)
    {
        spawnedPlayer = Instantiate(playerPrefab, spawnPoints[idx].position, spawnPoints[idx].rotation);
        spawnedPlayer.GetComponent<Player>().Initialize(playersData[idx]);
    }
}
