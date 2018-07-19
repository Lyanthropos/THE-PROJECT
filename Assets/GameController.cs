using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public delegate void ForceUpdate (GameObject force, bool active);
    public static event ForceUpdate OnForceUpdate;

    private static GameController instance = null;

    public static GameController Instance
    {
        get { return instance; }
    }

    // Use this for initialization
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static void OnScreenForces(GameObject gameObject, bool active)
    {
        if(OnForceUpdate != null) {
            OnForceUpdate(gameObject, active);
        }
    }

}
