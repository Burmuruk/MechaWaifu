using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels;
    GameObject currentPanel;
    //AudioManager auidioManager;

    private void Start()
    {
        //auidioManager = GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
        {
            if (currentPanel == null) return;
            currentPanel.SetActive(false);
            //auidioManager.PlaySFX(2);
        }
    }
    public void EnablePanel(int index)
    {
        panels[index].SetActive(true);
        currentPanel = panels[index];
    }
}
