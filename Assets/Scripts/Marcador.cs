using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Marcador : MonoBehaviour
{
    [SerializeField] Transform txtVidas;
    [SerializeField] Transform txtPuntuacion;

    public static byte Vidas { get; set; }
    public static int Puntuacion { get; set; }

    static private DateTime inicio;
    static private DateTime fin;

    void Start()
    {
    }

    void Update()
    {

    }

    public static int GetTiempoTotal()
    {
        /* Devuelve el tiempo transcurrido (seg.) desde el inicio del 
           nivel01 hasta la ultima destrucción de la nave */
        return (int) (inicio - fin).TotalSeconds;
    }

    public void RestarVida()
    {
        if (Vidas > 0)
        {
            Vidas--;

            string corazones = "";
            for (int i = 0; i < Vidas; i++)
                corazones += " <3";
            txtVidas.GetComponent<Text>().text = corazones.Trim();
        }

        else
            StartCoroutine(CargarCreditos());
    }

    public void SumarPuntuacion(int incremento)
    {
        Puntuacion += incremento;
        txtPuntuacion.GetComponent<Text>().text = 
            Puntuacion.ToString("0000") + " PTS.";
    }

    public static void Restablecer()
    {
        inicio = DateTime.Now;
        Vidas = 3;
        Puntuacion = 0;
    }

    private IEnumerator CargarCreditos()
    {
        fin = DateTime.Now;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Creditos");
    }
}
