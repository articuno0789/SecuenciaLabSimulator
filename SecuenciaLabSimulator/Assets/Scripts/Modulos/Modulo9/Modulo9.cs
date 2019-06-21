using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo9 : MonoBehaviour
{
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> botonesCircularesAzules;

    // Start is called before the first frame update
    void Start()
    {
        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        botonesCircularesAzules = new List<GameObject>();
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
            else if (child.name.Contains("BotonCircularAzul"))
            {
                botonesCircularesAzules.Add(child);
            }
            inicializarComponentes(child);
        }
    }
}
