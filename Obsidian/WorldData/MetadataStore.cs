using Obsidian.API;
using Obsidian.Blocks;
using System;
using System.Collections.Generic;

namespace Obsidian.WorldData
{
    public class MetadataStore
    {
        private readonly Dictionary<string, object> store = new Dictionary<string, object>();

        public Position BlockLocation { get; set; }

        public Materials BlockType { get; set; }

        public short BlockId { get; set; }

        public object Get(string key) => this.store.GetValueOrDefault(key);

        public void Set(string key, object value)
        {
            if (value is null)
                throw new NullReferenceException(nameof(value));

            this.store[key] = value;
        }
    }
}
