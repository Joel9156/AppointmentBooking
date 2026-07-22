namespace ENSE707_AppointmentBooking
{
    public class BookingResult
    {
        // True if the appointment was booked successfully.
        public bool Success { get; }

        // Explains why the booking succeeded or failed, so the user isn't left guessing.
        public string Message { get; }

        public BookingResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}