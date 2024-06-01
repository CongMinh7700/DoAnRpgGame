using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private void Start()
    {
        LoadData("");
    }
    public virtual void SaveData(string id)
    {
        string dataPath = GetIDPath(id);
        if (System.IO.File.Exists(dataPath))
        {
            System.IO.File.Delete(dataPath);
            Debug.Log("Existing SaveScripts data with id : " + id + "is overwriting");
        }
        try
        {
            SceneData sceneData = new SceneData();
            sceneData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string jsonData = JsonUtility.ToJson(sceneData);
            System.IO.File.WriteAllText(dataPath, jsonData);
            Debug.Log("<color=green>Data successfully saved! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Check StartScene Scrupts");
        }


    }
    public virtual void LoadData(string id)
    {
        string dataPath = GetIDPath(id);
        if (!System.IO.File.Exists(dataPath))
        {
            Debug.LogWarning("No saved data exists for the provided id: " + id);
            return;
        }
        try
        {
            string json = System.IO.File.ReadAllText(dataPath);

            SceneData sceneData = JsonUtility.FromJson<SceneData>(json);
            StartScene.sceneIndex = sceneData.sceneIndex;
            Debug.Log("<color=green>Data succesfully loaded! </color>");
        }
        catch
        {
            Debug.LogError("Could not load container data! Check StartScene");
        }
    }
    protected virtual string GetIDPath(string id)
    {
        return Application.persistentDataPath + $"/SceneData_{id}.json";
    }
    public void DeleteData(string id)
    {
        string path = GetIDPath(id);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log("Data with id: " + id + " is deleted.");
        }
    }

    [System.Serializable]
    public class SceneData
    {
        public int sceneIndex;
    }
}
