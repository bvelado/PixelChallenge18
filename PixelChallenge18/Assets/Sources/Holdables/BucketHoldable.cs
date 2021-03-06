﻿using System.Collections.Generic;
using UnityEngine;

public class BucketHoldable : MonoBehaviour, IHoldable
{
    public event System.Action<BucketHoldable> BeganBeingHeld;
    public event System.Action<BucketHoldable> EndedBeingHeld;

    public void OnBeginHold(CharacterHolder holder)
    {
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

        TryToExtinguishFire();

        if (EndedBeingHeld != null)
        {
            EndedBeingHeld.Invoke(this);
        }
    }

    private void TryToExtinguishFire()
    {
        bool extinguishedSomething = false;
        Debug.Log("Extinguish");
        foreach(var collider in Physics.OverlapSphere(transform.position, 5f))
        {
            var vegetableBurnable = collider.GetComponentInParent<VegetableBurnable>();
            if (vegetableBurnable != null && vegetableBurnable.IsBurning)
            {
                vegetableBurnable.Extinguish(); extinguishedSomething = true;
            }
        }
        if (extinguishedSomething)
        {
            Destroy(gameObject);
        }
        
    }
}
