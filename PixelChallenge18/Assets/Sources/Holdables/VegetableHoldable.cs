using UnityEngine;

public class VegetableHoldable : MonoBehaviour, IHoldable
{
    public event System.Action<VegetableHoldable> BeganBeingHeld;
    public event System.Action<VegetableHoldable> EndedBeingHeld;

    private CharacterHolder _holder;
    public CharacterHolder Holder { get { return _holder; } }

    private bool canBeHolded = true;

    public void OnBeginHold(CharacterHolder holder)
    {
        if (!canBeHolded)
        {
            holder.EndHold();
            return;
        }

        _holder = holder;

        transform.SetParent(holder.HoldRoot);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            rigidbody.GetComponent<Collider>().enabled = false;
        }

        if(BeganBeingHeld != null)
        {
            BeganBeingHeld.Invoke(this);
        }
    }

    public void OnEndHold(CharacterHolder holder)
    {
        transform.SetParent(null);

        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.GetComponent<Collider>().enabled = true;
        }

        TryToDropInBasket();

        _holder = null;

        if (EndedBeingHeld != null)
        {
            EndedBeingHeld.Invoke(this);
        }
    }

    private void TryToDropInBasket()
    {
        foreach(var collider in Physics.OverlapSphere(transform.position, 4f))
        {
            var basketDrop = collider.GetComponent<BasketDropSpot>();
            if (basketDrop != null)
            {
                basketDrop.TryDropVegetable(this);
                break;
            }
        }
    }

    public void SetHoldable(bool holdable)
    {
        canBeHolded = holdable;
    }
}
