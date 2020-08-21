using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Patient_Admission_Form
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            PatientList.ItemsSource = GetPatientList();
        }

       

        private ObservableCollection<PatientDetails> GetPatientList()
        {
            const string GetPatientQuery = @"SELECT [PatientID],[FirstName] ,[LastName],[Age],[MobileNumber],[Address]
          FROM [dbo].[PatientDetails]";

            var patientDetailsInfo = new ObservableCollection<PatientDetails>();
            try
            {

                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-K5BVOG3\SQLEXPRESS01;Initial Catalog=PatientAdmittionForm;user id=sa ; password=123;"))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetPatientQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var patientDetails = new PatientDetails();
                                    patientDetails.PatientID = reader.GetInt32(0).ToString();
                                    patientDetails.FirstName = reader.GetString(1);
                                    patientDetails.LastName = reader.GetString(2);
                                    patientDetails.Age = reader.GetString(3);
                                    patientDetails.MobileNumber = reader.GetString(4);
                                    patientDetails.Address = reader.GetString(5);

                                    patientDetailsInfo.Add(patientDetails);
                                }
                            }
                        }
                    }
                }
                return patientDetailsInfo;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

          
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K5BVOG3\SQLEXPRESS01;Initial Catalog=PatientAdmittionForm;user id=sa ; password=123;");
          
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(@" if ( (select  count(*) from PatientDetails where PatientID = '" + this.PatientID.Text + "') = 0) begin INSERT INTO[dbo].[PatientDetails] ([PatientID],[FirstName],[LastName],[Age],[MobileNumber],[Address]) VALUES(" + this.PatientID.Text + ", '" + this.FirstName.Text + "', '" + this.LastName.Text + "', '" + this.Age.Text + "', '" + this.MobileNumber.Text + "', '" + this.Address.Text + "') end", con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();

            PatientList.ItemsSource = GetPatientList();

        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K5BVOG3\SQLEXPRESS01;Initial Catalog=PatientAdmittionForm;user id=sa; password=123;");
            
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            string updateString = " UPDATE[dbo].[PatientDetails]  SET [FirstName] = '" + this.FirstName.Text + "',[LastName] = '" + this.LastName.Text + "',[Age] = '" + this.Age.Text + "',[MobileNumber] = '" + this.MobileNumber.Text + "',[Address] = '" + this.Address.Text + "'  WHERE PatientID = '" + this.PatientID.Text + "'";

            SqlCommand cmd = new SqlCommand(updateString, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();

            PatientList.ItemsSource = GetPatientList();


        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K5BVOG3\SQLEXPRESS01;Initial Catalog=PatientAdmittionForm;user id=sa ; password=123;");
            
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[PatientDetails] WHERE PatientID = ' " + this.PatientID.Text + "'", con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();

            PatientList.ItemsSource = GetPatientList();
        }
    }
    
}
