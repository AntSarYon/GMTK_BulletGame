using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    private Animator mAnimator;
    int animationIndex;

    //----------------------------------------------------

    void Awake()
    {
        mAnimator = GetComponent<Animator>();

        //Iniciamos con el indice de animacion en 1
        animationIndex = 1;
    }

    //----------------------------------------------------

    public void Continue()
    {
        //Incrementamos el indice de animacion
        animationIndex++;

        //Mientras el indice se mantenga por debajo o igual a 12
        if (animationIndex <= 3)
        {
            //Ejecutamos la Animacion correspondiente...
            mAnimator.Play(animationIndex.ToString());
        }
        //Si el indice ya paso el maximo
        else
        {
            //Ejecutamos el Skip...
            mAnimator.Play("FadeIn");
        }
    }

    //----------------------------------------------------

    public void StartGame()
    {
        //Cargamos la escena de juego
        SceneManager.LoadScene("Playground_Jaime");
    }

}
