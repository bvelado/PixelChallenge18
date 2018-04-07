using System.Collections;
using UnityEngine;

public class VegetableBurnable : MonoBehaviour {

    private bool _isBurning = false;
    public bool IsBurning { get { return _isBurning; } }

    [SerializeField] private GameObject _fireVFX;
    [SerializeField] private float _burnDuration = 5f;

    private Coroutine _burnCoroutine;

    public void BeginBurning()
    {
        _isBurning = true;
        _fireVFX.SetActive(true);
        _burnCoroutine = StartCoroutine(BurnCoroutine(_burnDuration));
    }

    public void Extinguish()
    {
        if (_isBurning)
        {
            StopCoroutine(_burnCoroutine);
        }
    }

    private IEnumerator BurnCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
