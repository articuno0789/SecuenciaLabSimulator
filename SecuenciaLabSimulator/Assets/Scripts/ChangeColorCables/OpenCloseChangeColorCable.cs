using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseChangeColorCable : MonoBehaviour
{
    public GameObject panelChangeColorCable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowHideChangeColorCable()
    {
        if (panelChangeColorCable.activeSelf)
        {
            panelChangeColorCable.SetActive(false);
        }
        else
        {
            panelChangeColorCable.SetActive(true);
        }
    }
}
