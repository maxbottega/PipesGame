using HutongGames.PlayMaker;
using UnityEngine;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[Tooltip("Changes the UIFont on a label")]
public class NguiChangeUifont : FsmStateAction
{
    [RequiredField]
    [Tooltip("NGUI Label to change font")]
    public FsmOwnerDefault NguiLabel;

    [RequiredField]
    [Tooltip("The new font to use")]
    public FsmGameObject NewFont;

    public override void Reset()
    {
        NguiLabel = null;
        NewFont = null;
    }


    public override void OnEnter()
    {
        DoChangeFont();
        Finish();
    }

    private void DoChangeFont()
    {
        // exit if objects are null
        if ((NguiLabel == null) || (NewFont == null))
            return;

        // get the label
        UILabel label = Fsm.GetOwnerDefaultTarget(NguiLabel).GetComponent<UILabel>();

        // exit if no label
        if (label == null)
            return;

        // get the font
        UIFont font = NewFont.Value.GetComponent<UIFont>();
        if (font == null)
        {
            Debug.Log("Font is null");
            return;
        }

        // change the font
        label.bitmapFont = font;
    }
}