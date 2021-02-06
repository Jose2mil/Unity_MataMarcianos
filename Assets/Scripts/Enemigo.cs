using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private int puntuacion = 5;
    public int Puntuacion { get { return puntuacion; } }
    [SerializeField] Vector2 cadenciaDisparo;

    // Atributos del disparo
    [SerializeField] Transform prefabDisparo;
    [SerializeField] float velocidadDisparo;

    [SerializeField] Transform particulasDestruccion;

    void Start()
    {
        StartCoroutine(Disparar());
    }

    void Update()
    {
    }

    IEnumerator Disparar()
    {
        float cadencia = UnityEngine.Random.Range(
                                                  cadenciaDisparo.x, 
                                                  cadenciaDisparo.y);

        yield return new WaitForSeconds(cadencia);
        Transform disparo = Instantiate(
                                        prefabDisparo, 
                                        transform.position, 
                                        Quaternion.identity);
        disparo.gameObject.GetComponent<Rigidbody2D>().velocity = 
            new Vector3(velocidadDisparo, 0, 0);

        // Bucle hasta que sea destruido
        StartCoroutine(Disparar());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arriba")
            transform.GetComponentInParent<BloqueEnemigo>()
                     .EmpezarABajar(transform.position.y);

        else if (other.tag == "Abajo")
            transform.GetComponentInParent<BloqueEnemigo>()
                     .EmpezarASubir(transform.position.y);

        if (other.tag == "Izquierda")
            Destroy(gameObject);

        if (other.tag == "DisparoNave")
        {
            transform.GetComponentInParent<BloqueEnemigo>()
                     .AumentarVelocidadY();

            Destroy(other.gameObject);
            Destruirse();
        }

        else if (other.tag == "Nave")
        {
            other.GetComponent<Nave>().Destruirse();
            Destruirse();
        }
    }

    public void Destruirse()
    {
        Instantiate(
                    particulasDestruccion, 
                    transform.position, 
                    Quaternion.identity);
        Destroy(gameObject);
    }
}
