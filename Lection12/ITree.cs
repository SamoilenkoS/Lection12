using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection12
{
    public interface ITree<T>
    {
        int Width { get; }
        int Height { get; }
        int Count { get; }
        bool Delete(T value);//true if find and deleted
        bool Contains(T value);//true if exist
    }
}
