using HutongGames.PlayMaker;
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
[HutongGames.PlayMaker.Tooltip("Sets the value (position) of the scrollbar")]
public class NguiScrollbarSetValue : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI scrollbar")]
    public FsmOwnerDefault NguiScrollbar;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("New scrollbar value")]
    public FsmFloat newValue;

    public override void Reset()
    {
        NguiScrollbar = null;
        newValue = null;
    }

    public override void OnEnter()
    {
        DoSetScrollbar();
        Finish();
    }

    public override void OnUpdate()
    {
    }

    private void DoSetScrollbar()
    {
        // exit if objects are null
        if ((NguiScrollbar == null) || (newValue == null))
            return;

        // get the scrollbar as a GO
        UIScrollBar NScroll = Fsm.GetOwnerDefaultTarget(NguiScrollbar).GetComponent<UIScrollBar>();

        // set the value
        //NScroll.scrollValue = newValue.Value;
        NScroll.value = newValue.Value;
    }
}