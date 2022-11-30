using Microsoft.EntityFrameworkCore.Update.Internal;
using ShiftTracker.Api.Entities;
using ShiftTracker.Ui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTracker.Ui
{
    internal class GetUserInput
    {
        ShiftServiceUi shiftServiceUi = new();

        public void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome to Shift Tracker!");
            Console.WriteLine("\nThis app helps you track the time you work and the money you make!");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\n1 - Add a Shift");            
            Console.WriteLine("2 - Edit a Shift");
            Console.WriteLine("3 - Delete a Shift");
            Console.WriteLine("4 - Calculate Weekly Totals");
            Console.WriteLine("5 - Display Shifts");
            Console.WriteLine("0 - Quit\n");

            string menuSelection = Console.ReadLine();

            switch (menuSelection)
            {
                case "0":
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
                case "1":
                    var newShift = TimeEntry();
                    shiftServiceUi.AddShift(newShift);
                    Console.WriteLine("\nShift Added successfully.\nPress Enter...");
                    Console.ReadLine();
                    MainMenu();
                    break;
                case "2":
                    var updateShift = UpdateShiftEntry();
                    shiftServiceUi.UpdateShift(updateShift);
                    Console.WriteLine("\nShift Updated succesfully.\nPress Enter...");
                    Console.ReadLine();
                    MainMenu();
                    break;
                case "3":
                    var deleteShift = DeleteShiftEntry();
                    if (deleteShift == 0) MainMenu();
                    shiftServiceUi.DeleteShift(deleteShift);
                    Console.WriteLine("\nShift Deleted successfully.\nPress Enter...");
                    Console.ReadLine();
                    MainMenu();
                    break;
                //case "4":
                //    apiController.GetTopics("classes");
                //    break;
                case "5":
                    Console.Clear();
                    shiftServiceUi.GetShifts();
                    Console.WriteLine("\nPress Enter...");
                    Console.ReadLine();
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Please make a valid choice, 0-5!\nPress Enter...");
                    Console.ReadLine();
                    MainMenu();
                    break;
            }
        }

        private int DeleteShiftEntry()
        {
            Console.Clear();
            shiftServiceUi.GetShifts();
            int currentShiftId = 0;

            Console.WriteLine("\nEnter the Id of the shift to Delete or 0 to return to Menu;");
            string shiftIdInput = Console.ReadLine();

            if (shiftIdInput == "0")
            {
                MainMenu();
            }

            while (!Validation.IsIdValid(shiftIdInput) || !Validation.IsShiftIdValid(shiftIdInput))
            {
                Console.WriteLine("Please enter a valid shift ID:\n");
                shiftIdInput = Console.ReadLine();
            }

            currentShiftId = Int32.Parse(shiftIdInput);                   

            var deleteShift = shiftServiceUi.GetShiftById(currentShiftId);            

            List<Shift> shifts = new List<Shift>();
            shifts.Add(deleteShift.Data);

            TableFormat.ShowTable(shifts, "Delete");

            Console.WriteLine("\nAre you sure you want to delete this shift? \nType \"yes\" to confirm. Enter 0 or any other input to go back to Menu.");

            string confirmDelete = Console.ReadLine();            

            if (confirmDelete.ToLower() == "yes")
            {
                return currentShiftId;                
            }

            else
            {
                return 0;
            }
        }

        private Shift UpdateShiftEntry()
        {
            Console.Clear();
            shiftServiceUi.GetShifts();

            Console.WriteLine("\nEnter the Id of the shift to edit or 0 to return to Menu;");
            string shiftIdInput = Console.ReadLine();            

            while (!Validation.IsIdValid(shiftIdInput) || !Validation.IsShiftIdValid(shiftIdInput))
            {
                Console.WriteLine("Please enter a valid shift ID:\n");
                shiftIdInput = Console.ReadLine();
            }

            int currentShiftId = Int32.Parse(shiftIdInput);

            if (currentShiftId == 0)
            {
                MainMenu();
            }           

            var updateShift = shiftServiceUi.GetShiftById(currentShiftId);     
            var updateCurrentShift = TimeEntry(updateShift.Data);

            return updateCurrentShift;
        }

        public Shift TimeEntry()
        {
            Console.Clear();
            Console.WriteLine("Time Entry");

            ShiftEntryServiceUi shiftEntryService = new();

            string startDate = shiftEntryService.GetStartDate();
            string startTime = shiftEntryService.GetStartTime();            

            string startTimeString = $"{startDate} {startTime}";
            DateTime shiftStart = DateTime.Parse(startTimeString);
            
            string endDate = shiftEntryService.GetEndDate();
            string endTime = shiftEntryService.GetEndTime();            

            string endTimeString = $"{endDate} {endTime}";
            DateTime shiftEnd = DateTime.Parse(endTimeString);

            decimal sqlHourlyRate = shiftEntryService.GetHourlyRate();
            string location = shiftEntryService.GetLocation();            

            Shift currentShift = new Shift();
            currentShift.Start = shiftStart;
            currentShift.End = shiftEnd;
            currentShift.Pay = sqlHourlyRate;
            currentShift.Location = location;

            return currentShift;            
        }

        public Shift TimeEntry(Shift shift)
        {
            Console.Clear();

            var updateShift = shiftServiceUi.GetShiftById(shift.ShiftId);

            List<Shift> shifts = new List<Shift>();
            shifts.Add(updateShift.Data);

            TableFormat.ShowTable(shifts, "Edit");

            Console.WriteLine("Update Shift");

            ShiftEntryServiceUi shiftEntryServiceUi = new();
            string startDate = shiftEntryServiceUi.GetUpdatedStartDate(shift);
            string startTime = shiftEntryServiceUi.GetUpdatedStartTime(shift);

            string startTimeString = $"{startDate} {startTime}";
            DateTime shiftStart = DateTime.Parse(startTimeString);
            string sqlShiftStart = shiftStart.ToString("yyyy-MM-ddTHH:mm:ss");

            string endDate = shiftEntryServiceUi.GetUpdatedEndDate(shift);
            string endTime = shiftEntryServiceUi.GetUpdatedEndTime(shift);  

            string endTimeString = $"{endDate} {endTime}";
            DateTime shiftEnd = DateTime.Parse(endTimeString);
            string sqlShiftEnd = shiftEnd.ToString("yyyy-MM-ddTHH:mm:ss");       

            decimal sqlHourlyRate = shiftEntryServiceUi.GetUpdatedHourlyRate(shift);
            string location = shiftEntryServiceUi.GetUpdatedLocation(shift);   

            Shift currentShift = new Shift();
            
            currentShift.ShiftId = shift.ShiftId;
            currentShift.Start = shiftStart;
            currentShift.End = shiftEnd;
            currentShift.Pay = sqlHourlyRate;
            currentShift.Minutes = 0;
            currentShift.Location = location;

            return currentShift;            
        }
    }
}
