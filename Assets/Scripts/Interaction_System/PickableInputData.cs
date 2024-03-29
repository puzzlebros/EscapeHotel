﻿using UnityEngine;

[CreateAssetMenu(fileName = "PickableInputData", menuName = "InteractionSystem/PickableInputData")]
public class PickableInputData : ScriptableObject
{
    private bool pickClicked;
    private bool pickHold;
    private bool pickReleased;

    public bool PickClicked
    {
        get => pickClicked;
        set => pickClicked = value;
    }
    public bool PickHold
    {
        get => pickHold;
        set => pickHold = value;
    }
    public bool PickReleased
    {
        get => pickReleased;
        set => pickReleased = value;
    }
}