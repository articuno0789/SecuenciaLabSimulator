using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod9PushButton : MonoBehaviour
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
        Debug.Log("Entra a presionar boton azul circular modulo 9--");
        animation.Play("Mod9PresBotonCircularAzul");
    }
}
