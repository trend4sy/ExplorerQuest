using UnityEngine;
using UnityEngine.SceneManagement;

public static class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        if (GameObject.Find("GameManager") != null) return;
        GameObject gmObj = new GameObject("GameManager");
        gmObj.AddComponent<GameManager>();
        Scene active = SceneManager.GetActiveScene();
        if (active.name == "MainMenu")
        {
            GameObject mmObj = new GameObject("MainMenuController");
            mmObj.AddComponent<MainMenuController>();
        }
        else if (active.name == "GameScene")
        {
            GameObject qcObj = new GameObject("QuizController");
            qcObj.AddComponent<QuizController>();
        }
    }
}
