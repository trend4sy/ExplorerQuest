using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        funFactPanel.SetActive(false);
        celebrationPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        LoadQuestion();
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
