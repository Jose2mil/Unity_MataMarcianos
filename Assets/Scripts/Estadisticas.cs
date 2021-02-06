using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estadisticas : MonoBehaviour
{
    [SerializeField] Transform tiempoTotal;
    [SerializeField] Transform puntuacionTotal;

    void Start()
    {
        MostrarInformacion();
    }

    void Update()
    {
        
    }

    private void MostrarInformacion()
    {
        tiempoTotal.GetComponent<Text>().text = 
            Marcador.GetTiempoTotal().ToString("0000").Substring(1) + " S.";
        puntuacionTotal.GetComponent<Text>().text = 
            Marcador.Puntuacion.ToString("0000") + " PTS.";
    }
}
