using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo7 : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> lucesRojasDict;
    [Header("Listas de elementos")]
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> lucesRojas;
    [Header("Perilla")]
    [SerializeField] public GameObject perilla;
    [SerializeField] public float limiteGiroInferiorPerilla = -90.0f;
    [SerializeField] public float limiteGiroSuperiorPerilla = 320.0f;
    public float valorActualPerilla = 0.1f;
    public float valorMinimoPerilla = 0.1f;
    public float valorMaximoPerilla = 30.0f;
    private Quaternion originalRotationKnob;
    [SerializeField] public float gradosActualesPerilla = -90.0f;
    [SerializeField] public float velocidadRotacion = 10;
    public bool rotarPerillaPrueba = false;
    [SerializeField] public int estaLimiteRotacion = -1;
    private bool puederotar = true;
    //Timers---------------------------
    [Header("Timers")]
    public int number = 0;
    public bool couroutineStarted = false;
    public float cuentaAtras = 0.0f;
    public bool cuentaIniciada = false;
    //---------------------------------
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Focos")]
    private string nombreTagFocoRojo = "FocoRojo";
    [Header("Parametros Perillas")]
    private string nombreTagPerilla = "Perilla";
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
        lucesRojasDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        lucesRojas = new List<GameObject>();
        InicializarComponentes(gameObject);
        if (moduloEncendido)
        {
            EncenderApagarLuzRoja(true);
        }
        //Contractores
        IncializacionContractores("EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3", false);
        IncializacionContractores("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", false);
        IncializacionContractores("EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7", false);
        IncializacionContractores("EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", true);
        IncializacionContractores("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", false);
        IncializacionContractores("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", true);

        /*plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado3"];
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado2"];
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado5"];
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado7"];
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado6"];
        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado9"];
        plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado8"];
        plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado11"];
        plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado10"];
        plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado13"];
        plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado12"];
        plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().relacionCerrada = true;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void IncializacionContractores(string nPlug1, string nPlug2, bool cerrado)
    {
        plugAnaranjadosDict[nPlug1].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlug2];
        plugAnaranjadosDict[nPlug1].GetComponent<Plugs>().relacionCerrada = cerrado;
        plugAnaranjadosDict[nPlug2].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlug1];
        plugAnaranjadosDict[nPlug2].GetComponent<Plugs>().relacionCerrada = cerrado;
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
                child.tag = nombreTagPlugAnaranjado;
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
                child.tag = nombreTagPlugNegro;
            }
            else if (child.name.Contains("Total_Perilla"))
            {
                perilla = child;
                originalRotationKnob = perilla.transform.rotation;
                child.tag = nombreTagPerilla;
            }
            else if (child.name.Contains("LuzRoja"))
            {
                lucesRojas.Add(child);
                LuzRoja luzRoja = child.AddComponent<LuzRoja>();
                luzRoja.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.SmokeEffect;
                luzRoja.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.ElectricalSparksEffect;
                luzRoja.padreTotalComponente = this.gameObject;
                lucesRojasDict.Add(child.name, child);
                child.tag = nombreTagFocoRojo;
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
            if (rotarPerillaPrueba)
            {
                RotarPerillaPrueba();
            }
            EncenderApagarLuzRoja(true);
            ComportamientoModulo();
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
            EncenderApagarLuzRoja(false);
        }
    }

    void EncenderApagarLuzRoja(bool encendida)
    {
        LuzRoja luz = lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>();
        if (luz != null)
        {
            if (encendida)
            {
                luz.EncenderFoco();
            }
            else
            {
                luz.ApagarFoco();
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. EncenderApagarLuz(bool encendida) - No se pudo obtener el componente LuzRoja.");
        }
    }


    /*Comportamiento para timers off delay (Punta de flecha para abajo) (Retardo al desenergizar)
    significa que cambiará de estado un tiempo predeterminado despupes que
    el time haya recibido la señal de apagar.*/
    public bool estadoAnteriorEnergizado;
    private void ComportamientoModulo()
    {
        if (!cuentaIniciada)
        {
            cuentaAtras = valorActualPerilla;
        }
        LuzRoja luzRoja = lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>();
        if (luzRoja != null && luzRoja.ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], plugNegrosDict["EntradaPlugNegro1"]))
        {
            estadoAnteriorEnergizado = true;
            if (cuentaIniciada)
            {
                cuentaIniciada = false;
                number = 0;
            }
            //Normalmente Abiertos
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            //Normalmente cerrados
            plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);

            //Timers
            plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
        }
        else
        {
            cuentaIniciada = true;
            if (!couroutineStarted && number <= cuentaAtras)
            {
                //InvokeRepeating("UsingInvokeRepeat", 0f, 0.1f);
                StartCoroutine(UsingYield(1));
            }
            //Normalmente Abiertos
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            //Normalmente cerrados
            plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);

            //Timers
            if (number >= cuentaAtras)
            {
                //Timers
                plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                estadoAnteriorEnergizado = false;
            }
            else if (estadoAnteriorEnergizado)
            {
                //Timers
                plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            }
            else
            {
                //Timers
                plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                estadoAnteriorEnergizado = false;
            }
        }
        //Debug.Log("Numbre: " + number + ", cuentaAtras: " + cuentaAtras);
        //Forma vieja
        /*if (!cuentaIniciada)
        {
            cuentaAtras = valorActualPerilla;
        }
        if (lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], plugNegrosDict["EntradaPlugNegro1"]))
        {
            estadoAnteriorEnergizado = true;
            if (cuentaIniciada)
            {
                cuentaIniciada = false;
                number = 0;
            }
            FuncionamientoContractorRojo("EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3", false); //Normalmente abierto
            FuncionamientoContractorRojo("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", false); //Normalmente abierto
            FuncionamientoContractorRojo("EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7", false); //Normalmente abierto
            FuncionamientoContractorRojo("EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", true); //Normalmente cerrado

            FuncionamientoContractorRojo("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", false); //Normalmente abierto
            FuncionamientoContractorRojo("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", true); //Normalmente cerrado
        }
        else
        {
            cuentaIniciada = true;
            if (!couroutineStarted && number <= cuentaAtras)
            {
                //InvokeRepeating("UsingInvokeRepeat", 0f, 0.1f);
                StartCoroutine(UsingYield(1));
            }
            FuncionamientoContractorRojo("EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3", true); //Normalmente abierto
            FuncionamientoContractorRojo("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", true); //Normalmente abierto
            FuncionamientoContractorRojo("EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7", true); //Normalmente abierto
            FuncionamientoContractorRojo("EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", false); //Normalmente cerrado

            //Timers
            if (number >= cuentaAtras)
            {
                //Timers
                FuncionamientoContractorRojo("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", true); //Normalmente abierto
                FuncionamientoContractorRojo("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", false); //Normalmente cerrado
                estadoAnteriorEnergizado = false;
            }
            else if(estadoAnteriorEnergizado)
            {
                FuncionamientoContractorRojo("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", false); //Normalmente abierto
                FuncionamientoContractorRojo("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", true); //Normalmente cerrado
            }
            else
            {
                FuncionamientoContractorRojo("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", true); //Normalmente abierto
                FuncionamientoContractorRojo("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", false); //Normalmente cerrado
                estadoAnteriorEnergizado = false;
            }
        }*/
        //Debug.Log("Numbre: " + number + "");
    }

    void UsingInvokeRepeat()
    {
        number++;
    }

    IEnumerator UsingYield(float seconds)
    {
        couroutineStarted = true;

        yield return new WaitForSeconds(seconds);
        number++;

        couroutineStarted = false;
    }

    //Codigo viejo
    /*void FuncionamientoContractorRojo(string nPlugConexionArribaCerrado, string nPlugConexionAbajoCerrado, bool conexionAbierta)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nPlugConexionArribaCerrado].GetComponent<Plugs>();
        Plugs plugConexionAbajoCerrado = plugAnaranjadosDict[nPlugConexionAbajoCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        //reiniciarTodosPlugsAnaranjados();
        //plugConexionArribaCerrado.EstablecerValoresNoConexion3(plugConexionAbajoCerrado);
        //plugConexionAbajoCerrado.EstablecerValoresNoConexion3(plugConexionArribaCerrado);
        /*if (!plugConexionArribaCerrado.estaConectado)
        {
            plugConexionArribaCerrado.EstablecerValoresNoConexion3(plugConexionAbajoCerrado);
        }
        if (!plugConexionAbajoCerrado.estaConectado)
        {
            plugConexionAbajoCerrado.EstablecerValoresNoConexion3(plugConexionArribaCerrado);
        }
        plugConexionArribaCerrado.EstablecerValoresNoConexion2();
        plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
        bool cortoElectrico = plugConexionArribaCerrado.ComprobarEstado(plugConexionArribaCerrado, plugConexionAbajoCerrado, conexionAbierta);
        Time.timeScale = 1.0F;
        if (!conexionAbierta && !cortoElectrico) // Para comprobar cortos
        {
            plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes();
            plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes();

            if (!conexionAbierta && plugConexionArribaCerrado.Conectado && plugConexionAbajoCerrado.Voltaje == 0 && plugConexionAbajoCerrado.TipoConexion == 0)// 
            {
                plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionArribaCerrado]);
            }
            else
            if (!conexionAbierta && plugConexionAbajoCerrado.Conectado)
            {
                plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionAbajoCerrado]);
            }
            else
            {
                //Debug.Log("Modulo 13: No entra a ningun caso");
            }
        }
    }*/

    public void RotarPerilla()
    {
        if (perilla != null)
        {
            if (valorActualPerilla >= valorMinimoPerilla && valorActualPerilla <= valorMaximoPerilla)
            {
                perilla.transform.rotation = originalRotationKnob;
                float valorRotacionGrados = (limiteGiroSuperiorPerilla * valorActualPerilla) / valorMaximoPerilla;
                Debug.Log("Modulo 7: valorRotacionGrados: " + valorRotacionGrados + ", valorActualPerilla: " + valorActualPerilla + ", limiteGiroSuperiorPerilla: " + limiteGiroSuperiorPerilla + ", valorMaximoPerilla: " + valorMaximoPerilla);
                perilla.transform.Rotate(0, 0, valorRotacionGrados);
            }
            else
            {
                Debug.LogError("Error. Modulo 7: rotarPerilla(float valorActual): El valor actual recibido sobrepasa los limites establecidos");
            }
        }
    }

    public void RotarPerillaPrueba()
    {
        /*Vector3 perillaRotation = UnityEditor.TransformUtils.GetInspectorRotation(perilla.gameObject.transform);
        //Debug.Log("perillaRotation: " + perillaRotation);
        if (perillaRotation.z >= limiteGiroSuperiorPerilla && puederotar)
        {
            //Debug.Log("if (perillaRotation.z >= limiteGiroInferiorPerilla && puederotar)");
            perilla.transform.Rotate(Vector3.back, velocidadRotacion * Time.deltaTime);
        }
        else
        if (perillaRotation.z >= limiteGiroInferiorPerilla && perillaRotation.z <= limiteGiroSuperiorPerilla && puederotar)
        {
            //Debug.Log("---if (perillaRotation.z <= limiteGiroInferiorPerilla && perillaRotation.z >= limiteGiroSuperiorPerilla && puederotar)");
            perilla.transform.Rotate(Vector3.forward, velocidadRotacion * Time.deltaTime);
        }
        else
        {
            puederotar = false;
            perilla.transform.Rotate(Vector3.forward, velocidadRotacion * Time.deltaTime);
            //agujaMedidora.transform.rotation = Quaternion.Euler(-35, -90, 90);
            //Debug.Log("Cambio de rotacion: " + agujaMedidoraRotation.x + " >= " + limiteGiroInferiorPerilla);
            if (perillaRotation.z >= limiteGiroInferiorPerilla)
            {
                puederotar = true;
            }
        }*/
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
