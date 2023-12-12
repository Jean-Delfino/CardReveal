using System.Collections.Generic;

namespace Reuse.Pooling
{
    public interface IPooledObjectHolder
    {
        public List<Spawner.PooledObjects> GetWantToPoolObjects();
    }

}