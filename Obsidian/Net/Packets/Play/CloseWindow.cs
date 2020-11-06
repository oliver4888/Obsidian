using Obsidian.API;
using Obsidian.Entities;
using Obsidian.Net.Packets.Play.Client;
using Obsidian.Serializer.Attributes;
using System.Threading.Tasks;

namespace Obsidian.Net.Packets.Play
{
    public class CloseWindow : IPacket
    {
        [Field(0)]
        public byte WindowId { get; set; }

        public int Id => 0x0A;

        public CloseWindow() { }

        public Task WriteAsync(MinecraftStream stream) => Task.CompletedTask;

        public async Task ReadAsync(MinecraftStream stream)
        {
            this.WindowId = await stream.ReadUnsignedByteAsync();
        }

        public async Task HandleAsync(Obsidian.Server server, Player player)
        {
            if (this.WindowId == 0)
                return;

            if (player.LastInteractedBlock.Type == Blocks.Materials.Chest)
            {
                await player.client.QueuePacketAsync(new BlockAction
                {
                    Location = player.LastInteractedBlock.Location,
                    ActionId = 1,
                    ActionParam = 0,
                    BlockType = player.LastInteractedBlock.Id
                });
                await player.SendSoundAsync(Sounds.BlockChestClose, player.Location.SoundPosition, SoundCategory.Blocks);
            }

        }
    }
}
