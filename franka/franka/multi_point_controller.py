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
        self._action_client = ActionClient(node=self, action_type=FollowJointTrajectory,
                                           action_name='/joint_trajectory_controller/follow_joint_trajectory')
        # self._subscriber =

    def send_goal(self, n, qs, dt_ns):
        goal_msg = FollowJointTrajectory.Goal()

        joint_names = ['panda_joint1',
                       'panda_joint2',
                       'panda_joint3',
                       'panda_joint4',
                       'panda_joint5',
                       'panda_joint6',
                       'panda_joint7']
        
        points = []
        
        for i in range(n):
            q = qs[7*i:7*(i+1)]
            point = JointTrajectoryPoint()
            point.time_from_start = Duration(nanoseconds=int(dt_ns*(i+1))).to_msg()
            point.positions = q
            points.append(point)
        
        # point1 = JointTrajectoryPoint()
        # point1.positions = [0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0]

        # # angles= [1.0, 1.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0]

        # point2 = JointTrajectoryPoint()
        # point2.time_from_start = Duration(seconds=int(time), nanoseconds=0).to_msg()
        # point2.positions = angles

        # # points.append(point1)
        # points.append(point2)

        goal_msg.goal_time_tolerance=Duration(seconds=0.001).to_msg()
        goal_msg.trajectory.joint_names = joint_names
        goal_msg.trajectory.points = points

        self._action_client.wait_for_server()
        self._send_goal_future = self._action_client.send_goal_async(
            goal_msg, feedback_callback=self.feedback_callback)

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

    n = int(sys.argv[1])
    dt = int(sys.argv[2])
    qs =[]
    for i in range(n*7):
        qs.append(float(sys.argv[i+3]))
        
    future = action_client.send_goal(n,qs,dt)

    
    rclpy.spin(action_client)


if __name__ == '__main__':
    main()

