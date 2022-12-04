using ShiftTracker.Api.Data.Dtos;
using ShiftTracker.Api.Entities;

namespace ShiftTracker.Api.Controllers;

public static class DtoController
{
    public static ShiftDto ToShiftDto(this Shift shift)
    {
        if (shift == null)
        {
            return null;
        }

        var readShift = new ShiftDto()
        {
            ShiftId = shift.ShiftId,
            Start = shift.Start,
            End = shift.End,
            Pay = shift.Pay,
            Minutes = shift.Minutes,
            Location = shift.Location
        };

        return readShift;
    }

    public static Shift ToShift(this ShiftDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var readShift = new Shift()
        {
            ShiftId = dto.ShiftId,
            Start = dto.Start,
            End = dto.End,
            Pay = dto.Pay,
            Minutes = dto.Minutes,
            Location = dto.Location
        };

        return readShift;
    }
}
