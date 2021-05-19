using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleSceneManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static SingleSceneManager _instance;
    public static SingleSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SingleSceneManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Bicycle");
                    _instance = container.AddComponent<SingleSceneManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    [Header("Settings")]
    public bool pressKeyReloadScene;
    public KeyCode keyReloadScene = KeyCode.R;

    private void Update()
    {

        // If reload scene is enabled and key is pressed, reload scene
        if (pressKeyReloadScene && Input.GetKeyDown(keyReloadScene))
            ReloadScene();
    }

    /// <summary>
    /// Reload current Scene
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
