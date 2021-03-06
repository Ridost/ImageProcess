import argparse
from PIL import Image
import sys

"""
argv[1] : image path
argv[2] : brightness value [-255 - 255]
argv[3] : output image path
----------------------
for specific range
argv[4], argv[5] : coordinate width, height
argv[6], argv[7] : width, height

--------------------------
python brightness.py 1.png 50 out.png
python brightness.py 1.png 50 out.png 100 100 200 200
"""

def Contrast(pixel, contrast=1):
    
    factor = (259 * (contrast + 255)) / (255 * (259 - contrast))

    def calculate(value):
        value = (factor * value - 128) + 128
        return value
    
    return (int(calculate(pixel[0])), int(calculate(pixel[1])), int(calculate(pixel[2])))
    

def main():

    try:
        image = Image.open(sys.argv[1])
        gamma_value = float(sys.argv[2])
        output_path = sys.argv[3]
    except FileNotFoundError as e:
        print(e)
        return
    except IndexError as e:
        print(e)
        return

    coordinate = (0, 0)

    height = image.height
    width = image.width

    if len(sys.argv) > 4:
        coordinate = ((int)(sys.argv[4]), (int)(sys.argv[5]))

        height = int(sys.argv[6])
        width = int(sys.argv[7])
    
    pixels = image.load()

    for h in range(height):
        for w in range(width):
            x = min(coordinate[0] + w, image.width - 1)
            y = min(coordinate[1] + h, image.height - 1)

            pixels[x, y] = Contrast(pixels[x, y], gamma_value)
            
    
    image.save(output_path)
    image.close()

if __name__ == "__main__":
    main()

