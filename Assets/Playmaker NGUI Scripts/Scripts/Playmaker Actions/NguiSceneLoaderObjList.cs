using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * 
 * Updated for NGUI v3
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Sends each object in the list to the GPU for pre-rendering")]
public class NguiSceneLoaderObjList : FsmStateAction
{
    #region Public_PlayMaker_Variables
    
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The camera used to load the objects into the GPU")]
    public FsmOwnerDefault SceneLoaderCamera;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The progressbar")]
    public FsmGameObject NguiProgressbar;

    [HutongGames.PlayMaker.Tooltip("The progressbar label")]
    public FsmGameObject NguiProgressbarLabel;

    /// <summary>
    /// NOTE: I generally only set this for demo purposes, as it forces a delay between each object being loaded and slows down the scene loader.
    /// It's really only useful to force showing of the loading screen
    /// </summary>
    [HutongGames.PlayMaker.Tooltip("When set, introduces a delay between each object being loaded. Useful to force showing of the progressbar")]
    public FsmFloat DelayBetweenObjectLoads;

    [CompoundArray("PreRenderList", "SceneObject", "ParseChildren")]

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("GameObject to pre-render to GPU")]
    public FsmGameObject[] SceneObjects;

    [HutongGames.PlayMaker.Tooltip("When checked, the camera will visit each child of the scene object.")]
    public FsmBool[] ParseChildrens;
    
    // Public_PlayMaker_Variables
    #endregion

    #region Private_Variables
    
    /// <summary>
    /// When true, we've finished loading the scene
    /// </summary>
    private bool IsFinishedLoading;

    /// <summary>
    /// List of GameObjects to load from the scene
    /// </summary>
    private readonly Queue<LoaderInfo> LoadingQ = new Queue<LoaderInfo>();

    /// <summary>
    /// Current object from the Q
    /// </summary>
    private LoaderInfo CurrentLoaderGO;

    /// <summary>
    /// The current GO we are focused on with the camera
    /// </summary>
    private GameObject CurrentGO;

    /// <summary>
    /// When true, the parent is set to inactive once all the children have been loaded
    /// </summary>
    private bool SetParentInactive;

    /// <summary>
    /// When true, the current GO will be set inactive after it is loaded.
    /// </summary>
    private bool SetToInactive;

    /// <summary>
    /// Used to increment the progress bar
    /// </summary>
    private float ProgressStepValue;

    private UISlider SldProgressbar;
    private UILabel LblProgressText;
    private Camera CameraLoader;

    /// <summary>
    /// Time when next object can be loaded
    /// </summary>
    private float TimeToLoadNextObject;
    
    // Private_Variables
    #endregion

    #region Reset
    
    public override void Reset()
    {
        SceneLoaderCamera = null;
        NguiProgressbar = null;
        NguiProgressbarLabel = null;
        SceneObjects = null;
        ParseChildrens = null;
        DelayBetweenObjectLoads = null;
    }
    
    // Reset
    #endregion

    #region OnEnter

    public override void OnEnter()
    {
        // build LoadingQ

        // Number of objects in the scene to parse
        int NumObjectsToParse = 0;

        // loop through each object in the list
        int j = SceneObjects.Length;
        for (int i = 0; i < j; i++)
        {
            // skip if null
            if (SceneObjects[i] == null)
                continue;

            // create the loader info
            LoaderInfo LoaderSceneObj = new LoaderInfo { LoaderGO = SceneObjects[i].Value };
            NumObjectsToParse++;

            // add children, if checked
            if ((ParseChildrens[i] != null) && (ParseChildrens[i].Value))
            {
                // get child count
                int ChildCount = LoaderSceneObj.LoaderGO.transform.childCount;

                // loop through and add the children
                for (int iChild = 0; iChild < ChildCount; iChild++)
                {
                    LoaderSceneObj.ChildQ.Enqueue(LoaderSceneObj.LoaderGO.transform.GetChild(iChild).gameObject);
                    NumObjectsToParse++;
                }
            }

            // add to the queue
            LoadingQ.Enqueue(LoaderSceneObj);
        }

        // unbox the progressbar
        SldProgressbar = NguiProgressbar.Value.GetComponent<UISlider>();

        // unbox the label
        if (NguiProgressbarLabel != null)
            LblProgressText = NguiProgressbarLabel.Value.GetComponent<UILabel>();

        // unbox the loader camera
        CameraLoader = Fsm.GetOwnerDefaultTarget(SceneLoaderCamera).GetComponent<Camera>();

        // set the steps for the progressbar update
        ProgressStepValue = (float) NumObjectsToParse / 10000;

        // set progressbar values
        SetProgressbarValue(0);
    }

    // OnEnter
    #endregion

    #region OnUpdate
    
    public override void OnUpdate()
    {
        // for safety, exit if finished
        if (IsFinishedLoading)
            return;

        // exit if we are still waiting for the next object time
        if (TimeToLoadNextObject > Time.time)
            return;

        // if we have an existing object we're tracking, we can clear it now
        if (CurrentGO != null)
        {
            // check if we need to set this to inactive
            if (SetToInactive)
                CurrentGO.SetActiveRecursively(false);

            // reset flag
            SetToInactive = false;
        }

        // process if we have an existing item from the Q
        if (CurrentLoaderGO != null)
        {
            // check if we have a parent, and have finished all it's children
            if (CurrentLoaderGO.ChildQ.Count == 0)
            {
                // move the camera to the parent, if it is NOT the current go
                if (!CurrentLoaderGO.LoaderGO.Equals(CurrentGO))
                {
                    // move the camera
                    MoveCameraToGO(CurrentLoaderGO.LoaderGO);

                    // exit update, so the the camera will render the object
                    return;
                }

                // parent has already been moved, time to clean up
                if (SetParentInactive)
                    CurrentLoaderGO.LoaderGO.SetActiveRecursively(false);

                // clear the flag
                SetParentInactive = false;

                // clear parent
                CurrentLoaderGO = null;
            }

            // we have children which need to be processed
            else
            {
                // get the next child
                CurrentGO = CurrentLoaderGO.ChildQ.Dequeue();

                // set inactive flag
                SetToInactive = !CurrentGO.active;

                // move the camera to this object
                MoveCameraToGO(CurrentGO);

                // exit update, so the the camera will render the object
                return;
            }
        }

        // when here, we are finished dealing with previous object and need to get the next object
        if (LoadingQ.Count > 0)
        {
            // get the next object from the Q
            CurrentLoaderGO = LoadingQ.Dequeue();

            // check if we have children
            if (CurrentLoaderGO.ChildQ.Count > 0)
            {
                // assign a child
                CurrentGO = CurrentLoaderGO.ChildQ.Dequeue();

                // set active flags
                SetParentInactive = !CurrentLoaderGO.LoaderGO.active;
                SetToInactive = !CurrentGO.active;

                // move the camera
                MoveCameraToGO(CurrentGO);

                // exit update, so the the camera will render the object
                return;
            }

            // no children, use the loader GO
            CurrentGO = CurrentLoaderGO.LoaderGO;

            // set active flag
            SetToInactive = !CurrentGO.active;

            // move the camera
            MoveCameraToGO(CurrentGO);

            // exit update, so the the camera will render the object
            return;
        }

        // nothing left to process!
        IsFinishedLoading = true;

        // don't call finish until we finish loading everything
        if (IsFinishedLoading)
            Finish();
    }
    
    // OnUpdate
    #endregion

    #region MoveCameraToGO

    /// <summary>
    /// Moves the camera to the GameObject
    /// </summary>
    /// <param name="goToLoad"></param>
    private void MoveCameraToGO(GameObject goToLoad)
    {
        // set current go
        CurrentGO = goToLoad;

        // activate the GO
        if (!goToLoad.active)
        {
            SetToInactive = true;
            goToLoad.SetActiveRecursively(true);
        }

        // move the camera
        CameraLoader.transform.position = goToLoad.transform.position * 2;
        CameraLoader.transform.LookAt(goToLoad.transform);

        // update progressbar
        SetProgressbarValue(ProgressStepValue);

        // set delay time
        if (DelayBetweenObjectLoads != null)
            TimeToLoadNextObject = Time.time + DelayBetweenObjectLoads.Value;
    }

    // MoveCameraToGO
    #endregion

    #region SetProgressbarValue

    /// <summary>
    /// Sets the value of the progressbar and its label
    /// </summary>
    /// <param name="increment">amount to increment the progressbar</param>
    private void SetProgressbarValue(float increment)
    {
        // check if we need to clear the progressbar
        if (increment <= 0)
        {
            // set value to zero
            SldProgressbar.value = 0;
        }

        // add to the value
        else
        {
            SldProgressbar.value += increment;
        }

        // update the label, if one is assigned
        if (LblProgressText != null)
            LblProgressText.text = string.Format("{0:N2}%", SldProgressbar.value * 100);
    }

    // SetProgressbarValue
    #endregion
}

/// <summary>
/// Info about objects which need to be loaded
/// </summary>
public class LoaderInfo
{
    /// <summary>
    /// The GameObject to load
    /// </summary>
    public GameObject LoaderGO { get; set; }

    /// <summary>
    /// Queue of children which need to be loaded before the parent
    /// </summary>
    public Queue<GameObject> ChildQ = new Queue<GameObject>();
}