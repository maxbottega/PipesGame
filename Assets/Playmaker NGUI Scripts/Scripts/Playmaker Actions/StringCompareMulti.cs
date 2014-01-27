using HutongGames.PlayMaker;

/*
 * *************************************************************************************
 * Created by: Rocket Games Mobile  (http://www.rocketgamesmobile.com), 2013-2014
 * For use in Unity 3.5, Unity 4.0+
 * *************************************************************************************
*/

[ActionCategory(ActionCategory.String)]
[Tooltip("Compares a string value to multiple options")]
public class StringCompareMulti : FsmStateAction
{
    [RequiredField]
    [Tooltip("String to check")]
    public FsmString StringVariable;

    [CompoundArray("Strings", "CompareTo", "CompareEvent")]

    [Tooltip("String to compare to")]
    [UIHint(UIHint.Variable)]
    public FsmString[] CompareTos;

    [Tooltip("Event to raise on match")]
    public FsmEvent[] CompareEvents;

    [Tooltip("Event to raise if no matches are found")]
    public FsmEvent NoMatchEvent;

    [Tooltip("When true, compare strings on case")]
    public FsmBool CaseSensitive;

    [Tooltip("When true, runs on every frame")]
    public bool everyFrame;

    public override void Reset()
    {
        StringVariable = null;
        CompareTos = null;
        CompareEvents = null;
        NoMatchEvent = null;
        everyFrame = false;
    }

    public override void OnEnter()
    {
        DoStringCompare();

        if (!everyFrame)
            Finish();
    }

    public override void OnUpdate()
    {
        DoStringCompare();
    }

    private void DoStringCompare()
    {
        // exit if objects are null
        if ((StringVariable == null) || (CompareTos == null) || (CompareEvents == null))
            return;

        // get our case sensitivity
        bool UseCase = ((CaseSensitive != null) && CaseSensitive.Value);

        // get our base string, changing case as needed
        string BaseString = UseCase ? StringVariable.Value : StringVariable.Value.ToLower();

        // loop until we find a match
        int j = CompareTos.Length;
        for (int i = 0; i < j; i++)
        {
            // get compare string
            string tempStr = UseCase ? CompareTos[i].Value : CompareTos[i].Value.ToLower();

            if (BaseString.Equals(tempStr, System.StringComparison.CurrentCulture))
            {
                // fire the event
                Fsm.Event(CompareEvents[i]);

                // exit
                return;
            }
        }

        // nothing found, so fire no math event
        if (NoMatchEvent != null)
            Fsm.Event(NoMatchEvent);
    }
}