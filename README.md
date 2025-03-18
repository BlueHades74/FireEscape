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

This README outlines the basic programming standards and naming conventions for this project, developed using C# within the Unity environment. Adhering to these guidelines ensures consistency, readability, and maintainability across the codebase. Please strictly follow these conventions to maintain uniformity.

## Naming Conventions

Consistent naming facilitates the identification of asset and script types and purposes. Below are the standards for naming different elements within this project.

### Game Objects

* **Format:** PascalCase (e.g., `FireExtinguisher`, `PlayerCharacter`).
* **Prefix:** Consider using prefixes to group related objects in the hierarchy (e.g., `NPC_Civilian`, `FX_Explosion`).

### Scripts

* **Format:** PascalCase (e.g., `PlayerMovement`, `FireController`).
* **File Name:** The script file name must match the class name.

### Sprites/Textures

* **Format:** lowercase with underscores (e.g., `player_idle`, `fire_particle`).

### Scenes

* **Prefix:** `Scene_`
* **Format:** PascalCase (e.g., `Scene_Level1`, `Scene_MainMenu`).

### Audio Clips

* **Prefix:** `Audio_`
* **Format:** PascalCase (e.g., `Audio_Jump`, `Audio_Explosion`).

### Materials

* **Prefix:** `Mat_`
* **Format:** PascalCase (e.g., `Mat_FloorTile`, `Mat_Fire`).

### Variables

* **Variables:** camelCase (e.g., `playerHealth`, `isJumping`).
* **Constants:** ALL_CAPS_WITH_UNDERSCORES (e.g., `MAX_HEALTH`, `JUMP_FORCE`).

## Code Formatting

Maintaining good formatting enhances code readability. Here's how to ensure cleanliness.

### Spacing

* Add spaces around operators and after commas.
* **Example:** `x = 5 + 3;` (not `x=5+3;`)

### Comments

* Use comments to explain complex logic or key sections.
* Keep comments concise and clear.
* Use `//` for single-line comments and `/* ... */` for multi-line comments.
* **Example:** `// Check if player is grounded before jumping`

### Regions

* Use `#region` and `#endregion` to group related code in scripts.
* **Example:**

    ```csharp
    #region Movement
    transform.Translate(Vector3.right * speed * Time.deltaTime);
    if (Physics2D.OverlapBox(transform.position, collisionSize, 0, wallLayer))
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    #endregion
    ```

### Initialization of Variables

* Initialize variables in the `Start()` or `Awake()` methods of your scripts.
* **Example:**

    ```csharp
    public class PlayerController : MonoBehaviour
    {
        public int MaxHealth = 100;
        private int _currentHealth;
        private bool _isJumping;

        void Start()
        {
            _currentHealth = MaxHealth;
            _isJumping = false;
        }
    }
    ```

### Constants

* Define constants using the `const` keyword for values that will not change at runtime.
* **Example:** `public const int MAX_HEALTH = 100;`

### Version Control

* Write clear and descriptive commit messages.
* Explain the changes made and the reasoning behind them.
* **Example:** `Added collision check for chair prefab to fix player overlap bug`

## Minimizing Merge Conflicts

To minimize merge conflicts, particularly when working with Unity's scene and project files, we recommend the following workflow:

1.  **Local Development:**
    * Each programmer should work on their own branch of the project.
    * Focus on specific features or bug fixes within your branch.
2.  **GitHub Repository:**
    * The `main` branch should represent the stable version of the game.
3.  **Workflow:**
    * **Pull First:** Before starting any work, always `git pull` the latest changes from the `main` branch to your local branch.
    * **Local Changes:** Make your changes within your local branch.
    * **Commit Frequently:** Commit your changes regularly with descriptive messages.
    * **Rebase or Merge:** When your feature is complete, rebase your branch onto the `main` branch (preferred for a clean history) or merge your branch into the `main` branch. Resolve any merge conflicts carefully.
    * **Descriptive Commits:** When committing, ensure commit messages are very descriptive, making it easier to identify the source of any issues.

## Additional Notes

* Refer to the [Unity Documentation](https://docs.unity3d.com/) for C# syntax and Unity API details.
* These standards are subject to evolution; please check back for updates.

## Timeline

* 3/12/2025 - Project started.
* 3/17/2015 - Groups assigned.

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
