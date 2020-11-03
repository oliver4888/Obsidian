using System;
using System.Collections.Generic;
using System.Text;

namespace Obsidian.Persistence
{
    public abstract class BasePersistenceProvider
    {
        protected PersistenceInfo info;
        protected BasePersistenceProvider(PersistenceInfo info) 
        {
            this.info = info;
        }

        public abstract void Create();

        public abstract void Read();

        public abstract void Update();

        public abstract void Destroy();

    }
}
