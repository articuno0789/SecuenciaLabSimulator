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

    public string myString;
    public Text myText;
    public GameObject panel;
    public float fateTime;
    public bool displayInfo;
    public GameObject player;
    public GameObject player2;

    void Start()
    {
        player2 = GameObject.Find("Player");
        player = GameObject.Find("FirstPersonCharacter");
        dropdown = GameObject.Find("DropdownChangeModule").GetComponent<Dropdown>();
        myText = GameObject.Find("TextChangeModule").GetComponent<Text>();
        myText.color = Color.clear;
        panel = GameObject.Find("PanelChangeModule");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReplaceModule()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = clickDetector.lastClickedGmObj;
        Debug.Log("***********Modulo seleccionado: " + module);
        module.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        EncontrarPadreTotal(module);
        Debug.Log("***********Modulo padre: " + padreTotal.name);
        DestruirObjetosRecursivos(padreTotal);
        GameObject newModule = CrearNuevoModulo();
        AsignacionRecursiva(newModule);
        panel.transform.position = new Vector3(0, -5, 0);
        //panel.SetActive(false);
        OpenCloseMenuChangeModule(clickDetector.lastClickedGmObj);
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
        return newModule;
    }

    private GameObject asignarLogicaModulo(GameObject module, string nameModule)
    {
        if(nameModule == "1")
        {
            module.AddComponent<Modulo1>();
        }else
        if (nameModule == "2")
        {
            module.AddComponent<Modulo2>();
        }
        else
        if (nameModule == "3")
        {
            module.AddComponent<Modulo3>();
        }
        else
        if (nameModule == "4")
        {
            module.AddComponent<Modulo4>();
        }
        else
        if (nameModule == "5")
        {
            module.AddComponent<Modulo5>();
        }
        else
        if (nameModule == "6")
        {
            module.AddComponent<Modulo6>();
        }
        else
        if (nameModule == "7")
        {
            module.AddComponent<Modulo7>();
        }
        else
        if (nameModule == "8, 11")
        {
            module.AddComponent<Modulo8_11>();
        }
        else
        if (nameModule == "9")
        {
            module.AddComponent<Modulo9>();
        }
        else
        if (nameModule == "10, 17, 18, 19")
        {
            module.AddComponent<Modulo10_17_18_19>();
        }
        else
        if (nameModule == "13")
        {
            module.AddComponent<Modulo13>();
        }
        else
        if (nameModule == "14, 16")
        {
            module.AddComponent<Modulo14_16>();
        }
        else
        if (nameModule == "15")
        {
            module.AddComponent<Modulo15>();
        }
        else
        if (nameModule == "20")
        {
            module.AddComponent<Modulo20>();
        }
        else
        if (nameModule == "21")
        {
            module.AddComponent<Modulo21>();
        }
        else
        if (nameModule == "22, 23")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (nameModule == "22")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (nameModule == "23")
        {
            module.AddComponent<Modulo22_23>();
        }
        else
        if (nameModule == "Potenciometro")
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

    private void DestruirObjetosRecursivos(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            DestruirObjetosRecursivos(nodo.transform.GetChild(i).gameObject);
        }
        Destroy(nodo);
    }

    private void AsignacionRecursiva(GameObject nodo)
    {
        nodo.AddComponent<OpenCloseChangeModule>();
        OpenCloseChangeModule OCChangeModule = nodo.GetComponent<OpenCloseChangeModule>();
        OCChangeModule.panel = panel;
        OCChangeModule.dropdown = dropdown;
        /*GameObject myObj = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/3.blend", typeof(GameObject));
        MouseClick mouseScript = nodo.GetComponent<MouseClick>();
        mouseScript.prefab = (GameObject)myObj;*/
        int numeroDeHijoss = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijoss; i++)
        {
            AsignacionRecursiva(nodo.transform.GetChild(i).gameObject);
        }
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
