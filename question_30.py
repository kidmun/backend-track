counter = 0
for num in range(100, 201):
    
    if (num %6==0 and num %5!=0) or (num%5==0 and num%6!=0):
        counter += 1
        print(num, end=(" " if counter < 10 else "\n"))
        if counter == 10:
            counter = 0

