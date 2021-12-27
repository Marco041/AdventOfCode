from collections import deque
import statistics

lines = open("input10.txt").read().splitlines()
match = {'}': '{', ']': '[', ')': '(', '>': '<'}
point =  {'}': 1197, ']': 57, ')': 3, '>': 25137}
point_part2 =  {'{': 3, '[': 2, '(': 1, '<': 4}
part1_cnt = 0
part2_cnt = []
for line in lines:
    stack = deque()
    corrupted_line = False
    for element in line:
        match_closed = match.get(element)
        if(len(stack) > 0 and match_closed == stack[-1]):
            stack.pop()
        elif(match_closed):
            part1_cnt += point[element]
            corrupted_line = True
            break
        else:
            stack.append(element)
    if(not corrupted_line):
        sum = 0
        while len(stack) != 0:
            t = stack.pop()
            sum *= 5
            sum += point_part2[t]
        part2_cnt.append(sum)
            
print("Part 1: ", part1_cnt)
print("Part 2: ", statistics.median(part2_cnt))