namespace ENSE707_AppointmentBooking
{
    public class Patient
    {
        // Legal identity fields — cannot be changed after creation.
        public string Id { get; }
        public string LegalName { get; }
        public string PreferredName { get; }

        // Shows the name that should be used when addressing the patient.
        // Falls back to LegalName if no PreferredName was given.
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PreferredName))
                    return LegalName;

                return PreferredName;
            }
        }

        public Patient(string id, string legalName, string preferredName = "")
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Patient ID is required.");

            if (string.IsNullOrWhiteSpace(legalName))
                throw new ArgumentException("Legal name is required.");

            Id = id;
            LegalName = legalName;
            PreferredName = preferredName;
        }
    }
}