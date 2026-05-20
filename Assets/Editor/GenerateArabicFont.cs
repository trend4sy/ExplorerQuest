using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.TextCore.LowLevel;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class GenerateArabicFont : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        string fontAssetPath = "Assets/Resources/NotoNaskhArabic SDF.asset";
        if (AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(fontAssetPath) != null)
            return;

        Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/NotoNaskhArabic.ttf");
        if (font == null)
        {
            return;
        }

        TMP_FontAsset fontAsset = null;
        try
        {
            fontAsset = TMP_FontAsset.CreateFontAsset(font, 90, 5, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Arabic font generation failed in batchmode: " + e.Message);
            return;
        }

        if (fontAsset == null)
        {
            Debug.LogWarning("Arabic font generation returned null");
            return;
        }

        fontAsset.name = "NotoNaskhArabic SDF";
        AssetDatabase.CreateAsset(fontAsset, fontAssetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
