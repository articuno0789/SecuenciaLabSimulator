using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo5 : MonoBehaviour
{
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public GameObject agujaMedidora;
    [SerializeField] public float limiteGiroInferiorAguja = -35.0f;
    [SerializeField] public float limiteGiroSuperiorAguja = -145.0f;
    public float valorActualAguja = 0.0f;
    public float valorMinimoAguja = 0.0f;
    public float valorMaximoAguja = 250.0f;
    private Quaternion originalRotationNeedle;


    [SerializeField] public float gradosActualesAguja = -90.0f;
    [SerializeField] public float velocidadRotacion = 10;
    public bool rotarAgujaPrueba = false;
    [SerializeField] public int estaLimiteRotacion = -1;
    private bool puederotar = true;

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
        //valorActualAguja += 0.5f;
        RotarAguja();
        if (rotarAgujaPrueba)
        {
            RotarAguajaPrueba();
        }
    }

    public void RotarAguja()
    {
        if (valorActualAguja >= valorMinimoAguja && valorActualAguja <= valorMaximoAguja)
        {
            float valorRotacionGrados = 0.0f;
            agujaMedidora.transform.rotation = originalRotationNeedle;
            if(valorActualAguja >= valorMinimoAguja && valorActualAguja <= 50)
            {
                valorRotacionGrados = 0.52f * valorActualAguja;
                //Debug.Log("if(valorActualAguja >= 0 && valorActualAguja <= 50)");
            } else if (valorActualAguja > 50 && valorActualAguja <= 100)
            {
                valorRotacionGrados = (0.465f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 50 && valorActualAguja <= 100)");
            }
            else if (valorActualAguja > 100 && valorActualAguja <= 150)
            {
                valorRotacionGrados = (0.45f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 100 && valorActualAguja <= 150)");
            }
            else if (valorActualAguja > 150 && valorActualAguja <= 200)
            {
                valorRotacionGrados = (0.4425f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 150 && valorActualAguja <= 200)");
            }
            else if (valorActualAguja > 200 && valorActualAguja <= valorMaximoAguja)
            {
                valorRotacionGrados = (0.44f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 200 && valorActualAguja <= 250)");
            }
            else
            {
                //valorRotacionGrados = -1 * (limiteGiroSuperiorAguja * valorActualAguja) / valorMaximoAguja;
                valorRotacionGrados = ((Mathf.Abs(limiteGiroSuperiorAguja) - Mathf.Abs(limiteGiroInferiorAguja)) / valorMaximoAguja) * valorActualAguja;
                Debug.LogError("Error. Modulo 5: El valor actual de la aguja supera el limite maximo establecido");
            }
            //agujaMedidora.transform.Rotate(0, 0, valorRotacionGrados, Space.Self); //z
            agujaMedidora.transform.Rotate(0, valorRotacionGrados, 0); //y
            //agujaMedidora.transform.Rotate(valorRotacionGrados, 0, 0, Space.Self); //x
            /*Debug.Log("Modulo 5: valorRotacionGrados: " + valorRotacionGrados 
                + ", valorActualPerilla: " + valorActualAguja 
                + ", limiteGiroSuperiorPerilla: " + limiteGiroSuperiorAguja 
                + ", valorMaximoPerilla: " + valorMaximoAguja
                + ", Posición: " + agujaMedidora.transform.rotation.eulerAngles);*/
        }
        else
        {
            //agujaMedidora.transform.rotation = originalRotationNeedle;
            Debug.LogError("Error. Modulo 5: rotarPerilla(float valorActual): El valor actual recibido sobrepasa los limites establecidos");
        }
    }

    public void RotarAguajaPrueba()
    {
        Vector3 agujaMedidoraRotation = UnityEditor.TransformUtils.GetInspectorRotation(agujaMedidora.gameObject.transform);
        if (agujaMedidoraRotation.x >= limiteGiroInferiorAguja && puederotar)
        {
            agujaMedidora.transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
        else
        if (agujaMedidoraRotation.x <= limiteGiroInferiorAguja && agujaMedidoraRotation.x >= limiteGiroSuperiorAguja && puederotar)
        {
            agujaMedidora.transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
        else
        {
            puederotar = false;
            agujaMedidora.transform.Rotate(Vector3.down, velocidadRotacion * Time.deltaTime);
            //agujaMedidora.transform.rotation = Quaternion.Euler(-35, -90, 90);
            //Debug.Log("Cambio de rotacion: " + agujaMedidoraRotation.x + " >= " + limiteGiroInferiorAguja);
            if (agujaMedidoraRotation.x >= limiteGiroInferiorAguja)
            {
                puederotar = true;
            }
        }
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
            else if (child.name.Contains("AgujaMedidora"))
            {
                agujaMedidora = child;
                originalRotationNeedle = agujaMedidora.transform.rotation;
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
