using HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * 
 * Updated for NGUI v3
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[Tooltip("Saves the value of a scrollbar to a variable")]
public class NguiScrollbarGetValue : FsmStateAction
{
    [RequiredField]
    [Tooltip("NGUI scrollbar")]
    public FsmOwnerDefault NguiScrollbar;

    [RequiredField]
    [UIHint(UIHint.Variable)]
    [Tooltip("Variable to store the scrollbar's value")]
    public FsmFloat storeValue;

    [Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiScrollbar = null;
        storeValue = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoReadScrollbar();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoReadScrollbar();
    }

    private void DoReadScrollbar()
    {
        // exit if objects are null
        if ((NguiScrollbar == null) || (storeValue == null))
            return;

        // get the scrollbar as a GO
        UIScrollBar NScroll = Fsm.GetOwnerDefaultTarget(NguiScrollbar).GetComponent<UIScrollBar>();

        // exit if there is no NGUI scrollbar on it
        if (NScroll == null)
            return;

        // save the value
        //storeValue.Value = NScroll.scrollValue;
        storeValue.Value = NScroll.value;
    }
}