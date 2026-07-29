using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENSE707_AppointmentBooking;
using System;

namespace ENSE707_AppointmentBooking.Tests
{
    [TestClass]
    public class AppointmentCancellationTests
    {
        private AppointmentBookingService _service = null!;
        private Doctor _doctor = null!;
        private Patient _patient = null!;

        [TestInitialize]
        public void Setup()
        {
            _service = new AppointmentBookingService();
            _doctor = new Doctor("D1", "Dr. Smith", 3);
            _patient = new Patient("P1", "John Doe");
        }

        [TestMethod]
        public void CancelAppointment_ExistingAppointment_MarksAppointmentAsCancelled()
        {
            var request = new AppointmentRequest(_patient, _doctor, DateTime.Today.AddDays(1));
            var result = _service.BookAppointment(request);
            var appointment = result.Appointment!;

            _service.CancelAppointment(appointment);

            Assert.IsTrue(appointment.IsCancelled);
        }

        [TestMethod]
        public void CancelAppointment_ExistingAppointment_ReleasesDoctorSlot()
        {
            var request = new AppointmentRequest(_patient, _doctor, DateTime.Today.AddDays(1));
            var result = _service.BookAppointment(request);
            var appointment = result.Appointment!;
            int slotsAfterBooking = _doctor.AvailableSlots;

            _service.CancelAppointment(appointment);

            Assert.AreEqual(slotsAfterBooking + 1, _doctor.AvailableSlots);
        }

        [TestMethod]
        public void CancelAppointment_NullAppointment_ThrowsException()
        {
            try
            {
                _service.CancelAppointment(null!);
                Assert.Fail("Expected an ArgumentNullException but none was thrown.");
            }
            catch (ArgumentNullException)
            {
                // expected
            }
        }

        [TestMethod]
        public void CancelAppointment_AlreadyCancelledAppointment_ThrowsException()
        {
            var request = new AppointmentRequest(_patient, _doctor, DateTime.Today.AddDays(1));
            var result = _service.BookAppointment(request);
            var appointment = result.Appointment!;
            _service.CancelAppointment(appointment);

            try
            {
                _service.CancelAppointment(appointment);
                Assert.Fail("Expected an InvalidOperationException but none was thrown.");
            }
            catch (InvalidOperationException)
            {
                // expected
            }
        }

        [TestMethod]
        public void BookAppointment_Success_ReturnsAppointmentWithCorrectDetails()
        {
            var requestedDate = DateTime.Today.AddDays(1);
            var request = new AppointmentRequest(_patient, _doctor, requestedDate);

            var result = _service.BookAppointment(request);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Appointment);
            Assert.AreEqual(_doctor, result.Appointment!.Doctor);
            Assert.AreEqual(_patient, result.Appointment!.Patient);
            Assert.AreEqual(requestedDate, result.Appointment!.AppointmentDate);
        }
    }
}