using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo4 : MonoBehaviour
{
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> focosVerde;
    [SerializeField] public List<GameObject> focosAmarillos;

    //Particulas
    private string rutaParticulaElectricalSparksEffect = "Assets/Assets Descargados/EffectExamples/Misc Effects/Prefabs/ElectricalSparksEffect.prefab";

    // Start is called before the first frame update
    void Start()
    {
        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        focosVerde = new List<GameObject>();
        focosAmarillos = new List<GameObject>();
        inicializarComponentes(gameObject);
    }

    /*void inicializarParticulas(int indice, string title, string nameModel,
        string description, GameObject modelSystemGO,
        Vector3 modelPosition, Vector3 modelRotation, Vector3 modelScale)
    {
        particle = new ParticlesInformation(title, nameModel, description, modelSystemGO, 
            modelPosition, modelRotation, modelScale);
        particles[indice] = particle;
    }*/

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
                FocoVerde focoVerde = child.AddComponent<FocoVerde>();
                focoVerde.padreTotalComponente = this.gameObject;
            }
            inicializarComponentes(child);
        }
    }
}
