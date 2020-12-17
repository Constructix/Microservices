using System.Collections.Generic;

namespace ConstructixOnLineServices.Repository
{
    public interface IRepository<T, K>
    {
        public T Get(K id);
        public List<T> GetAll();
    }
}