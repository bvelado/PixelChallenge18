﻿using UnityEngine;

public class TestHoldableItem : MonoBehaviour, IHoldable
{
    public void OnBeginHold(CharacterHolder holder)
    {
        Debug.Log("I'm being held by " + holder);
        transform.SetParent(holder.HoldRoot);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        var rigidbody = GetComponent<Rigidbody>();
        if(rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
        }
    }

    public void OnEndHold(CharacterHolder holder)
    {
        Debug.Log("I'm being thrown by " + holder);
        transform.SetParent(null);

        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
        }
    }
}
