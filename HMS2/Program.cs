using System;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS2
{
    internal class Program
    {
     
            // global storage
            static string[] patientNames = new string[100];
            static string[] patientIDs = new string[100];
            static string[] diagnoses = new string[100];
            static bool[] admitted = new bool[100];
            static string[] assignedDoctors = new string[100];
            static string[] departments = new string[100];
            static int[] visitCount = new int[100];
            static double[] billingAmount = new double[100];

            static DateTime[] lastVisitDate = new DateTime[100];
            static DateTime[] lastDischargeDate = new DateTime[100];
            static int[] daysInHospital = new int[100];
            static string[] bloodType = new string[100];

            static string[] doctorNames = new string[50];
            static int[] doctorAvailableSlots = new int[50];
            static int[] doctorVisitCount = new int[50];

            static int lastIndex = 0;
            static int lastDoctorIndex = 0;

        
        static public void seedData()
        {
            //patient 1
            patientNames[lastIndex] = "Ali Hassan";
            patientIDs[lastIndex] = "P001";
            diagnoses[lastIndex] = "Flu";
            departments[lastIndex] = "General";
            admitted[lastIndex] = false;
            assignedDoctors[lastIndex] = "";
            visitCount[lastIndex] = 2;
            billingAmount[lastIndex] = 0;
            lastVisitDate[lastIndex] = DateTime.Parse("2025-01-10");
            lastDischargeDate[lastIndex] = DateTime.Parse("2025-01-15");
            daysInHospital[lastIndex] = 12;
            bloodType[lastIndex] = "A+";
            lastIndex++;
            doctorNames[lastDoctorIndex] = "Dr. Noor ";
            doctorAvailableSlots[lastDoctorIndex] = 5;
            doctorVisitCount[lastDoctorIndex] = 0;
            lastDoctorIndex++;

            //patient 2
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
            doctorNames[lastDoctorIndex] = "Dr. Salem ";
            doctorAvailableSlots[lastDoctorIndex] = 3;
            doctorVisitCount[lastDoctorIndex] = 0;
            lastDoctorIndex++;

            //patient 3
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
            doctorNames[lastDoctorIndex] = "Dr. Hana ";
            doctorAvailableSlots[lastDoctorIndex] = 8;
            doctorVisitCount[lastDoctorIndex] = 0;
            lastDoctorIndex++;

        }
        static public void displayMenue()
        {
            Console.WriteLine("===== Healthcare Management System =====");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("1.  Register New Patient");  
            Console.WriteLine("2.  Admit Patient");
            Console.WriteLine("3.  Discharge Patient");
            Console.WriteLine("4.  Search Patient"); 
            Console.WriteLine("5.  List All Admitted Patients"); 
            Console.WriteLine("6.  Transfer Patient to Another Doctor");
            Console.WriteLine("7.  View Most Visited Patients");
            Console.WriteLine("8.  Search Patients by Department");
            Console.WriteLine("9.  Billing Report");           
            Console.WriteLine("10. Add Doctor");
            Console.WriteLine("11. Doctor Salary Report");
            Console.WriteLine("12. Exit");
            Console.Write("Choose option: ");

        }
        static public string RegisterPatient(string name, string blood, string department, string diagnosis)
        {
            Console.WriteLine("Register New Patient...");

            patientNames[lastIndex] = name;
            diagnoses[lastIndex] = diagnosis;
            departments[lastIndex] = department;
            bloodType[lastIndex] = blood.ToUpper();

            patientIDs[lastIndex] = "P" + (lastIndex + 1).ToString("D3");

            admitted[lastIndex] = false;
            assignedDoctors[lastIndex] = "";
            visitCount[lastIndex] = 0;
            billingAmount[lastIndex] = 0;

            lastVisitDate[lastIndex] = DateTime.Now;
            lastDischargeDate[lastIndex] = DateTime.Now;
            daysInHospital[lastIndex] = 0;

            Console.WriteLine("Patient registered successfully");
            //increament index after sorted
            lastIndex++; 

            return patientIDs[lastIndex - 1];
        }
        static public int searchPatient(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
                return -1;

            searchInput = searchInput.ToLower();

            for (int i = 0; i < lastIndex; i++)
            {
                if ((patientNames[i] != null && patientNames[i].ToLower() == searchInput) ||
                    (patientIDs[i] != null && patientIDs[i].ToLower() == searchInput))
                {
                    return i;
                }
            }
            return -1;
        }
        static public void printPatientDetails(int index)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Name:           " + patientNames[index]);
            Console.WriteLine("ID:             " + patientIDs[index]);

            // FIX: better formatting
            Console.WriteLine("Diagnosis:      " + diagnoses[index]);
            Console.WriteLine("Diagnosis Length: " + diagnoses[index].Length + " characters");

            Console.WriteLine("Department:     " + departments[index]);
            Console.WriteLine("Blood Type:     " + bloodType[index]);
            Console.WriteLine("Admitted:       " + admitted[index]);
            Console.WriteLine("Total Visits:   " + visitCount[index]);
        }
        static public bool errorMessage(int index)
        {
            if (index == -1)
            {
                Console.WriteLine("Patient not found");
                return false;
            }
            return true;

        }
        static public  void AdmitPatient(int index)
        {
            if (!admitted[index])
            {
                Console.Write("Doctor Name: ");
                assignedDoctors[index] = Console.ReadLine();

                Console.Write("Enter the admission date: ");
                DateTime admissionDate;

                if (!DateTime.TryParse(Console.ReadLine(), out admissionDate))
                {
                    Console.WriteLine("Invalid date. Using today's date.");
                    admissionDate = DateTime.Now;
                }

                string formattedTime = admissionDate.ToString("yyyy-MM-dd HH:mm");

                admitted[index] = true;
                visitCount[index]++;
                lastVisitDate[index] = admissionDate;

                Console.WriteLine("Patient admitted successfully and assigned to " + assignedDoctors[index]);

                if (visitCount[index] > 1)
                {
                    Console.WriteLine("This patient has been admitted " + visitCount[index] +
                                      " times. Last admission date: " + formattedTime);
                }
                else
                {
                    Console.WriteLine("This is the first time.");
                }
            }
            else
            {
                Console.WriteLine("Patient is already admitted under " + assignedDoctors[index]);
            }
        }
        static public int DischargePatient(string dischargeInput)
        {
          
                if (string.IsNullOrWhiteSpace(dischargeInput))
                {
                    Console.WriteLine("Invalid input.");
                    return -1;
                }

                dischargeInput = dischargeInput.Trim();

                for (int i = 0; i < lastIndex; i++)
                {
                    if (string.Equals(patientNames[i], dischargeInput, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(patientIDs[i], dischargeInput, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!admitted[i])
                        {
                            Console.WriteLine("This patient is not currently admitted.");
                            return i;
                        }

                        double visitCharges = 0;

                        // Discharge date
                        Console.Write("Enter the discharge date: ");
                        DateTime dischargeDate;
                        if (!DateTime.TryParse(Console.ReadLine(), out dischargeDate))
                        {
                            Console.WriteLine("Invalid date. Using today's date.");
                            dischargeDate = DateTime.Now;
                        }
                        lastDischargeDate[i] = dischargeDate;

                        // Days in hospital
                        Console.Write("Enter number of days: ");
                        if (int.TryParse(Console.ReadLine(), out int day) && day > 0)
                        {
                            daysInHospital[i] += day;
                        }
                        else
                        {
                            Console.WriteLine("Invalid days. Skipped.");
                        }

                        // Consultation fee
                        Console.Write("Consultation fee? (yes/no): ");
                        string hasFee = Console.ReadLine()?.Trim().ToLower();
                        if (hasFee == "yes")
                        {
                            Console.Write("Enter fee amount: ");
                            if (double.TryParse(Console.ReadLine(), out double fee) && fee > 0)
                            {
                                billingAmount[i] += fee;
                                visitCharges += fee;
                            }
                            else
                            {
                                Console.WriteLine("Invalid fee.");
                            }
                        }

                        // Medication charges
                        Console.Write("Medication charges? (yes/no): ");
                        string hasMeds = Console.ReadLine()?.Trim().ToLower();
                        if (hasMeds == "yes")
                        {
                            Console.Write("Enter medication amount: ");
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

                        // Print summary
                        Console.WriteLine("----------------------------------------");
                        if (visitCharges > 0)
                        {
                            Console.WriteLine("Total charges added: " + Math.Round(visitCharges, 2).ToString("0.00") + " OMR");
                        }
                        else
                        {
                            Console.WriteLine("No charges recorded.");
                        }

                        // Discharge update
                        admitted[i] = false;
                        assignedDoctors[i] = "";

                        Console.WriteLine("Patient discharged successfully!");
                        return i;
                    }
                }

                Console.WriteLine("Patient not found.");
                return -1;
            }
   
            static public void ShowAdmittedPatients(string keyword)
        {
            if (keyword != null)
                keyword = keyword.Trim();

            bool hasAdmitted = false;
            int admittedCounter = 0;
            double highestBillingAmount = 0;

            for (int i = 0; i < lastIndex; i++)
            {
                if (!admitted[i])
                    continue;

                // filter
                if (!string.IsNullOrEmpty(keyword) &&
                    !string.IsNullOrEmpty(patientNames[i]) &&
                    !patientNames[i].Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                Console.WriteLine("Name: " + patientNames[i] + " | ID: " + patientIDs[i]);

                string displayDiagnosis = string.IsNullOrEmpty(diagnoses[i])
                    ? "N/A"
                    : (diagnoses[i].Length > 15
                        ? diagnoses[i].Substring(0, 15) + "..."
                        : diagnoses[i]);

                Console.WriteLine("Diagnosis: " + displayDiagnosis +
                                  " | Department: " + departments[i] +
                                  " | Doctor: " + assignedDoctors[i]);

                Console.WriteLine("Admitted Since: " + lastVisitDate[i]);

                Console.WriteLine("Billing: " +
                    billingAmount[i].ToString("0.00") + " OMR");

                Console.WriteLine("----------------------------------------");

                hasAdmitted = true;
                admittedCounter++;
                highestBillingAmount = Math.Max(highestBillingAmount, billingAmount[i]);
            }

            if (hasAdmitted)
            {
                Console.WriteLine("Total admitted patients: " + admittedCounter);
                Console.WriteLine("Highest billing: " +
                    highestBillingAmount.ToString("0.00") + " OMR");
            }
            else
            {
                Console.WriteLine("No patients currently admitted.");
            }
        }
        static public void TransferPatientsToNewDoctor(string currentDoctor, string newDoctor)
        {
            if (string.IsNullOrWhiteSpace(currentDoctor) || string.IsNullOrWhiteSpace(newDoctor))
            {
                Console.WriteLine("Doctor names cannot be empty.");
                return;
            }

            // normalize names
            currentDoctor = currentDoctor.Replace("Dr ", "Dr. ");
            newDoctor = newDoctor.Replace("Dr ", "Dr. ");

            // check if same
            if (string.Equals(currentDoctor, newDoctor, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The names of current and new doctors must be different.");
                return;
            }

            bool doctorFound = false;
            int transferCount = 0;

            for (int i = 0; i < lastIndex; i++)
            {
                if (admitted[i] &&
                    string.Equals(assignedDoctors[i], currentDoctor, StringComparison.OrdinalIgnoreCase))
                {
                    doctorFound = true;
                    assignedDoctors[i] = newDoctor;
                    transferCount++;

                    Console.WriteLine("Patient " + patientNames[i] +
                                      " has been transferred to " + newDoctor +
                                      " | Last admitted on: " + lastVisitDate[i]);
                }
            }

            if (!doctorFound)
            {
                Console.WriteLine("No admitted patients found under this doctor.");
            }
            else
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Total patients transferred: " + transferCount);
            }
        }
        static public void ShowMostVisitedPatients()
        {
            Console.WriteLine("Most Visited Patients (by visit count):");
            Console.WriteLine("----------------------------------------");

            bool hasVisits = false;
            int maxVisits = 0;

            // Find the highest visit count
            for (int i = 0; i < lastIndex; i++)
            {
                if (visitCount[i] > maxVisits)
                    maxVisits = visitCount[i];
            }

            if (maxVisits == 0)
            {
                Console.WriteLine("No patient visits recorded yet.");
                return;
            }

            // Display patients with the highest visit count
            for (int i = 0; i < lastIndex; i++)
            {
                if (visitCount[i] == maxVisits)
                {
                    Console.WriteLine("Name: " + patientNames[i] + " | ID: " + patientIDs[i]);
                    Console.WriteLine("Total Visits: " + visitCount[i]);
                    Console.WriteLine("Department: " + departments[i] + " | Doctor: " + assignedDoctors[i]);
                    Console.WriteLine("----------------------------------------");
                    hasVisits = true;
                }
            }

            if (hasVisits)
                Console.WriteLine("Highest visit count: " + maxVisits);
        }
        static public void ShowPatientsByDepartment()
        {
            Console.Write("Enter department name: ");
            string searchDept = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(searchDept))
            {
                Console.WriteLine("Department name cannot be empty.");
                return;
            }

            bool deptFound = false;

            Console.WriteLine("Patients in department '" + searchDept.ToUpper() + "':");
            Console.WriteLine("----------------------------------------");

            for (int i = 0; i < lastIndex; i++)
            {
                if (!string.IsNullOrEmpty(departments[i]) &&
                    departments[i].ToLower().Contains(searchDept.ToLower()))
                {
                    deptFound = true;

                    string status = admitted[i] ? "Admitted" : "Not Admitted";

                    string displayDiagnosis = string.IsNullOrEmpty(diagnoses[i])
                        ? "N/A"
                        : (diagnoses[i].Length > 15 ? diagnoses[i].Substring(0, 15) + "..." : diagnoses[i]);

                    Console.WriteLine("ID: " + patientIDs[i] +
                                      " | Name: " + patientNames[i] +
                                      " | Diagnosis: " + displayDiagnosis +
                                      " | Status: " + status +
                                      " | Blood Type: " + bloodType[i]);
                }
            }

            if (!deptFound)
            {
                Console.WriteLine("No patients found in this department.");
            }
        }
        static public void ShowBillingSummary(int billingIndex)
        {
            if (billingIndex < 0 || billingIndex >= lastIndex)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            Console.WriteLine("----------------------------------------");

            double billingTotal = billingAmount[billingIndex];

            // initialize highest and lowest billing for this patient (single patient context)
            double highestBilling = billingTotal;
            double lowestBilling = billingTotal;

            Console.WriteLine("Billing for " + patientNames[billingIndex] + ": "
                + billingTotal.ToString("0.00") + " OMR"
                + " | Last Visit Date: " + lastVisitDate[billingIndex]
                + " | Total Days: " + daysInHospital[billingIndex]);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Total billing: " + billingTotal.ToString("0.00") + " OMR");
            Console.WriteLine("Lowest billing: " + lowestBilling.ToString("0.00") + " OMR");
            Console.WriteLine("Highest billing: " + highestBilling.ToString("0.00") + " OMR");

            // Apply random discount
            Random rand = new Random();
            int discountPercent = rand.Next(5, 21); // 5% to 20%
            double discountAmount = billingTotal * discountPercent / 100.0;
            double finalAmount = Math.Round(billingTotal - discountAmount, 2);

            Console.WriteLine("Discount applied: " + discountPercent + "%");
            Console.WriteLine("Final amount after discount: " + finalAmount.ToString("0.00") + " OMR");

            Console.WriteLine("----------------------------------------");
        }
        static public bool ConfirmExit()
        {
            Console.WriteLine("Are you sure you want to exit? (yes/no)");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm == "yes")
            {
                Console.WriteLine("Exiting system...");
                Console.WriteLine("Thank you for using the Healthcare Management System!");
                Console.WriteLine("----------------------------------------");
                return true; // user confirmed exit
            }
            else
            {
                Console.WriteLine("Returning to the menu...");
                return false; // user canceled exit
            }

        }
        static public void RegisterDoctor()
        {
            Console.Write("Enter Doctor Name: ");
            string doctorNameInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(doctorNameInput))
            {
                Console.WriteLine("Doctor name cannot be empty. Registration cancelled.");
                return;
            }

            // Capitalize first letter of each word
            doctorNameInput = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorNameInput.ToLower());

            Console.WriteLine("Doctor Name: " + doctorNameInput);

            // Read available slots
            Console.Write("Enter number of available slots: ");
            string inputSlots = Console.ReadLine()?.Trim();

            if (!int.TryParse(inputSlots, out int slotsNumber) || slotsNumber < 1)
            {
                Console.WriteLine("Invalid input. Slots must be a number greater than 0. Doctor not registered.");
                return;
            }

            // Success
            Console.WriteLine("Doctor registered successfully with " + slotsNumber + " available slots.");
        }
        static public void GenerateDoctorSalaryReport()
        {
            // Check if any doctors are registered
            if (lastDoctorIndex < 0)
            {
                Console.WriteLine("No doctors registered in the system.");
                return;
            }

            Console.WriteLine("Doctor Salary Report:");
            Console.WriteLine("----------------------------------------");

            double highestSalary = 0;
            int highestIndex = -1;

            // Loop through all registered doctors
            for (int i = 0; i <= lastDoctorIndex; i++)
            {
                string name = string.IsNullOrEmpty(doctorNames[i]) ? "N/A" : doctorNames[i];
                int visits = doctorVisitCount[i];
                int slots = doctorAvailableSlots[i];

                // Salary formula: base 500 + 50 per visit
                double salary = 500 + (50 * visits);
                salary = Math.Round(salary, 2);

                // Track highest salary
                if (salary > highestSalary)
                {
                    highestSalary = salary;
                    highestIndex = i;
                }

                // Convert salary to string for output
                string salaryStr = Convert.ToString(salary);

                // Print one line per doctor
                Console.WriteLine($"{name} | Visits: {visits} | Available Slots: {slots} | Salary: {salaryStr} OMR");
            }

            Console.WriteLine("----------------------------------------");

            // Print highest earning doctor
            if (highestIndex >= 0)
            {
                Console.WriteLine("Highest earning doctor: " + doctorNames[highestIndex] +
                                  " — " + Convert.ToString(highestSalary) + " OMR");
            }
        }
        static public void AdmitPatientToDoctor(int patientIndex)
        {
            if (patientIndex < 0 || patientIndex >= lastIndex)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            Console.Write("Doctor Name: ");
            string doctorInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(doctorInput))
            {
                Console.WriteLine("Invalid input. Admission cancelled.");
                return;
            }

            // Step 1 — Search doctorNames array (case-insensitive)
            int doctorIndex = -1;
            for (int i = 0; i <= lastDoctorIndex; i++)
            {
                if (!string.IsNullOrEmpty(doctorNames[i]) &&
                    doctorNames[i].ToLower() == doctorInput.ToLower())
                {
                    doctorIndex = i;
                    break;
                }
            }

            if (doctorIndex == -1)
            {
                Console.WriteLine("Doctor not found in the system. Please register the doctor first.");
                return; // exit without admitting patient
            }

            // Step 2 — Check available slots
            if (doctorAvailableSlots[doctorIndex] <= 0)
            {
                Console.WriteLine($"Dr. {doctorNames[doctorIndex]} has no available slots at this time.");
                return; // exit without admitting patient
            }

            // Step 3 — Admit patient and update registry
            admitted[patientIndex] = true;
            assignedDoctors[patientIndex] = doctorNames[doctorIndex];

            // Update doctor stats
            doctorAvailableSlots[doctorIndex] -= 1;
            doctorVisitCount[doctorIndex] += 1;

            Console.WriteLine($"Patient {patientNames[patientIndex]} admitted successfully.");
            Console.WriteLine($"Dr. {doctorNames[doctorIndex]} now has {doctorAvailableSlots[doctorIndex]} slot(s) remaining.");
        }
        static public void UpdateDoctorSlotsOnDischarge(int patientIndex)
        {
            if (patientIndex < 0 || patientIndex >= lastIndex)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            string assignedDoctor = assignedDoctors[patientIndex];

            if (string.IsNullOrEmpty(assignedDoctor))
            {
                Console.WriteLine("No doctor was assigned to this patient.");
                return;
            }

            // Search doctorNames array for a case-insensitive match
            int doctorIndex = -1;
            for (int i = 0; i <= lastDoctorIndex; i++)
            {
                if (!string.IsNullOrEmpty(doctorNames[i]) &&
                    doctorNames[i].ToLower() == assignedDoctor.ToLower())
                {
                    doctorIndex = i;
                    break;
                }
            }

            if (doctorIndex >= 0)
            {
                // Increment available slots
                doctorAvailableSlots[doctorIndex] += 1;
                Console.WriteLine($"Dr. {doctorNames[doctorIndex]} now has {doctorAvailableSlots[doctorIndex]} slot(s) available.");
            }
            else
            {
                // Doctor not found in registry
                Console.WriteLine("Warning: assigned doctor not found in registry. Slots not updated.");
            }
        }
        static void Main(string[] args)
        {

            seedData();

            bool exit = false;

            while (exit == false)
            {

                displayMenue();

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
                        string patientName = Console.ReadLine().Trim();

                        //Console.Write("Patient ID: ");
                        //patientIDs[lastIndex] = Console.ReadLine();

                        Console.Write("Diagnosis: ");
                        string diagnose = Console.ReadLine().Trim();

                        Console.Write("Department: ");
                        string department = Console.ReadLine().Trim();

                        Console.Write("Blood Type: ");
                        string bloodTypes = Console.ReadLine().Trim().ToUpper();

                        //call RegisterPatient Function
                        string patientID = RegisterPatient(patientName, bloodTypes, diagnose, department);

                        Console.WriteLine("Generted Patient ID  :" + patientID);
                        break;

                    case 2: // Admit Patient
                       
                            Console.Write("Enter Patient ID or Name: ");
                            string admitInput = Console.ReadLine().ToLower().Trim();

                            int searchFound2 = searchPatient(admitInput);

                            // check if patient exists
                            if (!errorMessage(searchFound2))
                            {
                                return;
                            }

                            //  call the function here for admit the patient
                            AdmitPatient(searchFound2);
                        //here after adding doctor to the admitted patient
                        AdmitPatientToDoctor(searchFound2);


                        break;

                    case 3: // Discharge Patient

                        Console.Write("Enter Patient ID or Name: ");
                        string dischargeInput = Console.ReadLine()?.Trim();

                        int searchFound3 = searchPatient(dischargeInput);

                        // Check if patient exists
                        if (!errorMessage(searchFound3))
                        {
                            return; // stop execution safely
                        }

                        else
                        {
                            // Call DischargePatient function for output
                            DischargePatient(dischargeInput);
                            //call after dischagre for patient
                        }
                            UpdateDoctorSlotsOnDischarge(searchFound3);
                     
                        break;

                    case 4: // Search Patient
                        Console.Write("Enter Patient ID or Name: ");
                        string searchInput = Console.ReadLine().Trim();

                        int searchFound = searchPatient(searchInput);

                        // check if found
                        if (!errorMessage(searchFound))
                        {
                            return; // stop execution safely
                        }

                        // print details
                        printPatientDetails(searchFound);

                        break;

                    case 5: // List all Admitted Patients
                        Console.WriteLine("Currently Admitted Patients:");
                        Console.WriteLine("----------------------------------------");

                        Console.Write("Enter Patient ID or Name to search (or press Enter to skip): ");
                        string input = Console.ReadLine();

                        // here search if patient if found
                        if (!string.IsNullOrWhiteSpace(input))
                        {
                            int index = searchPatient(input);

                            if (!errorMessage(index))
                                return;

                            printPatientDetails(index);
                        }

                        //
                        Console.WriteLine("Filtered Admitted Patients:");
                        Console.Write("Filter by name keyword (press Enter to skip): ");
                        string keyword = Console.ReadLine();

                        ShowAdmittedPatients(keyword);


                        break;

                    case 6: // Transfer Patient to Another Doctor
                        Console.Write("Enter current doctor name: ");
                        string currentDoctor = Console.ReadLine()?.Trim();

                        Console.Write("Enter new doctor name: ");
                        string newDoctor = Console.ReadLine()?.Trim();

                        Console.Write("Enter new doctor name: ");
                        
                            if (!string.IsNullOrWhiteSpace(currentDoctor) && !string.IsNullOrWhiteSpace(newDoctor))
                            {
                                // Optional: show all patients of current doctor before transfer
                                Console.WriteLine("\nPatients under Dr. " + currentDoctor + ":");
                                for (int i = 0; i < lastIndex; i++)
                                {
                                    if (admitted[i] &&
                                        string.Equals(assignedDoctors[i], currentDoctor, StringComparison.OrdinalIgnoreCase))
                                    {
                                        printPatientDetails(i);
                                    }
                                }

                                // Transfer patients
                                TransferPatientsToNewDoctor(currentDoctor, newDoctor);
                            }
                            else
                            {
                                Console.WriteLine("Both doctor names must be provided.");
                            }
                           

                        Console.WriteLine("Dortor not found.");

                        break;
                    case 7: // View Most Visited Patients
                        Console.WriteLine("Most Visited Patients (by visit count):");
                        Console.WriteLine("----------------------------------------");
                        ShowMostVisitedPatients();
                        break;


                    case 8: // Search Patients by Department
                        Console.Write("Enter department name: ");
                        string searchDept = Console.ReadLine();

                        //calling this defined function
                        ShowPatientsByDepartment();

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
                        double billingTotal = 0;
                        double highestBilling = 0;
                        double lowestBilling = 0;
                        bool firstMatch = true; // for initializing highest and lowest billing

                        if (billingOption == 1)
                        {
                            // System-wide total
                            double totalBilling = 0;

                            for (int i = 0; i <= lastIndex; i++)
                            {
                                totalBilling += billingAmount[i];
                            }

                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Total billing collected: " + Math.Round(totalBilling, 2) + " OMR");
                        }
                        else if (billingOption == 2)
                        {
                            // Individual patient
                            Console.Write("Enter Patient ID or Name: ");
                            string billingInput = Console.ReadLine().Trim();

                            int billingIndex = searchPatient(billingInput);

                            if (!errorMessage(billingIndex))
                            {
                                break;
                            }
                            else
                            {
                           //calling the function 
                                    ShowBillingSummary(billingIndex);
                        
                            }
                        } 

                            break;

                   

                
                    case 10://Add Doctor
                        //calling this function
                        RegisterDoctor();
                        break;
                    case 11://Doctor Salary Report
                        //here calling defined function to work 
                        GenerateDoctorSalaryReport();
                        break;
                    case 12: // Exit
                             //ask user if want to exit the prgram
                        bool exitt = false;

                        if (ConfirmExit())
                        {
                            exitt = true;
                            // exit the main loop or application
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

         
