# Author: Abdurrahman Yilmaz
# Date: October 9, 2023
# Description: Load tomato farm world file into Gazebo.
# AYilmaz@lincoln.ac.uk

import os
from ament_index_python.packages import get_package_share_directory
from launch import LaunchDescription
from launch_ros.actions import Node
from launch.actions import IncludeLaunchDescription
from launch.launch_description_sources import PythonLaunchDescriptionSource
from launch.actions import SetEnvironmentVariable
import xacro


def generate_launch_description():

    pkg_name = 'franka'  # the package name

    pkg_share = get_package_share_directory(pkg_name)

    urdf_path = 'description/manipulator.urdf.xacro'

    rviz_relative_path = 'rviz/config.rviz'

    rviz_absolute_path = os.path.join(pkg_share, rviz_relative_path)

    # extracting the robot deffinition from the xacro file
    xacro_file = os.path.join(pkg_share, urdf_path)
    robot_description_content = xacro.process_file(xacro_file).toxml()

    # add the path to the model file to  gazebo
    models_path = os.path.join(get_package_share_directory(pkg_name), 'models')

    if 'GAZEBO_MODEL_PATH' in os.environ:
        model_path = os.environ['GAZEBO_MODEL_PATH'] + ':' + models_path
    else:
        model_path = models_path

    world_path = os.path.join(pkg_share, 'worlds', 'empty.world')

    # robot state publisher node
    node_robot_state_publisher = Node(
        package='robot_state_publisher',
        executable='robot_state_publisher',
        output='screen',
        parameters=[{'robot_description': robot_description_content}]
    )
    # Rviz2 node
    node_rviz = Node(
        package='rviz2',
        executable='rviz2',
        name='rviz2',
        arguments=['-d', rviz_absolute_path]
    )
    # Gazebo launch file
    launch_gazebo = IncludeLaunchDescription(
        PythonLaunchDescriptionSource([os.path.join(get_package_share_directory('gazebo_ros'), 'launch'), '/gazebo.launch.py']),
        launch_arguments={'world': world_path}.items()
    )
    # entity spawn node (to spawn the robot from the /robot_description topic)
    node_spawn_entity = Node(package='gazebo_ros', executable='spawn_entity.py',
                             arguments=['-topic', 'robot_description',
                                        '-entity', 'my_bot'],
                             output='screen')
    # spawning the joint broadcaster
    spawn_broadcaster = Node(
        package="controller_manager",
        executable="spawner",
        arguments=["joint_state_broadcaster"],
        output="screen",
    )

    spawn_controller = Node(
        package="controller_manager",
        executable="spawner",
        arguments=["joint_trajectory_controller"],
        output="screen",
    )
    # Run the nodes
    return LaunchDescription([
        SetEnvironmentVariable(name='GAZEBO_MODEL_PATH', value=model_path),
        node_robot_state_publisher,
        launch_gazebo,
        node_spawn_entity,
        # node_rviz,
        spawn_broadcaster,
        spawn_controller
    ])
