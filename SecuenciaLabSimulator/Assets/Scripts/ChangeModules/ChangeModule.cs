using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

public class ChangeModule : MonoBehaviour
{
    #region Atributos
    //Atributos de componentes no alterables
        //GUI
    public Button buttonChangeModule;
    public Dropdown dropdown;
    public Text myText;
    public Text textInfoChangeModule;
        //Elementos interactuables
    public GameObject panel;
    public GameObject player;
    //Variables
    public GameObject selectedGameObjectRightClick;
    public GameObject modulesList;
    private GameObject padreTotal;
    //Constantes
    public int moduleLayer = 11; //La capa 11, equivale a la capa "Modulo"
    public int totalModulosSimulador = 28;
    //Debug
    public bool debug = false;
    #endregion

    #region Inicializacion
    private void Awake()
    {
        
    }

    void Start()
    {
        //Busqueda de todos los elementos necesarios para el cambio de modulos
        if(player == null)
        {
            player = GameObject.Find("FirstPersonCharacter");
        }
        if (dropdown == null)
        {
            dropdown = GameObject.Find("DropdownChangeModule").GetComponent<Dropdown>();
        }
        if (panel == null)
        {
            panel = GameObject.Find("PanelChangeModule");
        }
        if (buttonChangeModule == null)
        {
            buttonChangeModule = GameObject.Find("ButtonChangeModule").GetComponent<Button>();
        }
        if (modulesList == null)
        {
            modulesList = GameObject.Find("ModulesGroup");
        }
        if (textInfoChangeModule == null)
        {
            textInfoChangeModule = GameObject.Find("TextInfoChangeModule").GetComponent<Text>();
        }
        if (myText == null)
        {
            myText = GameObject.Find("TextChangeModule").GetComponent<Text>();
            myText.color = Color.clear;
        }
    }
    #endregion
    
    #region Comportamiento
    // Update is called once per frame
    void Update()
    {

    }

    /*Este método se utiliza para comporbar las restrcciones en la creación y remplazo de módulos.*/
    public void ComprobarRestricciones()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        //GameObject module = clickDetector.lastClickedGmObj;
        GameObject module = dropdown.GetComponent<ChangeModule>().selectedGameObjectRightClick;

        bool nuevoModuloNoRestringido = true;
        int m_DropdownValue = dropdown.value;
        string nombreModuloNuevo = dropdown.options[m_DropdownValue].text;
        if (nombreModuloNuevo == "20" || nombreModuloNuevo == "21")
        {
            GameObject[] listaMismosModulos = GameObject.FindGameObjectsWithTag(nombreModuloNuevo);
            if (listaMismosModulos.Length >= 1)
            {
                nuevoModuloNoRestringido = false;
            }
        }
        if (nuevoModuloNoRestringido)
        {
            EncontrarPadreTotal(module);
            if (padreTotal != null && padreTotal.tag == nombreModuloNuevo)
            {
                string mensajePreMismoModulo = "";
                mensajePreMismoModulo = "padreTotal.tag: " + padreTotal.tag + ", nombreModuloNuevo: " + nombreModuloNuevo + ", Precacición. Tiene seleccionado el mismo tipo de modulo que el modulo actual, por lo tanto no se puede realizar el cambio de modulo.";
                Debug.LogWarning(mensajePreMismoModulo);
                textInfoChangeModule.text = "Información: " + mensajePreMismoModulo;
                buttonChangeModule.enabled = false;
            }
            else if (nombreModuloNuevo == "22")
            {
                if ((SaberPosicionModuloListaModulos(padreTotal) % 4) == 0)
                {
                    string mensajeErrorModDobleDer = "";
                    mensajeErrorModDobleDer = "Error. No se puede crear este modulo en la posicion actual, elija una posición más a la izquierda. Además, el modulo de la derecha del modulo selccionado tambien se sustituirá.";
                    Debug.LogError(mensajeErrorModDobleDer);
                    textInfoChangeModule.text = "Información: " + mensajeErrorModDobleDer;
                    buttonChangeModule.enabled = false;
                    Debug.LogError("(saberPosicionModuloDoble()%4)==0");
                }
                else
                {
                    textInfoChangeModule.text = "Información: Ok. El modulo a la derecha del modulo seleccionado será tambien sustituido.";
                    buttonChangeModule.enabled = true;
                }
            }
            else if (nombreModuloNuevo == "23")
            {
                int posicion = SaberPosicionModuloListaModulos(padreTotal);
                if (posicion == 1 || posicion == 5 || posicion == 9 || posicion == 13 || posicion == 17 || posicion == 21 || posicion == 25)
                {
                    string mensajeErrorModDobleDer = "";
                    mensajeErrorModDobleDer = "Error. No se puede crear este modulo en la posicion actual, elija una posición más a la derecha. Además, el modulo de la derecha del modulo selccionado tambien se sustituirá.";
                    Debug.LogError(mensajeErrorModDobleDer);
                    textInfoChangeModule.text = "Información: " + mensajeErrorModDobleDer;
                    buttonChangeModule.enabled = false;
                    Debug.LogError("if (posicion == 1 || posicion == 5 || posicion == 9 || posicion == 13 || posicion == 17 || posicion == 21 || posicion == 25)ss");
                }
                else
                {
                    textInfoChangeModule.text = "Información: Ok. El modulo a la izquierda del modulo seleccionado será tambien sustituido.";
                    buttonChangeModule.enabled = true;
                }
            }
            else
            {
                textInfoChangeModule.text = "Información: Ok";
                buttonChangeModule.enabled = true;
            }
            if (debug)
            {
                Debug.LogError("Entra al if, nombreModuloNuevo: " + nombreModuloNuevo + ", padreTotal.tag: " + padreTotal.tag);
            }
        }
        else
        {
            string mensajeErrorModRestringidos = "";
            mensajeErrorModRestringidos = "Error. No se puede crear otro modulo " + nombreModuloNuevo + ", solo puede existir a la vez un modulo de ese tipo en el simulador. Intente crear un modulo de otro tipo.";
            Debug.LogError(mensajeErrorModRestringidos);
            textInfoChangeModule.text = "Información: " + mensajeErrorModRestringidos;
            buttonChangeModule.enabled = false;
        }
        if (debug)
        {
            Debug.LogError("Finaliza comrpobar estado");
        }
    }

    /*Este método es el encargado de iniciar y controlar el remplazo de módulos a partir de la selección realizada en
     * el dropDownMenu.*/
    public void ReplaceModule()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        //GameObject module = clickDetector.lastClickedGmObj;
        GameObject module = dropdown.GetComponent<ChangeModule>().selectedGameObjectRightClick;
        int m_DropdownValue = dropdown.value;
        string tipoNuevoModulo = dropdown.options[m_DropdownValue].text;
        GameObject newModule;
        //GameObject selectedGameObjectRightClick;
        //selectedGameObjectRightClick = GameObject.Find("SelectedGameObjectRightClick");
        module = selectedGameObjectRightClick;
        if (debug)
        {
            Debug.Log("***********Modulo seleccionado: " + module);
        }
        EncontrarPadreTotal(module);
        int posicion = SaberPosicionModuloListaModulos(padreTotal);
        if (debug)
        {
            Debug.Log("***********Modulo padre: " + padreTotal.name);
        }
        string nombrePadreTotalViejo = padreTotal.name;
        string tipoPadreTotalViejo = padreTotal.tag;
        //Restricciones de creación módulos 22 y 23
        if (tipoPadreTotalViejo == "22")//Destruir objeto de la derecha
        {
            ModulesList modelsList = modulesList.GetComponent<ModulesList>();
            Debug.Log("posicion: " + posicion);
            GameObject moduloDerecho = modelsList.modulesGroup[(posicion - 1) + 1].modelSystemGO;
            EncontrarPadreTotal(moduloDerecho);
            nombrePadreTotalViejo = padreTotal.name;
            DestruirObjetosRecursivos(padreTotal);
            newModule = CrearNuevoModuloTipoDado("ModuloVacio");
            AsignacionRecursivaLogicaComponentes(newModule);
            SustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
            Debug.LogError("if (tipoPadreTotalViejo == 22)");
        }
        else if (tipoPadreTotalViejo == "23")//Destruir objeto de la izquierda
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            Debug.Log("posicion: " + posicion);
            GameObject moduloDerecho = modelusList.modulesGroup[(posicion - 1) - 1].modelSystemGO;
            EncontrarPadreTotal(moduloDerecho);
            nombrePadreTotalViejo = padreTotal.name;
            DestruirObjetosRecursivos(padreTotal);
            newModule = CrearNuevoModuloTipoDado("ModuloVacio");
            AsignacionRecursivaLogicaComponentes(newModule);
            SustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
            Debug.LogError("if (tipoPadreTotalViejo == 23)");
        }
        EncontrarPadreTotal(module);
        nombrePadreTotalViejo = padreTotal.name;
        DestruirObjetosRecursivos(padreTotal);
        newModule = CrearNuevoModuloDropDown();
        AsignacionRecursivaLogicaComponentes(newModule);
        SustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
        //Restricciones de destrucción módulos 22 y 23
        if (tipoNuevoModulo == "22")
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            Debug.Log("posicion: " + posicion);
            GameObject moduloDerecho = modelusList.modulesGroup[(posicion - 1) + 1].modelSystemGO;
            EncontrarPadreTotal(moduloDerecho);
            nombrePadreTotalViejo = padreTotal.name;
            tipoPadreTotalViejo = padreTotal.tag;
            if (tipoPadreTotalViejo == "22")
            {
                GameObject moduloDerechoDerecho = modelusList.modulesGroup[(posicion - 1) + 2].modelSystemGO;
                EncontrarPadreTotal(moduloDerechoDerecho);
                DestruirObjetosRecursivos(moduloDerechoDerecho);
                string nombrePadreTotalDerechoDerecho = moduloDerechoDerecho.name;
                newModule = CrearNuevoModuloTipoDado("ModuloVacio");
                AsignacionRecursivaLogicaComponentes(newModule);
                SustituirNuevoModuloListaModulos(newModule, nombrePadreTotalDerechoDerecho);
            }
            EncontrarPadreTotal(moduloDerecho);
            DestruirObjetosRecursivos(padreTotal);
            newModule = CrearNuevoModuloTipoDado("23");
            AsignacionRecursivaLogicaComponentes(newModule);
            SustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
        }
        else if (tipoNuevoModulo == "23")
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            Debug.Log("posicion: " + posicion);
            GameObject moduloIzquierdo = modelusList.modulesGroup[(posicion - 1) - 1].modelSystemGO;
            EncontrarPadreTotal(moduloIzquierdo);
            nombrePadreTotalViejo = padreTotal.name;
            tipoPadreTotalViejo = padreTotal.tag;
            if (tipoPadreTotalViejo == "23")
            {
                GameObject moduloIzquierdoIzquierdo = modelusList.modulesGroup[(posicion - 1) - 2].modelSystemGO;
                EncontrarPadreTotal(moduloIzquierdoIzquierdo);
                DestruirObjetosRecursivos(moduloIzquierdoIzquierdo);
                string nombrePadreTotalIzquierdoIzquierdo = moduloIzquierdoIzquierdo.name;
                newModule = CrearNuevoModuloTipoDado("ModuloVacio");
                AsignacionRecursivaLogicaComponentes(newModule);
                SustituirNuevoModuloListaModulos(newModule, nombrePadreTotalIzquierdoIzquierdo);
            }
            EncontrarPadreTotal(moduloIzquierdo);
            DestruirObjetosRecursivos(padreTotal);
            newModule = CrearNuevoModuloTipoDado("22");
            AsignacionRecursivaLogicaComponentes(newModule);
            SustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
        }
        panel.transform.position = new Vector3(0, -5, 0);
        //panel.SetActive(false);
    }

    /*En este método se encarga de realizar la creación de los nuevos modulos y establecer sus carácteristicas,
      de acuerdo a un tipo de múdulo determinado.*/
    private GameObject CrearNuevoModuloTipoDado(string tipo)
    {
        string tipoModulo = tipo;
        string ruta = "Assets/Prefabs/" + tipoModulo + ".blend";
        GameObject newModule = (GameObject)AssetDatabase.LoadAssetAtPath(ruta, typeof(GameObject));
        newModule = Instantiate(newModule, padreTotal.transform.position, padreTotal.transform.rotation);
        string terminacion = DeterminarTerminacionNuevoModulo(tipoModulo);
        newModule.name = tipoModulo + terminacion;
        //newModule = AsignarLogicaModulo(newModule, tipoModulo);
        newModule = AuxiliarModulos.AsignarLogicaModulo(newModule, tipoModulo);
        newModule.tag = tipoModulo;
        newModule.layer = moduleLayer;
        if (debug)
        {
            Debug.Log("Ruta: " + ruta);
        }
        return newModule;
    }

    /*En este método se encarga de realizar la creación de los nuevos modulos y establecer sus carácteristicas,
      de acuerdo a lo determinado por medio del dropDownMenu de selección.*/
    private GameObject CrearNuevoModuloDropDown()
    {
        //Keep the current index of the Dropdown in a variable
        int m_DropdownValue = dropdown.value;
        //Change the message to say the name of the current Dropdown selection using the value
        string tipoModulos = dropdown.options[m_DropdownValue].text;
        string ruta = "Assets/Prefabs/" + tipoModulos + ".blend";
        GameObject newModule = (GameObject)AssetDatabase.LoadAssetAtPath(ruta, typeof(GameObject));
        newModule = Instantiate(newModule, padreTotal.transform.position, padreTotal.transform.rotation);
        string terminacion = DeterminarTerminacionNuevoModulo(tipoModulos);
        newModule.name = tipoModulos + terminacion;
        //newModule = AsignarLogicaModulo(newModule, tipoModulos);
        newModule = AuxiliarModulos.AsignarLogicaModulo(newModule, tipoModulos);
        newModule.tag = tipoModulos;
        newModule.layer = moduleLayer;
        if (debug)
        {
            Debug.Log("Ruta: " + ruta);
        }
        return newModule;
    }

    /*En este método se determina cuantos otros modulos del mismo tipo existen el el simulador.
      Por lo tanto determina la terminación de los módulos.*/
    private string DeterminarTerminacionNuevoModulo(string tipoModulo)
    {
        GameObject[] sameTypeObjects;
        string terminacion = "_0";
        sameTypeObjects = GameObject.FindGameObjectsWithTag(tipoModulo);
        List<int> listaTerminacion = new List<int>();
        if (sameTypeObjects.Length != 0)
        {
            if (debug)
            {
                Debug.Log("DeterminarTerminaciónNuevoModulo - ////////////Hay objetos con la misma tag");
            }
            foreach (GameObject sameTypeObject in sameTypeObjects)
            {
                Debug.Log(sameTypeObject.name);
                int position = sameTypeObject.name.IndexOf("_");
                string numeroElementoString = sameTypeObject.name.Substring(position + 1);
                int numeroElemento = int.Parse(numeroElementoString);
                listaTerminacion.Add(numeroElemento);
            }
            listaTerminacion.Sort();
            int valorMasGrande = listaTerminacion[listaTerminacion.Count - 1];
            int valor = -1;
            for (int i = 0; i <= valorMasGrande; i++)
            {
                if (listaTerminacion[i] != i)
                {
                    valor = i;
                    break;
                }
            }
            if (valor == -1)
            {
                valor = valorMasGrande + 1;
            }
            terminacion = "_" + valor.ToString();
        }
        else
        {
            Debug.Log("No hay más objetos con ma misma tag");
        }
        return terminacion;
    }

    //Ya no se deberia usar
    /*En este método se asigna la lógica de funcionamiento a un determinado módulo, de acuerdo a su tipo.*/
    /*private GameObject AsignarLogicaModulo(GameObject modulo, string tipoModulo)
    {
        if (modulo != null)
        {
            if (tipoModulo == "1")
            {
                modulo.AddComponent<Modulo1>();
            }
            else
            if (tipoModulo == "2")
            {
                modulo.AddComponent<Modulo2>();
            }
            else
            if (tipoModulo == "3")
            {
                modulo.AddComponent<Modulo3>();
            }
            else
            if (tipoModulo == "4")
            {
                modulo.AddComponent<Modulo4>();
            }
            else
            if (tipoModulo == "5")
            {
                modulo.AddComponent<Modulo5>();
            }
            else
            if (tipoModulo == "6")
            {
                modulo.AddComponent<Modulo6>();
            }
            else
            if (tipoModulo == "7")
            {
                modulo.AddComponent<Modulo7>();
            }
            else
            if (tipoModulo == "8, 11")
            {
                modulo.AddComponent<Modulo8_11>();
            }
            else
            if (tipoModulo == "9")
            {
                modulo.AddComponent<Modulo9>();
            }
            else
            if (tipoModulo == "10, 17, 18, 19")
            {
                modulo.AddComponent<Modulo10_17_18_19>();
            }
            else
            if (tipoModulo == "13")
            {
                modulo.AddComponent<Modulo13>();
            }
            else
            if (tipoModulo == "14, 16")
            {
                modulo.AddComponent<Modulo14_16>();
            }
            else
            if (tipoModulo == "15")
            {
                modulo.AddComponent<Modulo15>();
            }
            else
            if (tipoModulo == "20")
            {
                modulo.AddComponent<Modulo20>();
            }
            else
            if (tipoModulo == "21")
            {
                modulo.AddComponent<Modulo21>();
            }
            else
            if (tipoModulo == "22, 23")
            {
                modulo.AddComponent<Modulo22_23>();
            }
            else
            if (tipoModulo == "22")
            {
                modulo.AddComponent<Modulo22_23>();
            }
            else
            if (tipoModulo == "23")
            {
                modulo.AddComponent<Modulo22_23>();
            }
            else
            if (tipoModulo == "Potenciometro")
            {
                modulo.AddComponent<Potenciometro>();
            }
            else
            if (tipoModulo == "Multiconector")
            {
                modulo.AddComponent<Potenciometro>();
            }
            else
            if (tipoModulo == "MotorElectricoAC")
            {
                modulo.AddComponent<MotorElectricoAC>();
            }
            else
            {
                Debug.LogError("GameObject AsignarLogicaModulo(GameObject module, string tipo) - Tipo de modulo desconocido - " + tipoModulo + ".");
            }
        }
        else
        {
            Debug.LogError("GameObject AsignarLogicaModulo(GameObject module, string tipo) - Modulo no asignado, es decir, null.");
        }

        return modulo;
    }*/

    /*En este método se asigna la lógica de funcionamiento a los componentes de un determinado módulo´,
     * de acuerdo a su tipo.*/
    private void AsignacionRecursivaLogicaComponentes(GameObject nodo)
    {
        /*Asignación de capa adecuada a los componentes del modulo,
          para hacer los compointes interactuables y distinguirlos de los no
          interactuables.*/
        nodo.layer = moduleLayer;
        //Asignación de la lógica a los componentes
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

    /*Este método es el encargado de actualizar la lista de módulos actuales del simulador.*/
    private void SustituirNuevoModuloListaModulos(GameObject nuevoModulo, string nombrePadreTotalViejo)
    {
        if (modulesList != null)
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            for (int i = 0; i < totalModulosSimulador; i++)
            {
                if (modelusList.modulesGroup[i].modelSystemGO.name == nombrePadreTotalViejo)
                {
                    modelusList.modulesGroup[i].modelSystemGO = nuevoModulo;
                    modelusList.modulesGroup[i].modelPosition = nuevoModulo.transform.position;
                    modelusList.modulesGroup[i].modelRotation = nuevoModulo.transform.rotation.eulerAngles;
                    modelusList.modulesGroup[i].nameModule = nuevoModulo.name;
                    modelusList.modulesGroup[i].title = nuevoModulo.name;
                    break;
                }
            }
        }
    }

    /*Este método es el encargado de optener la lista de un módulo dentro de la lista de módulos del simulador.*/
    private int SaberPosicionModuloListaModulos(GameObject moduloActual)
    {
        int indiceEncontrado = -1;
        if (modulesList != null)
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            for (int i = 0; i < totalModulosSimulador; i++)
            {
                if (modelusList.modulesGroup[i].modelSystemGO.name == moduloActual.name)
                {
                    indiceEncontrado = i + 1;
                    break;
                }
            }
        }
        return indiceEncontrado;
    }
    #endregion
}
