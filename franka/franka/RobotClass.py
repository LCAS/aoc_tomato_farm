from spatialmath import SE3
from roboticstoolbox.models.DH import Panda
from roboticstoolbox import DHRobot
from roboticstoolbox.tools import ctraj,jtraj
import numpy as np
import os


class panda_7dof(Panda):
    def __init__(self,q0 = None):
        super().__init__()
        
        if(q0 == None):
            self.q0 = [0]*7
            self.q0[6]=np.pi/4
        else:
            self.q0 = q0
        self.q = self.q0
        self.p = self.fk_pos(self.q)
        
        self.plan = []
        
        self.set_q(self.q,1)
    
        
    def ik(self, T):
        q = self.ikine_min(T,qlim=True).q
        return q
    
    def fk(self, q):
        return self.fkine(q)
    
    def fk_pos(self,q):
        return self.fkine(q).t
    
    def traj (self, p1, p2, n):
        ps = jtraj(p1,p2,n).q
        q = []
        for p in ps:
            T = SE3.Trans(p)* SE3.Rx(np.pi)
            q.append(self.ik(T))
        return q
            
    def string_qs(self, qs):
        s=''
        for q in qs:
            for value in q:
                s+=str(value)+' '
        return s
    def string_q(self, q):
        s=''
        for value in q:
            s+=str(value)+' '
        return s
    
    def move_linear(self, p, t, n=3, plan=False):
        p0 = self.fk_pos(self.q)
        qs = self.traj(p0,p,n)
        
        dt = t/n
        if(plan):
            self.plan.extend(qs)
        else:
            os.system(f'ros2 run franka multi_point_controller {int(n)} {int(dt*1e9)} {self.string_qs(qs)}')
        self.update(qs[-1])
    
    def direction(self, direction, distance, t, n=3, plan = False):
        p = self.p.copy()
        if direction=='f':
            p[0]+=distance
        elif direction=='b':
            p[0]-=distance
        elif direction=='l':
            p[1]+=distance
        elif direction=='r':
            p[1]-=distance  
        elif direction=='u':
            p[2]+=distance
        elif direction=='d':
            p[2]-=distance
        else:
            print('Wrong Direction')
            return
        self.move_linear(p,t,n,plan)  
    def execute_plan(self,t,erase = True):
        qs = self.plan
        n= len(qs)
        dt = t/n
        os.system(f'ros2 run franka multi_point_controller {n} {int(dt*1e9)} {self.string_qs(qs)}')
        self.update(qs[-1])
        if(erase):
            self.plan = []
        
            
    
    def move_to_pos_1step(self, p, t, plan = False):
        T = SE3.Trans(p)* SE3.Rx(np.pi)
        q = self.ik(T)
        
        if(plan):
            self.plan.extend([q])
        else:
            os.system(f'ros2 run franka multi_point_controller 1 {int(t*1e9)} {self.string_q(q)}')
        self.update(q)
        
        
        
    def set_q(self, q, t):
        self.update(q)
        os.system(f'ros2 run aoc_gazebo_manipulator multi_point_controller 1 {int(t*1e9)} {self.string_q(q)} ')
    
    def update(self,q):
        self.q = q
        self.p = self.fk_pos(q)
        
    def reset(self):
        self.set_q(self.q0,1)
        
            
if __name__ == '__main__':
    robot = panda_7dof()

    robot.move_to_pos([0.5,0,0.3],3)

