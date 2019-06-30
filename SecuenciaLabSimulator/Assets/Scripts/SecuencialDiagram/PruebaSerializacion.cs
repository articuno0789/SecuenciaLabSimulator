using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PruebaSerializacion : MonoBehaviour
{
    public GameObject moduloPrueba; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            Modulo1 mod1 = moduloPrueba.GetComponent<Modulo1>(); 
            Debug.Log("TRATANDO DE EMPEZAR SERIALIZACION");
            string fileName = "PruebaSerializacion.json";
            string jsonString;
            jsonString = JsonUtility.ToJson(moduloPrueba.GetComponent<Modulo1>());
            Debug.Log(jsonString);
            File.WriteAllText(@"C:/Users/Cristian Castillo/Desktop/777" + "/" + fileName, jsonString);
        }
    }
}
