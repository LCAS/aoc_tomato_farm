import os
from launch import LaunchDescription
from launch.actions import DeclareLaunchArgument, IncludeLaunchDescription
from launch.conditions import IfCondition, UnlessCondition
from launch.launch_description_sources import PythonLaunchDescriptionSource
from launch.substitutions import Command, LaunchConfiguration, PythonExpression
from launch_ros.actions import Node
from launch.actions import ExecuteProcess
from launch_ros.substitutions import FindPackageShare

def generate_launch_description():
    #Set the path to this package
    pkg_share = FindPackageShare(package='tomato_farm_simulator').find('tomato_farm_simulator')    
    #Set the path to the world file 
    farm= "4mx3m" #Choose the model farm be used 
    world_file_name=  farm + "U.x86_64" 
    world_path = os.path.join(pkg_share+ "/worlds", farm+"U", world_file_name)
    os.system("chmod +x "+world_path)

    ld= LaunchDescription()
    
    #Unity World 
    declare_worldU_cmd= ExecuteProcess( 
        cmd=[world_path],
        output= "screen",
        )    
    

    ld.add_action(declare_worldU_cmd)
    
    return ld




