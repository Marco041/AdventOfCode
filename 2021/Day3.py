from os import linesep
import numpy as np

#Part 1
lines = np.loadtxt("input3.txt", "U")
row = lines.shape[0]
col = len(lines[0])
matrix = lines.view('U1').reshape(row, col)
gamma = np.zeros(col).astype('i')
for i in range(col-1):
    cnt = np.unique(matrix[0:row,i:i+1], return_counts = True)
    if cnt[1][1]>cnt[1][0]:
        gamma[i] = 1
    else:
        gamma[i] = 0

gamma_num = int("".join(str(i) for i in gamma), 2)
epsilon_num = int("".join(str(1-i) for i in gamma), 2)
print("Part 1: ", gamma_num*epsilon_num)

#Part 2
cnt_oxygen, cnt_co2 = np.zeros(col).astype('i'), np.zeros(col).astype('i')
oxygen, co2, new_oxygen, new_co2 = lines, lines, lines, lines
x,y = 0, 0
while(y <= col):
    while x < max(len(oxygen), len(co2)):
        if(y == 0):
            cnt_oxygen[y] += 1 if (oxygen[x][y] == '1') else 0
            cnt_co2[y] += 1 if (co2[x][y] == '1') else 0
        else:
            if len(oxygen) > 1 and len(oxygen) > x and int(oxygen[x][y-1]) == int(cnt_oxygen[y-1]*2>=len(oxygen)):
                new_oxygen.append(oxygen[x])
                if(len(cnt_oxygen) > y):
                    cnt_oxygen[y] += 1 if (oxygen[x][y] == '1') else 0

            if len(co2) > 1 and len(co2) > x and int(co2[x][y-1]) == int(cnt_co2[y-1]*2<len(co2)):
                new_co2.append(co2[x])
                if(len(cnt_co2) > y):
                    cnt_co2[y] += 1 if (co2[x][y] == '1') else 0
        x += 1      
    oxygen = new_oxygen
    co2 = new_co2
    if(len(oxygen) > 1):
        new_oxygen = []

    if(len(co2) > 1):
        new_co2 = []
    y += 1
    x = 0

oxygen_num = int("".join(str(i) for i in oxygen[0]), 2)
co2_num = int("".join(str(i) for i in co2[0]), 2)
print("Part 2 ",oxygen_num*co2_num)