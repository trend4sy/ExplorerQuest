using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI starsText;
    public Button[] worldButtons;
    public Image[] lockIcons;
    public TextMeshProUGUI[] costTexts;

    string[] worldEmojis = { "🚀", "🐠", "🌿", "❤️" };
    Color unlockedColor = new Color(0.4f, 0.85f, 0.5f);
    Color lockedColor   = new Color(0.7f, 0.7f, 0.7f);
    string[] worldNames = { "الفضاء", "المحيط", "الغابة", "جسم الإنسان" };

    static TMP_FontAsset _arabicFont;
    static TMP_FontAsset ArabicFont
    {
        get
        {
            if (_arabicFont == null)
            {
                _arabicFont = Resources.Load<TMP_FontAsset>("NotoNaskhArabic SDF");
                if (_arabicFont == null)
                {
                    Font unityFont = Resources.Load<Font>("NotoNaskhArabic");
                    if (unityFont != null)
                        _arabicFont = TMP_FontAsset.CreateFontAsset(unityFont);
                }
            }
            return _arabicFont;
        }
    }

    void Start()
    {
        if (starsText == null)
            CreateUI();
        RefreshUI();
    }

    void CreateUI()
    {
        Canvas canvas = Object.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        if (Object.FindObjectOfType<EventSystem>() == null)
        {
            GameObject esObj = new GameObject("EventSystem");
            esObj.AddComponent<EventSystem>();
            esObj.AddComponent<StandaloneInputModule>();
        }

        transform.SetParent(canvas.transform, false);

        GameObject starsObj = new GameObject("StarsText");
        starsObj.transform.SetParent(canvas.transform, false);
        starsText = starsObj.AddComponent<TextMeshProUGUI>();
        starsText.font = ArabicFont;
        starsText.fontSize = 40;
        starsText.alignment = TextAlignmentOptions.Center;
        starsText.isRightToLeftText = true;
        RectTransform starsRect = starsObj.GetComponent<RectTransform>();
        starsRect.anchorMin = new Vector2(0.5f, 0.95f);
        starsRect.anchorMax = new Vector2(0.5f, 0.95f);
        starsRect.pivot = new Vector2(0.5f, 1f);
        starsRect.sizeDelta = new Vector2(600, 60);
        starsRect.anchoredPosition = Vector2.zero;

        int worldCount = 4;
        worldButtons = new Button[worldCount];
        lockIcons = new Image[worldCount];
        costTexts = new TextMeshProUGUI[worldCount];

        for (int i = 0; i < worldCount; i++)
        {
            GameObject container = new GameObject("World" + i);
            container.transform.SetParent(canvas.transform, false);
            RectTransform cRect = container.AddComponent<RectTransform>();
            cRect.anchorMin = new Vector2(0.5f, 0.5f);
            cRect.anchorMax = new Vector2(0.5f, 0.5f);
            cRect.pivot = new Vector2(0.5f, 0.5f);
            cRect.sizeDelta = new Vector2(450, 200);
            float yOffset = 200f - i * 230f;
            cRect.anchoredPosition = new Vector2(0, yOffset);

            GameObject btnObj = new GameObject("WorldButton");
            btnObj.transform.SetParent(container.transform, false);
            RectTransform btnRect = btnObj.AddComponent<RectTransform>();
            btnRect.anchorMin = Vector2.zero;
            btnRect.anchorMax = Vector2.one;
            btnRect.sizeDelta = Vector2.zero;

            Image img = btnObj.AddComponent<Image>();
            Button btn = btnObj.AddComponent<Button>();
            btn.targetGraphic = img;
            worldButtons[i] = btn;

            GameObject txtObj = new GameObject("ButtonText");
            txtObj.transform.SetParent(btnObj.transform, false);
            TextMeshProUGUI txt = txtObj.AddComponent<TextMeshProUGUI>();
            txt.font = ArabicFont;
            txt.text = worldEmojis[i] + " " + worldNames[i];
            txt.fontSize = 44;
            txt.alignment = TextAlignmentOptions.Center;
            txt.isRightToLeftText = true;
            RectTransform txtRect = txtObj.GetComponent<RectTransform>();
            txtRect.anchorMin = Vector2.zero;
            txtRect.anchorMax = Vector2.one;
            txtRect.sizeDelta = Vector2.zero;

            GameObject lockObj = new GameObject("LockIcon");
            lockObj.transform.SetParent(container.transform, false);
            Image lockImg = lockObj.AddComponent<Image>();
            lockImg.color = new Color(0.8f, 0.2f, 0.2f, 0.9f);
            lockIcons[i] = lockImg;
            RectTransform lockRect = lockObj.GetComponent<RectTransform>();
            lockRect.anchorMin = new Vector2(0.85f, 0.85f);
            lockRect.anchorMax = new Vector2(0.85f, 0.85f);
            lockRect.pivot = new Vector2(0.5f, 0.5f);
            lockRect.sizeDelta = new Vector2(50, 50);

            GameObject costObj = new GameObject("CostText");
            costObj.transform.SetParent(container.transform, false);
            TextMeshProUGUI costTxt = costObj.AddComponent<TextMeshProUGUI>();
            costTxt.font = ArabicFont;
            costTxt.fontSize = 22;
            costTxt.alignment = TextAlignmentOptions.Center;
            costTxt.isRightToLeftText = true;
            costTexts[i] = costTxt;
            RectTransform costRect = costObj.GetComponent<RectTransform>();
            costRect.anchorMin = new Vector2(0.85f, 0.7f);
            costRect.anchorMax = new Vector2(0.85f, 0.7f);
            costRect.pivot = new Vector2(0.5f, 1f);
            costRect.sizeDelta = new Vector2(200, 40);

            int captured = i;
            btn.onClick.AddListener(() => OnWorldButtonClicked(captured));
        }
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

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.totalStars = 0;
        RefreshUI();
    }
}
