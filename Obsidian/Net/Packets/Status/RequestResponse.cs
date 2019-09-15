using Newtonsoft.Json;
using Obsidian.Entities;
using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class RequestResponse : Packet
    {
        public RequestResponse(string json) : base(0x00, System.Array.Empty<byte>()) => this.Json = json;

        public RequestResponse(ServerStatus status) : base(0x00, System.Array.Empty<byte>()) => this.Json = JsonConvert.SerializeObject(status);

        [Variable(0)]
        public string Json;
    }
}