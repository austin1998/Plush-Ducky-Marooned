using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingOptions
{
    public enum DifficultyLevel { Easy, Normal, Hard }

    public static DifficultyLevel difficultyLevel = DifficultyLevel.Normal;

    public static float difficultyModifier = 1f;
}
