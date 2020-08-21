using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    enum Particulars {
        ABG____350TK =1,
        ECG____600TK,
        MRI____3600TK,
        CBC____500TK,
        EKG____100TK,
        PSA____300TK
    };



    abstract class Patient
    {
        protected string PatientName { get; set; }
        protected DateTime BirthDate { get; set; }
    }



    interface IParticularList
    {
        string ListOfParticular(string ParticularName);
    }





    class ImplimentInterface : Patient, IParticularList
    {
        string IParticularList.ListOfParticular(string ParticularName)
        {
            return ParticularName;
        }
    }



    sealed class PatientDetails : ImplimentInterface
    {
        public string Address { get; set; }
        public string CabinNo { get; set; }
        public string Date { get; set; }

        public PatientDetails(string Name, DateTime dateTime )
        {
            this.PatientName = Name;
            this.BirthDate = dateTime;
        }


        public PatientDetails()
        {
           
        }

        public string GetAge()
        {
            var t = DateTime.Now - BirthDate;
            int y = t.Days / 365;
            int m = (t.Days % 365) / 30;
            int d = t.Days - y * 365 - m * 30;
            return $"{y} years {m} months {d} days";
        }

        public override string ToString()
        {
            return $"<<<<<<  Patient Details   >>>>>> \n >>>>>> Patient Name : {PatientName} \n >>>>>> Patient Age : {GetAge()} \n >>>>>> Admit Date : {Date} \n >>>>>> Cabin No : {CabinNo} \n >>>>>> Patient Address : {Address}";
        }
    }
}
