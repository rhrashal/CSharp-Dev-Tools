using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_With_Linq_Project
{
    public interface IGenericWork<T>
    {
        string GetGenericWork(T obj);
    }
}
