using System;

[Flags]
public enum BonusType
{
    None,
    DestroyWholeRowColumn
}

public enum GameState
{
    None,
    SelectionStarted,
    Animating
}

public static class BonusTypeChecker
{
    // Checking if we have a Bonus Type
    public static bool ContainsDestroyWholeRowColumn(BonusType bt)
    {
        return (bt & BonusType.DestroyWholeRowColumn) == BonusType.DestroyWholeRowColumn;
    }
}
