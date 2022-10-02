using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;

public class EvBatteryChanged : IGameEvent
{
    public int SecondsLeft { get; }

    public EvBatteryChanged(int secondsLeft) {
        SecondsLeft = secondsLeft;
    }
}