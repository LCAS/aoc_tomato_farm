## aoc_gazebo_dogtooth
Agri-OpenCore Gazebo models for robot platforms by Dogtooth Technologies

### Getting Started

Before you begin, please ensure you have the necessary ROS 2 control dependencies installed. If not, you can install them using the following commands:

```bash
sudo apt install ros-humble-xacro
sudo apt install ros-humble-ros2-control
sudo apt install ros-humble-ros2-controllers
sudo apt install ros-humble-gazebo-ros2-control
```

### Building the Package

To get started, follow these steps:

1. Clone the package into your ROS 2 workspace's source directory (`<YOUR_ROS2_WS>/src/`).
2. Build the package using the `colcon` build system with the following command:

```bash
colcon build --symlink-install 
```

3. Source the workspace setup file:

```bash
source install/setup.bash
```

### Launching the Simulation

To launch the Gazebo simulation, use the following command:

```bash
ros2 launch dogtooth_gazebo dogtooth_001_world.launch.py
```

For teleoperating the robot using keyboard input, execute the following command:

```bash
ros2 run teleop_twist_keyboard teleop_twist_keyboard --ros-args --remap /cmd_vel:=/diff_drive_controller/cmd_vel_unstamped
```

![Gazebo Simulation](docs/dogtooth_rviz_gazebo.png)

