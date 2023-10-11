# Author: Abdurrahman Yilmaz
# Date: October 9, 2023
# Description: Load tomato farm world file into Gazebo.
# AYilmaz@lincoln.ac.uk
 
import os
from launch import LaunchDescription
from launch.actions import DeclareLaunchArgument, IncludeLaunchDescription
from launch.conditions import IfCondition, UnlessCondition
from launch.launch_description_sources import PythonLaunchDescriptionSource
from launch.substitutions import Command, LaunchConfiguration, PythonExpression
from launch_ros.actions import Node
from launch_ros.substitutions import FindPackageShare
 
def generate_launch_description():
 
  # Set the path to the Gazebo ROS package
  pkg_gazebo_ros = FindPackageShare(package='gazebo_ros').find('gazebo_ros')   
   
  # Set the path to this package.
  pkg_share = FindPackageShare(package='aoc_tomato_farm_gazebo').find('aoc_tomato_farm_gazebo')
 
  # Set the path to the world file
  farm = '8mx7m' # Choose the farm model to be used
  world_file_name = 'tomato_farm_' + farm + '_gazebo_classic.world' # Choose world based on the farm to be desired
  world_path = os.path.join(pkg_share, 'worlds', farm, world_file_name)
   
  # Set the path to the SDF model files.
  gazebo_models_path = os.path.join(pkg_share, 'models',farm)
  os.environ["GAZEBO_MODEL_PATH"] = gazebo_models_path
  
  # Launch configuration variables specific to simulation
  headless = LaunchConfiguration('headless')
  use_sim_time = LaunchConfiguration('use_sim_time')
  use_simulator = LaunchConfiguration('use_simulator')
  world = LaunchConfiguration('world')
 
  declare_simulator_cmd = DeclareLaunchArgument(
    name='headless',
    default_value='False',
    description='Whether to execute gzclient')
     
  declare_use_sim_time_cmd = DeclareLaunchArgument(
    name='use_sim_time',
    default_value='true',
    description='Use simulation (Gazebo) clock if true')
 
  declare_use_simulator_cmd = DeclareLaunchArgument(
    name='use_simulator',
    default_value='True',
    description='Whether to start the simulator')
 
  declare_world_cmd = DeclareLaunchArgument(
    name='world',
    default_value=world_path,
    description='Full path to the world model file to load')
    
  # Specify the actions
   
  # Start Gazebo server
  start_gazebo_server_cmd = IncludeLaunchDescription(
    PythonLaunchDescriptionSource(os.path.join(pkg_gazebo_ros, 'launch', 'gzserver.launch.py')),
    condition=IfCondition(use_simulator),
    launch_arguments={'world': world}.items())
 
  # Start Gazebo client    
  start_gazebo_client_cmd = IncludeLaunchDescription(
    PythonLaunchDescriptionSource(os.path.join(pkg_gazebo_ros, 'launch', 'gzclient.launch.py')),
    condition=IfCondition(PythonExpression([use_simulator, ' and not ', headless])))
 
  # Create the launch description and populate
  ld = LaunchDescription()
 
  # Declare the launch options
  ld.add_action(declare_simulator_cmd)
  ld.add_action(declare_use_sim_time_cmd)
  ld.add_action(declare_use_simulator_cmd)
  ld.add_action(declare_world_cmd)
 
  # Add any actions
  ld.add_action(start_gazebo_server_cmd)
  ld.add_action(start_gazebo_client_cmd)
 
  return ld
