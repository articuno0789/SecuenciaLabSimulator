using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod8_11_Perilla : MonoBehaviour
{
    private Animation animation;
    private bool ani = true;

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
        if (ani)
        {
            Debug.Log("Entra a presionar Perilla MA");
            animation.Play("PerillaMA");
            ani = false;
        }
        else
        {
            Debug.Log("Entra a presionar Perilla AM");
            animation.Play("PerillaAM");
            ani = true;
        }
    }
}
