using HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("Asset Bundle")]
[Tooltip("Loads one asset bundle")]
public class LoadAssetBundle : FsmStateAction
{
    [RequiredField]
    [Tooltip("Asset Bundle Manager Prefab")]
    public FsmGameObject BundleManager;

    [RequiredField]
    [Tooltip("URL of asset bundle - including .unity3d extension")]
    public FsmString BundleURL;

    [RequiredField]
    [Tooltip("Asset Name")]
    public FsmString AssetName;

    [RequiredField]
    [Tooltip("Bundle version number")]
    public FsmInt VersionNumber;

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
        AssetName = null;
        VersionNumber = null;
    }
    
    public override void OnUpdate()
    {
        // start the bundle downloader
        if ((!FinishedBundle) && (!IsDownload))
            DoBundleDownload();

        // exit when finished
        if ((! FinishedBundle) && (IsDownload))
        {
            // set flag, and call finish event
            FinishedBundle = true;
            Finish();
        }
    }

    private void DoBundleDownload()
    {
        // exit if objects are null
        if ((BundleURL == null) || (AssetName == null) || (VersionNumber == null) || (BundleManager == null))
            return;

        // create the asset to load
        if (asset == null)
        {
            // set asset
            asset = BundleManager.Value.GetComponent<ManagerAssetBundle>();

            // start the download
            asset.QBundleForDownload(BundleURL.Value, AssetName.Value, VersionNumber.Value);
        }

        // check if the download is done
        // set the downloaded flag
        if (asset.IsDownloadFinished)
            IsDownload = true;
    }
}