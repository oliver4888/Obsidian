using Obsidian.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Obsidian.Items
{
    public class Inventory
    {
        internal static int LastSetId { get; set; } = 1;

        internal byte Id { get; set; }

        internal int ActionsNumber { get; set; }

        public Guid Uuid { get; private set; } = Guid.NewGuid();

        public InventoryType Type { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// The size you want the inventory to be only used when creating a generic inventory
        /// Default size is 45
        /// </summary>
        public int Size { get; }

        public ConcurrentDictionary<int, ItemStack> Items { get; private set; } = new ConcurrentDictionary<int, ItemStack>();

        public List<Player> Viewers { get; private set; } = new List<Player>();

        public Inventory(int size = 9 * 5)
        {
            if (size % 9 != 0)
                throw new InvalidOperationException($"Generic inventory size must be divisible by 9");
            else if (size > 9 * 6)
                throw new InvalidOperationException($"Generic inventory size must not be greater than ({9 * 6})");

            this.Size = size;
        }

        public void AddItems(params ItemStack[] items)
        {
            foreach (var item in items)
            {
                if (item is null)
                    throw new NullReferenceException(nameof(item));

                this.AddItem(item);
            }
        }

        public int AddItem(ItemStack item)
        {
            for (int i = 36; i < 45; i++)
            {
                if (this.Items.TryGetValue(i, out var invItem))
                {
                    if (invItem.Count >= 64)
                        continue;

                    invItem.Count += item.Count;

                    return i;
                }

                if (this.TryAddItem(i, item))
                    return i;
            }

            for (int i = 9; i < 36; i++)
            {
                if (this.Items.TryGetValue(i, out var invItem))
                {
                    if (invItem.Count >= 64)
                        continue;

                    invItem.Count += item.Count;

                    return i;
                }

                if (this.TryAddItem(i, item))
                    return i;
            }

            return 9;
        }

        private bool TryAddItem(int index, ItemStack item) => this.Items.TryAdd(index, item);

        public void SetItem(int slot, ItemStack item)
        {
            if (slot > this.Size - 1 || slot < 0)
                throw new IndexOutOfRangeException($"{slot} > {this.Size - 1}");

            this.Items[slot] = item;
        }

        public ItemStack GetItem(int slot)
        {
            if (slot > this.Size - 1 || slot < 0)
                throw new IndexOutOfRangeException(nameof(slot));

            //Assume there's nothing there so we give back air
            if (!this.Items.ContainsKey(slot))
                return new ItemStack(0, 0);

            return this.Items.GetValueOrDefault(slot);
        }

        public bool RemoveItem(int slot, int amount = 1)
        {
            if (slot > this.Size - 1 || slot < 0)
                throw new IndexOutOfRangeException($"{slot} > {this.Size - 1}");

            if (!this.Items.ContainsKey(slot))
                return false;

            if (amount >= 64 || this.Items[slot].Count - amount <= 0)
                return this.Items.TryRemove(slot, out var _);

            this.Items[slot].Count -= amount;

            return true;
        }
    }

    public enum InventoryType
    {
        Generic,
        Anvil,
        Beacon,
        BlastFurnace,
        BrewingStand,
        Crafting,
        Enchantment,
        Furnace,
        Grindstone,
        Hopper,
        Lectern,
        Loom,
        Merchant,
        ShulkerBox,
        Smoker,
        CartographyTable,
        Stonecutter
    }
}
