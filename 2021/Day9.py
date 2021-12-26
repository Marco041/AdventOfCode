from functools import reduce

fp = open("input9.txt")
grid = [[int(height) for height in row.rstrip()] for row in fp]
x_size, y_size = len(grid[0]), len(grid)
adj_cord = [(-1,0), (1,0), (0,-1), (0,1)]

def findBasin(grid, currentCord, alreadyCount):
    currentValue = grid[currentCord[0]][currentCord[1]]
    if(currentValue == 9):
        return 0
    alreadyCount.add(currentCord)
    size = 1
    for cord in adj_cord:
        newCord = (currentCord[0]+cord[0], currentCord[1]+cord[1])
        if(
            0 <= newCord[1] < x_size
            and 0 <= newCord[0] < y_size
            and newCord not in alreadyCount
        ):
            size += findBasin(grid, newCord, alreadyCount)
    return size

part1_cnt = 0
part2_cnt = []
for x in range(len(grid)):
    for y in range(len(grid[0])):
        if grid[x][y] == 9:
            continue
        cnt = 1
        for cord in adj_cord:
            xa = cord[0]+x
            ya = cord[1]+y
            if(
                0 <= ya < x_size 
                and 0 <= xa < y_size 
                and grid[x][y]>=grid[xa][ya]
            ):
                cnt = 0
        if(cnt==1):
            part1_cnt += grid[x][y] + 1
            part2_cnt.append(findBasin(grid, (x,y), set()))

print("Part 1: ", part1_cnt)
part2 = reduce((lambda x, y: x * y), sorted(part2_cnt)[-3:])
print("Part 2: ", part2)