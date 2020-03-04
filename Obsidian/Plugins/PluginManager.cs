using Obsidian.Concurrency;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Linq;
using Obsidian.Logging;
using System.Threading.Tasks;
using org.bukkit.plugin.java;
using YamlDotNet.Serialization;
using Exception = System.Exception;

namespace Obsidian.Plugins
{
    public class PluginManager
    {
        public ConcurrentHashSet<Plugin> Plugins { get; private set; }
        private readonly Server Server;
        private string Path => System.IO.Path.Combine(Server.Path, "plugins");

        internal PluginManager(Server server)
        {
            this.Plugins = new ConcurrentHashSet<Plugin>();
            this.Server = server;
        }

        internal async Task LoadPluginsAsync(AsyncLogger logger)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            await LoadJavaPluginsAsync(logger);
            await LoadCSharpPluginsAsync(logger);
        }

        private async Task LoadJavaPluginsAsync(AsyncLogger logger)
        {
            string[] files = Directory.GetFiles(Path, "*.jar");

            foreach (var file in files)
            {
                var pluginInfo = await GetPluginInfo(file);
                
                var urls = new []{new java.net.URL($"file:{file}.jar")};
                var loader = new java.net.URLClassLoader(urls);

                var mainClass = (string)pluginInfo["main"];
                var jarClass = java.lang.Class.forName(mainClass, true, loader);
                var pluginClass = jarClass.asSubclass(typeof(JavaPlugin));
                var plugin = pluginClass.newInstance();
                
                await logger.LogMessageAsync($"Loaded plugin: {pluginInfo["name"]} by {string.Join(", ", pluginInfo["authors"])} (Java/Bukkit Plugin)");
            }
        }

        private async Task<dynamic> GetPluginInfo(string jarFile)
        {
            using var archive = ZipFile.OpenRead(jarFile);
            var pluginInfo = archive.GetEntry("plugin.yml");

            if (pluginInfo == null)
                throw new Exception($"{jarFile} doesn't contain plugin.yml");

            await using var fileStream = pluginInfo.Open();
            using var reader = new StreamReader(fileStream);
            var deserializer = new DeserializerBuilder().Build();
            var yaml = deserializer.Deserialize(reader);

            return yaml;
        }

        private async Task LoadCSharpPluginsAsync(AsyncLogger logger)
        {
            string[] files = Directory.GetFiles(Path, "*.dll");
            // I don't do File IO often, I just know how to do reflection from a dll
            foreach (var file in files) // don't touch pls
            {
                var assembly = Assembly.LoadFile(file);
                var pluginclasses = assembly.GetTypes()
                    .Where(x => typeof(IPluginClass).IsAssignableFrom(x) && x != typeof(IPluginClass));

                foreach (var ptype in pluginclasses)
                {
                    var pluginClass = (IPluginClass) Activator.CreateInstance(ptype);
                    var pluginInfo = await pluginClass.InitializeAsync(Server);
                    var plugin = new Plugin(pluginInfo, pluginClass);

                    Plugins.Add(plugin);
                    await logger.LogMessageAsync($"Loaded plugin: {pluginInfo.Name} by {pluginInfo.Author} (C#/Obsidian Plugin)");
                }
            }
        }
    }
}