import os

from ament_index_python.packages import get_package_share_directory
from launch import LaunchDescription
from launch.substitutions import LaunchConfiguration
from launch.actions import DeclareLaunchArgument
from launch_ros.actions import Node
from launch import LaunchDescription
from launch.substitutions import Command, FindExecutable, PathJoinSubstitution
from launch_ros.actions import Node
from launch_ros.substitutions import FindPackageShare

import xacro

def generate_launch_description():

    ROBOT_NAME = os.environ['ROBOT_NAME']
    use_sim_time = LaunchConfiguration('use_sim_time')
    robot_description_content = Command(
        [
            PathJoinSubstitution([FindExecutable(name="xacro")])," ",
            PathJoinSubstitution([FindPackageShare("dogtooth_description"), "urdf", ROBOT_NAME + ".urdf.xacro"]),
        ]
    )
    
    # Create a robot_state_publisher node
    params = {'robot_description': robot_description_content, 'use_sim_time': use_sim_time}
    node_robot_state_publisher = Node(
        package='robot_state_publisher',
        executable='robot_state_publisher',
        output='screen',
        parameters=[params]
    )

    # Launch!
    return LaunchDescription([
        DeclareLaunchArgument(
            'use_sim_time',
            default_value='false',
            description='Use sim time if true'),
        node_robot_state_publisher
    ])