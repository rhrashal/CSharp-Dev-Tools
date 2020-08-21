using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_With_Linq_Project
{
    public class PersonsWorks
    {
        public MediPersonType PersonType { get; set; }

        public string Works { get; set; }

        public PersonsWorks[] personsWorks;

        public PersonsWorks(MediPersonType personType, string Works)
        {
            this.PersonType = personType;
            this.Works = Works;

        }

        public PersonsWorks()
        {
        
        }
        
    }

    public class StoredData
    {

        public PersonsWorks[] databuff;

        public StoredData()
        {
            PersonsWorks pWorks = new PersonsWorks();
            pWorks.personsWorks = new PersonsWorks[]
            {
                new PersonsWorks(){ PersonType =  MediPersonType.Doctor, Works = "A Doctor give Treatment to the Patient" } ,
                new PersonsWorks(){PersonType = MediPersonType.Nurse, Works = "A Nurse All Time Careing & observation To the Patient"} ,
                new PersonsWorks(){PersonType = MediPersonType.Patient, Works = "A Patient Take Treatment & Suggestions From Doctors & Nurse" }
            };
            databuff = pWorks.personsWorks;
        }

    }
}
