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
[Tooltip("Sets the value of an NGUI progressbar or Slider")]
public class NguiSetSliderValue : FsmStateAction
{
    [RequiredField]
    [Tooltip("NGUI slider or progressbar to update")]
    public FsmOwnerDefault NguiSlider;

    [RequiredField]
    [Tooltip("The new value to assign to the slider progressbar")]
    public FsmFloat value;

    [UIHint(UIHint.Variable)]
    [Tooltip("Save the value to a variable")]
    public FsmFloat saveValue;

    [Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        NguiSlider = null;
        value = null;
        saveValue = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoSetSliderValue();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoSetSliderValue();
    }

    private void DoSetSliderValue()
    {
        // exit if objects are null
        if ((NguiSlider == null) || (value == null))
            return;

        // get the object as a progressbar
        UISlider NguiSlide = Fsm.GetOwnerDefaultTarget(NguiSlider).GetComponent<UISlider>();

        // exit if no slider
        if (NguiSlide == null)
            return;

        // set value
        //NguiSlide.sliderValue = value.Value;
        NguiSlide.value = value.Value;

        // save value
        if (saveValue != null)
            saveValue.Value = value.Value;
    }
}