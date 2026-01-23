namespace MazeLearner
{
    public enum FileType : byte
    {
        None,
        Map,
        World,
        Player
    }

    public enum WindowMode
    {
        Windowed = 0,
        Borderless = 1,
        Fullscreen = 2
    }
    public enum GameState
    {
        Title,
        Play,
        Battle,
        Pause,
        Dialog,
        GameOver
    }
    public enum SettingTypes
    {
        Default,
        Graphics,
        Gameplay,
        Audio
    }

    public enum ScreenState
    {
        Active,
        Hidden
    }
    public enum EntityClass
    {
        Object,
        Player,
        Enemy,
        Boss,
        NPC
    }

    public enum MouseButton
    {
        Left,
        Middle,
        Right,
        XButton1,
        XButton2
    }
    public enum Facing
    {
        Up = 3,
        Down = 0,
        Left = 1,
        Right = 2
    }
    public enum CursorState
    {
        Default = 0,
        HoveredButton = 1,
    }
    public enum TileState
    {
        Passable = 0,
        NotPassable = 1
    }
    public enum MouseClick
    {
        None = -1,
        Left = 0,
        Right = 1,
        Middle = 2,
    }
}
