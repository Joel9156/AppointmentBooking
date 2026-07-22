using ENSE707_AppointmentBooking;

namespace ENSE707_AppointmentBooking.Tests
{
    [TestClass]
    public class AppointmentBookingServiceTests
    {
        [TestMethod]
        public void BookAppointment_WhenDoctorHasAvailableSlots_ReturnsSuccess()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void BookAppointment_WhenDoctorHasNoAvailableSlots_ReturnsFailure()
        {
            var doctor = new Doctor("D001", "Dr Mark", 0);
            var patient = new Patient("P001", "Diana William");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void BookAppointment_WhenSuccessful_DecreasesAvailableSlots()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            service.BookAppointment(request);

            Assert.AreEqual(1, doctor.AvailableSlots);
        }

        [TestMethod]
        public void BookAppointment_WhenFailed_DoesNotDecreaseAvailableSlots()
        {
            var doctor = new Doctor("D001", "Dr Mark", 0);
            var patient = new Patient("P001", "Diana William");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            service.BookAppointment(request);

            Assert.AreEqual(0, doctor.AvailableSlots);
        }

        [TestMethod]
        public void Doctor_WhenIdIsEmpty_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentException>(() =>
                new Doctor("", "Dr Mark", 2));
        }

        [TestMethod]
        public void Doctor_WhenAvailableSlotsIsNegative_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentException>(() =>
                new Doctor("D001", "Dr Mark", -1));
        }

        [TestMethod]
        public void Patient_WhenIdIsEmpty_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentException>(() =>
                new Patient("", "Diana William"));
        }

        [TestMethod]
        public void Patient_WhenPreferredNameExists_DisplayNameUsesPreferredName()
        {
            var patient = new Patient("P001", "Diana William", "Aroha");

            Assert.AreEqual("Aroha", patient.DisplayName);
        }

        [TestMethod]
        public void Patient_WhenPreferredNameMissing_DisplayNameUsesLegalName()
        {
            var patient = new Patient("P001", "Diana William");

            Assert.AreEqual("Diana William", patient.DisplayName);
        }

        [TestMethod]
        public void AppointmentRequest_WhenRequestedDateIsInPast_ThrowsException()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William");

            Assert.ThrowsExactly<ArgumentException>(() =>
                new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(-1)));
        }

        [TestMethod]
        public void BookAppointment_WhenSuccessful_ReturnsHelpfulMessage()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William", "Aroha");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            StringAssert.Contains(result.Message, "Appointment booked successfully");
            StringAssert.Contains(result.Message, "Aroha");
        }

        [TestMethod]
        public void BookAppointment_WhenNoSlots_ReturnsHelpfulMessage()
        {
            var doctor = new Doctor("D001", "Dr Mark", 0);
            var patient = new Patient("P001", "Diana William");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            StringAssert.Contains(result.Message, "no available slots");
        }
        [TestMethod]
        public void AppointmentRequest_WhenDateIsToday_ThrowsException()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William");

            Assert.ThrowsExactly<ArgumentException>(() =>
                new AppointmentRequest(patient, doctor, DateTime.Today));
        }

        [TestMethod]
        public void AppointmentRequest_WhenDateIsTomorrow_IsAccepted()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William");

            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            Assert.AreEqual(DateTime.Today.AddDays(1), request.RequestedDate);
        }

        [TestMethod]
        public void Doctor_WhenAvailableSlotsExceedsMaxDaily_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentException>(() =>
                new Doctor("D001", "Dr Mark", 6));
        }

        [TestMethod]
        public void BookAppointment_WhenSuccessful_MessageIncludesDoctorName()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);
            var patient = new Patient("P001", "Diana William");
            var request = new AppointmentRequest(patient, doctor, DateTime.Today.AddDays(1));

            var service = new AppointmentBookingService();

            BookingResult result = service.BookAppointment(request);

            StringAssert.Contains(result.Message, "Dr Mark");
        }

        [TestMethod]
        public void BookAppointment_WhenPatientIdMissing_ThrowsException()
        {
            var doctor = new Doctor("D001", "Dr Mark", 2);

            Assert.ThrowsExactly<ArgumentException>(() =>
                new Patient("", "Diana William"));
        }
    }
}