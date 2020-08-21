using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_With_Linq_Project
{
    public class MediPersons
    {
        public string Name { get; set; }
        public MediPersonType PersonType { get; set; }
        public Gender gender { set; get; }
        public int CabinNo { get; set; }


        public override string ToString()
        {
            return $"\n\n  Person Name : {Name}  \n  Qualification : {PersonType}  \n  Gender : {gender} \n  Patient Cabin No : {CabinNo}";
        }

    }
}
