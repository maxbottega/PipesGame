using HutongGames.PlayMaker;
using UnityEngine;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Sets an NGUI Buttons's Enabled property")]
public class NguiSetButtonEnabled : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI object to update")]
    public FsmOwnerDefault NguiButtonToUpdate;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The new value to assign the Enabled property")]
    public FsmBool enabled;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiButtonToUpdate = null;
        enabled = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoChangeEnabledValue();

        if (! everyFrame)
        Finish();
    }

    public override void OnUpdate()
    {
        DoChangeEnabledValue();
    }

    private void DoChangeEnabledValue()
    {
        // exit if objects are null
        if ((NguiButtonToUpdate == null) || (enabled == null))
            return;

        // get the object as a GO
        GameObject NguiGO = Fsm.GetOwnerDefaultTarget(NguiButtonToUpdate);

        // set enabled property, if image button
        UIImageButton NguiImgBtn = NguiGO.GetComponent<UIImageButton>();
        if (NguiImgBtn != null)
        {
            NguiImgBtn.isEnabled = enabled.Value;
            return;
        }

        // set enabled for regular button
        UIButton NguiBtn = NguiGO.GetComponent<UIButton>();
        if (NguiBtn != null)
            NguiBtn.isEnabled = enabled.Value;
    }
}