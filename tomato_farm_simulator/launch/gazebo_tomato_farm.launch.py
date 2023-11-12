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

    world_path = LaunchConfiguration('world_path')
    # Launch configuration variables specific to simulation
    prefix = LaunchConfiguration('prefix')
    use_sim_time = LaunchConfiguration('use_sim_time', default='true')
    gazebo_verbose = LaunchConfiguration('gazebo_verbose', default='false')

    pkg_gazebo_ros = get_package_share_directory('gazebo_ros')
    gzserver = IncludeLaunchDescription(
                    PythonLaunchDescriptionSource([os.path.join(pkg_gazebo_ros, 'launch', 'gzserver.launch.py')]),
                    launch_arguments={'world': world_path, 'verbose': gazebo_verbose, 'shell':'false'}.items())
    
    gzclient = IncludeLaunchDescription(
        PythonLaunchDescriptionSource(
            os.path.join(pkg_gazebo_ros, 'launch', 'gzclient.launch.py')
        )
    )

    ld = LaunchDescription(ARGUMENTS)
    ld.add_action(gzserver)
    ld.add_action(gzclient)
    
    return ld
