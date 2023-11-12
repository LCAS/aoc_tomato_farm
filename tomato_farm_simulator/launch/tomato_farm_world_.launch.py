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
    pkg_share = FindPackageShare(package='tomato_farm_simulator').find('tomato_farm_simulator')
    aoc_gazebo_pkg_dir = get_package_share_directory('tomato_farm_simulator')
    
    # Set the path to the world file
    farm = '8mx7m' # Choose the farm model to be used
    world_file_name = 'tomato_farm_' + farm + '_gazebo_classic.world' # Choose world based on the farm to be desired
    world_file = os.path.join(pkg_share, 'worlds', farm, world_file_name)   
    # world_file = os.path.join(pkg_share, 'worlds', 'empty_world.world')
    gazebo_launch = PathJoinSubstitution([FindPackageShare("tomato_farm_simulator"), "launch", "gazebo_tomato_farm.launch.py"],)

    use_sim_time = LaunchConfiguration('use_sim_time', default='true')
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
                            'gazebo_verbose': gazebo_verbose,
                            'use_sim_time': use_sim_time}.items())

     # Launch them all!
    return LaunchDescription([
        DeclareLaunchArgument(
            'use_sim_time',
            default_value='true',
            description='Use simulation (Gazebo) clock if true'),
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
