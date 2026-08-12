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