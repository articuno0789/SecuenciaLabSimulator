using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    string dataPath;
    GameObject[] saveModules;
    public string nombreArchivoGuardado = "";
    private GameObject padreTotal;
    public Dropdown dropdown;
    public GameObject panel;
    GameObject modulesList;
    int moduleLayer = 11; //La capa 11, equivale a la capa "Modulo"
    bool debug = false;
    bool cifrar = false;
    string palabraClave = "ProyectoModular";

    // Start is called before the first frame update
    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "SimulatorDataTest.txt");
        dropdown = GameObject.Find("DropdownChangeModule").GetComponent<Dropdown>();
        panel = GameObject.Find("PanelChangeModule");
        modulesList = GameObject.Find("ModulesGroup");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveSimulator(dataPath, "SimulatorDataTest.txt");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadSimulator(dataPath, "SimulatorDataTest.txt");
            //Debug.LogError("TODAVIA NO ESTA IMPLEMETANDO CARGAR SIMULADOR");
            //LoadSimulator(dataPath);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            DestruirModulos();
        }
    }

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
        Debug.Log("Modulos destruidos");
    }

    bool DestruirObjetos(string tagModulos)
    {
        GameObject[] modulosDestruir = GameObject.FindGameObjectsWithTag(tagModulos);
        int numeroMaximoModulos = modulosDestruir.Length;
        foreach (GameObject modulo in modulosDestruir)
        {
            Destroy(modulo);
        }
        if(numeroMaximoModulos != 0)
        {
            return true;
        }
        return false;
    }

    public void LoadSimulator(string path, string nameFile)
    {
        string jsonString = ObtenerCadenaJson(path);
        //DestruirModulos();
        JSONObject j = new JSONObject(jsonString);
        nombreArchivoGuardado = nameFile;
        //Debug.Log(j.keys[0]);
        Debug.Log("Empieza lectura json*--------------------------------------------------------------------------------------------");
        //accessData(j);
        if (IsValidJson(jsonString))
        {
            Debug.Log("************JSON Valido");
            DestruirModulos();
            CrearModulosDesdeJson(jsonString);
        }
        else
        {
            Debug.Log("//////////////JSON Invalido");
        }
    }

    void CrearModulosDesdeJson(string jsonString)
    {
        var jo = JObject.Parse(jsonString);
        int posicionModulesList = 0;
        ModulesList modList = modulesList.GetComponent<ModulesList>();
        foreach (var x in jo)
        {
            string nombreModulo = x.Key;
            JToken contenido = x.Value;
            Debug.Log("name: " + nombreModulo);
            Debug.Log("value: " + contenido);
            Debug.Log("Tipo: " + contenido["Tipo"]);
            string tipoModulo = contenido["Tipo"].ToString();
            JToken posicion = contenido["Posicion"];
            Vector3 vectorPosicion = new Vector3(float.Parse(posicion["X"].ToString()), float.Parse(posicion["Y"].ToString()), float.Parse(posicion["Z"].ToString()));
            Vector3 vectorRotacion = new Vector3(0, 180, 0);
            GameObject newModule = CrearNuevoModulo(tipoModulo, vectorPosicion, vectorRotacion, nombreModulo);
            AsignacionRecursiva(newModule);
            modList.modulesGroup[posicionModulesList].modelSystemGO = newModule;
            modList.modulesGroup[posicionModulesList].modelPosition = newModule.transform.position;
            modList.modulesGroup[posicionModulesList].modelRotation = newModule.transform.rotation.eulerAngles;
            modList.modulesGroup[posicionModulesList].nameModule = newModule.name;
            modList.modulesGroup[posicionModulesList].title = newModule.name;
            if (tipoModulo == "1")
            {
                float voltaje = float.Parse(contenido["Voltaje"].ToString());
                Modulo1 mod1 = newModule.GetComponent<Modulo1>();
                mod1.voltajeModulo = voltaje;
            }else if (tipoModulo == "6")
            {
                float valorPerilla = float.Parse(contenido["ValorPerilla"].ToString());
                Modulo6 mod6 = newModule.GetComponent<Modulo6>();
                mod6.valorActualPerilla = valorPerilla;
                mod6.RotarPerilla();
            }
            else if (tipoModulo == "7")
            {
                float valorPerilla = float.Parse(contenido["ValorPerilla"].ToString());
                Modulo7 mod7 = newModule.GetComponent<Modulo7>();
                mod7.valorActualPerilla = valorPerilla;
                mod7.RotarPerilla();
            }
            else if (tipoModulo == "Potenciometro")
            {
                float valorPerilla = float.Parse(contenido["ValorPerilla"].ToString());
                Potenciometro modPoten = newModule.GetComponent<Potenciometro>();
                modPoten.valorActualPerilla = valorPerilla;
                modPoten.RotarPerilla();
            }
            posicionModulesList++;
        }
        Debug.Log("Termina creación modulos");
    }

    private GameObject CrearNuevoModulo(string tipo, Vector3 vectorPosicion, Vector3 vectorRotacion, string nombreModulo)
    {
        string ruta = "Assets/Prefabs/" + tipo + ".blend";
        GameObject newModule = (GameObject)AssetDatabase.LoadAssetAtPath(ruta, typeof(GameObject));
        newModule = Instantiate(newModule, vectorPosicion, Quaternion.Euler(vectorRotacion)) as GameObject;
        newModule.tag = tipo;
        newModule.name = nombreModulo;
        newModule.layer = moduleLayer;
        newModule = AsignarLogicaModulo(newModule, tipo);
        return newModule;
    }

    private GameObject AsignarLogicaModulo(GameObject module, string nameModule)
    {
        if (nameModule == "1")
        {
            module.AddComponent<Modulo1>();
        }
        else
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

    public bool ComprobarExistenciaArchivo(string path)
    {
        bool existeArchivo = false;
        if (File.Exists(path))
        {
            existeArchivo = true;
        }
        return existeArchivo;
    }

    public void SaveSimulator(string path, string nameFile)
    {
        saveModules = GameObject.FindGameObjectsWithTag("1");
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
        string jsonModVacio = GenerarJsonModuloVacio();
        string json = "{";
        if(jsonMod1.Length != 0)
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
        if (jsonModVacio.Length != 0)
        {
            json += jsonModVacio;
        }
        if(json[json.Length-1] == ',')
        {
            json.Remove(json.Length-1);
        }
        json += "}";
        Debug.LogError(json);
        if (cifrar)
        {
             json = EncryptStringSample.StringCipher.Encrypt(json, palabraClave);
        }
        if (File.Exists(path))
        {
            Debug.LogError("El archivo ya existe y si conteninua el contenido anterior se perderá.");
            bool continuarGuardado = true;
            if (continuarGuardado)
            {
                using (StreamWriter streamWriter = File.CreateText(path))
                {
                    streamWriter.Write(json);
                }
                Debug.Log("Archivo guardado correctamente." + dataPath);
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
            Debug.Log("Archivo guardado correctamente." + dataPath);
            nombreArchivoGuardado = nameFile;
        }
    }

    string GenerarJsonModulo1()
    {
        GameObject[] modulos1 = GameObject.FindGameObjectsWithTag("1");
        string jsonModulo1 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos1.Length;
        foreach (GameObject modulo in modulos1)
        {
            Modulo1 mod1 = modulo.GetComponent<Modulo1>();
            jsonModulo1 += "\"" + modulo.name + "\":{\n";
            jsonModulo1 += "\"Tipo\":\"1\",\n";
            jsonModulo1 += "\"Voltaje\":" + mod1.voltajeModulo + ",\n";
            jsonModulo1 += PosicionModulo(modulo) + ",\n";
            jsonModulo1 += "\"Conexiones\":{" + CrearConexionesModulo(mod1.plugsConnections) + "}";
            jsonModulo1 += "}";
            if (numeroDeModulos != numeroMaximoModulos) {
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

    string GenerarJsonModulo2()
    {
        GameObject[] modulos2 = GameObject.FindGameObjectsWithTag("2");
        string jsonModulo2 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos2.Length;
        foreach (GameObject modulo in modulos2)
        {
            Modulo2 mod2 = modulo.GetComponent<Modulo2>();
            jsonModulo2 += "\"" + modulo.name + "\":{\n";
            jsonModulo2 += "\"Tipo\":\"2\",\n";
            jsonModulo2 += PosicionModulo(modulo) + ",\n";
            jsonModulo2 += "\"Conexiones\":{" + CrearConexionesModulo(mod2.plugsConnections) + "}";
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

    string GenerarJsonModulo3()
    {
        GameObject[] modulos3 = GameObject.FindGameObjectsWithTag("3");
        string jsonModulo3 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos3.Length;
        foreach (GameObject modulo in modulos3)
        {
            Modulo3 mod3 = modulo.GetComponent<Modulo3>();
            jsonModulo3 += "\"" + modulo.name + "\":{\n";
            jsonModulo3 += "\"Tipo\":\"3\",\n";
            jsonModulo3 += PosicionModulo(modulo) + ",\n";
            jsonModulo3 += "\"Conexiones\":{" + CrearConexionesModulo(mod3.plugsConnections) + "}";
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

    string GenerarJsonModulo4()
    {
        GameObject[] modulos4 = GameObject.FindGameObjectsWithTag("4");
        string jsonModulo4 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos4.Length;
        foreach (GameObject modulo in modulos4)
        {
            Modulo4 mod4 = modulo.GetComponent<Modulo4>();
            jsonModulo4 += "\"" + modulo.name + "\":{\n";
            jsonModulo4 += "\"Tipo\":\"4\",\n";
            jsonModulo4 += PosicionModulo(modulo) + ",\n";
            jsonModulo4 += "\"Conexiones\":{" + CrearConexionesModulo(mod4.plugsConnections) + "}";
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

    string GenerarJsonModulo5()
    {
        GameObject[] modulos5 = GameObject.FindGameObjectsWithTag("5");
        string jsonModulo5 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos5.Length;
        foreach (GameObject modulo in modulos5)
        {
            Modulo5 mod5 = modulo.GetComponent<Modulo5>();
            jsonModulo5 += "\"" + modulo.name + "\":{\n";
            jsonModulo5 += "\"Tipo\":\"5\",\n";
            jsonModulo5 += PosicionModulo(modulo) + ",\n";
            jsonModulo5 += "\"Conexiones\":{" + CrearConexionesModulo(mod5.plugsConnections) + "}";
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

    string GenerarJsonModulo6()
    {
        GameObject[] modulos6 = GameObject.FindGameObjectsWithTag("6");
        string jsonModulo6 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos6.Length;
        foreach (GameObject modulo in modulos6)
        {
            Modulo6 mod6 = modulo.GetComponent<Modulo6>();
            jsonModulo6 += "\"" + modulo.name + "\":{\n";
            jsonModulo6 += "\"Tipo\":\"6\",\n";
            jsonModulo6 += "\"ValorPerilla\":" + mod6.valorActualPerilla + ",\n";
            jsonModulo6 += PosicionModulo(modulo) + ",\n";
            jsonModulo6 += "\"Conexiones\":{" + CrearConexionesModulo(mod6.plugsConnections) + "}";
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

    string GenerarJsonModulo7()
    {
        GameObject[] modulos7 = GameObject.FindGameObjectsWithTag("7");
        string jsonModulo7 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos7.Length;
        foreach (GameObject modulo in modulos7)
        {
            Modulo7 mod7 = modulo.GetComponent<Modulo7>();
            jsonModulo7 += "\"" + modulo.name + "\":{\n";
            jsonModulo7 += "\"Tipo\":\"7\",\n";
            jsonModulo7 += "\"ValorPerilla\":" + mod7.valorActualPerilla + ",\n";
            jsonModulo7 += PosicionModulo(modulo) + ",\n";
            jsonModulo7 += "\"Conexiones\":{" + CrearConexionesModulo(mod7.plugsConnections) + "}";
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

    string GenerarJsonModulo8_11()
    {
        GameObject[] modulos8_11 = GameObject.FindGameObjectsWithTag("8, 11");
        string jsonModulo8_11 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos8_11.Length;
        foreach (GameObject modulo in modulos8_11)
        {
            Modulo8_11 mod8_11 = modulo.GetComponent<Modulo8_11>();
            jsonModulo8_11 += "\"" + modulo.name + "\":{\n";
            jsonModulo8_11 += "\"Tipo\":\"8, 11\",\n";
            jsonModulo8_11 += PosicionModulo(modulo) + ",\n";
            jsonModulo8_11 += "\"Conexiones\":{" + CrearConexionesModulo(mod8_11.plugsConnections) + "}";
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

    string GenerarJsonModulo9()
    {
        GameObject[] modulos9 = GameObject.FindGameObjectsWithTag("9");
        string jsonModulo9 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos9.Length;
        foreach (GameObject modulo in modulos9)
        {
            Modulo9 mod9 = modulo.GetComponent<Modulo9>();
            jsonModulo9 += "\"" + modulo.name + "\":{\n";
            jsonModulo9 += "\"Tipo\":\"9\",\n";
            jsonModulo9 += PosicionModulo(modulo) + ",\n";
            jsonModulo9 += "\"Conexiones\":{" + CrearConexionesModulo(mod9.plugsConnections) + "}";
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

    string GenerarJsonModulo10_17_18_19()
    {
        GameObject[] modulos10_17_18_19 = GameObject.FindGameObjectsWithTag("10, 17, 18, 19");
        string jsonModulo10_17_18_19 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos10_17_18_19.Length;
        foreach (GameObject modulo in modulos10_17_18_19)
        {
            Modulo10_17_18_19 mod10_17_18_19 = modulo.GetComponent<Modulo10_17_18_19>();
            jsonModulo10_17_18_19 += "\"" + modulo.name + "\":{\n";
            jsonModulo10_17_18_19 += "\"Tipo\":\"10, 17, 18, 19\",\n";
            jsonModulo10_17_18_19 += PosicionModulo(modulo) + ",\n";
            jsonModulo10_17_18_19 += "\"Conexiones\":{" + CrearConexionesModulo(mod10_17_18_19.plugsConnections) + "}";
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

    string GenerarJsonModulo13()
    {
        GameObject[] modulos13 = GameObject.FindGameObjectsWithTag("13");
        string jsonModulo13 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos13.Length;
        foreach (GameObject modulo in modulos13)
        {
            Modulo13 mod13 = modulo.GetComponent<Modulo13>();
            jsonModulo13 += "\"" + modulo.name + "\":{\n";
            jsonModulo13 += "\"Tipo\":\"13\",\n";
            jsonModulo13 += PosicionModulo(modulo) + ",\n";
            jsonModulo13 += "\"Conexiones\":{" + CrearConexionesModulo(mod13.plugsConnections) + "}";
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

    string GenerarJsonModulo14_16()
    {
        GameObject[] modulos14_16 = GameObject.FindGameObjectsWithTag("14, 16");
        string jsonModulo14_16 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos14_16.Length;
        foreach (GameObject modulo in modulos14_16)
        {
            Modulo14_16 mod14_16 = modulo.GetComponent<Modulo14_16>();
            jsonModulo14_16 += "\"" + modulo.name + "\":{\n";
            jsonModulo14_16 += "\"Tipo\":\"14, 16\",\n";
            jsonModulo14_16 += PosicionModulo(modulo) + ",\n";
            jsonModulo14_16 += "\"Conexiones\":{" + CrearConexionesModulo(mod14_16.plugsConnections) + "}";
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

    string GenerarJsonModulo15()
    {
        GameObject[] modulos15 = GameObject.FindGameObjectsWithTag("15");
        string jsonModulo15 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos15.Length;
        foreach (GameObject modulo in modulos15)
        {
            Modulo15 mod15 = modulo.GetComponent<Modulo15>();
            jsonModulo15 += "\"" + modulo.name + "\":{\n";
            jsonModulo15 += "\"Tipo\":\"15\",\n";
            jsonModulo15 += "\"Voltaje\":" + mod15.voltajeModulo + ",\n";
            jsonModulo15 += PosicionModulo(modulo) + ",\n";
            jsonModulo15 += "\"Conexiones\":{" + CrearConexionesModulo(mod15.plugsConnections) + "}";
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

    string GenerarJsonModulo20()
    {
        GameObject[] modulos20 = GameObject.FindGameObjectsWithTag("20");
        string jsonModulo20 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos20.Length;
        foreach (GameObject modulo in modulos20)
        {
            Modulo20 mod20 = modulo.GetComponent<Modulo20>();
            jsonModulo20 += "\"" + modulo.name + "\":{\n";
            jsonModulo20 += "\"Tipo\":\"20\",\n";
            jsonModulo20 += PosicionModulo(modulo) + ",\n";
            jsonModulo20 += "\"Conexiones\":{" + CrearConexionesModulo(mod20.plugsConnections) + "}";
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

    string GenerarJsonModulo21()
    {
        GameObject[] modulos21 = GameObject.FindGameObjectsWithTag("21");
        string jsonModulo21 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos21.Length;
        foreach (GameObject modulo in modulos21)
        {
            Modulo21 mod21 = modulo.GetComponent<Modulo21>();
            jsonModulo21 += "\"" + modulo.name + "\":{\n";
            jsonModulo21 += "\"Tipo\":\"21\",\n";
            jsonModulo21 += PosicionModulo(modulo) + ",\n";
            jsonModulo21 += "\"Conexiones\":{" + CrearConexionesModulo(mod21.plugsConnections) + "}";
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

    string GenerarJsonModulo22()
    {
        GameObject[] modulos22 = GameObject.FindGameObjectsWithTag("22");
        string jsonModulo22 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos22.Length;
        foreach (GameObject modulo in modulos22)
        {
            Modulo22_23 mod22 = modulo.GetComponent<Modulo22_23>();
            jsonModulo22 += "\"" + modulo.name + "\":{\n";
            jsonModulo22 += "\"Tipo\":\"22\",\n";
            jsonModulo22 += PosicionModulo(modulo) + ",\n";
            jsonModulo22 += "\"Conexiones\":{" + CrearConexionesModulo(mod22.plugsConnections) + "}";
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

    string GenerarJsonModulo23()
    {
        GameObject[] modulos23 = GameObject.FindGameObjectsWithTag("23");
        string jsonModulo23 = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulos23.Length;
        foreach (GameObject modulo in modulos23)
        {
            Modulo22_23 mod23 = modulo.GetComponent<Modulo22_23>();
            jsonModulo23 += "\"" + modulo.name + "\":{\n";
            jsonModulo23 += "\"Tipo\":\"23\",\n";
            jsonModulo23 += PosicionModulo(modulo) + ",\n";
            jsonModulo23 += "\"Conexiones\":{" + CrearConexionesModulo(mod23.plugsConnections) + "}";
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

    string GenerarJsonModuloPotenciometro()
    {
        GameObject[] modulosPotenciometro = GameObject.FindGameObjectsWithTag("Potenciometro");
        string jsonModuloPoten = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulosPotenciometro.Length;
        foreach (GameObject modulo in modulosPotenciometro)
        {
            Potenciometro modPoten = modulo.GetComponent<Potenciometro>();
            jsonModuloPoten += "\"" + modulo.name + "\":{\n";
            jsonModuloPoten += "\"Tipo\":\"Potenciometro\",\n";
            jsonModuloPoten += "\"ValorPerilla\":" + modPoten.valorActualPerilla + ",\n";
            jsonModuloPoten += PosicionModulo(modulo) + ",\n";
            jsonModuloPoten += "\"Conexiones\":{" + CrearConexionesModulo(modPoten.plugsConnections) + "}";
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

    string GenerarJsonModuloVacio()
    {
        GameObject[] modulosVacio = GameObject.FindGameObjectsWithTag("ModuloVacio");
        string jsonModuloPoten = "";
        int numeroDeModulos = 1;
        int numeroMaximoModulos = modulosVacio.Length;
        foreach (GameObject modulo in modulosVacio)
        {
            jsonModuloPoten += "\"" + modulo.name + "\":{\n";
            jsonModuloPoten += "\"Tipo\":\"ModuloVacio\",\n";
            jsonModuloPoten += PosicionModulo(modulo) + ",\n";
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

    string PosicionModulo(GameObject modulo)
    {
        string jsonPosicion = "";
        jsonPosicion += "\"Posicion\":{\n";
        jsonPosicion += "\"X\":" + modulo.transform.position.x + ",\n";
        jsonPosicion += "\"Y\":" + modulo.transform.position.y + ",\n";
        jsonPosicion += "\"Z\":" + modulo.transform.position.z + "\n}";
        return jsonPosicion;
    }

    string CrearConexionesModulo(Dictionary<string, string> conexionesDict)
    {
        string jsonConexiones = "";
        int numeroConexion = 1;
        int numeroMaximoConexiones = conexionesDict.Count;
        foreach (KeyValuePair<string, string> entry in conexionesDict)
        {
            jsonConexiones += "\"Conexion" + numeroConexion + "\":{\n";
            jsonConexiones += "\"Origen\": \"" + entry.Key + "\",\n";
            jsonConexiones += "\"Destino\": \"" + entry.Value + "\"\n}";
            if (numeroConexion != numeroMaximoConexiones)
            {
                jsonConexiones += ",\n";
            }
            numeroConexion++;
            // do something with entry.Value or entry.Key
        }
        return jsonConexiones;
    }
}
