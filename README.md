# Fire Escape

## Introduction

Fire Escape is a 2D game currently in development as a student project for the Fund Game Design Concepts class (GDD 2150 001) at UCCS. Players take on the role of a brave firefighter, tasked with rescuing civilians and their precious belongings from raging infernos. Developed to explore the game prompt of "Tragedy", Fire Escape aims to deliver a challenging and emotionally engaging experience.

## Created By

* Fund Game Design Concepts - 2025Sp GDD 2150 001 class at UCCS.

## Game Engine

* **Current Engine:** Unity

## Game Overview

Fire Escape is centered around firefighting and rescue. Players will navigate burning buildings to:

* Engage in Co-op gameplay, supporting 4 player and AI.
* Experience a Top-Down perspective.
* Save people and extinguish fires.

## Project Standards and Conventions

This README outlines the recommended programming standards and naming conventions for this project, developed using C# within the Unity environment. These guidelines are based on common C# and Unity practices to ensure consistency, readability, and maintainability across the codebase. Please adhere to these conventions.

### Naming Conventions

Consistent naming helps in understanding the codebase and navigating the project assets.

#### Game Objects

* **Format:** PascalCase (e.g., `PlayerCharacter`, `FireHydrant`, `RescueTarget`).
* **Organization:** Use empty GameObjects as folders in the hierarchy to group related objects (e.g., create an empty `Environment` object and place floor tiles, walls under it).

#### Scripts

* **Format:** PascalCase (e.g., `PlayerMovement`, `FireController`, `GameManager`).
* **File Name:** The script file name **must** match the public `MonoBehaviour` class name inside it for Unity to correctly attach the script.

#### Asset Files (Sprites, Textures, Audio Clips, Materials, etc.)

* **Format:** PascalCase or camelCase (e.g., `PlayerIdle`, `FireParticle`, `ButtonNormal`, `MainTheme`, `CharacterMaterial`).
* **Organization:** Use clear folder structures within your `Assets` directory (e.g., `Assets/Sprites/Characters`, `Assets/Audio/SFX`, `Assets/Materials`).

#### Scenes

* **Format:** PascalCase (e.g., `MainMenu`, `Level01`, `CharacterSelection`).

#### Variables and Methods

* **Public/Serialized Fields:** `camelCase` (e.g., `moveSpeed`, `playerHealth`). These are visible in the Unity Inspector.
* **Private/Protected Fields:** `_camelCase` (prefix with underscore) or `camelCase`. Using `_` helps distinguish private fields from local variables and public fields (e.g., `_currentHealth`, `_jumpForce`). Consistency within the project is key.
* **Method Parameters:** `camelCase` (e.g., `TakeDamage(int damageAmount)`).
* **Local Variables:** `camelCase` (e.g., `float timeElapsed = 0;`).
* **Methods:** PascalCase (e.g., `MovePlayer()`, `CalculateScore()`).
* **Properties:** PascalCase (e.g., `public int CurrentScore { get; private set; }`).
* **Constants:** `ALL_CAPS_WITH_UNDERSCORES` (e.g., `MAX_HEALTH`, `DEFAULT_SPEED`). Use `const` for compile-time constants and `static readonly` for runtime constants.

### Code Formatting

Clean formatting enhances readability and maintainability.

#### Spacing

* Use spaces around operators (`=`, `+`, `-`, `*`, `/`, `==`, etc.) and after commas.
* **Example:** `x = speed * Time.deltaTime;` (not `x=speed*Time.deltaTime;`)
* Use single spaces, not tabs, for indentation (configurable in your IDE, often set to 4 spaces).

#### Comments

* Use `//` for single-line comments.
* Use `/* ... */` for multi-line comments (less common than multiple `//`).
* Use `///` XML documentation comments for public methods, classes, and properties to explain their purpose, parameters, and return values. These integrate with IDE tooltips.
    ```csharp
    /// <summary>
    /// Makes the player character jump if grounded.
    /// </summary>
    /// <param name="jumpForce">The force to apply upwards.</param>
    public void Jump(float jumpForce)
    {
        // Check if player is grounded before jumping
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    ```
* Keep comments concise, clear, and focused on *why* something is done, rather than *what* it does (the code should explain the 'what').

#### Regions

* Use `#region` and `#endregion` sparingly. They can help organize very large classes, but often indicate that a class might be doing too much and could be refactored into smaller, more focused classes. Prefer smaller classes and methods over extensive use of regions.

### Initialization

* Initialize variables where they are declared if possible (e.g., `public float speed = 5.0f;`).
* Use `Awake()` primarily for getting component references (`GetComponent<T>()`) on the same GameObject or initializing state *before* any other script's `Start()` runs.
* Use `Start()` for initialization that might depend on other objects or components having already run their `Awake()` methods.

    ```csharp
    using UnityEngine;

    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100; // Initialized directly, adjustable in Inspector
        private int _currentHealth;
        private Rigidbody2D _rigidbody; // Reference to a component

        // Awake is called before the first frame update and before any Start methods
        void Awake()
        {
            // Good place for getting component references on the same GameObject
            _rigidbody = GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError("Rigidbody2D component missing from this GameObject");
            }
        }

        // Start is called before the first frame update, after all Awake methods have run
        void Start()
        {
            // Good place for setting initial state based on parameters or other objects
            _currentHealth = maxHealth;
        }
    }
    ```

### Constants

* Define compile-time constants using `const` (value must be known at compile time).
* Define runtime constants (or values set once at startup) using `static readonly`.
* **Example:**
    ```csharp
    public const int MAX_SCORE = 9999;
    public static readonly Color DEFAULT_COLOR = Color.blue;
    ```

## Contribution Workflow using Direct Git Commits

To manage contributions effectively, we will use a workflow combining direct Git commits on feature branches with GitHub for submission tracking and integration, alongside HacknPlan for task management and Clockify for time tracking. Programmers will commit their actual project changes directly to dedicated branches. The Programming Leads (Kenyou Teoh & Brian McLatchie) will then review and merge these branches into the main project repository (`main`), which they manage using Git and GitHub Desktop.

### Requirements

Before starting, ensure you have the following set up:

* **Git:** Installed on your system.
* **GitHub Account:** A personal GitHub account.
* **GitHub Desktop:** Installed and configured with your GitHub account.
* **Unity Hub & Unity Editor:** The correct version of Unity as specified for the project.
* **Project Access:** Clone access to the central GitHub repository for the project.
* **Clockify Account:** An account on Clockify and access to the team's workspace.
* **HacknPlan Account:** An account on HacknPlan and access to the team's project board.

### Programmer Workflow

1.  **Get Base Project & Setup:**
    * Obtain the latest version of the Unity project from the Programming Leads. This will typically involve **cloning** the central GitHub repository.
    * Ensure you have GitHub Desktop installed and configured with your GitHub account.
    * Make sure your local copy is up-to-date with the remote `main` branch before starting work (Use `Fetch origin` / `Pull origin` in GitHub Desktop and ensure your local `main` is synced with `origin/main`).

2.  **Identify Task & Create Your Branch:**
    * **Check HacknPlan:** Identify your assigned task(s) in HacknPlan. Understand the requirements and ensure the task status is moved to "In Progress" when you begin.
    * Using GitHub Desktop, create a **new branch** from the latest `main` branch. Name it descriptively, including your name and the feature/fix (e.g., `SophiaK-AddRescueCivilians`, `Brandon-FixFireSpreadBug`).
    * Switch to (`checkout`) your newly created branch. You will commit your code and asset changes directly to this branch.

3.  **Local Development & Time Tracking:**
    * Work on your assigned features or bug fixes within your local copy of the project (on your branch). Create/modify scripts, prefabs, scenes, assets, etc., following the project's standards.
    * **Track Your Time:** Start the Clockify timer for the corresponding HacknPlan task when you begin development work. Remember to stop/start the timer accurately as you work or take breaks.
    * **Save your work frequently in Unity.**

4.  **Commit Changes to Your Branch:**
    * Using GitHub Desktop, review the files listed under the "Changes" tab. These should reflect the assets, scripts, prefabs, scenes, and potentially project settings you created or modified.
    * **Crucially, ensure a proper `.gitignore` file is present and active in the repository root.** This prevents committing unnecessary temporary files, library data, build artifacts, logs, etc. If you are seeing unwanted files (like everything in the `Library` folder), check that your `.gitignore` file is correctly configured. You can refer to standard Unity `.gitignore` templates like the one found here: [github/gitignore/Unity.gitignore](https://github.com/github/gitignore/blob/main/Unity.gitignore). **Do not commit the `Library`, `Temp`, `Logs`, or `Build` folders.**
    * Stage the relevant files you want to include in the commit.
    * Commit the staged files to your branch with a clear, concise message describing the changes (e.g., `feat: Add rescue civilian functionality`, `fix: Correct fire spread bug calculation`). Commit frequently with small, logical changes.

5.  **Push Branch, Update Task Status & Notify Leads:**
    * Periodically (and definitely when your task is complete), **push** your branch (including your commits) to the central GitHub repository (`origin`) using GitHub Desktop (`Publish branch` or `Push origin` button).
    * **Update HacknPlan:** When your feature/fix is complete and pushed, update the corresponding task status in HacknPlan (e.g., move to "Ready for Review" or "Done", depending on your team's process). Ensure your time logged in Clockify is finalized for the task.
    * Notify one of the Programming Leads (Kenyou Teoh or Brian McLatchie) that your branch (`YourName-YourFeature`) is ready for review and merging. Provide the exact branch name and confirm the HacknPlan task has been updated.

6.  **Await Integration & Updates:**
    * The Programming Leads will review your branch (perform code review) and merge it into `main` when approved.
    * **Keep your branch updated:** Regularly fetch changes from `origin` (`Fetch origin` button) and merge the latest `main` branch into your feature branch (Switch to your branch, then use `Branch` -> `Update from main` or `Branch` -> `Merge into current branch` selecting `main`) to minimize future merge conflicts. Wait for confirmation from the Leads that significant changes have been merged into `main` before pulling updates if you are concerned about conflicts.

### Programming Lead Workflow (Overview)

1.  **Review Submitted Branches:** Monitor notifications or check GitHub/HacknPlan for branches/tasks marked as ready for review. Consider using GitHub Pull Requests for formal reviews if desired.
2.  **Review & Merge Branch:** Check out the programmer's submitted branch locally (`Fetch origin`, then find the branch under `Remote` or `Other branches` and check it out) or review the Pull Request on GitHub. Perform a code review, referencing the HacknPlan task for context. Once approved, merge the feature branch into the local `main` branch (ensure `main` is up-to-date first: `Fetch origin`, switch to `main`, `Pull origin` to ensure it matches `origin/main`). Use standard Git merge procedures (e.g., via GitHub Desktop: switch to `main`, then `Branch` -> `Merge into current branch...` -> select the feature branch; or command line `git merge YourName-YourFeature`).
3.  **Resolve Conflicts:** Handle any Git merge conflicts arising during the merge process. This often involves editing the conflicted files (scripts, `.unity` scenes, `.prefab` files viewed as text) to combine changes correctly. Communication with the programmer may be necessary.
4.  **Test Integration:** Thoroughly test the project in Unity after the merge to ensure the changes work correctly and haven't introduced regressions.
5.  **Push Updated `main`:** Once satisfied with the merge and testing, push the updated local `main` branch (including the merge commit) to the central GitHub repository (`origin`) using the `Push origin` button.
6.  **Distribute Updates & Update Tasks:** Inform programmers that the `main` branch has been updated, and they should pull the latest changes (`Fetch origin`/`Pull origin` on their `main` branch) and incorporate them into their ongoing work. Ensure the corresponding HacknPlan task is marked as "Closed" or moved to the appropriate final state.

### Minimizing Conflicts with Direct Commits

* **Communicate:** Crucial! Discuss plans with Leads and teammates **before** modifying shared assets (like scenes, prefabs used by multiple people) or core systems. **Use HacknPlan** to see who is assigned to which tasks and understand ongoing work.
* **Small, Focused Branches/Commits:** Keep branches focused on a single feature or bug fix (ideally one HacknPlan task). Commit small, logical changes frequently. This makes reviewing and merging easier.
* **Update Base Frequently:** Programmers **must** regularly fetch/pull the latest `main` branch (`Fetch origin` / `Pull origin` on `main`) and merge it into their feature branches. This integrates ongoing changes and allows resolving smaller conflicts sooner rather than facing large, complex conflicts later.
* **Lead Conflict Resolution:** Leads manage the final conflict resolution during the merge *into* the `main` branch, ensuring the stability of the primary codebase.

### Additional Notes

* Ensure you have access to the team's Clockify workspace and HacknPlan board.
* **If you lack access to any required tools/platforms (GitHub, Clockify, HacknPlan) or have questions about this workflow, please contact the Programming Leads (Kenyou Teoh & Brian McLatchie) on Discord.**
* Refer to the official [Unity User Manual](https://docs.unity3d.com/Manual/index.html) and [Scripting Reference](https://docs.unity3d.com/ScriptReference/index.html).
* Consult C# documentation for language features.
* These standards may evolve; always refer to the latest version of this document in the `main` branch.
* TL;DR: https://www.youtube.com/watch?v=LYdhjY0NFf4



## Timeline

* 2025-03-12: Project started.
* 2025-03-17: Groups assigned.

## Credits

* **Producer:** Joshua Grussendorf
* **Programming Lead:** Kenyou Teoh & Brian McLatchie
    * Tyler Austin
    * Gabe Wenchell
    * Rafael Gonzalez Atiles
    * Brandon Sith
    * Alex Workman
* **Design Lead:** Alex DeRooy
    * Sophia Kalua
    * Sebastian
    * Logan
    * Nicolas Suazo
* **Art Lead:** Nick Knehs
    * Elle Hoeper
    * Kaya
    * Star Grace Carpenter
* **Sound Lead:** Henry C
    * Kalista Hough
    * Treyvonn
    * Ryan M

