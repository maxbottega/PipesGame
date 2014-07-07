using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Saves the text in an NGUI Label to a variable")]
public class NguiGetInputText : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Label to read")]
    public FsmOwnerDefault NguiLabel;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("Variable to store the label's text")]
    public FsmString storeValue;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiLabel = null;
        storeValue = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoReadLabel();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoReadLabel();
    }

    private void DoReadLabel()
    {
        // exit if objects are null
        if ((NguiLabel == null) || (storeValue == null))
            return;

        // get the object as a GO
        UILabel NLabel = Fsm.GetOwnerDefaultTarget(NguiLabel).GetComponent<UILabel>();

        // exit if there is no NGUI label on it
        if (NLabel == null)
            return;

        // save the label text
        storeValue.Value = NLabel.text;
    }
}