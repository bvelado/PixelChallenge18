using System.Collections.Generic;
using UnityEngine;

public class VegetablesLookup : MonoBehaviour {

    private List<Vegetable> _lookup = new List<Vegetable>();
    
    public void RegisterVegetable(Vegetable vegetable)
    {
        _lookup.Add(vegetable);
    }

    public void UnregisterVegetable(Vegetable vegetable)
    {
        _lookup.Remove(vegetable);
    }

    public void Cleanup()
    {
        _lookup.Clear();
    }

    public Vegetable[] GetVegetables()
    {
        return _lookup.ToArray();
    }

}
