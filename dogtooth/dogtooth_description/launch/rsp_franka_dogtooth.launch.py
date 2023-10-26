import os

from ament_index_python.packages import get_package_share_directory

from launch import LaunchDescription
from launch.substitutions import LaunchConfiguration
from launch.actions import DeclareLaunchArgument
from launch_ros.actions import Node
from launch.actions import SetEnvironmentVariable

import xacro


def generate_launch_description():

    # Process the URDF file    
    pkg_path = os.path.join(get_package_share_directory('robot_description'))
    xacro_file = os.path.join(pkg_path,'description','robot.urdf.xacro')
    robot_description_config = xacro.process_file(xacro_file)

    # robot state publisher node
    node_robot_state_publisher = Node(
        package='robot_state_publisher',
        executable='robot_state_publisher',
        output='screen',
        parameters=[{'robot_description': robot_description_config.toxml()}]
    )

    # Launch!
    return LaunchDescription([
        node_robot_state_publisher
    ])
