using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

public class ChangeModule : MonoBehaviour
{

    // Use this for initialization

    private GameObject padreTotal;
    public Dropdown dropdown;
    public Text myText;
    public GameObject panel;
    public GameObject player;
    public int moduleLayer = 11; //La capa 11, equivale a la capa "Modulo"
    public GameObject selectedGameObjectRightClick;
    public Text textInfoChangeModule;
    public Button buttonChangeModule;
    public GameObject modulesList;

    void Start()
    {
        player = GameObject.Find("FirstPersonCharacter");
        dropdown = GameObject.Find("DropdownChangeModule").GetComponent<Dropdown>();
        myText = GameObject.Find("TextChangeModule").GetComponent<Text>();
        textInfoChangeModule = GameObject.Find("TextInfoChangeModule").GetComponent<Text>();
        myText.color = Color.clear;
        panel = GameObject.Find("PanelChangeModule");
        buttonChangeModule = GameObject.Find("ButtonChangeModule").GetComponent<Button>();
        modulesList = GameObject.Find("ModulesGroup");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ComprobarRestricciones()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = clickDetector.lastClickedGmObj;

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
            if (padreTotal!= null && padreTotal.tag == nombreModuloNuevo)
            {
                string mensajePreMismoModulo = "";
                mensajePreMismoModulo = "Precacición. Tiene seleccionado el mismo tipo de modulo que el modulo actual, por lo tanto no se puede realizar el cambio de modulo.";
                Debug.LogError(mensajePreMismoModulo);
                textInfoChangeModule.text = "Información: " + mensajePreMismoModulo;
                buttonChangeModule.enabled = false;
            }else if(nombreModuloNuevo == "22")
            {
                if ((saberPosicionModuloDoble(padreTotal)%4)==0)
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
                int posicion = saberPosicionModuloDoble(padreTotal);
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
            Debug.LogError("Entra al if, nombreModuloNuevo: " + nombreModuloNuevo + ", padreTotal.tag: " + padreTotal.tag);
        }
        else
        {
            string mensajeErrorModRestringidos = "";
            mensajeErrorModRestringidos = "Error. No se puede crear otro modulo " + nombreModuloNuevo + ", solo puede existir a la vez un modulo de ese tipo en el simulador. Intente crear un modulo de otro tipo.";
            Debug.LogError(mensajeErrorModRestringidos);
            textInfoChangeModule.text = "Información: " + mensajeErrorModRestringidos;
            buttonChangeModule.enabled = false;
            Debug.LogError("Entra al else");
        }
        Debug.LogError("Finaliza comrpobar estado");
    }

    public void ReplaceModule()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = clickDetector.lastClickedGmObj;
        int m_DropdownValue = dropdown.value;
        string nombreModuloNuevo = dropdown.options[m_DropdownValue].text;
        GameObject newModule;
        //GameObject selectedGameObjectRightClick;
        //selectedGameObjectRightClick = GameObject.Find("SelectedGameObjectRightClick");
        module = selectedGameObjectRightClick;
        Debug.Log("***********Modulo seleccionado: " + module);
        //module.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        EncontrarPadreTotal(module);
        int posicion = saberPosicionModuloDoble(padreTotal);
        Debug.Log("***********Modulo padre: " + padreTotal.name);
        string nombrePadreTotalViejo = padreTotal.name;
        string tipoPadreTotalViejo = padreTotal.tag;

        if (tipoPadreTotalViejo == "22")//Destruir objeto de la derecha
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            Debug.Log("posicion: " + posicion);
            GameObject moduloDerecho = modelusList.modulesGroup[(posicion - 1) + 1].modelSystemGO;
            EncontrarPadreTotal(moduloDerecho);
            nombrePadreTotalViejo = padreTotal.name;
            DestruirObjetosRecursivos(padreTotal);
            newModule = CrearNuevoModuloTipoDado("ModuloVacio");
            AsignacionRecursiva(newModule);
            sustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
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
            AsignacionRecursiva(newModule);
            sustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
            Debug.LogError("if (tipoPadreTotalViejo == 23)");
        }
        EncontrarPadreTotal(module);
        nombrePadreTotalViejo = padreTotal.name;
        DestruirObjetosRecursivos(padreTotal);
        newModule = CrearNuevoModulo();
        AsignacionRecursiva(newModule);
        sustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
        if(nombreModuloNuevo == "22")
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            Debug.Log("posicion: " + posicion);
            GameObject moduloDerecho = modelusList.modulesGroup[(posicion - 1) + 1].modelSystemGO;
            EncontrarPadreTotal(moduloDerecho);
            nombrePadreTotalViejo = padreTotal.name;
            tipoPadreTotalViejo = padreTotal.tag;
            if(tipoPadreTotalViejo == "22")
            {
                GameObject moduloDerechoDerecho = modelusList.modulesGroup[(posicion - 1) + 2].modelSystemGO;
                EncontrarPadreTotal(moduloDerechoDerecho);
                DestruirObjetosRecursivos(moduloDerechoDerecho);
                string nombrePadreTotalDerechoDerecho = moduloDerechoDerecho.name;
                newModule = CrearNuevoModuloTipoDado("ModuloVacio");
                AsignacionRecursiva(newModule);
                sustituirNuevoModuloListaModulos(newModule, nombrePadreTotalDerechoDerecho);
            }
            EncontrarPadreTotal(moduloDerecho);
            DestruirObjetosRecursivos(padreTotal);
            newModule = CrearNuevoModuloTipoDado("23");
            AsignacionRecursiva(newModule);
            sustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
        }
        else if(nombreModuloNuevo == "23")
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
                AsignacionRecursiva(newModule);
                sustituirNuevoModuloListaModulos(newModule, nombrePadreTotalIzquierdoIzquierdo);
            }
            EncontrarPadreTotal(moduloIzquierdo);
            DestruirObjetosRecursivos(padreTotal);
            newModule = CrearNuevoModuloTipoDado("22");
            AsignacionRecursiva(newModule);
            sustituirNuevoModuloListaModulos(newModule, nombrePadreTotalViejo);
        }
        panel.transform.position = new Vector3(0, -5, 0);
        //panel.SetActive(false);
        OpenCloseMenuChangeModule(clickDetector.lastClickedGmObj);
    }

    private void sustituirNuevoModuloListaModulos(GameObject nuevoModulo, string nombrePadreTotalViejo)
    {
        if(modulesList != null)
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            for (int i=0; i<28; i++)
            {
                if(modelusList.modulesGroup[i].modelSystemGO.name == nombrePadreTotalViejo)
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

    private int saberPosicionModuloDoble(GameObject moduloActual)
    {
        int indiceEncontrado = -1;
        if (modulesList != null)
        {
            ModulesList modelusList = modulesList.GetComponent<ModulesList>();
            for (int i = 0; i < 28; i++)
            {
                if (modelusList.modulesGroup[i].modelSystemGO.name == moduloActual.name)
                {
                    indiceEncontrado = i+1;
                    break;
                }
            }
        }
        return indiceEncontrado;
    }

    private GameObject CrearNuevoModuloTipoDado(string tipo)
    {
        GameObject[] sameTypeObjects;
        string m_Message = tipo;
        string ruta = "Assets/Prefabs/" + m_Message + ".blend";
        Debug.Log("Ruta: " + ruta);
        GameObject newModule = (GameObject)AssetDatabase.LoadAssetAtPath(ruta, typeof(GameObject));
        newModule = Instantiate(newModule, padreTotal.transform.position, padreTotal.transform.rotation);
        string terminacion = "_0";
        sameTypeObjects = GameObject.FindGameObjectsWithTag(m_Message);

        List<int> listaTerminacion = new List<int>();
        if (sameTypeObjects.Length != 0)
        {
            Debug.Log("////////////Hay objetos con la misma tag");
            foreach (GameObject sameTypeObject in sameTypeObjects)
            {
                Debug.Log(sameTypeObject.name);
                int position = sameTypeObject.name.IndexOf("_");
                string numeroElementoString = sameTypeObject.name.Substring(position + 1);
                int numeroElemento = int.Parse(numeroElementoString);
                listaTerminacion.Add(numeroElemento);
            }
            Debug.Log("////////////Salio for Each");
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
        newModule.name = m_Message + terminacion;
        newModule = asignarLogicaModulo(newModule, m_Message);
        newModule.tag = m_Message;
        newModule.layer = moduleLayer;
        return newModule;
    }

    private GameObject CrearNuevoModulo()
    {
        GameObject[] sameTypeObjects;

        //Keep the current index of the Dropdown in a variable
        int m_DropdownValue = dropdown.value;
        //Change the message to say the name of the current Dropdown selection using the value
        string m_Message = dropdown.options[m_DropdownValue].text;
        string ruta = "Assets/Prefabs/" + m_Message + ".blend";
        Debug.Log("Ruta: " + ruta);
        GameObject newModule = (GameObject)AssetDatabase.LoadAssetAtPath(ruta, typeof(GameObject));
        newModule = Instantiate(newModule, padreTotal.transform.position, padreTotal.transform.rotation);
        string terminacion = "_0";
        sameTypeObjects = GameObject.FindGameObjectsWithTag(m_Message);

        List<int> listaTerminacion = new List<int>();
        if (sameTypeObjects.Length != 0)
        {
            Debug.Log("////////////Hay objetos con la misma tag");
            foreach (GameObject sameTypeObject in sameTypeObjects)
            {
                Debug.Log(sameTypeObject.name);
                int position = sameTypeObject.name.IndexOf("_");
                string numeroElementoString = sameTypeObject.name.Substring(position + 1);
                int numeroElemento = int.Parse(numeroElementoString);
                listaTerminacion.Add(numeroElemento);
            }
            Debug.Log("////////////Salio for Each");
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
        newModule.name = m_Message + terminacion;
        newModule = asignarLogicaModulo(newModule, m_Message);
        newModule.tag = m_Message;
        newModule.layer = moduleLayer;
        return newModule;
    }

    private GameObject asignarLogicaModulo(GameObject module, string tipo)
    {
        if (tipo == "1")
        {
            module.AddComponent<Modulo1>();
        }
        else
        if (tipo == "2")
        {
            module.AddComponent<Modulo2>();
        }
        else
        if (tipo == "3")
        {
            module.AddComponent<Modulo3>();
        }
        else
        if (tipo == "4")
        {
            module.AddComponent<Modulo4>();
        }
        else
        if (tipo == "5")
        {
            module.AddComponent<Modulo5>();
        }
        else
        if (tipo == "6")
        {
            module.AddComponent<Modulo6>();
        }
        else
        if (tipo == "7")
        {
            module.AddComponent<Modulo7>();
        }
        else
        if (tipo == "8, 11")
        {
            module.AddComponent<Modulo8_11>();
        }
        else
        if (tipo == "9")
        {
            module.AddComponent<Modulo9>();
        }
        else
        if (tipo == "10, 17, 18, 19")
        {
            module.AddComponent<Modulo10_17_18_19>();
        }
        else
        if (tipo == "13")
        {
            module.AddComponent<Modulo13>();
        }
        else
        if (tipo == "14, 16")
        {
            module.AddComponent<Modulo14_16>();
        }
        else
        if (tipo == "15")
        {
            module.AddComponent<Modulo15>();
        }
        else
        if (tipo == "20")
        {
            module.AddComponent<Modulo20>();
        }
        else
        if (tipo == "21")
        {
            module.AddComponent<Modulo21>();
        }
        else
        if (tipo == "22, 23")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (tipo == "22")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (tipo == "23")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (tipo == "Potenciometro")
        {
            module.AddComponent<Potenciometro>();
        }
        return module;
    }

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

    private void AsignacionRecursiva(GameObject nodo)
    {
        nodo.layer = moduleLayer;
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

        /*GameObject myObj = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/3.blend", typeof(GameObject));
        MouseClick mouseScript = nodo.GetComponent<MouseClick>();
        mouseScript.prefab = (GameObject)myObj;*/
        int numeroDeHijoss = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijoss; i++)
        {
            AsignacionRecursiva(nodo.transform.GetChild(i).gameObject);
        }
    }

    private void DestruirObjetosRecursivos(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            DestruirObjetosRecursivos(nodo.transform.GetChild(i).gameObject);
        }
        Destroy(nodo);
    }

    private void OpenCloseMenuChangeModule(GameObject module)
    {
        //float step = 1 * Time.deltaTime;
        //panel.transform.position = Vector3.MoveTowards(panel.transform.position, module.transform.position, step);
        /*Debug.Log("Entra al LookAt");
        Debug.Log("transform.position.x: " + transform.position.x +
            ", transform.position.y: " + transform.position.y +
            ", transform.position.z: " + transform.position.z);
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //panel.transform.LookAt(targetPosition);
        //panel.transform.rotation = player.transform.rotation;
        Vector3 menuPosition = new Vector3(module.transform.position.x, module.transform.position.y+4, module.transform.position.z-3);
        panel.transform.position = menuPosition;*/
        /*if (!panel.activeSelf)
        {
            Debug.Log("Activa el panel");
            panel.SetActive(true);
            myText.text = myString;
            myText.color = Color.Lerp(myText.color, Color.red, fateTime * Time.deltaTime);
            //Renderer rend;
            //rend = GetComponent<Renderer>();
            //rend.material.shader = Shader.Find("Toon/Basic Outline");
            //rend.material.SetColor("_OutlineColor", Color.red);

            //dropdown.Show();
            //myText.transform.LookAt(player.transform);
            //myText.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, -player.transform.position.z));
        }
        else
        {
            Debug.Log("Desactiva el panel");
            panel.SetActive(false);
            myText.color = Color.Lerp(myText.color, Color.clear, fateTime * Time.deltaTime);
            //Renderer rend;
            //rend = GetComponent<Renderer>();
            //rend.material.shader = Shader.Find("Standard");
            //dropdown.Hide();
        }*/
    }

}
