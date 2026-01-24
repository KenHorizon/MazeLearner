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
    }/// <summary>
     /// Represents the layer type
     /// </summary>
    public enum TiledLayerType
    {
        /// <summary>
        /// Indicates that the layer is an object layer
        /// </summary>
        ObjectLayer,

        /// <summary>
        /// Indicates that the layer is a tile layer
        /// </summary>
        TileLayer,

        /// <summary>
        /// Indicates that the layer is an image layer
        /// </summary>
        ImageLayer
    }

    /// <summary>
    /// Represents property's value data type
    /// </summary>
    public enum TiledPropertyType
    {
        /// <summary>
        /// A string value
        /// </summary>
        String,

        /// <summary>
        /// A bool value
        /// </summary>
        Bool,

        /// <summary>
        /// A color value in hex format
        /// </summary>
        Color,

        /// <summary>
        /// A file path as string
        /// </summary>
        File,

        /// <summary>
        /// A float value
        /// </summary>
        Float,

        /// <summary>
        /// An int value
        /// </summary>
        Int,

        /// <summary>
        /// An object value which is the id of an object in the map
        /// </summary>
        Object
    }
}
