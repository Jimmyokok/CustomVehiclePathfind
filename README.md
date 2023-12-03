## Disclaimer

- This mod is experimental. It may affect *all vehicles* in the city and even *citizen's willingness to use vehicles* in your city. Please use at your own risk.

## CustomVehiclePathfind

- Configurable path-finding cost of *unsafe behaviors (violating intersection rules and making unsafe u-turns)* for vehicles. 

- Configurable path-finding cost of *lane changing* for vehicles. Higher cost makes vehicles more costly to change lanes during running. 
  - The effect is hardly observable under low lane changing cost, but devastating to your city if the lane changing cost is set to extremely high values.

- Configurable path-finding cost of *simply driving* for vehicles. Higher cost effectively reduces the usage of vehicles for citizens (cargo and service vehicles are not affected).

- All these function can be turned off.

## How it works

- The game makes a *pathfinding* calculation when deciding how to get from one location to another. Pathfinding is everywhere, affecting things like citizen and vehicle routes, cargo transportation and even building rent calculation.

- When doing *pathfinding*, the game finds an optimal path that minimizes the *pathfinding cost*. The total pathfinding cost is determined by the summation of the pathfinding cost of all moves/behaviors that constitute the entire path. Pathfinding cost of all moves/behaviors are pre-defined. Taking vehicles as examples, moves like *driving, turning, making u-turns, crossing lanes and breaking intersection rules* yield different pathfinding costs and impact the pathfinding decision.

- The mod alters these pre-defined pathfinding costs. Specifically, it makes pathfinding costs for *unsafe behaviors (violating intersection rules and making unsafe u-turns), lane changing and driving* configurable. High costs actually work like punishments and block vehicles from considering these options, forcing them to turn to some *cheaper* solutions encouraged by *you*.

- Explanations on the three configurable factors:
  - Factor *m_unsafe_punishment*： Modifies path-finding costs of unsafe behaviors. Under higher costs, vehicles are less likely to break rules. Setting it to extremely high value (> 1000) to forbid rule breaking. Recommended range: > 1000.
  - Factor *m_lane_punishment*： Modifies path-finding costs of lane changing. Under higher costs, vehicles change lanes less frequently. Setting it to high value is not recommended, as it would destroy your city's normal traffic flow. Recommended range: > 10 when really needed.
  - Factor *m_driving_punishment*： Modifies path-finding costs of vehicle driving. This is similar to changing a city's *fuel price*, as higher driving costs make citizen less likely to *travel by cars*. Setting it to extremely high value (> 1000) to disable personal car traffic, while cargo and service vehicles are not affected. Recommended range: 0.01-1.0 for slightly reduced traffic and > 1.0 for even less traffic.
  - Switches *enabled_unsafe_punishment, enabled_lane_punishment* and *enabled_driving_punishment* control if the above functions are active.

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
