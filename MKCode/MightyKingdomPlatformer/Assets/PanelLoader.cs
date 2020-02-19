using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLoader : MonoBehaviour
{
    public GameObject loadingPanel;

    public void LoadPanel()
    {
        loadingPanel.SetActive(true);

    }
}
