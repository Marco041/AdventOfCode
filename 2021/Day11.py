fp = open("input11.txt")
grid = [[int(height) for height in row.rstrip()] for row in fp]
x_size, y_size = len(grid[0]), len(grid)
adj_cord = [(-1,0), (1,0), (0,-1), (0,1), (-1,-1), (-1,1), (1,-1), (1, 1)]

def flash(grid, currentCord, flashes_count):
    currentValue = grid[currentCord[0]][currentCord[1]]
    if(currentValue >= 10 and currentCord not in flashes_count):
        flashes_count.add(currentCord)
        for cord in adj_cord:
            newCord = (currentCord[0]+cord[0], currentCord[1]+cord[1])
            if(0 <= newCord[1] < x_size and 0 <= newCord[0] < y_size):
                grid[newCord[0]][newCord[1]] += 1
                flash(grid, newCord, flashes_count)

part1_cnt = 0
step = 0
while(True):
    step += 1
    already_count = set()

    for x in range(len(grid)):
        for y in range(len(grid[0])):
            if(grid[x][y]>=10): grid[x][y] = 0
            grid[x][y] += + 1

    for x in range(len(grid)):
        for y in range(len(grid[0])):
            flash(grid, (x,y), already_count)

    if(step <= 100):
        part1_cnt += len(already_count)
    
    if(len(already_count) == x_size*y_size):
        break

print("Part 1: ", part1_cnt)
print("Part 2: ", step)