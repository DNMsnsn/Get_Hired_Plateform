using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjetS04.Models
{
    internal interface DAO<T>
    {
        T findById(int id);
        List<T> findAll();
        bool insert(T obj);
        bool update(T obj);
        bool delete(T obj);
    }
}
