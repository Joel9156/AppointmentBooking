# Continuous Improvement

## What Worked Well

- Writing tests immediately after implementing each feature (booking
  validation in Week 2, cancellation in Week 3) made it easy to
  confirm correctness before moving on.
- Running the full test suite after each change, not just the new
  tests, caught issues early and confirmed nothing existing was
  broken.
- Keeping documentation close to the code made it easier to trace
  requirements to actual test cases and to justify decisions like the
  release recommendation with real evidence.

## What Did Not Work Well

- A property (`Appointment.Id`) was initially left unassigned in the
  constructor and only caught via a compiler warning rather than a
  test, showing that warnings should be checked as carefully as test
  results.
- Namespace inconsistency between new and existing files caused early
  build issues that could have been avoided with a quick check of
  existing files before creating new ones.
- The Week 2 Copilot review identified a race condition in
  `Doctor.ReserveSlot()`, where two simultaneous bookings could both
  pass the availability check before either updates the count. This
  was documented as a known limitation rather than fixed, since
  concurrency testing was out of scope for this stage.

## Root Cause of One Issue

The `Appointment.Id` property was not assigned in the constructor
because the constructor validated the `id` parameter but never stored
it in the property. This was a simple oversight, not caught by the
build, and would only have surfaced later as a bug if a test had
specifically checked the `Id` value.

## Improvement Action

Before writing tests, review the implementation against the
constructor signature to confirm every parameter is assigned to its
corresponding property. Treat build warnings as seriously as build
errors, and resolve them before running tests.

## How We Will Check the Improvement

Add a test case that checks `Appointment.Id` matches the value passed
into the constructor, and review the "Warnings" count in the build
output, not just "0 failed", before considering a build complete.

## Quality Culture Reflection

Working through Week 2 and Week 3 showed how process assurance and
product assurance reinforce each other in practice. Constructor level
validation across Doctor, Patient, and AppointmentRequest prevented
several classes of defects (negative slots, missing IDs, past dated
requests) before they could reach testing, while the test suite then
confirmed the product actually behaved as expected. Copilot's review
was useful for surfacing risks like the race condition, but required
manual judgement to decide what was actually relevant to this
system's scope rather than accepting suggestions such as full
dependency injection or holiday calendar handling. Although this was
completed individually rather than in a team, the same principles
apply to teamwork: shared processes, checklists, and evidence create
a common standard everyone can be held to. In the next phase, tests
will be written before or alongside implementation rather than only
afterward, to catch issues even earlier.

## Agile and DevOps Quality Practices for This Project

| Practice | How It Could Be Used in This Project |
|---|---|
| Sprint planning | Select a small set of features and quality tasks for the week, e.g. implementing and testing the cancellation feature before starting the next one |
| Daily stand-up | Discuss progress, blockers, and testing issues, such as the namespace mismatch encountered while adding the Appointment class |
| Definition of Done | A feature is complete only when it is coded, reviewed, tested (unit tests passing), and documented (e.g. requirements and test plan updated) |
| Continuous Integration | Automatically run the full test suite (all 22 tests) whenever code is pushed, to catch regressions immediately |
| Regression testing | Re-run existing tests (e.g. the original 17 booking tests) after each change, as was done after adding the cancellation feature |
| Retrospective | Review what went well (e.g. writing tests right after implementation) and what should improve (e.g. checking build warnings more carefully), as captured in the Continuous Improvement section above |