
using UnityEngine;

public class Character_Sound_Script : MonoBehaviour {


    public void LaunchSoundEvent(string eventKey)
    {
        AkSoundEngine.PostEvent(eventKey, gameObject);
    }

    public void Start()
    {
        AkSoundEngine.PostEvent("Play_Ono_Ready", gameObject);
        
    }

}
