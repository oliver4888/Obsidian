namespace Obsidian.Blocks
{
    public class BlockState
    {
        public short Id;

        public string UnlocalizedName { get; }

        public Materials Type { get; }

        public bool IsAir => this.Type == Materials.Air || this.Type == Materials.CaveAir || this.Type == Materials.VoidAir;

        internal BlockState(short id)
        {
            this.Id = id;
        }

        internal BlockState(string unlocalizedName, short id, Materials type)
        {
            this.Id = id;
            this.UnlocalizedName = unlocalizedName;
            this.Type = type;
        }

        public override string ToString() => this.Type.ToString();
    }
}
