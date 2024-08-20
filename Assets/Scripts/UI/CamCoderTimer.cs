using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamCoderTimer : MonoBehaviour
{
    [Header("Referencia a HealthController")]
    [SerializeField] private PlayerHealthController HealthController;

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
        //Si el jugador sigue vivo...
        if (HealthController.PlayerIsAlive)
        {
            //Incrementamos el Tiempo de grabacion cada frame
            filmTime += Time.deltaTime;

            //Asignamos el Tiempo que llevamos grabando -< Tmb al GameManager

            txtRecordingTime.text = CalculateTimeInMiniutesSecondsFormat(filmTime);
            
            //Dependiendo del flag del GameManager, calculamos el tiempo
            if (GameManager.Instance.takeRecordTime)
            {
                GameManager.Instance.currentRecordTime = txtRecordingTime.text;
            }
            
        }
        
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
