using UnityEngine;

public class MakeCharacterFall : MonoBehaviour {

    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterMotor>().SetFalling(true);
        }
    }

}
