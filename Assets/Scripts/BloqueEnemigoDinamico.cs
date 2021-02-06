using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BloqueEnemigoDinamico : BloqueEnemigo
{
    [SerializeField] Transform zonaAparicion;
    [SerializeField] Transform prefabEnemigo;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        MoverEnemigos();
        if (transform.childCount == 0)
            GenerarColumnaDeEnemigos(2);
    }

    public void GenerarColumnaDeEnemigos(float y)
    {
        float columna = zonaAparicion.gameObject
                                     .GetComponent<Collider2D>()
                                     .bounds.center.x;
        float espacioEnemigos = y > 0 ? -1 : 1;
        for (int fila = 0; fila < 5; fila++)
            Instantiate(
                        prefabEnemigo, 
                        new Vector2(columna, y + fila * espacioEnemigos),
                        Quaternion.identity)
                .SetParent(transform);
    }

    protected override IEnumerator Avanzar(float y)
    {
        ComprobarZonaAparicion((y + VelocidadY * Time.deltaTime));
        yield return base.Avanzar(y);
    }

    private void ComprobarZonaAparicion(float y)
    {
        Enemigo[] enemigos = transform.GetComponentsInChildren<Enemigo>();
        bool puedeGenerarColumna = true;
        foreach (Enemigo e in enemigos)
            if (ColisionaConZonaReaparicion(e))
                puedeGenerarColumna = false;

        if (puedeGenerarColumna)
            GenerarColumnaDeEnemigos(y);
    }

    private bool ColisionaConZonaReaparicion(Enemigo enemigo)
    {
        float limiteX = 
            GameObject.FindGameObjectWithTag("ZonaAparicionEnemigos")
                      .GetComponent<Collider2D>().bounds.min.x;
        float maximaX = enemigo.GetComponent<Collider2D>().bounds.max.x;

        return maximaX >= limiteX ? true : false;
    }

    public override void ReajustarPosicion()
    {
        base.ReajustarPosicion();
        Enemigo[] enemigos = transform.GetComponentsInChildren<Enemigo>();
        foreach (Enemigo e in enemigos)
            Destroy(e.gameObject);
    }
}
