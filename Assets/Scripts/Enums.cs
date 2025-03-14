public enum Direction
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum CardType
{
    NONE,
    HUMAN,
    KNIGHTSWORD,
    KNIGHTSHIELD,
    MONSTER,
    MINIMONSTER,
    CAULDRON,
}

public enum Effects
{
    NONE,
    MOVE,
    SWAP,
    INVOKE
}

public enum GameState
{
    Playable,
    Busy
}

public enum MainScreenActive
{
    Start,
    LevelSelect,
    Board,
}

public enum SecondScreenActive
{
    None,
    PopUp,
    Pause,
    Win,
    Defeat,
    Options,
    Credits,
} 