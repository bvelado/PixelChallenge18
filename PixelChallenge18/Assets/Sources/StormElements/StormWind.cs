using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWind : MonoBehaviour {

    [SerializeField] private float _windPower = 1f;
    [SerializeField] private float _windEffectDuration = 3f;
    [SerializeField] private float _windEffectFrequency = 0.15f;

    public Transform windFX;
    public GameObject windsoundEmitter;
    private PlayersLookup _playersLookup;
    private VegetablesLookup _vegetablesLookup;
    private Coroutine _windEffectCoroutine;
    private WaitForSeconds _cachedWaitForSeconds;

    private void Awake()
    {
        _playersLookup = FindObjectOfType<PlayersLookup>();
        _vegetablesLookup = FindObjectOfType<VegetablesLookup>();
        _cachedWaitForSeconds = new WaitForSeconds(_windEffectFrequency);
    }

    public void BeginStormWind()
    {
        Vector2 windForce = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        windForce = windForce.normalized * _windPower;
                
        windFX.rotation = Quaternion.LookRotation(new Vector3(windForce.x, 0f, windForce.y));
        windFX.gameObject.SetActive(true);

        List<CharacterCrouch> players = new List<CharacterCrouch>();
        foreach(var player in _playersLookup.GetPlayers())
        {
            players.Add(player.GetComponent<CharacterCrouch>());
        }
        _windEffectCoroutine = StartCoroutine(WindEffectCoroutine(windForce, players.ToArray()));
        AkSoundEngine.PostEvent("Play_SFX_Wind_Gust", gameObject);
    }

    private IEnumerator WindEffectCoroutine(Vector2 windForce, CharacterCrouch[] players)
    {
        float timer = 0f;
        while(timer < _windEffectDuration)
        {
            foreach(var player in players)
            {
                if (!player.IsCrouched)
                {
                    player.GetComponent<CharacterMotor>().SetAdditionalVelocity(3 * windForce);
                    player.GetComponent<CharacterHolder>().EndHold();
                }
                else
                {
                    player.GetComponent<CharacterMotor>().SetAdditionalVelocity(Vector2.zero);
                }
            }

            foreach(var vegetable in _vegetablesLookup.GetVegetables())
            {
                var vegetableRigidbody = vegetable.GetComponent<Rigidbody>();
                if(vegetableRigidbody != null && !vegetableRigidbody.isKinematic)
                {
                    vegetableRigidbody.velocity += new Vector3(windForce.x, 0f, windForce.y);
                }
            }

            timer += _windEffectFrequency;
            yield return _cachedWaitForSeconds;
        }

        foreach (var player in players)
        {
            player.GetComponent<CharacterMotor>().SetAdditionalVelocity(Vector2.zero);
            
        }

        windFX.gameObject.SetActive(false);

        yield return null;
    }
}
