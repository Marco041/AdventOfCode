import numpy as np

positions = np.loadtxt("input7.txt", "i", delimiter=",")
f1, f2 = {}, {}

for x in range(min(positions), max(positions)+1):
    f1[x] = sum([abs(value - x) for value in positions])
    f2[x] = sum([((abs(value - x)+1)*abs(value - x))//2 for value in positions])

print("Part 1: ", min(f1.values()))
print("Part 2: ", min(f2.values()))