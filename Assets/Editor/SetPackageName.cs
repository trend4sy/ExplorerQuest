using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class SetPackageName : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.trend4sy.explorerquest");
        PlayerSettings.companyName = "Trend4Sy";
        PlayerSettings.productName = "ExplorerQuest";
    }
}
