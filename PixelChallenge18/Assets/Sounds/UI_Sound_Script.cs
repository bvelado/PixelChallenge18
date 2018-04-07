using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sound_Script : MonoBehaviour {

public void OnHighlight ()
    {
        AkSoundEngine.PostEvent("Play_UI_Highlight", gameObject);
    }


    public void OnClicked()
    {
        AkSoundEngine.PostEvent("Play_UI_Click", gameObject);
    }

}
