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
        TMP_FontAsset existing = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(fontAssetPath);
        if (existing != null)
            return;

        Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/NotoNaskhArabic.ttf");
        if (font == null)
        {
            Debug.LogError("Cannot find Assets/Resources/NotoNaskhArabic.ttf");
            return;
        }

        TMP_FontAsset fontAsset = TMP_FontAsset.CreateFontAsset(
            font,
            90,
            5,
            GlyphRenderMode.SDFAA,
            1024,
            1024,
            AtlasPopulationMode.Dynamic
        );

        fontAsset.name = "NotoNaskhArabic SDF";
        AssetDatabase.CreateAsset(fontAsset, fontAssetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
