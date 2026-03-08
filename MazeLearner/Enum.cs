namespace MazeLearner
{
    public enum WindowMode
    {
        Windowed,
        Fullscreen,
        Borderless
    }
    public enum EventMapId
    {
        None = 0,
        Warp = 1,
        Npc = 2,
        Item = 3,
        Sign = 4,
        Light = 5,
        Spawn = 6,
        Event = 7,
    }
    public enum EventMapTrigger
    {
        None = 0,
        AutoRun = 1,
        PlayerStep = 2
    }
    public enum GameState
    {
        Title,
        Play,
        Battle,
        Cutscene,
        Pause,
        Dialog,
        Loading,
        GameOver
    }

    public enum MouseButton
    {
        Left,
        Middle,
        Right,
        XButton1,
        XButton2
    }
    public enum Direction
    {
        Up = 3,
        Down = 0,
        Left = 1,
        Right = 2
    }
    public enum MouseClick
    {
        None = -1,
        Left = 0,
        Right = 1,
        Middle = 2,
    }
    /// <summary>
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
