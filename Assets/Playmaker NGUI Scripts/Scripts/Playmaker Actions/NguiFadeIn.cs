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
[HutongGames.PlayMaker.Tooltip("Fades in a group of NGUI widgets. Fades in all children. Sets the widgets to Active before Fade in")]
public class NguiFadeIn : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Parent widget. All children of this widget will be faded in")]
    public FsmOwnerDefault NguiParent;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Duration of the fade in, in seconds")]
    public FsmFloat Duration;

    /// <summary>
    /// Timer for fading out
    /// </summary>
    private float FadeInTimer;

    public override void Reset()
    {
        NguiParent = null;
        Duration = null;
        FadeInTimer = 0f;
    }

    public override void OnEnter()
    {
        DoFadeIn();
    }

    public override void OnUpdate()
    {
        // update timer
        FadeInTimer += Time.deltaTime;

        // only call finish after the fade in has finished
        if (FadeInTimer > (Duration.Value + 0.5f))
            Finish();
    }

    private void DoFadeIn()
    {
        // show obsolete message
        Debug.LogError("NGUI Fade In and NGUI Fade Out are obsolete for NGUI v3+. NGUI v3 made breaking changes to how tween fading works, which broke the functionality of these two actions");
        return;

        //// exit if objects are null
        //if ((NguiParent == null) || (Duration == null))
        //    return;

        //// attach fade in scripts
        //AttachFadeIn(Fsm.GetOwnerDefaultTarget(NguiParent));
    }

    /// <summary>
    /// loops through all children of parent and attaches a fade in script
    /// </summary>
    /// <param name="parent"></param>
    private void AttachFadeIn(GameObject parent)
    {
        // activate parent and all children
        NGUITools.SetActive(parent, true);

        // loop through all children
        int j = parent.transform.childCount;
        for (int i = 0; i < j; i++)
        {
            // get the child
            GameObject child = parent.transform.GetChild(i).gameObject;

            // activate the child if disabled
            if (!child.active)
                NGUITools.SetActive(child, true);

            // check if we already have a fade script attached
            TweenFade Twf = child.GetComponent<TweenFade>();

            // call reset if we have one
            if (Twf != null)
                Twf.Reset();

            // attach the tween fade script
            else
                Twf = child.AddComponent<TweenFade>();

            // finish
            Twf.AttachFadeIn(Duration.Value);
        }
    }
}