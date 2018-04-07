using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private PlayerData _playerData;

    public PlayerData Data { get { return _playerData; } }

    public void Initialize (PlayerData playerData)
    {
        _playerData = playerData;

        FindObjectOfType<PlayersLookup>().RegisterPlayer(this);

        string switchState = "SW_Player_0" + (playerData.ZeroBasedNumber+1).ToString();
        AkSoundEngine.SetSwitch("SW_Player", switchState, GetComponentInChildren<Character_Sound_Script>().gameObject);
    }

    private void OnDestroy()
    {
        FindObjectOfType<PlayersLookup>().UnregisterPlayer(this);
    }
}
