from launch import LaunchDescription
from launch.actions import IncludeLaunchDescription
from launch.launch_description_sources import PythonLaunchDescriptionSource
from launch.substitutions import PathJoinSubstitution
from launch_ros.substitutions import FindPackageShare
import os 
from ament_index_python.packages import get_package_share_directory
from launch.actions import SetEnvironmentVariable
from launch.substitutions import Command, EnvironmentVariable, FindExecutable, LaunchConfiguration, PathJoinSubstitution
from pathlib import Path
from launch.actions import SetEnvironmentVariable
from launch.actions import DeclareLaunchArgument


def generate_launch_description():
    
    # Set the path to this package.
    pkg_share = FindPackageShare(package='aoc_tomato_farm_gazebo').find('aoc_tomato_farm_gazebo')
    dogtooth_gazebo_pkg_dir = get_package_share_directory('dogtooth_gazebo')
    aoc_gazebo_pkg_dir = get_package_share_directory('aoc_tomato_farm_gazebo')
    
    # Set the path to the world file
    farm = '8mx7m' # Choose the farm model to be used
    world_file_name = 'tomato_farm_' + farm + '_gazebo_classic.world' # Choose world based on the farm to be desired
    world_file = os.path.join(pkg_share, 'worlds', farm, world_file_name)   
    # world_file = os.path.join(pkg_share, 'worlds', 'empty_world.world')
    gazebo_launch = PathJoinSubstitution([FindPackageShare("aoc_tomato_farm_gazebo"), "launch", "gazebo_mobile_manipulator.launch.py"],)

    os.environ["ROBOT_NAME"] = "mobile_manipulator_001"
    use_sim_time = LaunchConfiguration('use_sim_time', default='true')
    x_pose = LaunchConfiguration('x_pose', default='1.0')
    y_pose = LaunchConfiguration('y_pose', default='0.0')
    z_pose = LaunchConfiguration('z_pose', default='0.0')
    roll = LaunchConfiguration('roll', default='0.0')
    pitch = LaunchConfiguration('pitch', default='0.0')
    yaw = LaunchConfiguration('yaw', default='-1.57')
    with_gui = LaunchConfiguration('with_gui', default='true')
    gazebo_verbose = LaunchConfiguration('gazebo_verbose', default='false')

    # world = PathJoinSubstitution([dogtooth_gazebo_pkg_dir, 'worlds', world_name])
    if 'GAZEBO_MODEL_PATH' in os.environ:
        model_path =  os.environ['GAZEBO_MODEL_PATH'] + ':' + os.path.join(aoc_gazebo_pkg_dir, 'models',farm)
    else:
        model_path =  os.path.join(aoc_gazebo_pkg_dir, 'models',farm)

    gazebo_sim = IncludeLaunchDescription(
        PythonLaunchDescriptionSource([gazebo_launch]),
        launch_arguments={  'world_path': world_file, 
                            'x_pose': x_pose,
                            'y_pose': y_pose,
                            'z_pose': z_pose,
                            'roll': roll,
                            'pitch': pitch,
                            'yaw':yaw,
                            'gazebo_verbose': gazebo_verbose,
                            'use_sim_time': use_sim_time}.items())

     # Launch them all!
    return LaunchDescription([
        DeclareLaunchArgument(
            'use_sim_time',
            default_value='true',
            description='Use simulation (Gazebo) clock if true'),
        DeclareLaunchArgument(
            'x_pose',
            default_value='1.0',
            description='Start pose, x'),
        DeclareLaunchArgument(
            'y_pose',
            default_value='0.0',
            description='Start pose, y'),
        DeclareLaunchArgument(
            'z_pose',
            default_value='0.0',
            description='Start pose, z'),
        DeclareLaunchArgument(
            'roll',
            default_value='0.0',
            description='Start roll angle'),
        DeclareLaunchArgument(
            'pitch',
            default_value='0.0',
            description='Start pitch angle'),
        DeclareLaunchArgument(
            'yaw',
            default_value='-1.57',
            description='Start yaw angle'),
        DeclareLaunchArgument(
            'world_path',
            default_value=world_file,
            description='Gazebo sim world'),
        DeclareLaunchArgument(
            'gazebo_verbose',
            default_value='false',
            description='Log the whole processing'),
        DeclareLaunchArgument(
            'with_gui',
            default_value='true',
            description='Run Gazebo Sim GUI'),
        SetEnvironmentVariable(name='GAZEBO_MODEL_PATH', value=model_path),
        gazebo_sim
    ])
