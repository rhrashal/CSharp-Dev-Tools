using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("                     **********  Wellcome   *************");

            Console.WriteLine("             Medical Test Order Form            ");

            Console.WriteLine("Write Patient Name...");
            string PName = Console.ReadLine();

            Console.WriteLine("Write Patient Birth date...(mm-dd-yyyy)");
            DateTime dateTime = DateTime.Now;
            try
            {
                dateTime = DateTime.Parse(Console.ReadLine());
            }
            catch
            {

            }
                
              

            PatientDetails PDetails = new PatientDetails(PName, dateTime);
            
            Console.WriteLine("Write Patient Patient Admit Date...");
            PDetails.Date = Console.ReadLine();

            Console.WriteLine("Write Patient Address...");
            PDetails.Address = Console.ReadLine();

            Console.WriteLine("Write Patient Cabin Number...");
            PDetails.CabinNo = Console.ReadLine();

            Console.WriteLine("<<<<<<<  Avleable Test  >>>>>>>>>>>>>>>");
            foreach (var a in Enum.GetNames(typeof(Particulars)))
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("Pls Write The Name of Test From above List");
            string Particulars = (Console.ReadLine()).ToUpper();

            int TotalBalance = 0;
            if (Particulars == "MRI")
            {
                TotalBalance += 3600;
            }
            else if(Particulars == "ECG")
            {
                TotalBalance  += 600;
            }
            else if (Particulars == "CBC")
            {
                TotalBalance += 500;
            }
            else if (Particulars == "PSA")
            {
                TotalBalance += 300;
            }
            else if (Particulars == "EKG")
            {
                TotalBalance += 300;
            }
            else if (Particulars == "ABG")
            {
                TotalBalance += 350;
            }


            IParticularList isl = (IParticularList)PDetails;

            List<string> subjectList = new List<string>();
            subjectList.Add(isl.ListOfParticular(Particulars));

            bool yesno = true;
            while (yesno)
            {
                Console.WriteLine("Do More Test ?  Yes or No ");
                string YN = (Console.ReadLine()).ToUpper();
                if (YN == "YES")
                {
                    Console.WriteLine(" Write The Name from above List");
                    Particulars = Console.ReadLine().ToUpper();
                    subjectList.Add(isl.ListOfParticular(Particulars));
                    
                    if (Particulars == "MRI")
                    {
                        TotalBalance += 3600;
                    }
                    else if (Particulars == "ECG")
                    {
                        TotalBalance += 600;
                    }
                    else if (Particulars == "CBC")
                    {
                        TotalBalance += 500;
                    }
                    else if (Particulars == "PSA")
                    {
                        TotalBalance += 300;
                    }
                    else if (Particulars == "EKG")
                    {
                        TotalBalance += 100;
                    }
                    else if (Particulars == "ABG")
                    {
                        TotalBalance += 350;
                    }
                }
                else
                {
                    
                    yesno = false;
                }

            }
            
            Console.WriteLine(PDetails.ToString());

            Console.WriteLine($" Total Test Name For <<{PName}>>");
            foreach (var a in subjectList)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine($"Total Balance : { TotalBalance} TK Only");



            Console.ReadLine();
        }
    }
}
