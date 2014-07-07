using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Adds a new item to a popup list or menu")]
public class NguiAddPopupOption : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Popup to use")]
    public FsmOwnerDefault NguiPopup;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The new menu option to add")]
    public FsmString NewOption;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiPopup = null;
        NewOption = null;
        everyFrame = false;
    }


    public override void OnEnter()
    {
        DoAddOption();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoAddOption();
    }

    private void DoAddOption()
    {
        // exit if objects are null
        if ((NguiPopup == null) || (NewOption == null))
            return;

        // get the object as a popup
        UIPopupList NWidget = Fsm.GetOwnerDefaultTarget(NguiPopup).GetComponent<UIPopupList>();

        // exit if no widget
        if (NWidget == null)
            return;

        // add the option
        NWidget.items.Add(NewOption.Value);
    }
}