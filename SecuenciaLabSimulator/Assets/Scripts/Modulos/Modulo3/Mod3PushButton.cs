using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod3PushButton : MonoBehaviour
{
    private Animation ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Entra a presionar boton");
        ani.Play("Mod3PresBotonCircular");
    }
}
