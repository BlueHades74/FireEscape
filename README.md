# Fire Escape

## Introduction

Fire Escape is a 2D game currently in development, created as a student project for the Fund Game Design Concepts class (GDD 2150 001) at UCCS. This game puts you in the boots of a brave firefighter, tasked with rescuing civilians and their precious belongings from raging infernos. Developed to explore the game prompt of "Tragedy", Fire Escape aims to deliver a challenging and emotionally engaging experience.

## Created By

* Fund Game Design Concepts - 2025Sp GDD 2150 001 class in UCCS.

## Game Engine

* **Current Engine:** Unity

## Game Overview

Fire Escape is a game centered around the theme of firefighting and rescue. Players will take on the role of a firefighter and navigate through burning buildings to:

* Split-screen Couch coop game, with 2 players or player and AI.
* Top-Down perspective.
* Save people, put out fires.

# Project Standards and Conventions

This README outlines the basic programming standards and naming conventions for this project, developed using C# within the Unity environment. Following these guidelines ensures consistency, readability, and maintainability across the codebase. Please stick to these conventions strictly to keep everything uniform.

## Naming Conventions

Consistent naming makes it easy to identify the type and purpose of assets and scripts. Below are the standards for naming different elements in this project.

### Game Objects

* **Format:** Use PascalCase (e.g., `FireExtinguisher`, `PlayerCharacter`).
* **Prefix:** Consider using prefixes to group related objects in the hierarchy (e.g., `NPC_Civilian`, `FX_Explosion`).

### Scripts

* **Format:** Use PascalCase (e.g., `PlayerMovement`, `FireController`).
* **File Name:** The script file name should match the class name.

### Sprites/Textures

* **Format:** Use lowercase letters, with underscores to separate words (e.g., `player_idle`, `fire_particle`).
* **Prefix:** Consider using prefixes to group related objects in the hierarchy (e.g., `sprite_player_idle`, `sprite_fire_particle`).

### Scenes

* **Prefix:** `Scene_`
* **Format:** Use PascalCase (e.g., `Scene_Level1`, `Scene_MainMenu`).

### Audio Clips

* **Prefix:** `Audio_`
* **Format:** Use PascalCase (e.g., `Audio_Jump`, `Audio_Explosion`).

### Materials

* **Prefix:** `Mat_`
* **Format:** Use PascalCase (e.g., `Mat_FloorTile`, `Mat_Fire`).

### Variables

* **Variables:** Use camelCase (e.g., `playerHealth`, `isJumping`).
* **Constants:** Use all uppercase with underscores to separate words (e.g., `MAX_HEALTH`, `JUMP_FORCE`).

## Code Formatting

Good formatting improves code readability. Here’s how to keep it clean.

### Spacing

* Add spaces around operators and after commas.
* **Example:** `x = 5 + 3;` (not `x=5+3;`)

### Comments

* Use comments to explain complex logic or key sections.
* Keep them short and clear.
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

* Define constants using the `const` keyword for values that won't change at runtime.
* **Example:** `public const int MAX_HEALTH = 100;`

### Version Control

* Write clear, descriptive commit messages.
* Explain what you changed and why.
* **Example:** `Added collision check for chair prefab to fix player overlap bug`

## Minimizing Merge Conflicts

To minimize merge conflicts, especially when working with Unity's scene and project files, we recommend the following workflow:

1.  **Local Development:**
    * Each programmer should work on their own branch of the project.
    * Focus on specific features or bug fixes within your branch.
2.  **GitHub Repository:**
    * The main branch should represent the stable version of the game.
3.  **Workflow:**
    * **Pull First:** Before starting any work, always `git pull` the latest changes from the main branch to your local branch.
    * **Local Changes:** Make your changes within your local branch.
    * **Commit Frequently:** Commit your changes regularly with descriptive messages.
    * **Rebase or Merge:** When your feature is complete, rebase your branch onto the main branch (preferred for a clean history) or merge your branch into the main branch. Resolve any merge conflicts carefully.
    * **Descriptive Commits:** When committing, make sure that the commit messages are very descriptive, so that if a problem occurs, it is easy to find.

## Additional Notes

* Refer to the [Unity Documentation](https://docs.unity3d.com/) for C# syntax or Unity API details.
* These standards might evolve—check back for updates!


## Timeline

*   3/12/2025 - Start the porject.

## Credits

**Producer - Joshua Grussendorf**
-


**Programmer Lead - Kenyou Teoh & Brian McLatchie**
- Tyler Austin
- Gabe
- Rafael
-
-
-
-
-
-
-

**Design Lead - Alex**
-
-
-
-
-
-
-
-
-
-

**Art Lead - Nick**
-
-
-
-
-
-
-
-
-
-

**Music Lead - Henry**
-
-
-
-
-
-
-
-
-
-
