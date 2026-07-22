namespace ENSE707_AppointmentBooking
{
    public class Doctor
    {
        // Id and FullName have no public setter, so they can't be changed after creation.
        public string Id { get; }
        public string FullName { get; }

        // AvailableSlots can only be changed inside this class (private set),
        // preventing external code from directly modifying the slot count.
        public int AvailableSlots { get; private set; }

        // Validation happens here so a Doctor object can never exist in an invalid state.
        public Doctor(string id, string fullName, int availableSlots)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Doctor ID is required.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Doctor name is required.");

            if (availableSlots < 0)
                throw new ArgumentException("Available slots cannot be negative.");

            Id = id;
            FullName = fullName;
            AvailableSlots = availableSlots;
        }

        // Lets other code check slot availability without touching the slot count directly.
        public bool HasAvailableSlot()
        {
            return AvailableSlots > 0;
        }

        // The only way to reduce AvailableSlots, and it blocks reserving when none are left.
        public void ReserveSlot()
        {
            if (!HasAvailableSlot())
                throw new InvalidOperationException("No appointment slots are available.");

            AvailableSlots--;
        }
    }
}