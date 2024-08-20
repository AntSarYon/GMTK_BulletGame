using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    private Animator mAnimator;
    private AudioSource mAudioSource;

    int animationIndex;

    //-----------------------------------------------------------------------

    void Awake()
    {
        mAnimator = GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();

        //Iniciamos con el indice de animacion en 1
        animationIndex = 1;
    }

    //-----------------------------------------------------------------------

    public void Continue()
    {
        //Incrementamos el indice de animacion
        animationIndex++;

        //Mientras el indice se mantenga por debajo o igual a 12
        if (animationIndex <= 12)
        {
            //Ejecutamos la Animacion correspondiente...
            mAnimator.Play(animationIndex.ToString());
        }
        //Si el indice ya paso el maximo
        else
        {
            //Ejecutamos el Skip...
            mAnimator.Play("Skip");
        }
        
    }

    //-----------------------------------------------------------------------

    public void Skip()
    {
        //Ejecutamos el Skip...
        mAnimator.Play("Skip");
    }

    //-----------------------------------------------------------------------

    public void StartGame()
    {
        //Cargamos la escena de juego
        SceneManager.LoadScene("Playground_Jaime");
    }

    //-----------------------------------------------------------------------

    public void PlayTypingSound()
    {
        mAudioSource.Play();
    }

    public void StopTypingSound()
    {
        mAudioSource.Stop();
    }
}
