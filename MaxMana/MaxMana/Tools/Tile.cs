namespace MaxMana.Tools
{
    public class Tile
    {
        public TileValue Value { get; private set; }
        public FeatureType Feature { get; set; }

        public Tile() : this(TileValue.Void) { }
        public Tile(TileValue value) { this.Value = value; }

        public static implicit operator Tile(int value)
        {
            return new Tile((TileValue)value);
        }
    }
}
