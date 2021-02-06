using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoNave : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            GameObject.FindGameObjectWithTag("Marcador")
                      .GetComponent<Marcador>()
                      .SumarPuntuacion(enemigo.Puntuacion);
            enemigo.Destruirse();
            Destroy(gameObject);
        }

        if (other.tag == "Derecha")
            Destroy(gameObject);
    }
}
