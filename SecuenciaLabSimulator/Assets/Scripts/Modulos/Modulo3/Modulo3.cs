using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo3 : MonoBehaviour
{
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> botonesCircularesRojos;
    [SerializeField] public List<GameObject> botonesCircularesVerdes;
    private string rutaAnimacionBotonCircular = "Assets/Animation/Modulos/Modulo3/Mod3PresBotonCircular.anim";
    private string nombreAnimacionBotonCircular = "Mod3PresBotonCircular";

    // Start is called before the first frame update
    void Start()
    {
        plugsConnections = new Dictionary<string, string>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        botonesCircularesRojos = new List<GameObject>();
        botonesCircularesVerdes = new List<GameObject>();
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

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");
            }
            else if (child.name.Contains("BotonCircularRojo"))
            {
                botonesCircularesRojos.Add(child);
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircular, typeof(AnimationClip))), nombreAnimacionBotonCircular);
                child.AddComponent<Mod3PushButton>();
            }
            else if (child.name.Contains("BotonCircularVerde"))
            {
                botonesCircularesVerdes.Add(child);
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircular, typeof(AnimationClip))), nombreAnimacionBotonCircular);
                child.AddComponent<Mod3PushButton>();
            }
            inicializarComponentes(child);
        }
    }

    public void CrearConexionPlugs(string startPlug, string endPlug)
    {
        plugsConnections[startPlug] = endPlug;
        Debug.Log("plugsConnections[" + startPlug + "]: " + endPlug);
    }

}
