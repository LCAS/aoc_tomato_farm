import os
from ament_index_python.packages import get_package_share_directory
from launch import LaunchDescription
from launch_ros.actions import Node
from launch.actions import IncludeLaunchDescription
from launch.launch_description_sources import PythonLaunchDescriptionSource

import xacro


def generate_launch_description():

    pkg_name = 'franka' #the package name
    
    pkg_share= get_package_share_directory(pkg_name)
    
    urdf_path = 'description/manipulator.urdf.xacro'

    rviz_relative_path= 'rviz/config.rviz'

    rviz_absolute_path = os.path.join(pkg_share, rviz_relative_path)
    
    # extracting the robot deffinition from the xacro file
    xacro_file = os.path.join(pkg_share, urdf_path)
    robot_description_content = xacro.process_file(xacro_file).toxml()

    # robot state publisher node
    node_robot_state_publisher = Node(
        package='robot_state_publisher',
        executable='robot_state_publisher',
        output='screen',
        parameters=[{'robot_description': robot_description_content}]
    )
    #joint state publisher node (GUI mode)
    node_joint_state_publisher = Node(
        package='joint_state_publisher_gui',
        executable='joint_state_publisher_gui',
        name='joint_state_publisher_gui',
    )
    # Rviz2 node
    node_rviz = Node(
        package='rviz2',
        executable='rviz2',
        name='rviz2',
        arguments= ['-d', rviz_absolute_path]
    )

    # Run the nodes
    return LaunchDescription([
        node_robot_state_publisher,
        node_joint_state_publisher,
        node_rviz,
    ])
