using UnityEngine;

public class Vegetable : MonoBehaviour {

    private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } }

    private VegetableHoldable _holdable;
    private Rigidbody _rigidbody;
    

    private bool _isRooted = false;

    private void Awake()
    {
        _holdable = GetComponent<VegetableHoldable>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(PlayerData playerData)
    {
        _playerData = playerData;
        SetRooted(true);
    }

    private void SetRooted(bool rooted)
    {
        _isRooted = rooted;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        if (_isRooted)
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _rigidbody.GetComponent<Collider>().enabled = false;
        } else
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _rigidbody.GetComponent<Collider>().enabled = true;
        }
    }
}
