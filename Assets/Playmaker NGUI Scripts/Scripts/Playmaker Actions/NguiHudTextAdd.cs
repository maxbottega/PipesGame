//#define HudTextAvailable

// NOTE: You must uncomment the above define in order for this class to function properly.
//  I had to prevent the class from running for those people who do not have the HUD Text package
//  You can download the HUD Text package here: http://u3d.as/content/tasharen-entertainment/ngui-hud-text/37P


using HutongGames.PlayMaker;
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
[ActionCategory("NGUI")]
#if HudTextAvailable
    [HutongGames.PlayMaker.Tooltip("Adds text to an existing HUD Text object")]
#else
    [HutongGames.PlayMaker.Tooltip("HUD Text action is disabled. You must uncomment the define in NguiHudTextAdd.cs in order to use this action. I had to prevent the class from running for those people who do not have the HUD Text package. You can download the NGUI Hud Text package here: http://bit.ly/1d72um5")]
#endif
public class NguiHudTextAdd : FsmStateAction
{

#if HudTextAvailable

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Object with the HUDText component attached")]
    public FsmOwnerDefault HudTextObject;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("Color of the text")]
    public FsmColor TextColor;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("How long the text should stay in place on the screen, before moving. Set to 0 to begin moving immediately")]
    public FsmInt StayDuration;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The text to display")]
    public FsmString HudString;

    public override void Reset()
    {
        HudTextObject = null;
        TextColor = null;
        StayDuration = null;
        HudString = null;
    }

    public override void OnEnter()
    {
        DoAddHudText();
        Finish();
    }

    public override void OnUpdate()
    {
    }

    private void DoAddHudText()
    {
        // exit if objects are null
        if ((HudTextObject == null) || (TextColor == null) || (StayDuration == null) || (HudString == null))
            return;

        // get the hud text component
        HUDText hudt = Fsm.GetOwnerDefaultTarget(HudTextObject).GetComponent<HUDText>();

        // fail if component is not attached
        if (hudt == null)
        {
            string msg = string.Format("HUDText is not attached to object: {0}", Fsm.GetOwnerDefaultTarget(HudTextObject).name);
            UnityEngine.Debug.LogError(msg);
            FsmDebugUtility.Log(msg);
            return;
        }

        // add the hud text
        hudt.Add(HudString.Value, TextColor.Value, StayDuration.Value);
    }

#endif
}