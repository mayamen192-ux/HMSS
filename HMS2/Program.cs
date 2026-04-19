using System;
using System.Reflection;

namespace HMS2
{
    internal class Program
    {
        // global storage
        static List<string> patientNames = new List<string>();
        static List<string> patientIDs = new List<string>();
        static List<string> diagnoses = new List<string>();
        static List<bool> admitted = new List<bool>();
        static List<string> assignedDoctors = new List<string>();
        static List<string> departments = new List<string>();
        static List<int> visitCount = new List<int>();
        static List<double> billingAmount = new List<double>();
        static List<DateTime> lastVisitDate = new List<DateTime>();
        static List<DateTime> lastDischargeDate = new List<DateTime>();
        static List<int> daysInHospital = new List<int>();
        static List<string> bloodType = new List<string>();
        static List<string> doctorNames = new List<string>();
        static List<int> doctorAvailableSlots = new List<int>();
        static List<int> doctorVisitCount = new List<int>();
        static Random rand = new Random();

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
            patientNames.Add("Ali Hassan");
            patientIDs.Add("P001");
            diagnoses.Add("Flu");
            departments.Add("General");
            bloodType.Add("A+");
            admitted.Add(false);
            assignedDoctors.Add("");
            visitCount.Add(2);
            billingAmount.Add(0);
            lastVisitDate.Add(DateTime.Parse("2025-01-10"));
            lastDischargeDate.Add(DateTime.Parse("2025-01-15"));
            daysInHospital.Add(5);


            //Doctor 1
            doctorNames.Add("Dr. Noor");
            doctorAvailableSlots.Add(5);
            doctorVisitCount.Add(0);


            //patient 2
            patientNames.Add("Sara Ahmed");
            patientIDs.Add("P002");
            diagnoses.Add("Flu");
            departments.Add("Orthopedics");
            bloodType.Add("O-");
            admitted.Add(true);
            assignedDoctors.Add("Dr. Noor");
            visitCount.Add(4);
            billingAmount.Add(0);
            lastVisitDate.Add(DateTime.Parse("2025-03-02"));
            lastDischargeDate.Add(DateTime.Parse("2025-03-04"));
            daysInHospital.Add(1);


            //Doctor 2
            doctorNames.Add("Dr. Salem");
            doctorAvailableSlots.Add(3);
            doctorVisitCount.Add(0);

            //patient 3
            patientNames.Add("Omar Khalid");
            patientIDs.Add("P003");
            diagnoses.Add("Diabetes");
            departments.Add("Cardiology");
            bloodType.Add("B+");
            admitted.Add(false);
            assignedDoctors.Add("");
            visitCount.Add(1);
            billingAmount.Add(0);
            lastVisitDate.Add(DateTime.Parse("2024-12-20"));
            lastDischargeDate.Add(DateTime.Parse("2024-12-28"));
            daysInHospital.Add(8);


            //Doctor 3
            doctorNames.Add("Dr. Hana");
            doctorAvailableSlots.Add(8);
            doctorVisitCount.Add(0);


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
        /// Registers a new patient by adding their details to the system lists.
        /// </summary>
        /// <param name="name">List containing patient names.</param>
        /// <param name="diagnosis">List containing patient diagnoses.</param>
        /// <param name="department">List containing patient departments.</param>
        /// <param name="blood">List containing patient blood types.</param>
        /// <returns>
        /// A List of strings representing the updated patient names after registration.
        /// </returns>
        static public List<string> RegisterPatient(
    List<string> name,
    List<string> diagnosis,
     List<string> department,
    List<string> blood

    )
        {
            Console.WriteLine("Register New Patient...");

            List<string> newIDs = new List<string>();

            for (int i = 0; i < name.Count; i++)
            {
                string newID = "P" + (patientNames.Count + 1).ToString("D3");

                patientNames.Add(name[i]);
                diagnoses.Add(diagnosis[i]);
                departments.Add(department[i]);
                bloodType.Add(blood[i].ToUpper());

                patientIDs.Add(newID);

                admitted.Add(false);
                assignedDoctors.Add("");
                visitCount.Add(0);
                billingAmount.Add(0);

                lastVisitDate.Add(DateTime.Now);
                lastDischargeDate.Add(DateTime.Now);
                daysInHospital.Add(0);

                newIDs.Add(newID);
            }

            Console.WriteLine("Patients registered successfully");

            return newIDs;
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

            string input = searchInput.Trim();

            for (int i = 0; i < patientNames.Count; i++)
            {
                if (string.Equals(patientNames[i], input, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(patientIDs[i], input, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }



        /// <summary>
        /// Prints all stored details of a patient, including personal information,
        /// medical data, billing, and admission status.
        /// </summary>
        /// <param name="index">
        /// The index of the patient in the system lists.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It outputs patient details directly to the console.
        /// </returns>
        static public void printPatientDetails(int index)
        {
            // Safety check for List bounds
            if (index < 0 || index >= patientNames.Count)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            Console.WriteLine("Patient Details:");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Name: " + patientNames[index]);
            Console.WriteLine("ID: " + patientIDs[index]);
            Console.WriteLine("Department: " + departments[index]);
            Console.WriteLine("Diagnosis: " + diagnoses[index]);
            Console.WriteLine("Blood Type: " + bloodType[index]); // ensure List name is consistent

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
        /// Admits a patient into the hospital system and assigns them to a doctor if available.
        /// Updates visit counts, doctor availability, and admission records.
        /// </summary>
        /// <param name="index">
        /// The index of the patient in the patient list to be admitted.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It prints admission status and updates system records.
        /// </returns>
        static public void AdmitPatient(int index)
        {
            // Safety check using List.Count
            if (index < 0 || index >= patientNames.Count)
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

            // Doctor validation
            Console.Write("Doctor Name: ");
            string doctorInput = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(doctorInput))
            {
                doctorInput = System.Globalization.CultureInfo.CurrentCulture.TextInfo
                    .ToTitleCase(doctorInput.ToLower());
            }

            int doctorIndex = -1;

            // Use doctorNames.Count instead of lastDoctorIndex
            for (int i = 0; i < doctorNames.Count; i++)
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

            // Admit patient (same logic, works with List)
            admitted[index] = true;
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
        /// Discharges a patient from the hospital system, updates billing, records discharge details,
        /// updates hospital stay duration, and restores doctor availability slots.
        /// </summary>
        /// <param name="i">
        /// The index of the patient to be discharged in the system lists.
        /// </param>
        /// <returns>
        /// Returns the same patient index if the operation is successful or if the patient is already not admitted;
        /// returns -1 if the provided index is invalid.
        /// </returns>
        static public int DischargePatient(int i)
        {
            //  List boundary check
            if (i < 0 || i >= patientNames.Count)
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

            // Days in hospital
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

            //  List.Count instead of lastDoctorIndex
            for (int d = 0; d < doctorNames.Count; d++)
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
                                  " now has " + doctorAvailableSlots[doctorIndex] +
                                  " slot(s) available.");
            }

            // Summary
            Console.WriteLine("----------------------------------------");

            if (visitCharges > 0)
                Console.WriteLine("Total charges added: " + visitCharges.ToString("0.00") + " OMR");
            else
                Console.WriteLine("No charges recorded.");

            // Discharge patient
            admitted[i] = false;
            assignedDoctors[i] = "";

            Console.WriteLine("Patient discharged successfully!");

            return i;
        }
        /// <summary>
        /// Displays all currently admitted patients, optionally filtered by a keyword.
        /// Also calculates and displays admission statistics such as total count,
        /// highest billing, and lowest billing among admitted patients.
        /// </summary>
        /// <param name="keyword">
        /// Optional search keyword used to filter patient names. If null or empty,
        /// all admitted patients are displayed.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It outputs patient details
        /// and summary statistics directly to the console.
        /// </returns>
        static public void ShowAdmittedPatients(string keyword)
        {
            if (keyword != null)
                keyword = keyword.Trim();

            bool hasAdmitted = false;
            int admittedCounter = 0;

            double highestBilling = 0;
            double lowestBilling = double.MaxValue;

            for (int i = 0; i < patientNames.Count; i++)
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

                highestBilling = Math.Max(highestBilling, billingAmount[i]);
                lowestBilling = Math.Min(lowestBilling, billingAmount[i]);
            }

            if (hasAdmitted)
            {
                Console.WriteLine("Total admitted patients: " + admittedCounter);
                Console.WriteLine("Highest billing: " + highestBilling.ToString("0.00") + " OMR");
                Console.WriteLine("Lowest billing: " + lowestBilling.ToString("0.00") + " OMR");
            }
            else
            {
                Console.WriteLine("No patients currently admitted.");
            }
        }
        /// <summary>
        /// Normalizes a doctor's name into a consistent format.
        /// Converts input to lowercase, removes any existing "dr" or "dr." prefix,
        /// trims extra spaces, and returns the name in the format "Dr. Name"
        /// with proper title casing.
        /// </summary>
        /// <param name="name">
        /// The input doctor name provided by the user. It may include variations
        /// such as "dr noor", "Dr. noor", or extra spaces.
        /// </param>
        /// <returns>
        /// A formatted doctor name in the standard form "Dr. Name".
        /// Returns an empty string if the input is null, empty, or whitespace.
        /// </returns>
        static public string NormalizeDoctorName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "";

            name = name.ToLower().Replace("dr.", "").Replace("dr", "").Trim();
            return "Dr. " + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
        }
        /// <summary>
        /// Transfers all admitted patients from one doctor to another and updates doctor slot availability.
        /// </summary>
        /// <param name="currentDoctor">
        /// The name of the doctor currently assigned to the patients.
        /// </param>
        /// <param name="newDoctor">
        /// The name of the doctor who will receive the transferred patients.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It prints transfer results and status messages to the console.
        /// </returns>
        static public void TransferPatientsToNewDoctor(string currentDoctor, string newDoctor)
        {
            if (string.IsNullOrWhiteSpace(currentDoctor) || string.IsNullOrWhiteSpace(newDoctor))
            {
                Console.WriteLine("Doctor names cannot be empty.");
                return;
            }

            // normalize ONLY for comparison
            string currentInput = NormalizeDoctorName(currentDoctor);
            string newInput = NormalizeDoctorName(newDoctor);
            
            if (currentInput.Equals(newInput, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The names of current and new doctors must be different.");
                return;
            }

            // find doctors using system list (source of truth)
            int oldDoctorIndex = doctorNames.FindIndex(d =>
                NormalizeDoctorName(d).Equals(currentInput, StringComparison.OrdinalIgnoreCase));

            int newDoctorIndex = doctorNames.FindIndex(d =>
                NormalizeDoctorName(d).Equals(newInput, StringComparison.OrdinalIgnoreCase));

            if (newDoctorIndex == -1)
            {
                Console.WriteLine("New doctor does not exist.");
                return;
            }

            bool doctorFound = false;
            int transferCount = 0;

            for (int i = 0; i < patientNames.Count; i++)
            {
                if (admitted[i] &&
                    NormalizeDoctorName(assignedDoctors[i])
                        .Equals(currentInput, StringComparison.OrdinalIgnoreCase))
                {
                    doctorFound = true;

                    // store ORIGINAL system doctor name (not normalized input)
                    assignedDoctors[i] = doctorNames[newDoctorIndex];
                    transferCount++;

                    Console.WriteLine("Patient " + patientNames[i] +
                                      " transferred to " + doctorNames[newDoctorIndex] +
                                      " | Last admitted on: " + lastVisitDate[i]);
                }
            }

            // update slots ONCE
            if (doctorFound)
            {
                if (oldDoctorIndex != -1)
                    doctorAvailableSlots[oldDoctorIndex]++;

                if (doctorAvailableSlots[newDoctorIndex] > 0)
                    doctorAvailableSlots[newDoctorIndex]--;
                else
                    Console.WriteLine("Warning: New doctor has no available slots left.");

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Total patients transferred: " + transferCount);
            }
            else
            {
                Console.WriteLine("No admitted patients found under this doctor.");
            }
        }
        /// <summary>
        /// Displays a list of patients sorted by their visit count in descending order.
        /// Only patients with at least one visit are shown.
        /// </summary>
        /// <param name="none">
        /// This method does not take any parameters.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It outputs the sorted patient visit details to the console.
        /// </returns>
        static public void ShowMostVisitedPatients()
        {
            Console.WriteLine("Most Visited Patients (by visit count):");
            Console.WriteLine("----------------------------------------");

            // List check instead of lastIndex
            if (patientNames.Count == 0)
            {
                Console.WriteLine("No patients available.");
                return;
            }

            // Create index list (so we don't modify original data)
            List<int> indexes = new List<int>();

            for (int i = 0; i < patientNames.Count; i++)
            {
                indexes.Add(i);
            }

            //  Sort indexes by visit count (descending)
            indexes.Sort((a, b) => visitCount[b].CompareTo(visitCount[a]));

            bool hasVisits = false;

            foreach (int i in indexes)
            {
                if (visitCount[i] == 0)
                    continue;

                Console.WriteLine("Name: " + patientNames[i] + " | ID: " + patientIDs[i]);
                Console.WriteLine("Total Visits: " + visitCount[i]);
                Console.WriteLine("Department: " + departments[i] + " | Doctor: " + assignedDoctors[i]);
                Console.WriteLine("----------------------------------------");

                hasVisits = true;
            }

            if (!hasVisits)
                Console.WriteLine("No patient visits recorded yet.");
        }
        /// <summary>
        /// Displays all patients filtered by a specific department entered by the user.
        /// Shows basic patient details including ID, name, diagnosis, status, and blood type.
        /// </summary>
        /// <param name="none">
        /// This method does not take any parameters. The department name is read from console input.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It outputs matching patient details directly to the console.
        /// </returns>
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

            // List.Count instead of lastIndex
            for (int i = 0; i < patientNames.Count; i++)
            {
                if (!string.IsNullOrEmpty(departments[i]) &&
                    departments[i].ToLower().Contains(searchDept.ToLower()))
                {
                    deptFound = true;

                    string status = admitted[i] ? "Admitted" : "Not Admitted";

                    string displayDiagnosis = string.IsNullOrEmpty(diagnoses[i])
                        ? "N/A"
                        : (diagnoses[i].Length > 15
                            ? diagnoses[i].Substring(0, 15) + "..."
                            : diagnoses[i]);

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
        /// Displays a billing summary for a specific patient, including their total billing,
        /// last visit date, total days in hospital, and system-wide highest and lowest billing values.
        /// </summary>
        /// <param name="billingIndex">
        /// The index of the patient in the system whose billing summary will be displayed.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It outputs billing details directly to the console.
        /// </returns>
        static public void ShowBillingSummary(int billingIndex)
        {
            //  List boundary check
            if (billingIndex < 0 || billingIndex >= patientNames.Count)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            Console.WriteLine("----------------------------------------");

            double billingTotal = billingAmount[billingIndex];

            //If there are no patients
            if (billingAmount.Count == 0)
            {
                Console.WriteLine("No billing data available.");
                return;
            }

            double highestBilling = billingAmount[0];
            double lowestBilling = double.MaxValue;

           

            // List.Count instead of lastIndex
            for (int i = 1; i < patientNames.Count; i++)
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
        /// Applies a random discount to a patient's total billing amount and displays the final discounted value.
        /// </summary>
        /// <param name="billingIndex">
        /// The index of the patient whose billing amount will be discounted.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It outputs the discount details directly to the console.
        /// </returns>
        static public void ApplyBillingDiscount(int billingIndex)
        {
            // List boundary check
            if (billingIndex < 0 || billingIndex >= patientNames.Count)
            {
                Console.WriteLine("Invalid patient index.");
                return;
            }

            double billingTotal = billingAmount[billingIndex];

            int discountPercent = rand.Next(5, 21);

            double discountAmount = billingTotal * discountPercent / 100.0;
            double finalAmount = Math.Round(billingTotal - discountAmount, 2);

            billingAmount[billingIndex] = finalAmount;

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
                Console.WriteLine("Exit cancelled. Returning to menu...");
                return false; // user canceled exit
            }

        }

        /// <summary>
        /// Registers a new doctor in the system and initializes their available slots and visit count.
        /// </summary>
        /// <param name="none">
        /// This method does not take any parameters. Doctor details are entered via console input.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It updates doctor lists and prints confirmation messages to the console.
        /// </returns>
        /// 
        static public void RegisterDoctor()
        {
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

            // Store doctor using Lists
            doctorNames.Add(doctorNameInput);
            doctorAvailableSlots.Add(slotsNumber);
            doctorVisitCount.Add(0);

            Console.WriteLine("Doctor registered successfully.");
        }
        /// <summary>
        /// Generates a salary report for all registered doctors based on their visit count.
        /// Also identifies and displays the highest earning doctor.
        /// </summary>
        /// <param name="none">
        /// This method does not take any parameters. It uses internal doctor lists for processing.
        /// </param>
        /// <returns>
        /// This method does not return a value (void). It outputs the salary report directly to the console.
        /// </returns>
        static public void GenerateDoctorSalaryReport()
        {
            // Check if any doctors exist
            if (doctorNames.Count == 0)
            {
                Console.WriteLine("No doctors registered in the system.");
                return;
            }

            Console.WriteLine("Doctor Salary Report:");
            Console.WriteLine("----------------------------------------");

            double highestSalary = 0;
            int highestIndex = -1;

            // Loop through all doctors using List.Count
            for (int i = 0; i < doctorNames.Count; i++)
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

                Console.WriteLine($"{name} | Visits: {visits} | Available Slots: {slots} | Salary: {salary:0.00} OMR");
            }

            Console.WriteLine("----------------------------------------");

            // Highest earning doctor
            if (highestIndex >= 0)
            {
                Console.WriteLine("Highest earning doctor: " + doctorNames[highestIndex] +
                                  " — " + highestSalary.ToString("0.00") + " OMR");
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
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 12.");
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

                        // Convert to List and call function
                        List<string> newPatientIDs = RegisterPatient(
    new List<string> { patientName },
    new List<string> { diagnose },
    new List<string> { department },
    new List<string> { bloodTypes }


);

                        if (newPatientIDs.Count > 0)
                        {
                            Console.WriteLine("Generated Patient ID: " + newPatientIDs[0]);
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



                        if (billingOption == 1)
                        {
                            // System-wide total
                            double totalBilling = 0;

                            //  List.Count instead of lastIndex
                            for (int i = 0; i < patientNames.Count; i++)
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
                            string billingInput = Console.ReadLine()?.Trim() ?? string.Empty;

                            // search patient
                            int billingIndex = searchPatient(billingInput);

                            // check if patient not found
                            if (!errorMessage(billingIndex))
                            {
                                break;
                            }
                            else
                            {
                                // display billing summary
                                ShowBillingSummary(billingIndex);

                                // apply discount
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
                        Console.WriteLine("Invalid input. Please choose a number from 1 to 12.");
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