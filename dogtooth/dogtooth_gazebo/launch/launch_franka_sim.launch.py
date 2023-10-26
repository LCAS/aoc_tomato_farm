import os

from ament_index_python.packages import get_package_share_directory

from launch import LaunchDescription
from launch.actions import IncludeLaunchDescription
from launch.launch_description_sources import PythonLaunchDescriptionSource

from launch_ros.actions import Node


def generate_launch_description():

    # Include the robot_state_publisher launch file, provided by our own package. Force sim time to be enabled
    # !!! MAKE SURE YOU SET THE PACKAGE NAME CORRECTLY !!!

    rsp_franka = IncludeLaunchDescription(
                PythonLaunchDescriptionSource([os.path.join(
                    get_package_share_directory('franka'),'launch','rsp_franka.launch.py'
                )]), launch_arguments={'use_sim_time': 'true'}.items()
    )


    gazebo_params_file = os.path.join(get_package_share_directory('robot_gazebo'),'config','gazebo_params.yaml')

    # Include the Gazebo launch file, provided by the gazebo_ros package
    gazebo = IncludeLaunchDescription(
                PythonLaunchDescriptionSource([os.path.join(
                    get_package_share_directory('gazebo_ros'), 'launch', 'gazebo.launch.py')]),
                    launch_arguments={'extra_gazebo_args': '--ros-args --params-file ' + gazebo_params_file}.items()
             )

    # Run the spawner node from the gazebo_ros package. The entity name doesn't really matter if you only have a single robot.                        
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

    # Launch them all!
    return LaunchDescription([
        rsp_franka,
        gazebo,
        node_spawn_entity,
        spawn_broadcaster,
        spawn_controller
    ])
