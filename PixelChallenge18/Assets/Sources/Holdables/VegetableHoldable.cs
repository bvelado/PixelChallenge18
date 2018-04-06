using UnityEngine;

public class VegetableHoldable : MonoBehaviour, IHoldable
{
    public event System.Action<CharacterHolder> BeganBeingHeld;
    public event System.Action<CharacterHolder> EndedBeingHeld;

    public void OnBeginHold(CharacterHolder holder)
    {
        Debug.Log("I'm being held by " + holder);
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
            BeganBeingHeld.Invoke(holder);
        }
    }

    public void OnEndHold(CharacterHolder holder)
    {
        Debug.Log("I'm being thrown by " + holder);
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

        if (EndedBeingHeld != null)
        {
            EndedBeingHeld.Invoke(holder);
        }
    }
}
