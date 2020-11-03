using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Obsidian.Persistence
{
    public class PersistenceInfo
    {
        /// <summary>
        /// Location for storage.
        /// This will be a folder path for local storage, or
        /// a connection string for database storage.
        /// </summary>
        public string Location { get; set; }

        public bool Compress { get; set; }

        public Type DataType { get; set; }

        public PersistenceInfo()
        {
            Location = string.Empty;
            Compress = false;
            DataType = typeof(Stream);
        }
    }
}
