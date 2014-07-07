using HutongGames.PlayMaker;
// using Tooltip = HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory("NGUI")]
[HutongGames.PlayMaker.Tooltip("Sets the color value of multiple widgets to the same color")]
public class NguiSetWidgetColorMulti : FsmStateAction
{
    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("NGUI Widgets to update")]
    public FsmGameObject[] NguiWidgets;

    [RequiredField]
    [HutongGames.PlayMaker.Tooltip("The new color to assign to the widgets")]
    public FsmColor color;

    [HutongGames.PlayMaker.Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiWidgets = null;
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
        if ((NguiWidgets == null) || (NguiWidgets.Length == 0) || (color == null))
            return;

        // handle each widget
        int j = NguiWidgets.Length;
        for (int i = 0; i < j; i++)
        {
            // get the object as a widget
            UIWidget NWidget = NguiWidgets[i].Value.GetComponent<UIWidget>();

            // exit if no widget
            if (NWidget == null)
                continue;

            // set color value
            NWidget.color = color.Value;
        }
    }
}