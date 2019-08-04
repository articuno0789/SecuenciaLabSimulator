using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLoading : MonoBehaviour
{
    #region Atributos
    public GameObject looaderManager;
    public string nameScene = "MainMenuPrueba";
    public string lodingMenuStyle = "StartingLoadingMenu";
    #endregion

    #region Inicializacion
    /*En este método se inicia el proceso de carga hacia el menú principal, cuando unicia el simulador.*/
    void Start() // Puede ser Awake?
    {
        if (looaderManager != null)
        {
            looaderManager = GameObject.Find("LooaderManager");
        }
        LO_SelectStyle LO_SelSy = looaderManager.GetComponent<LO_SelectStyle>();
        LO_SelSy.SetStyle(lodingMenuStyle);
        LO_LoadScene LO_LoSc = looaderManager.GetComponent<LO_LoadScene>();
        LO_LoSc.ChangeToScene(nameScene);
    }
    #endregion

    #region Comportamiento
    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}
