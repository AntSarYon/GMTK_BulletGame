using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamCoderTimer : MonoBehaviour
{
    [Header("Texto de Tiempo de Grabado")]
    [SerializeField] private TextMeshProUGUI txtRecordingTime;

    //Tiempo de Grabacion
    private float filmTime;

    //---------------------------------------------------------------------

    void Awake()
    {
        //El tiempo de grabación empieza en 0
        filmTime = 0;
    }

    //---------------------------------------------------------------------

    void Update()
    {
        //Incrementamos el Tiempo de grabacion cada frame
        filmTime += Time.deltaTime;

        txtRecordingTime.text = CalculateTimeInMiniutesSecondsFormat(filmTime);
    }

    //---------------------------------------------------------------------------

    private string CalculateTimeInMiniutesSecondsFormat(float timer)
    {
        //Hacemos conversion del Timer a Formato Min y Sec
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - (minutes * 60));

        string formatTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return formatTime;
    }
}
