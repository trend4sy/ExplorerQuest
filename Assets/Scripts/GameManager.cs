using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalStars = 0;
    public int currentWorld = 0;

    // عوالم اللعبة المتاحة
    public string[] worldNames = { "Space", "Ocean", "Forest", "Body" };
    public int[]    worldUnlockCost = { 0, 3, 6, 10 };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddStar()
    {
        totalStars++;
        SaveProgress();
    }

    public bool IsWorldUnlocked(int index)
    {
        return totalStars >= worldUnlockCost[index];
    }

    public void LoadWorld(int index)
    {
        if (IsWorldUnlocked(index))
        {
            currentWorld = index;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void SaveProgress()
    {
        PlayerPrefs.SetInt("TotalStars", totalStars);
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        totalStars = PlayerPrefs.GetInt("TotalStars", 0);
    }
}
