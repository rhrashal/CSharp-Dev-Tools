using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_With_Linq_Project
{
    public interface ISpecificWork<T>
    {
        string GetSpecificWork<T>(T data) where T : MediPersons;
    }
}
