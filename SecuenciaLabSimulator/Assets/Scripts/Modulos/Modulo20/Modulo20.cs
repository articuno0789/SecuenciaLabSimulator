using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo20 : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    [Header("Listas de elementos")]
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [Header("Motor")]
    public GameObject motorAControlar;
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        //Inicialización de listas y diccionarios de elementos.
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
                plug.tipoNodo = 2;
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugAnaranjadosDict.Add(child.name, child);
                child.tag = nombreTagPlugAnaranjado;
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.tipoNodo = 2;
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
                child.tag = nombreTagPlugNegro;
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
            Comportamiento();
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    void Comportamiento()
    {
        MotorElectricoAC motor = motorAControlar.GetComponent<MotorElectricoAC>();
        Plugs plugConexion1 = plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>();
        Plugs plugConexion2 = plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>();
        Plugs plugConexion3 = plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>();
        if (motor != null && plugConexion1 != null && plugConexion2 != null && plugConexion3 != null)
        {
            float voltajeMinimo = motor.voltajeMinimo;
            float voltajeMaximo = motor.voltajeMaximo;
            if (plugConexion1.EstoConectado() && plugConexion2.EstoConectado() && plugConexion3.EstoConectado())
            {
                plugConexion1.EstablecerPropiedadesConexionesEntrantes();
                plugConexion2.EstablecerPropiedadesConexionesEntrantes();
                plugConexion3.EstablecerPropiedadesConexionesEntrantes();
                if (plugConexion1.VoltajeValido(voltajeMinimo, voltajeMaximo) &&
                    plugConexion2.VoltajeValido(voltajeMinimo, voltajeMaximo) &&
                    plugConexion3.VoltajeValido(voltajeMinimo, voltajeMaximo)
                    )
                {
                    if (plugConexion1.Voltaje != 0 && plugConexion2.Voltaje != 0 && plugConexion3.Voltaje != 0)
                    {
                        float voltajePromedio = (plugConexion1.Voltaje + plugConexion2.Voltaje + plugConexion3.Voltaje) / 3;
                        if (plugConexion1.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.PrimeraLinea) &&
                            plugConexion2.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.SegundaLinea) &&
                            plugConexion3.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.TerceraLinea))
                        {
                            motor.EstablecerParametrosMotor(true, (int)AuxiliarModulos.DireccionRotacion.Horario, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                        }
                        else
                        if (plugConexion1.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.PrimeraLinea) &&
                            plugConexion2.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.TerceraLinea) &&
                            plugConexion3.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.SegundaLinea))
                        {
                            motor.EstablecerParametrosMotor(true, (int)AuxiliarModulos.DireccionRotacion.Antihorario, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                        }
                        else
                        if (plugConexion1.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.SegundaLinea) &&
                            plugConexion2.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.PrimeraLinea) &&
                            plugConexion3.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.TerceraLinea))
                        {
                            motor.EstablecerParametrosMotor(true, (int)AuxiliarModulos.DireccionRotacion.Antihorario, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                        }
                        else
                        if (plugConexion1.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.SegundaLinea) &&
                            plugConexion2.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.TerceraLinea) &&
                            plugConexion3.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.PrimeraLinea))
                        {
                            motor.EstablecerParametrosMotor(true, (int)AuxiliarModulos.DireccionRotacion.Horario, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                        }
                        else
                        if (plugConexion1.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.TerceraLinea) &&
                            plugConexion2.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.PrimeraLinea) &&
                            plugConexion3.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.SegundaLinea))
                        {
                            motor.EstablecerParametrosMotor(true, (int)AuxiliarModulos.DireccionRotacion.Antihorario, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                        }
                        else
                        if (plugConexion1.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.TerceraLinea) &&
                            plugConexion2.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.SegundaLinea) &&
                            plugConexion3.ComprobaTipoLinea((int)AuxiliarModulos.NumeroLinea.PrimeraLinea))
                        {
                            motor.EstablecerParametrosMotor(true, (int)AuxiliarModulos.DireccionRotacion.Horario, voltajePromedio);//EncenderMotor, lado rotacion, velocidad
                        }
                        else
                        {
                            Debug.LogError("Error." + name + ": La combinacion de lineas no es valida. Plug 1: "
                                + plugConexion1.Linea + ", Plug 2: " + plugConexion2.Linea + ", Plug 3: " + plugConexion3.Linea);
                            motor.EstablecerParametrosMotor(false, (int)AuxiliarModulos.DireccionRotacion.SinRotar, 0);//EncenderMotor, lado rotacion, velocidad
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    Debug.LogError("Error." + name + ": Algunos de los conectores tiene un voltaje invalido. Plug 1: "
                            + plugConexion1.Voltaje + ", Plug 2: " + plugConexion2.Voltaje + ", Plug 3: " + plugConexion3.Voltaje);
                    motor.EstablecerParametrosMotor(false, (int)AuxiliarModulos.DireccionRotacion.SinRotar, 0);//EncenderMotor, lado rotacion, velocidad
                    motor.CrearAveria();
                }
            }
            else
            {
                /*Debug.LogError("Error." + name + ": Algunos de los conectores no esta conectadoo. Plug 1: "
                            + plugConexion1.estoConectado() + ", Plug 2: " + plugConexion2.estoConectado() + ", Plug 3: " + plugConexion3.estoConectado());*/
                motor.EstablecerParametrosMotor(false, 0, 0);//EncenderMotor, lado rotacion, velocidad
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. Comportamiento() - motor o plugConexion1 o plugConexion2 o plugConexion3 es nulo.");
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
