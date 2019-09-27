using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GreatArcStudios;

public class ModelsMenu : MonoBehaviour
{
    #region Atributos
    // nuestra clase ParticleExamples se convierte en una variedad de cosas a las que se puede hacer referencia
    [Header("Pause Manager")]
    public GameObject PauseMenuManager;
    [Header("Losta de Modelos")]
    public ModelsExamples[] modelSystems;

    // La pistola GameObject
    [Header("Arma")]
    public GameObject gunGameObject;
    [Header("Imagen de Fondo")]
    public GameObject BackgroundImage;
    public bool hideBackgroundImage;

    // Un entero privado para almacenar la posición actual en la matriz
    private int currentIndex;

    // la GameObject prefabricada actualmente mostrado
    private GameObject currentGO;

    // donde generar prefabricados
    [Header("Spawn")]
    public Transform spawnLocation;

    // referencias a los componentes de texto de la interfaz de usuario
    [Header("Párametros Interfaz de Usuario")]
    public Text title;
    public Text description;
    public Text navigationDetails;
    #endregion

    #region Inicializacion
    // configurar el primer elemento del menú y restablecer el CurrentIndex para asegurarse de que esté en cero
    void Start()
    {
        Navigate(0);
        currentIndex = 0;
    }
    #endregion

    #region Comportamiento

    private void Awake()
    {
        if(PauseMenuManager == null)
        {
            PauseMenuManager = GameObject.Find("Pause Menu Manager");
        }
    }

    private void Update()
    {
        /*Si se apreta la tecla de esc se oculta el panel donde se muestra la información de los modelos
          para evitar que interfiera con el menú de pausa.*/
        /*if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuManager.GetComponent<PauseManager>().menuOpen)
        {
            BackgroundImage.SetActive(false);
        }
        else
        {
            BackgroundImage.SetActive(true);
        }*/
        /*if (Input.GetKeyDown(KeyCode.Escape) && hideBackgroundImage)
        {
            
        }*/
    }

    public void ActivarBackgroundImage()
    {
        BackgroundImage.SetActive(true);
    }

    public void DesactivarBackgroundImage()
    {
        BackgroundImage.SetActive(false);
    }

    // Nuestra función pública que se llama por los botones de nuestro menú
    public void Navigate(int i)
    {

        /* Establece la posición actual en la matriz en la posición siguiente o anterior
         * dependiendo de si i es -1 o 1, definido en nuestro evento de botón*/
        currentIndex = (modelSystems.Length + currentIndex + i) % modelSystems.Length;

        // check if there is a currentGO, if there is (if its not null), then destroy it to make space for the new one..
        if (currentGO != null)
            Destroy(currentGO);

        /* ..pawn el objeto de juego relevante basado en la matriz de objetos de juego 
         * potenciales, de acuerdo con el índice actual (posición en la matriz)*/
        currentGO = Instantiate(modelSystems[currentIndex].modelSystemGO, spawnLocation.position + modelSystems[currentIndex].modelPosition, Quaternion.Euler(modelSystems[currentIndex].modelRotation)) as GameObject;
        TransformModel transModel = currentGO.AddComponent<TransformModel>();
        transModel.originalPosition = spawnLocation.position + modelSystems[currentIndex].modelPosition;
        transModel.originalScale = modelSystems[currentIndex].modelScale;
        transModel.originalRotation = modelSystems[currentIndex].modelRotation;
        //currentGO = asignarLogicaModulo(currentGO, modelSystems[currentIndex].nameModel);//Ya no se deberia usar
        currentGO = AuxiliarModulos.AsignarLogicaModulo(currentGO, modelSystems[currentIndex].nameModel);
        currentGO.transform.localScale = modelSystems[currentIndex].modelScale;

        // solo activa el arma GameObject si el efecto actual es un efecto de arma
        gunGameObject.SetActive(modelSystems[currentIndex].isWeaponEffect);

        // configurar los textos de la interfaz de usuario de acuerdo con las cadenas de la matriz
        title.text = modelSystems[currentIndex].title;
        description.text = modelSystems[currentIndex].description;
        navigationDetails.text = "" + (currentIndex + 1) + " de " + modelSystems.Length.ToString();

    }

    /*private GameObject asignarLogicaModulo(GameObject module, string nameModule)
    {
        Debug.Log(modelSystems[currentIndex].title);
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
        else
        if (nameModule == "MotorElectricoAC")
        {
            module.AddComponent<MotorElectricoAC>();
        }
        return module;
    }
    */
    #endregion
}
