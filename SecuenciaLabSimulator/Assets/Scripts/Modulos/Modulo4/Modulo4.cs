using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo4 : MonoBehaviour
{
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> focosVerde;
    [SerializeField] public List<GameObject> focosAmarillos;

    // Start is called before the first frame update
    void Start()
    {
        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        focosVerde = new List<GameObject>();
        focosAmarillos = new List<GameObject>();
        inicializarComponentes(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void inicializarComponentes(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            GameObject child = nodo.transform.GetChild(i).gameObject;
            if (child.name.Contains("EntradaPlugAnaranjado"))
            {
                plugAnaranjados.Add(child);
                child.AddComponent<CableComponent>();
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();
            }
            else if (child.name.Contains("FocoAmarilo"))
            {
                focosAmarillos.Add(child);
                child.AddComponent<FocoAmarillo>();
            }
            else if (child.name.Contains("FocoVerde"))
            {
                focosVerde.Add(child);
                child.AddComponent<FocoVerde>();
            }
            inicializarComponentes(child);
        }
    }
}
