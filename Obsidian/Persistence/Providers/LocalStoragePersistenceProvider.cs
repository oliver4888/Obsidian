using System;
using System.Collections.Generic;
using System.Text;

namespace Obsidian.Persistence.Providers
{
    public class LocalStoragePersistenceProvider : BasePersistenceProvider
    {
        private string folderPath;
        public LocalStoragePersistenceProvider(PersistenceInfo info) : base(info)
        {
            folderPath = this.info.Location;
        }

        public override void Create()
        {
            throw new NotImplementedException();
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Read()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
