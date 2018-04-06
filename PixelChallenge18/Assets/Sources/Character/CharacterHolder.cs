using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    public Transform HoldRoot {  get { return _holdRoot; } }

    [SerializeField] private Bounds _grabRange;
    [SerializeField] private LayerMask _holdableLayerMask;
    [SerializeField] private Transform _holdRoot;

    private CharacterInputs _inputs;
    private CharacterMotor _motor;

    private bool _isHolding = false;
    private IHoldable _heldObject;

    private RaycastHit _hit;

    private void Awake()
    {
        _inputs = GetComponent<CharacterInputs>();
        _motor = GetComponent<CharacterMotor>();

        _inputs.HoldInputEmitted += OnHoldInputEmitted;
    }

    private void OnHoldInputEmitted(bool hold)
    {
        // Impossible cases?
        //if((_isHolding && hold) || (!_isHolding && !hold))
        //{
        //    return;
        //}

        if (hold && !_isHolding)
        {
            BeginHold();
        }
        else if (!hold && _isHolding)
        {
            EndHold();
        }
    }

    private void BeginHold()
    {
        var holdable = FindClosestHoldable();

        if(holdable != null)
        {
            _isHolding = true;
            _heldObject = holdable;
            holdable.OnBeginHold(this);
        }
    }

    private void EndHold()
    {
        var holdable = _heldObject;
        _isHolding = false;
        _heldObject = null;
        holdable.OnEndHold(this);
    }

    private IHoldable FindClosestHoldable()
    {
        var grabBoxCenter = transform.position + (transform.rotation * _grabRange.center) - (transform.forward * _grabRange.extents.z / 2f);
        Debug.DrawLine(grabBoxCenter, grabBoxCenter + Vector3.forward * 10f, Color.red, 2f);
        if(Physics.BoxCast(grabBoxCenter, _grabRange.extents / 2f, transform.forward, out _hit, transform.rotation, _grabRange.extents.z, _holdableLayerMask.value))
        {
            var item = _hit.collider.gameObject;

            Debug.Log(string.Format("Found {0} with a LayerMask value of {1}", item.name, _holdableLayerMask.value));

            if (item != null && item.GetComponent<IHoldable>() != null)
            {
                return item.GetComponent<IHoldable>();
            }
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawWireCube(_grabRange.center, _grabRange.size);
    }
}
