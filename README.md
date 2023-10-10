# Tomato Farm Environment
This package is a tomato farm simulator compatible with both Gazebo Sim and Gazebo Classic in ROS2 for ROS-based agricultural environments. This repository consists of four key parts

1) Random tomato farm generator
2) Simulation of the generated tomato farms in Gazebo Classic and Gazebo Sim

Gazebo Classic 

<img src="docs/GazeboClassic_Farm01.png" width="400" > <img src="docs/GazeboClassic_Farm02.png" width="400" >

<!-- ![Gazebo Classic - Tomato Farm Entire Field](docs/GazeboClassic_Farm01.png?raw=true )
![Gazebo Classic - Tomato Farm Close-up View](docs/GazeboClassic_Farm02.png?raw=true )-->

Gazebo Sim

<!-- ![Gazebo Sim - Tomato Farm Entire Field](docs/GazeboSim_Farm01.png?raw=true )
![Gazebo Sim - Tomato Farm Close-up View](docs/GazeboSim_Farm02.png?raw=true )-->

<img src="docs/GazeboSim_Farm01.png" width="400" > <img src="docs/GazeboSim_Farm02.png" width="400" >

# Environment Template
1) Placeholders for specific map files
2) Generators to construct missing map files
3) Procedural generation tools for robustness evaluation maps
4) Compilation setups to make map file references easier

# Public Environment
To create a publically available environment, create a new branch on a fork of the repository. A PR can be used to include your public environment into the upstream here

# Private Environment
Some environments may be requested by owners to be kept private. If this is the case, you can create your own repository using the `Use This Template` button, then set your new respository to private. Please note: it is advised you only do this if you must. Pulling updates from a template's source can become tedious and may create problems for your workflow

