using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Vegetable"))
        {
            GameManager.s_Singleton.DestroyedVegetable(collider.GetComponent<Vegetable>().PlayerData.ID);
            Destroy(collider.gameObject);
        }
    }
}
