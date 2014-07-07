using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("Asset Bundle")]
[HutongGames.PlayMaker.Tooltip("Loads multiple asset bundles")]
public class LoadAssetBundleList : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Asset Bundle Manager Prefab")]
    public FsmGameObject BundleManager;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("URL of asset bundle - including .unity3d extension")]
    public FsmString BundleURL;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Bundle version number")]
    public FsmInt VersionNumber;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Name of the assets to load from the asset bundle")]
    public FsmString[] AssetNames;

    /// <summary>
    /// Track if the download is finished
    /// </summary>
    private bool IsDownload;

    /// <summary>
    /// When true, we are finished processing the bundle
    /// </summary>
    private bool FinishedBundle;

    private ManagerAssetBundle asset;

    public override void Reset()
    {
        BundleManager = null;
        BundleURL = null;
        AssetNames = null;
        VersionNumber = null;
    }

    public override void OnUpdate()
    {
        // start the bundle downloader
        if ((!FinishedBundle) && (!IsDownload))
            DoBundleDownload();

        // exit when finished
        if ((!FinishedBundle) && (IsDownload))
        {
            // set flag, and call finish event
            FinishedBundle = true;
            Finish();
        }
    }

    private void DoBundleDownload()
    {
        // exit if objects are null
        if ((BundleURL == null) || (AssetNames == null) || (VersionNumber == null))
            return;

        // create the asset to load
        if (asset == null)
        {
            // set asset
            asset = BundleManager.Value.GetComponent<ManagerAssetBundle>();

            // start the downloads
            int j = AssetNames.Length;
            for (int i = 0; i < j; i++)
                asset.QBundleForDownload(BundleURL.Value, AssetNames[i].Value, VersionNumber.Value);    
        }

        // check if the download is done
        // set the downloaded flag
        if (asset.IsDownloadFinished)
            IsDownload = true;
    }
}