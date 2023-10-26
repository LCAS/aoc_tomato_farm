
import numpy as np
import RobotClass

robot = RobotClass.panda_7dof()

dx = 0.076
x0 = 0.235
y0 = -0.266
ey = 0.003
positions = [[[0,0] for i in range(8)] for j in range(8)]

for i in range(8):
    for j in range(8):
        positions [i][j][0] = x0 + i*dx
        positions [i][j][1] = y0 + j*dx

def go_to_piece(piece,error_y = 0):
    robot.move_to_pos_1step([positions[piece[0]-1][piece[1]-1][0], positions[piece[0]-1][piece[1]-1][1] + ey + error_y,0.4],0.5,True)

def go_down(d = 0.09):
    robot.direction('d',d,1,5,True)
def go_up(d = 0.09):
    robot.direction('u',d,1,5,True)
def go_to_corner():
    robot.direction('l',dx/2,1,5,True)
    robot.direction('f',dx/2,1,5,True)
def go_to_center():
    robot.direction('r',dx/2,1,4,True)
    robot.direction('b',dx/2+0,1,4,True)
def centralize():
    robot.direction('l',dx/8,1,4,True)
    robot.direction('b',dx/8+0,1,4,True)
    robot.direction('f',dx/8+0,1,4,True)    

def piece2piece(piece1,piece2,error_y = 0, d=0.09):
    go_to_piece(piece1,error_y)
    
    go_down(d)
    
    go_to_corner()
    x1 = piece1[0]
    y1 = piece1[1]

    x2 = piece2[0]
    y2 = piece2[1]
    
    delta_x = x2 - x1
    delta_y = y2 - y1
    if delta_x>0:
        robot.direction('f',delta_x*dx,1,2 + abs(delta_x),True)
    else:
        robot.direction('b',-delta_x*dx,1,2 + abs(delta_x),True)
    
    if delta_y>0:
        robot.direction('l',delta_y*dx,1,2 + abs(delta_y),True)
    else:
        robot.direction('r',-delta_y*dx,1,2 + abs(delta_y),True)
    
    go_to_center()
    
    # centralize()
    
    go_up(d)
    

def main():
    print('Executing e2 to e4')
    piece2piece([2,4],[4,4])
    robot.execute_plan(60,erase=True)

    
    print('Executing d2 to d4')
    piece2piece([2,5],[4,5])
    robot.execute_plan(60,erase=True)

    print('Executing d1 takes d4')
    piece2piece([4,5],[4,8])
    robot.execute_plan(60,erase=True)

    piece2piece([1,5],[4,5])
    robot.execute_plan(60,erase=True)
    
if __name__ == '__main__':
    
    main()
    

