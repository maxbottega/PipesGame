using UnityEngine;
using UnityEditor;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

public class ExportAssetBundles
{
    #region PC

#if UNITY_STANDALONE_WIN

    [MenuItem("Assets/Asset Bundles/PC - Build AssetBundle From Selection - With dependencies")]
    static void ExportResource_PC()
    {
        ExportWithDependencies(BuildTarget.StandaloneWindows);
    }

    [MenuItem("Assets/Asset Bundles/PC - Build AssetBundle From Selection - No dependency tracking")]
    static void ExportResourceNoTrack_PC()
    {
        ExportNoDependencies(BuildTarget.StandaloneWindows);
    }

#endif

    // PC
    #endregion

    #region WePlayer

#if UNITY_WEBPLAYER

    [MenuItem("Assets/Asset Bundles/WebPlayer - Build AssetBundle From Selection - With dependencies")]
    static void ExportResourceWeb()
    {
        ExportWithDependencies(BuildTarget.WebPlayer);
    }

    [MenuItem("Assets/Asset Bundles/WebPlayer - Build AssetBundle From Selection - No dependency tracking")]
    static void ExportResourceNoTrackWeb()
    {
        ExportNoDependencies(BuildTarget.WebPlayer);
    }

#endif

    // WePlayer
    #endregion

    #region iOS

#if UNITY_IPHONE

    [MenuItem("Assets/Asset Bundles/iOS - Build AssetBundle From Selection - With dependencies")]
    static void ExportResource_iOS()
    {
        ExportWithDependencies(BuildTarget.iPhone);
    }

    [MenuItem("Assets/Asset Bundles/iOS - Build AssetBundle From Selection - No dependency tracking")]
    static void ExportResourceNoTrack_iOS()
    {
        ExportNoDependencies(BuildTarget.iPhone );
    }

#endif

    // iOS
    #endregion

    #region Android

#if UNITY_ANDROID

    [MenuItem("Assets/Asset Bundles/Android - Build AssetBundle From Selection - With dependencies")]
    static void ExportResource_Android()
    {
        ExportWithDependencies(BuildTarget.Android);
    }

    [MenuItem("Assets/Asset Bundles/Android - Build AssetBundle From Selection - No dependency tracking")]
    static void ExportResourceNoTrack_Android()
    {
        ExportNoDependencies(BuildTarget.Android);
    }

#endif

    // Android
    #endregion

    #region Blackberry

#if UNITY_BLACKBERRY

    [MenuItem("Assets/Asset Bundles/Blackberry - Build AssetBundle From Selection - With dependencies")]
    static void ExportResource_Blackberry()
    {
        ExportWithDependencies(BuildTarget.BB10);
    }

    [MenuItem("Assets/Asset Bundles/Blackberry - Build AssetBundle From Selection - No dependency tracking")]
    static void ExportResourceNoTrack_Blackberry()
    {
        ExportNoDependencies(BuildTarget.BB10);
    }

#endif

    // Blackberry
    #endregion

    #region Win8

    // for now, Windows 8 / Metro falls under PC

    // Win8
    #endregion

    #region ExportWithDependencies

    private static void ExportWithDependencies(BuildTarget buildTarget)
    {
        // Bring up save panel
        string basename = Selection.activeObject ? Selection.activeObject.name : "New Resource";
        string path = EditorUtility.SaveFilePanel("Save Resources", "", basename, "");

        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            Debug.Log(selection.Length);
            Debug.Log(selection[0].name);

            BuildPipeline.BuildAssetBundle(
                Selection.activeObject,
                selection, string.Format("{0}.{1}.unity3d", path, buildTarget.ToString()),
                BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
                buildTarget);

            Selection.objects = selection;
        }
    }

    // ExportWithDependencies
    #endregion

    #region ExportNoDependencies

    private static void ExportNoDependencies(BuildTarget buildTarget)
    {
        // Bring up save panel
        string basename = Selection.activeObject ? Selection.activeObject.name : "New Resource";
        string path = EditorUtility.SaveFilePanel("Save Resources", "", basename, "");

        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            Debug.Log(selection.Length);
            Debug.Log(selection[0].name);

            BuildPipeline.BuildAssetBundle(
                Selection.activeObject,
                selection, string.Format("{0}.{1}.unity3d", path, buildTarget.ToString()),
                BuildAssetBundleOptions.CompleteAssets,
                buildTarget);

            Selection.objects = selection;
        }
    }

    // ExportNoDependencies
    #endregion
}