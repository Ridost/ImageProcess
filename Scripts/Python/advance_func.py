import numpy as np
from PIL import Image, ImageDraw, ImageFont
import sys

'''
    sys.argv[0]:
                0 -> signature
                1 -> 


========================================
               signature
========================================
sys.argv[2] = input image path.
sys.argv[3] = output image path.
sys.argv[4] = output text. (English Only and Can not contain white space!!!)
sys.argv[5] = text font size.
sys.argv[6] = text color.
                #       1 : black
                #       2 : red
                #       3 : yellow
                #       4 : green
                #       5 : bright_blue
                #       6 : blue
                #       7 : purple
                # default : white


=========================================

'''

def signature(img_path, out_path, text, size, color):
    try:
        img = Image.open(img_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nPlease check arguments again!")
        return

    w, h = img.size
    drawing = ImageDraw.Draw(img)

    sig_font = ImageFont.truetype('AAntaraDistance-OVA2e.ttf', size=size)

    # draw config

    # position down right
    (x, y) = (0, h-1.05*size)

    # color 1 : black
    #       2 : red
    #       3 : yellow
    #       4 : green
    #       5 : bright_blue
    #       6 : blue
    #       7 : purple
    # default : white

    if color == "1":
        rgb = 'rgb(0,0,0)'
    elif color == "2":
        rgb = 'rgb(255,0,0)'
    elif color == "3":
        rgb = 'rgb(255,255,0)'
    elif color == "4":
        rgb = 'rgb(0,255,0)'
    elif color == "5":
        rgb = 'rgb(0,255,255)'
    elif color == "6":
        rgb = 'rgb(0,0,255)'
    elif color == "7":
        rgb = 'rgb(255,0,255)'
    else:
        rgb = 'rgb(255,255,255)'

    out_text = text

    drawing.text((x, y), out_text, fill=rgb, font=sig_font)

    out = img

    try:
        out.save(out_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nYou lack output file name!")
        return


if __name__ == "__main__":
    if sys.argv[1] == "0":
        signature(sys.argv[2], sys.argv[3], sys.argv[4], int(sys.argv[5]), sys.argv[6])
    else:
        print("Wrong function choose number, please check again.")
