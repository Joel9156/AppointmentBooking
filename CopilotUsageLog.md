# GitHub Copilot Usage — QA Process Suggestions

## Prompt Used
"Review this cancellation method for reliability, maintainability, and testability issues"
(CancelAppointment, Appointment.Cancel, Doctor.ReleaseSlot code provided)

## Useful Suggestion
Cancel() and ReleaseSlot() run as two separate steps with no coordination — if one fails,
the appointment could end up cancelled without the slot being released. Good catch on a
real consistency risk.

## Suggestion You Modified
Copilot suggested a non-throwing TryCancel() pattern. Kept the existing throwing
Cancel() instead, since a test already expects an exception on double cancellation —
only borrowed the idea of checking IsCancelled before releasing the slot.

## Suggestion You Rejected
Suggested Interlocked/locking for thread-safe slot updates. Rejected — this is a small
single-user student prototype with no concurrency, so it's unnecessary complexity here.

## Why Human Judgement Was Required
Copilot doesn't know the existing tests, project scope, or scale. It can't tell which
fixes are relevant now vs. only useful for a production system — that judgement call had
to be made manually.