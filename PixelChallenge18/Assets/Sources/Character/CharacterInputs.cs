using System;
using UnityEngine;

public sealed class CharacterInputs : MonoBehaviour {

    #region Input name consts
    /// All the following const are the generic input name
    /// They must be preceeded by the player id (pX_)

    /// <summary>
    /// Left joystick horizontal axis
    /// </summary>
    private const string LEFT_JOYSTICK_HORIZONTAL_AXIS = "horizontal";
    /// <summary>
    /// Left joystick vertical axis
    /// </summary>
    private const string LEFT_JOYSTICK_VERTICAL_AXIS = "vertical";
    /// <summary>
    /// Hold trigger axis
    /// Range 0...1
    /// </summary>
    private const string HOLD = "hold";
    /// <summary>
    /// Kick button
    /// </summary>
    private const string KICK = "kick";
    /// <summary>
    /// Crouch trigger axis
    /// Range 0...1
    /// </summary>
    private const string CROUCH = "crouch";
    #endregion

    private string _playerId = "p1_";

    #region Cached input name
    /// These are the actual name of the inputs used by this
    /// character. They must be filled using Initialize().
    private string _leftJoystickXAxisName;
    private string _leftJoystickYAxisName;
    private string _holdAxisName;
    private string _crouchAxisName;
    private string _kickButtonName;
    #endregion

    /// <summary>
    /// Left joystick input refreshed every Update frame
    /// </summary>
    private Vector2 _leftJoystickInput;
    /// <summary>
    /// Hold trigger input refreshed every Update frame
    /// </summary>
    private float _holdInput;

    private float _lastHoldInput;

    #region Input Events
    public event Action<Vector2> LeftJoystickInputEmitted;
    public event Action<bool> HoldInputEmitted;
    public event Action<bool> CrouchInputEmitted;
    public event Action KickEventEmitted;
    #endregion

#if UNITY_EDITOR
    private void Awake()
    {
        Initialize("p1_");
    }
#endif

    public void Initialize(string playerId)
    {
        _playerId = playerId;

        // LEFT JOYSTICK
        _leftJoystickXAxisName = string.Format("{0}{1}", _playerId, LEFT_JOYSTICK_HORIZONTAL_AXIS);
        _leftJoystickYAxisName = string.Format("{0}{1}", _playerId, LEFT_JOYSTICK_VERTICAL_AXIS);

        _holdAxisName = string.Format("{0}{1}", _playerId, HOLD);
        _crouchAxisName = string.Format("{0}{1}", _playerId, CROUCH);
        _kickButtonName = string.Format("{0}{1}", _playerId, KICK);
    }

    private void Update()
    {
        // LEFT JOYSTICK
        _leftJoystickInput = Vector2.zero;
        _leftJoystickInput.x = Input.GetAxis(_leftJoystickXAxisName);
        _leftJoystickInput.y = Input.GetAxis(_leftJoystickYAxisName);

        if(_leftJoystickInput.x != 0 || _leftJoystickInput.y != 0)
        {
            Debug.Log(string.Format("{0} emitted joystick input"));
            if(LeftJoystickInputEmitted != null)
            {
                LeftJoystickInputEmitted.Invoke(_leftJoystickInput);
            }
        }

        // KICK
        if (Input.GetButton(_kickButtonName))
        {
            Debug.Log(string.Format("{0} emitted kick input"));
            if(KickEventEmitted != null)
            {
                KickEventEmitted.Invoke();
            }
        }

        // HOLD
        _holdInput = Input.GetAxis(_holdAxisName);
        //if(_holdInput)
    }

}
