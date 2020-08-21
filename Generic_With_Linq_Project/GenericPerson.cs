using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_With_Linq_Project
{
    public class GenericPerson<T> : IGenericWork<T>
    {
        public string GetGenericWork(T obj)
        {
            StoredData storedData = new StoredData();

            string Work = string.Empty;
            if (obj is MediPersons)
            {
                MediPersons a = obj as MediPersons;
                switch (a.PersonType)
                {
                    case MediPersonType.Doctor:
                        Work = storedData.databuff.Where(w => w.PersonType == MediPersonType.Doctor ).FirstOrDefault().Works ;
                        break;
                    case MediPersonType.Nurse:
                        Work = storedData.databuff.Where(w => w.PersonType == MediPersonType.Nurse).FirstOrDefault().Works;
                        break;
                    case MediPersonType.Patient:
                        Work = storedData.databuff.Where(w => w.PersonType == MediPersonType.Patient).FirstOrDefault().Works;
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
