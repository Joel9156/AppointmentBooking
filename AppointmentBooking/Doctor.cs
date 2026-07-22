namespace ENSE707_AppointmentBooking
{
    public class Doctor
    {
        private const int MaxDailyAppointments = 5;

        public string Id { get; }
        public string FullName { get; }
        public int AvailableSlots { get; private set; }

        public Doctor(string id, string fullName, int availableSlots)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Doctor ID is required.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Doctor name is required.");

            if (availableSlots < 0)
                throw new ArgumentException("Available slots cannot be negative.");

            // A doctor cannot be configured with more slots than the daily appointment limit allows.
            if (availableSlots > MaxDailyAppointments)
                throw new ArgumentException($"Available slots cannot exceed the maximum of {MaxDailyAppointments} appointments per day.");

            Id = id;
            FullName = fullName;
            AvailableSlots = availableSlots;
        }

        public bool HasAvailableSlot()
        {
            return AvailableSlots > 0;
        }

        public void ReserveSlot()
        {
            if (!HasAvailableSlot())
                throw new InvalidOperationException("No appointment slots are available.");

            AvailableSlots--;
        }
    }
}