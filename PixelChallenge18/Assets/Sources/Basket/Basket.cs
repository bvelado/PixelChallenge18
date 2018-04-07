using UnityEngine;

public class Basket : MonoBehaviour {

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Transform _dropRoot;

    public event System.Action ItemDropped;

    private void Awake()
    {

    }

    public void Initialize()
    {

    }

    public bool CanDrop(Vegetable vegetable)
    {
        if(vegetable.PlayerData == _playerData)
        {
            return true;
        } 
        return false;
    }

    public void Drop(Vegetable vegetable)
    {
        if (vegetable.PlayerData != _playerData)
        {
            return;
        }

        vegetable.transform.position = _dropRoot.position;
        GameManager.s_Singleton.SecuredVegetable(_playerData.ID);
    }
}
