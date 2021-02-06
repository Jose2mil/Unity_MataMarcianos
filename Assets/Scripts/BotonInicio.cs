using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonInicio : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IniciarJuego()
    {
        Marcador.Restablecer();
        SceneManager.LoadScene("Nivel01");
    }
}
