# Black-Box Test Design

This design was created from the behavioural rules only (Rule sets A, B, C), before
reviewing the production implementation.

## Equivalence Partitions - Doctor Slots

| Partition | Representative value | Expected behaviour |
|---|---|---|
| Invalid (negative) | -1 | Rejected (exception at construction) |
| Unavailable (zero) | 0 | Doctor exists but cannot accept a booking |
| Available (one or more) | 3 | Doctor can accept a booking |

## Boundary Values - Doctor Slots

| Value | Region | Expected behaviour |
|---|---|---|
| -1 | Just below the invalid/unavailable boundary | Rejected |
| 0 | Unavailable boundary | Accepted as a doctor, but booking fails |
| 1 | Unavailable/available boundary | Booking succeeds |

## Equivalence Partitions - Appointment Date

| Partition | Representative value | Expected behaviour |
|---|---|---|
| Past | Yesterday | Invalid |
| Today | Today | Valid, per Rule set B (to be checked against implementation) |
| Future | Tomorrow / next week | Valid |

## Boundary Values - Past/Today Transition

| Value | Region | Expected behaviour |
|---|---|---|
| Yesterday | Just before the boundary | Rejected |
| Today | On the boundary | Valid per the rule set (needs verification against actual code) |
| Tomorrow | Just after the boundary | Valid |

## Decision Table - BookAppointment

| Condition | Case 1 | Case 2 | Case 3 | Case 4 |
|---|---|---|---|---|
| Date is valid (not past) | Yes | Yes | No | Yes |
| Doctor has available slots | Yes | No | Yes | Yes |
| **Result** | Success | Failure (no slots) | Failure (invalid date) | Success |
| **Slot consumed?** | Yes | No | No | Yes |
| **Message returned?** | Yes, success message | Yes, failure message | Yes, failure message (or exception) | Yes, success message |

## Designed Cases vs Baseline Suite

| Designed case | Already covered by baseline suite? |
|---|---|
| Negative slots rejected | Yes - `Doctor_WhenAvailableSlotsIsNegative_ThrowsException` |
| Zero slots -> booking fails | Yes - `BookAppointment_WhenDoctorHasNoAvailableSlots_ReturnsFailure` |
| One or more slots -> booking succeeds | Yes - `BookAppointment_WhenDoctorHasAvailableSlots_ReturnsSuccess` |
| Past date rejected | Yes - `AppointmentRequest_WhenRequestedDateIsInPast_ThrowsException` |
| Today's date valid | **Missing / contradicted** - baseline has `AppointmentRequest_WhenDateIsToday_ThrowsException`, which rejects today rather than accepting it |
| Future date valid | Yes - `AppointmentRequest_WhenDateIsTomorrow_IsAccepted` |
| Successful booking consumes exactly one slot | Yes - `BookAppointment_WhenSuccessful_DecreasesAvailableSlots` |
| Failed booking does not consume a slot | Yes - `BookAppointment_WhenFailed_DoesNotDecreaseAvailableSlots` |
| Slot boundary tested with a single data-driven test | Now covered - `Doctor_HasAvailableSlot_BoundaryCases` |
| Date boundary tested with a single data-driven test | Now covered - `AppointmentRequest_DateBoundary_Cases` |

## Note on a discovered discrepancy

Rule set B states "Today is valid in the current starter," but the baseline suite contains
`AppointmentRequest_WhenDateIsToday_ThrowsException`, which asserts the opposite - that
today's date throws an exception. This is exactly the kind of mismatch black-box design is
meant to surface: either the rule set description is out of date, or the implementation
does not match the intended behaviour. This should be raised as a question rather than
silently resolved either way.