using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    GameObject camaraSound;

    void Awake()
    {
        DontDestroyOnLoad(this);
        camaraSound = GameObject.Find("CamaraEffect");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F10))
        {
            string screenshotIMGName = System.DateTime.Now.ToString();
            string subString = screenshotIMGName.Replace('/', '_');
            string gypsy = subString.Replace(':', '_');
            Debug.Log("Screen shot captured: " + gypsy + ".png");
            ScreenCapture.CaptureScreenshot(gypsy + ".png");
            if(camaraSound != null)
            {
                AudioSource sound = camaraSound.GetComponent<AudioSource>();
                if(sound != null)
                {
                    Debug.Log("Suena sonido camara");
                    sound.Play();
                }
            }
        }
    }
}
