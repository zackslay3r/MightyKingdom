using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
  
public static LoadingScreen Instance{get; private set;}

public PanelLoader pl;
private void Awake() {
    
    if(Instance != null)
    {
        Destroy(this);
        return;
    }
    Instance = this;
    DontDestroyOnLoad(this);
    
}

public void LoadScene(string name)
{
    pl.LoadPanel();
    gameObject.SetActive(true);
    StartCoroutine(LoadSceneIE(name));
}

private IEnumerator LoadSceneIE(string name)
{
    
    yield return new WaitForSeconds(1);
    AsyncOperation op = SceneManager.LoadSceneAsync("MightyKingdom");
    yield return op;
    gameObject.SetActive(false);

}

}
