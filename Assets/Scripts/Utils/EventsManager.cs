using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager 
{
    public static Action OnSuccessFinish;

    public static Action OnTimeUp;

    public static Action OnGameStart;

    public static Action OnOpenCloseInventory;

    public static Action OnRopeClimbingActive;

    public static Action OnRopeClimbingDeactive;

    public static Action OnMissionEndedSuccessfully;

    public static Action OnMissionNotEndedSuccessfully;

    public static Action<Transform> OnSetPlayer;
}
