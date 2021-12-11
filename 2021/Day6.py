import numpy as np

def simulate(day_start, day_end, fish):
    max_day = 8
    for _ in range(day_start, day_end+1):
        fish_to_reset = fish.get(0, 0)
        for i in range(max_day+1):
            fish[i] = fish.get(i+1, 0)
        fish[6] = fish.get(6, 0) + fish_to_reset
        fish[max_day] = fish_to_reset

input = np.loadtxt("input6.txt", delimiter=",", dtype="uint32")
fish_day = dict()
for fish in input:
     fish_day[fish] = fish_day.get(fish, 0) + 1

simulate(1, 80, fish_day)
print("Part 1: ", sum(fish_day.values()))

simulate(81, 256, fish_day)
print("Part 2: ", sum(fish_day.values()))
