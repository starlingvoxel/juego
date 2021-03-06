using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    AudioSource fuenteDeAudio;

    public AudioClip audioInicio;

    void Start()
    {
        //Recupero el componente audio source;
        fuenteDeAudio = GetComponent<AudioSource>();

        //Reproduzco el sonido del rebote
        fuenteDeAudio.clip = audioInicio;
        fuenteDeAudio.Play();
    }

    void Update()
    {
        //Si pulsa la tecla P o hace clic izquierdo empieza el juego
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0))
        {
            //Cargo la escena de Juego
            // Nombre de la scene del juego, en mi caso es SampleScene
            SceneManager.LoadScene("Juego");
            fuenteDeAudio.Pause();
        }

        //Si pulsa la tecla I vuelve al inicio
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            //Cargo la escena de Inicio
            SceneManager.LoadScene("Inicio");
            fuenteDeAudio.Play();
        }
    }
}
