def count_overlap(coordinate_checked):
    cnt = 0
    for cord in coordinate_checked:
        if(coordinate_checked[cord] > 1):
            cnt += 1
    return cnt

def flag_coordinate(dictionary, cord):
     dictionary[cord] = dictionary.get(cord, 0) + 1

def get_range(cord1, cord2):
    return range(min(cord1, cord2), max(cord1, cord2) + 1)

data = open("input5.txt").readlines()
coordinate_checked = dict()
coordinate_checked_all = dict()
for line in data:
    x1, y1 = tuple(int(x) for x in line.split(" -> ")[0].split(","))
    x2, y2 = tuple(int(x) for x in line.split(" -> ")[1].split(","))

    if x1 == x2 or y1 == y2:
        for x in get_range(x1, x2):
            for y in get_range(y1, y2):
                flag_coordinate(coordinate_checked, (x, y))
                flag_coordinate(coordinate_checked_all, (x, y))
    else:
        if(x2 > x1):
            y = y1
            y_direction = -1 if y1>y2 else 1
        else:
            y = y2
            y_direction = -1 if y2>y1 else 1

        for x in get_range(x1, x2):
            flag_coordinate(coordinate_checked_all, (x, y))
            y += y_direction

print("Part 1: ", count_overlap(coordinate_checked))
print("Part 2: ", count_overlap(coordinate_checked_all))


