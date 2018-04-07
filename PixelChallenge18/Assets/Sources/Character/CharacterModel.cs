using System;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

    [SerializeField] private GameObject _modelRoot;

    [SerializeField] private Animator _animator;

    [Header("Animator parameters")]
    [SerializeField] private string _diveAnimatorParameter;
    [SerializeField] private string _idleAnimatorParameter;
    [SerializeField] private string _runAnimatorParameter;
    [SerializeField] private string _pickAnimatorParameter;
    [SerializeField] private string _holdAnimatorParameter;
    [SerializeField] private string _victoryAnimatorParameter;
    [SerializeField] private string _kickAnimatorParameter;
    [SerializeField] private string _chuteAnimatorParameter;
    [SerializeField] private string _stunAnimatorParameter;
    [SerializeField] private string _windAnimatorParameter;

    public void SetCrouched()
    {
        _animator.SetBool(_diveAnimatorParameter, true);
    }

    public void SetGetup()
    {
        _animator.SetBool(_diveAnimatorParameter, false);
    }

    public void SetIdle()
    {
        _animator.SetBool(_idleAnimatorParameter, true);
        _animator.SetBool(_runAnimatorParameter, false);
        _animator.SetBool(_kickAnimatorParameter, false);
        _animator.SetBool(_chuteAnimatorParameter, false);
        _animator.SetBool(_stunAnimatorParameter, false);
    }

    public void SetRun()
    {
        _animator.SetBool(_idleAnimatorParameter, false);
        _animator.SetBool(_runAnimatorParameter, true);
        _animator.SetBool(_kickAnimatorParameter, false);
    }

    public void SetPick()
    {
        _animator.SetBool(_pickAnimatorParameter, true);
        _animator.SetBool(_holdAnimatorParameter, true);
    }

    public void SetLoose()
    {
        _animator.SetBool(_pickAnimatorParameter, false);
        _animator.SetBool(_holdAnimatorParameter, false);
    }

    public void SetVictory()
    {
        _animator.SetTrigger(_victoryAnimatorParameter);
    }

    public void SetChute()
    {
        _animator.SetBool(_chuteAnimatorParameter, true);
    }

    public void SetKick()
    {
        _animator.SetBool(_kickAnimatorParameter, true);
    }

    public void SetStun()
    {
        _animator.SetBool(_stunAnimatorParameter, true);
    }

    public void SetWind()
    {
        _animator.SetBool(_windAnimatorParameter, true);
    }

    public void SetWindFalse()
    {
        _animator.SetBool(_windAnimatorParameter, false);
    }
}
