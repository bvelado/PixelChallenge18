﻿using UnityEngine;

[CreateAssetMenu(menuName ="Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private string _id;
    public string ID { get { return _id; } }
    [SerializeField] private int _zeroBasedNumber;
    public int ZeroBasedNumber { get { return _zeroBasedNumber; } }
    [SerializeField] private Color _color;
    public Color Color { get { return _color; } }
}