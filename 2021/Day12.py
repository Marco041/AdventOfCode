neighbours = dict()
fp = open("input12.txt")
for row in fp:
    v1, v2 = row.rstrip().split("-")   
    current = neighbours.get(v1, list())
    current.append(v2)
    neighbours[v1] = current
    current = neighbours.get(v2, list())
    current.append(v1)
    neighbours[v2] = current

def find_path(current_cave, small_cave_visited):
    cnt = 0
    if(current_cave.islower()):
        small_cave_visited = small_cave_visited+[current_cave]
    for cave in neighbours[current_cave]:
        if(cave not in small_cave_visited):
            if(cave == "end"):
                cnt += 1
            else:
                cnt += find_path(cave, small_cave_visited)
    return cnt

def find_path2(current_cave, small_cave_visited, small_cave_already_visited):
    cnt = 0
    if(current_cave == "start" and len(small_cave_visited) > 0):
        return 0
    if(current_cave == "end"):
        return 1
    if(current_cave.islower()):
        if(current_cave in small_cave_visited):
            if(not small_cave_already_visited):
                small_cave_already_visited = True
            else:
                return 0
        small_cave_visited = small_cave_visited+[current_cave]          
    for cave in neighbours[current_cave]:
            cnt += find_path2(cave, small_cave_visited, small_cave_already_visited)
    return cnt

part1_cnt = find_path("start", [])
print("Part 1: ", part1_cnt)

part2_cnt = find_path2("start", [], False)
print("Part 2: ", part2_cnt)

