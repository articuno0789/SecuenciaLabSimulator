using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo20 : MonoBehaviour
{
    #region Atributos
    public bool moduloEncendido = true;
    public GameObject motorAControlar;
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;

    //Variables de debug
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        InicializarComponentes(gameObject);
        if(motorAControlar == null)
        {
            motorAControlar = GameObject.Find("MotorElectricoAC1");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void InicializarComponentes(GameObject nodo)
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

                plugAnaranjadosDict.Add(child.name, child);
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
            }
            InicializarComponentes(child);
        }
    }

    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {
        ComprobarEstadosDiccionarios();
        if (moduloEncendido)
        {
            //Hacer algo si el modulo esta encendido.
            comportamiento();
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    void comportamiento()
    {
        float voltajeMinimo = motorAControlar.GetComponent<MotorElectricoAC>().voltajeMinimo;
        float voltajeMaximo = motorAControlar.GetComponent<MotorElectricoAC>().voltajeMaximo;
        Plugs plugConexion1 = plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>();
        Plugs plugConexion2 = plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>();
        Plugs plugConexion3 = plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>();
        if (plugConexion1.estoConectado() && plugConexion2.estoConectado() && plugConexion3.estoConectado())
        {
            plugConexion1.EstablecerPropiedadesConexionesEntrantes();
            plugConexion2.EstablecerPropiedadesConexionesEntrantes();
            plugConexion3.EstablecerPropiedadesConexionesEntrantes();
            if (plugConexion1.VoltajeValido(voltajeMinimo, voltajeMaximo) && 
                plugConexion2.VoltajeValido(voltajeMinimo, voltajeMaximo) && 
                plugConexion3.VoltajeValido(voltajeMinimo, voltajeMaximo))
            {
                float voltajePromedio = (plugConexion1.Voltaje + plugConexion2.Voltaje + plugConexion3.Voltaje) / 3;
                if (plugConexion1.ComprobaTipoLinea(1) && plugConexion2.ComprobaTipoLinea(2) && plugConexion3.ComprobaTipoLinea(3))
                {
                    motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(true, 1, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                }
                else if (plugConexion1.ComprobaTipoLinea(1) && plugConexion2.ComprobaTipoLinea(3) && plugConexion3.ComprobaTipoLinea(2))
                {
                    motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(true, 2, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                }
                else if (plugConexion1.ComprobaTipoLinea(2) && plugConexion2.ComprobaTipoLinea(1) && plugConexion3.ComprobaTipoLinea(3))
                {
                    motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(true, 2, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                }
                else if (plugConexion1.ComprobaTipoLinea(2) && plugConexion2.ComprobaTipoLinea(3) && plugConexion3.ComprobaTipoLinea(1))
                {
                    motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(true, 1, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                }
                else if (plugConexion1.ComprobaTipoLinea(3) && plugConexion2.ComprobaTipoLinea(1) && plugConexion3.ComprobaTipoLinea(2))
                {
                    motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(true, 2, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                }
                else if (plugConexion1.ComprobaTipoLinea(3) && plugConexion2.ComprobaTipoLinea(2) && plugConexion3.ComprobaTipoLinea(1))
                {
                    motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(true, 1, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                }
                else
                {
                    Debug.LogError("Error." + name + ": La combinacion de lineas no es valida. Plug 1: "
                        + plugConexion1.Linea + ", Plug 2: " + plugConexion2.Linea + ", Plug 3: " + plugConexion3.Linea);
                    motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(false, 0, 0);//EncenderMotor, lado rotacion, velocidad
                }
            }
            else
            {
                Debug.LogError("Error." + name + ": Algunos de los conectores tiene un voltaje invalido. Plug 1: "
                        + plugConexion1.Voltaje + ", Plug 2: " + plugConexion2.Voltaje + ", Plug 3: " + plugConexion3.Voltaje);
                motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(false, 0, 0);//EncenderMotor, lado rotacion, velocidad
                motorAControlar.GetComponent<MotorElectricoAC>().CrearAveria();
            }
        }
        else
        {
            /*Debug.LogError("Error." + name + ": Algunos de los conectores no esta conectadoo. Plug 1: "
                        + plugConexion1.estoConectado() + ", Plug 2: " + plugConexion2.estoConectado() + ", Plug 3: " + plugConexion3.estoConectado());*/
            motorAControlar.GetComponent<MotorElectricoAC>().EstablecerParametrosMotor(false, 0, 0);//EncenderMotor, lado rotacion, velocidad
        }
    }

    private void OnDestroy()
    {
        if (motorAControlar != null)
        {
            motorAControlar.GetComponent<MotorElectricoAC>().ReiniciarMotor();
        }
    }

    #endregion

    #region Conexiones Grafo
    public void CrearConexionPlugs(string startPlug, string endPlug)
    {
        plugsConnections[startPlug] = endPlug;
        Debug.Log("plugsConnections[" + startPlug + "]: " + endPlug);
    }

    void ComprobarEstadosDiccionarios()
    {
        if (mostrarDiccionarioConexiones)
        {
            ImprimirDiccionarioConexiones();
        }
        if (mostrarPlugAnaranjados)
        {
            ImprimirDiccionario(plugAnaranjadosDict, 1);
        }
        if (mostrarPlugNegros)
        {
            ImprimirDiccionario(plugNegrosDict, 2);
        }
    }

    public void ImprimirDiccionarioConexiones()
    {
        mostrarDiccionarioConexiones = false;
        Debug.Log("************************************************************************************");
        Debug.Log("************************** plugsConnections **********************************");
        foreach (KeyValuePair<string, string> entry in plugsConnections)
        {
            Debug.Log("Plug origen: " + entry.Key + ", Plug destino: " + entry.Value);
            // do something with entry.Value or entry.Key
        }
        Debug.Log("************************************************************************************");
    }

    public void ImprimirDiccionario(Dictionary<string, GameObject> diccionario, int bandera)
    {
        string nombreDiccionario = "No establecido";
        if (bandera == 1)
        {
            mostrarPlugAnaranjados = false;
            nombreDiccionario = "plugAnaranjadosDict";
        }
        else if (bandera == 2)
        {
            mostrarPlugNegros = false;
            nombreDiccionario = "plugNegrosDict";
        }
        Debug.Log("************************************************************************************");
        Debug.Log("************************** " + nombreDiccionario + "  **********************************");
        foreach (KeyValuePair<string, GameObject> entry in diccionario)
        {
            Debug.Log("Indice: " + entry.Key + ", Valor: " + entry.Value);
            // do something with entry.Value or entry.Key
        }
        Debug.Log("************************************************************************************");
    }

    #endregion
}
