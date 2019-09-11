using Obsidian.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Net.Packets
{
    public class PluginMessage : Packet
    {
        public List<PluginMessageHandler> Handlers = new List<PluginMessageHandler>()
        {
            new MinecraftBrand()
        };

        [Variable(0)]
        public string Channel { get; private set; }

        [Variable(1)]
        public byte[] Data { get; private set; }

        public PluginMessage(string channel, byte[] data) : base(0x19, System.Array.Empty<byte>())
        {
            //TODO: ADD check pls
            this.Channel = channel;
            this.Data = data;
        }

        public async override Task DeserializeAsync()
        {
            using var stream = new MinecraftStream(this.PacketData);
            this.Channel = await stream.ReadIdentifierAsync();

            await Handlers.First(h => h.Channel == this.Channel).HandleAsync(stream);
        }
    }

    public abstract class PluginMessageHandler
    {
        public abstract string Channel { get; }

        public abstract Task HandleAsync(MinecraftStream stream);
    }

    public class MinecraftBrand : PluginMessageHandler
    {
        public override string Channel => "minecraft:brand";

        public override async Task HandleAsync(MinecraftStream stream) => await stream.ReadStringAsync();
    }
}