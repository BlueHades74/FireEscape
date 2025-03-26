# Fire Escape

## Introduction

Fire Escape is a 2D game currently in development as a student project for the Fund Game Design Concepts class (GDD 2150 001) at UCCS. Players take on the role of a brave firefighter, tasked with rescuing civilians and their precious belongings from raging infernos. Developed to explore the game prompt of "Tragedy", Fire Escape aims to deliver a challenging and emotionally engaging experience.

## Created By

* Fund Game Design Concepts - 2025Sp GDD 2150 001 class at UCCS.

## Game Engine

* **Current Engine:** Unity

## Game Overview

Fire Escape is centered around firefighting and rescue. Players will navigate burning buildings to:

* Engage in Split-screen Couch Co-op gameplay, supporting 2 players or a player and AI.
* Experience a Top-Down perspective.
* Save people and extinguish fires.

# Project Standards and Conventions

This README outlines the recommended programming standards and naming conventions for this project, developed using C# within the Unity environment. These guidelines are based on common C# and Unity practices to ensure consistency, readability, and maintainability across the codebase. Please adhere to these conventions.

## Naming Conventions

Consistent naming helps in understanding the codebase and navigating the project assets.

### Game Objects

* **Format:** PascalCase (e.g., `PlayerCharacter`, `FireHydrant`, `RescueTarget`).
* **Organization:** Use empty GameObjects as folders in the hierarchy to group related objects (e.g., create an empty `Environment` object and place floor tiles, walls under it).

### Scripts

* **Format:** PascalCase (e.g., `PlayerMovement`, `FireController`, `GameManager`).
* **File Name:** The script file name **must** match the public `MonoBehaviour` class name inside it for Unity to correctly attach the script.

### Asset Files (Sprites, Textures, Audio Clips, Materials, etc.)

* **Format:** PascalCase or camelCase (e.g., `PlayerIdle`, `FireParticle`, `ButtonNormal`, `MainTheme`, `CharacterMaterial`).
* **Organization:** Use clear folder structures within your `Assets` directory (e.g., `Assets/Sprites/Characters`, `Assets/Audio/SFX`, `Assets/Materials`).

### Scenes

* **Format:** PascalCase (e.g., `MainMenu`, `Level01`, `CharacterSelection`).

### Variables and Methods

* **Public/Serialized Fields:** `camelCase` (e.g., `moveSpeed`, `playerHealth`). These are visible in the Unity Inspector.
* **Private/Protected Fields:** `_camelCase` (prefix with underscore) or `camelCase`. Using `_` helps distinguish private fields from local variables and public fields (e.g., `_currentHealth`, `_jumpForce`). Consistency within the project is key.
* **Method Parameters:** `camelCase` (e.g., `TakeDamage(int damageAmount)`).
* **Local Variables:** `camelCase` (e.g., `float timeElapsed = 0;`).
* **Methods:** PascalCase (e.g., `MovePlayer()`, `CalculateScore()`).
* **Properties:** PascalCase (e.g., `public int CurrentScore { get; private set; }`).
* **Constants:** `ALL_CAPS_WITH_UNDERSCORES` (e.g., `MAX_HEALTH`, `DEFAULT_SPEED`). Use `const` for compile-time constants and `static readonly` for runtime constants.

## Code Formatting

Clean formatting enhances readability and maintainability.

### Spacing

* Use spaces around operators (`=`, `+`, `-`, `*`, `/`, `==`, etc.) and after commas.
* **Example:** `x = speed * Time.deltaTime;` (not `x=speed*Time.deltaTime;`)
* Use single spaces, not tabs, for indentation (configurable in your IDE, often set to 4 spaces).

### Comments

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

### Regions

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

## Contribution Workflow using Unity Packages

To manage contributions effectively while managing repository size, we will use a workflow combining Unity Packages (`.unitypackage`) with Git/GitHub for submission tracking. Programmers will submit their work as packages committed to dedicated branches. The Programming Leads (Kenyou Teoh & Brian McLatchie) will then integrate these packages into the main project repository, which they manage using Git and GitHub Desktop.

### Programmer Workflow

1.  **Get Base Project & Setup:**
    * Obtain the latest version of the Unity project from the Programming Leads (e.g., download a ZIP from a shared link, or clone the central GitHub repository if instructed, but remember you **will not** push code changes directly to `main`).
    * Ensure you have GitHub Desktop installed and configured with your GitHub account.
    * If you cloned the repository, make sure your local copy is up-to-date with the `main` branch before starting work.
2.  **Create Your Branch:**
    * Using GitHub Desktop, create a **new branch** from the latest `main` branch. Name it descriptively, including your name and the feature/fix (e.g., `SophiaK/AddRescueCivilians`, `Brandon/FixFireSpreadBug`).
    * Switch to your newly created branch. You will commit your package to this branch.
3.  **Local Development:**
    * Work on your assigned features or bug fixes within your local copy of the project (on your branch). Create/modify scripts, prefabs, scenes, etc., following the project's standards.
4.  **Export Changes as `.unitypackage`:**
    * When your task is complete and tested locally, export **only your changes** as a `.unitypackage` file.
    * In Unity: `Assets -> Export Package...`.
    * **Carefully select** only the assets you **created or modified**.
    * **Crucially, check "Include dependencies"**.
    * Do **NOT** export the entire project or standard Unity packages.
    * Name your package descriptively (e.g., `SophiaK_RescueCivilians_v1.unitypackage`).
5.  **Commit Package to Your Branch:**
    * Create a specific folder in your local project structure for submitting packages if one doesn't exist (e.g., `Assets/_PackagesForReview/YourName/`). Discuss the exact location with the leads. **This folder must NOT be in the project's `.gitignore` file.**
    * Place your exported `.unitypackage` file into this designated folder.
    * Using GitHub Desktop, you should see the new package file as an uncommitted change.
    * Stage the `.unitypackage` file.
    * Commit **only the `.unitypackage` file** to your branch with a clear message indicating the purpose of the package (e.g., `feat: Add rescue civilian functionality package`, `fix: Package for fire spread bug`).
6.  **Push Branch & Notify Leads:**
    * Using GitHub Desktop, **push** your branch (including the commit with the `.unitypackage`) to the central GitHub repository (`origin`).
    * Notify one of the Programming Leads (Kenyou Teoh or Brian McLatchie) that your branch (`YourName/YourFeature`) is ready for review and contains the `.unitypackage` for integration. Provide the branch name.
7.  **Await Integration & Updates:** The Programming Leads will review your branch, download the package, and integrate it. Wait for updated versions of the base project from the leads before starting tasks dependent on recent changes.

### Programming Lead Workflow (Overview)

1.  **Review Submitted Branches:** Monitor notifications or check GitHub for branches pushed by programmers that are ready for review.
2.  **Download & Integrate Package:** Check out the programmer's submitted branch locally or browse it on GitHub. Download the `.unitypackage` file from the designated folder within that branch. Switch back to the local `main` branch (ensure it's up-to-date) and import the package (`Assets -> Import Package -> Custom Package...`).
3.  **Resolve Conflicts:** Handle any conflicts arising during import (scripts, prefabs, scenes). This might require communication with the programmer.
4.  **Test Integration:** Thoroughly test the project to ensure the imported package works correctly and doesn't break existing functionality.
5.  **Commit Integrated Changes to `main`:** Once satisfied, commit the .unitypackage to the `main` branch using descriptive messages via GitHub Desktop. Push the updated `main` branch.
6.  **Distribute Updates:** Periodically inform programmers how to get the updated base project (e.g., by pulling the latest `main` branch if they cloned, or by providing a new download link).

### Minimizing Conflicts with Packages

* **Communicate:** Crucial! Discuss plans with Leads and teammates **before** modifying shared assets. Knowing who is working on what helps prevent conflicting packages.
* **Small, Focused Packages:** Submit smaller packages addressing single features or bugs.
* **Update Base Frequently:** Programmers should try to work from the most recent base project version provided by the leads to reduce divergence.
* **Lead Conflict Resolution:** Leads manage the final conflict resolution during integration into the `main` branch.

## Additional Notes

* Refer to the official [Unity User Manual](https://docs.unity3d.com/Manual/index.html) and [Scripting Reference](https://docs.unity3d.com/ScriptReference/index.html).
* Consult C# documentation for language features.
* These standards may evolve; check this document for updates.

## Timeline

* 3/12/2025 - Project started.
* 3/17/2025 - Groups assigned.

## Credits

**Producer - Joshua Grussendorf**

**Programming Lead - Kenyou Teoh & Brian McLatchie**
- Tyler Austin
- Gabe
- Rafael
- Brandon
- Alex

**Design Lead - Alex DeRooy**
- SophiaK
- Sebastian

**Art Lead - Nick Knehs**
- Elle
- Kaya
- Star Grace

**Sound Lead - Henry C**
- Kalista
- Mono
- Ryan M
- Nico
