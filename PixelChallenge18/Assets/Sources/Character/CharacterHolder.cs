using UnityEngine;

public class CharacterHolder : MonoBehaviour
{
    [SerializeField] private Transform _holdRoot;
    public Transform HoldRoot { get { return _holdRoot; } }

    [SerializeField] private LayerMask _holdableLayerMask;
    [SerializeField] private Bounds _overlapBounds;

    private CharacterInputs _inputs;
    private CharacterMotor _motor;

    private bool _isHolding = false;
    public bool IsHolding { get { return _isHolding; } }
    private GameObject _heldObject;
    public GameObject HeldObject { get { return _heldObject; } }

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
        if (_isHolding)
        {
            return;
        }

        var holdable = FindClosestHoldable();

        if(holdable != null)
        {
            _isHolding = true;
            _heldObject = holdable;
            if(holdable != null)
            {
                holdable.GetComponentInParent<IHoldable>().OnBeginHold(this);
            }
            _model.SetPick();
        }
    }

    public void EndHold()
    {
        if (!_isHolding) {
            return;
        }

        var holdable = _heldObject;
        _isHolding = false;
        _heldObject = null;
        if(holdable != null)
        {
            holdable.GetComponentInParent<IHoldable>().OnEndHold(this);
        }        
        _model.SetLoose();
    }

    private GameObject FindClosestHoldable()
    {
        GameObject closest = null;
        foreach (var collider in Physics.OverlapBox(transform.position + transform.rotation*_overlapBounds.center, _overlapBounds.extents, transform.rotation, _holdableLayerMask.value))
        {
            var holdable = collider.GetComponentInParent<IHoldable>();
            if (holdable != null)
            {
                if(closest != null)
                {
                    if(Vector3.Distance(transform.position, collider.transform.position) < Vector3.Distance(transform.position, closest.transform.position)){
                        closest = collider.gameObject;
                    }
                } else
                {
                    closest = collider.gameObject;
                }
            }
        }

        //if(Physics.Raycast(_raycastOrigin.position, _raycastOrigin.forward, out _hit, _raycastDistance, _holdableLayerMask.value)) {
        //    return _hit.collider.GetComponentInParent<IHoldable>();
        //}

        return closest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(_overlapBounds.center, _overlapBounds.size);
    }
}
