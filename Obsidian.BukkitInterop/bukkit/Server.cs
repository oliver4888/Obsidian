using java.awt.image;
using java.io;
using java.lang;
using java.util;

namespace org.bukkit
{
    public interface Server
    {
        public string getVersion();
        public string getName();
        public string getBukkitVersion();
        public Collection<Player> getOnlinePlayers();
        public int getMaxPlayers();
        public int getPort();
        public int getViewDistance();
        public string getIp();
        public string getWorldType();
        public bool getGenerateStructures();
        public bool getAllowEnd();
        public bool getAllowNether();
        public bool hasWhitelist();
        public void setWhitelist(bool value);
        public Set<OfflinePlayer> getWhitelistedPlayers();
        public void reloadWhitelist();
        public int broadcastMessage(string message);
        public string getUpdateFolder();
        public string getUpdateFolderFile();
        public long getConnectionThrottle();
        public int getTicksPerAnimalSpawns();
        public int getTicksPerMonsterSpawns();
        public Player getPlayer(string name);
        public Player getPlayerExact(string name);
        public List<Player> matchPlayer(string name);
        public Player getPlayer(UUID id);
        public PluginManager getPluginManager();
        public BukkitScheduler getScheduler();
        public ServicesManager getServicesManager();
        public List<World> getWorlds();
        public World createWorld(WorldCreator creator);
        public bool unloadWorld(string name, bool save);
        public bool unloadWorld(World world, bool save);
        public World getWorld(string name);
        public World getWorld(UUID uid);
        public MapView getMap(int id);
        public MapView createMap(World world);
        public ItemStack createExplorerMap(World world, Location location, StructureType structureType);
        public ItemStack createExplorerMap(World world, Location location, StructureType structureType, int radius,
            bool findUnexplored);

        public void reload();
        public void reloadData();
        public Logger getLogger();
        public PluginCommand getPluginCommand(string name);
        public void savePlayers();
        public bool dispatchCommand(CommandSender sender, string commandLine);
        public bool addRecipe(Recipe recipe);
        public List<Recipe> getRecipesFor(ItemStack result);
        public Iterator<Recipe> recipeIterator();
        public void clearRecipes();
        public void resetRecipes();
        public bool removeRecipe(NamespacedKey key);
        public Map<string, string[]> getCommandAliases();
        public int getSpawnRadius();
        public void setSpawnRadius(int value);
        public bool getOnlineMode();
        public bool getAllowFlight();
        public bool isHardcore();
        public void shutdown();
        public int broadcast(string message, string permission);
        public OfflinePlayer getOfflinePlayer(string name);
        public OfflinePlayer getOfflinePlayer(UUID id);
        public Set<String> getIPBans();
        public void banIP(string address);
        public void unbanIP(string address);
        public Set<OfflinePlayer> getBannedPlayer();
        public BanList getBanList(BanList.Type type);
        public Set<OfflinePlayer> getOperators();
        public GameMode getDefaultGameMode();
        public void getDefaultGameMode(GameMode mode);
        public ConsoleCommandSender getConsoleSender();
        public File getWorldContainer();
        public OfflinePlayer[] getOfflinePlayers();
        public Messenger getMessenger();
        public HelpMap getHelpMap();
        public Inventory createInventory(InventoryHolder owner, InventoryType type);
        public Inventory createInventory(InventoryHolder owner, InventoryType type, string title);
        public Inventory createInventory(InventoryHolder owner, int size);
        public Inventory createInventory(InventoryHolder owner, int size, string title);
        public Merchant createMerchant(string title);
        public int getMonsterSpawnLimit();
        public int getAnimalSpawnLimit();
        public int getWaterAnimalSpawnLimit();
        public int getAmbientSpawnLimit();
        public bool isPrimaryThread();
        public string getMotd();
        public string getShutdownMessage();
        public WarningState getWarningState();
        public ItemFactory getItemFactory();
        public ScoreboardManager getScoreboardManager();
        public CachedServerIcon getServerIcon();
        public CachedServerIcon loadServerIcon(File file);
        public CachedServerIcon loadServerIcon(BufferedImage image);
        public void setIdleTimeout(int threshold);
        public int getIdleTimeout();
        public ChunkGenerator.ChunkData createChunkData(World world);
        public BossBar createBossBar(string title, BarColor color, BarStyle style, BarFlag flags);

        public KeyedBossBar createBossBar(NamespacedKey key, string title, BarColor color, BarStyle style,
            BarFlag flags);

        public Iterator<KeyedBossbar> getBossBars();
        public KeyedBossbar(NamespacedKey key);
        public bool removeBossBar(NamespacedKey key);
        public Entity getEntity(UUID uuid);
        public Advancement getAdvancement(NamespacedKey key);
        public Iterator<Advancement> advancedmentIterator();
        public BlockData createBlockData(Material material);
        public BlockData createBlockData(Material material, Consumer<BlockData> consumer);
        public BlockData createBlockData(string data);
        public BlockData createBlockData(Material material, string data);
        public Tag<T> getTag(String registry, NamespacedKey tag, Class<T> clazz) where T : Keyed;
        public Iterable<Tag<T>> getTags(string registry, Class<T>) where T : Keyed;
        public LootTable getLootTable(NamespacedKey key);
        public List<Entity> selectEntities(CommandSender sender, String selector);
        
        [Deprecated]
        public UnsafeValues getUnsafe();

    }
    
}