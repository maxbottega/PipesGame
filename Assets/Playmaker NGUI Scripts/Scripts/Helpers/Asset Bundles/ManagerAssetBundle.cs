using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

public class ManagerAssetBundle : MonoBehaviour 
{
    #region Properties

    /// <summary>
    /// When true, the assets are finished downloading
    /// </summary>
    public bool IsDownloadFinished { get; private set; }
    
    // Properties
    #endregion

    #region Variables

    /// <summary>
    /// Queue of bundles pending download
    /// </summary>
    private readonly Queue<AssetBundleInfo> BundleQ = new Queue<AssetBundleInfo>();

    /// <summary>
    /// Holds all loaded asset bundles, so they are duplicated
    /// </summary>
    private readonly Dictionary<string, AssetBundle> AssetBundleDict = new Dictionary<string, AssetBundle>();

    private bool IsCoroutineRunning;

    // Variables
    #endregion

    #region QBundleForDownload

    /// <summary>
    /// Queues an asset bundle for download
    /// </summary>
    public void QBundleForDownload(string BundleURL, string AssetName, int VersionNumber)
    {
        // add to the queue
        BundleQ.Enqueue(new AssetBundleInfo(BundleURL, AssetName, VersionNumber));

        // start the coroutine if it is not already running
        if (! IsCoroutineRunning)
            StartCoroutine(DownloadAndCache());
    }

    // QBundleForDownload
    #endregion

    #region DownloadAndCache

    IEnumerator DownloadAndCache()
    {
        // exit if already running
        if (IsCoroutineRunning)
            yield break;

        // set our running flag
        IsCoroutineRunning = true;
        IsDownloadFinished = false;

        while (BundleQ.Count > 0)
        {
            // Wait for the Caching system to be ready
            while (!Caching.ready)
                yield return null;

            // get a bundle from the queue
            AssetBundleInfo BundleItem = BundleQ.Dequeue();
            
            // check if we have the bundle already from the dictionary
            AssetBundle bundle;
            if (!AssetBundleDict.TryGetValue(BundleItem.BundleName(), out bundle))
            {

                // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
                using (WWW www = WWW.LoadFromCacheOrDownload(BundleItem.BundleURL, BundleItem.VersionNumber))
                {
                    yield return www;
                    if (www.error != null)
                        throw new Exception("WWW download had an error:" + www.error);

                    // get the bundle from the web
                    bundle = www.assetBundle;

                    //// instantiate the bundle
                    //if (string.IsNullOrEmpty(BundleItem.AssetName))
                    //    Instantiate(bundle.mainAsset);
                    //else
                    //    Instantiate(bundle.Load(BundleItem.AssetName));

                    //// Unload the AssetBundles compressed contents to conserve memory
                    //bundle.Unload(false);

                    // add to the queue
                    AssetBundleDict.Add(BundleItem.BundleName(), bundle);

                    // Frees the memory from the web stream
                    www.Dispose();
                }
            }

            // instantiate the bundle
            if (string.IsNullOrEmpty(BundleItem.AssetName))
                Instantiate(bundle.mainAsset);
            else
                Instantiate(bundle.Load(BundleItem.AssetName));
        }

        // exit coroutine
        IsCoroutineRunning = false;
        IsDownloadFinished = true;
    }

    // DownloadAndCache
    #endregion

    #region UnloadBundle

    /// <summary>
    /// Unloads a bundle
    /// </summary>
    public void UnloadBundle(int versionNumber, string bundleURL)
    {
        AssetBundle bundle;
        string BundleName = AssetBundleInfo.BundleName(versionNumber, bundleURL);

        // get from the dictionary cache, exit if it doesn't exit
        if (!AssetBundleDict.TryGetValue(BundleName, out bundle))
            return;

        // unload the asset
        bundle.Unload(true);

        // remove from the dictionary
        AssetBundleDict.Remove(BundleName);
    }

    // UnloadBundle
    #endregion

    #region UnloadBundleCompressedContents

    /// <summary>
    /// Unload the AssetBundle's compressed contents to conserve memory
    /// </summary>
    public void UnloadBundleCompressedContents(int versionNumber, string bundleURL)
    {
        AssetBundle bundle;
        string BundleName = AssetBundleInfo.BundleName(versionNumber, bundleURL);

        // get from the dictionary cache, exit if it doesn't exit
        if (!AssetBundleDict.TryGetValue(BundleName, out bundle))
            return;

        // unload the asset's compressed contents
        bundle.Unload(false);

        // remove from the dictionary
        AssetBundleDict.Remove(BundleName);
    }

    // UnloadBundleCompressedContents
    #endregion
}
