## What confidence is still missing?

The 22 passing tests give strong confidence that individual classes (Doctor, Patient, 
AppointmentRequest) validate their inputs correctly, and that AppointmentBookingService 
handles single, sequential booking and cancellation requests correctly in isolation.

However, several important kinds of confidence are missing:

1. **Concurrency safety** — All tests run against a single, sequential call. There is no 
   evidence for what happens when two booking requests for the same doctor's last available 
   slot are submitted at the same time.

2. **Real system boundary behaviour** — Every test calls the service directly in memory. 
   No test exercises the system the way a real user or external interface (e.g. a console 
   app, UI, or API) would, so we don't know how the system behaves at that boundary 
   (e.g. malformed input from a text field, unexpected null handling from user input).

3. **Persistence and integration** — All state is in-memory and discarded after each test. 
   There is no evidence about what happens with real file or database storage, including 
   partial writes, crashes mid-save, or data surviving a restart.

4. **Exploratory / unscripted behaviour** — All 22 tests are scripted, expected-path or 
   expected-failure tests. None of them look for unexpected behaviours such as duplicate 
   bookings, race conditions, or misleading feedback messages that a tester might discover 
   only by deliberately exploring the system.

A green suite tells us the scenarios it covers work as expected — it does not tell us the 
product is ready for real-world use.

## Activity 2 - Testing Map

| Existing test/evidence | Level | Type/focus | Technique/perspective | What it provides evidence for | Important gap |
|---|---|---|---|---|---|
| Doctor: negative slots (`Doctor_WhenAvailableSlotsIsNegative_ThrowsException`) | Unit | Input validation | Boundary value (below 0) / negative test | The Doctor constructor rejects a negative slot count | Doesn't check other invalid values, e.g. non-integer input or extremely large numbers |
| Booking: no slots (`BookAppointment_WhenDoctorHasNoAvailableSlots_ReturnsFailure`) | Unit / small component (touches Doctor + Service together) | Business rule | Equivalence partition (zero slots case) | A doctor with zero slots cannot be booked | Doesn't confirm the failure message content, and doesn't test what happens after several consecutive failed attempts |
| Patient: preferred display name (`Patient_WhenPreferredNameExists_DisplayNameUsesPreferredName`) | Unit | Display logic | Equivalence partition (preferred name present) | DisplayName correctly returns the preferred name when one is set | Doesn't test edge cases like a preferred name that is only whitespace |
| Request: past appointment date (`AppointmentRequest_WhenRequestedDateIsInPast_ThrowsException`) | Unit | Input validation | Boundary value (day before today) | The system rejects a request dated before today | Doesn't test dates far in the past or far in the future (e.g. multi-year gap) |
| Booking: helpful success message (`BookAppointment_WhenSuccessful_ReturnsHelpfulMessage`) | Unit / small component | Output/message content | Happy path | A successful booking returns a message containing the patient's preferred name | Only checks that the message *contains* certain text, not the full expected wording, so a badly worded message could still pass |
| CancelAppointment: releases doctor slot (`CancelAppointment_ExistingAppointment_ReleasesDoctorSlot`) | Small component (Service + Doctor + Appointment working together) | State change / business rule | Happy path | Cancelling a booked appointment correctly returns the slot to the doctor | Doesn't test cancelling one of several concurrent appointments for the same doctor, or cancelling right at the appointment date boundary |

### Level/focus we do not yet have convincing evidence for

| Level/focus | Do we have convincing evidence? | Notes |
|---|---|---|
| Integration | No | All tests call classes directly in memory; nothing exercises real file, database, or external service behaviour |
| System | No | No test goes through an external interface (console, UI, or API) the way a real user or another system would |
| Acceptance | No | No test is framed around a business stakeholder's acceptance criteria in a way a non-developer could review and sign off on |
| Non-functional | No | No test covers performance, load, concurrency, security, or usability — only functional correctness is covered |