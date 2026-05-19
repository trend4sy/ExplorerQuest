using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ضعه على أي GameObject لتشغيل تأثير نجمة طائرة
public class StarAnimation : MonoBehaviour
{
    public GameObject starPrefab;  // prefab لنجمة (Image بداخلها ⭐)
    public RectTransform canvas;

    public void SpawnStars(int count = 5)
    {
        StartCoroutine(AnimateStars(count));
    }

    IEnumerator AnimateStars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (starPrefab != null)
            {
                GameObject s = Instantiate(starPrefab, canvas);
                RectTransform rt = s.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(
                    Random.Range(-200f, 200f),
                    Random.Range(-100f, 100f)
                );
                StartCoroutine(FloatStar(rt));
            }
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator FloatStar(RectTransform rt)
    {
        float duration = 1.5f;
        float elapsed  = 0f;
        Vector2 start  = rt.anchoredPosition;
        Vector2 end    = start + new Vector2(Random.Range(-50f, 50f), 200f);
        CanvasGroup cg = rt.GetComponent<CanvasGroup>();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rt.anchoredPosition = Vector2.Lerp(start, end, t);
            if (cg != null) cg.alpha = 1f - t;
            yield return null;
        }

        Destroy(rt.gameObject);
    }
}
