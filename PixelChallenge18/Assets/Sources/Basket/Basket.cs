﻿using UnityEngine;

public class Basket : MonoBehaviour {

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Transform _dropRoot;

    private int _count = 0;

    public event System.Action ItemDropped;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Clear();
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
        AkSoundEngine.PostEvent("Play_SFX_Veggi_Success", gameObject);

        GameManager.s_Singleton.SecuredVegetable(_playerData.ID);
        Destroy(vegetable.gameObject);
        
        _count++;
        UpdateView();
        

    }

    private void UpdateView()
    {
        _dropRoot.GetChild(_count - 1).gameObject.SetActive(true);
    }

    public void Clear()
    {
        _count = 0;
        for(int i = 0; i < _dropRoot.childCount; i++)
        {
            _dropRoot.GetChild(i).gameObject.SetActive(false);
        }
    }
}
