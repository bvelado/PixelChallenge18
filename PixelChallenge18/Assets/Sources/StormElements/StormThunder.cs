using System.Collections;
using System.Linq;
using UnityEngine;

public class StormThunder : MonoBehaviour {

    [SerializeField] private float _delayBetweenHit = 5f;
    [SerializeField] private GameObject _hintPrefab;
    [SerializeField] private GameObject _lightningPrefab;

    private VegetablesLookup _vegetableLookup;

    private void Awake()
    {
        _vegetableLookup = FindObjectOfType<VegetablesLookup>();
    }

    public void BeginStormThunder()
    {
        var rootedVegetables = _vegetableLookup.GetVegetables().Where(x => x.IsRooted).ToList();
        Vegetable[] targetVegetables = new Vegetable[2];
        for (int i = 0; i < targetVegetables.Length; i++)
        {
            var vegetable = rootedVegetables[Random.Range(0, rootedVegetables.Count)];
            rootedVegetables.Remove(vegetable);
            targetVegetables[i] = vegetable;
        }

        StartCoroutine(ThunderCoroutine(targetVegetables));
    }

    private IEnumerator ThunderCoroutine(Vegetable[] targets)
    {
        DisplayThunderHints(new[] { targets[0].transform.position + Vector3.up * 0.2f, targets[1].transform.position+ Vector3.up * 0.2f });
        yield return new WaitForSeconds(_delayBetweenHit);

        foreach(var target in targets)
        {
            if (target != null && target.IsRooted)
            {
                Destroy(Instantiate(_lightningPrefab, target.transform.position, Quaternion.identity, null), 5f);
                target.GetComponent<VegetableBurnable>().BeginBurning();
            }
        }
    }

    private void DisplayThunderHints(Vector3[] positions)
    {
        foreach(var position in positions)
        {
            Destroy(Instantiate(_hintPrefab, position, Quaternion.identity, null), 10f);
        }
    }
}
