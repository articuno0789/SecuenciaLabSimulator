using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLoading : MonoBehaviour
{
    public GameObject looaderManager;
    public string nameScene = "MainMenuPrueba";
    public string lodingMenuStyle = "StartingLoadingMenu";

    // Start is called before the first frame update
    void Start()
    {
        if(looaderManager != null)
        {
            looaderManager = GameObject.Find("LooaderManager");
        }
        LO_SelectStyle LO_SelSy = looaderManager.GetComponent<LO_SelectStyle>();
        LO_SelSy.SetStyle(lodingMenuStyle);
        LO_LoadScene LO_LoSc = looaderManager.GetComponent<LO_LoadScene>();
        LO_LoSc.ChangeToScene(nameScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
