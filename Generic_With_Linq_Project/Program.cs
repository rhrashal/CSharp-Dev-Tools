using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_With_Linq_Project
{
    class Program
    {
        static void Main(string[] args)
        {
                
            MediPersons DrRehana = new MediPersons()
            {
                 PersonType = MediPersonType.Doctor,
                 Name = "DrRehana",
                 gender = Gender.Male,
                 CabinNo =  10
            };
            Console.WriteLine(DrRehana.ToString());
            GenericPerson<MediPersons> person = new GenericPerson<MediPersons>();
            Console.WriteLine(person.GetGenericWork(DrRehana));



            MediPersons Sufia = new MediPersons()
            {
                PersonType = MediPersonType.Nurse,
                Name = "Sufia",
                gender = Gender.Female,
                CabinNo = 1
            };
            Console.WriteLine(Sufia.ToString());
            SpecificPerson<MediPersons> specificPerson = new SpecificPerson<MediPersons>();
            ISpecificWork<MediPersons> specificWork = specificPerson;
            Console.WriteLine(specificWork.GetSpecificWork(Sufia));


            MediPersons Abdul = new MediPersons()
            {
                PersonType = MediPersonType.Patient,
                Name = "Abdul",
                gender = Gender.Male,
                CabinNo = 1
            };
            Console.WriteLine(Abdul.ToString());
            Console.WriteLine(specificWork.GetSpecificWork(Abdul));

            Console.ReadKey();

        }
    }

    
}
