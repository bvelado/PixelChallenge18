using UnityEngine;

public class VegetableModel : MonoBehaviour {

    [SerializeField] private Renderer _renderer;

#if UNITY_EDITOR
    private void Start()
    {
        Initialize(GetComponent<Vegetable>().PlayerData);
    }
#endif

    public void Initialize(PlayerData playerData)
    {
        _renderer.material.SetColor("_MaskColor", playerData.Color);
    }

}
