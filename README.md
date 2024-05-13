# Tomato Glass House Environment
This package is a tomato glass house simulator compatible with both Gazebo and Unity in ROS2 for ROS-based agricultural environment. This repository consists of three key parts  

1) Random tomato farm generator in Unity
2) Random tomato glass house generator in Gazebo
2) Simulation of the generated tomato farms in Unity and Gazebo

Gazebo 

<img src="docs/GazeboClassic_Farm04.png" width="400" > <img src="docs/GazeboClassic_Farm03.png" width="400" >

<!-- ![Gazebo Classic - Tomato Farm Entire Field](docs/GazeboClassic_Farm01.png?raw=true )
![Gazebo Classic - Tomato Farm Close-up View](docs/GazeboClassic_Farm02.png?raw=true )-->

Unity

<!-- ![Gazebo Sim - Tomato Farm Entire Field](docs/GazeboSim_Farm01.png?raw=true )
![Gazebo Sim - Tomato Farm Close-up View](docs/GazeboSim_Farm02.png?raw=true )-->

<img src="docs/Unity_Farm01.png" width="400" > <img src="docs/Unity_Farm04.png" width="400" >



# Attention!!!

**Follow instructions to build docker container to prepare environment**

https://github.com/LCAS/dogtooth_docker.git

## Getting Started (assume that you are inside the docker container)

```bash
mkdir -p /home/developer/dogtooth_ws/src 
cd /home/developer/dogtooth_ws/src
git clone --branch humble-unity-gazebo git@github.com:LCAS/aoc_tomato_farm.git
cd aoc_tomato_farm && git checkout humble-dev
cd /home/developer/dogtooth_ws && colcon build
source ~/.bashrc
```

# How to Generate a New Glass house Tomato Farm in Gazebo

Run Jupyter Notebook

```bash
cd /home/developer/dogtooth_ws/src/aoc_tomato_farm/tomato_farm_generator/scripts/
jupyter notebook glass_house_generator.ipynb
```

In this file, the farm size, the number of rows, the number of plants in each row, the distance between each plant and each row, the quantity of glass houses and the attenuation of the lights can be parametrically adjusted. Each plant in the farm is generated randomly, including locations of stems, leaves, fruits and so on. The generated tomato farm tomato models and world files, both compatible with Gazebo Sim and Gazebo Classic are saved in **tomato_farm_generator/generated** folder. 

## How to Use Generated Tomato Farms in Simulation

To use a generated tomato farm in simulation, copy files in **tomato_farm_generator/generated** folder into **tomato_farm_simulator/models** and **tomato_farm_simulator/worlds** folders and modify launch file (tomato_farm_world.launch.py) accordingly.

## Run Gazebo Simulation

Run the following launch file to simulate only Gazebo tomato farm world

```bash
ros2 launch tomato_farm_simulator tomato_farm_world.launch.py
```

If you do not want to use lights in your gazebo simulation, before generate your new environment go to **gz_tomato_farm_generator/templates/lamp_model_gazebo_classic_and_sim/{{cookiecutter.models}}/{{cookiecutter.world_name}}/{{cookiecutter.lamp_name/model.sdf}}** and comment the next lines
```bash
<light name='lightComponent' type='point'>
    <diffuse>0.37 0.203 0.513 1</diffuse>
    <specular>0.1 0.1 0.1 1</specular>
    <attenuation>
        <range>7</range>
        <constant>{{cookiecutter.light_constant}}</constant>     
        <linear>{{cookiecutter.light_linear}}</linear>
        <quadratic>{{cookiecutter.light_quadratic}}</quadratic>
    </attenuation>
    <cast_shadows>1</cast_shadows>
    <direction>0 0 -1</direction>
</light>

```


# How to Generate a New Glass house Tomato Farm in Unity

## Unity Setup
Go to https://unity.com/download and download Unity Hub. Once it is done, install Unity 2022.3.17f1 via Unity Hub. After successful installation the version will be available under the Installs tab in Unity Hub. 

## Add your **unity_tomato_farm_generator** project

Initialize your Unity Hub, go to the projects window and press the "Add" button. After press "Add" button you will need to find your **aoc_tomato_farm/unity_tomato_farm_generator** and open it. Your unity project will be displayed. 

In this Unity project , the farm size, the number of rows, the number of plants in each row, the distance between each plant and each row, the quantity of glass houses and the attenuation of the lights can be parametrically adjusted. Each plant in the farm is generated randomly, including locations of stems, leaves, fruits and so on. 

## Export your glass house tomato farm
You cand modify the parameters of your tomato farm with a group of public variables shown in the Inspector window of the Unity Editor. In adittion, you can replace the models easily for your own models. 

Once you have finished your customization of your environment, you can see a previsualization of the world in the play mode (pushing play button) and, finally, export your world. Before generate your Unity world, it is necessary to generate a  new folder in **tomato_farm_simulator/worlds** with the next format ***RowLenght+mx+NumberofRows+mU (8mx4mU, 7mx3mU, 2mx4mU)***. 

To generate your simulation, go to the option ***file*** of the menu and select ***Build Settings...***. In build settings, select  button ***build*** and save your project in the folder generated for this special environment with the format ***environment.x86_64***. 

## Run Unity Simulation
To use a generated tomato farm in simulation, you can  modify launch file (unity_Launch.py) accordingly or openinig directly your environment.x86_64 file in **tomato_farm_simulator/worlds/environment.x86_64** folder.  In adittion, you can stay in Unity's play mode all what you want before export your project, it is possible to test your project only using the play mode of Unity. 

### Display both environmets at the same time
You can display both, Unity and Gazebo environments, using **tomato_farm_simulator/worlds/GazeboxUnity.launch.py**

## Connect the Unity environment with ROS2

Please refer to the README of "ros2-for-unity" package for setup of communication between the tomato environment and ROS2 and import the package into the asset folder of your ***unity_tomato_farm_generator***

https://github.com/RobotecAI/ros2-for-unity

Once ROS2 For Unity is installed, you can modify the example scripts and associate them to your models to interact with your tomato farm environment. 

You can import your models directly to your environment usign 
"URDF-Importer" package. 

https://github.com/Unity-Technologies/URDF-Importer


# Glass house model

if you want to use the glass house model of the pictures, you can buy it here: https://www.cgtrader.com/3d-models/architectural/other/hydroponic-greenhouse
