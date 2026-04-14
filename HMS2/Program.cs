using System;
using System.Reflection;

namespace HMS2
{
    internal class Program
    { 
        // global storage
        const int MAX_PATIENTS = 100;
        const int MAX_DOCTORS = 50;
        static string[] patientNames = new string[MAX_PATIENTS];
        static string[] patientIDs = new string[MAX_PATIENTS];
        static string[] diagnoses = new string[MAX_PATIENTS];
        static bool[] admitted = new bool[MAX_PATIENTS];
        static string[] assignedDoctors = new string[MAX_PATIENTS];
        static string[] departments = new string[MAX_PATIENTS];
        static int[] visitCount = new int[MAX_PATIENTS];
        static double[] billingAmount = new double[MAX_PATIENTS];
        static DateTime[] lastVisitDate = new DateTime[MAX_PATIENTS];
        static DateTime[] lastDischargeDate = new DateTime[MAX_PATIENTS];
        static int[] daysInHospital = new int[MAX_PATIENTS];
        static string[] bloodType = new string[MAX_PATIENTS];
        static string[] doctorNames = new string[MAX_DOCTORS];
        static int[] doctorAvailableSlots = new int[MAX_DOCTORS];
        static int[] doctorVisitCount = new int[MAX_DOCTORS];
        static int lastIndex = -1;
        static int lastDoctorIndex = -1;

/////////////////////////////////////////////////////////////////////////////////
        //defined functions:
        /// <summary>
        /// Initializes the system with default sample data for patients and doctors
        /// to allow testing without manual input.
        /// </summary>
        /// <param name="none">This method does not take any parameters.</param>
        /// <returns>None</returns>
        static public void seedData()
        {

            //patient 1
            lastIndex++;
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

            //Doctor 1
            doctorNames[++lastDoctorIndex] = "Dr. Noor";
            doctorAvailableSlots[lastDoctorIndex] = 5;
            doctorVisitCount[lastDoctorIndex] = 0;


            //patient 2
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

            //Doctor 2
            doctorNames[++lastDoctorIndex] = "Dr. Salem";
            doctorAvailableSlots[lastDoctorIndex] = 3;
            doctorVisitCount[lastDoctorIndex] = 0;


            //patient 3
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

            //Doctor 3

            doctorNames[++lastDoctorIndex] = "Dr. Hana ";
            doctorAvailableSlots[lastDoctorIndex] = 8;
            doctorVisitCount[lastDoctorIndex] = 0;
          

        }

        // <summary>
        /// Displays the main menu options for the Hospital Management System,
        /// allowing the user to navigate through available features.
        /// </summary>
        /// <param name="none">This method does not take any parameters.</param>
        /// <returns>None</returns>
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
        /// <summary>
        /// Registers a new patient in the system and stores their details in arrays.
        /// </summary>
        /// <param name="name">Patient full name</param>
        /// <param name="blood">Blood type of patient</param>
        /// <param name="department">Department assigned</param>
        /// <param name="diagnosis">Patient diagnosis</param>
        /// <returns>Generated Patient ID</returns>
        static public string RegisterPatient(string name, string blood, string department, string diagnosis)
        {
            Console.WriteLine("Register New Patient...");

           
            //  check capacity
          
            if (lastIndex >= MAX_PATIENTS - 1)
            {
                Console.WriteLine("Error: Patient storage is full. Cannot register more patients.");
                return null;
            }

            int index = lastIndex; // safe index

            // Store data
            patientNames[index] = name;
            diagnoses[index] = diagnosis;
            departments[index] = department;
            bloodType[index] = blood.ToUpper();

            patientIDs[index] = "P" + (index + 1).ToString("D3");

            admitted[index] = false;
            assignedDoctors[index] = "";
            visitCount[index] = 0;
            billingAmount[index] = 0;

            lastVisitDate[index] = DateTime.Now;
            lastDischargeDate[index] = DateTime.Now;
            daysInHospital[index] = 0;

            Console.WriteLine("Patient registered successfully");

            lastIndex++; // move to next slot

            return patientIDs[index];
        }
        /// <summary>
        /// Searches for a patient by name or ID and returns the index in the patient array.
        /// </summary>
        /// <param name="searchInput">The patient name or patient ID to search for</param>
        /// <returns>
        /// Returns the index of the patient if found; otherwise returns -1.
        /// </returns>
        static public int searchPatient(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
                return -1;

            searchInput = searchInput.ToLower().Trim();

            for (int i = 0; i < lastIndex; i++)
            {
                if (!string.IsNullOrEmpty(patientNames[i]) &&
                    patientNames[i].ToLower() == searchInput)
                    return i;

                if (!string.IsNullOrEmpty(patientIDs[i]) &&
                    patientIDs[i].ToLower() == searchInput)
                    return i;
            }

            return -1;
        }
        /// <summary>
        /// Prints all stored details of a specific patient, including personal information,
        /// medical details, admission status, doctor assignment, and billing-related data.
        /// </summary>
        /// <param name="index">The index of the patient in the system arrays</param>
        /// <returns>None</returns>
        static public void printPatientDetails(int index)
        {
            Console.WriteLine("Patient Details:");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Name: " + patientNames[index]);
            Console.WriteLine("ID: " + patientIDs[index]);
            Console.WriteLine("Department: " + departments[index]);
            Console.WriteLine("Diagnosis: " + diagnoses[index]);
            Console.WriteLine("Blood Type: " + bloodType[index]);

            Console.WriteLine("Visit Count: " + visitCount[index]);          
            Console.WriteLine("Total Billing Amount: " + billingAmount[index]); 

            string status = admitted[index] ? "Admitted" : "Not Admitted";
            Console.WriteLine("Status: " + status);

            if (admitted[index])
                Console.WriteLine("Doctor: " + assignedDoctors[index]);

            Console.WriteLine("----------------------------------------");
        }
        /// <summary>
        /// Validates the given patient index and displays an error message if the patient is not found.
        /// </summary>
        /// <param name="index">The index returned from a search operation</param>
        /// <returns>
        /// Returns true if the index is valid (patient found), otherwise returns false and prints an error message.
        /// </returns>
        static public bool errorMessage(int index)
        {
            if (index == -1)
            {
                Console.WriteLine("Patient not found");
                return false;
            }
            return true;

        }
        /// <summary>
        /// Admits a patient into the hospital system by assigning a doctor,
        /// updating admission status, and initializing admission-related data.
        /// </summary>
        /// <param name="index">The index of the patient to be admitted</param>
        /// <returns>None</returns>
        static public void AdmitPatient(int index)
        {
            // Safety check
            if (index < 0 || index >= lastIndex)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            // Already admitted check
            if (admitted[index])
            {
                Console.WriteLine("Patient is already admitted under " + assignedDoctors[index]);
                return;
            }

       
            //  Doctor validation
         
            Console.Write("Doctor Name: ");
            string doctorInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(doctorInput))
            {
                Console.WriteLine("Invalid doctor name.");
                return;
            }

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
                return;
            }

        
            // Slot check
        
            if (doctorAvailableSlots[doctorIndex] <= 0)
            {
                Console.WriteLine("Dr. " + doctorNames[doctorIndex] +
                                  " has no available slots at this time.");
                return;
            }

         
            // Admission date
         
            Console.Write("Enter the admission date: ");
            DateTime admissionDate;

            if (!DateTime.TryParse(Console.ReadLine(), out admissionDate))
            {
                Console.WriteLine("Invalid date. Using today's date.");
                admissionDate = DateTime.Now;
            }

            string formattedTime = admissionDate.ToString("yyyy-MM-dd HH:mm");

          
            // Admit patient
           
            admitted[index] = true;

            //  store registry doctor name 
            assignedDoctors[index] = doctorNames[doctorIndex];

            visitCount[index]++;
            lastVisitDate[index] = admissionDate;

            // Doctor updates
            doctorAvailableSlots[doctorIndex]--;
            doctorVisitCount[doctorIndex]++;

          
            Console.WriteLine("Patient admitted successfully and assigned to " + assignedDoctors[index]);

            Console.WriteLine("Dr. " + doctorNames[doctorIndex] +
                              " now has " + doctorAvailableSlots[doctorIndex] +
                              " slot(s) remaining.");

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
        /// <summary>
        /// Discharges a patient from the hospital, updates medical records,
        /// calculates charges, restores doctor availability, and finalizes billing updates.
        /// </summary>
        /// <param name="i">The index of the patient in the system arrays</param>
        /// <returns>
        /// Returns the patient index if discharge is successful, or -1 if the index is invalid.
        /// </returns>
        static public int DischargePatient(int i)
        {
            if (i < 0 || i >= lastIndex)
            {
                Console.WriteLine("Invalid patient index.");
                return -1;
            }

            if (!admitted[i])
            {
                Console.WriteLine("This patient is not currently admitted.");
                return i;
            }

            double visitCharges = 0;

            // Discharge date
            Console.Write("Enter the discharge date: ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dischargeDate))
            {
                Console.WriteLine("Invalid date. Using today's date.");
                dischargeDate = DateTime.Now;
            }
            lastDischargeDate[i] = dischargeDate;

            // Days
            Console.Write("Enter number of days: ");
            if (int.TryParse(Console.ReadLine(), out int day) && day > 0)
                daysInHospital[i] += day;
            else
                Console.WriteLine("Invalid days. Skipped.");

            // Consultation fee
            Console.Write("Consultation fee? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() == "yes")
            {
                Console.Write("Enter fee amount: ");
                if (double.TryParse(Console.ReadLine(), out double fee) && fee > 0)
                {
                    billingAmount[i] += fee;
                    visitCharges += fee;
                }
            }

            // Medication
            Console.Write("Medication charges? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() == "yes")
            {
                Console.Write("Enter medication amount: ");
                if (double.TryParse(Console.ReadLine(), out double meds) && meds > 0)
                {
                    billingAmount[i] += meds;
                    visitCharges += meds;
                }
            }

            // Restore doctor slot
            string doctorName = assignedDoctors[i];
            int doctorIndex = -1;

            for (int d = 0; d <= lastDoctorIndex; d++)
            {
                if (!string.IsNullOrEmpty(doctorNames[d]) &&
                    doctorNames[d].Equals(doctorName, StringComparison.OrdinalIgnoreCase))
                {
                    doctorIndex = d;
                    break;
                }
            }

            if (doctorIndex != -1)
            {
                doctorAvailableSlots[doctorIndex]++;
                Console.WriteLine("Dr. " + doctorNames[doctorIndex] +
                                  " now has " + doctorAvailableSlots[doctorIndex] + " slot(s) available.");
            }

            // Summary
            Console.WriteLine("----------------------------------------");

            if (visitCharges > 0)
                Console.WriteLine("Total charges added: " + visitCharges.ToString("0.00") + " OMR");
            else
                Console.WriteLine("No charges recorded.");

            // Discharge
            admitted[i] = false;
            assignedDoctors[i] = "";

            Console.WriteLine("Patient discharged successfully!");

            return i;
        }
        /// <summary>
        /// Displays a list of admitted patients filtered by a search keyword (name or ID).
        /// Shows only patients whose admission status is currently active.
        /// </summary>
        /// <param name="keyword">The patient name or ID used to filter admitted patients</param>
        /// <returns>None</returns>
        static public void ShowAdmittedPatients(string keyword)
        {
            if (keyword != null)
                keyword = keyword.Trim();

            bool hasAdmitted = false;
            int admittedCounter = 0;
            double highestBillingAmount = 0;

            for (int i = 0; i <= lastIndex; i++)
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
        /// <summary>
        /// Transfers all admitted patients from one doctor to another and updates their assigned doctor records.
        /// </summary>
        /// <param name="currentDoctor">The name of the current doctor assigned to patients</param>
        /// <param name="newDoctor">The name of the new doctor to reassign patients to</param>
        /// <returns>None</returns>
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

            if (string.Equals(currentDoctor, newDoctor, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The names of current and new doctors must be different.");
                return;
            }

            bool doctorFound = false;
            int transferCount = 0;

            for (int i = 0; i <= lastIndex; i++)
            {
                if (admitted[i] &&
                    string.Equals(assignedDoctors[i], currentDoctor, StringComparison.OrdinalIgnoreCase))
                {
                    doctorFound = true;

                 
                    // update doctor slots
                  

                    int oldDoctorIndex = -1;
                    int newDoctorIndex = -1;

                    for (int d = 0; d <= lastDoctorIndex; d++)
                    {
                        if (doctorNames[d].Equals(currentDoctor, StringComparison.OrdinalIgnoreCase))
                            oldDoctorIndex = d;

                        if (doctorNames[d].Equals(newDoctor, StringComparison.OrdinalIgnoreCase))
                            newDoctorIndex = d;
                    }

                    // old doctor +1 slot
                    if (oldDoctorIndex != -1)
                        doctorAvailableSlots[oldDoctorIndex]++;

                    // new doctor -1 slot
                    if (newDoctorIndex != -1 && doctorAvailableSlots[newDoctorIndex] > 0)
                        doctorAvailableSlots[newDoctorIndex]--;
                    else if (newDoctorIndex != -1)
                        Console.WriteLine("Warning: New doctor has no available slots left.");

                    // transfer patient
                    assignedDoctors[i] = newDoctor;
                    transferCount++;

                    Console.WriteLine("Patient " + patientNames[i] +
                                      " has been transferred to " + newDoctor +
                                      " | Last admitted on: " + lastVisitDate[i]);
                    break;
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
        /// <summary>
        /// Displays the patients with the highest visit counts in the system.
        /// Helps identify the most frequently visiting patients.
        /// </summary>
        /// <param name="none">This method does not take any parameters.</param>
        /// <returns>None</returns>
        
       static public void ShowMostVisitedPatients()
        {
            Console.WriteLine("Most Visited Patients (by visit count):");
            Console.WriteLine("----------------------------------------");

            if (lastIndex == 0)
            {
                Console.WriteLine("No patients available.");
                return;
            }

            // Copy arrays (so original data is not modified)
            int[] visits = new int[lastIndex];
            string[] names = new string[lastIndex];
            string[] ids = new string[lastIndex];
            string[] depts = new string[lastIndex];
            string[] doctors = new string[lastIndex];

            for (int i = 0; i <= lastIndex; i++)
            {
                visits[i] = visitCount[i];
                names[i] = patientNames[i];
                ids[i] = patientIDs[i];
                depts[i] = departments[i];
                doctors[i] = assignedDoctors[i];
            }

            //  Bubble Sort (Descending)
            for (int i = 0; i <= lastIndex - 1; i++)
            {
                for (int j = 0; j <= lastIndex - i - 1; j++)
                {
                    if (visits[j] < visits[j + 1])
                    {
                        // swap visits
                        int tempVisit = visits[j];
                        visits[j] = visits[j + 1];
                        visits[j + 1] = tempVisit;

                        // swap related data
                        string temp;

                        temp = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = temp;

                        temp = ids[j];
                        ids[j] = ids[j + 1];
                        ids[j + 1] = temp;

                        temp = depts[j];
                        depts[j] = depts[j + 1];
                        depts[j + 1] = temp;

                        temp = doctors[j];
                        doctors[j] = doctors[j + 1];
                        doctors[j + 1] = temp;
                    }
                }
            }

            // Display sorted results
            bool hasVisits = false;

            for (int i = 0; i <= lastIndex; i++)
            {
                if (visits[i] == 0) break;

                Console.WriteLine("Name: " + names[i] + " | ID: " + ids[i]);
                Console.WriteLine("Total Visits: " + visits[i]);
                Console.WriteLine("Department: " + depts[i] + " | Doctor: " + doctors[i]);
                Console.WriteLine("----------------------------------------");

                hasVisits = true;
            }

            if (!hasVisits)
                Console.WriteLine("No patient visits recorded yet.");
        }
        /// <summary>
        /// Displays all patients grouped or filtered by their assigned department.
        /// Helps view patient distribution across hospital departments.
        /// </summary>
        /// <param name="none">This method does not take any parameters.</param>
        /// <returns>None</returns>
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

            for (int i = 0; i <= lastIndex; i++)
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
        /// <summary>
        /// Displays the billing summary for a specific patient, including total billing,
        /// visit details, and system-wide highest and lowest billing values.
        /// </summary>
        /// <param name="billingIndex">The index of the patient in the billing system</param>
        /// <returns>None</returns>
        static public void ShowBillingSummary(int billingIndex)
        {
            if (billingIndex < 0 || billingIndex > lastIndex)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            Console.WriteLine("----------------------------------------");

            double billingTotal = billingAmount[billingIndex];

            double highestBilling = billingAmount[0];
            double lowestBilling = billingAmount[0];

            for (int i = 1; i <= lastIndex; i++)
            {
                if (billingAmount[i] > highestBilling)
                    highestBilling = billingAmount[i];

                if (billingAmount[i] < lowestBilling)
                    lowestBilling = billingAmount[i];
            }

            Console.WriteLine("Billing for " + patientNames[billingIndex] + ": "
                + billingTotal.ToString("0.00") + " OMR"
                + " | Last Visit Date: " + lastVisitDate[billingIndex]
                + " | Total Days: " + daysInHospital[billingIndex]);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Total billing: " + billingTotal.ToString("0.00") + " OMR");
            Console.WriteLine("Lowest billing (all patients): " + lowestBilling.ToString("0.00") + " OMR");
            Console.WriteLine("Highest billing (all patients): " + highestBilling.ToString("0.00") + " OMR");

            Console.WriteLine("----------------------------------------");
        }
        /// <summary>
        /// Applies a random discount to the selected patient's total billing amount
        /// and displays the final payable amount after discount.
        /// </summary>
        /// <param name="billingIndex">The index of the patient in the billing system</param>
        /// <returns>None</returns>
        static public void ApplyBillingDiscount(int billingIndex)
        {
            if (billingIndex < 0 || billingIndex > lastIndex)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            double billingTotal = billingAmount[billingIndex];

            Random rand = new Random();
            int discountPercent = rand.Next(5, 21);

            double discountAmount = billingTotal * discountPercent / 100.0;
            double finalAmount = Math.Round(billingTotal - discountAmount, 2);

            Console.WriteLine("Discount applied: " + discountPercent + "%");
            Console.WriteLine("Final amount after discount: " + finalAmount.ToString("0.00") + " OMR");
        }
        /// <summary>
        /// Prompts the user to confirm whether they want to exit the system.
        /// </summary>
        /// <param name="none">This method does not take any parameters.</param>
        /// <returns>
        /// Returns true if the user confirms exit, otherwise returns false.
        /// </returns>
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
        /// <summary>
        /// Registers a new doctor in the system by storing their name and available slots,
        /// and initializes their visit count.
        /// </summary>
        /// <param name="none">This method does not take any parameters.</param>
        /// <returns>None</returns>
        static public void RegisterDoctor()
        {
          
            //check capacity
          
            if (lastDoctorIndex >= MAX_DOCTORS - 1)
            {
                Console.WriteLine("Error: Doctor storage is full. Cannot register more doctors.");
                return;
            }

            Console.Write("Enter Doctor Name: ");
            string doctorNameInput = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(doctorNameInput))
            {
                Console.WriteLine("Doctor name cannot be empty. Registration cancelled.");
                return;
            }

            doctorNameInput =
                System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                    doctorNameInput.ToLower()
                );

            Console.Write("Enter number of available slots: ");
            string inputSlots = Console.ReadLine()?.Trim();

            if (!int.TryParse(inputSlots, out int slotsNumber) || slotsNumber < 1)
            {
                Console.WriteLine("Invalid input. Slots must be > 0.");
                return;
            }

            // Store doctor safely
            doctorNames[++lastDoctorIndex] = doctorNameInput;
            doctorAvailableSlots[lastDoctorIndex] = slotsNumber;
            doctorVisitCount[lastDoctorIndex] = 0;

            Console.WriteLine("Doctor registered successfully.");
        }
        /// <summary>
        /// Generates a salary report for all doctors based on their visit counts and/or system-defined salary rules.
        /// Displays calculated salary information for each doctor.
        /// </summary>
        /// <param name="none">This method does not take any parameters.</param>
        /// <returns>None</returns>
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

                // Salary formula: base 300 + (15 * visits)
                double salary = 300 + (15 * visits);
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

       /// <summary>
       /// /////////////////////////////////////////////////////////
       /// </summary>
       /// <param name="args"></param>
        
        //Main Method:
        static void Main(string[] args)
        {
            //calling to seed data function
            seedData();

            bool exit = false;

            while (exit == false)
            {
                //display menue
                displayMenue();

                int choice = 0;

                try
                {
                    //input user to choose option from the menue
                    choice = int.Parse(Console.ReadLine());
                }
                //handle the error
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 10.");
                }

                switch (choice)
                {
                    case 1: // Register New Patient

                        Console.Write("Patient Name: ");
                        string patientName = Console.ReadLine()?.Trim() ?? string.Empty;

                        Console.Write("Diagnosis: ");
                        string diagnose = Console.ReadLine()?.Trim() ?? string.Empty;

                        Console.Write("Department: ");
                        string department = Console.ReadLine()?.Trim() ?? string.Empty;

                        Console.Write("Blood Type: ");
                        string bloodTypes = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                        // validation
                        if (string.IsNullOrWhiteSpace(patientName) ||
                            string.IsNullOrWhiteSpace(diagnose) ||
                            string.IsNullOrWhiteSpace(department))
                        {
                            Console.WriteLine("Error: Name, Diagnosis, and Department cannot be empty.");
                            break;
                        }

                        // call function
                        string patientID = RegisterPatient(patientName, bloodTypes, department, diagnose);

                        if (patientID != null)
                        {
                            Console.WriteLine("Generated Patient ID: " + patientID);
                        }
                        break;

                    case 2: // Admit Patient

                        Console.Write("Enter Patient ID or Name: ");
                        string admitInput = Console.ReadLine()?.Trim() ?? string.Empty;

                        //search the patient
                        int patientIndex = searchPatient(admitInput);

                        // check if patient exists
                        if (!errorMessage(patientIndex))
                        {
                            break;
                        }

                        // call the refined AdmitPatient function
                        AdmitPatient(patientIndex);

                        break;

                    case 3: // Discharge Patient

                        Console.Write("Enter Patient ID or Name: ");
                        string dischargeInput = Console.ReadLine()?.Trim() ?? string.Empty;

                        //search if patient found
                        int searchFound3 = searchPatient(dischargeInput);

                        //if patient is not found
                        if (!errorMessage(searchFound3))
                        {
                            break;
                        }
                        //  pass index instead of string
                        DischargePatient(searchFound3);

                        break;
                 

                    case 4: // Search Patient
                        Console.Write("Enter Patient ID or Name: ");
                        string searchInput = Console.ReadLine().Trim() ?? string.Empty;

                        int searchFound = searchPatient(searchInput);

                        // check if found
                        if (!errorMessage(searchFound))
                        {
                           break; // stop execution safely
                        }

                        // print details of patients
                        printPatientDetails(searchFound);

                        break;

                    case 5: // List all Admitted Patients
                        Console.WriteLine("Currently Admitted Patients:");
                        Console.WriteLine("----------------------------------------");

                        Console.Write("Enter Patient ID or Name to search (or press Enter to skip): ");
                        string input = Console.ReadLine() ?? string.Empty;

                        // here search if patient if found
                        if (!string.IsNullOrWhiteSpace(input))
                        {
                            int index = searchPatient(input);

                            if (!errorMessage(index))
                                break;

                            printPatientDetails(index);
                        }

                        //
                        Console.WriteLine("Filtered Admitted Patients:");
                        Console.Write("Filter by name keyword (press Enter to skip): ");
                        string keyword = Console.ReadLine() ?? string.Empty;

                        //show all admitted patients
                        ShowAdmittedPatients(keyword);

                        break;

                
                    case 6: // Transfer Patient to Another Doctor
                  
                        Console.Write("Enter current doctor name: ");
                        string currentDoctor = Console.ReadLine()?.Trim() ?? string.Empty;

                        Console.Write("Enter new doctor name: ");
                        string newDoctor = Console.ReadLine()?.Trim() ?? string.Empty;

                        if (!string.IsNullOrWhiteSpace(currentDoctor) &&
                            !string.IsNullOrWhiteSpace(newDoctor))
                        {
                            TransferPatientsToNewDoctor(currentDoctor, newDoctor);
                        }
                        else
                        {
                            Console.WriteLine("Both doctor names must be provided.");
                        }
                    
                        break;
              
                    case 7: // View Most Visited Patients
                        Console.WriteLine("Most Visited Patients (by visit count):");
                        Console.WriteLine("----------------------------------------");
                        ShowMostVisitedPatients();
                        break;

                    case 8: // Search Patients by Department
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
                            break; // exit or loop back to ask again
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

                            for (int i = 0; i < lastIndex; i++)
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
                            string billingInput = Console.ReadLine().Trim() ?? string.Empty;

                            //search of the patient
                            int billingIndex = searchPatient(billingInput);

                            //check if patient is not found
                            if (!errorMessage(billingIndex))
                            {
                                break;
                            }
                            else
                            {
                                //to display summary of billing
                                ShowBillingSummary(billingIndex);
                                //to Apply Billing Discount
                                ApplyBillingDiscount(billingIndex);

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
            
                    case 12://exist the program
                        if (ConfirmExit())
                        {
                            exit = true;   
                        }
                        break;

                    // Handles any menu choice that does not match defined cases
                    // Displays an error message and returns control to the menu
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                ////////////////////////The End of the switch/////////////////////////////

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            }
        }
    }
