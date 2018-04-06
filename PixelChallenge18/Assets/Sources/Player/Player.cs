using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private PlayerData _playerData;

    public PlayerData Data { get { return _playerData; } }

}
