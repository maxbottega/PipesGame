using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Sets the color value of a widget")]
public class NguiSetWidgetColor : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Widget to update")]
    public FsmOwnerDefault NguiWidget;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The new color to assign to the widget")]
    public FsmColor color;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiWidget = null;
        color = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoSetWidgetColor();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoSetWidgetColor();
    }

    private void DoSetWidgetColor()
    {
        // exit if objects are null
        if ((NguiWidget == null) || (color == null))
            return;

        // get the object as a widget
        UIWidget NWidget = Fsm.GetOwnerDefaultTarget(NguiWidget).GetComponent<UIWidget>();

        // exit if no widget
        if (NWidget == null)
            return;

        // set color value
        NWidget.color = color.Value;
    }
}