import rclpy 
from rclpy.action import ActionClient
from rclpy.node import Node
from control_msgs.action import FollowJointTrajectory
from trajectory_msgs.msg import JointTrajectoryPoint
from rclpy.duration import Duration

import sys

class JointControlClient(Node):
    def __init__(self):
        super().__init__(node_name='joint_controller')
        self._action_client=ActionClient(node=self, action_type=FollowJointTrajectory, action_name='/joint_trajectory_controller/follow_joint_trajectory')
        # self._subscriber =
    def send_goal(self,angles):
        goal_msg = FollowJointTrajectory.Goal()
        
        joint_names = ['panda_joint1', 
                       'panda_joint2', 
                       'panda_joint3', 
                       'panda_joint4', 
                       'panda_joint5',
                       'panda_joint6',
                       'panda_joint7']
        points=[]
        
        point2=JointTrajectoryPoint()
        point2.time_from_start= Duration(seconds=1, nanoseconds=0).to_msg()
        point2.positions=angles
        
        # points.append(point1)
        points.append(point2)
        
        # goal_msg.goal_time_tolerance=Duration(seconds=0.1, nanoseconds=0).to_msg()
        goal_msg.trajectory.joint_names = joint_names
        goal_msg.trajectory.points = points
        
        self._action_client.wait_for_server()
        self._send_goal_future = self._action_client.send_goal_async(goal_msg, feedback_callback=self.feedback_callback)

        self._send_goal_future.add_done_callback(self.goal_response_callback)
        
    def goal_response_callback(self, future):
        goal_handle = future.result()
        if not goal_handle.accepted:
            self.get_logger().info('Goal rejected :(')
            return

        self.get_logger().info('Goal accepted :)')

        self._get_result_future = goal_handle.get_result_async()
        self._get_result_future.add_done_callback(self.get_result_callback)
    
    def get_result_callback(self, future):
        result = future.result().result
        self.get_logger().info('Result: '+str(result))
        rclpy.shutdown()

    def feedback_callback(self, feedback_msg):
        feedback = feedback_msg.feedback
        # self.get_logger().info('Received feedback:'+str(feedback))

def main(args=None):
    
    rclpy.init()

    action_client = JointControlClient()

    angles = [float(sys.argv[1]),
              float(sys.argv[2]),
              float(sys.argv[3]),
              float(sys.argv[4]),
              float(sys.argv[5]),
              float(sys.argv[6]),
              float(sys.argv[7])
              ]
    future = action_client.send_goal(angles)

    rclpy.spin(action_client)


if __name__ == '__main__':
    main()