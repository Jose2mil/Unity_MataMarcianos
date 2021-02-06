using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BloqueEnemigo : MonoBehaviour
{
    [SerializeField] float velocidadX;
    [SerializeField] float velocidadY;
    [SerializeField] float aumentoVelocidadYPorMuerte;
    [SerializeField] float velocidadYMaxima;
    public float VelocidadY { get; }

    protected Vector2 posicionInicial;
    protected bool avanzando = false;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        MoverEnemigos();

        if (transform.childCount == 0)
            SceneManager.LoadScene("Nivel02");
    }

    protected void MoverEnemigos()
    {
        transform.Translate(0, velocidadY * Time.deltaTime, 0);
    }

    public void EmpezarASubir(float y)
    {
        velocidadY = Math.Abs(velocidadY);
        if(!avanzando)
            StartCoroutine(Avanzar(y));
    }

    public void EmpezarABajar(float y)
    {
        velocidadY = - Math.Abs(velocidadY);
        if (!avanzando)
            StartCoroutine(Avanzar(y));
    }

    protected virtual IEnumerator Avanzar(float y)
    {
        avanzando = true;
        transform.Translate(velocidadX, 0, 0);

        /* Se espera 0.1s para evitar que varios enemigos que se 
           encuentren en una misma fila colisionen y den un salto*/
        yield return new WaitForSeconds(0.1f);
        avanzando = false;
    }

    public void AumentarVelocidadY()
    {
        if(Math.Abs(velocidadY) < Math.Abs(velocidadYMaxima))
            velocidadY += velocidadY > 0? 
                                aumentoVelocidadYPorMuerte : 
                                -aumentoVelocidadYPorMuerte;
    }

    public virtual void ReajustarPosicion()
    {
        transform.position = posicionInicial;

        List<GameObject> disparoEnemigos = 
            GameObject.FindGameObjectsWithTag("DisparoEnemigo").ToList();
        disparoEnemigos.ForEach(disparo => Destroy(disparo));
    }
}
