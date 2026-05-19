using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuizController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI emojiText;
    public Button[] answerButtons;         // 4 أزرار إجابة
    public TextMeshProUGUI[] answerTexts;
    public TextMeshProUGUI funFactText;
    public GameObject funFactPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI progressText;
    public GameObject celebrationPanel;   // لوحة الاحتفال بالنجمة
    public TextMeshProUGUI celebrationText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    [Header("Colors")]
    public Color normalColor    = new Color(0.25f, 0.60f, 1.00f);
    public Color correctColor   = new Color(0.20f, 0.80f, 0.40f);
    public Color wrongColor     = new Color(0.90f, 0.30f, 0.30f);

    List<Question> questions;
    int currentIndex = 0;
    int score = 0;
    bool answered = false;

    string[] worldKeys = { "Space", "Ocean", "Forest", "Body" };

    void Start()
    {
        string worldKey = worldKeys[GameManager.Instance.currentWorld];
        questions = new List<Question>(QuizData.AllQuestions[worldKey]);
        ShuffleList(questions);

        if (questionText == null)
        {
            CreateCanvas();
            CreateQuizUI();
        }
        else
        {
            funFactPanel.SetActive(false);
            celebrationPanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        LoadQuestion();
    }

    void CreateCanvas()
    {
        GameObject canvasObj = new GameObject("GameCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        canvasObj.AddComponent<GraphicRaycaster>();
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }
        transform.SetParent(canvasObj.transform, false);
    }

    TextMeshProUGUI MakeText(Transform parent, string name, string text, float x, float y, float w, float h, int fontSize)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent, false);
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(w, h);
        TextMeshProUGUI tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        return tmp;
    }

    void CreateQuizUI()
    {
        questionText = MakeText(transform, "QuestionText", "", 540, 750, 800, 100, 36);
        emojiText = MakeText(transform, "EmojiText", "", 540, 600, 200, 80, 48);
        scoreText = MakeText(transform, "ScoreText", "⭐ 0", 100, 900, 200, 50, 24);
        scoreText.alignment = TextAlignmentOptions.Left;
        progressText = MakeText(transform, "ProgressText", "0/5", 980, 900, 200, 50, 24);
        progressText.alignment = TextAlignmentOptions.Right;
        answerButtons = new Button[4];
        answerTexts = new TextMeshProUGUI[4];
        for (int i = 0; i < 4; i++)
        {
            int idx = i;
            GameObject btnObj = new GameObject("Answer" + i);
            btnObj.transform.SetParent(transform, false);
            RectTransform rt = btnObj.AddComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(270 + (i % 2) * 540, 380 - (i / 2) * 140);
            rt.sizeDelta = new Vector2(480, 100);
            Image img = btnObj.AddComponent<Image>();
            img.color = normalColor;
            Button btn = btnObj.AddComponent<Button>();
            btn.targetGraphic = img;
            btn.onClick.AddListener(() => OnAnswerClicked(idx));
            answerButtons[i] = btn;
            GameObject txtObj = new GameObject("Text");
            txtObj.transform.SetParent(btnObj.transform, false);
            TextMeshProUGUI txt = txtObj.AddComponent<TextMeshProUGUI>();
            txt.fontSize = 22;
            txt.alignment = TextAlignmentOptions.Center;
            txt.color = Color.white;
            RectTransform txtRt = txtObj.GetComponent<RectTransform>();
            txtRt.anchorMin = Vector2.zero;
            txtRt.anchorMax = Vector2.one;
            txtRt.sizeDelta = Vector2.zero;
            answerTexts[i] = txt;
        }
        GameObject fp = new GameObject("FunFactPanel");
        fp.transform.SetParent(transform, false);
        RectTransform fpRt = fp.AddComponent<RectTransform>();
        fpRt.anchoredPosition = new Vector2(540, 300);
        fpRt.sizeDelta = new Vector2(800, 300);
        Image fpImg = fp.AddComponent<Image>();
        fpImg.color = new Color(0, 0, 0, 0.8f);
        funFactText = MakeText(fp.transform, "FunFactText", "", 0, 0, 700, 200, 20);
        funFactPanel = fp;
        funFactPanel.SetActive(false);
        GameObject cp = new GameObject("CelebrationPanel");
        cp.transform.SetParent(transform, false);
        RectTransform cpRt = cp.AddComponent<RectTransform>();
        cpRt.anchoredPosition = new Vector2(540, 300);
        cpRt.sizeDelta = new Vector2(800, 300);
        Image cpImg = cp.AddComponent<Image>();
        cpImg.color = new Color(0, 0, 0, 0.8f);
        celebrationText = MakeText(cp.transform, "CelebrationText", "🎉", 0, 0, 700, 200, 28);
        celebrationPanel = cp;
        celebrationPanel.SetActive(false);
        GameObject gp = new GameObject("GameOverPanel");
        gp.transform.SetParent(transform, false);
        RectTransform gpRt = gp.AddComponent<RectTransform>();
        gpRt.anchoredPosition = new Vector2(540, 300);
        gpRt.sizeDelta = new Vector2(800, 400);
        Image gpImg = gp.AddComponent<Image>();
        gpImg.color = new Color(0, 0, 0, 0.8f);
        finalScoreText = MakeText(gp.transform, "FinalScoreText", "", 0, 0, 700, 200, 28);
        gameOverPanel = gp;
        gameOverPanel.SetActive(false);
    }

    void LoadQuestion()
    {
        if (currentIndex >= questions.Count)
        {
            ShowGameOver();
            return;
        }

        answered = false;
        Question q = questions[currentIndex];

        questionText.text = q.questionText;
        emojiText.text    = q.emoji;
        progressText.text = (currentIndex + 1) + " / " + questions.Count;
        scoreText.text    = "⭐ " + score;
        funFactPanel.SetActive(false);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerTexts[i].text     = q.answers[i];
            answerButtons[i].image.color = normalColor;
            answerButtons[i].interactable = true;
        }
    }

    public void OnAnswerClicked(int index)
    {
        if (answered) return;
        answered = true;

        Question q = questions[currentIndex];
        bool correct = (index == q.correctIndex);

        // تلوين الأزرار
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = false;
            if (i == q.correctIndex)
                answerButtons[i].image.color = correctColor;
            else if (i == index && !correct)
                answerButtons[i].image.color = wrongColor;
        }

        if (correct)
        {
            score++;
            scoreText.text = "⭐ " + score;
            GameManager.Instance.AddStar();
            StartCoroutine(ShowCelebration(q.funFact));
        }
        else
        {
            funFactText.text = "الإجابة الصحيحة: " + q.answers[q.correctIndex] + "\n\n" + q.funFact;
            funFactPanel.SetActive(true);
        }
    }

    IEnumerator ShowCelebration(string fact)
    {
        celebrationText.text = "🎉 رائع! ⭐\n\n" + fact;
        celebrationPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        celebrationPanel.SetActive(false);
        currentIndex++;
        LoadQuestion();
    }

    public void OnNextQuestion()
    {
        funFactPanel.SetActive(false);
        currentIndex++;
        LoadQuestion();
    }

    void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "أحسنت! 🌟\nحصلت على " + score + " نجمة\nمن أصل " + questions.Count;
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T tmp = list[i]; list[i] = list[j]; list[j] = tmp;
        }
    }
}
