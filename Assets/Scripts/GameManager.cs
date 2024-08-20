using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public bool paused;

    public bool takeRecordTime;
    public string currentRecordTime;
    public float currentCinemaPercentage;

    public float MouseSensivility = 10;

    //----------------------------------------------------------------------------

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //Flag de tomar timepo record
        takeRecordTime = true;
    }

    //----------------------------------------------------------------------------

    public void Quit()
    {
        Application.Quit();
    }
}
