from collections import defaultdict
from random import random, randint,seed
import numpy as np
import time
numBin = 16*5


def getRanBin():
    num = randint(0,2**(numBin))
    data = bin(int(num))[2:]
    data = '0'*(numBin - len(data)) + data
    return [int(i) for i in data]

def getTestData(num):
    return [getRanBin() for _ in range(num)]

def getD(testData):
    DFData = []
    for item in testData:
        prev = 0
        Beg = True
        DFData.append([])
        fir = True
        for c in item:
            if(c):
                DFData[-1].append(prev)
                Beg = False
                prev = 0
                fir = True
            else:
                DFData[-1].append(0)
            if(not Beg and not fir):
                prev+=1
            else:
                fir = False
    return DFData

def getDF(testData):
    DFData = getD(testData)
    for j in range(len(testData)):
        item = testData[j]
        prev = 0
        Beg = True
        fir = True
        item = item[::-1]
        for i in range(len(item)):
            c = item[i]
            if(c):
                DFData[j][len(item)-1-i]+=prev
                Beg = False
                prev = 0
                fir = True
            if(not Beg and not fir):
                prev+=1
            else:
                fir = False
    return DFData

def PDI(testData):
    # calculate DF for each
    finalData = []
    dfData = getD(testData)
    numEntries = len(dfData)
    priorityArr = {k:0 for k in range(numEntries)}
    binI = 0
    while(binI<numBin):
        priorDict = defaultdict(list)
        # find min value given priority number 
        # print(testData)
        for j in range(numEntries):
            # print(testData[j][binI],j,binI)
            if(testData[j][binI]==1):
                priorDict[priorityArr[j]].append((j,dfData[j][binI]))
        # print(binI,priorDict)
        try:
            minPriority = min(priorDict.keys())
            minVal = 2**numBin
            minArr = []
            for itemNum,itemVal in priorDict[minPriority]:
                if(itemVal<minVal):
                    minVal = itemVal
                    minArr = [itemNum]
                elif(itemVal==minVal):
                    minArr.append(itemNum)
            entries,numIslands = getNumIslands(minArr,binI,testData)
            entry = entries[randint(0,len(entries)-1)]
            # find min number of islands
            # print(numIslands)
            priorityArr[entry]+=numIslands
            for _ in range(numIslands):
                finalData.append(entry)

            binI+=numIslands

        # randomly select one
        except ValueError:
            finalData.append(None)
            binI+=1
    return finalData

def PDI2(testData):
    # calculate DF for each
    finalData = []
    dfData = getD(testData)
    numEntries = len(dfData)
    priorityArr = {k:0 for k in range(numEntries)}
    binI = 0
    while(binI<numBin):
        priorDict = defaultdict(list)
        # find min value given priority number 
        for j in range(numEntries):
            # print(testData[j][binI],j,binI)
            if(testData[j][binI]==1):
                priorDict[priorityArr[j]].append((j,dfData[j][binI]))
        # print(binI,priorDict)
        try:
            minPriority = min(priorDict.keys())
            minVal = 2**numBin
            minArr = []
            for itemNum,itemVal in priorDict[minPriority]:
                if(itemVal<minVal):
                    minVal = itemVal
                    minArr = [itemNum]
                elif(itemVal==minVal):
                    minArr.append(itemNum)
            entries,numIslands = getNumIslands(minArr,binI,testData)
            entry = entries[randint(0,len(entries)-1)]
            # find min number of islands
            # print(numIslands)
            N = 2
            if(numIslands>N-1):
                numIslands = N
            priorityArr[entry]+=numIslands
            for _ in range(numIslands):
                finalData.append(entry)
            binI+=numIslands
        # randomly select one
        except ValueError:
            finalData.append(None)
            binI+=1
    return finalData

def PDF(testData):
    finalData = []
    dfData = getDF(testData)
    numEntries = len(dfData)
    priorityArr = {k:0 for k in range(numEntries)}
    binI = 0
    while(binI<numBin):
        priorDict = defaultdict(list)
        # find min value given priority number 
        # print(testData)
        for j in range(numEntries):
            # print(testData[j][binI],j,binI)
            if(testData[j][binI]==1):
                priorDict[priorityArr[j]].append((j,dfData[j][binI]))
        # print(binI,priorDict)
        try:
            minPriority = min(priorDict.keys())
            minVal = 2**numBin
            minArr = []
            for itemNum,itemVal in priorDict[minPriority]:
                if(itemVal<minVal):
                    minVal = itemVal
                    minArr = [itemNum]
                elif(itemVal==minVal):
                    minArr.append(itemNum)
            entry = minArr[randint(0,len(minArr)-1)]
            priorityArr[entry]+=1
            finalData.append(entry)
            binI+=1

        # randomly select one
        except ValueError:
            finalData.append(None)
            binI+=1

    return finalData

def getNumIslands(arr,binNum,testData):
    minVal = 2**numBin
    # minVal = 0
    minItemArr = []
    for item in arr:
        testVal = getIsland(item,binNum,testData)
        if(testVal<minVal):
        # if(testVal>minVal):
            minVal = testVal
            minItemArr = [item]
        if(testVal==minVal):
            minItemArr.append(item)
    return minItemArr,minVal

def getIsland(item,binNum,testData):
    numIslands = 0
    # print(binNum,numIslands,testData[item])
    while(
        (binNum+numIslands)<numBin
        and
        testData[item][binNum+numIslands] == 1
        ):
        numIslands+=1
        # print('test')
        # print(numIslands)
    return numIslands

def PD(testData):
    finalData = []
    dfData = getD(testData)
    numEntries = len(dfData)
    priorityArr = {k:0 for k in range(numEntries)}
    binI = 0
    while(binI<numBin):
        priorDict = defaultdict(list)
        # find min value given priority number 
        # print(testData)
        for j in range(numEntries):
            # print(testData[j][binI],j,binI)
            if(testData[j][binI]==1):
                priorDict[priorityArr[j]].append((j,dfData[j][binI]))
        # print(binI,priorDict)
        try:
            minPriority = min(priorDict.keys())
            minVal = 2**numBin
            # minVal = 0
            minArr = []
            for itemNum,itemVal in priorDict[minPriority]:
                if(itemVal<minVal):
                # if(itemVal > minVal):
                    minVal = itemVal
                    minArr = [itemNum]
                elif(itemVal==minVal):
                    minArr.append(itemNum)
            entry = minArr[randint(0,len(minArr)-1)]
            priorityArr[entry]+=1
            finalData.append(entry)
            binI+=1

        # randomly select one
        except ValueError:
            finalData.append(None)
            binI+=1

    return finalData

def PDR(testData):
    finalData = []
    dfData = getD(testData)
    numEntries = len(dfData)
    priorityArr = {k:0 for k in range(numEntries)}
    binI = 0
    while(binI<numBin):
        priorDict = defaultdict(list)
        # find min value given priority number 
        # print(testData)
        for j in range(numEntries):
            # print(testData[j][binI],j,binI)
            if(testData[j][binI]==1):
                priorDict[priorityArr[j]].append((j,dfData[j][binI]))
        # print(binI,priorDict)
        try:
            minPriority = min(priorDict.keys())
            # minVal = 2**numBin
            minVal = 0
            minArr = []
            for itemNum,itemVal in priorDict[minPriority]:
                # if(itemVal<minVal):
                if(itemVal > minVal):
                    minVal = itemVal
                    minArr = [itemNum]
                elif(itemVal==minVal):
                    minArr.append(itemNum)
            entry = minArr[randint(0,len(minArr)-1)]
            priorityArr[entry]+=1
            finalData.append(entry)
            binI+=1

        # randomly select one
        except ValueError:
            finalData.append(None)
            binI+=1

    return finalData

def hue(data):
    j = defaultdict(int)
    huer = 0
    for item in data:
        j[item]+=1
        huer+=1/j[item]
    return huer/len(data)

def main():
    testCases = 40
    seed(int(time.time())**2)
    avgPDFI2 = 0
    avgPDFI = 0
    avgPDF = 0
    avgPD = 0
    avgPDR = 0
    numIter = 10
    for i in range(1,numIter+1):
        data = getTestData(testCases)
        avgPDFI2+=hue(PDI2(data))
        avgPDFI+=hue(PDI(data))
        avgPDF+=hue(PDF(data))
        avgPD+=hue(PD(data))
        avgPDR+=hue(PDR(data))
        # print('PDI2',avgPDFI2/(i))
        # print('PDI ',avgPDFI/(i))
        # print('PDF  ',avgPDF/(i))
        # print('PDR  ',avgPDR/(i))
        # print('PD   ',avgPD/(i))
        # print()
        # print('PDI2 ',PDI2(data))
        # print('PDI  ',PDI(data))
        # print('PDF  ',PDF(data))
        # print('PDR  ',PDR(data))
        # print('PD   ',PD(data))

    print()
    print('PDI2',avgPDFI2/(numIter))
    print('PDI ',avgPDFI/(numIter))
    print('PDF ',avgPDF/(numIter))
    print('PDR ',avgPDR/(numIter))
    print('PD  ',avgPD/(numIter))

if(__name__ == '__main__'):
    main()