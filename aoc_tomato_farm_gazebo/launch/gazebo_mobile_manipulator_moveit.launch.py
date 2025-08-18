#!/usr/bin/env -S ros2 launch
"""Configure and setup Gazebo mobile manipulator"""

from launch import LaunchDescription
from launch.actions import DeclareLaunchArgument, ExecuteProcess, IncludeLaunchDescription, RegisterEventHandler, SetEnvironmentVariable
from launch.event_handlers import OnProcessExit
from launch.launch_description_sources import PythonLaunchDescriptionSource
from launch.substitutions import Command, EnvironmentVariable, FindExecutable, LaunchConfiguration, PathJoinSubstitution
from launch_ros.actions import Node
from launch.conditions import IfCondition

from launch_ros.substitutions import FindPackageShare

from ament_index_python.packages import get_package_share_directory

from pathlib import Path
import os
from os import path

import yaml
from typing import List

ARGUMENTS = [DeclareLaunchArgument('world_path', default_value='', description='The world path, by default is empty.world'),]

def generate_launch_description():
    # Declare all launch arguments
    declared_arguments = generate_declared_arguments()

    os.environ["ROBOT_NAME"] = "mobile_manipulator_001"
    ROBOT_NAME = os.environ['ROBOT_NAME']

    world_path = LaunchConfiguration('world_path')
    # Launch configuration variables specific to simulation
    use_sim_time = LaunchConfiguration('use_sim_time', default='true')
    x_pose = LaunchConfiguration('x_pose', default='0.0')
    y_pose = LaunchConfiguration('y_pose', default='0.0')
    z_pose = LaunchConfiguration('z_pose', default='0.05')
    roll = LaunchConfiguration('roll', default='0.0')
    pitch = LaunchConfiguration('pitch', default='0.0')
    yaw = LaunchConfiguration('yaw', default='0.0')
    gazebo_verbose = LaunchConfiguration('gazebo_verbose', default='false')
    rviz_config = LaunchConfiguration("rviz_config")

    log_level = LaunchConfiguration("log_level")

    enable_servo = LaunchConfiguration("enable_servo")

    moveit_config_package = "franka"

    config_dogtooth_franka_velocity_controller = PathJoinSubstitution(
        [FindPackageShare("dogtooth_control"), "config", "control.yaml"]
    )

    # Get URDF via xacro
    robot_description_content = Command(
        [
            PathJoinSubstitution([FindExecutable(name="xacro")]),
            " ",
            PathJoinSubstitution(
                [FindPackageShare("dogtooth_description"), "urdf", ROBOT_NAME + ".urdf.xacro"]
            ),
            " ",
            "name:=dogtooth",
            " ",
            "prefix:=''",
            " ",
            "is_sim:=true",
            " ",
            "gazebo_controllers:=",
            config_dogtooth_franka_velocity_controller,
        ]
    )
    robot_description = {"robot_description": robot_description_content}

    # SRDF
    _robot_description_semantic_xml = Command(
        [
            PathJoinSubstitution([FindExecutable(name="xacro")]),
            " ",
            PathJoinSubstitution(
                [
                    FindPackageShare(moveit_config_package),
                    "srdf",
                    "panda.srdf.xacro",
                ]
            ),
            " ",
            "name:=panda",
            " ",
            "prefix:=panda_",
        ]
    )
    robot_description_semantic = {
        "robot_description_semantic": _robot_description_semantic_xml
    }

    # Kinematics
    kinematics = load_yaml_file(
        moveit_config_package, path.join("config", "kinematics.yaml")
    )

    # Joint limits
    joint_limits = {
        "robot_description_planning": load_yaml_file(
            moveit_config_package, path.join("config", "joint_limits.yaml")
        )
    }

    # Servo
    servo_params = {
        "moveit_servo": load_yaml_file(
            moveit_config_package, path.join("config", "servo.yaml")
        )
    }
    servo_params["moveit_servo"].update({"use_gazebo": True})

    planning_pipeline = {
        "planning_pipelines": ["ompl"],
        "default_planning_pipeline": "ompl",
        "ompl": {
            "planning_plugin": "ompl_interface/OMPLPlanner",
            # TODO: Re-enable `default_planner_request_adapters/AddRuckigTrajectorySmoothing` once its issues are resolved
            "request_adapters": "default_planner_request_adapters/AddTimeOptimalParameterization default_planner_request_adapters/ResolveConstraintFrames default_planner_request_adapters/FixWorkspaceBounds default_planner_request_adapters/FixStartStateBounds default_planner_request_adapters/FixStartStateCollision default_planner_request_adapters/FixStartStatePathConstraints",
            # TODO: Reduce start_state_max_bounds_error once spawning with specific joint configuration is enabled
            "start_state_max_bounds_error": 0.31416,
        },
    }
    _ompl_yaml = load_yaml_file(
        moveit_config_package, path.join("config", "ompl_planning.yaml")
    )
    planning_pipeline["ompl"].update(_ompl_yaml)

    # Planning scene
    planning_scene_monitor_parameters = {
        "publish_planning_scene": True,
        "publish_geometry_updates": True,
        "publish_state_updates": True,
        "publish_transforms_updates": True,
    }

    # MoveIt controller manager
    moveit_controller_manager_yaml = load_yaml_file(
        moveit_config_package, path.join("config", "moveit_controller_manager.yaml")
    )
    moveit_controller_manager = {
        "moveit_controller_manager": "moveit_simple_controller_manager/MoveItSimpleControllerManager",
        "moveit_simple_controller_manager": moveit_controller_manager_yaml,
    }

    # Trajectory execution
    trajectory_execution = {
        "allow_trajectory_execution": True,
        "moveit_manage_controllers": False,
        "trajectory_execution.allowed_execution_duration_scaling": 1.2,
        "trajectory_execution.allowed_goal_duration_margin": 0.5,
        "trajectory_execution.allowed_start_tolerance": 0.01,
    }

    # Controller parameters

    controller_parameters = PathJoinSubstitution(
        [
            FindPackageShare(moveit_config_package),
            "config",
            LaunchConfiguration("__controller_parameters_basename"),
        ]
    )

    spawn_dogtooth_velocity_controller = Node(
        package='controller_manager',
        executable='spawner',
        arguments=['diff_drive_controller', '-c', '/controller_manager'],
        output='screen',
    )

    node_robot_state_publisher = Node(
        package="robot_state_publisher",
        executable="robot_state_publisher",
        output="screen",
        parameters=[{'use_sim_time': use_sim_time}, robot_description],
    )

    spawn_joint_state_broadcaster = Node(
        package='controller_manager',
        executable='spawner',
        arguments=['joint_state_broadcaster', '-c', '/controller_manager'],
        output='screen',
    )
    
    spawn_joint_trajectory_controller = Node(
        package="controller_manager",
        executable="spawner",
        arguments=["joint_trajectory_controller", '-c', '/controller_manager'],
        output="screen",
    )

    # Make sure spawn_dogtooth_velocity_controller starts after spawn_joint_state_broadcaster
    diffdrive_controller_spawn_callback = RegisterEventHandler(
        event_handler=OnProcessExit(
            target_action=spawn_joint_state_broadcaster,
            on_exit=[spawn_dogtooth_velocity_controller],
        )
    )
    
    pkg_gazebo_ros = get_package_share_directory('gazebo_ros')
    gzserver = IncludeLaunchDescription(
                    PythonLaunchDescriptionSource([os.path.join(pkg_gazebo_ros, 'launch', 'gzserver.launch.py')]),
                    launch_arguments={'world': world_path, 'verbose': gazebo_verbose, 'shell':'false'}.items())
    
    gzclient = IncludeLaunchDescription(
        PythonLaunchDescriptionSource(
            os.path.join(pkg_gazebo_ros, 'launch', 'gzclient.launch.py')
        )
    )

    # Spawn robot
    spawn_robot = Node(
        package='gazebo_ros',
        executable='spawn_entity.py',
        name='spawn_dogtooth',
        arguments=[ '-entity','dogtooth',
                    '-topic','robot_description',
                    '-x', x_pose,
                    '-y', y_pose,
                    '-z', z_pose,
                    '-R', roll,
                    '-P', pitch,
                    '-Y', yaw],
        output='screen',
    )

    # move_group (with execution)
    moveit_ros = Node(
        package="moveit_ros_move_group",
        executable="move_group",
        output="log",
        arguments=["--ros-args", "--log-level", "warn"],
        parameters=[
            robot_description,
            robot_description_semantic,
            kinematics,
            joint_limits,
            planning_pipeline,
            trajectory_execution,
            planning_scene_monitor_parameters,
            moveit_controller_manager,
            {"use_sim_time": use_sim_time},
        ],
    )
    # move_servo
    moveit_servo = Node(
        package="moveit_servo",
        executable="servo_node_main",
        output="log",
        arguments=["--ros-args", "--log-level", "warn"],
        parameters=[
            robot_description,
            robot_description_semantic,
            kinematics,
            joint_limits,
            planning_pipeline,
            trajectory_execution,
            planning_scene_monitor_parameters,
            servo_params,
            {"use_sim_time": use_sim_time},
        ]
    )

    IncludeLaunchDescription(
        PythonLaunchDescriptionSource(
            PathJoinSubstitution(
                [
                    FindPackageShare(moveit_config_package),
                    "launch",
                    "move_group.launch.py",
                ]
            )
        ),
        launch_arguments=[
            ("ros2_control_plugin", "gz"),
            ("ros2_control_command_interface", "effort"),
            # TODO: Re-enable colligion geometry for manipulator arm once spawning with specific joint configuration is enabled
            ("collision_arm", "false"),
            ("rviz_config", rviz_config),
            ("use_sim_time", use_sim_time),
            ("log_level", "warn"),
        ],
    )

    # Launch dogtooth_control/control.launch.py which is just robot_localization.
    launch_dogtooth_control = IncludeLaunchDescription(
        PythonLaunchDescriptionSource(PathJoinSubstitution(
        [FindPackageShare("dogtooth_control"), 'launch', 'control.launch.py'])))

    # Launch dogtooth_control/teleop_base.launch.py which is various ways to tele-op
    # the robot but does not include the joystick. Also, has a twist mux.
    launch_dogtooth_teleop_base = IncludeLaunchDescription(
        PythonLaunchDescriptionSource(PathJoinSubstitution(
        [FindPackageShare("dogtooth_control"), 'launch', 'teleop_base.launch.py'])))

    ld = LaunchDescription(ARGUMENTS)
    ld.add_action(node_robot_state_publisher)
    ld.add_action(spawn_joint_state_broadcaster)
    ld.add_action(spawn_joint_trajectory_controller)
    ld.add_action(diffdrive_controller_spawn_callback)
    ld.add_action(gzserver)
    ld.add_action(gzclient)
    ld.add_action(spawn_robot)
    ld.add_action(moveit_servo)
    ld.add_action(moveit_ros)
    ld.add_action(launch_dogtooth_control)
    ld.add_action(launch_dogtooth_teleop_base)

    return ld


def load_yaml_file(package_name: str, file_path: str):
    """
    Load yaml configuration based on package name and file path relative to its share.
    """

    package_path = get_package_share_directory(package_name)
    absolute_file_path = path.join(package_path, file_path)
    return parse_yaml(absolute_file_path)


def parse_yaml(absolute_file_path: str):
    """
    Parse yaml from file, given its absolute file path.
    """

    try:
        with open(absolute_file_path, "r") as file:
            return yaml.safe_load(file)
    except EnvironmentError:
        return None

def generate_declared_arguments() -> List[DeclareLaunchArgument]:

    return [
        # Servo
        DeclareLaunchArgument(
            "enable_servo",
            default_value="true",
            description="Flag to enable MoveIt2 Servo for manipulator.",
        ),
        DeclareLaunchArgument(
            "rviz_config",
            default_value=path.join(
                get_package_share_directory("franka"),
                "rviz",
                "config.rviz",
            ),
            description="Path to configuration for RViz2.",
        ),
    ]