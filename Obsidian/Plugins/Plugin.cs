namespace Obsidian.Plugins
{
    public struct Plugin
    {
        public PluginInfo Info { get; }
        public IPluginClass Class { get; }

        public Plugin(PluginInfo info, IPluginClass pclass)
        {
            this.Info = info;
            this.Class = pclass;
        }
    }
}