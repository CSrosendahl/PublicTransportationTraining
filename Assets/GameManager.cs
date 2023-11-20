using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public bool hasCheckedIn;

    void Start()
    {
        hasCheckedIn = false;
        Debug.Log("If you can see this, the game manager is working");
    }


}
