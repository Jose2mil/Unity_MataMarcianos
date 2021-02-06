using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Nave")
        {
            other.GetComponent<Nave>().Destruirse();
            Destroy(gameObject);
        }

        else if (other.tag == "Izquierda")
            Destroy(gameObject);
    }
}
