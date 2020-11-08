﻿// This would be saved in a file called [playeruuid].dat which holds a bunch of NBT data.
// https://wiki.vg/Map_Format
using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.Boss;
using Obsidian.Chat;
using Obsidian.Concurrency;
using Obsidian.Items;
using Obsidian.Net;
using Obsidian.Net.Packets.Play.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obsidian.Entities
{
    public class Player : Living, IPlayer
    {
        internal readonly Client client;
        
        public IServer Server => client.Server;
        public bool IsOperator => Server.Operators.IsOperator(this);

        public string Username { get; }

        /// <summary>
        /// The players inventory
        /// </summary>
        public Inventory Inventory { get; private set; } = new Inventory();
        public Inventory OpenedInventory { get; set; }

        public Guid Uuid { get; set; }

        public PlayerBitMask PlayerBitMask { get; set; }
        public Gamemode Gamemode { get; set; }
        public Hand MainHand { get; set; } = Hand.MainHand;

        public bool Sleeping { get; set; }
        public bool Sneaking { get; set; }
        public bool Sprinting { get; set; }
        public bool FlyingWithElytra { get; set; }
        public bool InHorseInventory { get; set; }

        public bool IsDragging { get; set; }

        public short AttackTime { get; set; }
        public short DeathTime { get; set; }
        public short HurtTime { get; set; }
        public short SleepTimer { get; set; }
        public short CurrentSlot { get; set; } = 36;

        public int Ping => this.client.ping;
        public int Dimension { get; set; }
        public int FoodLevel { get; set; }
        public int FoodTickTimer { get; set; }
        public int XpLevel { get; set; }
        public int XpTotal { get; set; }

        public double HeadY { get; private set; }

        public float AdditionalHearts { get; set; } = 0;
        public float FallDistance { get; set; }
        public float FoodExhastionLevel { get; set; } // not a type, it's in docs like this
        public float FoodSaturationLevel { get; set; }
        public float XpP { get; set; } = 0; // idfk, xp points?

        public Entity LeftShoulder { get; set; }
        public Entity RightShoulder { get; set; }

        /* Missing for now:
            NbtCompound(inventory)
            NbtList(Motion)
            NbtList(Pos)
            NbtList(Rotation)
        */

        // Properties set by Obsidian (unofficial)
        // Not sure whether these should be saved to the NBT file.
        // These could be saved under nbt tags prefixed with "obsidian_"
        // As minecraft might just ignore them.
        public ConcurrentHashSet<string> PlayerPermissions { get; } = new ConcurrentHashSet<string>();
        public ICollection<string> Permissions => PlayerPermissions;

        internal Player(Guid uuid, string username, Client client)
        {
            this.Uuid = uuid;
            this.Username = username;
            this.client = client;
            this.EntityId = client.id;
        }

        internal override async Task UpdateAsync(Server server, Position position, bool onGround)
        {
            await base.UpdateAsync(server, position, onGround);

            this.HeadY = position.Y + 1.62;

            foreach (var entity in this.World.GetEntitiesNear(this.Location, 1))
            {
                if (entity is ItemEntity item)
                {
                    if (!item.CanPickup)
                        continue;

                    await server.BroadcastPacketWithoutQueueAsync(new CollectItem
                    {
                        CollectedEntityId = item.EntityId,
                        CollectorEntityId = this.EntityId,
                        PickupItemCount = item.Count
                    });

                    var slot = this.Inventory.AddItem(new ItemStack(item.Id, item.Count)
                    {
                        Present = true,
                        Nbt = item.Nbt
                    });

                    await this.client.SendPacketAsync(new SetSlot
                    {
                        Slot = (short)slot,

                        WindowId = 0,

                        SlotData = this.Inventory.GetItem(slot)
                    });

                    await item.RemoveAsync();
                }
            }
        }

        internal override async Task UpdateAsync(Server server, Position position, Angle yaw, Angle pitch, bool onGround)
        {
            await base.UpdateAsync(server, position, yaw, pitch, onGround);

            this.HeadY = position.Y + 1.62;

            foreach (var entity in this.World.GetEntitiesNear(this.Location, .8))
            {
                if (entity is ItemEntity item)
                {
                    if (!item.CanPickup)
                        continue;

                    await server.BroadcastPacketWithoutQueueAsync(new CollectItem
                    {
                        CollectedEntityId = item.EntityId,
                        CollectorEntityId = this.EntityId,
                        PickupItemCount = item.Count
                    });
                    var slot = this.Inventory.AddItem(new ItemStack(item.Id, item.Count)
                    {
                        Present = true,
                        Nbt = item.Nbt
                    });

                    await this.client.SendPacketAsync(new SetSlot
                    {
                        Slot = (short)slot,

                        WindowId = 0,

                        SlotData = this.Inventory.GetItem(slot)
                    });

                    await item.RemoveAsync();
                }
            }
        }

        internal override async Task UpdateAsync(Server server, Angle yaw, Angle pitch, bool onGround)
        {
            await base.UpdateAsync(server, yaw, pitch, onGround);

            foreach (var entity in this.World.GetEntitiesNear(this.Location, 2))
            {
                if (entity is ItemEntity item)
                {
                    await server.BroadcastPacketWithoutQueueAsync(new CollectItem
                    {
                        CollectedEntityId = item.EntityId,
                        CollectorEntityId = this.EntityId,
                        PickupItemCount = item.Count
                    });

                    var slot = this.Inventory.AddItem(new ItemStack(item.Id, item.Count)
                    {
                        Present = true,
                        Nbt = item.Nbt
                    });

                    await this.client.SendPacketAsync(new SetSlot
                    {
                        Slot = (short)slot,

                        WindowId = 0,

                        SlotData = this.Inventory.GetItem(slot)
                    });
                    _ = Task.Run(() => item.RemoveAsync());
                }
            }
        }

        public ItemStack GetHeldItem() => this.Inventory.GetItem(this.CurrentSlot);

        public void LoadPerms(List<string> permissions)
        {
            foreach (var perm in permissions)
            {
                PlayerPermissions.Add(perm);
            }
        }

        public async Task TeleportAsync(Position pos)
        {
            var tid = Globals.Random.Next(0, 999);
            await this.client.QueuePacketAsync(new ClientPlayerPositionLook
            {
                Position = pos,
                Flags = PositionFlags.NONE,
                TeleportId = tid
            });
            this.TeleportId = tid;
        }

        public async Task TeleportAsync(IPlayer to) => await TeleportAsync(to as Player);
        public async Task TeleportAsync(Player to)
        {
            var tid = Globals.Random.Next(0, 999);
            await this.client.QueuePacketAsync(new ClientPlayerPositionLook
            {
                Position = to.Location,
                Flags = PositionFlags.NONE,
                TeleportId = tid
            });
            this.TeleportId = tid;
        }

        public Task SendMessageAsync(string message, sbyte position = 0, Guid? sender = null) => client.QueuePacketAsync(new ChatMessagePacket(ChatMessage.Simple(message), position, sender ?? Guid.Empty));

        public Task SendMessageAsync(IChatMessage message, Guid? sender = null)
        {
            var chatMessage = message as ChatMessage;
            if (chatMessage is null)
                return Task.FromException(new Exception("Message was of the wrong type or null. Expected instance supplied by IChatMessage.CreateNew."));
            return SendMessageAsync(chatMessage, sender);
        }

        public Task SendMessageAsync(ChatMessage message, Guid? sender = null) => client.QueuePacketAsync(new ChatMessagePacket(message, 0, sender ?? Guid.Empty));

        public Task SendSoundAsync(int soundId, SoundPosition position, SoundCategory category = SoundCategory.Master, float pitch = 1f, float volume = 1f) => client.QueuePacketAsync(new SoundEffect(soundId, position, category, pitch, volume));

        public Task SendNamedSoundAsync(string name, SoundPosition position, SoundCategory category = SoundCategory.Master, float pitch = 1f, float volume = 1f) => client.QueuePacketAsync(new NamedSoundEffect(name, position, category, pitch, volume));

        public Task SendBossBarAsync(Guid uuid, BossBarAction action) => client.QueuePacketAsync(new BossBar(uuid, action));

        public Task KickAsync(string reason) => this.client.DisconnectAsync(ChatMessage.Simple(reason));
        public Task KickAsync(IChatMessage reason)
        {
            var chatMessage = reason as ChatMessage;
            if (chatMessage is null)
                return Task.FromException(new Exception("Message was of the wrong type or null. Expected instance supplied by IChatMessage.CreateNew."));
            return KickAsync(chatMessage);
        }
        public Task KickAsync(ChatMessage reason) => this.client.DisconnectAsync(reason);

        public override async Task WriteAsync(MinecraftStream stream)
        {
            await base.WriteAsync(stream);

            await stream.WriteEntityMetdata(14, EntityMetadataType.Float, this.AdditionalHearts);

            await stream.WriteEntityMetdata(15, EntityMetadataType.VarInt, this.XpP);

            await stream.WriteEntityMetdata(16, EntityMetadataType.Byte, (int)this.PlayerBitMask);

            await stream.WriteEntityMetdata(17, EntityMetadataType.Byte, (byte)this.MainHand);

            if (this.LeftShoulder != null)
                await stream.WriteEntityMetdata(18, EntityMetadataType.Nbt, this.LeftShoulder);

            if (this.RightShoulder != null)
                await stream.WriteEntityMetdata(19, EntityMetadataType.Nbt, this.RightShoulder);
        }

        public override string ToString() => this.Username;

        public async Task SetGamemodeAsync(Gamemode gamemode)
        {
            await client.QueuePacketAsync(new Net.Packets.Play.Client.GameState.ChangeGamemodeState(gamemode));
            this.Gamemode = gamemode;
        }
    }
}