using ikvm.extensions;
using java.lang;
using sun.tools.tree;

namespace org.bukkit.plugin.java
{
    public class JavaPlugin : PluginBase
    {
        public JavaPlugin()
        {
            ClassLoader classLoader = this.getClass().getClassLoader();
            
            //if (!(classLoader is PluginClassLoader))
            //    throw new Illegal
        }

    }
}