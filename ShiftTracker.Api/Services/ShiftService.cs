using ShiftTracker.Api.Entities;

namespace ShiftTracker.Api.Services;

public class ShiftService
{
    public Shift CalculateTimeAndPay(Shift shift)
    {
        DateTime startShift = shift.Start;
        DateTime endShift = shift.End;
        TimeSpan calculatedTime = endShift - startShift;
        decimal hours = calculatedTime.Hours;
        decimal minutes = calculatedTime.Minutes;
        decimal payRate = shift.Pay;

        decimal calculatedMinutes = (hours * 60) + minutes;

        shift.Minutes = calculatedMinutes;
        shift.Pay = (calculatedMinutes / 60) * payRate;

        return shift;
    }
}
