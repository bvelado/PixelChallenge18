using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BasketDropSpot : MonoBehaviour {

    private Basket _basket;
    private SphereCollider _trigger;

    private void Reset()
    {
        _trigger = GetComponent<SphereCollider>();

        _trigger.isTrigger = false;
        gameObject.isStatic = true;
    }

    private void Awake()
    {
        _basket = GetComponent<Basket>();
        _trigger = GetComponent<SphereCollider>();
    }

    public void TryDropVegetable(VegetableHoldable holdable)
    {
        var vegetable = holdable.GetComponent<Vegetable>();
        if (vegetable != null && _basket.CanDrop(vegetable))
        {
            _basket.Drop(vegetable);
        }
    }
}
