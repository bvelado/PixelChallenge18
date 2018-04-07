using System.Collections;
using UnityEngine;

public enum EStormStep
{
    Small,
    Medium,
    Large
}

public class StormManager : MonoBehaviour {

    [SerializeField] private StormWind _wind;

    [Header("Parameters")]
    [SerializeField] private float _delayBetweenBucketAndThunder = 5f;

    [Header("Frequencies")]
    [SerializeField] private float _smallWindFrequency = 16f;
    [SerializeField] private float _mediumWindFrequency = 12f;
    [SerializeField] private float _largeWindFrequency = 18f;

    [SerializeField] private float _smallThunderFrequency = 24f;
    [SerializeField] private float _mediumThunderFrequency = 18f;
    [SerializeField] private float _largeThunderFrequency = 12f;

    private EStormStep _currentStep = EStormStep.Small;
    private float _currentWindFrequency = 16f;
    private float _currentThunderFrequency = 24f;
    private float _lastWind = 0f;
    private float _lastThunder = 0f;
    private bool _isStorming = false;
    
    public void Initialize()
    {
        SetStormStep(EStormStep.Small);
    }

    public void BeginStorm()
    {
        _isStorming = true;
    }

    public void EndStorm()
    {
        _isStorming = false;
    }

    public void SetStormStep(EStormStep step)
    {
        _currentStep = step;
        switch (_currentStep)
        {
            case EStormStep.Small:
                _currentWindFrequency = _smallWindFrequency;
                _currentThunderFrequency = _smallThunderFrequency;
                break;
            case EStormStep.Medium:
                _currentWindFrequency = _mediumWindFrequency;
                _currentThunderFrequency = _mediumThunderFrequency;
                break;
            case EStormStep.Large:
                _currentWindFrequency = _largeWindFrequency;
                _currentThunderFrequency = _largeThunderFrequency;
                break;
        }

        // Resets the timers
        _lastWind = 0f;
        _lastThunder = 0f;
    }

    private void Update()
    {
        if (_isStorming)
        {
            if (_lastWind > _currentWindFrequency)
            {
                TriggerWind();
                _lastWind = 0f;
            }

            if (_lastThunder > _currentThunderFrequency)
            {
                TriggerThunder();
                _lastThunder = 0f;
            }

            _lastWind += Time.deltaTime;
            _lastThunder += Time.deltaTime;
        }
    }

    private void TriggerWind()
    {
        _wind.BeginStormWind();
    }

    private void TriggerThunder()
    {

    }
}
