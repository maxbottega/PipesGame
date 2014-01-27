using System;
using System.Reflection;
using UnityEngine;

/*
 * *************************************************************************************
 * Parts of this class was modified from Playmaker's NGUI unity package (https://hutonggames.fogbugz.com/?W1111)
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

public class NGuiPlayMakerEvents : MonoBehaviour 
{
    /// <summary>
    /// Returns the FSM Event name as a string for the selected enum value. Example: "NGUI / ON CLICK"
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetFsmEventEnumValue(Enum value)
    {
        string output = null;
        Type type = value.GetType();

        FieldInfo fi = type.GetField(value.ToString());
        PlayMakerNGUI_FsmEvent[] attrs = fi.GetCustomAttributes(typeof(PlayMakerNGUI_FsmEvent), false) as PlayMakerNGUI_FsmEvent[];
        if ((attrs != null) && (attrs.Length > 0))
            output = attrs[0].Value;

        return output;
    }

}

/// <summary>
/// Definition of NGUI FSM events
/// </summary>
public enum NGuiPlayMakerEventsEnum
{
    [PlayMakerNGUI_FsmEvent("NGUI / ON CLICK")]
    OnClickEvent,

    // NEW
    [PlayMakerNGUI_FsmEvent("NGUI / ON DOUBLE CLICK")]
    OnDoubleClickEvent,

    // NEW
    [PlayMakerNGUI_FsmEvent("NGUI / ON INPUT")]
    OnInputEvent,

    // NEW
    [PlayMakerNGUI_FsmEvent("NGUI / ON MOUSEWHEEL SCROLL")]
    OnScrollEvent,

    // NEW
    [PlayMakerNGUI_FsmEvent("NGUI / ON KEY")]
    OnKeyEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON HOVER - HOVERING")]
    OnHoverEnterEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON HOVER - EXIT")]
    OnHoverExitEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON PRESS - PRESSED")]
    OnPressDownEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON PRESS - RELEASED")]
    OnPresReleasedEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON SELECT - PRESSED")]
    OnSelectDownEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON SELECT - RELEASED")]
    OnSelectReleasedEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON DRAG")]
    OnDragEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON DRAG FINISHED")]
    OnDragFinishedEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON DROP")]
    OnDropEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON SUBMIT")]
    OnSubmitEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON SLIDER CHANGE")]
    OnSliderChangeEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON SCROLLBAR CHANGE")]
    OnScrollBarChangeEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON SELECTION CHANGE")]
    OnSelectionChangeEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON TOOLTIP - SHOW")]
    OnTooltipShowEvent,

    [PlayMakerNGUI_FsmEvent("NGUI / ON TOOLTIP - HIDE")]
    OnTooltipHideEvent,
}


public class PlayMakerNGUI_FsmEvent : Attribute
{
    private readonly string _value;

    public PlayMakerNGUI_FsmEvent(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }

}
