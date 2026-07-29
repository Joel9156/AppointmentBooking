# Test Summary Report

## 1. Summary
This report summarises the testing carried out for the Week 3 appointment cancellation
feature, in addition to regression testing of the existing booking functionality. All
planned tests were executed and passed, with no defects remaining open.

## 2. Features Tested
- Appointment booking (existing feature, regression tested)
- Appointment cancellation (new feature)
- Doctor slot release on cancellation

## 3. Features Not Tested
- Performance/load testing (out of scope, see Test Strategy)
- UI testing (no graphical interface exists in this prototype)

## 4. Test Environment
- Local development machine, Visual Studio Community
- .NET 10, MSTest framework
- Tests executed via Visual Studio Test Explorer

## 5. Test Results

| Test Area | Number of Tests | Passed | Failed | Notes |
|---|---|---|---|---|
| Booking tests (existing) | 17 | 17 | 0 | Existing tests passed |
| Cancellation tests (new) | 5 | 5 | 0 | New feature passed |
| **Total** | **22** | **22** | **0** | |

## 6. Defects Found
No defects were found during this round of testing. All cancellation scenarios
(successful cancellation, slot release, null appointment, double cancellation) behaved
as expected on first implementation.

## 7. Defects Fixed
Not applicable — no defects were found (see Section 6). During development, one issue
was caught and corrected before testing: the `Appointment.Id` property was not being
assigned in the constructor. This was fixed by adding `Id = id;` before running any
tests, so it was never recorded as a formal defect.

## 8. Known Issues
None currently known.

## 9. Release Recommendation
**Recommended for demonstration.** All existing and new tests pass, the cancellation
feature meets its requirements (REQ-CAN-01, REQ-CAN-02, REQ-CAN-03), and no regressions
were introduced in the booking feature.

## 10. Lessons Learned
- Adding a small, well-scoped feature (cancellation) with tests written immediately
  after implementation made it easy to confirm correctness without breaking existing
  functionality.
- Running the full test suite after each change (not just the new tests) helped confirm
  there were no regressions.
- A property left unassigned in a constructor (`Appointment.Id`) was caught early by
  a compiler warning, showing the value of paying attention to build warnings, not
  just errors.