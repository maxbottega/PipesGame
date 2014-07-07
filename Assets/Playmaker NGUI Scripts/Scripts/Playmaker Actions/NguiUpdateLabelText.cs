using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Updates the text in an NGUI Label")]
public class NguiUpdateLabelText : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Label to update")]
    public FsmOwnerDefault NguiLabel;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("New text for NGUI Label")]
    public FsmString newValue;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    /// <summary>
    /// The UI Label to update
    /// </summary>
    private UILabel NLabel;

    /// <summary>
    /// The last string used on the label
    /// </summary>
    private string LastString = string.Empty;

    public override void Reset()
    {
        NguiLabel = null;
        newValue = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoUpdateLabel();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoUpdateLabel();
    }

    private void DoUpdateLabel()
    {
        // exit if objects are null
        if ((NguiLabel == null) || (newValue == null))
            return;

        // get the object as a GO
        NLabel = Fsm.GetOwnerDefaultTarget(NguiLabel).GetComponent<UILabel>();

        // exit if there is no NGUI label on it
        if (NLabel == null)
            return;

        // update the label text
        if (!newValue.Value.Equals(LastString))
        {
            NLabel.text = newValue.Value;
            LastString = newValue.Value;
        }
    }
}