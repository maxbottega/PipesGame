using HutongGames.PlayMaker;
using UnityEngine;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * 
 * Updated for NGUI v3
 * *************************************************************************************
*/

/// <summary>
/// Place on the NGUI GameObject you want to monitor events from (for example, a UIButton)
/// </summary>
public class NguiEventForwarder : MonoBehaviour 
{
    #region Variables

    /// <summary>
    /// When true, event messages are logged to the console
    /// </summary>
    public bool debug;

    /// <summary>
    /// The Playmaker FSM to forward event messages to
    /// </summary>
    public PlayMakerFSM targetFSM;

    // Variables
    #endregion

    #region OnEnable

    void OnEnable()
    {
        // use the first FSM on this GameObject, if one was not specified
        if (targetFSM == null)
            targetFSM = GetComponent<PlayMakerFSM>();

        // if we couldn't find the FSM, then disable this script
        if (targetFSM == null)
        {
            enabled = false;
            Debug.LogWarning("No FSM Target assigned. NGUIEventForwarder is now disabled");
        }
    }

    // OnEnable
    #endregion

    #region ForwardNGUIEvent

    /// <summary>
    /// Forwards the NGUI events to the selected Playmaker FSM
    /// </summary>
    /// <param name="nguiEvent"></param>
    void ForwardNGUIEvent(NGuiPlayMakerEventsEnum nguiEvent)
    {
        // exit if we are disabled or no FSM found
        if ((!enabled) || (targetFSM == null))
            return;

        // log the message
        if (debug)
            Debug.Log(string.Format("NGUI Event Forwarder - Sending {0} to {1} > {2}", nguiEvent.ToString(), targetFSM.gameObject.name, targetFSM.FsmName));

        // forward the event
        targetFSM.SendEvent(NGuiPlayMakerEvents.GetFsmEventEnumValue(nguiEvent));
    }

    // ForwardNGUIEvent
    #endregion

    #region NGUI_Events

    /// <summary>
    /// Sent to a mouse button or touch event gets released on the same collider as OnPress. UICamera.currentTouchID tells you which button was clicked.
    /// </summary>
    void OnClick()
    {
        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnClickEvent);
    }

    /// <summary>
    /// Sent when the click happens twice within a fourth of a second. UICamera.currentTouchID tells you which button was clicked.
    /// </summary>
    void OnDoubleClick()
    {
        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnDoubleClickEvent);
    }

    /// <summary>
    /// Sent out when the mouse hovers over the collider or moves away from it. Not sent on touch-based devices.
    /// </summary>
    /// <param name="isOver"></param>
    void OnHover(bool isOver)
    {
        // forward the event to the FSM
        ForwardNGUIEvent(
            isOver ?
                NGuiPlayMakerEventsEnum.OnHoverEnterEvent :
                NGuiPlayMakerEventsEnum.OnHoverExitEvent);
    }

    /// <summary>
    /// Sent when a mouse button (or touch event) gets pressed over the collider (with ‘true’) and when it gets released (with ‘false’, sent to the same collider even if it’s released elsewhere).
    /// </summary>
    /// <param name="isDown"></param>
    void OnPress(bool isDown)
    {
        // forward the event to the FSM
        ForwardNGUIEvent(
            isDown ? 
                NGuiPlayMakerEventsEnum.OnPressDownEvent :
                NGuiPlayMakerEventsEnum.OnPresReleasedEvent);
    }

    /// <summary>
    /// Same as OnClick, but once a collider is selected it will not receive any further OnSelect events until you select some other collider.
    /// </summary>
    /// <param name="selected"></param>
    void OnSelect(bool selected)
    {
        // forward the event to the FSM
        ForwardNGUIEvent(
            selected ?
                NGuiPlayMakerEventsEnum.OnSelectDownEvent :
                NGuiPlayMakerEventsEnum.OnSelectReleasedEvent);
    }

    /// <summary>
    /// Sent when the mouse or touch is moving in between of OnPress(true) and OnPress(false).
    /// </summary>
    /// <param name="delta"></param>
    void OnDrag(Vector2 delta)
    {
        // set event data
        Fsm.EventData.Vector3Data = new Vector3(delta.x, delta.y);

        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnDragEvent);
    }

    /// <summary>
    /// Sent out to the collider under the mouse or touch when OnPress(false) is called over a different collider than triggered the OnPress(true) event. The passed parameter is the game object of the collider that received the OnPress(true) event.
    /// </summary>
    /// <param name="drag"></param>
    void OnDrop(GameObject drag)
    {
        // set event data
        Fsm.EventData.GameObjectData = drag;

        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnDropEvent);
    }

    /// <summary>
    /// Sent after the mouse hovers over a collider without moving for longer than tooltipDelay, and when the tooltip should be hidden. Not sent on touch-based devices.
    /// </summary>
    /// <param name="show"></param>
    void OnTooltip(bool show)
    {
        // forward the event to the FSM
        ForwardNGUIEvent(
            show ?
                NGuiPlayMakerEventsEnum.OnTooltipShowEvent :
                NGuiPlayMakerEventsEnum.OnTooltipHideEvent);
    }

    /// <summary>
    /// Sent to the same collider that received OnSelect(true) message after typing something. You likely won’t need this, but it’s used by UIInput
    /// </summary>
    /// <param name="text"></param>
    void OnInput (string text)
    {
        // set event data
        Fsm.EventData.StringData = text;

        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnInputEvent);
    }

    /// <summary>
    /// Sent out when the mouse scroll wheel is moved.
    /// </summary>
    /// <param name="delta"></param>
    void OnScroll(float delta)
    {
        // set event data
        Fsm.EventData.FloatData = delta;

        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnScrollEvent);
    }

    /// <summary>
    /// sent when keyboard or controller input is used
    /// </summary>
    /// <param name="key"></param>
    void OnKey(KeyCode key)
    {
        // set event data
        Fsm.EventData.StringData = key.ToString();

        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnKeyEvent);
    }

    #region Events_Which_Use_Notify

    public void OnSubmitChange()
    {
        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnSubmitEvent);
    }

    public void OnSliderChange()
    {
        // set event data
        UISlider slider = gameObject.GetComponent<UISlider>();
        if (slider != null)
            Fsm.EventData.FloatData = slider.value;

        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnSliderChangeEvent);
    }

    public void OnSelectionChange()
    {
        // set event data
        //Fsm.EventData.StringData = item;

        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnSelectionChangeEvent);
    }

    void OnScrollBarChange()
    {
        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnScrollBarChangeEvent);
    }

    void OnDragFinished()
    {
        // forward the event to the FSM
        ForwardNGUIEvent(NGuiPlayMakerEventsEnum.OnDragFinishedEvent);
    }


    // Events_Which_Use_Notify
    #endregion

    // NGUI_Events
    #endregion
}
