# Test Strategy

## 1. Purpose

This document describes the testing approach for the Appointment Booking
System. Its purpose is to define what will be tested, how it will be
tested, and what quality standards the system must meet before it is
considered ready for use. The strategy is intended to guide both manual
review and automated MSTest testing throughout development.

## 2. Scope of Testing

Testing covers the core domain classes and the booking workflow,
including:

- Doctor creation and slot management (validation, HasAvailableSlot,
  ReserveSlot)
- Patient creation and display name behaviour (LegalName,
  PreferredName, DisplayName)
- AppointmentRequest validation, including the one day notice
  business rule
- AppointmentBookingService booking logic, including success and
  failure messaging through BookingResult
- Business rules added for this system, such as the maximum daily
  appointment limit per doctor

## 3. Out of Scope

The following are out of scope for this stage of testing, since they
are not part of the current implementation:

- Integration with an external patient records system
- SMS or email notification services
- Persistent storage or database integration
- Authentication, authorisation, or user accounts
- Concurrency and thread safety testing (identified as a known risk,
  but not yet implemented or tested)
- Localisation and multi language support

## 4. Test Levels

- **Unit testing**: individual classes (Doctor, Patient,
  AppointmentRequest, AppointmentBookingService) are tested in
  isolation using MSTest.
- **Integration testing**: tests that combine Doctor, Patient,
  AppointmentRequest and AppointmentBookingService together to verify
  the full booking workflow behaves correctly end to end within the
  system boundary.
- **System testing**: manual and automated checks that confirm the
  overall booking feature meets the requirements described in the
  problem statement.
- **Regression testing**: the full MSTest suite is re-run after every
  code change to confirm existing functionality still works as
  expected.

## 5. Test Types

- **Validation testing**: confirms that invalid input, such as a
  negative slot count, an empty patient ID, or a past appointment
  date, is correctly rejected with the expected exception.
- **Functional testing**: confirms that valid bookings succeed, slots
  are decreased correctly, and failed bookings do not change the
  slot count.
- **Usability testing**: confirms that BookingResult messages are
  clear and explain the reason for success or failure, rather than
  returning an unexplained true or false value.
- **Regression testing**: confirms that previously passing tests
  continue to pass after later changes, such as adding the daily
  appointment limit rule.

## 6. Test Environment

Tests are developed and executed locally using Visual Studio on
Windows, targeting .NET. No external services, databases, or network
dependencies are required, since the system currently has no
persistence layer or third party integrations.

## 7. Tools

- Visual Studio (development and test execution)
- MSTest (test framework)
- Test Explorer (running and reviewing test results)
- Git and GitHub (version control and commit history as evidence of
  progress)
- GitHub Copilot (used critically to suggest additional test ideas
  and review code for quality issues, with all suggestions manually
  reviewed before being accepted)

## 8. Defect Management Approach

When a test fails, the failure is treated as a defect and is not
ignored or deleted. The relevant code is reviewed to determine
whether the defect is in the implementation or the test itself. Once
identified, the code is corrected, and the full test suite is re-run
to confirm the fix does not break any other test. Commit messages
describe what was fixed, so the Git history keeps a record of when
each defect was identified and resolved.

## 9. Entry Criteria

- The relevant class or feature has been implemented in code
- The project builds successfully with no compiler errors
- Required test project references are correctly configured

## 10. Exit Criteria

- All MSTest tests in the suite pass
- All identified business rules (slot validation, one day notice,
  daily appointment limit) have at least one corresponding test
- No known unresolved defects remain in the tested scope
- Test results have been reviewed and recorded as evidence

## 11. Risks and Mitigation

- **Risk**: Concurrent booking requests could cause a race condition
  in Doctor.ReserveSlot, allowing overbooking.
  **Mitigation**: documented as a known limitation; concurrency
  testing is out of scope for this stage but flagged for a future
  iteration.

- **Risk**: AI assisted suggestions from GitHub Copilot may reference
  features that do not exist in the system or introduce unnecessary
  complexity.
  **Mitigation**: all Copilot suggestions are reviewed manually before
  being accepted, and rejected suggestions are documented along with
  the reasoning.

- **Risk**: Business rules may change over time, such as the maximum
  daily appointment limit.
  **Mitigation**: validation logic is centralised in class
  constructors rather than scattered across the codebase, making
  future rule changes easier to locate and test.

- **Risk**: Limited time available to test every edge case in detail.
  **Mitigation**: testing is prioritised around the core booking
  workflow and validation rules first, since these carry the highest
  risk of user facing defects.