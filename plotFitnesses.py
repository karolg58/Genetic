import argparse
from os import listdir
from os.path import isfile, join
import matplotlib.pyplot as plt
from matplotlib import colors as mcolors

parser = argparse.ArgumentParser()
parser.add_argument('-v','--versions', nargs='+', help='<Required> show plot for given versions', required=True)
parser.add_argument('-n','--fileNumber', help="number of file from directory to load", required=False)
args = parser.parse_args()

fileNr = 0
if(args.fileNumber):
     fileNr = int(args.fileNumber)

ssum = lambda x, y: x + y 
ddiff = lambda x, y: x - y 
fun= [ssum , ssum, ssum]
rgb = (0.0, 0.5, 1.0)
nr = 0
for v in args.versions:
    dirPath = join("data", "fitnessForAll", v)

    fileName = [f for f in listdir(dirPath) if isfile(join(dirPath, f))][fileNr]

    with open(join(dirPath, fileName), 'r') as f:
        lineId = 0
        lines = f.readlines()
        for line in lines:
            values = list(map(lambda x: int(x), line.split(',')))
            plt.plot(range(0, len(values)), values, color=rgb)
            lst = list(rgb)
            for i in range(0, 3):
                lst[i] = fun[i](lst[i], 1.5/(len(lines)))
                if(lineId > len(lines) - 2 - 3):
                    lst[0] = 0
                    lst[1] = 0
                    lst[2] = 0
                if(lst[i] > 1):
                    lst[i] = 1
                    fun[i] = ddiff
                if(lst[i] < 0):
                    lst[i] = 0
                    fun[i] = ssum
            rgb = tuple(lst)
            lineId += 1
    plt.title(args.versions[0])
    plt.savefig(join("data", "fitnessForAllPlots", args.versions[0] + ".png"))
    

