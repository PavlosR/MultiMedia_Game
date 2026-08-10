using UnityEngine;

public class CutsceneManager : MonoBehaviour
{

    public void Play(MonoBehaviour script)
    {
        script.enabled = true;
    }

    public void Stop(MonoBehaviour script)
    {
        script.enabled = false;
    }

}
