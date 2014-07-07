using HutongGames.PlayMaker;
using UnityEngine;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

/// <summary>
/// Fades out a group of NGUI widgets. Fades out all children
/// </summary>
[System.Obsolete("NGUI Fade In and NGUI Fade Out are obsolete for NGUI v3+. NGUI v3 made breaking changes to how tween fading works, which broke the functionality of these two actions")]
[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Fades out a group of NGUI widgets. Fades out all children. Sets the widgets to Inactive after fadeout")]
public class NguiFadeOut : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Parent widget. All children of this widget will be faded out")]
    public FsmOwnerDefault NguiParent;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Duration of the fade out, in seconds")]
    public FsmFloat Duration;

    /// <summary>
    /// Timer for fading out
    /// </summary>
    private float FadeOutTimer;

    public override void Reset()
    {
        NguiParent = null;
        Duration = null;
        FadeOutTimer = 0f;
    }

    public override void OnEnter()
    {
        DoFadeOut();
    }

    public override void OnUpdate()
    {
        // update timer
        FadeOutTimer += Time.deltaTime;

        // only call finish after the fadeout has finished
        if (FadeOutTimer > (Duration.Value + 0.5f))
            Finish();
    }

    private void DoFadeOut()
    {
        // show obsolete message
        Debug.LogError("NGUI Fade In and NGUI Fade Out are obsolete for NGUI v3+. NGUI v3 made breaking changes to how tween fading works, which broke the functionality of these two actions");
        return;

        //// exit if objects are null
        //if ((NguiParent == null) || (Duration == null))
        //    return;

        //// attach fade out scripts
        //AttachFadeOut(Fsm.GetOwnerDefaultTarget(NguiParent));
    }

    /// <summary>
    /// loops through all children of parent and attaches a fadeout script
    /// </summary>
    /// <param name="parent"></param>
    private void AttachFadeOut(GameObject parent, bool deactivate = true)
    {
        // loop through all children
        int j = parent.transform.childCount;
        for (int i = 0; i < j; i++)
        {
            // get the child
            GameObject child = parent.transform.GetChild(i).gameObject;

            // skip if disabled
            if (!child.active)
                continue;

            // check if we already have a fade script attached
            TweenFade Twf = child.GetComponent<TweenFade>();

            // call reset if we have one
            if (Twf != null)
                Twf.Reset();

                // attach the tween fade script
            else
                Twf = child.AddComponent<TweenFade>();

            // finish
            Twf.AttachFadeout(Duration.Value, deactivate);


            // NOTE: sliders and progressbars do not fade out the background image
            // I tried to find a work around (the code below attaches a fade script to each background image),
            // BUT, during Fade In, the background images do not reappear. I believe this is a bug in how NGUI
            // handles SetActive (and SetActiveSelf).

            //// continue if NOT progressbar or slider
            //if (child.GetComponent<UISlider>() == null)
            //    continue;

            //// if this is a progressbar or slider, then add one to the background image also
            //AttachFadeOut(child, false);
        }
    }
}