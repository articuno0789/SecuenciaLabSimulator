using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Credits : MonoBehaviour
{
    #region Atributos
    //Atributos variables
    //GUI
    [Header("GUI - Créditos")]
    public GameObject elementsCredits;
    public GameObject navegationInstruction;
    public GameObject loading;
    public GameObject mainMusic;
    //Variables
    [Header("Párametros de Manipulación Creditos")]
    public float speed = 30;
    public float minSpeed = -200;
    public float maxSpeed = 200;
    public bool pause = false;
    public bool finCreditos = false;
    //Constantes
    [Header("Constantes")]
    public float limiteMaximoCreditos = 2700;
    public float limiteMinimoCreditos = 0;
    public string nombreMenuRetorno = "MainMenuPrueba";
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        if(elementsCredits == null)
        {
            elementsCredits = GameObject.Find("ElementsCredits");
        }
        if (navegationInstruction == null)
        {
            navegationInstruction = GameObject.Find("NavegationInstruction");
        }
        if (loading == null)
        {
            loading = GameObject.Find("LoadSound");
        }
        if (mainMusic == null)
        {
            mainMusic = GameObject.Find("MainMusic");
        }
    }
    #endregion

    #region Comportamiento
    // Update is called once per frame
    void Update()
    {
        if (!finCreditos)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //Acelerar créditos
            {
                if (speed < maxSpeed && speed > minSpeed - 1)
                {
                    speed = speed + 1;
                }
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))//Realentizar créditos
            {
                if (speed < maxSpeed + 1 && speed > minSpeed)
                {
                    speed = speed - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.P))//Pausar créditos
            {
                if (pause)
                {
                    pause = false;
                }
                else
                {
                    pause = true;
                }
            }
            RectTransform rect = elementsCredits.GetComponent<RectTransform>();
            //Eliminar instrucciones de navegación, punto sin retorno
            if (rect.transform.position.y >= limiteMaximoCreditos - 200)
            {
                navegationInstruction.SetActive(false);
            }
            //Si entra a esta condición los créditos finalizan
            if (rect.transform.position.y >= limiteMaximoCreditos && !finCreditos)
            {
                //Debug.Log("----Entra finalizar creditos");
                finCreditos = true;
                pause = true;
                AudioSource main = mainMusic.GetComponent<AudioSource>();
                main.volume = 0.00f;
                AudioSource loadingEffect = loading.GetComponent<AudioSource>();
                StartCoroutine(goLevel(3, "MainMenuPrueba"));
                loadingEffect.Play();
            }
            else if (rect.transform.position.y <= -400) // Para evitar que pueda regresar totalmente los créditos.
            {
                speed = maxSpeed;
            }
            if (!pause)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }

    /*Este método se encarga cambiar de escena hasta el ménu principal.*/
    public void returnToMenu()
    {
        SceneManager.LoadScene(nombreMenuRetorno);
    }

    /*Este método se encarga de realizar la carga de un nivel, despues de determinados segundos.*/
    IEnumerator goLevel(int seconds, string levelName)
    {
        Debug.Log("------------------------------------------");
        Debug.Log("Inicia ir al " + levelName + seconds + " segundos.");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Finaliza carga.");
        SceneManager.LoadScene(levelName);
    }
    #endregion
}
