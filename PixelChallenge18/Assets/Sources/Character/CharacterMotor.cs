using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    [Header("Parameters")]
    [SerializeField] private float _moveSpeed = 1f;

    private Rigidbody _rigidbody;
    private CharacterInputs _inputs;

    private Vector2 _velocity;
    private bool _hasInputToProcess;
    private bool _movable = true;

    private Vector2 _additionalVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputs = GetComponent<CharacterInputs>();

        _additionalVelocity = Vector2.zero;

        _inputs.LeftJoystickInputEmitted += HandleLeftJoystickInput;
    }

    private void HandleLeftJoystickInput(Vector2 input)
    {
        Move(input * _moveSpeed * Time.deltaTime);
        if(!Mathf.Approximately(input.magnitude, 0f)){
            UpdateOrientation(input);
        }
    }

    public void Move(Vector2 velocity)
    {
        _velocity = velocity;
        _hasInputToProcess = true;
    }

    private void FixedUpdate()
    {
        if(!_movable)
        {
            _hasInputToProcess = false;
            return;
        }

        if (_hasInputToProcess)
        {
            _rigidbody.velocity = new Vector3(_velocity.x + _additionalVelocity.x, _rigidbody.velocity.y, _velocity.y + _additionalVelocity.y);
            _hasInputToProcess = false;
        }
    }

    private void UpdateOrientation(Vector2 orientation)
    {
        _rigidbody.transform.rotation = Quaternion.LookRotation(new Vector3(orientation.x, 0f, orientation.y), Vector3.up);
    }

    public void SetAdditionalVelocity(Vector2 force)
    {
        _additionalVelocity = force;
    }

    public void SetMovable(bool movable)
    {
        _movable = movable;

        if (!_movable)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
