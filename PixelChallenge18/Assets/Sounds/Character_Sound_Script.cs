
using UnityEngine;

public class Character_Sound_Script : MonoBehaviour {


    public void LaunchSoundEvent(string eventKey)
    {
        AkSoundEngine.PostEvent(eventKey, gameObject);
    }
	
}
