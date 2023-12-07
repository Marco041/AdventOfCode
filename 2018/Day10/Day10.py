import numpy as np
from scipy.optimize import fmin
import math as mth
import matplotlib.pyplot as plt

def varianceFun(x, v, t):
    return np.add(x, v*t)

def plotImage(x, v, time, length):
    xMin, yMin = 1000, 1000
    xMax, yMax = 0, 0
    im = np.zeros((300,300))
    for i in range(length):
        xCord, yCord = varianceFun(x,v,time)[i]
        xMin = min(xMin, xCord)
        yMin = min(yMin, yCord)
        xMax = max(xMax, xCord)
        yMax = max(yMax, yCord)
        im[xCord, yCord] = 1

    im = im[xMin:xMax+1, yMin:yMax+1]
    plt.imshow(im.T)
    plt.show()

def getInputLines():
    with open("input.txt") as f:
        content = f.readlines()
    content = [x for x in content]
    return content

x, v = [], []
for line in getInputLines():
    x.append((int(line[10:16]), int(line[18:24])))
    v.append((int(line[36:38]), int(line[40:42])))

x = np.array(x)
v = np.array(v)

time = mth.ceil(fmin(lambda t : np.var(varianceFun(x,v,t)), 1000))
print(time)
plotImage(x,v,time, np.shape(x)[0])