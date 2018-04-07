using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour
{

    [Header("Parameters")]
    [SerializeField] private float _moveSpeed = 1f;

    private Rigidbody _rigidbody;
    private CharacterInputs _inputs;
    private CharacterCrouch _crouch;

    private Vector3 _velocity;
    private bool _hasInputToProcess;
    private bool _movable = true;
    private bool _falling = false;

    private Vector2 _additionalVelocity;

    private RaycastHit _hit;

    public void Initialize()
    {
        _falling = false;
        _rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputs = GetComponent<CharacterInputs>();
        _crouch = GetComponent<CharacterCrouch>();

        _additionalVelocity = Vector2.zero;

        _inputs.LeftJoystickInputEmitted += HandleLeftJoystickInput;
    }

    private void HandleLeftJoystickInput(Vector2 input)
    {
        Move(input * _moveSpeed * Time.deltaTime);
        if (!Mathf.Approximately(input.magnitude, 0f) && !_crouch.IsCrouched)
        {
            UpdateOrientation(input);
        }
    }

    public void Move(Vector2 velocity)
    {
        if (_falling)
        {
            return;
        }

        _velocity = new Vector3(velocity.x, 0f, velocity.y);
        _hasInputToProcess = true;
    }

    private void FixedUpdate()
    {
        if (_falling)
        {
            _rigidbody.AddForce(Physics.gravity);
            return;
        }

        if (!_movable)
        {
            _hasInputToProcess = false;
            return;
        }

        if (_hasInputToProcess)
        {
            _rigidbody.velocity = _velocity;
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
        if (_falling)
        {
            return;
        }

        _movable = movable;

        if (!_movable)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void SetFalling(bool falling)
    {
        _falling = falling;

        if (falling)
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, 0.9f);
            _rigidbody.AddForce(Physics.gravity);
            _rigidbody.constraints -= RigidbodyConstraints.FreezePositionY;
        }
    }
}
