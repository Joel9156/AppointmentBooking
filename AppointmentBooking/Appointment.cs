using ENSE707_AppointmentBooking;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentBooking
{
    public class Appointment
    {
        public string Id { get; }
        public Doctor Doctor { get; }
        public Patient Patient { get; }
        public DateTime AppointmentDate { get; }
        public bool IsCancelled { get; private set; }

        public Appointment(string id, Doctor doctor, Patient patient, DateTime appointmentDate)
        {
            // Validate that an ID was provided; without it we cannot track this appointment.
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Appointment ID is required.");

            Id = id;

            // Doctor and Patient must exist; an appointment cannot be created without them.
            Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor));
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));

            AppointmentDate = appointmentDate;
            IsCancelled = false;
        }

        // Cancels the appointment. Cancelling twice is not allowed, since the slot
        // would otherwise be released more than once.
        public void Cancel()
        {
            if (IsCancelled)
                throw new InvalidOperationException("Appointment has already been cancelled.");

            IsCancelled = true;
        }
    }
}   