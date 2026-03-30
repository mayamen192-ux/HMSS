using System.ComponentModel;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace HMS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //input data
            string[] patientNames = new string[100];
            string[] patientIDs = new string[100];
            string[] diagnoses = new string[100];
            bool[] admitted = new bool[100]; // true = currently admitted
            string[] assignedDoctors = new string[100];
            string[] departments = new string[100]; // e.g. Cardiology, Orthopedics
            int[] visitCount = new int[100]; // total times admitted
            double[] billingAmount = new double[100]; // cumulative fees
            string[] appointmentDates = new string[100]; // e.g. "2025-09-15"
            string[] appointmentDoctors = new string[100]; // doctor for the appointment
            string[] appointmentDepts = new string[100]; // department for the appointment
            bool[] hasAppointment = new bool[100]; // true = appointment booked
            int lastPatientIndex = 0;


            //seed data
            //patient 1
            patientNames[lastPatientIndex] = "Ali Hassan";
            patientIDs[lastPatientIndex] = "P001";
            diagnoses[lastPatientIndex] = "Flu";
            departments[lastPatientIndex] = "General";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = " ";
            visitCount[lastPatientIndex] = 2;
            billingAmount[lastPatientIndex] = 0;
            appointmentDates[lastPatientIndex] = "2025-06-10";
            appointmentDoctors[lastPatientIndex] = " ";
            appointmentDepts[lastPatientIndex] = "General ";
            hasAppointment[lastPatientIndex]= false;
            lastPatientIndex++;


            //patient 2
            patientNames[lastPatientIndex] = "Sara Ahmed";
            patientIDs[lastPatientIndex] = "P002";
            diagnoses[lastPatientIndex] = "Fracture";
            departments[lastPatientIndex] = "Orthopedics";
            admitted[lastPatientIndex] = true;
            assignedDoctors[lastPatientIndex] = "Dr. Noor";
            visitCount[lastPatientIndex] = 4;
            billingAmount[lastPatientIndex] = 0;
            appointmentDates[lastPatientIndex] = "2025-03-11";
            appointmentDoctors[lastPatientIndex] = " Dr. Noor";
            appointmentDepts[lastPatientIndex] = "OrthopedicsGeneral ";
            hasAppointment[lastPatientIndex] = true;
            lastPatientIndex++;

            //patient 3
            patientNames[lastPatientIndex] = "Omar Khalid";
            patientIDs[lastPatientIndex] = "P003";
            diagnoses[lastPatientIndex] = "Diabetes";
            departments[lastPatientIndex] = "Cardiology";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = null;
            visitCount[lastPatientIndex] = 1;
            billingAmount[lastPatientIndex] = 0;
            appointmentDates[lastPatientIndex] = "2025-10-09";
            appointmentDoctors[lastPatientIndex] = "  ";
            appointmentDepts[lastPatientIndex] = "Cardiology ";
            hasAppointment[lastPatientIndex] = false;
            lastPatientIndex++;



            bool exit = false;
            while (exit == false)
            {
                //menue
                Console.WriteLine("===== Hospital Management System  =====");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Register New Patient");
                Console.WriteLine("2. Admit Patient");
                Console.WriteLine("3. Discharge Patient");
                Console.WriteLine("4. Search Patient");
                Console.WriteLine("5. List All Admitted Patients");
                Console.WriteLine("6. Transfer Patient to Another Doctor");
                Console.WriteLine("7. View Most Visited Patients");
                Console.WriteLine("8. Search Patients by Department");
                Console.WriteLine("9. Billing Report");
                Console.WriteLine("10. Schedule Appointment");
                Console.WriteLine("11. Exit");

                Console.Write("Choose option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        //Register New Patient
                        lastPatientIndex++;

                        Console.Write("patient Name: ");
                        patientNames[lastPatientIndex] = Console.ReadLine();
                        Console.Write("patient ID: ");
                        patientIDs[lastPatientIndex] = Console.ReadLine();
                        Console.Write("diagnoses: ");
                        diagnoses[lastPatientIndex] = Console.ReadLine();
                        Console.Write("department: ");
                        departments[lastPatientIndex] = Console.ReadLine();
                        admitted[lastPatientIndex] = false;
                        Console.Write("assignedDoctor: ");
                        assignedDoctors[lastPatientIndex] = Console.ReadLine();
                        visitCount[lastPatientIndex] = 0;
                        billingAmount[lastPatientIndex] = 0;

                        Console.WriteLine("Patient registered successfully!");


                        break;
                    case 2:
                        //Admit Patient
                        Console.Write("Enter patient ID or name: ");
                        string admitInput = Console.ReadLine();

                        Console.Write("Enter doctor name: ");
                        string doctorInput = Console.ReadLine();

                        bool admitFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            //check if patient is found or not
                            if (patientNames[i] == admitInput || patientIDs[i] == admitInput)
                            {

                                if (admitted[i] == false)
                                {
                                    Console.Write("patientNames: ");
                                    patientNames[i] = Console.ReadLine();
                                    admitted[i] = true;
                                    visitCount[i]++; //  Increment visit count
                                    billingAmount[i] = 0;

                                    Console.WriteLine("Patient admitted successfully and  assigned to doctor" + assignedDoctors[i]);
                                    Console.WriteLine(" This patient has been admitted    " + visitCount[i] + " times");
                                }
                                else
                                {
                                    Console.WriteLine("Patient is already admitted under" + assignedDoctors[i]);
                                }
                                break;


                            }

                        }
                        if (admitFound == false)
                        {
                            Console.WriteLine("Patient not found");

                        }
                        break;
                    case 3:
                        //Discharge Patient
                        Console.Write("Enter patient ID or name: ");
                        string dischargeInput = Console.ReadLine();

                        bool dischargeFound = false;

                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (patientNames[i] == dischargeInput || patientIDs[i] == dischargeInput)
                            {
                                dischargeFound = true;

                                // Check if patient is admitted
                                if (admitted[i] == true)
                                {
                                    double chargesThisVisit = 0;

                                    // Consultation fee
                                    Console.Write("Was there a consultation fee? (yes/no): ");
                                    string consultation = Console.ReadLine().ToLower();

                                    if (consultation == "yes")
                                    {
                                        Console.Write("Enter consultation amount: ");
                                        double amount = double.Parse(Console.ReadLine());
                                        chargesThisVisit += amount;
                                    }

                                    // Medication charges
                                    Console.Write("Any medication charges? (yes/no): ");
                                    string medication = Console.ReadLine().ToLower();

                                    if (medication == "yes")
                                    {
                                        Console.Write("Enter medication amount: ");
                                        double amount = double.Parse(Console.ReadLine());
                                        chargesThisVisit += amount;
                                    }

                                    // Add to total billing
                                    billingAmount[i] += chargesThisVisit;

                                    // Discharge patient
                                    admitted[i] = false;
                                    assignedDoctors[i] = " ";

                                    // Output result
                                    if (chargesThisVisit > 0)
                                    {
                                        Console.WriteLine("Total charges added this visit: " + chargesThisVisit + " OMR");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No charges recorded");
                                    }

                                    Console.WriteLine("Patient discharged successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("This patient is not currently admitted");
                                }

                                break;
                            }
                        }

                        // If patient not found
                        if (dischargeFound == false)
                        {
                            Console.WriteLine("Patient not found");
                        }

                        break;


                    case 4:
                        //Search Patient
                        Console.Write("Enter patient ID or name: ");
                        string searchInput = Console.ReadLine();

                        bool searchFound = false;

                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (patientNames[i] == searchInput || patientIDs[i] == searchInput)
                            {
                                searchFound = true;
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine("Patient Name: " + patientNames[i]);
                                Console.WriteLine("Patent ID : " + patientIDs[i]);
                                Console.WriteLine("diagnoses: " + diagnoses[i]);
                                Console.WriteLine("departments: " + departments[i]);
                                Console.WriteLine("admitted: " + admitted[i]);
                                Console.WriteLine("Times visited: " + visitCount[i]);
                                if (admitted[i] == false)
                                {
                                    Console.WriteLine("Current Patient: " + patientNames[i]);
                                }
                                Console.WriteLine("----------------------------------------");
                                break;
                            }
                        }

                        if (searchFound == false)
                        {
                            Console.WriteLine("Patient not found");
                        }



                        break;
                    case 5:
                        //List All Admitted Patients
                        Console.WriteLine("Available admmitted Patients:");
                        Console.WriteLine("----------------------------------------");

                        bool hasAdmmitted = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (admitted[i] == true)
                            {
                                Console.WriteLine("patient Names: " + patientNames[i] + " | patient ID: " + patientIDs[i] + " | diagnoses: " + diagnoses[i] + " |assignedDoctors: " + assignedDoctors[i]);
                                hasAdmmitted = true;
                            }
                        }
                        if (hasAdmmitted == false)
                        {
                            Console.WriteLine("No patients currently admitted");
                        }


                        break;
                    case 6:
                        //Transfer Patient to Another Doctor
                        Console.WriteLine("Enter current doctor name");
                        string currentDoctorName = Console.ReadLine();
                        Console.WriteLine("Enter new doctor name");
                        string newDoctorName = Console.ReadLine();

                        bool currentDoctorFound = false;
                        int currentDoctorIndex = 0;

                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (currentDoctorName == assignedDoctors[i])
                            {
                                currentDoctorIndex = i;
                                currentDoctorFound = true;
                                break;
                            }



                        }

                        if (currentDoctorFound == false)
                        {
                            Console.WriteLine("Current doctor name not found");
                        }
                        else
                        {
                            assignedDoctors[currentDoctorIndex] = newDoctorName;
                            Console.WriteLine("Doctors transferred successfully!");
                            Console.WriteLine("Patient " + patientNames[currentDoctorIndex] + "' is now assigned to " + newDoctorName);
                        }

                        break;
                    case 7:
                        //View Most Visited Patients
                        Console.WriteLine("Most Visited Patients (by visit count):");
                        Console.WriteLine("----------------------------------------");
                        for (int count = 100; count >= 0; count--) // Start from highest possible count
                        {
                            for (int i = 0; i <= lastPatientIndex; i++)
                            {
                                if (visitCount[i] == count)
                                {
                                    Console.WriteLine("patient ID: " + patientIDs[i] + " | patient Names: " + patientNames[i] + " | departments: " + departments[i] + " | diagnoses: " + diagnoses[i] + " | visit Count: " + visitCount[i]);

                                }

                            }
                        }
                        break;
                    case 8:
                        //Search Patients by Department 
                        Console.Write("Enter department name: ");
                        string departmentName = Console.ReadLine().ToLower();

                        bool departmentFound = false;
                        Console.WriteLine("Patients in department " + departmentName);
                        Console.WriteLine("----------------------------------------");
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (departments[i] != null && departments[i].ToLower() == departmentName)
                            {
                                departmentFound = true;
                                string admittedStatus = admitted[i] ? "admitted" : "Not admitted";
                                Console.WriteLine("patient ID: " + patientIDs[i] + " | patient Name: " + patientNames[i] + " | diagnoses: " + diagnoses[i] + " | Status: " + admittedStatus);
                            }
                        }

                        if (departmentFound == false)
                        {
                            Console.WriteLine("No patients found in this department");
                        }



                        break;
                    case 9:
                        //Billing Report
                        Console.WriteLine("===== Billing Report =====");
                        Console.WriteLine("1. System-wide total");
                        Console.WriteLine("2. Individual patient");
                        Console.Write("Choose option: ");
                        int option = int.Parse(Console.ReadLine());

                        switch (option)
                        {
                            case 1://System-wide total
                                double totalBilling = 0;
                                for (int i = 0; i <= lastPatientIndex; i++)
                                {
                                    totalBilling += billingAmount[i];

                                }
                                Console.WriteLine("Total  billing: " + totalBilling + " OMR");


                                break;
                            case 2://Individual patient
                                Console.Write("Enter patient ID or  patient name: ");
                                string input = Console.ReadLine();
                                bool found = false;
                                for (int i = 0; i <= lastPatientIndex; i++)
                                {
                                   

                                    if(input == patientIDs[i] ||  input == patientNames[i])
                                    {
                                        found = true;
                                        
                                        Console.WriteLine("Billing amount for " + patientNames[i] + ": " + billingAmount[i] + " OMR");

                                        break;
                                    }


                              
                                }


                                if (found == false)
                                {
                                    Console.WriteLine("No billing records found for this patient");
                                }
                                break;


                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }

                        break;

                    case 10:
                        //Schedule Appointment
                        Console.Write("Enter patient ID or  patient name: ");
                        string input2 = Console.ReadLine();
                        bool found2 = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {


                            if (input2 == patientIDs[i] || input2 == patientNames[i])
                            {
                                found2 = true;
                                if (hasAppointment[i] = false)
                                {
                                    Console.WriteLine("Patient already has an appointment on"+ appointmentDates[i]);


                                }
                                //on success
                                else
                                {
                                    Console.WriteLine("Enter appointment Date: ");
                                    string appointmentDate = Console.ReadLine();
                                    Console.WriteLine("Enter Doctor name for appointment");
                                    string DoctorAppoinment = Console.ReadLine();
                                    Console.WriteLine(" Enter department for appoinment");
                                    string departmentAppoinmment=Console.ReadLine();

                                    hasAppointment[i] = true;
                                    appointmentDates[i] = appointmentDate;
                                    appointmentDoctors[i] = DoctorAppoinment;
                                    appointmentDepts[i] = departmentAppoinmment;
                                }

                                Console.WriteLine("Appointment scheduled for "+ patientNames[i]+" on "+ appointmentDates[i]+" with DR. "+ appointmentDoctors[i]+" in department "+ appointmentDepts[i]);
                                break;
                            }
                        }

                        if (found2 == false)
                        {
                            Console.WriteLine("Patient not found");
                        }
                        break;
                    case 11:
                        //exit operation
                        Console.WriteLine("Exiting program...");
                        Console.WriteLine("Thank you for using Hospital Management System!");
                        Console.WriteLine("----------------------------------------");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();


            }
        }
    }
}