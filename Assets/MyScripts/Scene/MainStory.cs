using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStory : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitToLoadScene());
        Debug.Log("Load Scene Village");
    }
    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }
}
