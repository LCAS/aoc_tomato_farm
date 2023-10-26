from launch import LaunchDescription
from launch.actions import DeclareLaunchArgument, ExecuteProcess, IncludeLaunchDescription, RegisterEventHandler, SetEnvironmentVariable
from launch.event_handlers import OnProcessExit
from launch.launch_description_sources import PythonLaunchDescriptionSource
from launch.substitutions import Command, EnvironmentVariable, FindExecutable, LaunchConfiguration, PathJoinSubstitution
from launch_ros.actions import Node
from launch_ros.substitutions import FindPackageShare

from ament_index_python.packages import get_package_share_directory

from pathlib import Path
import os

ARGUMENTS = [DeclareLaunchArgument('world_path', default_value='', description='The world path, by default is empty.world'),]

def generate_launch_description():

    ROBOT_NAME = os.environ['ROBOT_NAME']

    world_path = LaunchConfiguration('world_path')
    # Launch configuration variables specific to simulation
    prefix = LaunchConfiguration('prefix')
    use_sim_time = LaunchConfiguration('use_sim_time', default='true')
    x_pose = LaunchConfiguration('x_pose', default='0.0')
    y_pose = LaunchConfiguration('y_pose', default='0.0')
    z_pose = LaunchConfiguration('z_pose', default='0.05')
    roll = LaunchConfiguration('roll', default='0.0')
    pitch = LaunchConfiguration('pitch', default='0.0')
    yaw = LaunchConfiguration('yaw', default='0.0')
    gazebo_verbose = LaunchConfiguration('gazebo_verbose', default='false')

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
    ld.add_action(launch_dogtooth_control)
    ld.add_action(launch_dogtooth_teleop_base)

    return ld
