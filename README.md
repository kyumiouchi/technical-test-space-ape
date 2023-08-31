# Space Ape "Dodgy Boxes" Client Developer Technical Test

## Introduction

First off, thanks so much for taking the time to try our technical test. At Space Ape we value fun, responsive, mobile-only games and we find one of the best ways to learn about your values and skills is to get a little glimpse at the way you work.

We have provided you with the shell of a very simple dodging game, as well as selection of art and audio assets.

There are two exercises, on which you should spend **no more than 5 hours in total**:

### Game Development Task

We would like you to do something within this shell which impresses us, and plays to our core values. Feel free to change the version of Unity, add assets, and use third party plugins so long as they are called out.

**You do not need to build a whole game**. Just focus on improving or adding one thing to the point where you feel it shows off your skills.

To give you direction, When we review your submission we will be looking for you to demonstrate understanding of:
* User experience
* Feedback
* Game feel
* Code quality

### Bug Fixing Task

There is also a small bug in the `GameCamera` class. We would like you to write a test which identifies this issue, and fix the code.

### Returning your Test

When you return your test, please put your name in the file description so it's clear who submitted it.

If you have any questions, please feel free to contact our recruiters.

## Project Summary and Notes

The game is structured as a standard Unity project. All of the game classes are part of the `DodgyBoxes` assembly. There is also a `Tests` assembly with a single passing test.

Inside the Assets folder you will find:
* Prefabs - A single prefab for the enemy game object
* Scenes - The main scene
* Scripts - All C# scripts, and tests
* Sounds - Library of wav files available to use
* Sprites - Library of png files available to use

The game is **intended to be played in a 9:16 portrait aspect ratio**. You will need to set this manually otherwise the game view in editor will be incorrect. Sadly Unity doesn't make it possible to share this setting with you.

There is only one scene `Main` which contains all of the game objects used throughout the game.

The entry point of the game is `Controller.cs` which is responsible for setting up, and managing the game state.
