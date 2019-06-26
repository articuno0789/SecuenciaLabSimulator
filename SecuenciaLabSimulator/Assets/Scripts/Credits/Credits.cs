using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Credits : MonoBehaviour
{
    public float speed = 30;
    public float minSpeed = -200;
    public float maxSpeed = 200;
    public float limiteMaximoCreditos = 2700;
    public float limiteMinimoCreditos = 0;
    public bool pause = false;
    public bool finCreditos = false;
    public GameObject elementsCredits;
    public GameObject navegationInstruction;
    public GameObject loading;
    public GameObject mainMusic;

    // Start is called before the first frame update
    void Start()
    {
        elementsCredits = GameObject.Find("ElementsCredits");
        navegationInstruction = GameObject.Find("NavegationInstruction");
        loading = GameObject.Find("LoadSound");
        mainMusic = GameObject.Find("MainMusic");
    }

    // Update is called once per frame
    void Update()
    {
        if (!finCreditos)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (speed < maxSpeed && speed > minSpeed - 1)
                {
                    speed = speed + 1;
                }
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (speed < maxSpeed + 1 && speed > minSpeed)
                {
                    speed = speed - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.P))
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
            if (rect.transform.position.y >= limiteMaximoCreditos - 200)
            {
                navegationInstruction.SetActive(false);
            }
            if (rect.transform.position.y >= limiteMaximoCreditos && !finCreditos)
            {
                Debug.Log("----Entra finalizar creditos");
                finCreditos = true;
                pause = true;
                AudioSource main = mainMusic.GetComponent<AudioSource>();
                main.volume = 0.00f;
                AudioSource loadingEffect = loading.GetComponent<AudioSource>();
                StartCoroutine(goLevel(3, "MainMenuPrueba"));
                loadingEffect.Play();
            }
            else if (rect.transform.position.y <= -400)
            {
                speed = maxSpeed;
            }
            if (!pause)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("MainMenuPrueba");
    }

    IEnumerator goLevel(int seconds, string levelName)
    {
        Debug.Log("------------------------------------------");
        Debug.Log("Inicia ir al " + levelName + seconds + " segundos.");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Finaliza carga.");
        SceneManager.LoadScene(levelName);
    }

}
