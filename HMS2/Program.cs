using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////function test:
            //string textValue = "HELLO";

            //static string MyToLower(string text)
            //{
            //    if (string.IsNullOrEmpty(text))//This method checks if a string is either null or empty
            //        return text;

            //    char[] chars = text.ToCharArray();//It breaks a string into individual characters so you can work with each one.

            //    for (int i = 0; i < chars.Length; i++)
            //    {
            //        if (chars[i] >= 'A' && chars[i] <= 'Z')
            //        {
            //            chars[i] = (char)(chars[i] + 32);
            //        }
            //    }

            //    return new string(chars);
            //}
            ////for test pervious method
            //Console.Write("Enter text: ");
            //string input = Console.ReadLine();

            //string result = MyToLower(input);

            //Console.WriteLine("Lowercase: " + result);

            // System Storage

            string[] patientNames = new string[100];
            string[] patientIDs = new string[100];
            string[] diagnoses = new string[100];
            bool[] admitted = new bool[100];
            string[] assignedDoctors = new string[100];
            string[] departments = new string[100];
            int[] visitCount = new int[100];
            double[] billingAmount = new double[100];


            DateTime[] lastVisitDate = new DateTime[100];
            DateTime[] lastDischargeDate= new DateTime[100];
            int[]daysInHospital= new int[100];
            string[]bloodType= new string[100];


            // Patient[] patients = new Patient[100];

            //////////////////////////////////////////////////////


            // Seed Data
            /////////////////////////////////////////////////////
            int lastIndex = 0;


            patientNames[lastIndex] = "Ali Hassan";
            patientIDs[lastIndex] = "P001";
            diagnoses[lastIndex] = "Flu";
            departments[lastIndex] = "General";
            admitted[lastIndex] = false;
            assignedDoctors[lastIndex] = "";
            visitCount[lastIndex] = 2;
            billingAmount[lastIndex] = 0;
            lastVisitDate[lastIndex] = DateTime.Parse("2025-01-10");
            lastDischargeDate[lastIndex]= DateTime.Parse("2025-01-15");
            daysInHospital[lastIndex] = 12;
            bloodType[lastIndex] = "A+";
            lastIndex++;

            patientNames[lastIndex] = "Sara Ahmed";
            patientIDs[lastIndex] = "P002";
            diagnoses[lastIndex] = "Fracture";
            departments[lastIndex] = "Orthopedics";
            admitted[lastIndex] = true;
            assignedDoctors[lastIndex] = "Dr. Noor";
            visitCount[lastIndex] = 4;
            billingAmount[lastIndex] = 0;
            lastVisitDate[lastIndex] = DateTime.Parse("2025-03-02");
            lastDischargeDate[lastIndex] = DateTime.Today;
            daysInHospital[lastIndex] = 8;
            bloodType[lastIndex] = "O-";
            lastIndex++;

            patientNames[lastIndex] = "Omar Khalid";
            patientIDs[lastIndex] = "P003";
            diagnoses[lastIndex] = "Diabetes";
            departments[lastIndex] = "Cardiology";
            admitted[lastIndex] = false;
            assignedDoctors[lastIndex] = "";
            visitCount[lastIndex] = 1;
            billingAmount[lastIndex] = 0;
            lastVisitDate[lastIndex] = DateTime.Parse("2024-12-20");
            lastDischargeDate[lastIndex] = DateTime.Parse("2024-12-28");
            daysInHospital[lastIndex] = 5;
            bloodType[lastIndex] = "B+";
            ////////////////////////////////////////////////////////////////////




            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("===== Healthcare Management System =====");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1.  Register New Patient");  //1 easy
                Console.WriteLine("2.  Admit Patient");//4 easy
                Console.WriteLine("3.  Discharge Patient");
                Console.WriteLine("4.  Search Patient"); //2 easy
                Console.WriteLine("5.  List All Admitted Patients"); //3 easy
                Console.WriteLine("6.  Transfer Patient to Another Doctor");
                Console.WriteLine("7.  View Most Visited Patients");
                Console.WriteLine("8.  Search Patients by Department");
                Console.WriteLine("9.  Billing Report");
                Console.WriteLine("10. Exit");
                Console.Write("Choose option: ");

                int choice = 0;

                try
                {

                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 10.");
                }

                switch (choice)
                {




                    case 1: // Register New Patient
                        lastIndex++;

                        Console.Write("Patient Name: ");
                        patientNames[lastIndex] = Console.ReadLine().Trim();

                        //Console.Write("Patient ID: ");
                        //patientIDs[lastIndex] = Console.ReadLine();

                        Console.Write("Diagnosis: ");
                        diagnoses[lastIndex] = Console.ReadLine().Trim();

                        Console.Write("Department: ");
                        departments[lastIndex] = Console.ReadLine().Trim();


                        //patientIDs[lastIndex] = "P00" + lastIndex;
                        //logical error handled for increament IDs auotomtically
                        patientIDs[lastIndex] = "P" + (lastIndex + 1).ToString("D3");

                        admitted[lastIndex] = false;
                        assignedDoctors[lastIndex] = "";
                        visitCount[lastIndex] = 0;
                        billingAmount[lastIndex] = 0;
                        Console.Write("blood Type: ");
                        bloodType[lastIndex] = Console.ReadLine().ToUpper();
                        lastVisitDate[lastIndex] = DateTime.Now;
                        lastDischargeDate[lastIndex] = DateTime.Now;
                        daysInHospital[lastIndex] = 0;
                        Console.WriteLine("Patient registered successfully with ID :" + patientIDs[lastIndex]);
                        break;

                    case 2: // Admit Patient
                        Console.Write("Enter Patient ID or Name: ");
                        string admitInput = Console.ReadLine();

                        bool admitFound = false;

                        for (int i = 0; i <= lastIndex; i++)
                        {
                            if (patientNames[i].ToLower() == admitInput.ToLower() || patientIDs[i].ToLower() == admitInput.ToLower())
                            {
                                admitFound = true;

                                if (admitted[i] == false)
                                {
                                    Console.Write("Doctor Name: ");
                                    assignedDoctors[i] = Console.ReadLine();
                                    //add admission date for admitted patient
                                    Console.Write("Enter the admission date: ");
                                    DateTime admissionDate;
                                    string formattedTime;

                                    if (!DateTime.TryParse(Console.ReadLine(), out admissionDate))
                                    {
                                        Console.WriteLine("Invalid date. Using today's date.");
                                        admissionDate = DateTime.Now;
                                    }

                                    formattedTime = admissionDate.ToString("yyyy-MM-dd HH:mm"); // always set formattedTime

                                    admitted[i] = true;
                                    visitCount[i]++;
                                    lastVisitDate[i] = admissionDate;

                                    Console.WriteLine("Patient admitted successfully and assigned to " + assignedDoctors[i]);

                                    if (visitCount[i] > 1)
                                        Console.WriteLine("This patient has been admitted " + visitCount[i] + " times. Last admission date: " + formattedTime);
                                    else
                                        Console.WriteLine("This is the first time.");
                                }
                                else
                                {
                                    Console.WriteLine("Patient is already admitted under " + assignedDoctors[i]);
                                }

                                break;
                            }
                        }

                        if (!admitFound)
                        {
                            Console.WriteLine("Patient not found.");
                        }
                        break;

                    case 3: // Discharge Patient
                        Console.Write("Enter Patient ID or Name: ");
                        string dischargeInput = Console.ReadLine();

                        bool dischargeFound = false;

                        for (int i = 0; i <= lastIndex; i++)
                        {
                            if (patientNames[i].ToLower() == dischargeInput.ToLower() ||
                                patientIDs[i].ToLower() == dischargeInput.ToLower())
                            {
                                dischargeFound = true;

                                if (!admitted[i])
                                {
                                    Console.WriteLine("This patient is not currently admitted");
                                    break;
                                }

                                double visitCharges = 0;
                                //visitCharges = Math.Round(visitCharges,2);
                                //add discharge date for admitted patent
                                Console.Write("Enter the discharge date: ");
                                DateTime dischargeDate;

                                if (!DateTime.TryParse(Console.ReadLine(), out dischargeDate))
                                {
                                    Console.WriteLine("Invalid date. Using today's date.");
                                    dischargeDate = DateTime.Now;
                                }

                                lastDischargeDate[i] = dischargeDate;
                                Console.WriteLine("enter the number of days the patient spent in hospital during this visit:");
                                int day = int.Parse(Console.ReadLine());
                                daysInHospital[i] += day;
                                Console.Write("Was there a consultation fee? (yes/no): ");
                                string hasFee = Console.ReadLine().ToLower();

                                if (hasFee == "yes")
                                {
                                    Console.Write("Enter consultation fee amount: ");
                                    if (double.TryParse(Console.ReadLine(), out double fee) && fee > 0)
                                    {
                                        billingAmount[i] += fee;

                                        visitCharges += fee;


                                    }
                                    else
                                    {
                                        Console.WriteLine("Fee must be a positive number.");
                                    }
                                }

                                Console.Write("Any medication charges? (yes/no): ");
                                string hasMeds = Console.ReadLine().ToLower();

                                if (hasMeds == "yes")
                                {
                                    Console.Write("Enter medication charges amount: ");
                                    if (double.TryParse(Console.ReadLine(), out double meds) && meds > 0)
                                    {
                                        billingAmount[i] += meds;
                                        visitCharges += meds;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid amount.");
                                    }
                                }

                                if (visitCharges > 0)
                                {
                                    double roundedVisitCharges = Math.Round(visitCharges, 2);
                                    Console.WriteLine("Total charges added this visit: " + roundedVisitCharges + " OMR" + " and  patient's updated total days in hospital:" + daysInHospital[i] + " dischargeDate:" + dischargeDate);
                                }
                                else
                                {
                                    Console.WriteLine("No charges recorded for this visit");
                                }

                                admitted[i] = false;
                                assignedDoctors[i] = "";

                                Console.WriteLine("Patient discharged successfully!");
                                break;
                            }
                        }


                        if (!dischargeFound)
                        {
                            Console.WriteLine("Patient not found");
                        }

                        break;

                    case 4: // Search Patient
                        Console.Write("Enter Patient ID or Name: ");
                        string searchInput = Console.ReadLine().ToUpper();


                        bool pateintFound = false;


                        for (int i = 0; i <= lastIndex; i++)
                        {


                            if (patientNames[i] == searchInput || patientIDs[i] == searchInput)
                            {
                                pateintFound = true;
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine("Name:           " + patientNames[i]);
                                Console.WriteLine("ID:             " + patientIDs[i]);
                                Console.WriteLine("Diagnosis:      " + diagnoses[i]);
                                Console.WriteLine("Department:     " + departments[i]);
                                Console.WriteLine(" blood Type:    " + bloodType[i]);
                                Console.WriteLine("Admitted:       " + admitted[i]);
                                Console.WriteLine("Total Visits:   " + visitCount[i]);
                                string diagnosis = diagnoses[i];
                                Console.WriteLine("Diagnosis: " + diagnosis);
                                Console.WriteLine("Number of  characters the diagnosis text contains:" + diagnosis.Length + "characters");
                                if (lastVisitDate[i] == null)
                                {
                                    Console.WriteLine("No admission recorded");
                                }
                                else
                                {
                                    Console.WriteLine("Last visit date: " + lastVisitDate[i]);
                                }
                                if (lastDischargeDate[i] == null)
                                {
                                    Console.WriteLine("No admission recorded");
                                }
                                else
                                {
                                    Console.WriteLine("Last Discharge Date: " + lastDischargeDate[i]);
                                }
                                Console.WriteLine("Total Days in Hospital:   " + daysInHospital[i]);
                                double roundedBilling = Math.Round(billingAmount[i], 2);

                                string billingText = Convert.ToString(roundedBilling);

                                Console.WriteLine("Total Billing:  " + billingText + " OMR");
                                //logical error handled 
                                //print doctor name if patient is admitted
                                bool isAdmitted = true; // or false depending on patient status
                                string doctorName = "";

                                Console.WriteLine(isAdmitted ? "Doctor: " + assignedDoctors[i] : "Patient is not currently admitted.");

                                Console.WriteLine("----------------------------------------");
                                break;
                            }


                        }

                        if (pateintFound == false)
                        {
                            Console.WriteLine("Patient not found");
                        }

                        break;






                    case 5: // List All Admitted Patients
                        Console.WriteLine("Currently Admitted Patients:");
                        Console.WriteLine("----------------------------------------");

                        Console.Write("Filter by name keyword (press Enter to skip): ");
                        string keyword = Console.ReadLine().Trim();

                        bool hasAdmitted = false;
                        int admittedCounter = 0;
                        double highestBillingAmount = 0;

                        for (int i = 0; i < lastIndex; i++)
                        {
                            if (admitted[i])
                            {
                                if (!string.IsNullOrEmpty(keyword) &&
                                    !patientNames[i].ToLower().Contains(keyword.ToLower()))
                                {
                                    continue;
                                }

                                Console.WriteLine("Name: " + patientNames[i] + " | ID: " + patientIDs[i]);

                                string displayDiagnosis = diagnoses[i].Length > 15
                                    ? diagnoses[i].Substring(0, 15) + "..."
                                    : diagnoses[i];

                                if (string.IsNullOrEmpty(displayDiagnosis))
                                    Console.WriteLine("Diagnosis: " + displayDiagnosis + " | Department: " + departments[i] + " | Doctor: " + assignedDoctors[i]);
                                Console.WriteLine("Admitted Since: " + lastVisitDate[i]);
                                Console.WriteLine("Billing: " + billingAmount[i].ToString("0.00") + " OMR");

                                hasAdmitted = true;
                                admittedCounter++;
                                highestBillingAmount = Math.Max(highestBillingAmount, billingAmount[i]);
                                continue;
                            }


                        }

                        if (hasAdmitted)
                        {
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Total admitted patients: " + admittedCounter);
                            Console.WriteLine("Highest billing among admitted patients: " + highestBillingAmount.ToString("0.00") + " OMR");
                        }
                        else
                        {
                            Console.WriteLine("No patients currently admitted");
                        }
                        break;

                    case 6: // Transfer Patient to Another Doctor
                        Console.Write("Enter current doctor name: ");
                        string currentDoctor = Console.ReadLine().Trim();

                        Console.Write("Enter new doctor name: ");
                        string newDoctor = Console.ReadLine().Trim();
                        currentDoctor = currentDoctor.Replace("Dr ", "Dr. ");
                        newDoctor = newDoctor.Replace("Dr ", "Dr. ");

                        //logical error handled in case both current and new doctor name have same name
                        bool doctorFound = false;
                        if (currentDoctor == newDoctor)
                        {
                            Console.WriteLine(" The names of current and new doctors must be different");
                            break;
                        }
                        else
                        {

                            for (int i = 0; i <= lastIndex; i++)
                            {
                                if (assignedDoctors[i] == currentDoctor && admitted[i] == true)
                                {
                                    doctorFound = true;
                                    assignedDoctors[i] = newDoctor;

                                    Console.WriteLine("Patient " + patientNames[i] + " has been transferred to " + newDoctor + "  and Patient last admitted on:" + lastVisitDate[i]);
                                    break;
                                }
                            }
                        }
                        if (doctorFound == false)
                        {
                            Console.WriteLine("No admitted patients found under this doctor");
                        }

                        break;

                    case 7: // View Most Visited Patients
                        Console.WriteLine("Most Visited Patients (by visit count):");
                        Console.WriteLine("----------------------------------------");

                        int[] tempVisits = new int[100];

                        for (int i = 0; i <= lastIndex; i++)
                        {
                            tempVisits[i] = visitCount[i];
                        }

                        for (int pass = 0; pass <= lastIndex; pass++)
                        {
                            int maxIndex = 0;

                            for (int i = 0; i <= lastIndex; i++)
                            {
                                if (tempVisits[i] > tempVisits[maxIndex])
                                {
                                    maxIndex = i;
                                }
                            }

                            Console.WriteLine("ID: " + patientIDs[maxIndex] + " | Name: " + patientNames[maxIndex] + " | Visits: " + tempVisits[maxIndex]);

                            tempVisits[maxIndex] = -1;
                        }

                        break;


                    case 8: // Search Patients by Department
                        Console.Write("Enter department name: ");
                        string searchDept = Console.ReadLine();

                        bool deptFound = false;

                        Console.WriteLine("Patients in department '" + searchDept.ToUpper() + "':");
                        Console.WriteLine("----------------------------------------");

                        for (int i = 0; i <= lastIndex; i++)
                        {
                            //here logical error handled using Contains()
                            if (departments[i].ToLower().Contains(searchDept.ToLower()))
                            {
                                deptFound = true;


                                string status = admitted[i] ? "Admitted" : "Not Admitted"; //ternary operator

                                // Truncate diagnosis if longer than 15 characters
                                string displayDiagnosis = diagnoses[i].Length > 15
                                    ? diagnoses[i].Substring(0, 15) + "..."
                                    : diagnoses[i];


                                Console.WriteLine("ID: " + patientIDs[i] + " | Name: " + patientNames[i] + " | Diagnosis: " + displayDiagnosis + " | Status: " + status + " | bloodType:" + bloodType[i]);
                            }
                        }

                        if (deptFound == false)
                        {
                            Console.WriteLine("No patients found in this department");
                        }

                        break;

                    case 9: // Billing Report
                        Console.WriteLine("Billing Report:");
                        Console.WriteLine("1. System-wide total");
                        Console.WriteLine("2. Individual patient");
                        Console.Write("Choose option: ");

                        int billingOption;
                        if (!int.TryParse(Console.ReadLine(), out billingOption) || (billingOption != 1 && billingOption != 2))
                        {
                            Console.WriteLine("Invalid input. Please enter 1 or 2.");
                            return; // exit or loop back to ask again
                        }

                        bool billingFound = false;

                        if (billingOption == 1)
                        {
                            // System-wide total
                            double totalBilling = 0;

                            for (int i = 0; i < lastIndex; i++)
                            {
                                totalBilling += billingAmount[i];
                            }

                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Total billing collected: " + totalBilling.ToString("0.00") + " OMR");
                        }
                        else if (billingOption == 2)
                        {
                            // Individual patient
                            Console.Write("Enter Patient ID or Name: ");
                            string billingInput = Console.ReadLine().Trim();

                            double grandTotal = 0;
                            double highestBilling = 0;
                            double lowestBilling = 0;
                            bool firstMatch = true; // for initializing highest and lowest billing


                            // Loop through all patients (use <= lastIndex if lastIndex is the last valid index)
                            for (int i = 0; i <= lastIndex; i++)
                            {
                                // Match patient by ID or name (case-insensitive for names)
                                if (patientIDs[i] == billingInput || patientNames[i].Equals(billingInput, StringComparison.OrdinalIgnoreCase))
                                {
                                    billingFound = true;

                                    // Add to grand total
                                    grandTotal += billingAmount[i];

                                    // Initialize or update highest/lowest billing
                                    if (firstMatch)
                                    {
                                        highestBilling = billingAmount[i];
                                        lowestBilling = billingAmount[i];
                                        firstMatch = false;
                                    }
                                    else
                                    {
                                        if (billingAmount[i] > highestBilling)
                                            highestBilling = billingAmount[i];
                                        if (billingAmount[i] < lowestBilling)
                                            lowestBilling = billingAmount[i];
                                    }

                                    // Print individual billing info
                                    Console.WriteLine("----------------------------------------");
                                    Console.WriteLine("Billing for " + patientNames[i] + ": "
                                        + billingAmount[i].ToString("0.00") + " OMR"
                                        + " | Last Visit Date: " + lastVisitDate[i]
                                        + " | Total Days: " + daysInHospital[i]);
                                }
                            }

                            // After loop, print totals if any match found
                            if (billingFound)
                            {
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine("Grand total billing for this patient: " + grandTotal.ToString("0.00") + " OMR");
                                Console.WriteLine("Lowest billing amount: " + lowestBilling.ToString("0.00") + " OMR");
                                Console.WriteLine("Highest billing amount: " + highestBilling.ToString("0.00") + " OMR");
                            }
                            else
                            {
                                Console.WriteLine("No billing records found for " + billingInput);
                            }
                        }

                            break;

                    case 10: // Exit
                        //ask user if want to exit the prgram
                        Console.WriteLine("Are you sure you want to exit? (yes/no)");
                        string confrim = Console.ReadLine().ToLower();
                        
                        if (confrim == "yes")
                        {
                            Console.WriteLine("Exiting system...");
                            Console.WriteLine("Thank you for using the Healthcare Management System!");
                            Console.WriteLine("----------------------------------------");
                            exit = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("return to the menu");
                        }

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

         
