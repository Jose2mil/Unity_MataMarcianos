using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nave : MonoBehaviour
{
    private float velocidad = 5;
    private Vector2 posicionInicial;

    //Posiciones de aparicion del disparo
    [SerializeField] Transform canyonDerecho;
    [SerializeField] Transform canyonIzquierdo;
    private bool dipararCanyonDerecho = false;

    //Atributos del disparo
    [SerializeField] Transform prefabDisparo;
    [SerializeField] float velocidadDisparo;

    [SerializeField] Transform particulasDestruccion;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        Moverse();

        if (Input.GetButtonDown("Jump"))
            Disparar();
    }

    private void Moverse()
    {
        float vertical = Input.GetAxis("Vertical");
        float desplazamiento = vertical * velocidad * Time.deltaTime;

        if(! ColisionaraConMargen(desplazamiento))
            transform.Translate(0, desplazamiento, 0);
    }

    private bool ColisionaraConMargen(float desplazamiento)
    {
        float techo = GameObject.FindGameObjectWithTag("Arriba")
                                .GetComponent<Collider2D>().bounds.min.y;
        float suelo = GameObject.FindGameObjectWithTag("Abajo")
                                .GetComponent<Collider2D>().bounds.max.y;
        float proximaMaxY = 
            GetComponent<Collider2D>().bounds.max.y + desplazamiento;
        float proximaMinY = 
            GetComponent<Collider2D>().bounds.min.y + desplazamiento;

        return proximaMaxY > techo || proximaMinY < suelo ? true : false;
    }

    private void Disparar()
    {
        Transform disparo = dipararCanyonDerecho?
                            Instantiate(
                                        prefabDisparo, 
                                        canyonDerecho.position, 
                                        Quaternion.identity):
                            Instantiate(
                                        prefabDisparo, 
                                        canyonIzquierdo.position, 
                                        Quaternion.identity);

        dipararCanyonDerecho = !dipararCanyonDerecho;
        disparo.gameObject.GetComponent<Rigidbody2D>().velocity = 
            new Vector3(velocidadDisparo, 0, 0);
    }

    public void Destruirse()
    {
        Marcador marcador = GameObject.FindGameObjectWithTag("Marcador")
                                      .GetComponent<Marcador>();
        Instantiate(particulasDestruccion, transform.position, Quaternion.identity);

        if (Marcador.Vidas > 0)
            ReajustarPosiciones();
        else
            Destroy(gameObject);

        marcador.RestarVida();
    }

    public void ReajustarPosiciones()
    {
        transform.position = posicionInicial;
        GameObject.FindGameObjectWithTag("BloqueEnemigos")
                  .GetComponent<BloqueEnemigo>().ReajustarPosicion();

        List<GameObject> disparosNave = 
            GameObject.FindGameObjectsWithTag("DisparoNave").ToList();
        disparosNave.ForEach(disparo => Destroy(disparo));
    }
}
