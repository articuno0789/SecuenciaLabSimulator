using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo1 : MonoBehaviour
{
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> luzRojos;
    [SerializeField] public float voltaje = 127;
    [SerializeField] public bool pruebaDeLuz = true;

    // Start is called before the first frame update
    void Start()
    {
        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        luzRojos = new List<GameObject>();
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
            }else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();
            }
            else if (child.name.Contains("LuzRoja"))
            {
                luzRojos.Add(child);
                child.AddComponent<LuzRoja>();
            }
            inicializarComponentes(child);
        }
    }

}
