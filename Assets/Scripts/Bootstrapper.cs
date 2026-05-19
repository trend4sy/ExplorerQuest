using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public static class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        GameObject canvasObj = CreateCanvasAndEventSystem();
        GameObject gmObj = new GameObject("GameManager");
        gmObj.AddComponent<GameManager>();
        GameObject mmObj = new GameObject("MainMenuController");
        mmObj.transform.SetParent(canvasObj.transform, false);
        MainMenuController mmc = mmObj.AddComponent<MainMenuController>();
        TextMeshProUGUI starsText = CreateText(canvasObj, "StarsText", "⭐ 0 نجمة", 540, 900, 500, 60);
        starsText.alignment = TextAlignmentOptions.Center;
        mmc.starsText = starsText;
        mmc.worldButtons = new Button[4];
        mmc.lockIcons = new Image[4];
        mmc.costTexts = new TextMeshProUGUI[4];
        string[] emojis = { "🚀", "🐠", "🌿", "❤️" };
        string[] names = { "الفضاء", "البحر", "الغابة", "جسم الإنسان" };
        for (int i = 0; i < 4; i++)
        {
            int idx = i;
            GameObject btnObj = CreateButton(canvasObj, "World" + i, emojis[i] + " " + names[i], 540, 700 - 130 * i, 400, 90);
            Button btn = btnObj.GetComponent<Button>();
            btn.onClick.AddListener(() => mmc.OnWorldButtonClicked(idx));
            mmc.worldButtons[i] = btn;
            GameObject lockObj = new GameObject("Lock" + i);
            lockObj.transform.SetParent(btnObj.transform, false);
            Image lockImg = lockObj.AddComponent<Image>();
            lockImg.color = Color.gray;
            RectTransform lockRt = lockObj.GetComponent<RectTransform>();
            lockRt.anchoredPosition = new Vector2(-150, 0);
            lockRt.sizeDelta = new Vector2(40, 40);
            mmc.lockIcons[i] = lockImg;
            GameObject costObj = new GameObject("Cost" + i);
            costObj.transform.SetParent(btnObj.transform, false);
            TextMeshProUGUI costText = costObj.AddComponent<TextMeshProUGUI>();
            costText.text = "يحتاج X ⭐";
            costText.fontSize = 14;
            costText.alignment = TextAlignmentOptions.Center;
            RectTransform costRt = costObj.GetComponent<RectTransform>();
            costRt.anchoredPosition = new Vector2(150, 0);
            costRt.sizeDelta = new Vector2(200, 40);
            mmc.costTexts[i] = costText;
        }
    }

    static GameObject CreateCanvasAndEventSystem()
    {
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        canvasObj.AddComponent<GraphicRaycaster>();
        if (Object.FindObjectOfType<EventSystem>() == null)
        {
            GameObject esObj = new GameObject("EventSystem");
            esObj.AddComponent<EventSystem>();
            esObj.AddComponent<StandaloneInputModule>();
        }
        return canvasObj;
    }

    static TextMeshProUGUI CreateText(GameObject parent, string name, string text, float x, float y, float w, float h)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent.transform, false);
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(w, h);
        TextMeshProUGUI tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 24;
        tmp.color = Color.white;
        return tmp;
    }

    static GameObject CreateButton(GameObject parent, string name, string text, float x, float y, float w, float h)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent.transform, false);
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(w, h);
        Image img = obj.AddComponent<Image>();
        img.color = new Color(0.2f, 0.5f, 0.8f);
        Button btn = obj.AddComponent<Button>();
        btn.targetGraphic = img;
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(obj.transform, false);
        TextMeshProUGUI txt = textObj.AddComponent<TextMeshProUGUI>();
        txt.text = text;
        txt.fontSize = 20;
        txt.alignment = TextAlignmentOptions.Center;
        txt.color = Color.white;
        RectTransform txtRt = textObj.GetComponent<RectTransform>();
        txtRt.anchorMin = Vector2.zero;
        txtRt.anchorMax = Vector2.one;
        txtRt.sizeDelta = Vector2.zero;
        return obj;
    }
}
