import numpy as np
from PIL import Image
import sys

'''
sys.argv[1] = function choose. (
                                0: rgb2gray
                                1: neg_effect
                                2: carbon
                                3: cold felling
                                4: warm felling
                                )
sys.argv[2] = input image path.
sys.argv[3] = output image path.


'''


def rgb2gray(img_path, out_path):
    try:
        img = Image.open(img_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nPlease check arguments again!")
        return
    img = np.array(img)
    grayout = img

    R = img[:, :, 0]
    G = img[:, :, 1]
    B = img[:, :, 2]

    gray = (0.3*R + 0.59*G + 0.11*B)
    gray = np.rint(gray)
    gray = gray.astype(int)

    for i in range(3):
        grayout[:, :, i] = gray

    out = Image.fromarray(grayout)

    try:
        out.save(out_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nYou lack output file name!")
        return


def neg_effect(img_path, out_path):
    try:
        img = Image.open(img_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nPlease check arguments again!")
        return
    img = np.array(img)

    img[:, :, 0] = (255 - img[:, :, 0])
    img[:, :, 1] = (255 - img[:, :, 1])
    img[:, :, 2] = (255 - img[:, :, 2])

    out = Image.fromarray(img)

    try:
        out.save(out_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nYou lack output file name!")
        return


def carbon(img_path, out_path):
    try:
        img = Image.open(img_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nPlease check arguments again!")
        return

    img = np.array(img)
    gray_out = img

    R = img[:, :, 0]
    G = img[:, :, 1]
    B = img[:, :, 2]

    gray = (0.3*R + 0.59*G + 0.11*B)
    gray = np.rint(gray)
    gray = gray.astype(int)

    for i in range(3):
        gray_out[:, :, i] = (gray//128)*255

    out = Image.fromarray(gray_out)

    try:
        out.save(out_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nYou lack output file name!")
        return


def cold(img_path, out_path):
    try:
        img = Image.open(img_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nPlease check arguments again!")
        return
    img = np.array(img)

    img[:, :, 0] = (img[:, :, 0] * 0.65)
    img[:, :, 1] = (img[:, :, 1] * 0.75)
    img[:, :, 2] = (img[:, :, 2] * 1)
    out = Image.fromarray(img)

    try:
        out.save(out_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nYou lack output file name!")
        return


def warm(img_path, out_path):
    try:
        img = Image.open(img_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nPlease check arguments again!")
        return
    img = np.array(img)

    img[:, :, 0] = (img[:, :, 0] * 0.99)
    img[:, :, 1] = (img[:, :, 1] * 0.95)
    img[:, :, 2] = (img[:, :, 2] * 0.75)
    out = Image.fromarray(img)

    try:
        out.save(out_path)
    except FileNotFoundError as f:
        print(f, "\nPlease check file name or path again!")
        return
    except IndexError as i:
        print(i, "\nYou lack output file name!")
        return


def main():
    if sys.argv[1] == "0":
        rgb2gray(sys.argv[2], sys.argv[3])

    elif sys.argv[1] == "1":
        neg_effect(sys.argv[2], sys.argv[3])

    elif sys.argv[1] == "2":
        carbon(sys.argv[2], sys.argv[3])

    elif sys.argv[1] == "3":
        cold(sys.argv[2], sys.argv[3])

    elif sys.argv[1] == "4":
        warm(sys.argv[2], sys.argv[3])

    else:
        print("Wrong function choose argument, please check again.")


if __name__ == '__main__':
    main()
