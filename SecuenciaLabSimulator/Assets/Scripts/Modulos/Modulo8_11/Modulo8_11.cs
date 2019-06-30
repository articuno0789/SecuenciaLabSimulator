using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo8_11 : MonoBehaviour
{
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public GameObject botonStop;
    [SerializeField] public GameObject perillaMA;
    private string rutaAnimacionBotonStop = "Assets/Animation/Modulos/Modulo 8, 11/StopButton.anim";
    private string nombreAnimacionBotonStop = "StopButton";
    private string rutaAnimacionPerillaMA = "Assets/Animation/Modulos/Modulo 8, 11/PerillaMA.anim";
    private string nombreAnimacionPerillaMA = "PerillaMA";
    private string rutaAnimacionPerillaAM = "Assets/Animation/Modulos/Modulo 8, 11/PerillaAM.anim";
    private string nombreAnimacionPerillaAM = "PerillaAM";

    // Start is called before the first frame update
    void Start()
    {
        plugsConnections = new Dictionary<string, string>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
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
            else if (child.name.Contains("BotonStop"))
            {
                botonStop = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonStop, typeof(AnimationClip))), nombreAnimacionBotonStop);
                child.AddComponent<Mod8_11_BotonStop>();
            }
            else if (child.name.Contains("PerillaMA"))
            {
                perillaMA = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionPerillaMA, typeof(AnimationClip))), nombreAnimacionPerillaMA);
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionPerillaAM, typeof(AnimationClip))), nombreAnimacionPerillaAM);
                child.AddComponent<Mod8_11_Perilla>();
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
