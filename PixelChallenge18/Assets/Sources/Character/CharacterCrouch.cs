using UnityEngine;

public class CharacterCrouch : MonoBehaviour {

    private CharacterModel _model;
    private CharacterInputs _inputs;

    private void Awake()
    {
        _inputs = GetComponent<CharacterInputs>();
        _model = GetComponent<CharacterModel>();
    }

    private void OnEnable()
    {
        _inputs.CrouchInputEmitted += OnCrouchInputEmitted;
    }

    private void OnDisable()
    {
        _inputs.CrouchInputEmitted -= OnCrouchInputEmitted;
    }

    private void OnCrouchInputEmitted(bool crouch)
    {
        if (crouch)
        {
            _model.SetCrouched();
        }
        else
        {
            _model.SetGetup();
        }
        AkSoundEngine.PostEvent("Play_Ono_Crouch", gameObject);
    }
}
