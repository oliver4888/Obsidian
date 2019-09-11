using Obsidian.Util;
using System;
using System.Threading.Tasks;

namespace Obsidian.Net.Packets
{
    public class EncryptionResponse : Packet
    {
        [Variable(0)]
        public int SecretLength { get; set; }

        [Variable(1)]
        public byte[] SharedSecret { get; private set; }

        [Variable(2)]
        public int TokenLength { get; set; }

        [Variable(3)]
        public byte[] VerifyToken { get; set; }

        public EncryptionResponse() : base(0x01, Array.Empty<byte>()) { }

        public EncryptionResponse(byte[] data) : base(0x01, data) { }

        public override async Task DeserializeAsync()
        {
            using var stream = new MinecraftStream(this.PacketData);
            var secretLength = await stream.ReadVarIntAsync();
            this.SharedSecret = await stream.ReadUInt8ArrayAsync(secretLength);

            var tokenLength = await stream.ReadVarIntAsync();
            this.VerifyToken = await stream.ReadUInt8ArrayAsync(tokenLength);
        }
    }
}