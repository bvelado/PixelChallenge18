using System;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

    [SerializeField] private GameObject _modelRoot;

    [SerializeField] private Animator _animator;

    [Header("Animator parameters")]
    [SerializeField] private string _crouchedAnimatorParameter;

    internal void SetCrouched(bool crouch)
    {
        _animator.SetBool(_crouchedAnimatorParameter, crouch);
    }
}
