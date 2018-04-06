using System;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

    [SerializeField] private GameObject _modelRoot;

    [SerializeField] private Animator _animator;

    [Header("Animator parameters")]
    [SerializeField] private string _crouchedAnimatorParameter;
    [SerializeField] private string _idleAnimatorParameter;
    [SerializeField] private string _runAnimatorParameter;

    public void SetCrouched(bool crouch)
    {
        //_animator.SetBool(_crouchedAnimatorParameter, crouch);
        //_animator.SetBool(_idleAnimatorParameter, false);
    }

    public void SetIdle()
    {
        _animator.SetBool(_idleAnimatorParameter, true);
        _animator.SetBool(_runAnimatorParameter, false);
    }

    public void SetRun()
    {
        _animator.SetBool(_idleAnimatorParameter, false);
        _animator.SetBool(_runAnimatorParameter, true);
    }
}
