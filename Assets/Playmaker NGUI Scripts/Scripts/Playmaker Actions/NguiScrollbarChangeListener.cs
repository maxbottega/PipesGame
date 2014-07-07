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
[HutongGames.PlayMaker.Tooltip("Listens for the scrollbar to change")]
public class NguiScrollbarChangeListener : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI scrollbar")]
    public FsmOwnerDefault NguiScrollbar;

    [HutongGames.PlayMaker.Tooltip("Event to raise when scrollbar changes")]
    public FsmEvent ChangeEvent;

    [UIHint(UIHint.Variable)]
    [HutongGames.PlayMaker.Tooltip("Variable to store the scrollbar's value")]
    public FsmFloat storeValue;

    private bool HasChanged;

    public override void Reset()
    {
        NguiScrollbar = null;
        storeValue = null;
        HasChanged = false;
        ChangeEvent = null;
    }

    public override void OnEnter()
    {
        // exit if objects are null
        if (NguiScrollbar == null)
            return;

        // get the scrollbar as a GO
        UIScrollBar NScroll = Fsm.GetOwnerDefaultTarget(NguiScrollbar).GetComponent<UIScrollBar>();

        // exit if there is no NGUI scrollbar on it
        if (NScroll == null)
            return;

        // hook the listener
        NScroll.onDragFinished += DragFinished;
    }

    private void DragFinished()
    {
        // unhook listener
        Fsm.GetOwnerDefaultTarget(NguiScrollbar).GetComponent<UIScrollBar>().onDragFinished -= DragFinished;

        // set flag
        HasChanged = true;
    }

    public override void OnUpdate()
    {
        // only process when true
        if (!HasChanged)
            return;

        // scroll bar changed
        DoReadScrollbar();
        Finish();
    }

    private void DoReadScrollbar()
    {
        // exit if objects are null
        if ((NguiScrollbar == null) || (ChangeEvent == null))
            return;

        // get the scrollbar as a GO
        UIScrollBar NScroll = Fsm.GetOwnerDefaultTarget(NguiScrollbar).GetComponent<UIScrollBar>();

        // save the value
        if (storeValue != null)
            storeValue.Value = NScroll.value;

        // call the event
        Fsm.Event(ChangeEvent);
    }
}