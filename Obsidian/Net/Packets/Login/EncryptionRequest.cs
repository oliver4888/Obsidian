using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class EncryptionRequest : Packet
    {
        [Variable(0, VariableType.String)]
        public string ServerId { get; private set; }

        [Variable(1)]
        public int KeyLength { get; set; }

        [Variable(2)]
        public byte[] PublicKey { get; private set; }

        [Variable(3)]
        public int TokenLength = 4;

        [Variable(4)]
        public byte[] VerifyToken { get; private set; }

        public EncryptionRequest(byte[] publicKey, byte[] verifyToken) : base(0x01, new byte[0])
        {
            this.PublicKey = publicKey;
            this.KeyLength = publicKey.Length;
            this.VerifyToken = verifyToken;
        }
    }
}