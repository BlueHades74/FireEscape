# Fire Escape

## Introduction

Fire Escape is a 2D game currently in development, created as a student project for the Fund Game Design Concepts class (GDD 2150 001) at UCCS. This game puts you in the boots of a brave firefighter, tasked with rescuing civilians and their precious belongings from raging infernos.  Developed to explore the game prompt of "Tragedy", Fire Escape aims to deliver a challenging and emotionally engaging experience.

## Created By

*   Fund Game Design Concepts - 2025Sp GDD 2150 001 class in UCCS.

## Game Engine

*   **Current Engine:** GameMaker 2
*   **Potential Future Engine:** Unity (Consideration for future development)

## Game Overview

Fire Escape is a game centered around the theme of firefighting and rescue. Players will take on the role of a firefighter and navigate through burning buildings to:

*   **Save People:**  Your primary objective is to locate and safely extract civilians trapped within the fire.
*   **Rescue Belongings:** In addition to saving lives, you will also be tasked with recovering valuable belongings that people desperately want to save from the flames.
*   **Tragedy Theme:**  The game is designed to explore the "Tragedy" prompt, aiming to create a compelling narrative and gameplay that reflects the challenging and often heartbreaking realities faced by firefighters.

# Project Standards and Conventions

This README outlines the basic programming standards and naming conventions for this project, developed using GameMaker Language (GML). Following these guidelines ensures consistency, readability, and maintainability across the codebase. Please stick to these conventions strictly to keep everything uniform.

## Naming Conventions

Consistent naming makes it easy to identify the type and purpose of assets and variables. Below are the standards for naming different elements in this project.

### Objects

* **Prefix:** `obj_`
* **Format:** Use lowercase letters, with underscores to separate words.
* **Examples:** `obj_chair`, `obj_chair_large`, `obj_player`

### Sprites

* **Prefix:** `spr_`
* **Format:** Use lowercase letters, with underscores to separate words.
* **Examples:** `spr_chair`, `spr_chair_large`, `spr_player_idle`

### Scripts

* **Format:** Use descriptive names in lowercase, with underscores to separate words.
* **Examples:** `move_player`, `calculate_score`, `update_health`

### Rooms

* **Prefix:** `rm_`
* **Format:** Use lowercase letters, with underscores to separate words.
* **Examples:** `rm_level1`, `rm_boss_fight`, `rm_main_menu`

### Sounds

* **Prefix:** `snd_`
* **Format:** Use lowercase letters, with underscores to separate words.
* **Examples:** `snd_jump`, `snd_explosion`, `snd_background_music`

### Fonts

* **Prefix:** `fnt_`
* **Format:** Use lowercase letters, with underscores to separate words.
* **Examples:** `fnt_main`, `fnt_title`, `fnt_score`

### Timelines

* **Prefix:** `tml_`
* **Format:** Use lowercase letters, with underscores to separate words.
* **Examples:** `tml_intro`, `tml_cutscene1`, `tml_game_over`

### Paths

* **Prefix:** `pth_`
* **Format:** Use lowercase letters, with underscores to separate words.
* **Examples:** `pth_enemy_patrol`, `pth_player_movement`, `pth_boss_route`

### Variables

* **Format:** Use descriptive names in lowercase, with underscores to separate words.
* **Examples:** `player_health`, `is_jumping`, `enemy_speed`

## Code Formatting

Good formatting improves code readability. Here’s how to keep it clean.

### Indentation

* Use 4 spaces for indentation.
* Stay consistent across all scripts and objects.

### Spacing

* Add spaces around operators and after commas.
* **Example:** `x = 5 + 3;` (not `x=5+3;`)

### Comments

* Use comments to explain tricky logic or key sections.
* Keep them short and clear.
* **Example:** `// Check if player is on ground before jumping`

### Regions

* Use `#region` and `#endregion` to group related code in scripts.
* **Example:**

    ```gml
    #region Movement
    x += speed;
    if (place_meeting(x, y, obj_wall)) {
        x -= speed;
    }
    #endregion
    ```

## Best Practices

These tips will help keep your code efficient and organized.

### Use of Parent Objects

* Use parent objects for shared properties or behaviors.
* Name them with a `_parent` suffix.
* **Example:** `obj_enemy_parent`

### Initialization of Variables

* Set up instance variables in the Create event of each object.
* **Example:**

    ```gml
    player_health = 100;
    is_jumping = false;
    ```

### Constants

* Define constants for repeated or meaningful values.
* **Example:** `const MAX_HEALTH = 100;`

## Minimizing Merge Conflicts (GML Specific)

Due to the nature of GameMaker Language and its project files, merge conflicts can be particularly challenging. To minimize these conflicts, we recommend the following workflow:

1.  **Local Development:**
    * Each developer should maintain their own separate, local GameMaker project file for active development.
    * This file is where all coding and asset creation occurs.
2.  **GitHub Repository:**
    * A single, shared GameMaker project file should reside in the GitHub repository. This is the "main" project.
3.  **Workflow:**
    * **Pull First:** Before making any changes, always `git pull` the latest version of the repository to your local machine.
    * **Local Changes:** Make your changes in your local GameMaker project.
    * **Create Local Package:** Once your changes are complete and tested, create a local package (.yyz) of your project. This will export your project into a single file.
    * **Import into GitHub File:** Open the "main" GameMaker project file from the GitHub repository. Import the local package into this file. This will merge your changes into the shared project.
    * **Commit and Push:** Commit and push the updated "main" GameMaker project file to the GitHub repository.
    * **Descriptive Commits:** when committing, make sure that the commit messages are very descriptive, so that if a problem occurs, it is easy to find.

This workflow minimizes merge conflicts because:

* **Isolated Development:** Developers work in their own environments, reducing the chance of simultaneous edits to the same files.
* **Package Merging:** Importing a package into the main project handles most merging automatically, reducing manual conflict resolution.
* **Single Point of Integration:** the github file becomes the single point of integration, making it easier to track changes.


### Version Control

* Write clear, descriptive commit messages.
* Explain what you changed and why.
* **Example:** `Added collision check for obj_chair to fix player overlap bug`

## Additional Notes

* Refer to the [GameMaker Documentation](https://manual.yoyogames.com/#t=Content.htm) for GML syntax or function details.
* These standards might evolve—check back for updates!


## Timeline

*   3/12/2025 - Start the porject.

## Credits

**Producer - Joshua Grussendorf**
-


**Programmer Lead - Kenyou Teoh & Brian McLatchie**
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
