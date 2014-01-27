using HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("Asset Bundle")]
[Tooltip("Unloads the compressed contents of one asset bundle, to save memory. NOTE: Once you unload the compressed contents, you cannot load any new assets from This bundle")]
public class UnloadAssetBundleCompressedContents : FsmStateAction
{
    [RequiredField]
    [Tooltip("Asset Bundle Manager Prefab")]
    public FsmGameObject BundleManager;

    [RequiredField]
    [Tooltip("URL of asset bundle - including .unity3d extension")]
    public FsmString BundleURL;

    [RequiredField]
    [Tooltip("Bundle version number")]
    public FsmInt VersionNumber;

    private ManagerAssetBundle asset;

    public override void Reset()
    {
        BundleManager = null;
        BundleURL = null;
        VersionNumber = null;
    }

    public override void OnUpdate()
    {
        DoBundleUnload();
        Finish();
    }

    private void DoBundleUnload()
    {
        // exit if objects are null
        if ((BundleURL == null) || (VersionNumber == null) || (BundleManager == null))
            return;

        // unload the bundle
        BundleManager.Value.GetComponent<ManagerAssetBundle>().UnloadBundleCompressedContents(VersionNumber.Value, BundleURL.Value);
    }
}