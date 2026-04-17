# .NET Mastery — Learning Workspace

> Welcome back, future-Jose. This is where you're rebuilding yourself into a senior .NET engineer, one reference card at a time. Don't skip the reps.

---

## How to start a session

Open a new chat in this Claude project and say one of:

- **"Ready for 1.3 — async & concurrency."** *(you pick the topic)*
- **"I've got 60 minutes. What should I work on today?"** *(Claude picks based on the tracker)*
- **"Quiz me on what I've learned so far."** *(review mode — uses the reference library)*
- **"I need to draft a LinkedIn post."** *(weekly ship cadence)*

Claude has the curriculum and the whole `/reference/` library as context every session. It won't re-teach things you've already mastered.

---

## The folder structure

| Path | Purpose | Temperature |
|---|---|---|
| `dotnet-mastery-curriculum.md` | The tracker. Marks: `[ ] [~] [x] [★]` | 🔥 Hot — edit daily |
| `lessons/` | Full notes from each session | 🌤 Warm — re-readable |
| `reference/` | Tight one-page cards per concept | ❄️ Cold — grab-and-go |
| `code/` | .NET 10 file-based sandboxes | ⚙️ Executable |
| `blog-drafts/` | Posts not yet published | ✍️ Pre-publish |
| `linkedin/` | Short-form posts linking to blog | 📣 Public |
| `_templates/` | Scaffolds Claude reuses | 🔒 Don't edit |

---

## Session anatomy

1. **Pick topic.** Cite a sub-section (e.g. 1.3 or 2.4). No Googling yet.
2. **Pre-check.** 3 questions, honest answers. This is the diagnostic.
3. **Gap map.** Agreed focus for this session.
4. **Teach + probe.** Diagrams, metaphors, spot questions.
5. **Apply.** Code sandbox, or explain-it-back.
6. **Consolidate.** Build reference card together.
7. **Artifacts produced.**
   - **Always** → reference card in `/reference/`
   - **Often** → code sandbox in `/code/`
   - **~1–2×/week** → LinkedIn draft in `/linkedin/`
   - **Section complete** → blog draft in `/blog-drafts/`
8. **Update tracker.** Paste the updated lines into the curriculum file.

---

## The progress marks

| Mark | Meaning |
|---|---|
| `[ ]` | Not started |
| `[~]` | In progress — learning, not yet comfortable |
| `[x]` | Learned + artifact produced |
| `[★]` | Can explain with a diagram to a non-expert |

**Rule:** you don't get to use `[★]` on the resume-claim ladder until every item in a section has it.

---

## The reference library effect

Every reference card you produce lives in `/reference/` and is automatically visible to Claude in every future session. That means:

- In month 3, Claude cites *your* words on idempotency instead of reteaching it
- For interview prep, you scan `/reference/INDEX.md` to review your whole curriculum in your own voice
- The library **compounds**. By month 6 it's the most valuable file in your GitHub

---

## Rules of the road

1. **No topic is done until it has an artifact.** Reading doesn't count.
2. **Depth over breadth.** Better to know `IUnitOfWork` cold than 40 patterns shallow.
3. **Teach what you learn.** If you can't draw it for a junior, you don't know it.
4. **One discomfort rep per week.** Pick the topic you've been avoiding.
5. **Ship in public.** GitHub is your resume. Blog is your cover letter. LinkedIn is your beacon.

---

*Last updated: April 2026*
