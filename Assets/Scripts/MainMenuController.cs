using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI starsText;
    public Button[] worldButtons;       // 4 أزرار، كل زر عالم
    public Image[] lockIcons;           // أيقونات القفل لكل عالم
    public TextMeshProUGUI[] costTexts; // نص "يحتاج X نجمة"

    string[] worldEmojis = { "🚀", "🐠", "🌿", "❤️" };
    Color unlockedColor = new Color(0.4f, 0.85f, 0.5f);
    Color lockedColor   = new Color(0.7f, 0.7f, 0.7f);

    void Start()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        int stars = GameManager.Instance.totalStars;
        starsText.text = "⭐ " + stars + " نجمة";

        for (int i = 0; i < worldButtons.Length; i++)
        {
            bool unlocked = GameManager.Instance.IsWorldUnlocked(i);
            worldButtons[i].interactable = unlocked;
            worldButtons[i].image.color  = unlocked ? unlockedColor : lockedColor;

            if (lockIcons != null && lockIcons.Length > i)
                lockIcons[i].gameObject.SetActive(!unlocked);

            if (costTexts != null && costTexts.Length > i && !unlocked)
                costTexts[i].text = "يحتاج " + GameManager.Instance.worldUnlockCost[i] + " ⭐";
        }
    }

    public void OnWorldButtonClicked(int index)
    {
        GameManager.Instance.LoadWorld(index);
    }

    // زر إعادة الضبط (للاختبار فقط)
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.totalStars = 0;
        RefreshUI();
    }
}
