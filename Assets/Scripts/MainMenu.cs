using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public List<GameObject> panels;
    
    public void ChangePanel(string panel)
    {
        foreach (var p in panels)
            p.SetActive(p.name.Contains(panel));
    }

    public void StartGame()
    {
        Application.LoadLevel(1);
    }
}
