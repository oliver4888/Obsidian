using java.awt.image;
using java.io;
using java.lang;
using java.util;
using org.bukkit;
// ReSharper disable CheckNamespace

namespace org.obsidian
{
    public class ObsidianServer : Server
    {
        public string getVersion()
        {
            throw new System.NotImplementedException();
        }

        public string getName()
        {
            throw new System.NotImplementedException();
        }

        public string getBukkitVersion()
        {
            throw new System.NotImplementedException();
        }

        public Collection getOnlinePlayers()
        {
            throw new System.NotImplementedException();
        }

        public int getMaxPlayers()
        {
            throw new System.NotImplementedException();
        }

        public int getPort()
        {
            throw new System.NotImplementedException();
        }

        public int getViewDistance()
        {
            throw new System.NotImplementedException();
        }

        public string getIp()
        {
            throw new System.NotImplementedException();
        }

        public string getWorldType()
        {
            throw new System.NotImplementedException();
        }

        public bool getGenerateStructures()
        {
            throw new System.NotImplementedException();
        }

        public bool getAllowEnd()
        {
            throw new System.NotImplementedException();
        }

        public bool getAllowNether()
        {
            throw new System.NotImplementedException();
        }

        public bool hasWhitelist()
        {
            throw new System.NotImplementedException();
        }

        public void setWhitelist(bool value)
        {
            throw new System.NotImplementedException();
        }

        public Set getWhitelistedPlayers()
        {
            throw new System.NotImplementedException();
        }

        public void reloadWhitelist()
        {
            throw new System.NotImplementedException();
        }

        public int broadcastMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        public string getUpdateFolder()
        {
            throw new System.NotImplementedException();
        }

        public string getUpdateFolderFile()
        {
            throw new System.NotImplementedException();
        }

        public long getConnectionThrottle()
        {
            throw new System.NotImplementedException();
        }

        public int getTicksPerAnimalSpawns()
        {
            throw new System.NotImplementedException();
        }

        public int getTicksPerMonsterSpawns()
        {
            throw new System.NotImplementedException();
        }

        public Player getPlayer(string name)
        {
            throw new System.NotImplementedException();
        }

        public Player getPlayerExact(string name)
        {
            throw new System.NotImplementedException();
        }

        public List matchPlayer(string name)
        {
            throw new System.NotImplementedException();
        }

        public Player getPlayer(UUID id)
        {
            throw new System.NotImplementedException();
        }

        public PluginManager getPluginManager()
        {
            throw new System.NotImplementedException();
        }

        public BukkitScheduler getScheduler()
        {
            throw new System.NotImplementedException();
        }

        public ServicesManager getServicesManager()
        {
            throw new System.NotImplementedException();
        }

        public List getWorlds()
        {
            throw new System.NotImplementedException();
        }

        public World createWorld(WorldCreator creator)
        {
            throw new System.NotImplementedException();
        }

        public bool unloadWorld(string name, bool save)
        {
            throw new System.NotImplementedException();
        }

        public bool unloadWorld(World world, bool save)
        {
            throw new System.NotImplementedException();
        }

        public World getWorld(string name)
        {
            throw new System.NotImplementedException();
        }

        public World getWorld(UUID uid)
        {
            throw new System.NotImplementedException();
        }

        public MapView getMap(int id)
        {
            throw new System.NotImplementedException();
        }

        public MapView createMap(World world)
        {
            throw new System.NotImplementedException();
        }

        public ItemStack createExplorerMap(World world, Location location, StructureType structureType)
        {
            throw new System.NotImplementedException();
        }

        public ItemStack createExplorerMap(World world, Location location, StructureType structureType, int radius,
            bool findUnexplored)
        {
            throw new System.NotImplementedException();
        }

        public void reload()
        {
            throw new System.NotImplementedException();
        }

        public void reloadData()
        {
            throw new System.NotImplementedException();
        }

        public Logger getLogger()
        {
            throw new System.NotImplementedException();
        }

        public PluginCommand getPluginCommand(string name)
        {
            throw new System.NotImplementedException();
        }

        public void savePlayers()
        {
            throw new System.NotImplementedException();
        }

        public bool dispatchCommand(CommandSender sender, string commandLine)
        {
            throw new System.NotImplementedException();
        }

        public bool addRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public List getRecipesFor(ItemStack result)
        {
            throw new System.NotImplementedException();
        }

        public Iterator recipeIterator()
        {
            throw new System.NotImplementedException();
        }

        public void clearRecipes()
        {
            throw new System.NotImplementedException();
        }

        public void resetRecipes()
        {
            throw new System.NotImplementedException();
        }

        public bool removeRecipe(NamespacedKey key)
        {
            throw new System.NotImplementedException();
        }

        public Map getCommandAliases()
        {
            throw new System.NotImplementedException();
        }

        public int getSpawnRadius()
        {
            throw new System.NotImplementedException();
        }

        public void setSpawnRadius(int value)
        {
            throw new System.NotImplementedException();
        }

        public bool getOnlineMode()
        {
            throw new System.NotImplementedException();
        }

        public bool getAllowFlight()
        {
            throw new System.NotImplementedException();
        }

        public bool isHardcore()
        {
            throw new System.NotImplementedException();
        }

        public void shutdown()
        {
            throw new System.NotImplementedException();
        }

        public int broadcast(string message, string permission)
        {
            throw new System.NotImplementedException();
        }

        public OfflinePlayer getOfflinePlayer(string name)
        {
            throw new System.NotImplementedException();
        }

        public OfflinePlayer getOfflinePlayer(UUID id)
        {
            throw new System.NotImplementedException();
        }

        public Set getIPBans()
        {
            throw new System.NotImplementedException();
        }

        public void banIP(string address)
        {
            throw new System.NotImplementedException();
        }

        public void unbanIP(string address)
        {
            throw new System.NotImplementedException();
        }

        public Set getBannedPlayer()
        {
            throw new System.NotImplementedException();
        }

        public BanList getBanList(Type type)
        {
            throw new System.NotImplementedException();
        }

        public Set getOperators()
        {
            throw new System.NotImplementedException();
        }

        public GameMode getDefaultGameMode()
        {
            throw new System.NotImplementedException();
        }

        public void getDefaultGameMode(GameMode mode)
        {
            throw new System.NotImplementedException();
        }

        public ConsoleCommandSender getConsoleSender()
        {
            throw new System.NotImplementedException();
        }

        public File getWorldContainer()
        {
            throw new System.NotImplementedException();
        }

        public OfflinePlayer[] getOfflinePlayers()
        {
            throw new System.NotImplementedException();
        }

        public Messenger getMessenger()
        {
            throw new System.NotImplementedException();
        }

        public HelpMap getHelpMap()
        {
            throw new System.NotImplementedException();
        }

        public Inventory createInventory(InventoryHolder owner, InventoryType type)
        {
            throw new System.NotImplementedException();
        }

        public Inventory createInventory(InventoryHolder owner, InventoryType type, string title)
        {
            throw new System.NotImplementedException();
        }

        public Inventory createInventory(InventoryHolder owner, int size)
        {
            throw new System.NotImplementedException();
        }

        public Inventory createInventory(InventoryHolder owner, int size, string title)
        {
            throw new System.NotImplementedException();
        }

        public Merchant createMerchant(string title)
        {
            throw new System.NotImplementedException();
        }

        public int getMonsterSpawnLimit()
        {
            throw new System.NotImplementedException();
        }

        public int getAnimalSpawnLimit()
        {
            throw new System.NotImplementedException();
        }

        public int getWaterAnimalSpawnLimit()
        {
            throw new System.NotImplementedException();
        }

        public int getAmbientSpawnLimit()
        {
            throw new System.NotImplementedException();
        }

        public bool isPrimaryThread()
        {
            throw new System.NotImplementedException();
        }

        public string getMotd()
        {
            throw new System.NotImplementedException();
        }

        public string getShutdownMessage()
        {
            throw new System.NotImplementedException();
        }

        public WarningState getWarningState()
        {
            throw new System.NotImplementedException();
        }

        public ItemFactory getItemFactory()
        {
            throw new System.NotImplementedException();
        }

        public ScoreboardManager getScoreboardManager()
        {
            throw new System.NotImplementedException();
        }

        public CachedServerIcon getServerIcon()
        {
            throw new System.NotImplementedException();
        }

        public CachedServerIcon loadServerIcon(File file)
        {
            throw new System.NotImplementedException();
        }

        public CachedServerIcon loadServerIcon(BufferedImage image)
        {
            throw new System.NotImplementedException();
        }

        public void setIdleTimeout(int threshold)
        {
            throw new System.NotImplementedException();
        }

        public int getIdleTimeout()
        {
            throw new System.NotImplementedException();
        }

        public ChunkData createChunkData(World world)
        {
            throw new System.NotImplementedException();
        }

        public BossBar createBossBar(string title, BarColor color, BarStyle style, BarFlag flags)
        {
            throw new System.NotImplementedException();
        }

        public KeyedBossBar createBossBar(NamespacedKey key, string title, BarColor color, BarStyle style, BarFlag flags)
        {
            throw new System.NotImplementedException();
        }

        public Iterator getBossBars()
        {
            throw new System.NotImplementedException();
        }

        public bool removeBossBar(NamespacedKey key)
        {
            throw new System.NotImplementedException();
        }

        public Entity getEntity(UUID uuid)
        {
            throw new System.NotImplementedException();
        }

        public Advancement getAdvancement(NamespacedKey key)
        {
            throw new System.NotImplementedException();
        }

        public Iterator advancedmentIterator()
        {
            throw new System.NotImplementedException();
        }

        public BlockData createBlockData(Material material)
        {
            throw new System.NotImplementedException();
        }

        public BlockData createBlockData(Material material, Consumer<BlockData> consumer)
        {
            throw new System.NotImplementedException();
        }

        public BlockData createBlockData(string data)
        {
            throw new System.NotImplementedException();
        }

        public BlockData createBlockData(Material material, string data)
        {
            throw new System.NotImplementedException();
        }

        public Tag<T> getTag(String registry, NamespacedKey tag, Class clazz)
        {
            throw new System.NotImplementedException();
        }

        public Iterable getTags(string registry, Class missing_name)
        {
            throw new System.NotImplementedException();
        }

        public LootTable getLootTable(NamespacedKey key)
        {
            throw new System.NotImplementedException();
        }

        public List selectEntities(CommandSender sender, String selector)
        {
            throw new System.NotImplementedException();
        }

        public UnsafeValues getUnsafe()
        {
            throw new System.NotImplementedException();
        }
    }
}