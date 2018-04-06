using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BasketDropSpot : MonoBehaviour {

    private Basket _basket;
    private SphereCollider _trigger;

    private void Reset()
    {
        _trigger = GetComponent<SphereCollider>();

        _trigger.isTrigger = true;
        gameObject.isStatic = true;
    }

    private void Awake()
    {
        _basket = GetComponent<Basket>();
        _trigger = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var holdable = other.GetComponentInParent<VegetableHoldable>();
        if (holdable != null)
        {
            holdable.EndedBeingHeld += TryDropVegetable;      
        }
    }

    private void TryDropVegetable(VegetableHoldable holdable)
    {
        var vegetable = holdable.GetComponent<Vegetable>();
        if (vegetable != null && _basket.CanDrop(vegetable))
        {
            _basket.Drop(vegetable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var holdable = other.GetComponentInParent<VegetableHoldable>();
        if (holdable != null)
        {
            holdable.EndedBeingHeld -= TryDropVegetable;
        }
    }
}
