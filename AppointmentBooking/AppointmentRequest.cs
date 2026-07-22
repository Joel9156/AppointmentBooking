namespace ENSE707_AppointmentBooking
{
    public class AppointmentRequest
    {
        public Patient Patient { get; }
        public Doctor Doctor { get; }
        public DateTime RequestedDate { get; }

        public AppointmentRequest(Patient patient, Doctor doctor, DateTime requestedDate)
        {
            // Reject a request that's missing a patient or doctor,
            // instead of letting a null slip through to later code.
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor));

            // A request for a past date makes no sense for booking a future appointment.
            if (requestedDate.Date < DateTime.Today)
                throw new ArgumentException("Requested appointment date cannot be in the past.");

            RequestedDate = requestedDate;
        }
    }
}