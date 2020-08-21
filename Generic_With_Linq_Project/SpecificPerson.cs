using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_With_Linq_Project
{
    public class SpecificPerson<T> : ISpecificWork<T>
    {
        
        public string GetSpecificWork<T>(T data) where T : MediPersons
        {
            StoredData storedData = new StoredData();

            string Work = string.Empty;
            if (data is MediPersons)
            {
                MediPersons a = data as MediPersons;

                switch (a.PersonType)
                {
                    case MediPersonType.Doctor:
                        Work = storedData.databuff.Where(I => I.PersonType == MediPersonType.Doctor).FirstOrDefault().Works;
                        break;
                    case MediPersonType.Nurse:
                        Work = storedData.databuff.Where(I => I.PersonType == MediPersonType.Nurse).FirstOrDefault().Works;
                        break;
                    case MediPersonType.Patient:
                        Work = storedData.databuff.Where(I => I.PersonType == MediPersonType.Patient).FirstOrDefault().Works;
                        break;
                    default:
                        Work = "Unknown";
                        break;

                }
            }
            else
            {
                Work = "This is Another Person Who are not related Medical";
            }
            return Work;
        }
    }
}
