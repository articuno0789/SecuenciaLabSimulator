using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ProgressManager : MonoBehaviour
{
    #region Atributos
    //Variables
    [Header("GUI - Interfaces Auxiliares")]
    public Material cableMaterial;
    public Dropdown dropdown;
    public GameObject panel;
    private GameObject padreTotal;
    private GameObject modulesList;
    //Opciones de Guardado
    [Header("Parametros de Guardado")]
    public string rutaGuardado;
    public string nombreArchivoGuardado = "";
    //Cifrado
    [Header("Parametros de cifrado")]
    public bool cifrar = false;
    private readonly string palabraClave = "ProyectoModular";
    //Debug
    [Header("Debug")]
    public bool debug = false;
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        rutaGuardado = Path.Combine(Application.persistentDataPath, "SimulatorDataTest.txt");
    }
    #endregion

    #region Comportamiento

    void ConfirmarElemetosExternos()
    {
        if (dropdown == null)
        {
            dropdown = GameObject.Find("DropdownChangeModule").GetComponent<Dropdown>();
        }
        if (panel == null)
        {
            panel = GameObject.Find("PanelChangeModule");
        }
        if (modulesList == null)
        {
            modulesList = GameObject.Find("ModulesGroup");
        }
    }

    /*Este método se encarga de verificar las entradas por teclado.*/
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.G))//Se guarda el contenido actual del simulador en el archivo de prueba.
        {
            SaveSimulator(rutaGuardado, "SimulatorDataTest.txt");
        }

        if (Input.GetKeyDown(KeyCode.C))//Se carga el contenido del archivo de pruebas guardado.
        {
            LoadSimulator(rutaGuardado, "SimulatorDataTest.txt");
        }*/

        if (Input.GetKeyDown(KeyCode.F3))//Se destrullen los módulos actuales del siulador y remplazarlos con modulos vaicos
        {
            VaciarSimulador();
        }
    }

    //Se destrullen los módulos actuales del siulador y remplazarlos con modulos vaicos
    public void VaciarSimulador()
    {
        ConfirmarElemetosExternos();
        DestruirModulos();
        CrearMaquinaVacia();
    }

    /*Este método se encarga de destruir todos los tipos de módulos especificados en el simulador.*/
    void DestruirModulos()
    {
        DestruirObjetos("1");
        DestruirObjetos("2");
        DestruirObjetos("3");
        DestruirObjetos("4");
        DestruirObjetos("5");
        DestruirObjetos("6");
        DestruirObjetos("7");
        DestruirObjetos("8, 11");
        DestruirObjetos("9");
        DestruirObjetos("10, 17, 18, 19");
        DestruirObjetos("13");
        DestruirObjetos("14, 16");
        DestruirObjetos("15");
        DestruirObjetos("20");
        DestruirObjetos("21");
        DestruirObjetos("22");
        DestruirObjetos("23");
        DestruirObjetos("Potenciometro");
        DestruirObjetos("ModuloVacio");
        DestruirObjetos("Multiconector");
        if (debug)
        {
            Debug.Log("Modulos destruidos");
        }
    }

    /*Este método se encarga de destruir todos los objetos especificados de acuerdo a la tag.*/
    bool DestruirObjetos(string tagModulos)
    {
        bool resultado = false;
        GameObject[] modulosDestruir = GameObject.FindGameObjectsWithTag(tagModulos);
        int numeroMaximoModulos = modulosDestruir.Length;
        foreach (GameObject modulo in modulosDestruir)
        {
            Destroy(modulo);
        }
        if (numeroMaximoModulos != 0)
        {
            resultado = true;
        }
        return resultado;
    }

    public void LoadSimulator(string path, string nameFile)
    {
        ConfirmarElemetosExternos();
        string jsonString = ObtenerCadenaJson(path);
        JSONObject j = new JSONObject(jsonString);
        nombreArchivoGuardado = nameFile;
        if (IsValidJson(jsonString))
        {
            Debug.Log("************JSON Valido");
            DestruirModulos();
            CrearModulosDesdeJson(jsonString);
            CrearConexionesDesdeJson(jsonString);
        }
        else
        {
            Debug.Log("//////////////JSON Invalido");
        }
    }

    private void CrearMaquinaVacia()
    {
        int posicionModulesList = 0;
        ModulesList modList = modulesList.GetComponent<ModulesList>();
        if (modList != null)
        {
            int longitudModulos = AuxiliarModulos.numModSimulador;
            for (int i = 0; i < longitudModulos; i++)
            {
                string tipoModulo = AuxiliarModulos.tagModVacio;
                Vector3 posicion = modList.modulesGroup[i].modelPosition;
                Vector3 vectorPosicion = posicion;
                Vector3 vectorRotacion = new Vector3(0, 180, 0);
                GameObject newModule = CrearNuevoModulo(tipoModulo, vectorPosicion, vectorRotacion, tipoModulo + "_" + i);
                AsignacionRecursivaLogicaComponentes(newModule);
                modList.modulesGroup[posicionModulesList].modelSystemGO = newModule;
                modList.modulesGroup[posicionModulesList].modelPosition = newModule.transform.position;
                modList.modulesGroup[posicionModulesList].modelRotation = newModule.transform.rotation.eulerAngles;
                modList.modulesGroup[posicionModulesList].nameModule = newModule.name;
                modList.modulesGroup[posicionModulesList].title = newModule.name;
                //EstablecerParametrosVariablesModulos(newModule, tipoModulo, contenido);
                posicionModulesList++;
            }
            if (debug)
            {
                Debug.Log("Termina creación modulos vacios");
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. CrearMaquinaVacia() - modList es nulo.");
        }
    }

    /*Este método se encarga en crear los módulos del simulador, desde un archivo de guardado Json válido.*/
    private void CrearModulosDesdeJson(string jsonString)
    {
        var jo = JObject.Parse(jsonString);
        int posicionModulesList = 0;
        ModulesList modList = modulesList.GetComponent<ModulesList>();
        if (jo != null)
        {
            if (modList != null)
            {
                foreach (var x in jo)
                {
                    string nombreModulo = x.Key;
                    JToken contenido = x.Value;
                    if (debug)
                    {
                        Debug.Log("name: " + nombreModulo);
                        Debug.Log("value: " + contenido);
                        Debug.Log("Tipo: " + contenido["Tipo"]);
                    }
                    string tipoModulo = contenido["Tipo"].ToString();
                    JToken posicion = contenido["Posicion"];
                    Vector3 vectorPosicion = new Vector3(float.Parse(posicion["X"].ToString()), float.Parse(posicion["Y"].ToString()), float.Parse(posicion["Z"].ToString()));
                    Vector3 vectorRotacion = new Vector3(0, 180, 0);
                    GameObject newModule = CrearNuevoModulo(tipoModulo, vectorPosicion, vectorRotacion, nombreModulo);
                    AsignacionRecursivaLogicaComponentes(newModule);
                    modList.modulesGroup[posicionModulesList].modelSystemGO = newModule;
                    modList.modulesGroup[posicionModulesList].modelPosition = newModule.transform.position;
                    modList.modulesGroup[posicionModulesList].modelRotation = newModule.transform.rotation.eulerAngles;
                    modList.modulesGroup[posicionModulesList].nameModule = newModule.name;
                    modList.modulesGroup[posicionModulesList].title = newModule.name;
                    EstablecerParametrosVariablesModulos(newModule, tipoModulo, contenido);
                    posicionModulesList++;
                }
                if (debug)
                {
                    Debug.Log("Termina creación modulos");
                }
            }
            else
            {
                Debug.LogError(this.name + ", Error. CrearModulosDesdeJson(string jsonString)- modList es nulo.");
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. CrearModulosDesdeJson(string jsonString)- jo es nulo.");
        }
    }

    /*Este método se encarga de establecer a los módulos creados con el método "void CrearModulosDesdeJson()"
      los valores variabbles particulares de cada uno.*/
    private void EstablecerParametrosVariablesModulos(GameObject newModule, string tipoModulo, JToken contenido)
    {
        /*En esta sección se establecen los valores variables a cada modulo*/
        if (tipoModulo == AuxiliarModulos.tagMod1)
        {
            float voltaje = float.Parse(contenido["Voltaje"].ToString());
            Modulo1 mod1 = newModule.GetComponent<Modulo1>();
            mod1.voltajeModulo = voltaje;
        }
        else if (tipoModulo == AuxiliarModulos.tagMod6)
        {
            float valorPerilla = float.Parse(contenido["ValorPerilla"].ToString());
            Modulo6 mod6 = newModule.GetComponent<Modulo6>();
            mod6.valorActualPerilla = valorPerilla;
            mod6.RotarPerilla();
        }
        else if (tipoModulo == AuxiliarModulos.tagMod7)
        {
            float valorPerilla = float.Parse(contenido["ValorPerilla"].ToString());
            Modulo7 mod7 = newModule.GetComponent<Modulo7>();
            mod7.valorActualPerilla = valorPerilla;
            mod7.RotarPerilla();
        }
        else if (tipoModulo == AuxiliarModulos.tagModPotenciometro)
        {
            float valorPerilla = float.Parse(contenido["ValorPerilla"].ToString());
            Potenciometro modPoten = newModule.GetComponent<Potenciometro>();
            modPoten.valorActualPerilla = valorPerilla;
            modPoten.RotarPerilla();
        }
    }

    /*Este método se encarga de recuperar el indice de un módulo (Con el nombre), que se encuentre presente en la lista
      de módulos actuales del simulador.*/
    private int RecuperarIndiceModuloPorNombre(string nombreModuloBuscar)
    {
        int indiceEncontrado = -1;
        if (modulesList != null)
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            for (int i = 0; i < AuxiliarModulos.numModSimulador; i++)
            {
                if (modelusList.modulesGroup[i].modelSystemGO.name == nombreModuloBuscar)
                {
                    indiceEncontrado = i + 1;
                    break;
                }
            }
        }
        return indiceEncontrado;
    }

    /*Este método se encarga en crear las conexiones de los módulos del simulador, desde un archivo de guardado Json
     * válido.*/
    private void CrearConexionesDesdeJson(string jsonString)
    {
        GameObject moduloActual = null;
        var jo = JObject.Parse(jsonString);
        int posicionModulesList = 0;
        ModulesList modList = modulesList.GetComponent<ModulesList>();
        if (jo != null)
        {
            if (modList != null)
            {
                foreach (var x in jo)
                {
                    string nombreModulo = x.Key;
                    JToken contenido = x.Value;
                    if (debug)
                    {
                        Debug.Log("name: " + nombreModulo);
                        Debug.Log("value: " + contenido);
                        Debug.Log("Tipo: " + contenido["Tipo"]);
                    }
                    string tipoModulo = contenido["Tipo"].ToString();
                    JToken posicion = contenido["Posicion"];
                    JToken Conexiones = contenido["Conexiones"];
                    if (tipoModulo != "ModuloVacio")
                    {
                        int indiceModuloActual = RecuperarIndiceModuloPorNombre(nombreModulo);
                        if (indiceModuloActual != -1)
                        {
                            moduloActual = modList.modulesGroup[indiceModuloActual - 1].modelSystemGO;
                        }
                        GameObject.Find(nombreModulo);
                        Dictionary<string, string> plugsConnections = AuxiliarModulos.ObtenerPlugConnections(tipoModulo, moduloActual);
                        int numeroPlugs = plugsConnections.Count;
                        int j = 0;
                        if (debug)
                        {
                            Debug.Log(nombreModulo + " - Numero de plugs: " + numeroPlugs);
                        }
                        //foreach (KeyValuePair<string, string> entry in plugsConnections)
                        for (int i = 0; i < numeroPlugs; i++)
                        {
                            string conexionActual = "Conexion" + (j + 1);
                            string[] parametrosConexionOrigen = Conexiones[conexionActual]["Origen"].ToString().Split('|');
                            string[] parametrosConexionDestino = Conexiones[conexionActual]["Destino"].ToString().Split('|');
                            if (debug)
                            {
                                Debug.Log("parametrosConexionDestino.Length: " + parametrosConexionDestino.Length);
                            }
                            if (parametrosConexionDestino.Length != 0 && parametrosConexionDestino[0] != "")
                            {
                                //Debug.LogError("parametrosConexionDestino.Length: " + parametrosConexionDestino.Length);
                                //Debug.LogError("parametrosConexionDestino[0]: " + parametrosConexionDestino[0]);
                                //Debug.LogError(parametrosConexionDestino[0] + " " + parametrosConexionDestino[1]);
                                GameObject moduloDestino = null;
                                int indiceModuloDestino = RecuperarIndiceModuloPorNombre(parametrosConexionDestino[0]);
                                if (indiceModuloDestino != -1)
                                {
                                    moduloDestino = modList.modulesGroup[indiceModuloDestino - 1].modelSystemGO;
                                    //Debug.LogError("moduloActual: " + moduloActual.name + ", moduloDestino: " + moduloDestino.name);
                                    GameObject conexionOrigen = AuxiliarModulos.BuscarPlugConexion(moduloActual.tag, moduloActual, parametrosConexionOrigen[1]);
                                    GameObject conexionDestino = AuxiliarModulos.BuscarPlugConexion(moduloDestino.tag, moduloDestino, parametrosConexionDestino[1]);
                                    if (conexionOrigen != null && conexionDestino != null)
                                    {
                                        Color colorCable = new Color(0, 0, 0, 1);
                                        Debug.LogError("Longitud error: " + parametrosConexionDestino.Length);
                                        if (parametrosConexionDestino.Length == 2)
                                        {
                                            //Debug.LogError("Color ROJO: " + Conexiones[conexionActual]["Color"]["R"].ToString());
                                            colorCable = new Color(float.Parse(Conexiones[conexionActual]["Color"]["R"].ToString()),
                                                float.Parse(Conexiones[conexionActual]["Color"]["G"].ToString()),
                                                float.Parse(Conexiones[conexionActual]["Color"]["B"].ToString()),
                                                float.Parse(Conexiones[conexionActual]["Color"]["A"].ToString()));
                                            Debug.LogError("Color leido: " + colorCable.ToString());
                                        }
                                        CrearConexionCable(conexionOrigen, conexionDestino, colorCable);
                                        if (debug)
                                        {
                                            Debug.Log("Se creo una conexion");
                                        }
                                    }

                                }
                            }
                            j++;
                        }
                    }
                    posicionModulesList++;
                }
                Debug.Log("Termina creación conexiones");
            }
            else
            {
                Debug.LogError(this.name + ", CrearConexionesDesdeJson(string jsonString) - modList es nulo");
            }
        }
        else
        {
            Debug.LogError(this.name + ", CrearConexionesDesdeJson(string jsonString) - jo es nulo");
        }
    }

    /*Este método se encarga de realizar y establecer la conexión entre dos plugs de dos módulos determinados.*/
    private void CrearConexionCable(GameObject conexionOrigen, GameObject conexionDestino, Color colorCable)
    {
        CableComponent cableCompStart = conexionOrigen.GetComponent<CableComponent>();
        CableComponent cableCompEnd = conexionDestino.GetComponent<CableComponent>();
        //bool eliminarCable = ComprobarEliminarConexion(cableCompStart, conexionOrigen);
        //ComprobarEliminarConexion(cableCompEnd, conexionDestino);
        if (true)
        {
            cableCompStart.startPoint = conexionOrigen;
            cableCompStart.endPoint = conexionDestino;

            //cableCompStart.cableMaterial = (Material)Resources.Load("CableMaterial.mat", typeof(Material));
            //Primer conector en  ser seleccionado
            if (cableCompStart.todoCableMismoColor)
            {
                cableCompStart.startColor = colorCable;
                cableCompStart.endColor = colorCable;
            }
            else
            {
                cableCompStart.startColor = AuxiliarModulos.startColor;
                cableCompStart.endColor = colorCable;
            }
            //cableCompStart.startColor = colorCable;
            cableCompStart.cableMaterial = cableMaterial;
            cableCompStart.InitCableParticles();
            cableCompStart.InitLineRenderer();

            if (cableCompStart.line != null)
            {
                //cableCompStart.line.endColor = colorCable;
                //cableCompStart.line.startColor = colorCable;
                if (cableCompStart.todoCableMismoColor)
                {
                    cableCompStart.startColor = colorCable;
                    cableCompStart.endColor = colorCable;
                }
                else
                {
                    cableCompStart.startColor = AuxiliarModulos.startColor;
                    cableCompStart.endColor = colorCable;
                }
                cableCompStart.line.SetColors(cableCompStart.line.startColor, cableCompStart.line.endColor);
            }

            //Segundo conector en eser seleccionado
            if (cableCompEnd.todoCableMismoColor)
            {
                cableCompEnd.startColor = colorCable;
                cableCompEnd.endColor = colorCable;
            }
            else
            {
                cableCompEnd.startColor = AuxiliarModulos.startColor;
                cableCompEnd.endColor = colorCable;
            }
            cableCompEnd.startColor = colorCable;
            cableCompEnd.startPoint = conexionDestino;
            cableCompEnd.endPoint = conexionOrigen;
            cableCompEnd.cableMaterial = cableMaterial;

            if (cableCompEnd.line != null)
            {
                //cableCompStart.line.endColor = colorCable;
                //cableCompStart.line.startColor = colorCable;
                if (cableCompEnd.todoCableMismoColor)
                {
                    cableCompEnd.startColor = colorCable;
                    cableCompEnd.endColor = colorCable;
                }
                else
                {
                    cableCompEnd.startColor = AuxiliarModulos.startColor;
                    cableCompEnd.endColor = colorCable;
                }
                cableCompEnd.line.SetColors(cableCompStart.line.startColor, cableCompStart.line.endColor);
            }

            //lastClickedGmObj.GetComponent<Renderer>().material.color = Color.white;
            //clickedGmObj.GetComponent<Renderer>().material.color = Color.white;
            if (conexionOrigen.GetComponent<ChangeColorCables>() == null)
            {
                conexionOrigen.AddComponent<ChangeColorCables>();
            }
            if (conexionDestino.GetComponent<ChangeColorCables>() == null)
            {
                conexionDestino.AddComponent<ChangeColorCables>();
            }

            //Pasar energia a todos los cables
            conexionDestino.GetComponent<Plugs>().EstablecerPropiedadesConexionesEntrantesPrueba();
            conexionOrigen.GetComponent<Plugs>().EstablecerPropiedadesConexionesEntrantesPrueba();
            conexionDestino.GetComponent<Plugs>().Conectado = true;
            conexionOrigen.GetComponent<Plugs>().Conectado = true;
        }
        //Crear conexiones entre plugs
        conexionDestino.SendMessage("CrearConexionPlugs", false, SendMessageOptions.DontRequireReceiver);
        conexionOrigen.SendMessage("CrearConexionPlugs", false, SendMessageOptions.DontRequireReceiver);
    }

    /*Este étodo se encarga de eliminar la conexión entre dos plugs de dos módulos determinados.*/
    private bool ComprobarEliminarConexion(CableComponent cableCompStart, GameObject objectStart)
    {
        bool eliminarCable = false;
        if (cableCompStart.endPoint != null)//Si este valor es diferente a nulo, kquiere decir que este plug ya tiene una conexión
        {
            eliminarCable = true;
            GameObject endPoint = cableCompStart.endPoint;
            CableComponent cableCompLastEndPointStart = endPoint.GetComponent<CableComponent>();

            //Deterner energia a todos los cables
            objectStart.GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
            endPoint.GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
            objectStart.GetComponent<Plugs>().Conectado = false;
            endPoint.GetComponent<Plugs>().Conectado = false;

            Debug.Log("GameObject endPoint = cableCompStart.endPoint;");
            cableCompLastEndPointStart.endPoint = null;
            cableCompStart.endPoint = null;
            cableCompLastEndPointStart.showRender = true;
            cableCompStart.showRender = true;

            //Desturir elementos Line Render
            LineRenderer lineRenderEndPoint = endPoint.GetComponent<LineRenderer>();
            if (lineRenderEndPoint != null)
            {
                Destroy(lineRenderEndPoint);
                endPoint.AddComponent<LineRenderer>();
            }

            LineRenderer lineRenderStartPoint = objectStart.GetComponent<LineRenderer>();
            if (lineRenderStartPoint != null)
            {
                Destroy(lineRenderStartPoint);
                endPoint.AddComponent<LineRenderer>();
            }
        }
        return eliminarCable;
    }

    /*Este método se encarga de regresar el GameObject de un plug determinado, apartir del nombre del
     * plug y el módulo padre (Nodo raíz).*/
    /*private GameObject BuscarPlugConexion(string tipoModulo, GameObject modulo, string nombrePlug)
    {
        GameObject plugEncontrado = null;
        if (tipoModulo == "1")
        {
            Modulo1 mod1 = modulo.GetComponent<Modulo1>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod1.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod1.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "2")
        {
            Modulo2 mod2 = modulo.GetComponent<Modulo2>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod2.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod2.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "3")
        {
            Modulo3 mod3 = modulo.GetComponent<Modulo3>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod3.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod3.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "4")
        {
            Modulo4 mod4 = modulo.GetComponent<Modulo4>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod4.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod4.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "5")
        {
            Modulo5 mod5 = modulo.GetComponent<Modulo5>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod5.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod5.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "6")
        {
            Modulo6 mod6 = modulo.GetComponent<Modulo6>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod6.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod6.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "7")
        {
            Modulo7 mod7 = modulo.GetComponent<Modulo7>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod7.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod7.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "8, 11")
        {
            Modulo8_11 mod8_11 = modulo.GetComponent<Modulo8_11>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod8_11.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod8_11.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "9")
        {
            Modulo9 mod9 = modulo.GetComponent<Modulo9>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod9.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod9.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "10, 17, 18, 19")
        {
            Modulo10_17_18_19 mod10_17_18_19 = modulo.GetComponent<Modulo10_17_18_19>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod10_17_18_19.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod10_17_18_19.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "13")
        {
            Modulo13 mod13 = modulo.GetComponent<Modulo13>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod13.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod13.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "14, 16")
        {
            Modulo14_16 mod14_16 = modulo.GetComponent<Modulo14_16>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod14_16.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod14_16.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "15")
        {
            Modulo15 mod15 = modulo.GetComponent<Modulo15>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod15.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod15.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "20")
        {
            Modulo20 mod20 = modulo.GetComponent<Modulo20>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod20.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod20.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "21")
        {
            Modulo21 mod21 = modulo.GetComponent<Modulo21>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod21.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod21.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "22, 23")
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod22_23.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod22_23.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "22")
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod22_23.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod22_23.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "23")
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = mod22_23.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = mod22_23.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == "Potenciometro")
        {
            Potenciometro modPoten = modulo.GetComponent<Potenciometro>();
            if (Regex.IsMatch(nombrePlug, patronPlugAnaranjado))
            {
                plugEncontrado = modPoten.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, patronPlugNegro))
            {
                plugEncontrado = modPoten.plugNegrosDict[nombrePlug];
            }
        }
        else
        {
            Debug.LogError("BuscarPlugConexion - ProgressManager - No entra a ningun tipo, " + tipoModulo);
        }
        return plugEncontrado;
    }
    */

    /*Este método se encarga de regresar el diccionario de conexiones de un respectivo módulo.*/
    /*private Dictionary<string, string> ObtenerPlugConnections(string tipoModulo, GameObject modulo)
    {
        Dictionary<string, string> diccionario = null;
        if (tipoModulo == "1")
        {
            Modulo1 mod1 = modulo.GetComponent<Modulo1>();
            diccionario = mod1.plugsConnections;
        }
        else
        if (tipoModulo == "2")
        {
            Modulo2 mod2 = modulo.GetComponent<Modulo2>();
            diccionario = mod2.plugsConnections;
        }
        else
        if (tipoModulo == "3")
        {
            Modulo3 mod3 = modulo.GetComponent<Modulo3>();
            diccionario = mod3.plugsConnections;
        }
        else
        if (tipoModulo == "4")
        {
            Modulo4 mod4 = modulo.GetComponent<Modulo4>();
            diccionario = mod4.plugsConnections;
        }
        else
        if (tipoModulo == "5")
        {
            Modulo5 mod5 = modulo.GetComponent<Modulo5>();
            diccionario = mod5.plugsConnections;
        }
        else
        if (tipoModulo == "6")
        {
            Modulo6 mod6 = modulo.GetComponent<Modulo6>();
            diccionario = mod6.plugsConnections;
        }
        else
        if (tipoModulo == "7")
        {
            Modulo7 mod7 = modulo.GetComponent<Modulo7>();
            diccionario = mod7.plugsConnections;
        }
        else
        if (tipoModulo == "8, 11")
        {
            Modulo8_11 mod8_11 = modulo.GetComponent<Modulo8_11>();
            diccionario = mod8_11.plugsConnections;
        }
        else
        if (tipoModulo == "9")
        {
            Modulo9 mod9 = modulo.GetComponent<Modulo9>();
            diccionario = mod9.plugsConnections;
        }
        else
        if (tipoModulo == "10, 17, 18, 19")
        {
            Modulo10_17_18_19 mod10_17_18_19 = modulo.GetComponent<Modulo10_17_18_19>();
            diccionario = mod10_17_18_19.plugsConnections;
        }
        else
        if (tipoModulo == "13")
        {
            Modulo13 mod13 = modulo.GetComponent<Modulo13>();
            diccionario = mod13.plugsConnections;
        }
        else
        if (tipoModulo == "14, 16")
        {
            Modulo14_16 mod14_16 = modulo.GetComponent<Modulo14_16>();
            diccionario = mod14_16.plugsConnections;
        }
        else
        if (tipoModulo == "15")
        {
            Modulo15 mod15 = modulo.GetComponent<Modulo15>();
            diccionario = mod15.plugsConnections;
        }
        else
        if (tipoModulo == "20")
        {
            Modulo20 mod20 = modulo.GetComponent<Modulo20>();
            diccionario = mod20.plugsConnections;
        }
        else
        if (tipoModulo == "21")
        {
            Modulo21 mod21 = modulo.GetComponent<Modulo21>();
            diccionario = mod21.plugsConnections;
        }
        else
        if (tipoModulo == "22, 23")
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            diccionario = mod22_23.plugsConnections;
        }
        else
        if (tipoModulo == "22")
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            diccionario = mod22_23.plugsConnections;
        }
        else
        if (tipoModulo == "23")
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            diccionario = mod22_23.plugsConnections;
        }
        else
        if (tipoModulo == "Potenciometro")
        {
            Potenciometro modPoten = modulo.GetComponent<Potenciometro>();
            diccionario = modPoten.plugsConnections;
        }
        else
        {
            Debug.LogError("ObtenerPlugConnections - ProgressManager - No entra a ningun tipo, " + tipoModulo);
        }
        return diccionario;
    }
    */

    /*En este método se encarga de realizar la creación de los nuevos modulos y establecer sus carácteristicas,
      de acuerdo a un tipo de múdulo determinado.*/
    private GameObject CrearNuevoModulo(string tipo, Vector3 vectorPosicion, Vector3 vectorRotacion, string nombreModulo)
    {
        string ruta = "Assets/Prefabs/" + tipo + ".blend";
        //GameObject newModule = (GameObject)AssetDatabase.LoadAssetAtPath(ruta, typeof(GameObject));
        GameObject newModule = AuxiliarModulos.RegresarObjetoModulo(tipo);
        if (newModule != null)
        {
            newModule = Instantiate(newModule, vectorPosicion, Quaternion.Euler(vectorRotacion)) as GameObject;
            newModule.tag = tipo;
            newModule.name = nombreModulo;
            newModule.layer = AuxiliarModulos.capaModulos;
            newModule = AuxiliarModulos.AsignarLogicaModulo(newModule, tipo);
        }
        else
        {
            Debug.LogError(this.name + ", Error. GameObject CrearNuevoModulo(string tipo, Vector3 vectorPosicion, Vector3 vectorRotacion, string nombreModulo) - newModule es nulo.");
        }
        return newModule;
    }

    /*En este método se asigna la lógica de funcionamiento a un determinado módulo, de acuerdo a su tipo.*/
    /*private GameObject AsignarLogicaModulo(GameObject module, string tipoModulo)
    {
        if (tipoModulo == "1")
        {
            module.AddComponent<Modulo1>();
        }
        else
        if (tipoModulo == "2")
        {
            module.AddComponent<Modulo2>();
        }
        else
        if (tipoModulo == "3")
        {
            module.AddComponent<Modulo3>();
        }
        else
        if (tipoModulo == "4")
        {
            module.AddComponent<Modulo4>();
        }
        else
        if (tipoModulo == "5")
        {
            module.AddComponent<Modulo5>();
        }
        else
        if (tipoModulo == "6")
        {
            module.AddComponent<Modulo6>();
        }
        else
        if (tipoModulo == "7")
        {
            module.AddComponent<Modulo7>();
        }
        else
        if (tipoModulo == "8, 11")
        {
            module.AddComponent<Modulo8_11>();
        }
        else
        if (tipoModulo == "9")
        {
            module.AddComponent<Modulo9>();
        }
        else
        if (tipoModulo == "10, 17, 18, 19")
        {
            module.AddComponent<Modulo10_17_18_19>();
        }
        else
        if (tipoModulo == "13")
        {
            module.AddComponent<Modulo13>();
        }
        else
        if (tipoModulo == "14, 16")
        {
            module.AddComponent<Modulo14_16>();
        }
        else
        if (tipoModulo == "15")
        {
            module.AddComponent<Modulo15>();
        }
        else
        if (tipoModulo == "20")
        {
            module.AddComponent<Modulo20>();
        }
        else
        if (tipoModulo == "21")
        {
            module.AddComponent<Modulo21>();
        }
        else
        if (tipoModulo == "22, 23")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (tipoModulo == "22")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (tipoModulo == "23")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (tipoModulo == "Potenciometro")
        {
            module.AddComponent<Potenciometro>();
        }
        return module;
    }
    */

    /*Este método recursivo se utiliza para encontrar el padretotal (Nodo raíz) de un componente*/
    private void EncontrarPadreTotal(GameObject nodo)
    {
        if (nodo.transform.parent == null)
        {
            padreTotal = nodo;
        }
        else
        {
            GameObject padre = nodo.transform.parent.gameObject;
            EncontrarPadreTotal(padre);
        }
    }

    /*En este método se asigna la lógica de funcionamiento a los componentes de un determinado módulo´,
     * de acuerdo a su tipo.*/
    private void AsignacionRecursivaLogicaComponentes(GameObject nodo)
    {
        nodo.layer = AuxiliarModulos.capaModulos;
        if (nodo.name.Contains("BaseModulo"))
        {
            nodo.AddComponent<OpenCloseChangeModule>();
            OpenCloseChangeModule OCChangeModule = nodo.GetComponent<OpenCloseChangeModule>();
            OCChangeModule.panel = panel;
            OCChangeModule.dropdown = dropdown;
        }
        else if (nodo.name.Contains("Total_Perilla"))
        {
            nodo.AddComponent<OpenClosePerillas>();
        }
        else if (nodo.name.Contains("PerillaPotenciometro"))
        {
            nodo.AddComponent<OpenClosePerillas>();
        }
        int numeroDeHijoss = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijoss; i++)
        {
            AsignacionRecursivaLogicaComponentes(nodo.transform.GetChild(i).gameObject);
        }
    }

    /*Este método recursivo se utiliza para eliminar los componetes en cascada aparitr de un objeto.*/
    private void DestruirObjetosRecursivos(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            DestruirObjetosRecursivos(nodo.transform.GetChild(i).gameObject);
        }
        Destroy(nodo);
    }

    public string ObtenerCadenaJson(string path)
    {
        string jsonString;
        using (StreamReader streamReader = File.OpenText(path))
        {
            jsonString = streamReader.ReadToEnd();
        }
        jsonString = jsonString.Trim();
        if (cifrar)
        {
            jsonString = EncryptStringSample.StringCipher.Decrypt(jsonString, palabraClave);
        }
        return jsonString;
    }

    /*Este método se encarga de comprobar si un Json tiene un formato válido.*/
    public bool ComprobarValidezJson(string path)
    {
        bool info = false;
        string jsonString = ObtenerCadenaJson(path);
        if (IsValidJson(jsonString))
        {
            info = true;
        }
        return info;
    }

    /*Este método se encarga de comprobar si un Json tiene un formato válido.*/
    private static bool IsValidJson(string strInput)
    {
        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
            (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
        {
            try
            {
                object hola = JsonConvert.DeserializeObject(strInput);
                return true;
            }
            catch (JsonReaderException jex)
            {
                //Exception in parsing json
                Debug.Log(jex.Message);
                return false;
            }
            catch (Exception ex) //some other exception
            {
                Debug.Log(ex.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /*Este método se encarga de mostrar todo el contenido de un Json separado por tipo.*/
    void AccessData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    Debug.Log(key);
                    AccessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list)
                {
                    AccessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                Debug.Log(obj.str);
                break;
            case JSONObject.Type.NUMBER:
                Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;

        }
    }

    /*Este método se encarga de comprobar si la ruta de un archivo o carpeta existe, es decir, si es válida.*/
    public bool ComprobarExistenciaArchivo(string path)
    {
        bool existeArchivo = false;
        if (File.Exists(path))
        {
            existeArchivo = true;
        }
        return existeArchivo;
    }

    /*Este método se encarga de generar el archivo Json de acuerdo al estado actual del simulador y 
     * guardar dicho contenido en disco.*/
    public void SaveSimulator(string path, string nameFile)
    {
        string jsonMod1 = GenerarJsonModulo1();
        string jsonMod2 = GenerarJsonModulo2();
        string jsonMod3 = GenerarJsonModulo3();
        string jsonMod4 = GenerarJsonModulo4();
        string jsonMod5 = GenerarJsonModulo5();
        string jsonMod6 = GenerarJsonModulo6();
        string jsonMod7 = GenerarJsonModulo7();
        string jsonMod8_11 = GenerarJsonModulo8_11();
        string jsonMod9 = GenerarJsonModulo9();
        string jsonMod10_17_18_19 = GenerarJsonModulo10_17_18_19();
        string jsonMod13 = GenerarJsonModulo13();
        string jsonMod14_16 = GenerarJsonModulo14_16();
        string jsonMod15 = GenerarJsonModulo15();
        string jsonMod20 = GenerarJsonModulo20();
        string jsonMod21 = GenerarJsonModulo21();
        string jsonMod22 = GenerarJsonModulo22();
        string jsonMod23 = GenerarJsonModulo23();
        string jsonModPotenciometro = GenerarJsonModuloPotenciometro();
        string jsonModMulticonector = GenerarJsonModuloMulticonector();
        string jsonModVacio = GenerarJsonModuloVacio();
        string json = "{";
        if (jsonMod1.Length != 0)
        {
            json += jsonMod1 + ",\n";
        }
        if (jsonMod2.Length != 0)
        {
            json += jsonMod2 + ",\n";
        }
        if (jsonMod3.Length != 0)
        {
            json += jsonMod3 + ",\n";
        }
        if (jsonMod4.Length != 0)
        {
            json += jsonMod4 + ",\n";
        }
        if (jsonMod5.Length != 0)
        {
            json += jsonMod5 + ",\n";
        }
        if (jsonMod6.Length != 0)
        {
            json += jsonMod6 + ",\n";
        }
        if (jsonMod7.Length != 0)
        {
            json += jsonMod7 + ",\n";
        }
        if (jsonMod9.Length != 0)
        {
            json += jsonMod9 + ",\n";
        }
        if (jsonMod8_11.Length != 0)
        {
            json += jsonMod8_11 + ",\n";
        }
        if (jsonMod10_17_18_19.Length != 0)
        {
            json += jsonMod10_17_18_19 + ",\n";
        }
        if (jsonMod13.Length != 0)
        {
            json += jsonMod13 + ",\n";
        }
        if (jsonMod14_16.Length != 0)
        {
            json += jsonMod14_16 + ",\n";
        }
        if (jsonMod15.Length != 0)
        {
            json += jsonMod15 + ",\n";
        }
        if (jsonMod20.Length != 0)
        {
            json += jsonMod20 + ",\n";
        }
        if (jsonMod21.Length != 0)
        {
            json += jsonMod21 + ",\n";
        }
        if (jsonMod22.Length != 0)
        {
            json += jsonMod22 + ",\n";
        }
        if (jsonMod23.Length != 0)
        {
            json += jsonMod23 + ",\n";
        }
        if (jsonModPotenciometro.Length != 0)
        {
            json += jsonModPotenciometro + ",\n";
        }
        if (jsonModMulticonector.Length != 0)
        {
            json += jsonModMulticonector + ",\n";
        }
        if (jsonModVacio.Length != 0)
        {
            json += jsonModVacio;
        }
        if (json[json.Length - 1] == ',')
        {
            json.Remove(json.Length - 1);
        }
        json += "}";
        Debug.LogError(json);
        if (cifrar)
        {
            json = EncryptStringSample.StringCipher.Encrypt(json, palabraClave);
        }
        if (File.Exists(path))
        {
            Debug.LogError("El archivo ya existe y si continua el contenido anterior se perderá.");
            bool continuarGuardado = true;
            if (continuarGuardado)
            {
                using (StreamWriter streamWriter = File.CreateText(path))
                {
                    streamWriter.Write(json);
                }
                Debug.Log("Archivo guardado correctamente." + rutaGuardado);
                nombreArchivoGuardado = nameFile;
            }
            else
            {
                Debug.Log("Cambie el nombre de sua rchivo de guardado, por uno diferente.");
            }
        }
        else
        {
            using (StreamWriter streamWriter = File.CreateText(path))
            {
                streamWriter.Write(json);
            }
            Debug.Log("Archivo guardado correctamente." + rutaGuardado);
            nombreArchivoGuardado = nameFile;
        }
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "1", 
     * existentes en el simulador.*/
    string GenerarJsonModulo1()
    {
        GameObject[] modulos1 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod1);
        string jsonModulo1 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos1.Length;
        foreach (GameObject modulo in modulos1)
        {
            Modulo1 mod1 = modulo.GetComponent<Modulo1>();
            jsonModulo1 += "\"" + modulo.name + "\":{\n";
            jsonModulo1 += "\"Tipo\":\"" + AuxiliarModulos.tagMod1 + "\",\n";
            jsonModulo1 += "\"Voltaje\":" + mod1.voltajeModulo + ",\n";
            jsonModulo1 += PosicionModulo(modulo) + ",\n";
            jsonModulo1 += "\"Conexiones\":{" + CrearConexionesModulo(mod1.plugsConnections, modulo) + "}";
            jsonModulo1 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo1 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo1);
        }
        return jsonModulo1;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "2", 
    * existentes en el simulador.*/
    string GenerarJsonModulo2()
    {
        GameObject[] modulos2 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod2);
        string jsonModulo2 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos2.Length;
        foreach (GameObject modulo in modulos2)
        {
            Modulo2 mod2 = modulo.GetComponent<Modulo2>();
            jsonModulo2 += "\"" + modulo.name + "\":{\n";
            jsonModulo2 += "\"Tipo\":\"" + AuxiliarModulos.tagMod2 + "\",\n";
            jsonModulo2 += PosicionModulo(modulo) + ",\n";
            jsonModulo2 += "\"Conexiones\":{" + CrearConexionesModulo(mod2.plugsConnections, modulo) + "}";
            jsonModulo2 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo2 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo2);
        }
        return jsonModulo2;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "3", 
    * existentes en el simulador.*/
    string GenerarJsonModulo3()
    {
        GameObject[] modulos3 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod3);
        string jsonModulo3 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos3.Length;
        foreach (GameObject modulo in modulos3)
        {
            Modulo3 mod3 = modulo.GetComponent<Modulo3>();
            jsonModulo3 += "\"" + modulo.name + "\":{\n";
            jsonModulo3 += "\"Tipo\":\"" + AuxiliarModulos.tagMod3 + "\",\n";
            jsonModulo3 += PosicionModulo(modulo) + ",\n";
            jsonModulo3 += "\"Conexiones\":{" + CrearConexionesModulo(mod3.plugsConnections, modulo) + "}";
            jsonModulo3 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo3 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo3);
        }
        return jsonModulo3;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "4", 
    * existentes en el simulador.*/
    string GenerarJsonModulo4()
    {
        GameObject[] modulos4 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod4);
        string jsonModulo4 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos4.Length;
        foreach (GameObject modulo in modulos4)
        {
            Modulo4 mod4 = modulo.GetComponent<Modulo4>();
            jsonModulo4 += "\"" + modulo.name + "\":{\n";
            jsonModulo4 += "\"Tipo\":\"" + AuxiliarModulos.tagMod4 + "\",\n";
            jsonModulo4 += PosicionModulo(modulo) + ",\n";
            jsonModulo4 += "\"Conexiones\":{" + CrearConexionesModulo(mod4.plugsConnections, modulo) + "}";
            jsonModulo4 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo4 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo4);
        }
        return jsonModulo4;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "5", 
    * existentes en el simulador.*/
    string GenerarJsonModulo5()
    {
        GameObject[] modulos5 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod5);
        string jsonModulo5 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos5.Length;
        foreach (GameObject modulo in modulos5)
        {
            Modulo5 mod5 = modulo.GetComponent<Modulo5>();
            jsonModulo5 += "\"" + modulo.name + "\":{\n";
            jsonModulo5 += "\"Tipo\":\"" + AuxiliarModulos.tagMod5 + "\",\n";
            jsonModulo5 += PosicionModulo(modulo) + ",\n";
            jsonModulo5 += "\"Conexiones\":{" + CrearConexionesModulo(mod5.plugsConnections, modulo) + "}";
            jsonModulo5 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo5 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo5);
        }
        return jsonModulo5;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "6", 
    * existentes en el simulador.*/
    string GenerarJsonModulo6()
    {
        GameObject[] modulos6 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod6);
        string jsonModulo6 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos6.Length;
        foreach (GameObject modulo in modulos6)
        {
            Modulo6 mod6 = modulo.GetComponent<Modulo6>();
            jsonModulo6 += "\"" + modulo.name + "\":{\n";
            jsonModulo6 += "\"Tipo\":\"" + AuxiliarModulos.tagMod6 + "\",\n";
            jsonModulo6 += "\"ValorPerilla\":" + mod6.valorActualPerilla + ",\n";
            jsonModulo6 += PosicionModulo(modulo) + ",\n";
            jsonModulo6 += "\"Conexiones\":{" + CrearConexionesModulo(mod6.plugsConnections, modulo) + "}";
            jsonModulo6 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo6 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo6);
        }
        return jsonModulo6;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "7", 
    * existentes en el simulador.*/
    string GenerarJsonModulo7()
    {
        GameObject[] modulos7 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod7);
        string jsonModulo7 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos7.Length;
        foreach (GameObject modulo in modulos7)
        {
            Modulo7 mod7 = modulo.GetComponent<Modulo7>();
            jsonModulo7 += "\"" + modulo.name + "\":{\n";
            jsonModulo7 += "\"Tipo\":\"" + AuxiliarModulos.tagMod7 + "\",\n";
            jsonModulo7 += "\"ValorPerilla\":" + mod7.valorActualPerilla + ",\n";
            jsonModulo7 += PosicionModulo(modulo) + ",\n";
            jsonModulo7 += "\"Conexiones\":{" + CrearConexionesModulo(mod7.plugsConnections, modulo) + "}";
            jsonModulo7 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo7 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo7);
        }
        return jsonModulo7;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "8, 11", 
    * existentes en el simulador.*/
    string GenerarJsonModulo8_11()
    {
        GameObject[] modulos8_11 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagtagMod8_11);
        string jsonModulo8_11 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos8_11.Length;
        foreach (GameObject modulo in modulos8_11)
        {
            Modulo8_11 mod8_11 = modulo.GetComponent<Modulo8_11>();
            jsonModulo8_11 += "\"" + modulo.name + "\":{\n";
            jsonModulo8_11 += "\"Tipo\":\"" + AuxiliarModulos.tagtagMod8_11 + "\",\n";
            jsonModulo8_11 += PosicionModulo(modulo) + ",\n";
            jsonModulo8_11 += "\"Conexiones\":{" + CrearConexionesModulo(mod8_11.plugsConnections, modulo) + "}";
            jsonModulo8_11 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo8_11 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo8_11);
        }
        return jsonModulo8_11;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "9", 
    * existentes en el simulador.*/
    string GenerarJsonModulo9()
    {
        GameObject[] modulos9 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod9);
        string jsonModulo9 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos9.Length;
        foreach (GameObject modulo in modulos9)
        {
            Modulo9 mod9 = modulo.GetComponent<Modulo9>();
            jsonModulo9 += "\"" + modulo.name + "\":{\n";
            jsonModulo9 += "\"Tipo\":\"" + AuxiliarModulos.tagMod9 + "\",\n";
            jsonModulo9 += PosicionModulo(modulo) + ",\n";
            jsonModulo9 += "\"Conexiones\":{" + CrearConexionesModulo(mod9.plugsConnections, modulo) + "}";
            jsonModulo9 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo9 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo9);
        }
        return jsonModulo9;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "10, 17, 18, 19", 
    * existentes en el simulador.*/
    string GenerarJsonModulo10_17_18_19()
    {
        GameObject[] modulos10_17_18_19 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod10_17_18_19);
        string jsonModulo10_17_18_19 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos10_17_18_19.Length;
        foreach (GameObject modulo in modulos10_17_18_19)
        {
            Modulo10_17_18_19 mod10_17_18_19 = modulo.GetComponent<Modulo10_17_18_19>();
            jsonModulo10_17_18_19 += "\"" + modulo.name + "\":{\n";
            jsonModulo10_17_18_19 += "\"Tipo\":\"" + AuxiliarModulos.tagMod10_17_18_19 + "\",\n";
            jsonModulo10_17_18_19 += PosicionModulo(modulo) + ",\n";
            jsonModulo10_17_18_19 += "\"Conexiones\":{" + CrearConexionesModulo(mod10_17_18_19.plugsConnections, modulo) + "}";
            jsonModulo10_17_18_19 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo10_17_18_19 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo10_17_18_19);
        }
        return jsonModulo10_17_18_19;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "13", 
    * existentes en el simulador.*/
    string GenerarJsonModulo13()
    {
        GameObject[] modulos13 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod13);
        string jsonModulo13 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos13.Length;
        foreach (GameObject modulo in modulos13)
        {
            Modulo13 mod13 = modulo.GetComponent<Modulo13>();
            jsonModulo13 += "\"" + modulo.name + "\":{\n";
            jsonModulo13 += "\"Tipo\":\"" + AuxiliarModulos.tagMod13 + "\",\n";
            jsonModulo13 += PosicionModulo(modulo) + ",\n";
            jsonModulo13 += "\"Conexiones\":{" + CrearConexionesModulo(mod13.plugsConnections, modulo) + "}";
            jsonModulo13 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo13 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo13);
        }
        return jsonModulo13;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "14, 16", 
    * existentes en el simulador.*/
    string GenerarJsonModulo14_16()
    {
        GameObject[] modulos14_16 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod14_16);
        string jsonModulo14_16 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos14_16.Length;
        foreach (GameObject modulo in modulos14_16)
        {
            Modulo14_16 mod14_16 = modulo.GetComponent<Modulo14_16>();
            jsonModulo14_16 += "\"" + modulo.name + "\":{\n";
            jsonModulo14_16 += "\"Tipo\":\"" + AuxiliarModulos.tagMod14_16 + "\",\n";
            jsonModulo14_16 += PosicionModulo(modulo) + ",\n";
            jsonModulo14_16 += "\"Conexiones\":{" + CrearConexionesModulo(mod14_16.plugsConnections, modulo) + "}";
            jsonModulo14_16 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo14_16 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo14_16);
        }
        return jsonModulo14_16;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "15", 
    * existentes en el simulador.*/
    string GenerarJsonModulo15()
    {
        GameObject[] modulos15 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod15);
        string jsonModulo15 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos15.Length;
        foreach (GameObject modulo in modulos15)
        {
            Modulo15 mod15 = modulo.GetComponent<Modulo15>();
            jsonModulo15 += "\"" + modulo.name + "\":{\n";
            jsonModulo15 += "\"Tipo\":\"" + AuxiliarModulos.tagMod15 + "\",\n";
            jsonModulo15 += "\"Voltaje\":" + mod15.voltajeModulo + ",\n";
            jsonModulo15 += PosicionModulo(modulo) + ",\n";
            jsonModulo15 += "\"Conexiones\":{" + CrearConexionesModulo(mod15.plugsConnections, modulo) + "}";
            jsonModulo15 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo15 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo15);
        }
        return jsonModulo15;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "20", 
    * existentes en el simulador.*/
    string GenerarJsonModulo20()
    {
        GameObject[] modulos20 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod20);
        string jsonModulo20 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos20.Length;
        foreach (GameObject modulo in modulos20)
        {
            Modulo20 mod20 = modulo.GetComponent<Modulo20>();
            jsonModulo20 += "\"" + modulo.name + "\":{\n";
            jsonModulo20 += "\"Tipo\":\"" + AuxiliarModulos.tagMod20 + "\",\n";
            jsonModulo20 += PosicionModulo(modulo) + ",\n";
            jsonModulo20 += "\"Conexiones\":{" + CrearConexionesModulo(mod20.plugsConnections, modulo) + "}";
            jsonModulo20 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo20 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo20);
        }
        return jsonModulo20;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "21", 
    * existentes en el simulador.*/
    string GenerarJsonModulo21()
    {
        GameObject[] modulos21 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod21);
        string jsonModulo21 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos21.Length;
        foreach (GameObject modulo in modulos21)
        {
            Modulo21 mod21 = modulo.GetComponent<Modulo21>();
            jsonModulo21 += "\"" + modulo.name + "\":{\n";
            jsonModulo21 += "\"Tipo\":\"" + AuxiliarModulos.tagMod21 + "\",\n";
            jsonModulo21 += PosicionModulo(modulo) + ",\n";
            jsonModulo21 += "\"Conexiones\":{" + CrearConexionesModulo(mod21.plugsConnections, modulo) + "}";
            jsonModulo21 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo21 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo21);
        }
        return jsonModulo21;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "22", 
    * existentes en el simulador.*/
    string GenerarJsonModulo22()
    {
        GameObject[] modulos22 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod22);
        string jsonModulo22 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos22.Length;
        foreach (GameObject modulo in modulos22)
        {
            Modulo22_23 mod22 = modulo.GetComponent<Modulo22_23>();
            jsonModulo22 += "\"" + modulo.name + "\":{\n";
            jsonModulo22 += "\"Tipo\":\"" + AuxiliarModulos.tagMod22 + "\",\n";
            jsonModulo22 += PosicionModulo(modulo) + ",\n";
            jsonModulo22 += "\"Conexiones\":{" + CrearConexionesModulo(mod22.plugsConnections, modulo) + "}";
            jsonModulo22 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo22 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo22);
        }
        return jsonModulo22;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "23", 
    * existentes en el simulador.*/
    string GenerarJsonModulo23()
    {
        GameObject[] modulos23 = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagMod23);
        string jsonModulo23 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos23.Length;
        foreach (GameObject modulo in modulos23)
        {
            Modulo22_23 mod23 = modulo.GetComponent<Modulo22_23>();
            jsonModulo23 += "\"" + modulo.name + "\":{\n";
            jsonModulo23 += "\"Tipo\":\"" + AuxiliarModulos.tagMod23 + "\",\n";
            jsonModulo23 += PosicionModulo(modulo) + ",\n";
            jsonModulo23 += "\"Conexiones\":{" + CrearConexionesModulo(mod23.plugsConnections, modulo) + "}";
            jsonModulo23 += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModulo23 += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModulo23);
        }
        return jsonModulo23;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "Potenciometro", 
    * existentes en el simulador.*/
    string GenerarJsonModuloPotenciometro()
    {
        GameObject[] modulosPotenciometro = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagModPotenciometro);
        string jsonModuloPoten = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulosPotenciometro.Length;
        foreach (GameObject modulo in modulosPotenciometro)
        {
            Potenciometro modPoten = modulo.GetComponent<Potenciometro>();
            jsonModuloPoten += "\"" + modulo.name + "\":{\n";
            jsonModuloPoten += "\"Tipo\":\"" + AuxiliarModulos.tagModPotenciometro + "\",\n";
            jsonModuloPoten += "\"ValorPerilla\":" + modPoten.valorActualPerilla + ",\n";
            jsonModuloPoten += PosicionModulo(modulo) + ",\n";
            jsonModuloPoten += "\"Conexiones\":{" + CrearConexionesModulo(modPoten.plugsConnections, modulo) + "}";
            jsonModuloPoten += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModuloPoten += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModuloPoten);
        }
        return jsonModuloPoten;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "Multiconector", 
    * existentes en el simulador.*/
    string GenerarJsonModuloMulticonector()
    {
        GameObject[] modulosMulti = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagModMulticonector);
        string jsonModuloMulti = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulosMulti.Length;
        foreach (GameObject modulo in modulosMulti)
        {
            Multiconector modMulticonector = modulo.GetComponent<Multiconector>();
            jsonModuloMulti += "\"" + modulo.name + "\":{\n";
            jsonModuloMulti += "\"Tipo\":\"" + AuxiliarModulos.tagModMulticonector + "\",\n";
            jsonModuloMulti += PosicionModulo(modulo) + ",\n";
            jsonModuloMulti += "\"Conexiones\":{" + CrearConexionesModulo(modMulticonector.plugsConnections, modulo) + "}";
            jsonModuloMulti += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModuloMulti += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModuloMulti);
        }
        return jsonModuloMulti;
    }

    /*Este método se encarga de generar la representación Json de todos los módulos de tipo "ModuloVacio", 
    * existentes en el simulador.*/
    string GenerarJsonModuloVacio()
    {
        GameObject[] modulosVacio = GameObject.FindGameObjectsWithTag(AuxiliarModulos.tagModVacio);
        string jsonModuloPoten = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulosVacio.Length;
        foreach (GameObject modulo in modulosVacio)
        {
            jsonModuloPoten += "\"" + modulo.name + "\":{\n";
            jsonModuloPoten += "\"Tipo\":\"" + AuxiliarModulos.tagModVacio + "\",\n";
            jsonModuloPoten += PosicionModulo(modulo) + "\n";
            jsonModuloPoten += "}";
            if (numeroDeModulos != numeroMaximoModulos)
            {
                jsonModuloPoten += ",\n";
            }
            numeroDeModulos++;
        }
        if (debug)
        {
            Debug.Log(jsonModuloPoten);
        }
        return jsonModuloPoten;
    }

    /*Este método se encarga de generar la representación Json de la posición de un GameObject determinado.*/
    string PosicionModulo(GameObject modulo)
    {
        string jsonPosicion = "";
        if (modulo != null)
        {
            jsonPosicion += "\"Posicion\":{\n";
            jsonPosicion += "\"X\":" + modulo.transform.position.x + ",\n";
            jsonPosicion += "\"Y\":" + modulo.transform.position.y + ",\n";
            jsonPosicion += "\"Z\":" + modulo.transform.position.z + "\n}";
        }
        else
        {
            Debug.LogError(this.name + ", Error. PosicionModulo(GameObject modulo) - modulo es nulo.");
        }
        
        return jsonPosicion;
    }

    /*Este método se encarga de generar la representación Json de todas las conexiones de un módulo a 
     * partir de un dicctionario de conexiones.*/
    string CrearConexionesModulo(Dictionary<string, string> conexionesDict, GameObject modulo)
    {
        string jsonConexiones = "";
        int numeroConexion = 1;
        int numeroMaximoConexiones = conexionesDict.Count;
        if(conexionesDict != null && modulo != null)
        {
            foreach (KeyValuePair<string, string> entry in conexionesDict)
            {
                string colorCable = ColorCable(entry.Key, modulo);
                jsonConexiones += "\"Conexion" + numeroConexion + "\":{\n";
                jsonConexiones += "\"Origen\": \"" + entry.Key + "\",\n";
                jsonConexiones += "\"Destino\": \"" + entry.Value + "\",\n";
                jsonConexiones += "\"Color\": {" + colorCable + "}\n}";
                if (numeroConexion != numeroMaximoConexiones)
                {
                    jsonConexiones += ",\n";
                }
                numeroConexion++;
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. CrearConexionesModulo(Dictionary<string, string> conexionesDict, GameObject modulo) - conexionesDict o modulo es nulo.");
        }
        return jsonConexiones;
    }

    private string ColorCable(string plugOrigenString, GameObject modulo)
    {
        string colorString = "";
        string[] parametrosConexionOrigen = plugOrigenString.ToString().Split('|');
        GameObject plugOrigen = AuxiliarModulos.BuscarPlugConexion(modulo.tag, modulo, parametrosConexionOrigen[1]);
        CableComponent camble;
        if (plugOrigen != null)
        {
            camble = plugOrigen.GetComponent<CableComponent>();
            if (camble != null)
            {
                //LineRenderer line = camble.line;
                if (camble != null)
                {
                    //Color color = line.startColor;
                    Color color = camble.endColor;
                    colorString += "\"R\": " + color.r + ",\n";
                    colorString += "\"G\": " + color.g + ",\n";
                    colorString += "\"B\": " + color.b + ",\n";
                    colorString += "\"A\": " + color.a + "\n";
                    //colorString = color.ToString();
                }
                else
                {
                    colorString += "\"R\": " + 0 + ",\n";
                    colorString += "\"G\": " + 0 + ",\n";
                    colorString += "\"B\": " + 0 + ",\n";
                    colorString += "\"A\": " + 1 + "\n";
                }
            }
        }
        return colorString;
    }
    #endregion
}
