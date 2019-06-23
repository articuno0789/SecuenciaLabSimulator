using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod8_11_BotonStop : MonoBehaviour
{
    private Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("Entra a presionar boton stop");
        animation.Play("StopButton");
    }
}
