# Quality Governance

## Process Assurance vs Product Assurance

| Area | Process Assurance | Product Assurance |
|---|---|---|
| Main focus | How the work is performed | Quality of the software product |
| Example in this project | Requirements review, coding standards, Git commits, test process | Validation logic, working booking feature, passing tests |
| Evidence | Review checklist, commits, test plan, CI results | Test results, defect reports, working prototype |
| Goal | Prevent quality problems | Detect and confirm product quality |

Process assurance and product assurance work together rather than
replacing each other. Process assurance looks at how the team builds
the appointment booking system, things like following coding standards,
reviewing requirements before implementation, and committing work
regularly to Git so progress can be tracked over time. This side of
quality is preventive. It tries to stop defects from being introduced
in the first place by making sure the development process itself is
disciplined and consistent.

## Quality Governance Rules

| Governance Area | Rule | Evidence |
|---|---|---|
| Requirements | Each new feature must have at least one requirement ID | Requirements list (e.g. REQ-CAN-01/02/03) |
| Testing | Each requirement must have at least one test case | Traceability matrix / test files |
| Code quality | Code must pass all unit tests before commit | Test results (Test Explorer output) |
| GitHub | Each student must commit meaningful work regularly | Git history |
| AI use | Copilot suggestions must be reviewed and tested | AI reflection notes |
| Defects | Defects must be recorded with status and severity | Defect log |
| Release | A feature can only be released if exit criteria are met | Test summary report |

These governance rules support quality governance by making expectations explicit and
verifiable rather than relying on assumptions. Each rule is tied to concrete evidence
(a commit, a test result, a document) so that quality is not just claimed but can be
checked at any time. For example, the rule that "code must pass all unit tests before
commit" gives the team a clear gate to check before pushing changes, and the requirement
that AI-generated suggestions be reviewed ensures that GitHub Copilot supports the team's
judgement rather than