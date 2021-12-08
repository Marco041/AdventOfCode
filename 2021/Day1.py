import numpy as np

numbers = np.loadtxt("input1.txt", "i")
cnt = (numbers[1:] > numbers[:-1]).sum()
print("Part 1: ", cnt)

window_sum = np.convolve(numbers,np.ones(3,dtype=int),'valid')
cnt = (window_sum[1:] > window_sum[:-1]).sum()
print("Part 2: ", cnt)
