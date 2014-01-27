
/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

public class AssetBundleInfo
{
    #region Variables

    /// <summary>
    /// URL of bundle - including .unity3d extension
    /// </summary>
    public string BundleURL;

    /// <summary>
    /// Asset name to load from bundle
    /// </summary>
    public string AssetName;

    /// <summary>
    /// Bundle version number
    /// </summary>
    public int VersionNumber;

    // Variables
    #endregion

    #region Init

    /// <summary>
    /// Initializes a new instance of the AssetBundleInfo class.
    /// </summary>
    /// <param name="bundleURL"></param>
    /// <param name="assetName"></param>
    /// <param name="versionNumber"></param>
    public AssetBundleInfo(string bundleURL, string assetName, int versionNumber)
    {
        BundleURL = bundleURL;
        AssetName = assetName;
        VersionNumber = versionNumber;
    }

    // Init
    #endregion

    #region BundleName

    /// <summary>
    /// Returns a unique string name, used for storing in the cache
    /// </summary>
    /// <returns></returns>
    public string BundleName()
    {
        return string.Format("{0} : {1}", VersionNumber.ToString().Trim(), BundleURL);
    }

    /// <summary>
    /// Returns a unique string name, used for storing in the cache
    /// </summary>
    /// <returns></returns>
    public static string BundleName(int versionNumber, string bundleURL)
    {
        return string.Format("{0} : {1}", versionNumber.ToString().Trim(), bundleURL);
    }

    // BundleName
    #endregion
}
