using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private PlayerData _playerData;

    public PlayerData Data { get { return _playerData; } }

    public void Initialize (PlayerData playerData)
    {
        _playerData = playerData;

        FindObjectOfType<PlayersLookup>().RegisterPlayer(this);
    }
}
