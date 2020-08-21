using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Linq
{
    class Program
    {
        static void Main(string[] args)
        {

            Patient[] patients = new Patient[]
            {
                new Patient{ PatienFirstName = "Md", PatienLastName = "Alauddin", Age = 16, DoctorId = 1, ThanaID = 1},
                new Patient{ PatienFirstName = "Mohib", PatienLastName = "ullah", Age = 26, DoctorId = 2, ThanaID = 2},
                new Patient{ PatienFirstName = "Zulhas", PatienLastName = "Ahmed", Age = 36, DoctorId = 3, ThanaID = 3},
                new Patient{ PatienFirstName = "Robiul", PatienLastName = "Hossain", Age = 46, DoctorId = 4, ThanaID = 4},
                new Patient{ PatienFirstName = "Md", PatienLastName = "Kawser", Age = 6, DoctorId = 5, ThanaID = 5},
                new Patient{ PatienFirstName = "Benjir", PatienLastName = "Nilay", Age = 40, DoctorId = 1, ThanaID = 6},
                new Patient{ PatienFirstName = "Rokiya", PatienLastName = "Akter", Age = 10, DoctorId = 2, ThanaID = 7},
                new Patient{ PatienFirstName = "Akram", PatienLastName = "Hossain", Age = 11, DoctorId = 3, ThanaID = 8},
                new Patient{ PatienFirstName = "Alli", PatienLastName = "Ahmed", Age = 19, DoctorId = 4, ThanaID = 9},
                new Patient{ PatienFirstName = "Mahammad", PatienLastName = "alli", Age = 13, DoctorId = 5, ThanaID = 10},
                new Patient{ PatienFirstName = "Sharmin", PatienLastName = "Akter", Age = 70, DoctorId = 5, ThanaID = 11},
                new Patient{ PatienFirstName = "Rashed", PatienLastName = "khan", Age = 22, DoctorId = 4, ThanaID = 12 },
                new Patient{ PatienFirstName = "Md", PatienLastName = "hasan", Age = 33, DoctorId = 3, ThanaID = 13},
                new Patient{ PatienFirstName = "Rabeya", PatienLastName = "akter", Age = 1, DoctorId = 2, ThanaID = 14},
                new Patient{ PatienFirstName = "Arif", PatienLastName = "Hossain", Age = 44, DoctorId = 1, ThanaID = 15},
                new Patient{ PatienFirstName = "Md", PatienLastName = "Hamid", Age = 13, DoctorId = 1, ThanaID = 16},
                new Patient{ PatienFirstName = "Atik", PatienLastName = "hasan", Age = 25, DoctorId = 2, ThanaID = 17},
                new Patient{ PatienFirstName = "Kabir", PatienLastName = "Miah", Age = 28, DoctorId = 3, ThanaID = 18},
                new Patient{ PatienFirstName = "Imran", PatienLastName = "Hossain", Age = 17, DoctorId = 4, ThanaID = 19},
                new Patient{ PatienFirstName = "Anis", PatienLastName = "Admed", Age = 16, DoctorId = 5, ThanaID = 20}

            };




            Doctor[] doctor = new Doctor[]
            {
                new Doctor{ DoctorId = 1, DoctorName = "Md Korimul" },
                new Doctor{ DoctorId = 2, DoctorName = "Prity Ray" },
                new Doctor{ DoctorId = 3, DoctorName = "Shodiul islam" },
                new Doctor{ DoctorId = 4, DoctorName = "Yeasin kobir" },
                new Doctor{ DoctorId = 5, DoctorName = "Mahabub ul haque" }
            };




            Country[] countries = new Country[]
            {
                new Country {CountryID = 1, CountryName  = "Bangladesh"},
                new Country {CountryID = 2, CountryName  = "Pakistan"},
                new Country {CountryID = 3, CountryName  = "India"}
            };

            Division[] divisions = new Division[]
            {
                new Division {DivisionID = 1, DivisionName = "Dhaka", CountryID = 1},
                new Division {DivisionID = 2, DivisionName = "Chitagong", CountryID = 1},
                new Division {DivisionID = 3, DivisionName = "Rajshai", CountryID = 1},
                new Division {DivisionID = 4, DivisionName = "Barisal", CountryID = 1},
                new Division {DivisionID = 5, DivisionName = "Khulna", CountryID = 1},
                new Division {DivisionID = 6, DivisionName = "Rangpur", CountryID = 1},
                new Division {DivisionID = 7, DivisionName = "Maymanshing", CountryID = 1},
                new Division {DivisionID = 8, DivisionName = "Sylhet", CountryID = 1},
                new Division {DivisionID = 9, DivisionName = "Naya Dillhi", CountryID = 3},
                new Division {DivisionID = 10, DivisionName = "Tripura", CountryID = 3},
                new Division {DivisionID = 11, DivisionName = "Islamabad", CountryID = 2},
                new Division {DivisionID = 12, DivisionName = "Lahor", CountryID = 2}
            };



            District[] districts = new District[]
            {
                new District {DistrictID = 1, DistrictName = "Dhaka", DivisionID = 1},
                new District {DistrictID = 2, DistrictName = "Faridpur", DivisionID = 1},
                new District {DistrictID = 3, DistrictName = "Gazipur", DivisionID = 1},
                new District {DistrictID = 4, DistrictName = "Gopalganj", DivisionID = 1},
                new District {DistrictID = 5, DistrictName = "Jamalpur", DivisionID = 1},
                new District {DistrictID = 6, DistrictName = "Kishoreganj", DivisionID = 1},
                new District {DistrictID = 7, DistrictName = "Madaripur", DivisionID = 1},
                new District {DistrictID = 8, DistrictName = "Munshiganj", DivisionID = 1},
                new District {DistrictID = 9, DistrictName = "Narayanganj", DivisionID = 1},
                new District {DistrictID = 10, DistrictName = "Manikganj", DivisionID = 1},

                new District {DistrictID = 11, DistrictName = "Comilla", DivisionID = 2},
                new District {DistrictID = 12, DistrictName = "Cox's Bazar", DivisionID = 2},
                new District {DistrictID = 13, DistrictName = "Feni", DivisionID = 2},
                new District {DistrictID = 14, DistrictName = "Lakshmipur", DivisionID = 2},
                new District {DistrictID = 15, DistrictName = "Noakhali", DivisionID = 2},

                new District {DistrictID = 16, DistrictName = "Bogra", DivisionID = 3},
                new District {DistrictID = 17, DistrictName = "Joypurhat", DivisionID = 3},
                new District {DistrictID = 18, DistrictName = "Natore", DivisionID = 3},
                new District {DistrictID = 19, DistrictName = "Pabna", DivisionID = 3},
                new District {DistrictID = 20, DistrictName = "Sirajganj", DivisionID = 3},

                new District {DistrictID = 21, DistrictName = "Barguna", DivisionID = 4},
                new District {DistrictID = 22, DistrictName = "Bhola", DivisionID = 4},
                new District {DistrictID = 23, DistrictName = "Jhalokati", DivisionID = 4},
                new District {DistrictID = 24, DistrictName = "Patuakhali", DivisionID = 4},
                new District {DistrictID = 25, DistrictName = "Pirojpur", DivisionID = 4},

                new District {DistrictID = 26, DistrictName = "Bagerhat", DivisionID = 5},
                new District {DistrictID = 27, DistrictName = "Jessore", DivisionID = 5},
                new District {DistrictID = 28, DistrictName = "Jhenaidaha", DivisionID = 5},
                new District {DistrictID = 29, DistrictName = "Kushtia", DivisionID = 5},
                new District {DistrictID = 30, DistrictName = "Magura", DivisionID = 5},

                new District {DistrictID = 31, DistrictName = "Dinajpur", DivisionID = 6},
                new District {DistrictID = 32, DistrictName = "Gaibandha", DivisionID = 6},
                new District {DistrictID = 33, DistrictName = "Lalmonirhat", DivisionID = 6},
                new District {DistrictID = 34, DistrictName = "Nilphamari", DivisionID = 6},
                new District {DistrictID = 35, DistrictName = "Panchagarh", DivisionID = 6},

                new District {DistrictID = 36, DistrictName = "Jamalpur", DivisionID = 7},
                new District {DistrictID = 37, DistrictName = "Mymensingh", DivisionID = 7},
                new District {DistrictID = 38, DistrictName = "Netrokona", DivisionID = 7},
                new District {DistrictID = 39, DistrictName = "Sherpur", DivisionID = 7},

                new District {DistrictID = 40, DistrictName = "Sylhet", DivisionID = 8},
                new District {DistrictID = 41, DistrictName = "Habiganj", DivisionID = 8},
                new District {DistrictID = 42, DistrictName = "Maulvi Bazar", DivisionID = 8},
                new District {DistrictID = 43, DistrictName = "Sunamganj", DivisionID = 8}
             
            };

            Thana[] thanas = new Thana[]
            {
                new Thana {ThanaID = 1, ThanaName = "Kotwali", ZipCode = 3511, DistrictID = 1},
                new Thana {ThanaID = 2, ThanaName = "Sher-e-Bangla Nagar", ZipCode = 3512, DistrictID = 1},
                new Thana {ThanaID = 3, ThanaName = "Lalbagh", ZipCode = 3513, DistrictID = 1},
                new Thana {ThanaID = 4, ThanaName = "Badda", ZipCode = 3514, DistrictID = 1},

                new Thana {ThanaID = 5, ThanaName = "Boalmari", ZipCode = 3515, DistrictID = 2},
                new Thana {ThanaID = 6, ThanaName = "Bhanga", ZipCode = 3516, DistrictID = 2},
                new Thana {ThanaID = 7, ThanaName = "Charbhadrasan", ZipCode = 3517, DistrictID = 2},
                new Thana {ThanaID = 8, ThanaName = "Nagarkanda", ZipCode = 3518, DistrictID = 2},

                new Thana {ThanaID = 9, ThanaName = "Kaliakair", ZipCode = 3519, DistrictID = 3},
                new Thana {ThanaID = 10, ThanaName = "Kapasia", ZipCode = 3520, DistrictID = 3},
                new Thana {ThanaID = 11, ThanaName = "Sreepur", ZipCode = 3521, DistrictID = 3},

                new Thana {ThanaID = 12, ThanaName = "Kashiani", ZipCode = 3522, DistrictID = 4},
                new Thana {ThanaID = 13, ThanaName = "Kotalipara", ZipCode = 3523, DistrictID = 4},

                new Thana {ThanaID = 14, ThanaName = "Dewanganj", ZipCode = 3524, DistrictID = 5},
                new Thana {ThanaID = 15, ThanaName = "Jamalpur Sadar", ZipCode = 3525, DistrictID = 5},

                new Thana {ThanaID = 16, ThanaName = "Hossainpur", ZipCode = 3526, DistrictID = 6},
                new Thana {ThanaID = 17, ThanaName = "Austagram", ZipCode = 3527, DistrictID = 6},

                new Thana {ThanaID = 18, ThanaName = "Madaripur Sadar", ZipCode = 3528, DistrictID = 7},
                new Thana {ThanaID = 19, ThanaName = "kalkini", ZipCode = 3529, DistrictID = 7}

            
            };


            var Print = countries.Select(co => new { co.CountryID, co.CountryName })
                        .Join(divisions, co => co.CountryID, dv => dv.CountryID, (co, dv) =>
                                    new { co.CountryID, co.CountryName, dv.DivisionID, dv.DivisionName })
                        .Join(districts, dv => dv.DivisionID, ds => ds.DivisionID, (dv, ds) =>
                                    new { dv.CountryID, dv.CountryName, dv.DivisionID, dv.DivisionName, ds.DistrictID, ds.DistrictName })
                        .Join(thanas, ds => ds.DistrictID, th => th.DistrictID, (ds, th) =>
                                    new { ds.CountryName, ds.DivisionName, ds.DistrictName, th.ThanaName, th.ZipCode });


           

            var Print2 = from country in countries
                        join division in divisions
                            on country.CountryID equals division.CountryID
                        join district in districts
                            on division.DivisionID equals district.DivisionID
                        join thana in thanas
                            on district.DistrictID equals thana.DistrictID
                        join patient in patients
                            on thana.ThanaID equals patient.ThanaID
                        join doct in doctor
                            on patient.DoctorId equals doct.DoctorId
                        select new {
                                    patient.PatienFirstName,
                                    patient.PatienLastName,
                                    doct.DoctorName,
                                    thana.ThanaName,
                                    thana.ZipCode,
                                    district.DistrictName,
                                    division.DivisionName,
                                    country.CountryName,
                            
                        };


            foreach (var i in Print2 )
            {
                Console.WriteLine(i.ToString().Replace("{", " ").Replace("}", " "));
            }

            foreach (var i in Print)
            {
                Console.WriteLine(i.ToString().Replace("{", " ").Replace("}", " "));
            }


            Console.ReadKey();
        }
    }
}
