using UnityEngine;

public class Vegetable : MonoBehaviour {

    private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } }

    private VegetableHoldable _holdable;
    private Rigidbody _rigidbody;

    private bool _isRooted = false;
    public bool IsRooted { get { return _isRooted; } }

    private void Awake()
    {
        _holdable = GetComponent<VegetableHoldable>();
        _rigidbody = GetComponent<Rigidbody>();

        FindObjectOfType<VegetablesLookup>().RegisterVegetable(this);
    }

    private void OnDestroy()
    {
        var lookup = FindObjectOfType<VegetablesLookup>();
        if(lookup != null)
        {
            lookup.UnregisterVegetable(this);
        }
    }

    public void Initialize(PlayerData playerData)
    {
        _playerData = playerData;
        SetRooted(true);
    }

    public void SetRooted(bool rooted)
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
