using UnityEngine;

public class CharacterCrouch : MonoBehaviour {

    private CharacterModel _model;
    private CharacterInputs _inputs;
    private CharacterMotor _motor;

    private bool _isCrouched = false;
    public bool IsCrouched { get { return _isCrouched; } }

    private void Awake()
    {
        _inputs = GetComponent<CharacterInputs>();
        _model = GetComponent<CharacterModel>();
        _motor = GetComponent<CharacterMotor>();
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
        _isCrouched = crouch;
        _motor.SetMovable(!crouch);
        if (crouch)
        {
            _model.SetCrouched();
        }
        else
        {
            _model.SetGetup();
        }
        
    }
}
