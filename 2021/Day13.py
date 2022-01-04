import numpy as np

def build_matrix(input_dot):
    col_size = max([int(x[0]) for x in input_dot])+1
    row_size = max([int(y[1]) for y in input_dot])+1
    matrix = np.zeros((row_size, col_size), "i").reshape(row_size, col_size)
    for dot in input_dot:
        matrix[dot[1]][dot[0]] = 1
    return  matrix

data = open("input13.txt")
input_dot = []
fold = []
for row in data:
    if "," in row:
        input_dot.append((int(row.split(',')[0]),int(row.split(',')[1])))
    if "=" in row:
        fold.append((row.split("=")[0][-1], int(row.split("=")[1])))

firstStep = True
for item in fold:
    for idx,point in enumerate(input_dot):
        if item[0] == "x":
            new_point = item[1]*2-point[0] if point[0] > item[1] else point[0]
            input_dot[idx] = (new_point, point[1])
        if item[0] == "y":
            new_point = item[1]*2-point[1] if point[1] > item[1] else point[1]
            input_dot[idx] = (point[0], new_point)

    if(firstStep): print("Part 1: ", (build_matrix(input_dot) > 0).sum())
    firstStep = False

result = build_matrix(input_dot)
for line in result:
    print (''.join(map(lambda x: " " if x == 0 else "#", line)))