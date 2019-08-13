<div align="center">
<h1>GD-Edit</h1>
      <a href="https://ci.appveyor.com/project/AltenGD/gd-edit"><img src="https://ci.appveyor.com/api/projects/status/rr383gfmmby75c2p?svg=true" alt="Join Discord Server"/></a>
      
[![GitHub license](https://img.shields.io/github/license/gd-edit/GDE.svg?style=flat-square)](https://github.com/gd-edit/GDE/blob/master/LICENSE) 
[![GitHub stars](https://img.shields.io/github/stars/gd-edit/GDE.svg?style=flat-square)](https://github.com/gd-edit/GDE/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/gd-edit/GDE.svg?style=flat-square)](https://github.com/gd-edit/GDE/network)
[![GitHub issues](https://img.shields.io/github/issues/gd-edit/GDE.svg?style=flat-square)](https://github.com/gd-edit/GDE/issues)
</div>

---

# Contributing

Aside from code performance and functionality, we care about [Code Style](CodeStyle.md) as well, it's important that you check it out, as during PR approvals abiding to those rules is also taken into consideration. Refusing to comply with those will result in forced pushes from project admins.

# Features to be added:

## General

- Basic editor functionality
- Edit common properties of all objects
- Custom files for the editor; allows creators to have features specific to this tool saved
- Autosave
- Unlimited custom objects
- The program warns but doesn't limit going above the object limit (applies for both 40,000 and 80,000)
- Add-ons and custom scripts
- In game timer / time in editor
- Next free layer button

## Functions

- Some sort of 'for loop'-like thing to place/edit/delete a lot of the same action over and over with different groups (probably even within triggers), colors, etc.
- Automatic block variation
- Looping trigger module
- Option for randomised block rotation and scale for placing objects
- Scattering objects or object groups randomly over a specified area
- Speed control (.50x, .25x, etc)
- Replace object(s) with other object(s)
- Linking object data like id's and colours to other objects
- Proportional editing (https://bit.ly/2z2UQLe) with the following types:
- - Linear
- - Sharp
- - Constant (to move multiple objects at once)
- - Sphere
- - Random
- Pivot point dropdown (https://streamable.com/22ouq https://bit.ly/2JhnrRG)
- Smart Replace (https://bit.ly/2JkafeS)

## User Interface

- RGBA/HEX color selector with the ability to type in the specific numbers
- Color trigger easing preset
- Newgrounds song browser
- Better trigger organization
- Ability to open a trigger menu as a window from the properties panel
- Ability to add notes inside the level

## Scene view

- Camera indicator when playtesting. Moves like the in-game camera. Different options for monitor resolutions and styles: corner indicators, white border, hide outside (Covers everything but what the player would be able to see)

## Settings

- Ability to customize keybindings
- Dark mode
- Automatic updates

## Selection

- Filters for editing, deleting, selecting and visibility (by group, object type etc.)
- Selecting objects that belong in the currently selected triggers' target group IDs
- Selecting an object and choosing select linked. This pops up a menu that allows you to select everything of the same type (color, group, rotation, editor layer, actual layer, object type)
- Randomized selection over an area

## Triggers

- Snap triggers to guidelines

## Interaction

- Scroll -> vertical scroll
- Shift+Scroll -> horizontal scroll
- Ctrl+Scroll -> zoom
- Middle Click Down -> moving the scene ?
- When you perform an action such as scale(s) or rotate(r) with their keyboard shortcuts, you can type the rotation or scale value in.
- Right Click -> Delete

## Scripts

- Automatic circle generator
- Custom scripting (currently planning for C# and *miniC#*)

### And a whole lot more

<div align="center">
    <a href="https://discord.gg/cq2FKbb"><img src="https://canary.discordapp.com/api/guilds/467885469108142100/widget.png?style=banner2" alt="Join Discord Server"/></a>
</div>
