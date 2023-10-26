import os

from launch import LaunchDescription
from launch.actions import DeclareLaunchArgument
from launch.substitutions import LaunchConfiguration
from launch_ros.actions import Node


def generate_launch_description():

    # Launch configuration variables specific to simulation
    x_pose = LaunchConfiguration('x_pose', default='0.0')
    y_pose = LaunchConfiguration('y_pose', default='0.0')
    roll = LaunchConfiguration('roll', default='0.0')
    pitch = LaunchConfiguration('pitch', default='0.0')
    yaw = LaunchConfiguration('yaw', default='0.0')

    start_gazebo_ros_spawner_cmd = Node(
        package='gazebo_ros',
        executable='spawn_entity.py',
        arguments=['-topic', 'robot_description',
                    '-entity', 'dogtooth_gazebo',
                    '-x', x_pose,
                    '-y', y_pose,
                    '-z', '0.01',
                    '-R', roll,
                    '-P', pitch,
                    '-Y', yaw,
                    ],
        output='screen',
    )

    ld = LaunchDescription()
    ld.add_action(start_gazebo_ros_spawner_cmd)

    return ld