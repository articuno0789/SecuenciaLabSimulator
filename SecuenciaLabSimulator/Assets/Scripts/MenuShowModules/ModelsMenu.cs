using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ModelsMenu : MonoBehaviour
{

    // our ParticleExamples class being turned into an array of things that can be referenced
    public ModelsExamples[] modelSystems;

    // the gun GameObject
    public GameObject gunGameObject;

    public GameObject BackgroundImage;

    public bool hideBackgroundImage;

    // a private integer to store the current position in the array
    private int currentIndex;

    // the currently shown prefab game object
    private GameObject currentGO;

    // where to spawn prefabs 
    public Transform spawnLocation;

    // references to the UI Text components
    public Text title;
    public Text description;
    public Text navigationDetails;

    // setting up the first menu item and resetting the currentIndex to ensure it's at zero
    void Start()
    {
        Navigate(0);
        currentIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && hideBackgroundImage)
        {
            if (BackgroundImage.activeSelf)
            {
                BackgroundImage.SetActive(false);
            }
            else
            {
                BackgroundImage.SetActive(true);
            }
        }
    }

    // our public function that gets called by our menu's buttons
    public void Navigate(int i)
    {

        // set the current position in the array to the next or previous position depending on whether i is -1 or 1, defined in our button event
        currentIndex = (modelSystems.Length + currentIndex + i) % modelSystems.Length;

        // check if there is a currentGO, if there is (if its not null), then destroy it to make space for the new one..
        if (currentGO != null)
            Destroy(currentGO);

        // ..spawn the relevant game object based on the array of potential game objects, according to the current index (position in the array)
        currentGO = Instantiate(modelSystems[currentIndex].modelSystemGO, spawnLocation.position + modelSystems[currentIndex].modelPosition, Quaternion.Euler(modelSystems[currentIndex].modelRotation)) as GameObject;
        currentGO.AddComponent<TransformModel>();
        currentGO = asignarLogicaModulo(currentGO, modelSystems[currentIndex].nameModel);
        currentGO.transform.localScale = modelSystems[currentIndex].modelScale;

        // only activate the gun GameObject if the current effect is a weapon effect
        gunGameObject.SetActive(modelSystems[currentIndex].isWeaponEffect);

        // setup the UI texts according to the strings in the array 
        title.text = modelSystems[currentIndex].title;
        description.text = modelSystems[currentIndex].description;
        navigationDetails.text = "" + (currentIndex + 1) + " de " + modelSystems.Length.ToString();

    }

    private GameObject asignarLogicaModulo(GameObject module, string nameModule)
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
        }else 
        if (nameModule == "MotorElectricoAC")
        {
            module.AddComponent<MotorElectricoAC>();
        }
        return module;
    }


}
