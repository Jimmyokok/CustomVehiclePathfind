## Disclaimer

- This mod is experimental. It may affect *all vehicles* in the city and even *citizen's willingness to use vehicles* in your city. Please use at your own risk.

## CustomVehiclePathfind

- Configurable path-finding cost of *unsafe behaviors (violating intersection rules and making unsafe u-turns)* for vehicles. Higher cost restricts vehicles from making these moves.
  - Recommended value: 0 for everybody breaking rules, 10000 or larger to forbid rule-breaking.

- Configurable path-finding cost of *lane changing* for vehicles. Higher cost makes vehicles more costly to change lanes during running. 
  - The effect is hardly observable under low lane changing cost, but devastating to your city if the lane changing cost is set to extremely high values.

- Configurable path-finding cost of *simply driving* for vehicles. Higher cost effectively reduces the usage of vehicles for citizens (cargo and service vehicles are not affected).
  - Recommended value: 0.01 is the default value of the game, 1.0 results in significantly reduced vehicle traffic.

- All these function can be turned off.

## Configuring the Setting

- First launch the game with this mod loaded, and close the game (to generate the configuration file).
- Open the configuration file (..\Cities Skylines II\BepInEx\config\CustomVehiclePathfind.cfg), modify each configuration entries and save the file.
- Now launch the game again with this mod loaded and enjoy it!

## Requirements

- Game version equals or below 1.0.15f1.
- BepInEx 5

## Planned Features

- In-game configuration.
- Configurable path-finding cost of more vehicle behaviors.

## Credits

- [Captain-Of-Coit](https://github.com/Captain-Of-Coit/cities-skylines-2-mod-template): A Cities: Skylines 2 mod template.
- [BepInEx](https://github.com/BepInEx/BepInEx): Unity / XNA game patcher and plugin framework.
- [Harmony](https://github.com/pardeike/Harmony): A library for patching, replacing and decorating .NET and Mono methods during runtime.
- [CSLBBS](https://www.cslbbs.net): A chinese Cities: Skylines 2 community, for extensive test and feedback efforts.
