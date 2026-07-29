# Test Plan

## Feature Under Test

Appointment cancellation. Reception staff will be able to cancel an
existing appointment, which releases the doctor's slot so it becomes
available for another patient to book.

## Test Objective

To confirm that the cancellation feature correctly releases a
doctor's slot when a valid appointment is cancelled, and that it
correctly rejects cancellation attempts for appointments that do not
exist, without affecting the reliability of the existing booking
functionality.

## Requirements to be Tested

- REQ-CAN-01: The system shall allow an existing appointment to be
  cancelled.
- REQ-CAN-02: When an appointment is cancelled, the doctor's available
  slot count shall increase by one.
- REQ-CAN-03: The system shall not allow cancellation of an
  appointment that does not exist.

## Test Items

- Doctor class, specifically the slot count and any new method added
  to release a slot (for example, ReleaseSlot)
- AppointmentBookingService, specifically the new
  CancelAppointment method
- BookingResult, used to report whether cancellation succeeded and
  why

## Test Approach

Cancellation will be tested primarily at the unit level using MSTest,
following the same approach used for the existing booking feature.
Each requirement above will have at least one corresponding test case.
Valid cancellation, invalid cancellation, and slot count behaviour
before and after cancellation will all be tested directly, rather than
relying on manual testing alone. Existing booking tests will be
re-run as regression tests to confirm cancellation does not break
booking behaviour.

## Test Data

- A doctor with a reduced slot count, representing an existing
  appointment that was previously booked
- A valid appointment reference or identifier belonging to that
  doctor, used to test successful cancellation
- An invalid or non-existent appointment reference, used to test
  REQ-CAN-03
- A doctor at maximum available slots, used to confirm slot count
  does not exceed its original capacity if cancellation logic is
  triggered incorrectly

## Responsibilities

The developer implementing the cancellation feature is responsible
for writing the corresponding unit tests before the feature is
considered complete, in line with the process already used for
Doctor, Patient, AppointmentRequest, and AppointmentBookingService.
Any GitHub Copilot suggested tests must be reviewed and understood
before being added to the test suite, consistent with the course
requirement for responsible AI assisted testing.

## Schedule

Test cases will be written alongside the implementation of the
cancellation feature, not after it. Testing will be completed before
the feature is committed as finished work, and the full regression
suite will be run one final time before the feature is considered
ready for review.

## Pass and Fail Criteria

**Pass criteria:**
- A valid appointment can be cancelled successfully
- Cancelling a valid appointment increases the doctor's available
  slot count by exactly one
- Attempting to cancel a non-existent appointment fails with a clear,
  explanatory message rather than an unhandled exception
- All existing booking tests continue to pass after the feature is
  added

**Fail criteria:**
- Any of the three requirements (REQ-CAN-01 to REQ-CAN-03) does not
  have a passing test
- Cancelling an appointment changes the slot count by an incorrect
  amount
- Cancelling a non-existent appointment throws an unhandled exception
  instead of returning a controlled failure result
- Any previously passing test in the existing suite fails after the
  feature is added

## Risks

- **Risk**: Without a way to track individual appointments, it may be
  unclear which appointment is being cancelled, since the current
  system does not store a list of booked appointments.
  **Mitigation**: this will need to be addressed during
  implementation, for example by introducing an appointment
  identifier or a simple in memory list of active appointments before
  cancellation logic is added.

- **Risk**: Cancelling an appointment could incorrectly increase a
  doctor's slot count beyond its original maximum if called more than
  once for the same appointment.
  **Mitigation**: add a test that cancels the same appointment twice
  and confirms the second attempt is rejected rather than increasing
  the slot count again.

- **Risk**: As with the existing booking feature, concurrent
  cancellation requests could cause inconsistent slot counts.
  **Mitigation**: documented as a known limitation, consistent with
  the same risk already recorded for ReserveSlot in the test strategy.