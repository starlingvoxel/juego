using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour
{
    //Audio Source
    public Text resultado;

    //Velocidad de la pelota
    public float velocidad = 30.0f;

    //Audio Source
    AudioSource fuenteDeAudio;

    public GameObject pos;

    //Clips de audio
    public AudioClip

            audioGol,
            audioRaqueta,
            audioRebote,
            audioWin;

    //Contadores de goles
    public int golesIzquierda = 0;

    public int golesDerecha = 0;

    //Cajas de texto de los contadores
    public Text contadorIzquierda;

    public Text contadorDerecha;

    // Use this for initialization
    void Start()
    {
        /* Añade en el método Start() */
        //Desactivo la caja de resultado
        resultado.enabled = false;

        //Quito la pausa
        Time.timeScale = 1;

        //Velocidad inicial hacia la derecha
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

        //Recupero el componente audio source;
        fuenteDeAudio = GetComponent<AudioSource>();

        //Pongo los contadores a 0
        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();
    }

    //Se ejecuta si choco con la raqueta
    void OnCollisionEnter2D(Collision2D micolision)
    {
        //Si me choco con la raqueta izquierda
        if (micolision.gameObject.name == "RaquetaIzquierda")
        {
            //Valor de x
            int x = 1;

            //Valor de y
            int y =
                direccionY(transform.position, micolision.transform.position);

            //Vector de dirección
            Vector2 direccion = new Vector2(x, y);

            //Aplico la velocidad a la bola
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        } //Si me choco con la raqueta derechas
        else if (micolision.gameObject.name == "RaquetaDerecha")
        {
            //Valor de x
            int x = -1;

            //Valor de y
            int y =
                direccionY(transform.position, micolision.transform.position);

            //Vector de dirección
            Vector2 direccion = new Vector2(x, y);

            //Aplico la velocidad a la bola
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }

        //Para el sonido del rebote
        if (
            micolision.gameObject.name == "Arriba" ||
            micolision.gameObject.name == "Abajo"
        )
        {
            //Reproduzco el sonido del rebote
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }
    }

    //Calculo la dirección de Y
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    //Reinicio la posición de la bola
    public void reiniciarBola(string direccion)
    {
        //Posición 0 de la bola
        transform.position = Vector2.zero;

        //Vector2.zero es lo mismo que new Vector2(0,0);
        //Velocidad inicial de la bola
        //Velocidad y dirección
        if (direccion == "Derecha")
        {
            //Incremento goles al de la derecha
            golesDerecha++;

            //Reproduzco el sonido del gol
            fuenteDeAudio.clip = audioGol;

            fuenteDeAudio.Play();

            //Lo escribo en el marcador
            contadorDerecha.text = golesDerecha.ToString();

            /* Modifica y sustituye el método reiniciarBola en la comprobación de la dirección Derecha */
            //Reinicio la bola (si no ha llegado a 5)
            if (!comprobarFinal())
            {
                GetComponent<Rigidbody2D>().velocity =
                    Vector2.right * velocidad;

                //Vector2.right es lo mismo que new Vector2(1,0)
                //Vector2.right es lo mismo que new Vector2(1,0)
                velocidad = velocidad + 0.5f;
            }
        }
        else if (direccion == "Izquierda")
        {
            //Incremento goles al de la izquierda
            golesIzquierda++;

            //Lo escribo en el marcador
            contadorIzquierda.text = golesIzquierda.ToString();

            //Reproduzco el sonido del gol
            fuenteDeAudio.clip = audioGol;

            fuenteDeAudio.Play();

            /* Modifica y sustituye el método reiniciarBola en la comprobación de la dirección Izquierda */
            //Reinicio la bola (si no ha llegado a 5)
            if (!comprobarFinal())
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;

                //Vector2.left es lo mismo que new Vector2(-1,0)
                //Vector2.right es lo mismo que new Vector2(-1,0)
                velocidad = velocidad + 5f;
            }
        }
    }

    /* Añade en la declaración de variables */
    /* Añade en el nuevo método comprobarFinal(), que comprueba si he llegado al final del juego */
    //Compruebo si alguno ha llegado a 5 goles
    bool comprobarFinal()
    {
        //Si el de la izquierda ha llegado a 5
        if (golesIzquierda == 5)
        {
            fuenteDeAudio.clip = audioWin;
            fuenteDeAudio.Play();

            //Escribo y muestro el resultado
            resultado.text =
                "¡Jugador Izquierda GANA!\nPulsa ESC para volver a Inicio\nPulsa Enter para volver a jugar";

            //Muestro el resultado, pauso el juego y devuelvo true
            resultado.enabled = true;
            pos.SetActive(false);
            Time.timeScale = 0;

            //Pausa
            return true;
        } //Si el de le aderecha a llegado a 5
        else if (golesDerecha == 5)
        {
            fuenteDeAudio.clip = audioWin;
            fuenteDeAudio.Play();

            //Escribo y muestro el resultado
            resultado.text =
                "¡Jugador Derecha GANA!\nPulsa ESC para volver a Inicio\nPulsa Enter para volver a jugar";

            //Muestro el resultado, pauso el juego y devuelvo true
            resultado.enabled = true;

            Time.timeScale = 0; //Pausa
            return true;
        }
        else
        //Si ninguno ha llegado a 5, continúa el juego
        {
            return false;
        }
    }
}
