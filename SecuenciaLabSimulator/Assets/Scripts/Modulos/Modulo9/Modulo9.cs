using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo9 : MonoBehaviour
{
    #region Atributos
    public Dictionary<string, string> plugsConnections;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> focosCircularesAzulesDict;
    public List<GameObject> plugAnaranjados;
    public List<GameObject> plugNegros;
    public List<GameObject> focosCircularesAzules;
    private string rutaAnimacionBotonCircularAzul = "Assets/Animation/Modulos/Modulo9/Mod9PresBotonCircularAzul.anim";
    private string nombreAnimacionBotonCircularAzul = "Mod9PresBotonCircularAzul";

    public enum ParticlesErrorTypes
    {
        BigExplosion,
        DrippingFlames,
        ElectricalSparksEffect,
        SmallExplosionEffect,
        SmokeEffect,
        SparksEffect,
        RibbonSmoke,
        PlasmaExplosionEffect
    }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        focosCircularesAzulesDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        focosCircularesAzules = new List<GameObject>();
        InicializarComponentes(gameObject);
    }

    private void InicializarComponentes(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            GameObject child = nodo.transform.GetChild(i).gameObject;
            Debug.Log("child.name: " + child.name);
            if (child.name.Contains("EntradaPlugAnaranjado"))
            {
                plugAnaranjados.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugAnaranjadosDict.Add(child.name, child);
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");
            }
            else if (child.name.Contains("FocoCircularAzul"))
            {
                focosCircularesAzules.Add(child);
                FocoCircularAzul fococircularAzul = child.AddComponent<FocoCircularAzul>();
                fococircularAzul.CurrentTypeParticleError = (int)ParticlesErrorTypes.SmokeEffect;
                fococircularAzul.CurrentTypeParticleError = (int)ParticlesErrorTypes.ElectricalSparksEffect;
                fococircularAzul.padreTotalComponente = this.gameObject;
                focosCircularesAzulesDict.Add(child.name, child);


                //fococircularAzul.currentTypeParticleError = (int)ParticlesError.SmokeEffect;
                /*Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircularAzul, typeof(AnimationClip))), nombreAnimacionBotonCircularAzul);
                child.AddComponent<Mod9PushButton>();*/
            }
            InicializarComponentes(child);
        }
    }

    #endregion
    
    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {
        ComportamientoModulo();
    }
    
    private void ComportamientoModulo()
    {
        focosCircularesAzulesDict["FocoCircularAzul1"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], plugAnaranjadosDict["EntradaPlugAnaranjado2"]);
        focosCircularesAzulesDict["FocoCircularAzul2"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado3"], plugAnaranjadosDict["EntradaPlugAnaranjado4"]);
        focosCircularesAzulesDict["FocoCircularAzul3"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado5"], plugAnaranjadosDict["EntradaPlugAnaranjado6"]);
        focosCircularesAzulesDict["FocoCircularAzul4"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado7"], plugAnaranjadosDict["EntradaPlugAnaranjado8"]);
        focosCircularesAzulesDict["FocoCircularAzul5"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado9"], plugAnaranjadosDict["EntradaPlugAnaranjado10"]);
        focosCircularesAzulesDict["FocoCircularAzul6"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado11"], plugAnaranjadosDict["EntradaPlugAnaranjado12"]);
    }
    #endregion

    #region Conexiones Grafo
    public void CrearConexionPlugs(string startPlug, string endPlug)
    {
        plugsConnections[startPlug] = endPlug;
        Debug.Log("plugsConnections[" + startPlug + "]: " + endPlug);
    }
    #endregion
}
