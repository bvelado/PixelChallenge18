using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    [SerializeField] private Transform _holdRoot;
    public Transform HoldRoot { get { return _holdRoot; } }

    [SerializeField] private LayerMask _holdableLayerMask;
    [SerializeField] private Transform _raycastOrigin;
    [SerializeField] private float _raycastDistance = 0.5f;

    private CharacterInputs _inputs;
    private CharacterMotor _motor;

    private bool _isHolding = false;
    private IHoldable _heldObject;

    private RaycastHit _hit;

    private CharacterModel _model;

    private void Awake()
    {
        _inputs = GetComponent<CharacterInputs>();
        _motor = GetComponent<CharacterMotor>();
        _model = GetComponent<CharacterModel>();
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
            _model.SetPick();
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
        if(Physics.Raycast(_raycastOrigin.position, _raycastOrigin.forward, out _hit, _raycastDistance, _holdableLayerMask.value)) {
            return _hit.collider.GetComponentInParent<IHoldable>();
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_raycastOrigin.position, _raycastOrigin.position + _raycastOrigin.forward * _raycastDistance);
    }
}
