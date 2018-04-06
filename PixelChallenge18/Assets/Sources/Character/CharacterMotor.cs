using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    [Header("Parameters")]
    [SerializeField] private float _moveSpeed = 1f;

    private Rigidbody _rigidbody;
    private CharacterInputs _inputs;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputs = GetComponent<CharacterInputs>();

        _inputs.LeftJoystickInputEmitted += HandleLeftJoystickInput;
    }

    private void HandleLeftJoystickInput(Vector2 input)
    {
        Move(input * _moveSpeed * Time.deltaTime);
    }

    public void Move(Vector2 velocity)
    {
        _rigidbody.velocity = (Vector3.right * velocity.x) + (Vector3.forward * velocity.y);
    }
}
